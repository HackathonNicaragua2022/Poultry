using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class TipoAlimentosEngorde_obj
    {
        public List<Cat_TipoAlimentoAves> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_TipoAlimentoAves> result = from alimentos in dc.Cat_TipoAlimentoAves select alimentos;
                if (result == null || result.ToList().Count <= 0)
                {
                    Cat_TipoAlimentoAves nuevoA = new Cat_TipoAlimentoAves();
                    nuevoA.NombreAlimento = "PRE-INICIO";
                    dc.Cat_TipoAlimentoAves.InsertOnSubmit(nuevoA);                    
                    Cat_TipoAlimentoAves nuevoB = new Cat_TipoAlimentoAves();
                    nuevoB.NombreAlimento = "FINALIZADOR";
                    dc.Cat_TipoAlimentoAves.InsertOnSubmit(nuevoB);
                    dc.SubmitChanges();
                    return getAll();
                }
                return result.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
