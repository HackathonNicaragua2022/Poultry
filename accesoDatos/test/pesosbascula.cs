using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.test
{
    public class pesosbascula
    {
        public List<Tbl_PesosBascula_test> getAllPesos()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_PesosBascula_test> result = from _pesos in dc.Tbl_PesosBascula_test select _pesos;
                return result.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
