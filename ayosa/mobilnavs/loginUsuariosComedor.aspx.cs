using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos.Comedor.Control_Comedor;
namespace POULTRY.mobilnavs
{
    public partial class loginUsuariosComedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Ingresar_Click(object sender, EventArgs e)
        {
            if (txt_contrasena.Text.Length > 0 && txt_codigo.Text.Length > 0)
            {
                loginUsuarioComedor_obj usuario = new loginUsuarioComedor_obj();
                lbl_mensaje.Visible = true;
                string result = usuario.verificarUsuario(txt_codigo.Text, txt_contrasena.Text);
                if (result.Equals("1"))
                {
                    int IDTrabajador = usuario.getIDUsuarioComedor(txt_codigo.Text, txt_contrasena.Text);
                    Session["IDTrabajador"] = IDTrabajador;
                    Response.Redirect("/mobilnavs/menuComedorTrabajadores.aspx");
                }
                else if (result.Equals("0"))
                {
                    int IDTrabajador = usuario.getIDUsuarioComedor(txt_codigo.Text, txt_contrasena.Text);
                    Session["IDTrabajador"] = IDTrabajador;
                    Response.Redirect("/mobilnavs/cambioContrasenausuarioscomedor.aspx");
                }
                else if (result.Equals("-1"))
                {
                    lbl_mensaje.Text = "El usuario o Contraseña no son correctos";
                }
                else
                {
                    lbl_mensaje.Text = result;
                }
            }
            else
            {
                lbl_mensaje.Text = "No puede dejar los campos vacios!!";
            }
        }
    }
}