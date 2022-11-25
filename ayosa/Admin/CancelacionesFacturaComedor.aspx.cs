using accesoDatos.Comedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.Admin
{
    public partial class CancelacionesFacturaComedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fill_FechaFacturasCanceladas();
        }
        public void Fill_FechaFacturasCanceladas()
        {
            Tbl_FaturaComedor_obj facturasComedor = new Tbl_FaturaComedor_obj();
            rp_FechasCancelacion.DataSource = facturasComedor.ObtenerFechasFacturasCanceladas(chk_devoluciones_0.Checked);
            rp_FechasCancelacion.DataBind();
        }
        protected void rp_FechasCancelacion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Facturas"))
            {
                DateTime fecha = DateTime.Parse(e.CommandArgument.ToString());
                System.Console.WriteLine(fecha);
                Session["Fecha_FacturasCanceladas"] = fecha;
                Response.Redirect("~/Admin/facturasPorFechaCancelacion.aspx");
            }
        }
    }
}