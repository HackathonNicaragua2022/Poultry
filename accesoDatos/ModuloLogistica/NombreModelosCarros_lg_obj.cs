using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class NombreModelosCarros_lg_obj
    {
        public List<Cat_MarcaModeloCarros> getAllMarcaModeloCarro()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_MarcaModeloCarros> result = from modeloCarros in dc.Cat_MarcaModeloCarros where (modeloCarros.Activo == true) orderby modeloCarros.NombreVehiculo select modeloCarros;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Cat_MarcaModeloCarros>();
            }
        }
        public bool checkSiExiste(string nombre)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Cat_MarcaModeloCarros.Any(a => a.NombreVehiculo.Equals(nombre)))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool guardarNuevoNombreModelo(string nombre)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_MarcaModeloCarros nuevo = new Cat_MarcaModeloCarros();
                nuevo.NombreVehiculo = nombre;
                nuevo.Activo = true;
                dc.Cat_MarcaModeloCarros.InsertOnSubmit(nuevo);
                dc.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
