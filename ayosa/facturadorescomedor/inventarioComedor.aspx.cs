using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.Catalogos;
namespace POULTRY 
{
    public partial class inventarioComedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                LlenarCategorias();
                txt_FechaRegistro.Text = DateTime.Now.ToLongDateString();
            }
            CargarDatosInventario();
        }
        private void LlenarCategorias()
        {
            Cat_CategoriaProducto_obj categoriaP = new Cat_CategoriaProducto_obj();
            dr_Categorias.DataSource = categoriaP.getAllCategoriaProducto();
            dr_Categorias.DataTextField = "NombreCategoriaProducto";
            dr_Categorias.DataValueField = "ID_CategoriaProducto";
            dr_Categorias.DataBind();
            dr_Categorias.Items.Insert(0, "MOSTRAR TODO");
        }
        public void CargarDatosInventario()
        {
            Tbl_Inventario_obj invetario = new Tbl_Inventario_obj();
            int IDCategoria = 0;
            if (dr_Categorias.Items.Count > 0)
            {
                if (dr_Categorias.SelectedIndex != 0)
                {
                    IDCategoria = int.Parse(dr_Categorias.SelectedValue);
                }
            }
            List<UPS_InventarioResult> reslut = invetario.getAllInventario(true, IDCategoria);
            if (reslut.Count > 0)
            {
                if (!gv_invetario.Visible)
                {
                    gv_invetario.Visible = true;
                    panelMensaje.Visible = false;
                }
                dataCount_span.Attributes["data-count"] = reslut.Count.ToString();

                decimal costoTotal = reslut.Where(c => c.HabilitadoParaVenta).Sum(d => d.CostoCompra);
                decimal VentaTotal = reslut.Where(c => c.HabilitadoParaVenta).Sum(d => d.PrecioVenta);
                decimal utilidad = VentaTotal - costoTotal;

                lbl_VentasTotales.Text = VentaTotal.ToString();
                lbl_costoTotales.Text = costoTotal.ToString();
                lbl_Utilidad.Text = utilidad.ToString();

                gv_invetario.DataSource = reslut;
                gv_invetario.DataBind();
                gv_invetario.HeaderRow.TableSection = TableRowSection.TableHeader;

                this.gv_invetario.UseAccessibleHeader = true;
                TableCellCollection cells = gv_invetario.HeaderRow.Cells;
                gv_invetario.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //cells[1].Attributes.Add("data-breakpoints", "xs sm md");
                cells[2].Attributes.Add("data-breakpoints", "all");
                cells[3].Attributes.Add("data-breakpoints", "all");
                cells[4].Attributes.Add("data-breakpoints", "all");
                cells[5].Attributes.Add("data-breakpoints", "all");
                cells[6].Attributes.Add("data-breakpoints", "all");
                //cells[7].Attributes.Add("data-breakpoints", "all");
                cells[8].Attributes.Add("data-breakpoints", "all");
                cells[9].Attributes.Add("data-breakpoints", "all");
            }
            else
            {
                gv_invetario.Visible = false;
                panelMensaje.Visible = true;
            }
        }

        protected void dr_Categorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosInventario();
        }

        protected void gv_invetario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gv_invetario.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int ID_Inventario = int.Parse(gv_invetario.SelectedValue.ToString());
            switch (e.CommandName)
            {
                case "perdidos":
                    mostrarModalActualizacion(tiposActualizacion.ProductosPerdidos, ID_Inventario);
                    break;
                case "danados":
                    mostrarModalActualizacion(tiposActualizacion.ProductosDanados, ID_Inventario);
                    break;
                case "vencidos":
                    mostrarModalActualizacion(tiposActualizacion.ProductosVencidos, ID_Inventario);
                    break;
                default:
                    break;
            }
        }
        public enum tiposActualizacion
        {
            ProductosPerdidos = 1,
            ProductosDanados = 2,
            ProductosVencidos = 3,
        }

        public void mostrarModalActualizacion(tiposActualizacion tiposA, int ID_Inventario)
        {
            string tipoActualizacion = "";
            switch (tiposA)
            {
                case tiposActualizacion.ProductosPerdidos:
                    btn_RegistrarDanado.Visible = false;
                    btn_RegistrarVencido.Visible = false;
                    btn_Registrar.Visible = true;
                    tipoActualizacion = "Producto Perdidos";
                    break;
                case tiposActualizacion.ProductosDanados:
                    btn_RegistrarDanado.Visible = true;
                    btn_RegistrarVencido.Visible = false;
                    btn_Registrar.Visible = false;
                    tipoActualizacion = "Producto Dañados";
                    break;
                case tiposActualizacion.ProductosVencidos:
                    btn_RegistrarDanado.Visible = false;
                    btn_RegistrarVencido.Visible = true;
                    btn_Registrar.Visible = false;
                    tipoActualizacion = "Producto Vencidos";
                    break;
                default:
                    break;
            }

            lbl_TipoRegistro.Text = "Tipo de Registro: " + tipoActualizacion;
            lbl_IDInventario.Text = ID_Inventario.ToString().PadLeft(5, '0');
            Session["ID_Inventario_inventario"] = ID_Inventario;
            ScriptManager.RegisterStartupScript(this, GetType(), "modalEditar", "modal_RegistroProductos();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
        }
        protected void btn_Registrar_Click(object sender, EventArgs e)
        {
            if (txt_CantidadRegistrar.Text.Length > 0)
            {
                if (Session["ID_Inventario_inventario"] != null)
                {
                    // Variables
                    int ID_Inventario = (int)Session["ID_Inventario_inventario"];
                    DateTime fechaIngreso = DateTime.Now;
                    int cantidadRegistrar = int.Parse(txt_CantidadRegistrar.Text);
                    //-------------------------
                    if (cantidadRegistrar > 0)
                    {
                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid userId = (Guid)membershipUser.ProviderUserKey;

                        Tbl_Inventario_obj invetarioObj = new Tbl_Inventario_obj();
                        String Result = invetarioObj.ActualizarProducto_Perdido(ID_Inventario, fechaIngreso, cantidadRegistrar, userId);
                        if (Result.Equals("1"))
                        {
                            txt_CantidadRegistrar.Text = string.Empty;
                            Session["ID_Inventario_inventario"] = null;
                            CargarDatosInventario();
                            ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                            modalInfo("El inventario se actualizo correctamente", tiposAdvertencias.informacion);
                        }
                        else
                        {
                            modalInfo(Result, tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        txt_CantidadRegistrar.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                        modalInfo("Tiene que ingresar una cantidad mayor a 0!!", tiposAdvertencias.advertencia);
                    }

                    //  Response.Redirect(Request.Url.ToString(), false);
                }
            }
            else
            {
                txt_CantidadRegistrar.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                modalInfo("Tiene que ingresar una cantidad para el inventario a actualizar", tiposAdvertencias.advertencia);
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
        protected void btn_RegistrarDanado_Click(object sender, EventArgs e)
        {
            if (txt_CantidadRegistrar.Text.Length > 0)
            {
                if (Session["ID_Inventario_inventario"] != null)
                {
                    // Variables
                    int ID_Inventario = (int)Session["ID_Inventario_inventario"];
                    DateTime fechaIngreso = DateTime.Now;
                    int cantidadRegistrar = int.Parse(txt_CantidadRegistrar.Text);
                    //-------------------------
                    if (cantidadRegistrar > 0)
                    {
                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid userId = (Guid)membershipUser.ProviderUserKey;

                        Tbl_Inventario_obj invetarioObj = new Tbl_Inventario_obj();
                        String Result = invetarioObj.ActualizarProducto_Danger(ID_Inventario, fechaIngreso, cantidadRegistrar, userId);
                        if (Result.Equals("1"))
                        {
                            txt_CantidadRegistrar.Text = string.Empty;
                            Session["ID_Inventario_inventario"] = null;
                            CargarDatosInventario();
                            ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                            modalInfo("El inventario se actualizo correctamente", tiposAdvertencias.informacion);
                        }
                        else
                        {
                            modalInfo(Result, tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        txt_CantidadRegistrar.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                        modalInfo("Tiene que ingresar una cantidad mayor a 0!!", tiposAdvertencias.advertencia);
                    }

                    //  Response.Redirect(Request.Url.ToString(), false);
                }
            }
            else
            {
                txt_CantidadRegistrar.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                modalInfo("Tiene que ingresar una cantidad para el inventario a actualizar", tiposAdvertencias.advertencia);
            }
        }

        protected void btn_RegistrarVencido_Click(object sender, EventArgs e)
        {
            if (txt_CantidadRegistrar.Text.Length > 0)
            {
                if (Session["ID_Inventario_inventario"] != null)
                {
                    // Variables
                    int ID_Inventario = (int)Session["ID_Inventario_inventario"];
                    DateTime fechaIngreso = DateTime.Now;
                    int cantidadRegistrar = int.Parse(txt_CantidadRegistrar.Text);
                    //-------------------------
                    if (cantidadRegistrar > 0)
                    {
                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid userId = (Guid)membershipUser.ProviderUserKey;

                        Tbl_Inventario_obj invetarioObj = new Tbl_Inventario_obj();
                        String Result = invetarioObj.ActualizarProducto_Vencidos(ID_Inventario, fechaIngreso, cantidadRegistrar, userId);
                        if (Result.Equals("1"))
                        {
                            txt_CantidadRegistrar.Text = string.Empty;
                            Session["ID_Inventario_inventario"] = null;
                            CargarDatosInventario();
                            ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                            modalInfo("El inventario se actualizo correctamente", tiposAdvertencias.informacion);
                        }
                        else
                        {
                            modalInfo(Result, tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        txt_CantidadRegistrar.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                        modalInfo("Tiene que ingresar una cantidad mayor a 0!!", tiposAdvertencias.advertencia);
                    }

                    //  Response.Redirect(Request.Url.ToString(), false);
                }
            }
            else
            {
                txt_CantidadRegistrar.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "Reformatear_Tabla", "reformatiarTabla();", true);
                modalInfo("Tiene que ingresar una cantidad para el inventario a actualizar", tiposAdvertencias.advertencia);
            }
        }
    }
}