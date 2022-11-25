using accesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlHuevos.ClasesModelos.BodegaPrincipal
{
    class clasificacionHuevo
    {
        public clasificacionHuevo()
        {
        }
        /// <summary>
        /// CREA UNA NUEVA CLASIFICACIO DE HUEVO TOMANDO EJ> JUMBO
        /// </summary>
        /// <param name="tipoHuevo"></param>
        /// <returns></returns>
        public string nuevaClasificacion(string Clasificacion)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Cat_ClasificacionHuevo.Any(a => a.Clasificacion.Equals(Clasificacion)))
                {
                    Cat_ClasificacionHuevo nuevo = new Cat_ClasificacionHuevo();
                    nuevo.Clasificacion = Clasificacion;
                    nuevo.sync = false;
                    nuevo.Activo = true;
                    dc.Cat_ClasificacionHuevo.InsertOnSubmit(nuevo);
                    dc.SubmitChanges();
                }
                else
                {
                    return "Ya existe una Clasificion de Huevo con ese nombre";
                }
                return "1";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        /// <summary>
        /// Actualiza una Clasificacion de huevo segun su ID
        /// </summary>
        /// <param name="IDTipoHuevo"></param>
        /// <param name="tipoHuevo"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public string actualizarTipoHuevo(int IDclasificacionHuevo, string ClasificacionHuevo, bool activo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_ClasificacionHuevo actualizarClasificacion = (from _Clasificacion in dc.Cat_ClasificacionHuevo where (_Clasificacion.IDClasificacionHuevo == IDclasificacionHuevo) select _Clasificacion).FirstOrDefault();
                actualizarClasificacion.Clasificacion = ClasificacionHuevo;
                actualizarClasificacion.Activo = activo;
                actualizarClasificacion.sync = false;
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        /// <summary>
        /// Elimina una clasificacion de Huevo segun su ID
        /// </summary>
        /// <param name="IdTipoHuevo"></param>
        /// <returns></returns>
        public string eliminarclasificacionHuevo(int IdClasificacion)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_ClasificacionHuevo eliminarClasificacion = (from _Clasificacion in dc.Cat_ClasificacionHuevo where (_Clasificacion.IDClasificacionHuevo == IdClasificacion) select _Clasificacion).FirstOrDefault();
                dc.Cat_ClasificacionHuevo.DeleteOnSubmit(eliminarClasificacion);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// otiene todos las clasificaciones de huevos que hay en el sistema
        /// </summary>
        /// <returns></returns>
        public List<Cat_ClasificacionHuevo> getClasificaciones() {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_ClasificacionHuevo> resul = from _catClasificaciones in dc.Cat_ClasificacionHuevo select _catClasificaciones;
                return resul.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
