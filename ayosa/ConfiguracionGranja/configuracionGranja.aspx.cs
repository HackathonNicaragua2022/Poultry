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
    public partial class configuracionGranja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGranjasList();
                fillGaleras();
                fillGranjas();
                fillBroilersRaza();
            }
        }

        protected void btn_CrearGranja_Click(object sender, EventArgs e)
        {
            if (txt_NombreGranja.Text.Length > 0)
            {
                if (txt_Ubicacion.Text.Length <= 0 || txt_NumeroTotalGaleras.Text.Length <= 0 || txt_CapacidadxGaleras.Text.Length <= 0)
                {
                    modalInfo("NO deje campos vacios, verifique que ingreso: Ubicacion, Numero Total de Galeras y Capacidad x Galera", tiposAdvertencias.advertencia);
                }
                else
                {
                    String NombreGalera = txt_NombreGranja.Text;
                    String Ubicacion = txt_Ubicacion.Text;
                    String NombreResponsable = txt_NombreResponsable.Text;
                    String TelefonoResponsable = txt_TelefonoResponsable.Text;
                    DateTime fechaCreacion = DateTime.Now;

                    var membershipUser = System.Web.Security.Membership.GetUser();
                    Guid userId = (Guid)membershipUser.ProviderUserKey;

                    // Verificar que no ingresen numeros decimales en los campos enteros
                    try
                    {
                        int TotalGaleras = int.Parse(txt_NumeroTotalGaleras.Text);
                        int CapacidadxGaleras = int.Parse(txt_CapacidadxGaleras.Text);
                        Tbl_Granjas_obj nuevaGranja = new Tbl_Granjas_obj();
                        string result = nuevaGranja.NuevaGranja(NombreGalera, Ubicacion, NombreResponsable, TelefonoResponsable, fechaCreacion, TotalGaleras, CapacidadxGaleras, userId);
                        if (result.Equals("1"))
                        {
                            txt_NombreGranja.Text = string.Empty;
                            txt_Ubicacion.Text = string.Empty;
                            txt_NombreResponsable.Text = string.Empty;
                            txt_TelefonoResponsable.Text = string.Empty;
                            txt_NumeroTotalGaleras.Text = string.Empty;
                            txt_CapacidadxGaleras.Text = string.Empty;
                            fillGranjas();
                            FillGranjasList();
                            fillGaleras();
                            modalInfo("Se ha Creado correctamente la Nueva granja", tiposAdvertencias.exito);
                        }
                        else
                        {
                            modalInfo("No se ha podido ingresar la nueva Galera Error: " + result, tiposAdvertencias.error);
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
                modalInfo("Ingrese un Nombre para la Granja", tiposAdvertencias.advertencia);
            }
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

        protected void gv_Granjas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        /// <summary>
        /// Carga Todas las Granjas en la base de Datos a la Lista
        /// </summary>
        public void fillGranjas()
        {
            List<Tbl_Granjas_obj.GranjasRel> granjas = new Tbl_Granjas_obj().getAllGranjas_Rel();
            gv_Granjas.DataSource = granjas;
            gv_Granjas.DataBind();
            if (gv_Granjas.Rows.Count > 0)
            {
                this.gv_Granjas.UseAccessibleHeader = true;
                TableCellCollection cells = gv_Granjas.HeaderRow.Cells;
                gv_Granjas.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                cells[2].Attributes.Add("data-breakpoints", "all");
                cells[3].Attributes.Add("data-breakpoints", "all");
                cells[5].Attributes.Add("data-breakpoints", "all");
                cells[8].Attributes.Add("data-breakpoints", "all");
                cells[9].Attributes.Add("data-breakpoints", "all");
                gv_Granjas.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        /// <summary>
        /// Carga Todas las Galeras de la Base de Datos segun la Granja seleccionada en la lista desplegable
        /// </summary>
        public void fillGaleras()
        {

            if (dr_Granjas.Items.Count > 0)
            {
                int idGranja = int.Parse(dr_Granjas.SelectedValue.ToString());
                List<Tbl_Galeras_obj.Tbl_GalerasRel> granjas = new Tbl_Galeras_obj().getAllGalerasxID(idGranja);
                gv_Galeras.DataSource = granjas;
                gv_Galeras.DataBind();
                if (gv_Galeras.Rows.Count > 0)
                {
                    this.gv_Galeras.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_Galeras.HeaderRow.Cells;
                    gv_Galeras.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                    cells[1].Attributes.Add("data-breakpoints", "all");
                    //cells[3].Attributes.Add("data-breakpoints", "all");
                    cells[5].Attributes.Add("data-breakpoints", "all");
                    //cells[8].Attributes.Add("data-breakpoints", "all");
                    //cells[9].Attributes.Add("data-breakpoints", "all");
                    gv_Galeras.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        /// <summary>
        /// Carga todas las Razas en la base de datos en la tabla
        /// </summary>
        public void fillBroilersRaza()
        {
            List<Tbl_Broilers_Raza> broilers = new Tbl_Broilers_Raza_obj().getAllRazaBroilers();
            gv_RazaBroilers.DataSource = broilers;
            gv_RazaBroilers.DataBind();
            if (gv_RazaBroilers.Rows.Count > 0)
            {
                this.gv_RazaBroilers.UseAccessibleHeader = true;
                TableCellCollection cells = gv_RazaBroilers.HeaderRow.Cells;
                gv_RazaBroilers.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //cells[1].Attributes.Add("data-breakpoints", "all");
                cells[3].Attributes.Add("data-breakpoints", "all");
                //cells[5].Attributes.Add("data-breakpoints", "all");
                //cells[8].Attributes.Add("data-breakpoints", "all");
                //cells[9].Attributes.Add("data-breakpoints", "all");
                gv_RazaBroilers.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        /// <summary>
        /// Crea una nueva Galera para la Granja Seleccionanda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_NuevaGalera_Click(object sender, EventArgs e)
        {
            if (dr_Granjas.Items.Count > 0)
            {
                if (txt_NumeroOrden.Text.Length > 0)
                {
                    if (txt_CapacidadInstalada.Text.Length <= 0 || txt_CapacidadNormal.Text.Length <= 0)
                    {
                        modalInfo("Tiene que indicar la Capacidad instalada y la Capacidad Normal", tiposAdvertencias.advertencia);
                    }
                    else
                    {
                        try
                        {
                            /* SE PROCEDE A GUARDAR LA NUEVA GALERA*/
                            int idGranja = int.Parse(dr_Granjas.SelectedValue.ToString());
                            int NumeroOrder = int.Parse(txt_NumeroOrden.Text);
                            int CapacidadInstalada = int.Parse(txt_CapacidadInstalada.Text);
                            int CapacidadNormal = int.Parse(txt_CapacidadNormal.Text);
                            string NombreApodo = txt_NombreApodo.Text;
                            var membershipUser = System.Web.Security.Membership.GetUser();
                            Guid userId = (Guid)membershipUser.ProviderUserKey;

                            Tbl_Galeras_obj nuevaGalera = new Tbl_Galeras_obj();
                            string Result = nuevaGalera.NuevaGalera(idGranja, NumeroOrder, NombreApodo, CapacidadInstalada, CapacidadNormal, userId);
                            if (Result.Equals("1"))
                            {
                                txt_NumeroOrden.Text = string.Empty;
                                txt_CapacidadInstalada.Text = string.Empty;
                                txt_CapacidadNormal.Text = string.Empty;
                                txt_NombreApodo.Text = string.Empty;
                                fillGaleras();
                                modalInfo("La Nueva Galera se ha Guardado Correctamente!", tiposAdvertencias.exito);
                            }
                            else
                            {
                                modalInfo("Error al Guardar la Galera Error: " + Result, tiposAdvertencias.error);
                            }
                        }
                        catch (Exception)
                        {
                            modalInfo("Solo se aceptan numeros enteros en los Campos: Capacidad Normal, Capacidad Instalada y numero de Orden", tiposAdvertencias.advertencia);
                        }
                    }
                }
                else
                {
                    modalInfo("No puede dejar Vacio el Campo Numero de Orden", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modalInfo("No hay Granjas Creadas aun!, debe crear una primero y luego seleccionarla de la lista", tiposAdvertencias.advertencia);
            }
        }
        /// <summary>
        /// Carga Todas las Granjas en la Lista desplegable del grupo de Galeras
        /// </summary>
        public void FillGranjasList()
        {
            dr_Granjas.DataSource = new Tbl_Granjas_obj().getAllGranjas_Rel();
            dr_Granjas.DataTextField = "Nombre";
            dr_Granjas.DataValueField = "ID_Granjas";
            dr_Granjas.DataBind();
        }

        protected void gv_Galeras_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gv_RazaBroilers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btn_nuevaRazaBroilers_Click(object sender, EventArgs e)
        {
            if (txt_NombreRaza_b.Text.Length > 0)
            {
                if (txt_TotalEdadDiasP_broilers.Text.Length <= 0 || txt_PesoPromedioIdea_prod.Text.Length <= 0)
                {
                    modalInfo("Los campos de:Total Edad en Días para Producción y Peso Ideal para Produccion, no pueden estar Vacios !!", tiposAdvertencias.advertencia);
                }
                else
                {
                    try
                    {
                        string nombreRaza = txt_NombreRaza_b.Text;
                        int TotalDiasIdealProduccion = int.Parse(txt_TotalEdadDiasP_broilers.Text);
                        decimal PesoIdealparaProduccion = decimal.Parse(txt_PesoPromedioIdea_prod.Text);

                        Tbl_Broilers_Raza_obj nuevaRaza = new Tbl_Broilers_Raza_obj();

                        string resul = nuevaRaza.nuevaRazaBroilers(nombreRaza, TotalDiasIdealProduccion, PesoIdealparaProduccion);
                        if (resul.Equals("1"))
                        {
                            txt_NombreRaza_b.Text = string.Empty;
                            txt_TotalEdadDiasP_broilers.Text = string.Empty;
                            txt_PesoPromedioIdea_prod.Text = string.Empty;
                            fillBroilersRaza();
                            modalInfo("La Nueva Raza de Broilers se Creo Correctamente!!", tiposAdvertencias.exito);
                        }
                        else
                        {
                            modalInfo("Error intentando crear la nueva Raza Error" + resul, tiposAdvertencias.error);
                        }
                    }
                    catch (Exception)
                    {
                        modalInfo("Verifique los datos, el campos Total Edad en Días para Producción (Entero) y Peso Ideal para Produccion(Numero Real) no pueden estar vacio ni contener caracteres alfabeticos", tiposAdvertencias.advertencia);
                    }
                }
            }
            else
            {
                modalInfo("No deje el campo de Nombre Raza Vacio!!", tiposAdvertencias.advertencia);
            }
        }

    }
}