using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class TipoCombustible_lg_obj
    {
        public List<Cat_Combustible> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_Combustible> resul = from combustible in dc.Cat_Combustible orderby combustible.TipoCombustible select combustible;
                return resul.ToList();
            }
            catch (Exception)
            {
                return new List<Cat_Combustible>();
            }
        }
    }
}
