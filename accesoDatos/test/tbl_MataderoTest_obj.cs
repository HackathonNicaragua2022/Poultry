using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.test
{
    public class tbl_MataderoTest_obj
    {
        public void guardaarConteo(int totalPollo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_CompraPollos result = (from m in dc.Tbl_CompraPollos where (m.EnProceso == true) select m).FirstOrDefault();
                //Tbl_CompraPollos result = (from m in dc.Tbl_CompraPollos where (m.ID_CompraBroilers==11) select m).FirstOrDefault();
                if(result!=null){                    
                    result.TotalAvesConteoAutomatico = totalPollo;
                }
                dc.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int getavesContadas() {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //IQueryable<Tbl_CompraPollos> result = from m in dc.Tbl_CompraPollos where (m.ID_CompraBroilers == 11) select m;
                IQueryable<Tbl_CompraPollos> result = from m in dc.Tbl_CompraPollos where(m.EnProceso==true) select m;
                if (result.Count() > 0)
                {
                    Tbl_CompraPollos totalPollos = result.FirstOrDefault();
                    return (int)totalPollos.TotalAvesConteoAutomatico;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
