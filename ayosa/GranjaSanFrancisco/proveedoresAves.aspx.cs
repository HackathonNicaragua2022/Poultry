using accesoDatos;
using accesoDatos.Granja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;
namespace POULTRY.GranjaAvicola
{
    public partial class proveedoresAves : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
            fill_Proveedor();
        }
        public void fill_Proveedor()
        {
            proveedoresAvesEngorde_obj proveedores = new proveedoresAvesEngorde_obj();
            rp_proveedoresAves.DataSource = proveedores.getAllProveedores();
            rp_proveedoresAves.DataBind();
        }
        public void mensaje(string mensaje, tiposAdvertencias alerta)
        {
            switch (alerta)
            {
                case tiposAdvertencias.informacion:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "info_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.exito:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "success_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.advertencia:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "advertencia_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.error:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "error_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.pregunta:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "pregunta_alert('" + mensaje + "')", true);
                    break;
                default:
                    break;
            }
        }

        protected void gv_Proveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btn_AgregarProv_Click(object sender, EventArgs e)
        {
            nuevoProveedor.Visible = !nuevoProveedor.Visible;
            txt_NombreProveedor.Focus();
        }

        protected void btn_NuevoProveedor_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_NombreProveedor, txt_Procedencia, txt_Representante))
            {
                //new TextboxValidator().setFormControl(txt_NombreProveedor, txt_Procedencia, txt_Representante);
                div_alertMarquee.InnerHtml = "";
                string nombreProveedor = txt_NombreProveedor.Text;
                string procedencia = txt_Procedencia.Text;
                string representantes = txt_Representante.Text;
                string telefono = txt_TelefonoContacto.Text;

                tbl_ProveedoresAvesEngorde _nuevoProveedor = new tbl_ProveedoresAvesEngorde();
                _nuevoProveedor.Nombre_Proveedor = nombreProveedor;
                _nuevoProveedor.Procedencia = procedencia;
                _nuevoProveedor.Representante = representantes;
                _nuevoProveedor.TelefonoContacto = telefono;
                _nuevoProveedor.Activo = chk_activo.Checked;
                string result = new proveedoresAvesEngorde_obj().nuevo(_nuevoProveedor);
                if (result.Equals("1"))
                {
                    txt_NombreProveedor.Text = "";
                    txt_Procedencia.Text = "";
                    txt_Representante.Text = "";
                    txt_TelefonoContacto.Text = "";
                    nuevoProveedor.Visible = false;
                    fill_Proveedor();
                    mensaje("Guardado Correctamente!!", tiposAdvertencias.exito);
                }
                else
                {
                    mensaje("Error Guardando!!", tiposAdvertencias.error);
                    div_alertMarquee.InnerHtml = Boostrap4Utill.getBloc_Alert(TiposAdvertenciasBoostrap.ERROR, "Error", "No se a podido guardar el proveedor: " + result);
                }
            }
            else
            {
                div_alertMarquee.InnerHtml = Boostrap4Utill.getBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Campos Vacios", "No deje los Campos: Nombre Proveedor, Pais o Precedencia, Representante o contacto Vacios!!");
            }
        }
        protected void btn_GuardarCambiosEnProveedor_Click(object sender, EventArgs e)
        {
            if (Session["IDProveedorAE"] != null)
            {
                if (new TextboxValidator().validarLargoCadena_mayorCero(txt_NombreProveedor, txt_Procedencia, txt_Representante))
                {
                    //new TextboxValidator().setFormControl(txt_NombreProveedor, txt_Procedencia, txt_Representante);
                    div_alertMarquee.InnerHtml = "";
                    string nombreProveedor = txt_NombreProveedor.Text;
                    string procedencia = txt_Procedencia.Text;
                    string representantes = txt_Representante.Text;
                    string telefono = txt_TelefonoContacto.Text;
                    int idProveedor = (int)Session["IDProveedorAE"];
                    bool activo = chk_activo.Checked;
                    string result = new proveedoresAvesEngorde_obj().editarProveedor(idProveedor, nombreProveedor, procedencia, telefono, representantes, activo);
                    if (result.Equals("1"))
                    {
                        txt_NombreProveedor.Text = "";
                        txt_Procedencia.Text = "";
                        txt_Representante.Text = "";
                        txt_TelefonoContacto.Text = "";
                        nuevoProveedor.Visible = false;
                        btn_GuardarCambiosEnProveedor.Visible = false;
                        btn_GuardarNuevoProveedor.Visible = true;
                        fill_Proveedor();
                        mensaje("Se guardo los Cambios Correctamente!!", tiposAdvertencias.exito);
                    }
                    else
                    {
                        mensaje("Error Guardando los Cambios!!", tiposAdvertencias.error);
                        div_alertMarquee.InnerHtml = Boostrap4Utill.getBloc_Alert(TiposAdvertenciasBoostrap.ERROR, "Error", "No se a podido guardar los Cambios en el proveedor: " + result);
                    }
                }
                else
                {
                    div_alertMarquee.InnerHtml = Boostrap4Utill.getBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Campos Vacios", "No deje los Campos: Nombre Proveedor, Pais o Precedencia, Representante o contacto Vacios!!");
                }
            }
        }

        protected void btn_CancelarVentanaEdicion_Click(object sender, EventArgs e)
        {
            nuevoProveedor.Visible = false;
        }

        protected void rp_proveedoresAves_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Eliminar"))
            {
                int ID_Proveedor = int.Parse(e.CommandArgument.ToString());
                proveedoresAvesEngorde_obj proveedores = new proveedoresAvesEngorde_obj();
                string respuesta = proveedores.eliminarProveedor(ID_Proveedor);
                if (respuesta.Equals("1"))
                {
                    fill_Proveedor();
                    mensaje("El proveedor fue eliminado correctamente!!", tiposAdvertencias.exito);
                    div_alertMarquee.InnerHtml = Boostrap4Utill.getBloc_Alert(TiposAdvertenciasBoostrap.EXITO, "Eliminado", "El Proveedor fue eliminado correctamente: ");
                }
                else
                {
                    mensaje("Error al Eliminar el Proveedor!!", tiposAdvertencias.exito);
                    div_alertMarquee.InnerHtml = Boostrap4Utill.getBloc_Alert(TiposAdvertenciasBoostrap.EXITO, "Eliminado", "El Proveedor fue eliminado correctamente: ");
                }
            }
            else if (e.CommandName.Equals("cmd_Editar"))
            {
                int ID_Proveedor = int.Parse(e.CommandArgument.ToString());
                proveedoresAvesEngorde_obj proveedores = new proveedoresAvesEngorde_obj();
                tbl_ProveedoresAvesEngorde proveedoredit = proveedores.getProveedor(ID_Proveedor);
                txt_NombreProveedor.Text = proveedoredit.Nombre_Proveedor;
                txt_Procedencia.Text = proveedoredit.Procedencia;
                txt_Representante.Text = proveedoredit.Representante;
                txt_TelefonoContacto.Text = proveedoredit.TelefonoContacto;
                chk_activo.Checked = (bool)proveedoredit.Activo;
                btn_GuardarCambiosEnProveedor.Visible = true;
                btn_GuardarNuevoProveedor.Visible = false;
                nuevoProveedor.Visible = true;
                Session["IDProveedorAE"] = ID_Proveedor;
            }
        }

    }
}