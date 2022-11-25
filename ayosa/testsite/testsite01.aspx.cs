using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;

namespace POULTRY.testsite
{
    public partial class testsite01 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_go_Click(object sender, EventArgs e)
        {
            TazaCambio taza = new TazaCambio();
            int ano = int.Parse(txt_ano.Text);
            int mes = int.Parse(txt_mes.Text);
            List<ultil.TazaCambio.tazaMes> taz = taza.getTazaMes(ano, mes);
            rp_taza.DataSource = taz;
            rp_taza.DataBind();
        }
    }
}