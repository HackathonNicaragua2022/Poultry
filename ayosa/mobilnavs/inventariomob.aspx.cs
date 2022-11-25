using accesoDatos;
using accesoDatos.ProduccionHuevos.Clasificadora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class inventariomob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fillInventario();
            fillCajillasClasificadasDia();
            fillCajillasProcesadasDia();            
        }
        public void fillInventario()
        {
            inventarioHuevoSinClasificar inventario = new inventarioHuevoSinClasificar();
            List<USP_Consolidado_InventarioHSCResult> rs = inventario.getHuevoEnBodehaHSC();

            decimal total_General = (decimal)rs.Sum(a => a.SALDO);
            lbl_TotalGeneral.Text = String.Format("{0:n}", total_General);
            rs.ToList().ForEach(c => { c.TotalGeneral = total_General; c.Porcentaje = ((decimal)(c.SALDO / total_General) * 100); });


            //rp_inventariohsc.DataSource = rs;
            //rp_inventariohsc.DataBind();

            rp_Tabla.DataSource = rs;
            rp_Tabla.DataBind();

        }
        public void fillCajillasClasificadasDia()
        {
            inventarioHuevoSinClasificar inventario = new inventarioHuevoSinClasificar();
            rp_totalClasificadoEnelDia.DataSource = inventario.TotalCajillasClasificadas();
            rp_totalClasificadoEnelDia.DataBind();
        }

        public void fillCajillasProcesadasDia()
        {
            HuevoProcesado inventario = new HuevoProcesado();
            rp_HuevoProduccion.DataSource = inventario.getAllCajillasProcesadasDia();
            rp_HuevoProduccion.DataBind();
        }
        protected void rp_inventariohsc_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName.Equals("cmd_MostarDetalle"))
            //{
            Session["IDTipoHuevoDetalleIHSC"] = int.Parse(e.CommandArgument.ToString());
            Response.Redirect("detalleProduccionMobilNav.aspx");
            //}
        }
        protected void btn_Mostrar_Click(object sender, EventArgs e)
        {
            if (!Cards.Visible)
            {
                Cards.Visible = true;
                btn_Mostrar.Text = "Ocultar Clasificaciones y Produccion del Dia";
            }
            else
            {
                Cards.Visible = false;
                btn_Mostrar.Text = "Mostrar Clasificaciones y Produccion del Dia";
            }
        }                      
    }
}