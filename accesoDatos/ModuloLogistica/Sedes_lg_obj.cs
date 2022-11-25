using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class Sedes_lg_obj
    {
        public List<Cat_Cedes> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_Cedes> resultado = from _sedes in dc.Cat_Cedes orderby _sedes.NombreCedes select _sedes;
                return resultado.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
