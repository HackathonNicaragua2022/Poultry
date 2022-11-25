using accesoDatos;
using accesoDatos.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.Admin_gerencial
{
    public partial class ConteoPollos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tm_conteo_Tick(object sender, EventArgs e)
        {
            CargarTotalPollos();
        }
        public void CargarTotalPollos()
        {
            tbl_MataderoTest_obj mt = new tbl_MataderoTest_obj();
            int totalPollos = mt.getavesContadas();            
            lbl_patas.Text = string.Format("{0:n}", (totalPollos));
        }
    }
}