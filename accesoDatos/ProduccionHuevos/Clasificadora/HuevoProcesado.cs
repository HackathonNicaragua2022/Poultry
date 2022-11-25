using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class HuevoProcesado
    {
        public List<USP_TOTAL_PRODUCCION_HSC_DIAResult> getAllCajillasProcesadasDia()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_TOTAL_PRODUCCION_HSC_DIA().ToList();
            }
            catch (Exception)
            {
                return new List<USP_TOTAL_PRODUCCION_HSC_DIAResult>();
            }
        }
    }
}
