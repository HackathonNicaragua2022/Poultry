using accesoDatos;
using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.livetest
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_ejecutarPesoVivo_Click(object sender, EventArgs e)
        {
            MembershipUser user = Membership.GetUser("CEO");
            Guid guid = (Guid)user.ProviderUserKey;

            tbl_IngresoPesoVivo nuevoPeso = new tbl_IngresoPesoVivo();
            nuevoPeso.IngresadoPor = guid;
            nuevoPeso.ID_ViajesRemision = 7;
            nuevoPeso.NumPesaje =1;
            nuevoPeso.CantidadJavasPesadas = 100;
            nuevoPeso.PollosxJava = 12;
            nuevoPeso.PesoJavaConPollo_Libras = Convert.ToDecimal("1200.50");
            nuevoPeso.PesoxJavaVacia_libDefault = Convert.ToDecimal("15.50");
            //---------------------------------------------------
            Tbl_IngresoPesoVivo_Obj IngresoPesoVivo = new Tbl_IngresoPesoVivo_Obj();
            string resul = IngresoPesoVivo.NuevoPesoVivo(nuevoPeso, 1, DateTime.Now);
            Response.Write(resul);
        }
    }
}