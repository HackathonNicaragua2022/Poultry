using accesoDatos.Comedor.Control_Comedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class cambioContrasenausuarioscomedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_GuardarContrasena_Click(object sender, EventArgs e)
        {
            lbl_mensaje.Visible = true;
            if (txt_nuevacontrasena.Text.Length > 0 && txt_repetircontrasena.Text.Length > 0)
            {
                if (txt_nuevacontrasena.Text.Equals(txt_repetircontrasena.Text))
                {
                    if (txt_nuevacontrasena.Text.Length >= 5 && txt_repetircontrasena.Text.Length >= 5)
                    {
                        loginUsuarioComedor_obj usuario = new loginUsuarioComedor_obj();
                        string result = usuario.actualizarcontrasena((int)Session["IDTrabajador"], txt_nuevacontrasena.Text);
                        if (result.Equals("1"))
                        {
                            Response.Redirect("/mobilnavs/menuComedorTrabajadores.aspx");
                        }
                        else
                        {
                            lbl_mensaje.Text = "No se pudo actualizar la contraseña, verifique he intentelo de nuevo: " + result;
                        }

                    }
                    else
                    {
                        lbl_mensaje.Text = "La contraseña debe tener minimo 5 caracteres alfanumericos";
                    }
                }
                else
                {
                    lbl_mensaje.Text = "Las contraseñas no coinciden, verifiquelas he intentelo nuevamente";
                }
            }
            else
            {
                lbl_mensaje.Text = "No puede dejar campos vacios";
            }
        }
    }
}