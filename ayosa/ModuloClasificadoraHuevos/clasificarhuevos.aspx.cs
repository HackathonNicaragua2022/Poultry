using accesoDatos;
using accesoDatos.ClasesModelos.BodegaPrincipal;
using accesoDatos.ProduccionHuevos;
using accesoDatos.ProduccionHuevos.Clasificadora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;

namespace POULTRY.ModuloClasificadoraHuevos
{
    public partial class clasificarhuevos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // LISTA DE DETALLES DE INGRESO, QUE EN TODO CASO SOLO ES BLANCO Y ROJO HUEVOS
                // DECLARAR UNA VEZ POR CADA CARGA DE PAGINA
                Session["lst_DeltalleSalidaHC"] = new List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado>();
                //Lenar la lista de Tipos de Huevos
                fillTipoHuevo();
                fillClasificacion();
                fillDetalle();
            }
        }
        public void fillClasificacion()
        {

            clasificacionHuevo clasificacionHuevo = new clasificacionHuevo();
            dr_Clasificacion.DataSource = clasificacionHuevo.getClasificaciones();
            dr_Clasificacion.DataTextField = "Clasificacion";
            dr_Clasificacion.DataValueField = "IDClasificacionHuevo";
            dr_Clasificacion.DataBind();
        }
        public void fillDetalle()
        {
            if (Session["lst_DeltalleSalidaHC"] != null)
            {
                List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado> listaDetalles = (List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado>)Session["lst_DeltalleSalidaHC"];
                rp_detalles.DataSource = listaDetalles.OrderBy(a => a.TipoHuevo);
                rp_detalles.DataBind();
            }
        }
        public void fillTipoHuevo()
        {
            inventarioHuevoSinClasificar inventarioHuevoSinClasificar = new inventarioHuevoSinClasificar();
            dr_TipoHuevoInventarioSC.DataSource = inventarioHuevoSinClasificar.getHuevoEnBodehaHSC();
            dr_TipoHuevoInventarioSC.DataTextField = "TipoHuevo";
            dr_TipoHuevoInventarioSC.DataValueField = "ID_TipoHuevo";
            dr_TipoHuevoInventarioSC.DataBind();
            //----------------------------------------------------------------------------------------------
            try
            {
                int IDTipoHuevo = int.Parse(dr_TipoHuevoInventarioSC.SelectedItem.Value.ToString());
                List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora> producciones = inventarioHuevoSinClasificar.getSaldoInventarioHuevoSinClasificarxIDparaClasificadora(IDTipoHuevo);
                Session["produccionesSeleccionados"] = producciones;
                fillDatosInventario();
            }
            catch (Exception)
            {
                //No hay Producciones para el tipo de huevo seleccionado
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
        public void AlertaNuevoVehiculo(string tipoAdvertencia, String Mensaje)
        {
            alerta_nuevoVehiculo.Visible = true;
            alerta_nuevoVehiculo.Attributes["Class"] = tipoAdvertencia;
            lbl_NuevoMensaje.Text = Mensaje;
        }
        protected void btn_IngresarTotalCajillas_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_CantidadCajillasRecibidas))
            {
                if (dr_TipoHuevoInventarioSC.Items.Count > 0 && dr_Clasificacion.Items.Count > 0)
                {
                    int ID_Clasificacion = int.Parse(dr_Clasificacion.SelectedValue.ToString());
                    decimal CantidadCajilla = decimal.Parse(txt_CantidadCajillasRecibidas.Text);
                    string clasificacion = dr_Clasificacion.SelectedItem.Text;
                    string tipoHuevo = dr_TipoHuevoInventarioSC.SelectedItem.Text;

                    List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora> producciones = (List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora>)Session["produccionesSeleccionados"];
                    accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora prod_selec = producciones.Where(q => q.Seleccionado).FirstOrDefault();

                    if (producciones.Any(q => q.Seleccionado))
                    {
                        if (CantidadCajilla <= prod_selec.SALDO)
                        {
                            //-Reducir temporalmente el inventario para no ingresar mas de la existencia
                            producciones.Where(q => q.Seleccionado).FirstOrDefault().SALDO = (prod_selec.SALDO - CantidadCajilla);
                            fillDatosInventario();
                            int ID_HuevoSinClasificar = prod_selec.ID_HuevoSinClasificar;

                            string fechaProduccion = string.Format("{0:D}", prod_selec.FechaProduccion.Value.Date);


                            // La lista de detalles Existente
                            List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado> listaDetalle = (List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado>)Session["lst_DeltalleSalidaHC"];

                            if (listaDetalle != null)
                            {
                                //id temporal para remover de las lista en caso de ser nacesario
                                int idTemporal = int.Parse(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString());

                                //Nuevo Detalle
                                DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado nuevoDetalle = new DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado();
                                nuevoDetalle.ID_DetalleSalidaHuevoClasificado = idTemporal;
                                nuevoDetalle.IDClasificacionHuevo = ID_Clasificacion;
                                nuevoDetalle.CANTIDADCAJILLAS = CantidadCajilla;
                                nuevoDetalle.ID_HuevoSinClasificar = ID_HuevoSinClasificar;
                                nuevoDetalle.NombreClasificacion = clasificacion;
                                nuevoDetalle.TipoHuevo = tipoHuevo;
                                nuevoDetalle.FechaProduccion = fechaProduccion;
                                if (!listaDetalle.Any(a => a.IDClasificacionHuevo == ID_Clasificacion && a.ID_HuevoSinClasificar == ID_HuevoSinClasificar))
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
                                    listaDetalle.Where(a => a.IDClasificacionHuevo == ID_Clasificacion && a.ID_HuevoSinClasificar == ID_HuevoSinClasificar).FirstOrDefault().CANTIDADCAJILLAS += CantidadCajilla;
                                    fillDetalle();
                                    mensaje("Agregado a la lista", tiposAdvertencias.exito);
                                    //-------------------------------
                                    alerta_nuevoVehiculo.Visible = false;
                                    txt_CantidadCajillasRecibidas.Text = "";
                                    AlertaNuevoVehiculo(tiposAlertaCss_bstrap.informacion, "Se encontro en la lista el mismo tipo de huevo y Clasificacion, por tanto solo se sumaron las cajilla!!");
                                }
                            }
                        }
                        else
                        {
                            AlertaNuevoVehiculo(tiposAlertaCss_bstrap.advertencia, "La Cantidad de Cajillas Clasificadas no coincide con el Saldo Disponible");
                        }
                    }
                    else
                    {
                        AlertaNuevoVehiculo(tiposAlertaCss_bstrap.advertencia, "Seleccione una produccion antes de continuar");
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

        protected void rp_detalles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int IDDetalleSeleccionado = int.Parse(e.CommandArgument.ToString());
            // La lista de detalles Existente
            List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado> listaDetalle = (List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado>)Session["lst_DeltalleSalidaHC"];
            DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado Detalle = listaDetalle.Where(a => a.ID_DetalleSalidaHuevoClasificado == IDDetalleSeleccionado).FirstOrDefault();
            listaDetalle.Remove(Detalle);
            fillDetalle();

            //devolver todas las cajillas a el saldo de su correspodiente valor
            try
            {
                List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora> producciones = (List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora>)Session["produccionesSeleccionados"];
                producciones.Where(q => q.ID_HuevoSinClasificar == Detalle.ID_HuevoSinClasificar).FirstOrDefault().SALDO = producciones.Where(q => q.ID_HuevoSinClasificar == Detalle.ID_HuevoSinClasificar).FirstOrDefault().SALDO + Detalle.CANTIDADCAJILLAS;
                fillDatosInventario();
            }
            catch (Exception)
            {

                throw;
            }

            mensaje("Se eliminó Correctamente el Detalle de la lista!", tiposAdvertencias.informacion);
        }

        protected void btn_guardarNuevoIngreso_Click(object sender, EventArgs e)
        {
            if (Session["lst_DeltalleSalidaHC"] != null)
            {
                List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado> listaDetalle = (List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado>)Session["lst_DeltalleSalidaHC"];
                if (listaDetalle.Count > 0)
                {
                    //if (new TextboxValidator().validarLargoCadena_mayorCero(txt_Observaciones))
                    //{
                    var membershipUser = System.Web.Security.Membership.GetUser();
                    Guid userId = (Guid)membershipUser.ProviderUserKey;
                    int IDUsuario = new ControlUsuario_obj().getIDFromGUID(userId);
                    string observaciones = txt_Observaciones.Text;
                    if (IDUsuario > 0)
                    {
                        SalidaHuevoClasificado_obj nuevoIngreso = new SalidaHuevoClasificado_obj();

                        Tbl_SalidaHuevoClasificado salidaHuevoClasificado = new Tbl_SalidaHuevoClasificado();
                        salidaHuevoClasificado.TOTALGAJILLAS_CLASIFICADAS = listaDetalle.Sum(a => a.CANTIDADCAJILLAS);
                        salidaHuevoClasificado.RegistradoPor = IDUsuario;
                        salidaHuevoClasificado.FechaRegistroSistema = DateTime.Now;
                        salidaHuevoClasificado.Observaciones = observaciones;

                        string Result = nuevoIngreso.nuevaClasificacion(salidaHuevoClasificado, listaDetalle);
                        if (Result.Equals("1"))
                        {
                            // LISTA DE DETALLES DE INGRESO, QUE EN TODO CASO SOLO ES BLANCO Y ROJO HUEVOS
                            // DECLARAR UNA VEZ POR CADA CARGA DE PAGINA
                            Session["lst_DeltalleSalidaHC"] = new List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado>();
                            //Lenar la lista de Tipos de Huevos
                            fillTipoHuevo();
                            fillClasificacion();
                            fillDetalle();
                            txt_CantidadCajillasRecibidas.Text = "";
                            txt_Observaciones.Text = "";
                            AlertaNuevoVehiculo(tiposAlertaCss_bstrap.exito, "La salida de Huevo clasificado y empacado fue registrada exitosamente¡¡");
                            mensaje("El Huevo Clasificado y empacado fue registrado correctamente!!", tiposAdvertencias.exito);
                        }
                        else
                        {
                            AlertaNuevoVehiculo(tiposAlertaCss_bstrap.error, "Error registrando el huevo Clasificado</br>" + Result);
                            mensaje("Error ingresando el registro de huevo Clasificado", tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        mensaje("No se ha podido obtener el ID del usuario actual, favor inicie sesion nuevamente", tiposAdvertencias.advertencia);
                    }

                    //}
                    //else
                    //{
                    //    mensaje("Ingrese el Numero de Orden", tiposAdvertencias.advertencia);
                    //}
                }
                else
                {
                    mensaje("Ingrese detalles a la Lista", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("La lista de detalles esta vacia", tiposAdvertencias.advertencia);
            }
        }

        protected void dr_TipoHuevoInventarioSC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IDTipoHuevo = int.Parse(dr_TipoHuevoInventarioSC.SelectedItem.Value.ToString());
            inventarioHuevoSinClasificar inventario = new inventarioHuevoSinClasificar();
            List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora> producciones = inventario.getSaldoInventarioHuevoSinClasificarxIDparaClasificadora(IDTipoHuevo);
            if (Session["lst_DeltalleSalidaHC"] != null)
            {
                List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado> listaDetalle = (List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado>)Session["lst_DeltalleSalidaHC"];

                foreach (accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora _prod in producciones)
                {
                    if (listaDetalle.Any(a => a.ID_HuevoSinClasificar == _prod.ID_HuevoSinClasificar))//<- ya hay en la lista de clasificaciones una produccion con este id para evitar se siga deduciendo cantidad, se actualiza el saldo con lo ya existente en la lista de detalles
                    {
                        _prod.SALDO -= listaDetalle.Where(a => a.ID_HuevoSinClasificar == _prod.ID_HuevoSinClasificar).Sum(b => b.CANTIDADCAJILLAS);
                    }
                }
            }

            Session["produccionesSeleccionados"] = producciones;
            lbl_FechaProduccion.Text = String.Format("{0:D}", "Seleccione una Produccion");
            lbl_EdadDias.Text = String.Format("{0:n}", "Seleccione una Produccion");
            lbl_TotalEntradas.Text = String.Format("{0:n}", "Seleccione una Produccion");
            lbl_TotalSalidas.Text = String.Format("{0:n}", "Seleccione una Produccion");
            lbl_SaltoTotal.Text = String.Format("{0:n}", "Seleccione una Produccion");
            fillDatosInventario();
        }
        public void fillDatosInventario()
        {
            try
            {
                if (Session["produccionesSeleccionados"] != null)
                {
                    List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora> producciones = (List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora>)Session["produccionesSeleccionados"];
                    rp_DetallesIHSC.DataSource = producciones;
                    rp_DetallesIHSC.DataBind();
                    //--------------------------------------
                    accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora seleccionado = producciones.Where(q => q.Seleccionado).FirstOrDefault();
                    Session["ProduccionSeleccionada_Clasificadora"] = seleccionado;
                    lbl_FechaProduccion.Text = String.Format("{0:D}", seleccionado.FechaProduccion);
                    lbl_EdadDias.Text = String.Format("{0:n}", seleccionado.PROMEDIO_DIAS);
                    lbl_TotalEntradas.Text = String.Format("{0:n}", seleccionado.TOTALE);
                    lbl_TotalSalidas.Text = String.Format("{0:n}", seleccionado.TS);
                    lbl_SaltoTotal.Text = String.Format("{0:n}", seleccionado.SALDO);
                }
            }
            catch (Exception)
            {
            }
        }

        protected void rp_DetallesIHSC_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Seleccionar"))
            {
                if (Session["produccionesSeleccionados"] != null)
                {
                    int IDHuevoSinClasificar = int.Parse(e.CommandArgument.ToString());
                    List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora> producciones = (List<accesoDatos.ProduccionHuevos.Clasificadora.inventarioHuevoSinClasificar.SaldoHuevosSinClasificarParaClasificadora>)Session["produccionesSeleccionados"];

                    try
                    {
                        producciones.Where(q => q.Seleccionado).FirstOrDefault().Seleccionado = false;
                    }
                    catch (Exception)
                    {
                    }

                    producciones.Where(q => q.ID_HuevoSinClasificar == IDHuevoSinClasificar).FirstOrDefault().Seleccionado = true;
                    fillDatosInventario();
                }
            }
        }
    }
}