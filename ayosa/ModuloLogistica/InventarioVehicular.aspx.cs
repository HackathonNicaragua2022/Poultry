using accesoDatos;
using accesoDatos.ModuloLogistica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ultil;

namespace POULTRY.ModuloLogistica
{
    public partial class InventarioVehicular : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillNombreModelosMarcas();
                fillSedes();
                fillCombustible();
            }
            fill_facturasPendientes();
        }
        public void fillNombreModelosMarcas()
        {
            dr_NombreVehiculos.DataSource = new NombreModelosCarros_lg_obj().getAllMarcaModeloCarro();
            dr_NombreVehiculos.DataTextField = "NombreVehiculo";
            dr_NombreVehiculos.DataValueField = "ID_MarcaModelosCarros";
            dr_NombreVehiculos.DataBind();
        }
        public void fillSedes()
        {
            dr_Sedes.DataSource = new Sedes_lg_obj().getAll();
            dr_Sedes.DataTextField = "NombreCedes";
            dr_Sedes.DataValueField = "IDCedes";
            dr_Sedes.DataBind();
        }
        public void fillCombustible()
        {
            dr_TipoCombustible.DataSource = new TipoCombustible_lg_obj().getAll();
            dr_TipoCombustible.DataTextField = "TipoCombustible";
            dr_TipoCombustible.DataValueField = "IDCombustible";
            dr_TipoCombustible.DataBind();
        }


        public void fill_facturasPendientes()
        {
            Vehiculos_lg_obj vehiculos = new Vehiculos_lg_obj();
            gv_vehiculos.DataSource = vehiculos.getAllListado();
            gv_vehiculos.DataBind();
            if (gv_vehiculos.Rows.Count > 0)
            {
                //this.gv_vehiculos.UseAccessibleHeader = true;
                //TableCellCollection cells = gv_vehiculos.HeaderRow.Cells;
                //gv_vehiculos.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                ////cells[1].Attributes.Add("data-breakpoints", "all");                
                //cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                //cells[3].Attributes.Add("data-breakpoints", "all");
                ////cells[4].Attributes.Add("data-breakpoints", "xs sm md");           
                //gv_vehiculos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


        protected void btn_Nuevo_Click(object sender, EventArgs e)
        {
            panelNuevoVehiculo.Visible = true;
            panelNuevoNombre.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal_Nuevo", "ShowModalNuevo();", true);
            up_principal.Update();
        }
        public void mostrarImagen(string UrlImagen)
        {
            img_Imagen.Src = UrlImagen;
            Session["img_Mostrada"] = UrlImagen;
            ScriptManager.RegisterStartupScript(this, GetType(), "Show_Imagen", "ShowImagen();", true);
        }
        protected void gv_vehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_galeria"))
            {
                gv_vehiculos.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Session["#PlacaGaleria"] = (string)gv_vehiculos.SelectedValue;
                Response.Redirect("/ModuloLogistica/galeriaVehiculos.aspx");
            }
            else
            {
                mostrarImagen((string)e.CommandArgument);
            }
            ///  btn_Descargar.PostBackUrl = (string)e.CommandArgument;
        }

        protected void btn_Continuar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_NuevoModelosNombre_Click(object sender, EventArgs e)
        {
            panelNuevoNombre.Visible = true;
            panelNuevoVehiculo.Visible = false;
        }

        protected void btn_GuardarNuevoNombre_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txt_NombreModelo.Text;
            NombreModelosCarros_lg_obj nuevoModelo = new NombreModelosCarros_lg_obj();
            if (nuevoNombre.Length > 0)
            {
                if (!nuevoModelo.checkSiExiste(nuevoNombre))
                {
                    if (nuevoModelo.guardarNuevoNombreModelo(nuevoNombre))
                    {
                        txt_NombreModelo.Text = "";
                        alertaNombre.Visible = false;
                        panelNuevoNombre.Visible = false;
                        panelNuevoVehiculo.Visible = true;
                        fillNombreModelosMarcas();
                    }
                    else
                    {
                        AlertaNuevoNOmbre(tiposAlertaCss_bstrap.error, "Algo ocurrio mientras se guardaba en dato, no se ha podido guardar!!");
                    }
                }
                else
                {
                    AlertaNuevoNOmbre(tiposAlertaCss_bstrap.error, "El nombre ingresado ya esta en el sistema");
                }
            }
            else
            {
                AlertaNuevoNOmbre(tiposAlertaCss_bstrap.advertencia, "No deje el campo de nombre vacio");
            }
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
        public void AlertaNuevoNOmbre(String tipoAdvertencia, String Mensaje)
        {
            alertaNombre.Visible = true;
            alertaNombre.Attributes["Class"] = tipoAdvertencia;
            lbl_alertaNombre.Text = Mensaje;
        }
        public void AlertaNuevoVehiculo(String tipoAdvertencia, String Mensaje)
        {
            alerta_nuevoVehiculo.Visible = true;
            alerta_nuevoVehiculo.Attributes["Class"] = tipoAdvertencia;
            lbl_NuevoMensaje.Text = Mensaje;
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            panelNuevoNombre.Visible = false;
            panelNuevoVehiculo.Visible = true;
            alertaNombre.Visible = false;
            txt_NombreModelo.Text = "";
        }

        protected void btn_GuardarNuevo_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_ColorDetalle, txt_placa))
            {
                if (dr_NombreVehiculos.Items.Count > 0 && dr_Sedes.Items.Count > 0 && dr_TipoCombustible.Items.Count > 0)
                {
                    //Guardar la Imagen en el servidor, si ocurre un error se usara una por defecto
                    String RutaImgCirculacioC1 = UploadImagen(ip_circulacionc1, "c1");
                    String RutaImgCirculacioC2 = UploadImagen(ip_circulacionc2, "c2");
                    Tbl_Vehiculos nuevoVehiculo = new Tbl_Vehiculos();
                    nuevoVehiculo.ID_MarcaModelosCarros = int.Parse(dr_NombreVehiculos.SelectedValue);
                    nuevoVehiculo.IDCedes = int.Parse(dr_Sedes.SelectedValue);
                    nuevoVehiculo.IDCombustible = int.Parse(dr_TipoCombustible.SelectedValue);
                    nuevoVehiculo.URL_CirculacionImg = RutaImgCirculacioC1;
                    nuevoVehiculo.URL_CirculacionImgCara2 = RutaImgCirculacioC2;
                    nuevoVehiculo.Placa = txt_placa.Text;
                    nuevoVehiculo.ColorYDetalle = txt_ColorDetalle.Text;

                    string resultado = new Vehiculos_lg_obj().nuevoVehiculo(nuevoVehiculo);
                    if (resultado.Equals("1"))
                    {
                        txt_ColorDetalle.Text = "";
                        txt_NombreModelo.Text = "";
                        txt_placa.Text = "";
                        fill_facturasPendientes();
                        up_principal.Update();
                        mensaje("El Nuevo Vehiculo se ha guardado correctamente!!", tiposAdvertencias.exito);
                    }
                    else
                    {
                        AlertaNuevoVehiculo(tiposAlertaCss_bstrap.error, "No se ha Podido guardar debido a los siguiente errores: " + resultado);
                    }

                }
                else
                {
                    AlertaNuevoVehiculo(tiposAlertaCss_bstrap.advertencia, "Verifique que las listas de Sede, Combustible, nombre Vehiculo no esten vacias para continuar");
                }
            }
            else
            {
                AlertaNuevoVehiculo(tiposAlertaCss_bstrap.advertencia, "El Numero de Placa y el color son obligatorios");
            }
        }
        public String UploadImagen(HtmlInputFile input, string tag)
        {
            String url = "https://ayemadeoro.com/ModuloLogistica/imagenesCar/";
            try
            {
                if (input.PostedFile != null)
                {
                    string ext = System.IO.Path.GetExtension(input.PostedFile.FileName);
                    string nombreArchivo = tag + "-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ext;
                    string rutaGuardado = Server.MapPath("~/ModuloLogistica/imagenesCar/" + nombreArchivo);
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

        protected void btn_Descargar_Click(object sender, EventArgs e)
        {
            //Path of the File to be downloaded.
            string filePath = (string)Session["img_Mostrada"];
            string nameFile = filePath.Substring(filePath.LastIndexOf('/'));
            Response.ContentType = "application/unknown";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + nameFile);
            Response.TransmitFile(Server.MapPath("~/ModuloLogistica/imagenesCar/" + nameFile));
            Response.End();
        }
    }
}