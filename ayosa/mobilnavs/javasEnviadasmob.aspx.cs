using accesoDatos;
using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class javasEnviadasmob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Session["idremision"] = int.Parse(Request.QueryString["idremision"]);
                    Session["usuario"] = Request.QueryString["usuario"].ToString();
                    Session["contrasena"] = Request.QueryString["contrasena"].ToString();
                    servicios.ServicioMatadero s = new servicios.ServicioMatadero();
                    if (!s.verifyUser((string)Session["usuario"], (string)Session["contrasena"]))
                    {
                        Response.Redirect("sinacceso.aspx");
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("sinacceso.aspx");
                }
            }
            fill_grid();
        }
        public void fill_grid()
        {
            if (Session["idremision"] != null)
            {
                int ID_ViajeRemision = (int)Session["idremision"];
                //----------------------------------------------------------------------------------                
                Tbl_JavasEnviadas_obj javasEnviadas = new Tbl_JavasEnviadas_obj();

                List<Tbl_JavasEnviadas> result = javasEnviadas.getAllJavasEnviadas(ID_ViajeRemision);

                gv_Pesajes.DataSource = result;
                gv_Pesajes.DataBind();
            }
        }
    }
}