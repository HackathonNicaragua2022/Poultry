using accesoDatos;
using accesoDatos.ModuloLogistica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace POULTRY.ModuloLogistica
{
    public partial class galeriaVehiculos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_NombreVehiculo.Text = (string)Session["#PlacaGaleria"];
            fillGaleria();
        }
        public void fillGaleria()
        {
            try
            {
                GaleriaImagenesPorVehiculo nuevaImagen = new GaleriaImagenesPorVehiculo();
                string Placa = (string)Session["#PlacaGaleria"];

                rp_imagenes.DataSource = nuevaImagen.getAll(Placa);
                rp_imagenes.DataBind();
            }
            catch (Exception)
            {
            }
        }
        protected void btn_GuardarNuevo_Click(object sender, EventArgs e)
        {
            if (txt_descripcion.Text.Length > 0)
            {
                string descripcion = txt_descripcion.Text;
                string URLIMagen = UploadImagen(ip_circulacionc1, "img");
                string Placa = (string)Session["#PlacaGaleria"];
                var membershipUser = System.Web.Security.Membership.GetUser();
                Guid userId = (Guid)membershipUser.ProviderUserKey;

                GaleriaImagenesPorVehiculo nuevaImagen = new GaleriaImagenesPorVehiculo();
                string result = nuevaImagen.subirImagen(Placa, URLIMagen, userId, descripcion);
                if (result.Equals("1"))
                {
                    txt_descripcion.Text = "";
                    fillGaleria();
                    up_principal.Update();
                }
                else
                {
                    AlertaNuevo(tiposAlertaCss_bstrap.error, result);
                }
            }
            else
            {
                AlertaNuevo(tiposAlertaCss_bstrap.advertencia, "No deje el campo de Descripcion vacio!!");
            }
        }
        public void AlertaNuevo(String tipoAdvertencia, String Mensaje)
        {
            alerta_nuevaImagen.Visible = true;
            alerta_nuevaImagen.Attributes["Class"] = tipoAdvertencia;
            lbl_NuevoMensaje.Text = Mensaje;
        }
        protected void btn_NuevaImagen_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal_Nuevo", "ShowModalNuevo();", true);
            up_principal.Update();
        }
        public String UploadImagen(HtmlInputFile input, string tag)
        {
            String url = "https://ayemadeoro.com/ModuloLogistica/ImagenesGal/";
            try
            {
                if (input.PostedFile != null)
                {
                    string ext = System.IO.Path.GetExtension(input.PostedFile.FileName);
                    string nombreArchivo = tag + "-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ext;
                    string rutaGuardado = Server.MapPath("~/ModuloLogistica/ImagenesGal/" + nombreArchivo);
                    input.PostedFile.SaveAs(rutaGuardado);
                    return url + nombreArchivo;
                }
                else
                {
                    return "https://ayemadeoro.com/imagenes/nophoto.png";
                }

            }
            catch (Exception)
            {
                return "https://ayemadeoro.com/imagenes/nophoto.png";
            }
            // Display the result of the upload.
            //frmConfirmation.Visible = true;
        }

        protected void rp_imagenes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Eliminar"))
            {
                int IDIMagen = Convert.ToInt32(e.CommandArgument.ToString());
                GaleriaImagenesPorVehiculo nuevaImagen = new GaleriaImagenesPorVehiculo();
                string result = nuevaImagen.eliminarImagenDetalle(IDIMagen);
                fillGaleria();
            }
            if (e.CommandName.Equals("cmd_Bajar"))
            {
                //Path of the File to be downloaded.
                string URL = e.CommandArgument.ToString();
                string nameFile = URL.Substring(URL.LastIndexOf('/'));
                Response.ContentType = "application/unknown";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nameFile);
                Response.TransmitFile(Server.MapPath("~/ModuloLogistica/ImagenesGal/" + nameFile));
                Response.End();
            }
        }
    }
}