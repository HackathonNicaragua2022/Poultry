using accesoDatos.ProduccionHuevos.Clasificadora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.ModuloClasificadoraHuevos
{
    public partial class DetalleProduccionxTipoHuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IDTipoHuevoDetalleIHSC"] != null)
                {
                    int IDTipoHuevo = (int)Session["IDTipoHuevoDetalleIHSC"];
                    inventarioHuevoSinClasificar inventario = new inventarioHuevoSinClasificar();
                    rp_DetallesIHSC.DataSource = inventario.getSaldoInventarioHuevoSinClasificarxID(IDTipoHuevo);
                    rp_DetallesIHSC.DataBind();
                }
            }
        }
    }
}