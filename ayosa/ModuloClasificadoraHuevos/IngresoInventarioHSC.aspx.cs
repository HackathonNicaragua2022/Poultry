using accesoDatos;
using accesoDatos.ProduccionHuevos;
using accesoDatos.ProduccionHuevos.Clasificadora;
using accesoDatos.ProduccionHuevos.Clasificadora.Catalogos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;
namespace POULTRY.ModuloClasificadoraHuevos
{
    public partial class IngresoInventarioHSC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // LISTA DE DETALLES DE INGRESO, QUE EN TODO CASO SOLO ES BLANCO Y ROJO HUEVOS
                // DECLARAR UNA VEZ POR CADA CARGA DE PAGINA
                Session["lst_DeltalleIngreso"] = new List<DetalleIngresoHSC_obj.DetalleIngresoHSC>();
                //Lenar la lista de Tipos de Huevos
                fillTipoHuevo();
                fillJaulasIngresoClasificadora();
                fillDetalle();
                date.Text = DateTime.Now.ToString("dd/mm/aaaa");                
            }
        }
        public void fillDetalle()
        {
            if (Session["lst_DeltalleIngreso"] != null)
            {
                List<DetalleIngresoHSC_obj.DetalleIngresoHSC> listaDetalles = (List<DetalleIngresoHSC_obj.DetalleIngresoHSC>)Session["lst_DeltalleIngreso"];
                rp_detalles.DataSource = listaDetalles;
                rp_detalles.DataBind();

            }
        }
        public void fillTipoHuevo()
        {
            TipoHuevo_obj tipoHuevo = new TipoHuevo_obj();
            dr_TipoHuevo.DataSource = tipoHuevo.getAll();
            dr_TipoHuevo.DataTextField = "TipoHuevo";
            dr_TipoHuevo.DataValueField = "ID_TipoHuevo";
            dr_TipoHuevo.DataBind();
        }
        public void fillJaulasIngresoClasificadora()
        {
            JaulasIngresoClasificadora_obj jaulas = new JaulasIngresoClasificadora_obj();
            dr_Ubicacion.DataSource = jaulas.getAll();
            dr_Ubicacion.DataTextField = "Hubicacion";
            dr_Ubicacion.DataValueField = "ID_Jaulas";
            dr_Ubicacion.DataBind();
        }
        protected void btn_IngresarTotalCajillas_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_CantidadCajillasRecibidas))
            {
                if (dr_TipoHuevo.Items.Count > 0 && dr_Ubicacion.Items.Count > 0)
                {
                    int IdJaula = int.Parse(dr_Ubicacion.SelectedValue.ToString());
                    int IdTipoHuevo = int.Parse(dr_TipoHuevo.SelectedValue.ToString());

                    decimal CantidadCajilla = decimal.Parse(txt_CantidadCajillasRecibidas.Text);
                    string ubicacion = dr_Ubicacion.SelectedItem.Text;
                    string tipoHuevo = dr_TipoHuevo.SelectedItem.Text;

                    // La lista de detalles Existente
                    List<DetalleIngresoHSC_obj.DetalleIngresoHSC> listaDetalle = (List<DetalleIngresoHSC_obj.DetalleIngresoHSC>)Session["lst_DeltalleIngreso"];

                    if (listaDetalle != null)
                    {
                        try
                        {
                            //id temporal para remover de las lista en caso de ser nacesario
                            int idTemporal = int.Parse(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString());
                            string fechas = date.Text;
                            CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                            DateTime fechaProduccion = DateTime.Parse(fechas, provider);


                            //Nuevo Detalle
                            DetalleIngresoHSC_obj.DetalleIngresoHSC nuevoDetalle = new DetalleIngresoHSC_obj.DetalleIngresoHSC();
                            nuevoDetalle.ID_DetalleIngresoInventarioHSC = idTemporal;
                            nuevoDetalle.ID_Jaulas = IdJaula;
                            nuevoDetalle.CANTIDADCAJILLA = CantidadCajilla;
                            nuevoDetalle.ID_TipoHUevo = IdTipoHuevo;
                            nuevoDetalle.NombreJaula = ubicacion;
                            nuevoDetalle.TipoHuevo = tipoHuevo;
                            nuevoDetalle.FechaProduccion = fechaProduccion;


                            if (!listaDetalle.Any(a => a.ID_Jaulas == IdJaula && a.ID_TipoHUevo == IdTipoHuevo))
                            {
                                listaDetalle.Add(nuevoDetalle);
                                fillDetalle();
                                mensaje("Agregado a la lista", tiposAdvertencias.exito);
                                //-------------------------------
                                alerta_nuevoVehiculo.Visible = false;
                                txt_CantidadCajillasRecibidas.Text = "";
                            }
                            else
                            {
                                listaDetalle.Where(a => a.ID_Jaulas == IdJaula && a.ID_TipoHUevo == IdTipoHuevo).FirstOrDefault().CANTIDADCAJILLA += CantidadCajilla;
                                fillDetalle();
                                mensaje("Agregado a la lista", tiposAdvertencias.exito);
                                //-------------------------------
                                alerta_nuevoVehiculo.Visible = false;
                                txt_CantidadCajillasRecibidas.Text = "";
                                AlertaNuevoVehiculo(tiposAlertaCss_bstrap.informacion, "Se encontro en la lista el mismo tipo de huevo y ubicacion, por tanto solo se sumaron las cajilla!!");
                            }
                        }
                        catch (Exception)
                        {
                            mensaje("La fecha no es correcta!!, facvor ingrese la fecha de produccion correctamente!!", tiposAdvertencias.error);
                        }
                    }
                }
                else
                {
                    AlertaNuevoVehiculo(tiposAlertaCss_bstrap.advertencia, "Una de las listas desplegables esta vacia por favor verifique que las listas estes completas antes de continuar");
                }
            }
            else
            {
                AlertaNuevoVehiculo(tiposAlertaCss_bstrap.advertencia, "No deje el Campos Cantidad Vacio");
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
        protected void btn_guardarNuevoIngreso_Click(object sender, EventArgs e)
        {

            if (Session["lst_DeltalleIngreso"] != null)
            {
                List<DetalleIngresoHSC_obj.DetalleIngresoHSC> listaDetalle = (List<DetalleIngresoHSC_obj.DetalleIngresoHSC>)Session["lst_DeltalleIngreso"];
                if (listaDetalle.Count > 0)
                {
                    if (new TextboxValidator().validarLargoCadena_mayorCero(txt_NumeroOrden))
                    {
                        string numeroOrden = txt_NumeroOrden.Text;
                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid userId = (Guid)membershipUser.ProviderUserKey;
                        int IDUsuario = new ControlUsuario_obj().getIDFromGUID(userId);
                        if (IDUsuario > 0)
                        {
                            ingresoInventario_HSC nuevoIngreso = new ingresoInventario_HSC();

                            Tbl_IngresoInventario_HSC ingresoHuevosInventario = new Tbl_IngresoInventario_HSC();
                            ingresoHuevosInventario.TOTALCAJILLAS = listaDetalle.Sum(a => a.CANTIDADCAJILLA);
                            ingresoHuevosInventario.IngresadoPor = IDUsuario;
                            ingresoHuevosInventario.FechaIngresoSistema = DateTime.Now;
                            ingresoHuevosInventario.NumeroOrden = numeroOrden;                            
                            string Result = nuevoIngreso.nuevoIngreso(ingresoHuevosInventario, listaDetalle);
                            if (Result.Equals("1"))
                            {
                                // LISTA DE DETALLES DE INGRESO, QUE EN TODO CASO SOLO ES BLANCO Y ROJO HUEVOS
                                // DECLARAR UNA VEZ POR CADA CARGA DE PAGINA
                                Session["lst_DeltalleIngreso"] = new List<DetalleIngresoHSC_obj.DetalleIngresoHSC>();
                                //Lenar la lista de Tipos de Huevos
                                fillTipoHuevo();
                                fillJaulasIngresoClasificadora();
                                fillDetalle();
                                txt_CantidadCajillasRecibidas.Text = "";
                                txt_NumeroOrden.Text = "";

                                AlertaNuevoVehiculo(tiposAlertaCss_bstrap.exito, "El Ingreso fue registrado correctamente¡¡");
                                mensaje("El ingreso fue registrado correctamente", tiposAdvertencias.exito);
                            }
                            else
                            {
                                AlertaNuevoVehiculo(tiposAlertaCss_bstrap.error, "Error registrando el ingreso</br>" + Result);
                                mensaje("Error ingresando el registro de huevo", tiposAdvertencias.error);
                            }
                        }
                        else
                        {
                            AlertaNuevoVehiculo(tiposAlertaCss_bstrap.error, "El usuario debe iniciar Sesion");
                            mensaje("El usuario debe iniciar Sesion", tiposAdvertencias.error);
                        }

                    }
                    else
                    {
                        mensaje("Ingrese el Numero de Orden", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    mensaje("Ingrese detalles a la Lista", tiposAdvertencias.advertencia);
                }
            }
        }
        public void AlertaNuevoVehiculo(string tipoAdvertencia, String Mensaje)
        {
            alerta_nuevoVehiculo.Visible = true;
            alerta_nuevoVehiculo.Attributes["Class"] = tipoAdvertencia;
            lbl_NuevoMensaje.Text = Mensaje;
        }

        protected void rp_detalles_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            int IDDetalleSeleccionado = int.Parse(e.CommandArgument.ToString());
            // La lista de detalles Existente
            List<DetalleIngresoHSC_obj.DetalleIngresoHSC> listaDetalle = (List<DetalleIngresoHSC_obj.DetalleIngresoHSC>)Session["lst_DeltalleIngreso"];
            DetalleIngresoHSC_obj.DetalleIngresoHSC Detalle = listaDetalle.Where(a => a.ID_DetalleIngresoInventarioHSC == IDDetalleSeleccionado).FirstOrDefault();

            listaDetalle.Remove(Detalle);
            fillDetalle();
            mensaje("Se eliminó Correctamente el Detalle de la lista!", tiposAdvertencias.informacion);
        }
    }
}