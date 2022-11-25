using accesoDatos;
using accesoDatos.ProduccionPollos.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.ConfiguracionGranja
{
    public partial class PlantaProcesamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillMataderos();
                //Obtener el peso de java vacia en caso de existir en el sistema
                txt_librasJavaVacia.Text = new Tbl_ConfiguracionesGenerales().getPesoJavaVacia();
                txt_LibrasCanastaVacia.Text = new Tbl_ConfiguracionesGenerales().getPesoCanastaVacia();
            }
        }

        protected void btn_CrearMatadero_Click(object sender, EventArgs e)
        {

            if (txt_NombreMatadero.Text.Length > 0)
            {
                if (txt_Ubicacion.Text.Length <= 0 || txt_CapacidadProduccionMatadero.Text.Length <= 0 || txt_CapcidadLibraxHora.Text.Length <= 0)
                {
                    modalInfo("NO deje campos vacios, verifique que ingreso: Ubicacion, Capacidad Produccion Pollos por Hora y Capacidad Produccion Libras x hora", tiposAdvertencias.advertencia);
                }
                else
                {
                    String NombreMatadero = txt_NombreMatadero.Text;
                    String Ubicacion = txt_Ubicacion.Text;
                    String NombreResponsable = txt_NombreResponsable.Text;
                    String TelefonoResponsable = txt_TelefonoResponsable.Text;
                    DateTime fechaCreacion = DateTime.Now;

                    var membershipUser = System.Web.Security.Membership.GetUser();
                    Guid userId = (Guid)membershipUser.ProviderUserKey;

                    // Verificar que no ingresen numeros decimales en los campos enteros
                    try
                    {
                        int CapacidadProduccionPollosxHora = int.Parse(txt_CapacidadProduccionMatadero.Text);
                        int CapacidadProduccionLibrasxHora = int.Parse(txt_CapcidadLibraxHora.Text);
                        Tbl_Mataderos_obj nuevoMatadero = new Tbl_Mataderos_obj();
                        string result = nuevoMatadero.nuevoMatadero(NombreMatadero, Ubicacion, NombreResponsable, TelefonoResponsable, CapacidadProduccionPollosxHora, CapacidadProduccionLibrasxHora);
                        if (result.Equals("1"))
                        {
                            txt_NombreMatadero.Text = string.Empty;
                            txt_Ubicacion.Text = string.Empty;
                            txt_NombreResponsable.Text = string.Empty;
                            txt_TelefonoResponsable.Text = string.Empty;
                            txt_CapacidadProduccionMatadero.Text = string.Empty;
                            txt_CapcidadLibraxHora.Text = string.Empty;
                            fillMataderos();
                            modalInfo("Se ha Creado correctamente El Nuevo Matadero", tiposAdvertencias.exito);
                        }
                        else
                        {
                            modalInfo("No se ha podido ingresar El nuevo Matadero Error: " + result, tiposAdvertencias.error);
                        }
                    }
                    catch (Exception)
                    {
                        modalInfo("Ingrese solo Numeros Enteros", tiposAdvertencias.advertencia);
                    }
                }
            }
            else
            {
                modalInfo("Ingrese un Nombre para el Matadero", tiposAdvertencias.advertencia);
            }
        }

        protected void gv_Mataderos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        /// <summary>
        /// Se usa para mostrar un mensaje de advertencia al usuario
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="advertencias"></param>
        private void modalInfo(String msg, tiposAdvertencias advertencias)
        {
            lbl_modalInfo.Text = msg;
            switch (advertencias)
            {
                case tiposAdvertencias.exito:
                    imagenModalInfo.Src = "~/Imagenes/camera_test.png";
                    break;
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
        /// <summary>
        /// Carga Todos los Mataderos en la base de Datos a la Lista
        /// </summary>
        public void fillMataderos()
        {
            List<tbl_Matadero> mataderos = new Tbl_Mataderos_obj().getAllMatadero();
            gv_Mataderos.DataSource = mataderos;
            gv_Mataderos.DataBind();
            if (gv_Mataderos.Rows.Count > 0)
            {
                this.gv_Mataderos.UseAccessibleHeader = true;
                TableCellCollection cells = gv_Mataderos.HeaderRow.Cells;
                gv_Mataderos.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                cells[2].Attributes.Add("data-breakpoints", "all");
                cells[3].Attributes.Add("data-breakpoints", "all");
                cells[5].Attributes.Add("data-breakpoints", "all");
                // cells[8].Attributes.Add("data-breakpoints", "all");
                // cells[9].Attributes.Add("data-breakpoints", "all");
                gv_Mataderos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        protected void txt_librasJavaVacia_TextChanged(object sender, EventArgs e)
        {
            if (txt_librasJavaVacia.Text.Length > 0)
            {
                try
                {
                    Tbl_ConfiguracionesGenerales cg = new Tbl_ConfiguracionesGenerales();
                    string peso = txt_librasJavaVacia.Text;
                    string resultado = cg.guardarPesoJava(peso);
                    if (resultado.Equals("1"))
                    {
                        showToast("Se han guardado los cambios correctamente!!");
                    }
                    else
                    {
                        showToast("No se pudo guardar los cambios en el peso de las javas vacias: " + resultado);
                    }
                }
                catch (Exception)
                {
                    showToast("Solo se admiten numeros");
                }
            }
        }
        public void showToast(string Mensaje)
        {
            lbl_Toast.Text = Mensaje;
            ScriptManager.RegisterStartupScript(this, GetType(), "Modal_toast", "ShowtoastMessage();", true);
        }

        protected void txt_LibrasCanastaVacia_TextChanged(object sender, EventArgs e)
        {
            if (txt_librasJavaVacia.Text.Length > 0)
            {
                try
                {
                    Tbl_ConfiguracionesGenerales cg = new Tbl_ConfiguracionesGenerales();
                    string peso = txt_librasJavaVacia.Text;
                    string resultado = cg.guardarPesoJava(peso);
                    if (resultado.Equals("1"))
                    {
                        showToast("Se han guardado los cambios correctamente!!");
                    }
                    else
                    {
                        showToast("No se pudo guardar los cambios en el peso de las javas vacias: " + resultado);
                    }
                }
                catch (Exception)
                {
                    showToast("Solo se admiten numeros");
                }
            }
        }
    }
}