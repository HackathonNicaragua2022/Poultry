using accesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlHuevos.ClasesModelos.BodegaPrincipal
{
    public class tipoHuevo
    {
        public tipoHuevo()
        {
        }
        /// <summary>
        /// CREA UN NUEVO TIPO DE HUEVO EN LA BASE DE DATOS
        /// </summary>
        /// <param name="tipoHuevo"></param>
        /// <returns></returns>
        public string nuevoTipo(string tipoHuevo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Cat_TipoHuevo.Any(a => a.TipoHuevo.Equals(tipoHuevo)))
                {
                    Cat_TipoHuevo nuevo = new Cat_TipoHuevo();
                    nuevo.TipoHuevo = tipoHuevo;
                    nuevo.Activo = true;
                    nuevo.sync = false;
                    dc.Cat_TipoHuevo.InsertOnSubmit(nuevo);
                    dc.SubmitChanges();
                }
                else
                {
                    return "Ya existe un tipo de Huevo con ese nombre";
                }
                return "1";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        /// <summary>
        /// Actualiza un tipo de huevo segun su ID
        /// </summary>
        /// <param name="IDTipoHuevo"></param>
        /// <param name="tipoHuevo"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public string actualizarTipoHuevo(int IDTipoHuevo, string tipoHuevo, bool activo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_TipoHuevo actualizarTH = (from _tipoHuevo in dc.Cat_TipoHuevo where (_tipoHuevo.ID_TipoHUevo == IDTipoHuevo) select _tipoHuevo).FirstOrDefault();
                actualizarTH.TipoHuevo = tipoHuevo;
                actualizarTH.Activo = activo;
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        /// <summary>
        /// Elimina un tipo de Huevo segun su ID
        /// </summary>
        /// <param name="IdTipoHuevo"></param>
        /// <returns></returns>
        public string eliminarTipoHuevo(int IdTipoHuevo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_TipoHuevo eliminarTipoHuevo = (from _tipoHuevo in dc.Cat_TipoHuevo where (_tipoHuevo.ID_TipoHUevo == IdTipoHuevo) select _tipoHuevo).FirstOrDefault();
                dc.Cat_TipoHuevo.DeleteOnSubmit(eliminarTipoHuevo);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// otiene todos los tipos de huevos que hay en el sistema
        /// </summary>
        /// <returns></returns>
        public List<Cat_TipoHuevo> getTiposHuevos() {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_TipoHuevo> resul = from _catTipoHuevo in dc.Cat_TipoHuevo select _catTipoHuevo;
                return resul.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
