using accesoDatos;
using accesoDatos.ModuloLogistica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;
namespace POULTRY.ModuloLogistica
{
    public partial class proveedoresCombustible : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fillProveedores();
        }
        public void fillProveedores()
        {
            Proveedor_obj proveedores = new Proveedor_obj();
            rp_Proveedores.DataSource = proveedores.getAll();
            rp_Proveedores.DataBind();
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
        protected void mostrarPanel_Click(object sender, EventArgs e)
        {
            ingresoProveedorPanel.Visible = !ingresoProveedorPanel.Visible;
        }

        protected void btn_GuardarNuevo_Click(object sender, EventArgs e)
        {

            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_NombreProveedor, txt_Direccion))
            {
                string nombre = txt_NombreProveedor.Text;
                string direccion = txt_Direccion.Text;
                bool activo = chk_Activo_0.Checked;

                Proveedor_obj nuevoP = new Proveedor_obj();
                string result = nuevoP.nuevoProveedor(nombre, direccion, activo);
                if (result.Equals("1"))
                {
                    ingresoProveedorPanel.Visible = false;
                    txt_NombreProveedor.Text = "";
                    txt_Direccion.Text = "";
                    chk_Activo_0.Checked = false;
                    fillProveedores();
                    mensaje("Proveedor ingresado Correctamente!!", tiposAdvertencias.exito);
                }
                else
                {
                    mensaje("Error ingresando el Proveedor </br>ERROR: " + result, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("No deje Campos Vacios", tiposAdvertencias.advertencia);
            }
        }

        protected void btn_GuardarCambios_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_NombreProveedor, txt_Direccion))
            {
                string nombre = txt_NombreProveedor.Text;
                string direccion = txt_Direccion.Text;
                bool activo = chk_Activo_0.Checked;
                int IDProveedor = (int)Session["id_proveedore_mod"];

                Proveedor_obj nuevoP = new Proveedor_obj();
                string result = nuevoP.update(IDProveedor, nombre, direccion, activo);
                if (result.Equals("1"))
                {
                    ingresoProveedorPanel.Visible = false;
                    txt_NombreProveedor.Text = "";
                    txt_Direccion.Text = "";
                    chk_Activo_0.Checked = false;
                    fillProveedores();
                    mensaje("Proveedor fue modificado Correctamente!!", tiposAdvertencias.exito);
                }
                else
                {
                    mensaje("Error Editando el Proveedor </br>ERROR: " + result, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("No deje Campos Vacios", tiposAdvertencias.advertencia);
            }
        }

        protected void rp_Proveedores_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Editar"))
            {
                int id_proveedore = int.Parse(e.CommandArgument.ToString());
                Session["id_proveedore_mod"] = id_proveedore;

                Proveedor_obj proveedor = new Proveedor_obj();
                Cat_ProveedorCombustible editarPro = proveedor.getbyID(id_proveedore);
                txt_NombreProveedor.Text = editarPro.NombreProveedor;
                txt_Direccion.Text = editarPro.DireccionProveedor;
                chk_Activo_0.Checked = (bool)editarPro.Activo;
                ingresoProveedorPanel.Visible = true;
            }
            else if (e.CommandName.Equals("cmd_remover"))
            {
                int id_proveedore = int.Parse(e.CommandArgument.ToString());
                Session["id_proveedore_mod"] = id_proveedore;
                ScriptManager.RegisterStartupScript(this, GetType(), "Show_Modal_Advertencia", "ShowModal_Advertencia();", true);
            }
        }

        protected void btn_eliminarProveedor_Click(object sender, EventArgs e)
        {
            if (Session["id_proveedore_mod"] != null)
            {
                int IDProveedor = (int)Session["id_proveedore_mod"];

                Proveedor_obj nuevoP = new Proveedor_obj();
                string result = nuevoP.delete(IDProveedor);
                if (result.Equals("1"))
                {
                    txt_NombreProveedor.Text = "";
                    txt_Direccion.Text = "";
                    chk_Activo_0.Checked = false;
                    ingresoProveedorPanel.Visible = false;
                    fillProveedores();
                    mensaje("Proveedor fue modificado Correctamente!!", tiposAdvertencias.exito);
                }
                else
                {
                    mensaje("Error Editando el Proveedor </br>ERROR: " + result, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("Seleccione nuevamene el proveedor para continuar!!", tiposAdvertencias.error);
            }
        }

    }
}