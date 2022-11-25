using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using accesoDatos;
using accesoDatos.Catalogos;
using accesoDatos.ProduccionPollos.PlantasProcesadoras;
namespace POULTRY.servicios
{
    /// <summary>
    /// Descripción breve de ServicioMatadero
    /// </summary>
    [WebService(Namespace = "ayemadeoro.com/servicios/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioMatadero : System.Web.Services.WebService
    {
        [WebMethod]
        public bool verifyUser(string nombre, string password)
        {
            System.GC.Collect();//limpiar memoria ram
            if (Membership.ValidateUser(nombre, password))
                return true;
            else
                return false;
        }
        [WebMethod]
        public Guid getUserID(String Name)
        {
            try
            {
                MembershipUser user = Membership.GetUser(Name);
                Guid guid = (Guid)user.ProviderUserKey;
                return guid;
            }
            catch (Exception)
            {
                return new Guid();
            }
        }
        /// <summary>
        /// Verifica si hay un proceso activo donde pueda obtener remisiones y agregar sus pesajes
        /// </summary>
        /// <returns>Boolean</returns>
        [WebMethod]
        public int getprocesoActivo()
        {
            Tbl_CompraPollos_obj compra = new Tbl_CompraPollos_obj();
            return compra.getIDProcesoActivo();
        }
        [WebMethod]
        public string getPesoJavaVacia()
        {
            try
            {
                
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.Tbl_Configuraciones.FirstOrDefault().PesoJavaVacia.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        [WebMethod]
        public String guardarPesajeVivo(String UserName, int IDViajeRemision, int IDCompra, int CantidadJavasPesadas, int PollosxJava, double PesoJavasConPollos, double PesoJavaVacia)
        {
            try
            {
                tbl_IngresoPesoVivo nuevoPeso = new tbl_IngresoPesoVivo();
                nuevoPeso.IngresadoPor = getUserID(UserName);
                nuevoPeso.ID_ViajesRemision = IDViajeRemision;
                nuevoPeso.NumPesaje = new Tbl_IngresoPesoVivo_Obj().getTotalPesajesxRemision(IDViajeRemision);
                nuevoPeso.CantidadJavasPesadas = CantidadJavasPesadas;
                nuevoPeso.PollosxJava = PollosxJava;
                nuevoPeso.PesoJavaConPollo_Libras = Convert.ToDecimal(PesoJavasConPollos);
                nuevoPeso.PesoxJavaVacia_libDefault = Convert.ToDecimal(PesoJavaVacia);
                //---------------------------------------------------
                Tbl_IngresoPesoVivo_Obj IngresoPesoVivo = new Tbl_IngresoPesoVivo_Obj();
                string resul = IngresoPesoVivo.NuevoPesoVivo(nuevoPeso, IDCompra, DateTime.Now);
                // Retunr 1, en caso de Exito, de lo contrario el Mensaje de Error Correspondiente
                return resul;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [WebMethod]
        public string finalizarPesaje_Remision(int IDViajeRemision)
        {
            return new Tbl_IngresoPesoVivo_Obj().finalizarPesajeRemision(IDViajeRemision);
        }

        [WebMethod]
        public string guardarPesajeaCuartoFrio(int IDRubroAve, decimal PesoBascula,int CajasPesadas, decimal pesoPromedioCajaVacia, decimal pesoPromedioPolin, bool seUsoPolin,String UserName) {
            ayosabdDataContext dc = new ayosabdDataContext();
            Guid UsuarioqueIngresa=getUserID(UserName);
            return dc.USP_INGRESARCORTE_A_CUARTO_FRIO(IDRubroAve, PesoBascula, CajasPesadas, pesoPromedioCajaVacia, pesoPromedioPolin, seUsoPolin, UsuarioqueIngresa).FirstOrDefault().ResultadoSP;
        }
        [WebMethod]
        public int getnothing()
        {
            return 0;
        }
        [WebMethod]
        public int getnothing1()
        {
            return 0;
        }
        [WebMethod]
        public int getnothing2()
        {
            return 0;
        }
        [WebMethod]
        public int getnothing3()
        {
            return 0;
        }
        [WebMethod]
        public int getnothing4()
        {
            return 0;
        }
        [WebMethod]
        public int getnothing5()
        {
            return 0;
        }
        [WebMethod]
        public int getnothing6()
        {
            return 0;
        }
    }
}
