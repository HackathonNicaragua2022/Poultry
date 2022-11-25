using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.Catalogos;
using accesoDatos.CajaChicaComedor;
using DevExpress.Web;
namespace POULTRY 
{
    public partial class comedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["FacturandoA"] = null;
                Session["DetalleFactura"] = new List<DetalleFacturado>();
                Session["ID_CategoriaProdcuto"] = null;
                FillProductos();                
                fillData();
                fill_todaslasCategorias();
                //dr_Categoria.DataSource = new Cat_CategoriaProducto_obj().getAllCategoriaProducto();
                //dr_Categoria.DataTextField = "NombreCategoriaProducto";
                //dr_Categoria.DataValueField = "ID_CategoriaProducto";
                //dr_Categoria.DataBind();


                //if (dr_Categoria.Items.Count > 0)
                //{
                //    dr_Producto.DataSource = new Tbl_Producto_obj().findByIdCategoria(Convert.ToInt32(dr_Categoria.SelectedValue));
                //    dr_Producto.DataTextField = "NombreProducto";
                //    dr_Producto.DataValueField = "ID_Producto";
                //    dr_Producto.DataBind();
                //}                
            }
            else
            {
                FillProductos();
                //txt_codigoBarra.Focus();
                fillData();
            }

        }
       
        public void fillData()
        {
            List<DetalleFacturado> listaDetalle = (List<DetalleFacturado>)Session["DetalleFactura"];
            if (Session["DetalleFactura"] != null)
            {
                gv_DetalleFactura.Visible = true;
                gv_DetalleFactura.DataSource = listaDetalle;
                gv_DetalleFactura.DataBind();
                if (gv_DetalleFactura.Rows.Count > 0)
                {


                    this.gv_DetalleFactura.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_DetalleFactura.HeaderRow.Cells;
                    gv_DetalleFactura.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                    cells[1].Attributes.Add("data-breakpoints", "all");
                    cells[2].Attributes.Add("data-breakpoints", "all");
                    cells[5].Attributes.Add("data-breakpoints", "all");
                    gv_DetalleFactura.HeaderRow.TableSection = TableRowSection.TableHeader;

                    lbl_totalItem.Text = "  " + listaDetalle.Sum(a => a.Cantidad);
                    lbl_PrecioTotal.Text = "C$" + listaDetalle.Sum(a => a.PrecioTotal);
                }
            }
            else
            {
                gv_DetalleFactura.Visible = false;
            }
        }
        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (Session["DetalleFactura"] != null)
            {
                List<DetalleFacturado> listaDetalle = (List<DetalleFacturado>)Session["DetalleFactura"];
                if (listaDetalle.Count > 0)
                {
                    if (Session["FacturandoA"] != null)
                    {
                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid userId = (Guid)membershipUser.ProviderUserKey;

                        tbl_trabajadores empleado = (tbl_trabajadores)Session["FacturandoA"];
                        int idTrabajador = empleado.ID_Trabajador;

                        int TotalITEM = listaDetalle.Sum(a => a.Cantidad);
                        decimal totalFactura = (decimal)listaDetalle.Sum(a => a.PrecioTotal);

                        Tbl_Comedor_Obj NuevaFactura = new Tbl_Comedor_Obj();

                        // Verificar el Tipo de Venta
                        string selected = Request.Form["tipoFactura"];
                        try
                        {
                            if (selected.Equals("Credito"))// Es de Contado
                            {
                                string result = NuevaFactura.guardarVenta(idTrabajador, userId, totalFactura, TotalITEM, listaDetalle);
                                if (result.Equals("1"))
                                {
                                    lbl_totalItem.Text = "";
                                    lbl_PrecioTotal.Text = "";
                                    Session["FacturandoA"] = null;
                                    Session["DetalleFactura"] = new List<DetalleFacturado>();//new List<DetalleFacturado>();
                                    Session["ID_CategoriaProdcuto"] = null;
                                    FillProductos();
                                    //txt_codigoBarra.Focus();
                                    fillData();
                                    modalInfo("Factura Guardada Correctamente", tiposAdvertencias.exito);
                                }
                                else
                                {
                                    modalInfo("Error al Guardar la Factura: " + result, tiposAdvertencias.error);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            /*  ===== { FACTURA DE CONTADO =====}*/
                            CajaChicaComedor_obj caja = new CajaChicaComedor_obj();
                            if (caja.cajaHoy())
                            {

                                if (caja.estaAperturadaCajaHoy())
                                {// SI LA CAJA DE HOY AUN NO ESTA APERTURADA
                                    if (!caja.EstaCerradaHoy())//LA CAJA ESTA CERRADA
                                    {
                                        string result = NuevaFactura.guardarVenta_DeContado(idTrabajador, userId, totalFactura, TotalITEM, listaDetalle);
                                        if (result.Equals("1"))
                                        {
                                            lbl_totalItem.Text = "";
                                            lbl_PrecioTotal.Text = "";
                                            Session["FacturandoA"] = null;
                                            Session["DetalleFactura"] = new List<DetalleFacturado>();//new List<DetalleFacturado>();
                                            Session["ID_CategoriaProdcuto"] = null;
                                            FillProductos();
                                            //txt_codigoBarra.Focus();
                                            fillData();
                                            modalInfo("Factura de Contado Guardada Correctamente", tiposAdvertencias.exito);
                                        }
                                        else
                                        {
                                            modalInfo("Error al Guardar la Factura: " + result, tiposAdvertencias.error);
                                        }
                                    }
                                    else
                                    {
                                        modalInfo("LA CAJA CHICA DEL DIA YA ESTA CERRADA, NO SE PUEDEN HACER MAS FACTURAS DE CONTADO", tiposAdvertencias.error);
                                    }
                                }
                                else
                                {
                                    modalInfo("La Caja del Dia aun no esta aperturada, no se puede ingresar movimientos, puede aperturarla con solo entrar a la pagina de administracion  CAJA CHICA", tiposAdvertencias.error);
                                }
                            }
                            else
                            {
                                modalInfo("No se puede Guardar la factura, No hay Caja Creadas para el Dia </br> Puede Crear una Caja para el Dia de Hoy con solo entrar a la pagina de administracion  CAJA CHICA", tiposAdvertencias.error);
                            }
                        }
                    }
                    else
                    {
                        modalInfo("Porfavor ingrese el Codigo del Cliente", tiposAdvertencias.advertencia);
                    }

                }
                else
                {
                    modalInfo("No ha Cargado productos aun", tiposAdvertencias.advertencia);
                }

            }
        }
        private void modalInfo(String msg, tiposAdvertencias advertencias)
        {
            lbl_modalInfo.Text = msg;
            switch (advertencias)
            {
                case tiposAdvertencias.advertencia:
                    imagenModalInfo.Src = "~/Imagenes/warning.png";
                    break;
                case tiposAdvertencias.error:
                    imagenModalInfo.Src = "~/Imagenes/error.png";
                    break;
                case tiposAdvertencias.informacion:
                    imagenModalInfo.Src = "~/Imagenes/camera_test.png";
                    break;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "ModalInfo", "ShowModalInfo();", true);
        }
        protected void desayunos_id_Click(object sender, ImageClickEventArgs e)
        {
            Session["ID_CategoriaProdcuto"] = 2;//Desayunos       
            FillProductos();
        }
        public void FillProductos()
        {
            if (Session["ID_CategoriaProdcuto"] != null)
            {
                int idCategoria = (int)Session["ID_CategoriaProdcuto"];
                gv_productos.Visible = true;
                gv_productos.DataSource = new Tbl_Producto_obj().findByIdCategoriaParaComedor(Convert.ToInt32(idCategoria));
                gv_productos.DataBind();
                if (gv_productos.Rows.Count > 0)
                {
                    //this.gv_productos.UseAccessibleHeader = true;
                    //TableCellCollection cells = gv_productos.HeaderRow.Cells;
                    //gv_productos.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    //cells[1].Attributes.Add("data-breakpoints", "all");
                    //cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[3].Attributes.Add("data-breakpoints", "all");
                    //gv_productos.HeaderRow.TableSection = TableRowSection.TableHeader;


                    this.gv_productos.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_productos.HeaderRow.Cells;
                    gv_productos.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                    //cells[1].Attributes.Add("data-sortable", "true");
                    cells[0].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[5].Attributes.Add("data-breakpoints", "all");
                    //cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[7].Attributes.Add("data-breakpoints", "xs sm md");
                    // cells[4].Attributes.Add("data-breakpoints", "all");
                    gv_productos.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
            }
            else
            {
                gv_productos.Visible = false;
            }
        }
        protected void img_cenas_Click(object sender, ImageClickEventArgs e)
        {
            Session["ID_CategoriaProdcuto"] = 4;//Cenas
            FillProductos();
        }

        protected void img_almuerzo_Click(object sender, ImageClickEventArgs e)
        {
            Session["ID_CategoriaProdcuto"] = 3;//Almuerzos
            FillProductos();
        }

        protected void img_bebidas_Click(object sender, ImageClickEventArgs e)
        {
            Session["ID_CategoriaProdcuto"] = 7;//Bebidas
            FillProductos();
        }

        protected void img_reposteria_Click(object sender, ImageClickEventArgs e)
        {
            Session["ID_CategoriaProdcuto"] = 6;//Reposteria
            FillProductos();
        }

        protected void img_extras_Click(object sender, ImageClickEventArgs e)
        {
            Session["ID_CategoriaProdcuto"] = 5; //Extras
            FillProductos();
        }


        protected void gv_productos_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "agregar")
            {                
                gv_productos.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDProducto = Convert.ToInt32(gv_productos.SelectedValue);
                //Buscas el control ubicandolo por fila y columna, y lo agregas a un textbox  
                TextBox txtValor = (gv_productos.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].FindControl("txt_cantidad") as TextBox);

                //Obtienes el valor del textbox    
                string cantidad = txtValor.Text;
                if (cantidad.Length > 0)
                {
                    int Cantidad_pro = int.Parse(cantidad);

                    Tbl_Inventario inventario_pro = new Tbl_Inventario_obj().findById(IDProducto);
                    Tbl_Producto prodSelected = new Tbl_Producto_obj().findById(IDProducto);

                    //Verificar que hay productos en inventario e impedir el ingreso en caso de estar en 0 o negativo
                    if (inventario_pro.ExistenciaActual > 0)
                    {

                      if(Cantidad_pro<=inventario_pro.ExistenciaActual){
                          List<DetalleFacturado> listaDetalle = (List<DetalleFacturado>)Session["DetalleFactura"];
                          DetalleFacturado detalleP = new DetalleFacturado();
                          detalleP.NombreProducto = prodSelected.NombreProducto;
                          detalleP.ID_Inventario = inventario_pro.ID_Inventario;
                          detalleP.Cantidad = Cantidad_pro;
                          detalleP.PrecioUnidad = inventario_pro.PrecioVenta;
                          detalleP.CosteUnidad = (decimal)inventario_pro.CostoCompra;
                          detalleP.PrecioTotal = detalleP.Cantidad * detalleP.PrecioUnidad;
                          listaDetalle.Add(detalleP);
                          //Muestra el panel de alerta
                          InfoPanel.Visible = true;
                          lblMessage.Text = "El producto " + prodSelected.NombreProducto + " fue agregado a la lista-   TOTAL DEL PRODUCTO EN INVENTARIO (" + inventario_pro.ExistenciaActual.ToString() + ")";
                          ScriptManager.RegisterStartupScript(this, GetType(), "ocultarAlerta", "ocultarAlerta();", true);
                          fillData();
                      }
                      else
                      {
                          InfoPanel.Visible = true;
                          lblMessage.Text = "La Cantidad del producto " + prodSelected.NombreProducto + ", es mayor a la existencia actual-   TOTAL DEL PRODUCTO EN INVENTARIO (" + inventario_pro.ExistenciaActual.ToString() + ")";
                          ScriptManager.RegisterStartupScript(this, GetType(), "ocultarAlerta", "ocultarAlerta();", true);
                          ScriptManager.RegisterStartupScript(this, GetType(), "llevarArriba", "<script>function topscroll(){javascript:scroll(0,0); return false;}</script>", true);
                      }
                    }
                    else {
                        InfoPanel.Visible = true;
                        lblMessage.Text = "El producto " + prodSelected.NombreProducto + " NO TIENE  INVENTARIO-   TOTAL DEL PRODUCTO EN INVENTARIO (" + inventario_pro.ExistenciaActual.ToString() + ")";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ocultarAlerta", "ocultarAlerta();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "llevarArriba", "<script>function topscroll(){javascript:scroll(0,0); return false;}</script>", true);
                    }
                }
            }
        }

        protected void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            string nombreCliente = txt_buscar.Text;
            if (nombreCliente.Length > 0)
            {
                List<usp_BuscarClienteResult> trabajadores = new Tbl_Trabajador_obj().buscarxNombre(nombreCliente);
                if (trabajadores != null)
                {
                    listCliente.DataSource = trabajadores;
                    listCliente.DataBind();
                    Empleados.Visible = true;
                }
            }
        }

        protected void btn_SeleccionarEmpleado_Click(object sender, EventArgs e)
        {
            ListViewItem item = (sender as Button).NamingContainer as ListViewItem;
            int ID = (int)listCliente.DataKeys[item.DataItemIndex].Values["ID_Trabajador"];
            //string groupName = (string)lvGroups.DataKeys[item.DataItemIndex].Values["GroupName"];
            tbl_trabajadores trabajador = new Tbl_Trabajador_obj().getTrabajador_byID(ID);
            if (trabajador != null)
            {
                Session["FacturandoA"] = trabajador;
                lbl_NombreTrabajador.Text = trabajador.Nombre_1;
                txt_buscar.Text = "";
                //txt_codigoBarra.Focus();
                Empleados.Visible = false;
            }
        }

        protected void gv_DetalleFactura_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eliminar")
            {
                //Primero capturas la fila
                int numFila = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
                gv_DetalleFactura.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                List<DetalleFacturado> listaDetalle = (List<DetalleFacturado>)Session["DetalleFactura"];
                listaDetalle.RemoveAt(numFila);
                Session["DetalleFactura"] = listaDetalle;
                fillData();
            }
        }

        protected void glu_TodasCategorias_ValueChanged(object sender, EventArgs e)
        {
            int IDCategoria = Convert.ToInt32(glu_TodasCategorias.Value);

            Session["ID_CategoriaProdcuto"] = IDCategoria;
            FillProductos();
        }
        public void fill_todaslasCategorias() {
            Cat_CategoriaProducto_obj categorias = new Cat_CategoriaProducto_obj();
            glu_TodasCategorias.DataSource = categorias.getAllCategoriaProducto();
            glu_TodasCategorias.KeyFieldName = "ID_CategoriaProducto";
            glu_TodasCategorias.SelectionMode = GridLookupSelectionMode.Single;
            glu_TodasCategorias.DataBind();
        }
    }
}