using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos.test;
using accesoDatos;
namespace POULTRY.Admin_gerencial
{
    public partial class PesoBascula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fillPesos()
        {
            List<Tbl_PesosBascula_test> result = new pesosbascula().getAllPesos();
            gv_pesos.DataSource = result;
            gv_pesos.DataBind();
            if (gv_pesos.Rows.Count > 0)
            {
                this.gv_pesos.UseAccessibleHeader = true;
                TableCellCollection cells = gv_pesos.HeaderRow.Cells;
                gv_pesos.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //cells[1].Attributes.Add("data-breakpoints", "all");
                //cells[3].Attributes.Add("data-breakpoints", "all");
                //cells[5].Attributes.Add("data-breakpoints", "all");
                //cells[8].Attributes.Add("data-breakpoints", "all");
                //cells[9].Attributes.Add("data-breakpoints", "all");
                gv_pesos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void Unnamed_Tick(object sender, EventArgs e)
        {
            fillPesos();
        }
    }
}