using accesoDatos.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.postTest
{
    public partial class PruebaPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Obtiene el valor de la variable idgalera de la Url
                    int TotalPollos = int.Parse(Request.QueryString["ConteoPollo"]);
                   // test.Text = TotalPollos.ToString();
                    tbl_MataderoTest_obj t = new tbl_MataderoTest_obj();
                    t.guardaarConteo(TotalPollos);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}