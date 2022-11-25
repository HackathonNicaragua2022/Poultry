using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.PlantasProcesadoras
{
    public class Tbl_JavasEnviadas_obj
    {
        public List<Tbl_JavasEnviadas> getAllJavasEnviadas(int Id_ViajeRemision)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_JavasEnviadas> result=from javasEnviadas in dc.Tbl_JavasEnviadas where (javasEnviadas.ID_ViajesRemision == Id_ViajeRemision) select javasEnviadas;
                return result.ToList();
            }
            catch (Exception)
            {
               return new List<Tbl_JavasEnviadas>();
            }
        }
    }
}
