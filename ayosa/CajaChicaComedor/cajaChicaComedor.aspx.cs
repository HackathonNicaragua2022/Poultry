using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos.CajaChicaComedor;
using accesoDatos;
using System.Globalization;
namespace POULTRY.CajaChicaComedor
{
    public partial class cajaChicaComedor : System.Web.UI.Page
    {
        public String Fecha = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                contenidoCuerpo.Visible = false;// HACER NO VISIBLE PARA VERIFICAR LA BASE DE DATOS Y EL CONTENIDO DE LA CAJA
                /*
                 * VERIFICAR LA BASE DE DATOS EN BUSCA DE UNA CAJA PARA LA FECHA DE HOY DE LO CONTRARIO CREAR UNA CAJA
                 * 
                 */
                CajaChicaComedor_obj caja = new CajaChicaComedor_obj();
                if (!caja.cajaHoy())
                {
                    var membershipUser = System.Web.Security.Membership.GetUser();
                    Guid userId = (Guid)membershipUser.ProviderUserKey;
                    string CajaCreada = caja.crearCajaDelDia(userId);
                    if (CajaCreada.Equals("1"))
                    {
                        mensajeAlerta(tiposAdvertencias.exito, "Caja Creada con Exito!!", "Se creo correctamente la caja del Dia de Hoy! (" + DateTime.Now.ToLongDateString() + ")", false);
                        contenidoCuerpo.Visible = true;
                    }
                    else
                    {
                        mensajeAlerta(tiposAdvertencias.error, "Error al Crear la Caja!!", "Ha ocurrido un error al intentar crear la caja del dia de Hoy Error: " + CajaCreada, false);
                    }
                }
                /*
                 *  VERIFICAR SI YA EXISTE UNA CAJA PERO AUN NO TIENE APERTURA ENTOCES PROCEDER A CREAR UNA APERTURA 
                 */
                if (caja.cajaHoy())
                {// SI HAY UNA CAJA PARA HOY
                    if (!caja.estaAperturadaCajaHoy())
                    {// SI LA CAJA DE HOY AUN NO ESTA APERTURADA
                        contenidoCuerpo.Visible = false;
                        contenidoApertura.Visible = true;
                    }
                    else
                    {
                        //Verifica si la caja esta cerrada para no realizar mas movimientos
                        if (caja.EstaCerradaHoy())
                        {
                            contenidoCuerpo.Visible = false;
                            contenidoApertura.Visible = false;
                            CuerpoCajaCerrada.Visible = true;
                        }
                        else
                        {
                            contenidoCuerpo.Visible = true;
                            contenidoApertura.Visible = false;
                            CuerpoCajaCerrada.Visible = false;
                        }
                    }
                    //Cargar Los Datos solo si existe una caja
                    CargarDatosCajaChica();
                }
                Session["ArqueoList"] = new List<tbl_ArqueoCajaChica_Comedor_obj>();//Lista de arqueos para el Cierre de la caja
                Session["collapse_Cierre"] = true;//Hacer la variable de session true para que el control Colapse siempre al iniciar la pagina cargue cerrada
            }
            //fillDetalle_Ingreso();
            CargarListaArqueo();
        }

        protected void btn_AperturaC_Click(object sender, EventArgs e)
        {
            if (txt_montoApertura.Text.Length > 0)
            {
                try
                {
                    decimal MontoApertura = decimal.Parse(txt_montoApertura.Text);
                    string observaciones = txt_observaciones.Text;
                    if (MontoApertura < 0)
                    {
                        mensajeAlerta(tiposAdvertencias.advertencia, "Valor Negativo!!", "El Campo de Monto no puede tener un valor negativo", true);
                    }
                    else
                    {
                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid userId = (Guid)membershipUser.ProviderUserKey;
                        CajaChicaComedor_obj ch = new CajaChicaComedor_obj();
                        string result = ch.IngresarApertura(MontoApertura, observaciones, userId);
                        if (result.Equals("1"))
                        {
                            mensajeAlerta(tiposAdvertencias.informacion, "Apertura Registrada Correctamente!!", "La apertura se registro Correctamente", true);
                            contenidoCuerpo.Visible = true;
                            contenidoApertura.Visible = false;
                        }
                        else
                        {
                            mensajeAlerta(tiposAdvertencias.advertencia, "Error al Guardar la Apertura", result, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    mensajeAlerta(tiposAdvertencias.advertencia, "Valores incorrectos!!", ex.Message, true);
                }
            }
            else
            {
                mensajeAlerta(tiposAdvertencias.advertencia, "Campo Vacio!!", "El Campo de Monto no puede quedar Vacio", true);
            }
        }
        public void mensajeAlerta(tiposAdvertencias tipo, String titulo, String contenido, bool conMarquesina)
        {

            switch (tipo)
            {
                case tiposAdvertencias.informacion:
                    alert_attrib.Attributes.Add("class", "alert-info");
                    break;
                case tiposAdvertencias.exito:
                    alert_attrib.Attributes.Add("class", "alert-success");
                    break;
                case tiposAdvertencias.advertencia:
                    alert_attrib.Attributes.Add("class", "alert-warning");
                    break;
                case tiposAdvertencias.error:
                    alert_attrib.Attributes.Add("class", "alert-danger");
                    break;
                default:
                    break;
            }

            lbl_titleAlert.Text = titulo;
            if (conMarquesina)
            {
                contenido = "<marquee>" + contenido + "</marquee>";
            }
            lbl_bodyAlert.Text = contenido;
            alerta_smg.Visible = true;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "BorrarAlerta", "quitarAlerta();", true);
        }
        /// <summary>
        /// Cargar los Datos de la Caja del Dia en los controles de la pagina
        /// </summary>
        public void CargarDatosCajaChica()
        {
            try
            {
                CajaChicaComedor_obj caja = new CajaChicaComedor_obj();
                Tbl_CajaChica_Comedor cajaHoy = caja.getCaja(DateTime.Now.Date);
                //----------------------------------------------------
                lbl_fechaCaja.Text = DateTime.Now.ToLongDateString();
                lbl_totalApertura.Text = String.Format("{0:n}", cajaHoy.AperturaCaja);
                lbl_DescripcionApCaja.Text = cajaHoy.ObservacionesApertura;
                //----------------------[TOTAL INGRESOS]------------------------------
                lbl_TotalIngresos.Text = String.Format("{0:C2}", cajaHoy.TotalIngresos);
                //----------------------[TOTAL EGRESOS]------------------------------
                lbl_TotalEgresos.Text = String.Format("{0:C2}", cajaHoy.TotalEgresos);
                //----------------------[TOTAL GASTOS]------------------------------
                lbl_TotalGastosC.Text = String.Format("{0:C2}", cajaHoy.TotalGastos);
                //----------------------[TOTAL EN CAJA]------------------------------
                lbl_TotalCaja.Text = String.Format("{0:C2}", cajaHoy.TotalEnCaja);
                lbl_TotalCajaParaCierre.Text = String.Format("{0:C2}", cajaHoy.TotalEnCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btn_GuardarIngreso_Click(object sender, EventArgs e)
        {
            //string dt = Request.Form[txt_fechaRegistro.UniqueID];
            if (txt_Monto.Text.Length <= 0)
            {
                mensajeAlerta(tiposAdvertencias.advertencia, "Cantidad Vacia", "No puede dejar el cambo cantidad vacio y no puede ser = 0", true);
            }
            else
            {
                decimal Monto = decimal.Parse(txt_Monto.Text);
                if (Monto <= 0)
                {
                    mensajeAlerta(tiposAdvertencias.advertencia, "Cantidad Negativa", "La cantidad a Ingresar no puede tener un valor negativo", true);
                }
                else
                {
                    if (txt_recibedeentrega.Text.Length <= 0)
                    {
                        mensajeAlerta(tiposAdvertencias.advertencia, "Campo Vacio", "Por favor indique el nombre de la persona que entrega o recibe el dinero", true);
                    }
                    else
                    {
                        try
                        {
                            string recibeEntrega = txt_recibedeentrega.Text;
                            string ConceptoDe = txt_Concepto.Text;
                            var cultureInfo = new CultureInfo("es-ES");
                            /*
                             * AL UTILIZAR EL CONTROL DATETIME PICKER, LOS TEXTBOX NO CONSERVAN LOS VALORES, ASI QUE USANDO EL REGESFORM, SE OBTIENE EL VALOR DEL INPUT
                             */
                            string dt = Request.Form[txt_fechaRegistro.UniqueID];
                            //string dateString = txt_fechaRegistro.Text;
                            string dateString = dt;
                            DateTime fechaUsuario = DateTime.Parse(dateString, cultureInfo);
                            //Monto
                            //FechaIngreso
                            var membershipUser = System.Web.Security.Membership.GetUser();
                            Guid userId = (Guid)membershipUser.ProviderUserKey;

                            CajaChicaComedor_obj cajac = new CajaChicaComedor_obj();
                            string Result = cajac.insertarNevoIngreso(Monto, recibeEntrega, ConceptoDe, DateTime.Now.Date, fechaUsuario, userId);
                            if (Result.Equals("1"))
                            {
                                mensajeAlerta(tiposAdvertencias.exito, "Ingreso Realizado", "El Ingreso se Realizo Correctamente!!", true);
                                /*
                                 Limpiar los Campos
                                 */
                                txt_recibedeentrega.Text = "";
                                txt_Concepto.Text = "";
                                txt_Monto.Text = "";
                                txt_fechaRegistro.Text = "";
                                CargarDatosCajaChica();
                            }
                            else
                            {
                                mensajeAlerta(tiposAdvertencias.error, "Error Registrando el Ingreso", Result, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            mensajeAlerta(tiposAdvertencias.error, "Error", "Error Registrando el Ingreso ERROR: " + ex.Message, true);
                        }
                    }
                }
            }

        }

        protected void btn_GuardarEgreso_Click(object sender, EventArgs e)
        {
            //string dt = Request.Form[txt_fechaRegistro.UniqueID];
            if (txt_Monto.Text.Length <= 0)
            {
                mensajeAlerta(tiposAdvertencias.advertencia, "Cantidad Vacia", "No puede dejar el cambo cantidad vacio y no puede ser = 0", true);
            }
            else
            {
                decimal Monto = decimal.Parse(txt_Monto.Text);
                if (Monto <= 0)
                {
                    mensajeAlerta(tiposAdvertencias.advertencia, "Cantidad Negativa", "La cantidad a Egresar no puede tener un valor negativo", true);
                }
                else
                {
                    if (txt_recibedeentrega.Text.Length <= 0)
                    {
                        mensajeAlerta(tiposAdvertencias.advertencia, "Campo Vacio", "Por favor indique el nombre de la persona que entrega o recibe el dinero", true);
                    }
                    else
                    {
                        try
                        {
                            string recibeEntrega = txt_recibedeentrega.Text;
                            string ConceptoDe = txt_Concepto.Text;
                            var cultureInfo = new CultureInfo("es-ES");
                            /*
                             * AL UTILIZAR EL CONTROL DATETIME PICKER, LOS TEXTBOX NO CONSERVAN LOS VALORES, ASI QUE USANDO EL REGESFORM, SE OBTIENE EL VALOR DEL INPUT
                             */
                            string dt = Request.Form[txt_fechaRegistro.UniqueID];
                            //string dateString = txt_fechaRegistro.Text;
                            string dateString = dt;
                            DateTime fechaUsuario = DateTime.Parse(dateString, cultureInfo);
                            //Monto
                            //FechaIngreso
                            var membershipUser = System.Web.Security.Membership.GetUser();
                            Guid userId = (Guid)membershipUser.ProviderUserKey;

                            CajaChicaComedor_obj cajac = new CajaChicaComedor_obj();
                            string Result = cajac.insertarNuevoEgreso(Monto, recibeEntrega, ConceptoDe, DateTime.Now.Date, fechaUsuario, userId);
                            if (Result.Equals("1"))
                            {
                                mensajeAlerta(tiposAdvertencias.exito, "Egreso Realizado", "El Ingreso se Realizo Correctamente!!", true);
                                /*
                                 Limpiar los Campos
                                 */
                                txt_recibedeentrega.Text = "";
                                txt_Concepto.Text = "";
                                txt_Monto.Text = "";
                                txt_fechaRegistro.Text = "";
                                CargarDatosCajaChica();
                            }
                            else
                            {
                                mensajeAlerta(tiposAdvertencias.error, "Error Registrando el Egreso", Result, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            mensajeAlerta(tiposAdvertencias.error, "Error", "Error Registrando el Egreso ERROR: " + ex.Message, true);
                        }
                    }
                }
            }
        }

        protected void btn_GuardarGasto_Click(object sender, EventArgs e)
        {
            //string dt = Request.Form[txt_fechaRegistro.UniqueID];
            if (txt_Monto.Text.Length <= 0)
            {
                mensajeAlerta(tiposAdvertencias.advertencia, "Cantidad Vacia", "No puede dejar el cambo cantidad vacio y no puede ser = 0", true);
            }
            else
            {
                decimal Monto = decimal.Parse(txt_Monto.Text);
                if (Monto <= 0)
                {
                    mensajeAlerta(tiposAdvertencias.advertencia, "Cantidad Negativa", "La cantidad de Gasto no puede tener un valor negativo", true);
                }
                else
                {
                    if (txt_recibedeentrega.Text.Length <= 0)
                    {
                        mensajeAlerta(tiposAdvertencias.advertencia, "Campo Vacio", "Por favor indique el nombre de la persona que entrega o recibe el dinero", true);
                    }
                    else
                    {
                        try
                        {
                            string recibeEntrega = txt_recibedeentrega.Text;
                            string ConceptoDe = txt_Concepto.Text;
                            var cultureInfo = new CultureInfo("es-ES");
                            /*
                             * AL UTILIZAR EL CONTROL DATETIME PICKER, LOS TEXTBOX NO CONSERVAN LOS VALORES, ASI QUE USANDO EL REGESFORM, SE OBTIENE EL VALOR DEL INPUT
                             */
                            string dt = Request.Form[txt_fechaRegistro.UniqueID];
                            //string dateString = txt_fechaRegistro.Text;
                            string dateString = dt;
                            DateTime fechaUsuario = DateTime.Parse(dateString, cultureInfo);
                            //Monto
                            //FechaIngreso
                            var membershipUser = System.Web.Security.Membership.GetUser();
                            Guid userId = (Guid)membershipUser.ProviderUserKey;

                            CajaChicaComedor_obj cajac = new CajaChicaComedor_obj();
                            string Result = cajac.insertarNuevoGasto(Monto, recibeEntrega, ConceptoDe, DateTime.Now.Date, fechaUsuario, userId);
                            if (Result.Equals("1"))
                            {
                                mensajeAlerta(tiposAdvertencias.exito, "Gasto Registrado", "El Gasto se a registrado Correctamente!!", true);
                                /*
                                 Limpiar los Campos
                                 */
                                txt_recibedeentrega.Text = "";
                                txt_Concepto.Text = "";
                                txt_Monto.Text = "";
                                txt_fechaRegistro.Text = "";
                                CargarDatosCajaChica();
                            }
                            else
                            {
                                mensajeAlerta(tiposAdvertencias.error, "Error Registrando el Gasto", Result, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            mensajeAlerta(tiposAdvertencias.error, "Error", "Error Registrando el Gasto ERROR: " + ex.Message, true);
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lbl_titulo_REcibeEntrega.Text = "Recibe De:";
            btn_GuardarIngreso.Visible = true;
            btn_GuardarEgreso.Visible = false;
            btn_GuardarGasto.Visible = false;
            CargarModalRegistro();
        }

        protected void btn_NuevoEgreso_Click(object sender, EventArgs e)
        {
            lbl_titulo_REcibeEntrega.Text = "Entrega A: ";
            btn_GuardarIngreso.Visible = false;
            btn_GuardarEgreso.Visible = true;
            btn_GuardarGasto.Visible = false;
            CargarModalRegistro();

        }

        protected void btn_NuevoGasto_Click(object sender, EventArgs e)
        {
            lbl_titulo_REcibeEntrega.Text = "Entrega A: ";
            btn_GuardarIngreso.Visible = false;
            btn_GuardarEgreso.Visible = false;
            btn_GuardarGasto.Visible = true;
            CargarModalRegistro();
        }
        public void CargarModalRegistro()
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "CargarModalRegistro", "ShowModalRegistro();", true);
        }

        protected void gv_IngresosEnCaja_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btn_MasDetallesIngreso_Click(object sender, EventArgs e)
        {
            fillDetalle_Ingreso();
        }
        public void fillDetalle_Ingreso()
        {
            CajaChicaComedor_obj ch = new CajaChicaComedor_obj();
            List<usp_IngresosEnCajaResult> Result = ch.getAllIngresosEnCaja(DateTime.Now.Date);
            gv_IngresosEnCaja.DataSource = Result;
            gv_IngresosEnCaja.DataBind();
            if (gv_IngresosEnCaja.Rows.Count > 0)
            {
                this.gv_IngresosEnCaja.UseAccessibleHeader = true;
                TableCellCollection cells = gv_IngresosEnCaja.HeaderRow.Cells;
                gv_IngresosEnCaja.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                cells[3].Attributes.Add("data-breakpoints", "all");
                cells[4].Attributes.Add("data-breakpoints", "all");
                //   cells[5].Attributes.Add("data-breakpoints", "all");
                gv_IngresosEnCaja.HeaderRow.TableSection = TableRowSection.TableHeader;

                gv_IngresosEnCaja.Visible = true;
                gv_EgresosEnCaja.Visible = false;
                gv_GastosEnCaja.Visible = false;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "CargarModalIngresos", "ShowModalDetalles_mb();", true);
            }

        }
        public void fillDetalle_Egresos()
        {
            CajaChicaComedor_obj ch = new CajaChicaComedor_obj();
            List<usp_EgresosEnCajaResult> Result = ch.getAllEgresosEnCaja(DateTime.Now.Date);
            gv_EgresosEnCaja.DataSource = Result;
            gv_EgresosEnCaja.DataBind();
            if (gv_EgresosEnCaja.Rows.Count > 0)
            {
                this.gv_EgresosEnCaja.UseAccessibleHeader = true;
                TableCellCollection cells = gv_EgresosEnCaja.HeaderRow.Cells;
                gv_EgresosEnCaja.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                cells[3].Attributes.Add("data-breakpoints", "all");
                cells[4].Attributes.Add("data-breakpoints", "all");
                //   cells[5].Attributes.Add("data-breakpoints", "all");
                gv_EgresosEnCaja.HeaderRow.TableSection = TableRowSection.TableHeader;

                gv_EgresosEnCaja.Visible = false;
                gv_EgresosEnCaja.Visible = true;
                gv_GastosEnCaja.Visible = false;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "CargarModalIngresos", "ShowModalDetalles_mb();", true);
            }

        }
        public void fillDetalle_Gastos()
        {
            CajaChicaComedor_obj ch = new CajaChicaComedor_obj();
            List<usp_GastosEnCajaResult> Result = ch.getAllGastosEnCaja(DateTime.Now.Date);
            gv_GastosEnCaja.DataSource = Result;
            gv_GastosEnCaja.DataBind();
            if (gv_GastosEnCaja.Rows.Count > 0)
            {
                this.gv_GastosEnCaja.UseAccessibleHeader = true;
                TableCellCollection cells = gv_GastosEnCaja.HeaderRow.Cells;
                gv_GastosEnCaja.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                cells[3].Attributes.Add("data-breakpoints", "all");
                cells[4].Attributes.Add("data-breakpoints", "all");
                //   cells[5].Attributes.Add("data-breakpoints", "all");
                gv_GastosEnCaja.HeaderRow.TableSection = TableRowSection.TableHeader;

                gv_GastosEnCaja.Visible = false;
                gv_GastosEnCaja.Visible = false;
                gv_GastosEnCaja.Visible = true;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "CargarModalIngresos", "ShowModalDetalles_mb();", true);
            }

        }
        protected void gv_Egresos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btn_MasDetallesEgreso_Click(object sender, EventArgs e)
        {
            fillDetalle_Egresos();
        }

        protected void gv_GastosEnCaja_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btn_MasDetallesGastos_Click(object sender, EventArgs e)
        {
            fillDetalle_Gastos();
        }

        protected void btn_CierreCaja_Click(object sender, EventArgs e)
        {
            bool collapse_Cierre = (bool)Session["collapse_Cierre"];
            if (collapse_Cierre)
            {
                collapse_Cierre = false;
                card_Cierre.Visible = true;
            }
            else
            {
                collapse_Cierre = true;
                card_Cierre.Visible = false;
            }
            Session["collapse_Cierre"] = collapse_Cierre;
        }

        protected void agregarDen_Click(object sender, EventArgs e)
        {
            if (txt_denominacion.Text.Length > 0 && txt_Cantidad.Text.Length > 0)
            {
                try
                {
                    decimal denom = Convert.ToDecimal(txt_denominacion.Text);
                    int cant = int.Parse(txt_Cantidad.Text);
                    var membershipUser = System.Web.Security.Membership.GetUser();
                    Guid userId = (Guid)membershipUser.ProviderUserKey;
                    List<tbl_ArqueoCajaChica_Comedor_obj> listaA = (List<tbl_ArqueoCajaChica_Comedor_obj>)Session["ArqueoList"];
                    int Indice = 1;//Se usa para crear un id unico temporal para remover de la lista cuando sea necesario, pero al guardar de la base de datos se debe omitir puesto que la tabla tiene auto increment
                    try
                    {
                        tbl_ArqueoCajaChica_Comedor_obj listaA_Last = listaA.Last();

                        if (listaA_Last != null)
                        {
                            Indice = listaA_Last.ID_ArqueoCajaChica_Comedor;
                            Indice++;
                        }
                    }
                    catch (Exception)
                    {

                    }


                    tbl_ArqueoCajaChica_Comedor_obj arqueocc = new tbl_ArqueoCajaChica_Comedor_obj();
                    arqueocc.ID_ArqueoCajaChica_Comedor = Indice;
                    arqueocc.Denominacion = denom;
                    arqueocc.Cantidad_Denominacion = cant;
                    arqueocc.TotalDenominacion_local = denom * cant;
                    arqueocc.RegistradoPor = userId;

                    listaA.Add(arqueocc);

                    txt_denominacion.Text = "";
                    txt_Cantidad.Text = "";

                    CargarListaArqueo();
                }
                catch (Exception)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "AlertMsg", "<script language='javascript'>alert('Acegurece de solo ingresar numeros en los campos, para reprecentar 50 Centavos use 0.5 o 0.50 y solo se admiten enteros en Cantidad, no se admiten letras ni caracteres especiales');</script>", false);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "AlertMsg", "<script language='javascript'>alert('Los Campos Denominacion y Cantidad No pueden Quedar Vacios');</script>", false);
            }
        }
        public void CargarListaArqueo()
        {
            List<tbl_ArqueoCajaChica_Comedor_obj> listaA = (List<tbl_ArqueoCajaChica_Comedor_obj>)Session["ArqueoList"];
            gv_Denominaciones.DataSource = listaA;
            gv_Denominaciones.DataBind();
            if (gv_Denominaciones.Rows.Count > 0)
            {
                this.gv_Denominaciones.UseAccessibleHeader = true;
                TableCellCollection cells = gv_Denominaciones.HeaderRow.Cells;
                gv_Denominaciones.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //cells[3].Attributes.Add("data-breakpoints", "all");
                //cells[4].Attributes.Add("data-breakpoints", "all");
                //cells[5].Attributes.Add("data-breakpoints", "all");
                gv_Denominaciones.HeaderRow.TableSection = TableRowSection.TableHeader;

                lbl_TotalArqueo.Text = String.Format("{0:C2}", listaA.Sum(a => a.TotalDenominacion_local));
            }
        }

        protected void gv_Denominaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "remover")
            {

                gv_Denominaciones.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDArqueoCajaChica_Comedor = Convert.ToInt32(gv_Denominaciones.SelectedValue);
                List<tbl_ArqueoCajaChica_Comedor_obj> listaA = (List<tbl_ArqueoCajaChica_Comedor_obj>)Session["ArqueoList"];

                tbl_ArqueoCajaChica_Comedor_obj listaA_Last = listaA.Last();

                tbl_ArqueoCajaChica_Comedor_obj remover = listaA.Where(s => s.ID_ArqueoCajaChica_Comedor == IDArqueoCajaChica_Comedor).FirstOrDefault();
                listaA.Remove(remover);
                CargarListaArqueo();
                if (listaA.Count <= 0)
                {
                    //btn_Guardar.Visible = false;
                }
            }
        }

        protected void btn_ConfirmarCierre_Click(object sender, EventArgs e)
        {
            string comentarioCierre = txt_ComentarioCierre.Text;
            List<tbl_ArqueoCajaChica_Comedor_obj> listaA = (List<tbl_ArqueoCajaChica_Comedor_obj>)Session["ArqueoList"];
            var membershipUser = System.Web.Security.Membership.GetUser();
            Guid userId = (Guid)membershipUser.ProviderUserKey;
            CajaChicaComedor_obj chc = new CajaChicaComedor_obj();
            //-----------------Guardar
            string result = chc.CrearCierreDeCaja(DateTime.Now, listaA, userId, comentarioCierre);

            string msg = result;
            if (result.Equals("1"))
            {
                msg = "El Cierre de Caja Se ha Realizado Correctamente";
                contenidoCuerpo.Visible = false;
                CuerpoCajaCerrada.Visible = true;
            }
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "AlertMsg", "<script language='javascript'>alert('" + msg + "');</script>", false);
        }
    }
}