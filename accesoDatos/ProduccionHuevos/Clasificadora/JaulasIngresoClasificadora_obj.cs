using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class JaulasIngresoClasificadora_obj
    {
        public List<Cat_Jaulas> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_Jaulas> result = from jauls in dc.Cat_Jaulas select jauls;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Cat_Jaulas>();
            }
        }
    }
}
