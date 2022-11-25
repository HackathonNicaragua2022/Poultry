using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora.Catalogos
{
    public class TipoHuevo_obj
    {
        public List<Cat_TipoHuevo> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from tipoHuevo in dc.Cat_TipoHuevo select tipoHuevo).ToList();
            }
            catch (Exception)
            {
                return new List<Cat_TipoHuevo>();
            }
        }
    }
}
