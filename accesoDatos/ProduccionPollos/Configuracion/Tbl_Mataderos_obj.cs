using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.Configuracion
{
    public class Tbl_Mataderos_obj
    {
        public List<tbl_Matadero> getAllMatadero()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<tbl_Matadero> resutl = from matadero in dc.tbl_Matadero select matadero;
                return resutl.ToList();
            }
            catch (Exception)
            {
                return new List<tbl_Matadero>();
            }
        }
        public string nuevoMatadero(string NombreMatadero, String DireccionMatadero, String Encargado, String TelefonoMatadero, int CapacidadProduccionPollosxHora, decimal CapacidadProduccionLibrasxHora)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                tbl_Matadero nuevoMatadero = new tbl_Matadero();
                nuevoMatadero.NombreMatadero = NombreMatadero;
                nuevoMatadero.DireccionMatadero = DireccionMatadero;
                nuevoMatadero.EncargadoMatadero = Encargado;
                nuevoMatadero.TelefonoMatadero = TelefonoMatadero;
                nuevoMatadero.CapacidadProduccionPollosxHora = CapacidadProduccionPollosxHora;
                nuevoMatadero.CapacidadProduccionxLibrasHora = CapacidadProduccionLibrasxHora;
                dc.tbl_Matadero.InsertOnSubmit(nuevoMatadero);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
