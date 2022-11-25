using accesoDatos.ProduccionHuevos.Clasificadora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.ModuloClasificadoraHuevos
{
    public partial class InventarioHuevoSinClasificar : System.Web.UI.Page
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
            rp_inventariohsc.DataSource = inventario.getHuevoEnBodehaHSC();
            rp_inventariohsc.DataBind();
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
            Response.Redirect("DetalleProduccionxTipoHuevo.aspx");
            //}
        }
    }
}