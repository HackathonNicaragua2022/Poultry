using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class Cat_MediasInsumosGranja_obj
    {
        public List<Cat_UnidadMedidaInsumoGranja> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_UnidadMedidaInsumoGranja> result = from unidadMedidaInsumo in dc.Cat_UnidadMedidaInsumoGranja select unidadMedidaInsumo;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Cat_UnidadMedidaInsumoGranja>();
            }
        }
        public bool delete(int IDUnidadMedida)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_UnidadMedidaInsumoGranja UnidadMedidaInsumo = (from _unidadMedidaInsumo in dc.Cat_UnidadMedidaInsumoGranja where (_unidadMedidaInsumo.ID_unidadMedida == IDUnidadMedida) select _unidadMedidaInsumo).FirstOrDefault();
                dc.Cat_UnidadMedidaInsumoGranja.DeleteOnSubmit(UnidadMedidaInsumo);
                dc.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool nuevaUnidadMedida(string nombreUniadMedida, string simbolo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_UnidadMedidaInsumoGranja unidadMedida = new Cat_UnidadMedidaInsumoGranja();
                if (!dc.Cat_UnidadMedidaInsumoGranja.Any(a => a.UnidadMedidaInsumo.Equals(nombreUniadMedida)))
                {
                    unidadMedida.UnidadMedidaInsumo = nombreUniadMedida;
                    unidadMedida.Simbolo = simbolo;
                    dc.Cat_UnidadMedidaInsumoGranja.InsertOnSubmit(unidadMedida);
                    dc.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
