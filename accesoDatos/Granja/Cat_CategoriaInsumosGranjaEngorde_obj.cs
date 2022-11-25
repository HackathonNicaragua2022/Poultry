using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class Cat_CategoriaInsumosGranjaEngorde_obj
    {
        public List<Cat_CategoriaInsumosGranjaEngorde> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_CategoriaInsumosGranjaEngorde> result = from categoriasInsumos in dc.Cat_CategoriaInsumosGranjaEngorde select categoriasInsumos;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Cat_CategoriaInsumosGranjaEngorde>();
            }
        }
        public bool delete(int IDCategoria)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_CategoriaInsumosGranjaEngorde categoriaInsumo = (from _categoriaInsumo in dc.Cat_CategoriaInsumosGranjaEngorde where (_categoriaInsumo.ID_CategoriaInsumosGranjaEngorde == IDCategoria) select _categoriaInsumo).FirstOrDefault();
                dc.Cat_CategoriaInsumosGranjaEngorde.DeleteOnSubmit(categoriaInsumo);
                dc.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool nuevaCategoria(string nombreCategoria)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_CategoriaInsumosGranjaEngorde nuevaCategoria = new Cat_CategoriaInsumosGranjaEngorde();
                if (!dc.Cat_CategoriaInsumosGranjaEngorde.Any(a => a.CategoriaInsumo.Equals(nombreCategoria)))
                {
                    nuevaCategoria.CategoriaInsumo = nombreCategoria;
                    dc.Cat_CategoriaInsumosGranjaEngorde.InsertOnSubmit(nuevaCategoria);
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
