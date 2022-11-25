using accesoDatos;
using accesoDatos.Granja;
using accesoDatos.ProduccionPollos.Configuracion;
using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.ProduccionDiaria_matadero
{
    public partial class CompraPollos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGranjasList();
                fillLotesActivos();
                fillBroilersRaza();
                fillMataderos();
                EnlazarUsuarios();
                txt_FechaMatanza.Text = DateTime.Now.ToLongDateString();
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
        /// <summary>
        /// Carga todas las Razas en la base de datos en la tabla
        /// </summary>
        public void fillBroilersRaza()
        {
            List<Tbl_Broilers_Raza> broilers = new Tbl_Broilers_Raza_obj().getAllRazaBroilers();
            dr_RazaBroilers.DataSource = broilers;
            dr_RazaBroilers.DataTextField = "NombreRaza";
            dr_RazaBroilers.DataValueField = "ID_Broilers_Raza";
            dr_RazaBroilers.DataBind();
        }
        public void fillMataderos()
        {
            List<tbl_Matadero> broilers = new Tbl_Mataderos_obj().getAllMatadero();
            dr_Matadero.DataSource = broilers;
            dr_Matadero.DataTextField = "NombreMatadero";
            dr_Matadero.DataValueField = "ID_Matadero";
            dr_Matadero.DataBind();
        }
        private void EnlazarUsuarios()
        {
            // Get all of the user accounts 
            MembershipUserCollection users = Membership.GetAllUsers();
            dr_UsuarioaCargo.DataSource = users;
            dr_UsuarioaCargo.DataTextField = "UserName";
            dr_UsuarioaCargo.DataBind();
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
        protected void btn_GuardarProcesoRemision_Click(object sender, EventArgs e)
        {
            //SOLO GUARDAR SI TODOS LAS LISTAS DESPLEGABLES TIENEN DATOS
            if (dr_Granjas.Items.Count > 0 && dr_Matadero.Items.Count > 0 && dr_RazaBroilers.Items.Count > 0 && dr_UsuarioaCargo.Items.Count > 0)
            {
                if (txt_PrecioCompra.Text.Length > 0)
                {
                    if (txt_FechaMatanza.Text.Length > 0)
                    {
                        if (dr_lotesDisponibles.Items.Count > 0)
                        {
                            try
                            {
                                //Almacenar los valores de las variables
                                int IDGranjaRemitente = int.Parse(dr_Granjas.SelectedValue.ToString());
                                string NumeroReferencia = txt_NumeroReferencia.Text;
                                int ID_Lote = int.Parse(dr_lotesDisponibles.SelectedItem.Value);
                                DateTime fechaMatanza = Convert.ToDateTime(txt_FechaMatanza.Text, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat);
                                decimal precioCompraxLibra = decimal.Parse(txt_PrecioCompra.Text);
                                int IDBroilerRaza = int.Parse(dr_RazaBroilers.SelectedValue.ToString());
                                int IDMatadero = int.Parse(dr_Matadero.SelectedValue.ToString());
                                //-----------------------------------------------------------------------
                                String _user = dr_UsuarioaCargo.SelectedValue;
                                MembershipUser user = Membership.GetUser(_user);
                                Guid UsuarioResponsable = (Guid)(user.ProviderUserKey);
                                //-------------------------------------------------------------------
                                var membershipUser = System.Web.Security.Membership.GetUser();
                                Guid CreadoPor = (Guid)membershipUser.ProviderUserKey;

                                Tbl_CompraPollos_obj nuevaCompra = new Tbl_CompraPollos_obj();
                                string Result = nuevaCompra.guardarNuevoProcesoCompraPollos(IDGranjaRemitente, NumeroReferencia, ID_Lote, fechaMatanza, precioCompraxLibra, IDBroilerRaza, IDMatadero, UsuarioResponsable, CreadoPor);
                                if (Result.Equals("1"))
                                {
                                    modalInfo("El Proceso se ha Guardado Correctamente, Puede Continuar con el Ingreso de las Remisiones y los Pesos</br></hr>Se ha creado el proceso de ingreso a cuartos Frios", tiposAdvertencias.exito);
                                }
                                else
                                {
                                    modalInfo("Error al Crear el Proceso: " + Result, tiposAdvertencias.error);
                                }
                            }
                            catch (Exception ex)
                            {
                                modalInfo("Ha Ocurrido un Error: " + ex.Message, tiposAdvertencias.advertencia);
                            }
                        }
                        else
                        {
                            modalInfo("Aparentemente la granja seleccionada no contiene lotes activos, verifiquelo he intene nuevamente!!", tiposAdvertencias.advertencia);
                        }
                    }
                    else
                    {
                        modalInfo("No deje el campo fecha vacio, use el boton del calendario para seleccionar la fecha correcta!!", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    modalInfo("El precio de Compra por Libra es necesario para continuar!!", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modalInfo("Antes debe Ingresar al sistea cualquiera de los siguientes datos que hagan falta: </hr>1) Granjas</hr>2) Mataderos</hr>3) Raza de Broilers</hr>De lo contrario no podra continuar", tiposAdvertencias.advertencia);
            }
        }

        protected void btn_ProcesosCreados_Click(object sender, EventArgs e)
        {
            Response.Redirect("procesosEnSistema.aspx");
        }
        public void fillLotesActivos()
        {
            if (dr_Granjas.Items.Count > 0)
            {
                InventarioGaleras_obj inventarioGalera = new InventarioGaleras_obj();
                int IDGranja = int.Parse(dr_Granjas.SelectedItem.Value);
                dr_lotesDisponibles.DataSource = inventarioGalera.getInventariosActivos(IDGranja);
                dr_lotesDisponibles.DataTextField = "CodLote";
                dr_lotesDisponibles.DataValueField = "ID_Lote";
                dr_lotesDisponibles.DataBind();
            }
        }
        protected void dr_Granjas_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillLotesActivos();
        }
    }
}