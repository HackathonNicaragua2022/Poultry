using accesoDatos;
using accesoDatos.Granja;
using accesoDatos.ProduccionPollos.Configuracion;
using accesoDatos.ProduccionPollos.ControlLotes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;
namespace POULTRY.GranjaSanFrancisco
{
    public partial class IngresoLote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ListaPersonasEnRecibo"] = new List<Tbl_PersonalEnEntrada>();
                fillProveedores();
                fillGranja();
                fillBroilersRaza();
                txt_tazaCambio.Text = new TazaCambio().getTazaString();
            }
            fillPersonalPrecente();

        }
        /// <summary>
        /// Carga todas las Razas en la base de datos en la tabla
        /// </summary>
        public void fillBroilersRaza()
        {
            List<Tbl_Broilers_Raza> broilers = new Tbl_Broilers_Raza_obj().getAllRazaBroilers();
            dr_RazaAve.DataSource = broilers;
            dr_RazaAve.DataTextField = "NombreRaza";
            dr_RazaAve.DataValueField = "ID_Broilers_Raza";
            dr_RazaAve.DataBind();
        }
        public void fillProveedores()
        {
            proveedoresAvesEngorde_obj proveedores = new proveedoresAvesEngorde_obj();
            dr_Proveedor.DataSource = proveedores.getAllProveedores();
            dr_Proveedor.DataValueField = "ID_ProveedoresAves";
            dr_Proveedor.DataTextField = "Nombre_Proveedor";
            dr_Proveedor.DataBind();
        }
        public void fillRaza()
        {
            proveedoresAvesEngorde_obj proveedores = new proveedoresAvesEngorde_obj();
            dr_Proveedor.DataSource = proveedores.getAllProveedores();
            dr_Proveedor.DataValueField = "ID_ProveedoresAves";
            dr_Proveedor.DataTextField = "Nombre_Proveedor";
            dr_Proveedor.DataBind();
        }

        protected void ID_AgregarPersonaRecibo_Click(object sender, EventArgs e)
        {
            if (Session["ListaPersonasEnRecibo"] == null) Session["ListaPersonasEnRecibo"] = new List<Tbl_PersonalEnEntrada>();
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_NombrePersonal, txt_CargoEnRecibo, txt_CargoDuranteCrecimiento))
            {
                new TextboxValidator().setFormControl(txt_NombrePersonal, txt_CargoEnRecibo, txt_CargoDuranteCrecimiento);
                List<Tbl_PersonalEnEntrada> lista = Session["ListaPersonasEnRecibo"] as List<Tbl_PersonalEnEntrada>;

                Tbl_PersonalEnEntrada personalEntrada = new Tbl_PersonalEnEntrada();
                personalEntrada.ID_PersonalEnEntrada = DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Millisecond;
                personalEntrada.NombrePersona = txt_NombrePersonal.Text;
                personalEntrada.FuncionDuranteRecibo = txt_CargoEnRecibo.Text;
                personalEntrada.FuncionDuranteCrecimiento = txt_CargoDuranteCrecimiento.Text;
                lista.Add(personalEntrada);
                txt_NombrePersonal.Text = "";
                txt_CargoEnRecibo.Text = "";
                txt_CargoDuranteCrecimiento.Text = "";
                fillPersonalPrecente();
                mensaje("Se agrego correctamente el personal", tiposAdvertencias.exito);
                Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.INFORMACION, "Ingresado Correctamente", personalEntrada.NombrePersona + " fue ingresado a la lista correctamente!", alertaIngresoPersonal);
            }
        }

        protected void rp_personalPrecente_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Eliminar"))
            {
                int IDElementoRemover = int.Parse(e.CommandArgument.ToString());
                List<Tbl_PersonalEnEntrada> lista = Session["ListaPersonasEnRecibo"] as List<Tbl_PersonalEnEntrada>;
                Tbl_PersonalEnEntrada quitarObj = lista.Where(r => r.ID_PersonalEnEntrada == IDElementoRemover).FirstOrDefault();
                lista.Remove(quitarObj);
                fillPersonalPrecente();
                alertaIngresoPersonal.InnerHtml = "";
            }
        }
        public void fillGranja()
        {
            dr_Granja.DataSource = new Tbl_Granjas_obj().getAllGranjas_Rel();
            dr_Granja.DataTextField = "Nombre";
            dr_Granja.DataValueField = "ID_Granjas";
            dr_Granja.DataBind();
            fillGalerasxGranja();

        }
        public void fillGalerasxGranja()
        {
            if (dr_Granja.Items.Count > 0)
            {
                int idGranja = int.Parse(dr_Granja.SelectedValue.ToString());
                ControlGaleras cGaleras = new ControlGaleras();
                //obtiene todas las galeras que no tienen una produccion activa
                dr_Galeras.DataSource = cGaleras.getAllGalerasxGranja(idGranja);
                dr_Galeras.DataTextField = "NumeroOrden";
                dr_Galeras.DataValueField = "ID_Galeras";
                dr_Galeras.DataBind();
                if (dr_Galeras.Items.Count <= 0)
                {
                    galerasEnProduccion.Visible = true;
                    row_principal.Visible = false;
                }
                else
                {
                    galerasEnProduccion.Visible = false;
                    row_principal.Visible = true;
                }
            }
        }
        public void fillPersonalPrecente()
        {
            List<Tbl_PersonalEnEntrada> lista = Session["ListaPersonasEnRecibo"] as List<Tbl_PersonalEnEntrada>;
            rp_personalPrecente.DataSource = lista;
            rp_personalPrecente.DataBind();
        }

        protected void txt_FechaEntradaGalera_TextChanged(object sender, EventArgs e)
        {
            string fechas = txt_FechaEntradaGalera.Text;
            if (fechas.Length > 0)
            {
                CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                DateTime fechaIngresoGalera = DateTime.Parse(fechas, provider);
                Tbl_Broilers_Raza_obj broilers = new Tbl_Broilers_Raza_obj();
                int IDRaza = int.Parse(dr_RazaAve.SelectedItem.Value.ToString());
                int TotalDias = broilers.getDiasIdealLiquidar(IDRaza);
                txt_FechaLiquidar.Text = fechaIngresoGalera.AddDays(TotalDias).ToLongDateString();
            }
        }

        protected void dr_Granja_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGalerasxGranja();
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

        protected void btn_IngresoLote_Click(object sender, EventArgs e)
        {
            if (dr_Galeras.Items.Count > 0 && dr_Granja.Items.Count > 0)
            {
                if (txt_FechaEntradaGalera.Text.Length > 0)
                {
                    if (txt_TotalAvesCompradas.Text.Length > 0)
                    {
                        if (txt_CostoPorAveUSD.Text.Length > 0)
                        {
                            if (Session["ListaPersonasEnRecibo"] != null)
                            {
                                if (txt_MortalidadRecibida.Text.Length > 0 && txt_ConteoFisico.Text.Length > 0 && txt_verificadoPor.Text.Length > 0)
                                {
                                    if (txt_NumeroFactura.Text.Length > 0)
                                    {
                                        List<Tbl_PersonalEnEntrada> lista = Session["ListaPersonasEnRecibo"] as List<Tbl_PersonalEnEntrada>;
                                        if (lista.Count > 0)
                                        {
                                            alerta_Ingreso.InnerHtml = "";
                                            int IDGalera = int.Parse(dr_Galeras.SelectedItem.Value);
                                            int IDProveedor = int.Parse(dr_Proveedor.SelectedItem.Value);
                                            decimal pesoPromedio = decimal.Parse(txt_PesoPromedioAve.Text);
                                            int cantidadComprada = int.Parse(txt_TotalAvesCompradas.Text);
                                            decimal PesoTotalLote = cantidadComprada * pesoPromedio;
                                            int bonificacion = int.Parse(txt_PorcentajeExtra.Text);
                                            decimal costoTotalCompraLoteNIO = decimal.Parse(txt_CostoTotalNIO.Text);
                                            decimal costoTotalCompraLoteUSD = decimal.Parse(txt_CostoTotalUSD.Text);
                                            decimal TazaConvercion = decimal.Parse(txt_tazaCambio.Text);
                                            int IDBroilerRaz = int.Parse(dr_RazaAve.SelectedItem.Value);
                                            int mortalidadRecibida = int.Parse(txt_MortalidadRecibida.Text);
                                            int conteoFisico = int.Parse(txt_ConteoFisico.Text);
                                            string quienVerificoConteo = txt_verificadoPor.Text;
                                            string factura = txt_NumeroFactura.Text;

                                            CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                                            DateTime fechaIngresoGalera = DateTime.Parse(txt_FechaEntradaGalera.Text, provider);

                                            string codigoLote = "G" + dr_Galeras.SelectedItem.Text + fechaIngresoGalera.Day.ToString() + fechaIngresoGalera.Month.ToString() + fechaIngresoGalera.Year.ToString();

                                            var membershipUser = System.Web.Security.Membership.GetUser();
                                            Guid ingresadoPor = (Guid)membershipUser.ProviderUserKey;

                                            DateTime fechaAproximadaparaLiquidar = fechaIngresoGalera.AddDays(new Tbl_Broilers_Raza_obj().getDiasIdealLiquidar(IDBroilerRaz));
                                            decimal temperaturaInicial = decimal.Parse(txt_TemperaturaInicial.Text);
                                            decimal CostoxAveUSD = decimal.Parse(txt_CostoPorAveUSD.Text);
                                            decimal CostoxAveNIO = TazaConvercion * CostoxAveUSD;

                                            int cantidadAvesPesadas = int.Parse(txt_CantidadAvesPesadasPesoProme.Text);

                                            Tbl_IngresoLotes NuevoLote = new Tbl_IngresoLotes();
                                            NuevoLote.NumeroFactura = factura;
                                            NuevoLote.CodLote = codigoLote;
                                            NuevoLote.PesoPromedioxAveRecibida = pesoPromedio;
                                            NuevoLote.PesoTotalxLote = PesoTotalLote;
                                            NuevoLote.CantidadComprada = cantidadComprada;
                                            NuevoLote.Bonificacion = bonificacion;
                                            NuevoLote.CostoTotal_CompraLoteNIO = costoTotalCompraLoteNIO;
                                            NuevoLote.CostoTotal_CompraLoteUSD = costoTotalCompraLoteUSD;
                                            NuevoLote.TazaConvercion = TazaConvercion;
                                            NuevoLote.ID_Broilers_Raza = IDBroilerRaz;
                                            NuevoLote.FechaEntradaGalera = fechaIngresoGalera;
                                            NuevoLote.IngresadoPor = ingresadoPor;
                                            NuevoLote.NumeroAprobacionesAdministracion = 0;//futuramente un administrador de mayor rango debe validar esta entrada
                                            NuevoLote.Aprobaciones = 0;//futuramente un administrador de mayor rango debe validar esta entrada
                                            NuevoLote.Aprobado = true;
                                            NuevoLote.FechaAproximadaparaMatarLote = fechaAproximadaparaLiquidar;
                                            NuevoLote.ID_ProveedoresAves = IDProveedor;
                                            NuevoLote.Temperatura_InicialGalera = temperaturaInicial;
                                            NuevoLote.CostoxAveUSD = CostoxAveUSD;
                                            NuevoLote.CostoxAveNIO = CostoxAveNIO;
                                            NuevoLote.CantidadAvesPesadasparaPesoProm = cantidadAvesPesadas;

                                            NuevoLote.MortalidadRecibida = mortalidadRecibida;
                                            NuevoLote.ConteoFisico = conteoFisico;
                                            NuevoLote.ApruebaConteo = quienVerificoConteo;

                                            IngresoLote_obj nuevoIngresoLote = new IngresoLote_obj();
                                            string result = nuevoIngresoLote.IngresarLote(NuevoLote, lista, IDGalera);
                                            if (result.Equals("1"))
                                            {
                                                Session["ListaPersonasEnRecibo"] = new List<Tbl_PersonalEnEntrada>();
                                                txt_PesoPromedioAve.Text = "";
                                                txt_TotalAvesCompradas.Text = "";
                                                txt_PorcentajeExtra.Text = "";
                                                txt_FechaEntradaGalera.Text = "";
                                                txt_CantidadAvesPesadasPesoProme.Text = "";
                                                txt_CargoDuranteCrecimiento.Text = "";
                                                txt_CargoEnRecibo.Text = "";
                                                txt_PorcentajeExtra.Text = "";
                                                txt_TotalAvesCompradas.Text = "";
                                                txt_TotalRecibido.Text = "";
                                                txt_CostoPorAveUSD.Text = "";
                                                txt_CostoTotalNIO.Text = "";
                                                txt_CostoTotalUSD.Text = "";
                                                txt_TemperaturaInicial.Text = "";
                                                txt_FechaLiquidar.Text = "";
                                                txt_EquivalenteNIO.Text = "";
                                                txt_tazaCambio.Text = "";
                                                txt_verificadoPor.Text = "";
                                                txt_MortalidadRecibida.Text = "";
                                                txt_DiferenciaFactura.Text = "";
                                                txt_ConteoFisico.Text = "";
                                                //alerta_Ingreso.InnerHtml = "";
                                                alertaIngresoPersonal.InnerHtml = "";
                                                fillPersonalPrecente();
                                                fillGranja();// cargar para usar solo las galeras disponibles
                                                mensaje("Ingreso a lote Guardado Correctamente", tiposAdvertencias.exito);
                                                Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.EXITO, "Guadado Correctamente¡¡", "Se ha ingresado Correctamente el Lote, ahora puede monitoriar su control de crecimiento en LOTES BROILERS", alerta_Ingreso);
                                            }
                                            else
                                            {
                                                mensaje("Error ingresando el lote", tiposAdvertencias.error);
                                                Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ERROR, "Error ingresando el Lote!!", "No se ha podido ingresar el lote por el siguiente error: " + result, alerta_Ingreso);
                                            }
                                        }
                                        else
                                        {
                                            mensaje("La lista de personal esta vacia", tiposAdvertencias.advertencia);
                                            Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Personal vacio", "La lista de personal esta vacio!, por favor indique almenos un personal", alerta_Ingreso);
                                        }
                                    }
                                    else
                                    {
                                        mensaje("Ingrese el Numero Factura!", tiposAdvertencias.advertencia);
                                        Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Ingrese el Numero de Factura porfavor!","Hace falta el numero de factura", alerta_Ingreso);
                                    }
                                }
                                else
                                {
                                    mensaje("indique la mortalidad recibida, el conteo en fisico o quien verifico el conteo de pollitos!", tiposAdvertencias.advertencia);
                                    Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Mortalidad, conteo Fisico o persona que verifico el conteo estan vacio", "Indique la mortalidad recibida, el Conteo fisico realizado o quien verifico el coteo, estos campos no deben quedar vacios!", alerta_Ingreso);
                                }
                            }
                            else
                            {
                                mensaje("La lista de personal esta vacia", tiposAdvertencias.advertencia);
                                Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Personal vacio", "La lista de personal esta vacio!, por favor indique almenos un personal", alerta_Ingreso);
                            }
                        }
                        else
                        {
                            mensaje("Es necesario que indique el costo de aves", tiposAdvertencias.advertencia);
                            Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Costo por Unidad Vacio!!", "Por favor indique el costo por ave en USD, si la taza de cambio no se muestra favor desmarque la casilla de <b>Usar 2% adicional</b> e ingrese la cantidad total manualmente!!", alerta_Ingreso);
                        }
                    }
                    else
                    {
                        mensaje("No ha ingresado el total de aves", tiposAdvertencias.advertencia);
                        Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Cantidad Avez Vacias!!", "Debe indicar la cantidad de avez compradas.!!", alerta_Ingreso);
                    }
                }
                else
                {
                    mensaje("Es necesario que indique la fecha de ingreso a la galera de destino!!", tiposAdvertencias.advertencia);
                    Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Fecha Vacias!!", "Debe indicar la fecha en la que se estara ingresando el lote a la galera!!", alerta_Ingreso);
                }
            }
            else
            {
                mensaje("No se puede ingresar lote si la listas de galera y granja estan vacias!!", tiposAdvertencias.advertencia);
                Boostrap4Utill.setBloc_Alert(TiposAdvertenciasBoostrap.ADVERTENCIA, "Lista Vacias!!", "No se puede ingresar lote si la listas de galera y granja estan vacias!!", alerta_Ingreso);
            }
        }
    }
}