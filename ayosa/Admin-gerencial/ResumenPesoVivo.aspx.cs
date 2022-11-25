using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.Admin_gerencial
{
    public partial class ResumenPesoVivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_FechaMatanza.Text = DateTime.Now.ToShortDateString();
                btn_CargarDatos_Click(this, null);
            }
            UpdateDatosGenerales();
        }

        public void UpdateDatosGenerales()
        {
            if (Session["IDCompra_Seleccionada"] != null)
            {
                resumendelDiaRemision resumenGeneral = new resumendelDiaRemision();
                int IDCompra = (int)Session["IDCompra_Seleccionada"];
                resumendelDiaRemision.datosGenerales datosGenerales = resumenGeneral.getResumenCompraPollosAGranja(IDCompra);
                if (datosGenerales != null)
                {
                    lbl_fechaMatanza.Text = datosGenerales.Fecha.ToLongDateString();
                    lbl_lote.Text = datosGenerales.NumeroLote1;
                    lbl_TotalAves.Text = string.Format("{0:n}", datosGenerales.TotalAves) + " Aves";

                    int totalAvesxRemision = datosGenerales.TotalAvesRemision <= 0 ? 1 : datosGenerales.TotalAvesRemision;// correjir divicion por 0

                    lbl_totalAvesRemision.Text = string.Format("{0:n}", totalAvesxRemision) + " Aves";
                    lbl_TotalJavas.Text = string.Format("{0:n}", datosGenerales.TotalJavas) + " Unds";

                    decimal TotalLibrasPesadas = datosGenerales.TotalLibrasPesadas;

                    lbl_LibrasPesadas.Text = string.Format("{0:n}", TotalLibrasPesadas) + " lbs";

                    //Este peso promedio suma las cantidades de pollos enviadas por remison
                    //se tomo la decision de calcular el peso promedio en base a las javas pesdas por ende usando totalavesxRemision
                    //lbl_TotalPesoPromedio.Text = string.Format("{0:n}", datosGenerales.PesoPromedio) + " lbs";
                    decimal pesoPromedio = TotalLibrasPesadas / totalAvesxRemision;
                    lbl_TotalPesoPromedio.Text = string.Format("{0:n}", pesoPromedio) + " lbs";

                    lbl_EdadDias.Text = datosGenerales.EdadDias.ToString() + " Dias";


                    //-----------------------------------                
                    Tbl_ViajesRemisionGranja_obj viajes = new Tbl_ViajesRemisionGranja_obj();
                    List<Tbl_ViajesRemisionGranja_obj.ViajesRemision> remisiones = viajes.getAllViajesRemision(IDCompra);
                    gv_Remisiones.DataSource = pesoAcumulado(remisiones);
                    gv_Remisiones.DataBind();
                }
            }
        }


        public List<Tbl_ViajesRemisionGranja_obj.ViajesRemision> pesoAcumulado(List<Tbl_ViajesRemisionGranja_obj.ViajesRemision> remisiones)
        {
            List<Tbl_ViajesRemisionGranja_obj.ViajesRemision> RemisionesConAcumulado = new List<Tbl_ViajesRemisionGranja_obj.ViajesRemision>();
            List<Tbl_ViajesRemisionGranja_obj.ViajesRemision> RemisionesSinAcumulado = new List<Tbl_ViajesRemisionGranja_obj.ViajesRemision>();
            foreach (Tbl_ViajesRemisionGranja_obj.ViajesRemision peso in remisiones)
            {
                RemisionesSinAcumulado.Add(peso);
                peso.PesoAcumulado = (decimal)RemisionesSinAcumulado.Sum(a => a.TotalLibrasxRemision);
                RemisionesConAcumulado.Add(peso);
            }
            return RemisionesConAcumulado;
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateDatosGenerales();
        }

        protected void rp_ProcesosEn_elDia_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int IDCompraSeleccionado = int.Parse(e.CommandArgument.ToString());
            Session["IDCompra_Seleccionada"] = IDCompraSeleccionado;
            UpdateDatosGenerales();
        }

        protected void btn_CargarDatos_Click(object sender, EventArgs e)
        {
            if (txt_FechaMatanza.Text.Length > 0)
            {
                DateTime fechaMatanza = Convert.ToDateTime(txt_FechaMatanza.Text, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat);
                try
                {
                    resumendelDiaRemision resumenGeneral = new resumendelDiaRemision();
                    //Total de remisiones para la fecha, si el resultado es uno se carga solo esa remision, si no se cargan todas en la lista para seleccionar cual se vera                  
                    int totalProcesos = resumenGeneral.getTotalComprasFecha(fechaMatanza);
                    if (totalProcesos == 1)
                    {
                        Session["IDCompra_Seleccionada"] = resumenGeneral.getResumenDiaRemision(fechaMatanza).IDCompra;
                        rp_ProcesosEn_elDia.DataSource = null;
                        rp_ProcesosEn_elDia.DataBind();
                    }
                    else if (totalProcesos > 1)
                    {
                        rp_ProcesosEn_elDia.DataSource = resumenGeneral.getResumenProcesosxFecha(fechaMatanza);
                        rp_ProcesosEn_elDia.DataBind();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}