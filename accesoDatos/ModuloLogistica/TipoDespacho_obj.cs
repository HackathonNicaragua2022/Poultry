using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class TipoDespacho_obj
    {
        /// <summary>
        /// Ingresa un nuevo tipo de despacho al sistema de base de datos pero verifica antes que el nombre no se usa ya en el sistema
        /// </summary>
        /// <param name="nuevoTipo"></param>
        /// <returns></returns>
        public String nuevo(Cat_TipoDespacho nuevoTipo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Cat_TipoDespacho.Any(a => a.TipoDespacho.Equals(nuevoTipo.TipoDespacho)))
                {
                    dc.Cat_TipoDespacho.InsertOnSubmit(nuevoTipo);
                    dc.SubmitChanges();
                    return "1";
                }
                else
                {
                    return "Ya existe un tipo de Despacho con ese Nombre, pruebe ingresar otro e intentelo de nuevo";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Elimina un objeto de la base de datos de tipos de despachos usando el id
        /// </summary>
        /// <param name="IDTipoDespacho"></param>
        /// <returns></returns>
        public string eliminarxID(int IDTipoDespacho)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_TipoDespacho tipoDespacho = (from tipoD in dc.Cat_TipoDespacho where (tipoD.ID_TipoDespacho == IDTipoDespacho) select tipoD).FirstOrDefault();
                dc.Cat_TipoDespacho.DeleteOnSubmit(tipoDespacho);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Elimina un objeto indicado en el parametro de la base de datos de tipos de despachos
        /// </summary>
        /// <param name="eliminartipoDespacho"></param>
        /// <returns></returns>
        public string eliminar(Cat_TipoDespacho eliminartipoDespacho)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Cat_TipoDespacho.DeleteOnSubmit(eliminartipoDespacho);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Actualiza el tipo de despacho usando el ID como referencia
        /// </summary>
        /// <param name="tipoDespacho"></param>
        /// <param name="IDTipoDespacho"></param>
        /// <returns></returns>
        public string actualizar(string tipoDespacho, int IDTipoDespacho)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_TipoDespacho old = (from tipoDepacho in dc.Cat_TipoDespacho where (tipoDepacho.ID_TipoDespacho == IDTipoDespacho) select tipoDepacho).FirstOrDefault();
                old.TipoDespacho = tipoDespacho;//Nuevo Valor
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        /// <summary>
        /// obtiene todos los tipos de despacho del sistema de base de datos y devuelve una lista
        /// </summary>
        /// <returns></returns>
        public List<Cat_TipoDespacho> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_TipoDespacho> result = from TipoDespachos in dc.Cat_TipoDespacho orderby TipoDespachos.TipoDespacho select TipoDespachos;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Cat_TipoDespacho>();
            }
        }
    }
}
