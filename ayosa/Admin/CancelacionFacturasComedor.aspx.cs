using accesoDatos;
using accesoDatos.Comedor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.Admin
{
    public partial class CancelacionFacturasComedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["TotalCobrar"] = (decimal)0;
                Session["TotalItem"] = (int)0;
                lbl_fechaReporte.Text = DateTime.Now.ToLongDateString();
            }
        }
        public void fill_facturasPendientes(DateTime fechaInicial, DateTime fechaFinal)
        {
            Tbl_FaturaComedor_obj facturasPendientes = new Tbl_FaturaComedor_obj();
            List<USP_TrabajadoresConFacturasPendientesxRangoFechaResult> facturas = facturasPendientes.ObtenerFacturasPendientesPorRangoFecha(fechaInicial, fechaFinal);
            lbl_totalClientes.Text = facturas.Count.ToString() + " Pers";
            gv_facturas_por_trabajadador.DataSource = facturas;
            gv_facturas_por_trabajadador.DataBind();
            if (gv_facturas_por_trabajadador.Rows.Count > 0)
            {
                this.gv_facturas_por_trabajadador.UseAccessibleHeader = true;
                TableCellCollection cells = gv_facturas_por_trabajadador.HeaderRow.Cells;


                gv_facturas_por_trabajadador.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //cells[1].Attributes.Add("data-breakpoints", "all");                
                cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                cells[3].Attributes.Add("data-breakpoints", "all");
                //cells[4].Attributes.Add("data-breakpoints", "xs sm md");           
                gv_facturas_por_trabajadador.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                lbl_totalCobrar.Text = string.Format("{0:C}", 0);
                lbl_cantidadArticulosTotal.Text = string.Format("{0:n}", 0);
            }
        }
        public void fill_TodasLasfacturasPendientes()
        {
            Tbl_FaturaComedor_obj facturasPendientes = new Tbl_FaturaComedor_obj();
            List<USP_TodasLasFacturasPendientesResult> facturas = facturasPendientes.ObtenerTodasLasFacturasPendientes();
            lbl_totalClientes.Text = facturas.Count.ToString() + " Pers";
            gv_facturas_por_trabajadador.DataSource = facturas;
            gv_facturas_por_trabajadador.DataBind();
            if (gv_facturas_por_trabajadador.Rows.Count > 0)
            {
                this.gv_facturas_por_trabajadador.UseAccessibleHeader = true;
                TableCellCollection cells = gv_facturas_por_trabajadador.HeaderRow.Cells;


                gv_facturas_por_trabajadador.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //cells[1].Attributes.Add("data-breakpoints", "all");                
                cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                cells[3].Attributes.Add("data-breakpoints", "all");
                //cells[4].Attributes.Add("data-breakpoints", "xs sm md");           
                gv_facturas_por_trabajadador.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                lbl_totalCobrar.Text = string.Format("{0:C}", 0);
                lbl_cantidadArticulosTotal.Text = string.Format("{0:n}", 0);
            }
        }
        protected void btn_cargar_Click(object sender, EventArgs e)
        {
            if (txt_rangofecha.Text.Length > 0)
            {
                try
                {
                    string[] fechas = txt_rangofecha.Text.Trim().Split('-');
                    CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                    DateTime fecha1 = DateTime.Parse(fechas[0], provider);
                    Session["fechaInicial"] = fecha1;
                    DateTime fecha2 = DateTime.Parse(fechas[1], provider);
                    Session["fechaFinal"] = fecha2;
                    Session["TipoCargaFacturas"] = tipoCarga.PORRANGOFECHA;
                    Session["TotalCobrar"] = 0.0M;
                    Session["TotalItem"] = 0;
                    fill_facturasPendientes(fecha1, fecha2);
                    mensaje("Se muestran las facturas en contradas en el rango seleccionado!", tiposAdvertencias.informacion);
                    //mensaje(fecha1.ToLongDateString() + " fecha 2= " + fecha2.ToLongDateString(), tiposAdvertencias.informacion);    
                }
                catch (Exception ex)
                {
                    mensaje("Ocurrio un error: " + ex.Message, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("El Campo de Fecha esta Vacio, por favor use el cuadro de calendario para seleccionar una fecha valida", tiposAdvertencias.error);
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

        protected void gv_facturas_por_trabajadador_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gv_facturas_por_trabajadador_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID_Trabajador = (int)gv_facturas_por_trabajadador.DataKeys[e.Row.RowIndex].Value;
                //----------------------------------------------------------------------------------

                GridView gv_Facturas = (GridView)e.Row.FindControl("gv_Facturas");
                Tbl_FaturaComedor_obj facturasxTrabajador = new Tbl_FaturaComedor_obj();
                List<Tbl_FaturaComedor_obj.facturasComedor> result = new List<Tbl_FaturaComedor_obj.facturasComedor>();

                if ((tipoCarga)Session["TipoCargaFacturas"] == tipoCarga.PORRANGOFECHA)
                {
                    DateTime fecha1 = (DateTime)Session["fechaInicial"];
                    DateTime fecha2 = (DateTime)Session["fechaFinal"];

                    result = facturasxTrabajador.getFacturasComedorxidTrabajador(ID_Trabajador, fecha1, fecha2);
                }
                else
                {
                    result = facturasxTrabajador.getFacturasComedorxidTrabajador(ID_Trabajador);
                }
                Tbl_FaturaComedor_obj.facturasComedor resumen = new Tbl_FaturaComedor_obj.facturasComedor();
                resumen.FacturadoP = "RESUMEN FACTURAS ===>";
                resumen.ID_Factura = 0;
                resumen.Fecha_Factura_Hora = DateTime.Now;
                resumen.ID_Trabajador = 0;
                resumen.FacturadoPor = new Guid();
                resumen.MontoTotalFactura = result.Sum(a => a.MontoTotalFactura);
                resumen.NumerodeProductos = result.Sum(a => a.NumerodeProductos);
                resumen.Cancelada = true;
                resumen.Fecha_Cancelacion = DateTime.Now;
                resumen.ConceptoCancelacion = "";

                List<Tbl_FaturaComedor_obj.facturasComedor> listafacturas = result.ToList();
                listafacturas.Add(resumen);
                gv_Facturas.DataSource = listafacturas;
                gv_Facturas.DataBind();

                decimal totalCobrar = (decimal)Session["TotalCobrar"];
                int totalItem = (int)Session["TotalItem"];
                totalCobrar += result.Sum(a => a.MontoTotalFactura);
                totalItem += result.Sum(a => a.NumerodeProductos);

                Session["TotalCobrar"] = totalCobrar;
                Session["TotalItem"] = totalItem;

                lbl_totalCobrar.Text = string.Format("{0:C}", totalCobrar);
                lbl_cantidadArticulosTotal.Text = string.Format("{0:n}", totalItem);
            }
        }

        protected void gv_Facturas_PreRender(object sender, EventArgs e)
        {
            GridView gvProducts = sender as GridView;

            GridViewRow getRow = gvProducts.Rows[gvProducts.Rows.Count - 1];
            getRow.Attributes.Add("class", "bg-dark");
            // or
            // getRow.BackColor = System.Drawing.Color.FromName("#EE0000");
        }

        protected void btn_CancelarFacturasRango_Click(object sender, EventArgs e)
        {
            btn_Continuar.Visible = true;
            btn_ContinuarTodo.Visible = false;
            modalAdvertencia("Esta seguro que desea Cancelar todas las facturas entre el rango de fecha seleccionado!!<br/><br/>¿Desea continuar?");
        }
        private void modalAdvertencia(String msg)
        {
            lbl_MsgAdvertencia.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalAdvertencia", "ShowModalAdvertencia();", true);
        }
        protected void btn_CancelarTodo_Click(object sender, EventArgs e)
        {
            btn_Continuar.Visible = false;
            btn_ContinuarTodo.Visible = true;
            modalAdvertencia("Esta seguro que desea Cancelar todas las facturas en el sistema<br/>esto incluye facturas creadas el dia de Hoy!!<br/><br/>¿Desea continuar?");
        }

        protected void btn_CargarTodasLasFacturas_Click(object sender, EventArgs e)
        {
            Session["TotalCobrar"] = 0.0M;
            Session["TotalItem"] = 0;

            Session["TipoCargaFacturas"] = tipoCarga.TODASLASFACTURAS;
            fill_TodasLasfacturasPendientes();
            mensaje("Se muestran todas las facturas de Credito pendiente por cancelar en el sistema", tiposAdvertencias.informacion);
        }
        private enum tipoCarga
        {
            PORRANGOFECHA,
            TODASLASFACTURAS
        }

        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (txt_rangofecha.Text.Length > 0)
            {
                try
                {
                    string[] fechas = txt_rangofecha.Text.Trim().Split('-');
                    CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                    DateTime fecha1 = DateTime.Parse(fechas[0], provider);
                    DateTime fecha2 = DateTime.Parse(fechas[1], provider);

                    var membershipUser = System.Web.Security.Membership.GetUser();
                    Guid userId = (Guid)membershipUser.ProviderUserKey;

                    Tbl_FaturaComedor_obj facturasPendientes = new Tbl_FaturaComedor_obj();
                    long total_facturas = facturasPendientes.CancelarTodaslasFacturasxRango(fecha1, fecha2, userId);


                    mensaje("Un Total de " + String.Format("{0:n}", total_facturas) + " facturas, fueron canceldas en el rango especificado !", tiposAdvertencias.informacion);
                    //mensaje(fecha1.ToLongDateString() + " fecha 2= " + fecha2.ToLongDateString(), tiposAdvertencias.informacion);    
                }
                catch (Exception ex)
                {
                    mensaje("Ocurrio un error: " + ex.Message, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("El Campo de Fecha esta Vacio, por favor use el cuadro de calendario para seleccionar una fecha valida", tiposAdvertencias.error);
            }
        }

        protected void btn_ContinuarTodo_Click(object sender, EventArgs e)
        {
            var membershipUser = System.Web.Security.Membership.GetUser();
            Guid userId = (Guid)membershipUser.ProviderUserKey;

            Tbl_FaturaComedor_obj facturasPendientes = new Tbl_FaturaComedor_obj();
            long total_facturas = facturasPendientes.CancelarTodaslasFacturasCredito(userId);

            mensaje("Un Total de " + String.Format("{0:n}", total_facturas) + " facturas, fueron canceldas, Felicidades todas las cuentas fueron Canceladas !", tiposAdvertencias.informacion);
        }

        protected void btn_reportePorRango_Click(object sender, EventArgs e)
        {
            if (txt_rangofecha.Text.Length > 0)
            {
                try
                {
                    string[] fechas = txt_rangofecha.Text.Trim().Split('-');
                    CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                    DateTime fecha1 = DateTime.Parse(fechas[0], provider);
                    DateTime fecha2 = DateTime.Parse(fechas[1], provider);

                    Response.Redirect("/Reportes/visorReporte.aspx?tiporeporte=fptrf&fechai=" + fecha1.ToShortDateString() + "&fechaf=" + fecha2.ToShortDateString());
                }
                catch (Exception ex)
                {
                    mensaje("Ocurrio un error: " + ex.Message, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("El Campo de Fecha esta Vacio, por favor use el cuadro de calendario para seleccionar una fecha valida", tiposAdvertencias.error);
            }
        }

        protected void btn_reporteTodas_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/Reportes/visorReporte.aspx?tiporeporte=fptsrf");
            }
            catch (Exception ex)
            {
                mensaje("Ocurrio un error: " + ex.Message, tiposAdvertencias.error);
            }
        }
    }
}