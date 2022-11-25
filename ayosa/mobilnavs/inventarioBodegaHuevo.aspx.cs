using ControlHuevos.ClasesModelos.BodegaPrincipal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class inventarioBodegaHuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fill_grid();
        }
        public void fill_grid()
        {
            List<ControlHuevos.ClasesModelos.BodegaPrincipal.BodegaCentral.bodega> resultBodega = new BodegaCentral().getAllBodega();

            decimal totalCajillas = (decimal)resultBodega.Sum(a => a.TOTAL_CAJIAS);
            decimal totalHuevos = (decimal)resultBodega.Sum(a => a.EXISTENCIA);
            lbl_TotalCajias.Text = string.Format("{0:n}", totalCajillas);
           // lbl_TotalHuevos.Text = string.Format("{0:n}", totalHuevos);

            gv_BodegaHuevo.DataSource = resultBodega;
            gv_BodegaHuevo.DataBind();

            if (resultBodega.Count > 0)
            {
                this.gv_BodegaHuevo.UseAccessibleHeader = true;
                TableCellCollection cells = gv_BodegaHuevo.HeaderRow.Cells;
                gv_BodegaHuevo.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                // cells[1].Attributes.Add("data-breakpoints", "all");
                //cells[2].Attributes.Add("data-breakpoints", "all");
                //cells[3].Attributes.Add("data-breakpoints", "all");
                //cells[4].Attributes.Add("data-breakpoints", "all");
                //cells[5].Attributes.Add("data-breakpoints", "all");
                //cells[6].Attributes.Add("data-breakpoints", "all");
                //  cells[7].Attributes.Add("data-breakpoints", "all");
                gv_BodegaHuevo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            fill_grid();
        }

        protected void Unnamed_CheckedChanged(object sender, EventArgs e)
        {
            if (!chk_pararActualizacion.Checked)
            {
                //     Timer1.Enabled = false;
            }
            else
            {
                //    Timer1.Enabled = true;
            }
        }
    }
}