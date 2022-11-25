using accesoDatos.Comedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.Admin
{
    public partial class facturasPorFechaCancelacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fillFacturasCanceladasParaFecha();
        }
        public void fillFacturasCanceladasParaFecha()
        {
            if (Session["Fecha_FacturasCanceladas"] != null)
            {
                Tbl_FaturaComedor_obj facturasPendientes = new Tbl_FaturaComedor_obj();
                DateTime fecha = (DateTime)Session["Fecha_FacturasCanceladas"];
                rp_FacturasCanceladas.DataSource = facturasPendientes.ObtenerTrabajadoresConFacturasCanceladasxFecha(fecha);
                rp_FacturasCanceladas.DataBind();
            }
        }
        protected void rp_FacturasCanceladas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rp_FacturasCanceladas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void rp_facturas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}