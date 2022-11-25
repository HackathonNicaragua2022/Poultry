using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos.BodegaPrincipal
{
    public class tipoIngreso
    {          
        /// <summary>
        /// CREA UN NUEVO TIPO DE INGRESO EN LA BASE DE DATOS
        /// </summary>
        /// <param name="tipoIngreso"></param>
        /// <returns></returns>
        public string nuevoTipoIngreso(string _tipoIngreso)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Cat_TipoIngreso.Any(a => a.TIPO_INGRESO.Equals(_tipoIngreso)))
                {
                    Cat_TipoIngreso nuevo = new Cat_TipoIngreso();
                    nuevo.TIPO_INGRESO = _tipoIngreso;
                    dc.Cat_TipoIngreso.InsertOnSubmit(nuevo);
                    dc.SubmitChanges();
                }
                else
                {
                    return "Ya existe un tipo de Ingreso con ese nombre";
                }
                return "1";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
         public string actualizarTipoIgreso(int IDTipoIngreso, string _tipoIngreso)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_TipoIngreso actualizarTI = (from _ingresos in dc.Cat_TipoIngreso where (_ingresos.ID_TIPOINGRESO == IDTipoIngreso) select _ingresos).FirstOrDefault();
                actualizarTI.TIPO_INGRESO = _tipoIngreso;               
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }        
        public string eliminar(int IdTipoIngreso)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_TipoIngreso eliminarTipoHuevo = (from _tipoIngreso in dc.Cat_TipoIngreso where (_tipoIngreso.ID_TIPOINGRESO == IdTipoIngreso) select _tipoIngreso).FirstOrDefault();
                dc.Cat_TipoIngreso.DeleteOnSubmit(eliminarTipoHuevo);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }      
        public List<Cat_TipoIngreso> getTiposIngresos() {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_TipoIngreso> resul = from _catTipoing in dc.Cat_TipoIngreso select _catTipoing;
                return resul.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
