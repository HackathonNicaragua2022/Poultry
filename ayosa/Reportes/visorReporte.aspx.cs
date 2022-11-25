using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.Reportes
{
    public partial class visorReporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Obtiene el valor de la variable que indicara que reporte ver
                    String tipoReporte = Request.QueryString["tiporeporte"].ToString();
                    switch (tipoReporte)
                    {
                        case "rppv":
                            // LOS PARAMETROS TIENE CASO SENSITIVE, RESPONDEN A MAYUSCULAS O MINUSCULAS
                            PesoVivoReport PesoVivo = new PesoVivoReport();
                            int CodigoCompra = int.Parse(Request.QueryString["codcompra"]);
                            PesoVivo.RequestParameters = false;
                            PesoVivo.Parameters["IDCOMPRA"].Value = CodigoCompra;
                            PesoVivo.Parameters["IDCOMPRA"].Visible = false;
                            visorReportes.Report = PesoVivo;
                            visorReportes.ReportTypeName = "POULTRY.Reportes.PesoVivoReport";

                            visorReportes.DataBind();
                            break;
                        case "fptrf":// facturas por trabajador y rango de fechas


                            // LOS PARAMETROS TIENE CASO SENSITIVE, RESPONDEN A MAYUSCULAS O MINUSCULAS
                            facturascomedorxRangoFecha facturasPendientesxCobrar = new facturascomedorxRangoFecha();
                            string valfechaInicial = Request.QueryString["fechai"];
                            string valfechaFinal = Request.QueryString["fechaf"];
                            
                            CultureInfo provider = CultureInfo.CreateSpecificCulture("es-EN");
                            DateTime fechaInicial = DateTime.Parse(valfechaInicial, provider);
                            DateTime fechaFinal = DateTime.Parse(valfechaFinal, provider);
                            facturasPendientesxCobrar.RequestParameters = false;
                            facturasPendientesxCobrar.Parameters["fechaInicial"].Value = fechaInicial;
                            facturasPendientesxCobrar.Parameters["fechaInicial"].Visible = false;

                            facturasPendientesxCobrar.Parameters["fechaFinal"].Value = fechaFinal;
                            facturasPendientesxCobrar.Parameters["fechaFinal"].Visible = false;

                            visorReportes.Report = facturasPendientesxCobrar;
                            visorReportes.ReportTypeName = "POULTRY.Reportes.facturascomedorxRangoFecha";

                            visorReportes.DataBind();
                            break;
                        case "fptsrf":// facturas por trabajador sin rango de fechas


                            // LOS PARAMETROS TIENE CASO SENSITIVE, RESPONDEN A MAYUSCULAS O MINUSCULAS
                            TodaslasFacturasCreditoPendientes todaslasFacturasCredito = new TodaslasFacturasCreditoPendientes();

                            visorReportes.Report = todaslasFacturasCredito;
                            visorReportes.ReportTypeName = "POULTRY.Reportes.TodaslasFacturasCreditoPendientes";
                            visorReportes.DataBind();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}