using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.Catalogos;
namespace POULTRY.facturadorescomedor
{
    public partial class IngresoComedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.GC.Collect();//limpiar memoria ram                
                dr_ProductosEninventario.DataSource = new Tbl_Inventario_obj().ProductosEnInventario(0);
                dr_ProductosEninventario.DataTextField = "NombreProducto";
                dr_ProductosEninventario.DataValueField = "ID_Inventario";
                dr_ProductosEninventario.DataBind();
                int IDInv = int.Parse(dr_ProductosEninventario.SelectedValue);
                CargarDetallesProducto(IDInv);
                //----------------------------------------------------------------------
                dr_Proveedores.DataSource = new Tbl_Proveedores_obj().getAllProveedores();
                dr_Proveedores.DataTextField = "NombreEmpresa";
                dr_Proveedores.DataValueField = "ID_Proveedores";
                dr_Proveedores.DataBind();
                if (dr_Proveedores.Items.Count < 0)
                {
                    panelP.Visible = false;
                }
                //---------------------------------------------------------------------
                Session["ListaProductosCompra"] = new List<Tbl_DetalleCompraFacturando>();
                //---------------------------------------------------------------------
                lbl_FechaIngreso.Text = DateTime.Now.ToLongDateString();
            }
            fillDetalle();
        }

        protected void btn_AgregarALista_Click(object sender, EventArgs e)
        {
            if (dr_ProductosEninventario.Items.Count > 0)
            {
                if (txt_CantidadIngresar.Text.Length > 0)
                {
                    //Variables
                    int ID_Inventario = int.Parse(dr_ProductosEninventario.SelectedValue);
                    string nombreProducto = dr_ProductosEninventario.SelectedItem.ToString();
                    int Cantidad = int.Parse(txt_CantidadIngresar.Text);
                    decimal CostoCompra = decimal.Parse(txt_costodeCompra.Text);
                    decimal VentaCompra = decimal.Parse(txt_PrecioVenta.Text);
                    string comentario = txt_Comentario.Text;
                    Boolean habilitadoVenta = chk_habilitar.Checked;
                    //-------------------------
                    if (CostoCompra > 0 && VentaCompra > 0)
                    {
                        Tbl_DetalleCompraFacturando dfac = new Tbl_DetalleCompraFacturando();
                        dfac.ID_Inventario = ID_Inventario;
                        dfac.Cantidad = Cantidad;
                        dfac.CosteUnidad = CostoCompra;//Costo de Compra
                        dfac.PrecioUnidad = VentaCompra;//Costo de Compra
                        dfac.Observaciones = comentario;
                        //dfac.NombreProducto = new Tbl_Inventario_obj().getProdcutoByInventarioID(ID_Inventario).NombreProducto;
                        dfac.NombreProducto = nombreProducto;
                        dfac.HabilatoVenta = habilitadoVenta;
                        List<Tbl_DetalleCompraFacturando> detalle = (List<Tbl_DetalleCompraFacturando>)Session["ListaProductosCompra"];
                        detalle.Add(dfac);
                        fillDetalle();
                        ///---------------------------------
                        txt_CantidadIngresar.Text = "";
                        txt_costodeCompra.Text = "";
                        txt_PrecioVenta.Text = "";
                        txt_Comentario.Text = "";
                        btn_Guardar.Visible = true;
                    }
                    else
                    {
                        modalInfo("Costo de Compra y precio de Venta tienen que ser mayores a 0", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    modalInfo("debe ingresar una cantidad para el producto", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modalInfo("No hay productos en Inventario", tiposAdvertencias.advertencia);
            }
        }

        protected void btn_DetallesP_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Modaldet", "ShowModalDet();", true);
        }
        public void CargarDetallesProducto(int IDInventario)
        {
            Tbl_Inventario pIventario = new Tbl_Inventario_obj().findByIdInventario(IDInventario);
            //---------------
            txt_costodeCompra.Text = pIventario.CostoCompra.ToString();
            txt_PrecioVenta.Text = pIventario.PrecioVenta.ToString();
            txt_EntradasTotales.Text = pIventario.CantidadEntrante.ToString();
            txt_VentasTotales.Text = pIventario.CantidadSaliente.ToString();
            txt_ExistenciaActual.Text = pIventario.ExistenciaActual.ToString();
            chk_habilitar.Checked = pIventario.HabilitadoParaVenta;
        }

        protected void dr_ProductosEninventario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dr_ProductosEninventario.Items.Count > 0)
            {
                int IDInv = int.Parse(dr_ProductosEninventario.SelectedValue);
                CargarDetallesProducto(IDInv);
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

        public void fillDetalle()
        {
            List<Tbl_DetalleCompraFacturando> listaDetalle = (List<Tbl_DetalleCompraFacturando>)Session["ListaProductosCompra"];
            gv_productos.DataSource = listaDetalle;
            gv_productos.DataBind();
            if (gv_productos.Rows.Count > 0)
            {
                this.gv_productos.UseAccessibleHeader = true;
                TableCellCollection cells = gv_productos.HeaderRow.Cells;
                gv_productos.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //  cells[1].Attributes.Add("data-breakpoints", "all");
                //   cells[2].Attributes.Add("data-breakpoints", "all");
                //   cells[5].Attributes.Add("data-breakpoints", "all");
                gv_productos.HeaderRow.TableSection = TableRowSection.TableHeader;

            }

        }
        protected void gv_productos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "remover")
            {

                gv_productos.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IdInventario = Convert.ToInt32(gv_productos.SelectedValue);
                List<Tbl_DetalleCompraFacturando> detalle = (List<Tbl_DetalleCompraFacturando>)Session["ListaProductosCompra"];
                Tbl_DetalleCompraFacturando remover = detalle.Where(s => s.ID_Inventario == IdInventario).FirstOrDefault();
                detalle.Remove(remover);
                fillDetalle();
                if (detalle.Count <= 0)
                {
                    btn_Guardar.Visible = false;
                }
            }
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (Session["ListaProductosCompra"] != null)
            {
                List<Tbl_DetalleCompraFacturando> detalle = (List<Tbl_DetalleCompraFacturando>)Session["ListaProductosCompra"];
                if (detalle.Count > 0)
                {
                    // Variales
                    int ID_Proveedor = int.Parse(dr_Proveedores.SelectedValue);
                    DateTime fechaIngreso = DateTime.Now;
                    var membershipUser = System.Web.Security.Membership.GetUser();
                    Guid userId = (Guid)membershipUser.ProviderUserKey;
                    String Referencia = txt_Referencia.Text;
                    //-------------------------------------------
                    ingresoInventarioComedor ingresInventario = new ingresoInventarioComedor();
                    // GUARDA EL INGREO DEL PRODUCTO EN EL INVENTARIO
                    string result = ingresInventario.guardarCompraComedor(ID_Proveedor,
                        fechaIngreso,
                        userId,
                        Referencia,
                        detalle);
                    if (result.Equals("1"))
                    {
                        Session["ListaProductosCompra"] = null;
                        fillDetalle();
                        ///---------------------------------
                        txt_CantidadIngresar.Text = "";
                        txt_costodeCompra.Text = "";
                        txt_PrecioVenta.Text = "";
                        txt_Comentario.Text = "";
                        txt_Referencia.Text = string.Empty;
                        btn_Guardar.Visible = false;
                        modalInfo("La actualizacion del inventario se ha realizado de manera exitosa!!", tiposAdvertencias.informacion);
                    }
                    else
                    {
                        modalInfo("Ocurrio un error: <br/>" + result, tiposAdvertencias.error);
                    }
                }
                else
                {
                    modalInfo("Debe agregar productos primero, antes de guardar", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modalInfo("Debe agregar productos primero, antes de guardar", tiposAdvertencias.advertencia);
            }
        }
    }
}