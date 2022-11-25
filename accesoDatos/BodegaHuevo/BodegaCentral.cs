using accesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlHuevos.ClasesModelos.BodegaPrincipal
{
    public class BodegaCentral
    {
        /// <summary>
        /// ingresa un producto a bodega con los valores iniciales en 0
        /// </summary>
        /// <param name="idClasificacion"></param>
        /// <param name="idTipoProducto"></param>
        /// <returns></returns>
        public string ingresarProductoABodega(int idClasificacion, int idTipoProducto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Tbl_BodegaHuevos.Any(a => a.IDClasificacionHuevo == idClasificacion && a.ID_TipoHUevo == idTipoProducto))
                {
                    Tbl_BodegaHuevos nuevo = new Tbl_BodegaHuevos();
                    nuevo.ID_TipoHUevo = idTipoProducto;
                    nuevo.IDClasificacionHuevo = idClasificacion;
                    nuevo.ENTRADAS = 0;
                    nuevo.SALIDAS = 0;
                    nuevo.PRECIO_VENTA_ALDETALLE = 0;
                    nuevo.PRECIO_VENTA_MAYORISTA = 0;
                    dc.Tbl_BodegaHuevos.InsertOnSubmit(nuevo);
                    dc.SubmitChanges();
                }
                else
                {
                    throw new Exception("El producto ya esta en el Inventario");
                }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<bodega> getAllBodega()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<bodega> result = from _bodega in dc.Tbl_BodegaHuevos
                                            join _tipoHuevo in dc.Cat_TipoHuevo on _bodega.ID_TipoHUevo equals _tipoHuevo.ID_TipoHUevo
                                            join _clasificacion in dc.Cat_ClasificacionHuevo on _bodega.IDClasificacionHuevo equals _clasificacion.IDClasificacionHuevo
                                            select new bodega
                                            {
                                                ID_BodegaHuevos = _bodega.ID_BodegaHuevos,
                                                ID_TipoHUevo = _bodega.ID_TipoHUevo,
                                                IDClasificacionHuevo = _bodega.IDClasificacionHuevo,
                                                ENTRADAS = _bodega.ENTRADAS,
                                                SALIDAS = _bodega.SALIDAS,
                                                EXISTENCIA = _bodega.EXISTENCIA,
                                                TOTAL_CAJIAS = _bodega.TOTAL_CAJIAS,
                                                PRECIO_VENTA_ALDETALLE = _bodega.PRECIO_VENTA_ALDETALLE,
                                                PRECIO_VENTA_MAYORISTA = _bodega.PRECIO_VENTA_MAYORISTA,
                                                Producto = _tipoHuevo.TipoHuevo + " " + _clasificacion.Clasificacion
                                            };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<bodega>();
            }
        }
        public List<bodega> getAllBodegaConExistencia()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<bodega> result = from _bodega in dc.Tbl_BodegaHuevos
                                            join _tipoHuevo in dc.Cat_TipoHuevo on _bodega.ID_TipoHUevo equals _tipoHuevo.ID_TipoHUevo
                                            join _clasificacion in dc.Cat_ClasificacionHuevo on _bodega.IDClasificacionHuevo equals _clasificacion.IDClasificacionHuevo
                                            where (_bodega.EXISTENCIA > 0)
                                            select new bodega
                                            {
                                                ID_BodegaHuevos = _bodega.ID_BodegaHuevos,
                                                ID_TipoHUevo = _bodega.ID_TipoHUevo,
                                                IDClasificacionHuevo = _bodega.IDClasificacionHuevo,
                                                ENTRADAS = _bodega.ENTRADAS,
                                                SALIDAS = _bodega.SALIDAS,
                                                EXISTENCIA = _bodega.EXISTENCIA,
                                                TOTAL_CAJIAS = _bodega.TOTAL_CAJIAS,
                                                PRECIO_VENTA_ALDETALLE = _bodega.PRECIO_VENTA_ALDETALLE,
                                                PRECIO_VENTA_MAYORISTA = _bodega.PRECIO_VENTA_MAYORISTA,
                                                Producto = _tipoHuevo.TipoHuevo + " " + _clasificacion.Clasificacion
                                            };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<bodega>();
            }
        }
        public bodega getBodegaxID(int IDBodega)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                bodega result = (from _bodega in dc.Tbl_BodegaHuevos
                                 join _tipoHuevo in dc.Cat_TipoHuevo on _bodega.ID_TipoHUevo equals _tipoHuevo.ID_TipoHUevo
                                 join _clasificacion in dc.Cat_ClasificacionHuevo on _bodega.IDClasificacionHuevo equals _clasificacion.IDClasificacionHuevo
                                 where (_bodega.ID_BodegaHuevos == IDBodega)
                                 select new bodega
                                 {
                                     ID_BodegaHuevos = _bodega.ID_BodegaHuevos,
                                     ID_TipoHUevo = _bodega.ID_TipoHUevo,
                                     IDClasificacionHuevo = _bodega.IDClasificacionHuevo,
                                     ENTRADAS = _bodega.ENTRADAS,
                                     SALIDAS = _bodega.SALIDAS,
                                     EXISTENCIA = _bodega.EXISTENCIA,
                                     TOTAL_CAJIAS = _bodega.TOTAL_CAJIAS,
                                     PRECIO_VENTA_ALDETALLE = _bodega.PRECIO_VENTA_ALDETALLE,
                                     PRECIO_VENTA_MAYORISTA = _bodega.PRECIO_VENTA_MAYORISTA,
                                     Producto = _tipoHuevo.TipoHuevo + " " + _clasificacion.Clasificacion
                                 }).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return new bodega();
            }
        }
        public class bodega : Tbl_BodegaHuevos
        {
            private string producto;

            public string Producto
            {
                get { return producto; }
                set { producto = value; }
            }
        }
        public string ingresoBodega(Tbl_IngresoHuevos nuevoIngreso, List<DetalleIngreso> detalleIngreso)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////

                //INGRESAR LOS DATOS DE INGRESO PARA OBTENER UN ID
                dc.Tbl_IngresoHuevos.InsertOnSubmit(nuevoIngreso);
                dc.SubmitChanges();
                int IDIngresoHuevo = nuevoIngreso.ID_IngresoHuevos;

                //INGRESAR A LA TABLA DE DETALLE LOS DETALLES DEL INGRESO
                foreach (DetalleIngreso detalle in detalleIngreso)
                {
                    Tbl_DetalleCajias _detalleIngreso = new Tbl_DetalleCajias();
                    _detalleIngreso.ID_IngresoHuevos = IDIngresoHuevo;
                    _detalleIngreso.CANTIDAD_CAJIAS = detalle.CANTIDAD_CAJIAS;
                    _detalleIngreso.MULTIPLICADOR = detalle.MULTIPLICADOR;
                    _detalleIngreso.ID_BodegaHuevos = detalle.ID_BodegaHuevos;
                    _detalleIngreso.PRECIO_VENTA_ALDETALLE = detalle.PRECIO_VENTA_ALDETALLE;
                    _detalleIngreso.PRECIO_VENTA_MAYORISTA = detalle.PRECIO_VENTA_MAYORISTA;
                    dc.Tbl_DetalleCajias.InsertOnSubmit(_detalleIngreso);
                    //si hasta aqui todo bien, entoces ingresar a bodega la cantidad entrante
                    Tbl_BodegaHuevos bodega = (from _bodega in dc.Tbl_BodegaHuevos where (_bodega.ID_BodegaHuevos == detalle.ID_BodegaHuevos) select _bodega).FirstOrDefault();
                    bodega.ENTRADAS += (int)detalle.TOTAL_HUEVOS;
                    //actualizamos los precios en caso de un cambio
                    bodega.PRECIO_VENTA_ALDETALLE = detalle.PRECIO_VENTA_ALDETALLE;
                    bodega.PRECIO_VENTA_MAYORISTA = detalle.PRECIO_VENTA_MAYORISTA;
                    dc.SubmitChanges();
                }

                ///////////////[ FINALIZA EL BLOQUE DE TRANSACCION ]////////////////////
                dc.SubmitChanges();
                dc.Transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                dc.Transaction.Rollback();
                return ex.Message;
            }
            finally
            {
                dc.Connection.Close();
            }
        }

        /// <summary>
        /// realiza un proceso de salida de la bodega general
        /// </summary>
        /// <param name="nuevaSalida"></param>
        /// <param name="detalleEgreso"></param>
        /// <returns></returns>
        public string SalidaBodega(Tbl_SalidaBodega nuevaSalida, List<DetalleEgreso> detalleEgreso)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////

                //INGRESAR LOS DATOS DE INGRESO PARA OBTENER UN ID
                dc.Tbl_SalidaBodega.InsertOnSubmit(nuevaSalida);
                dc.SubmitChanges();
                int IDSalidaHuevo = nuevaSalida.ID_SalidaBodega;

                //INGRESAR A LA TABLA DE DETALLE LOS DETALLES DEL INGRESO
                foreach (DetalleEgreso detalle in detalleEgreso)
                {
                    Tbl_DetalleSalida _detalleEgreso = new Tbl_DetalleSalida();
                    _detalleEgreso.ID_SalidaBodega = IDSalidaHuevo;
                    _detalleEgreso.ID_BodegaHuevos = detalle.ID_BodegaHuevos;
                    _detalleEgreso.CANTIDADUNIDADES = detalle.CANTIDADUNIDADES;
                    _detalleEgreso.CANTIDAD_CAJIAS = detalle.CANTIDAD_CAJIAS;
                    _detalleEgreso.PRECIO_VENTA_CAJIA = detalle.PRECIO_VENTA_CAJIA;
                    _detalleEgreso.TOTALCOBRAR = detalle.TOTALCOBRAR;
                    _detalleEgreso.OBSERVACIONES = detalle.OBSERVACIONES;
                    dc.Tbl_DetalleSalida.InsertOnSubmit(_detalleEgreso);

                    //si hasta aqui todo bien, entoces ingresar a bodega la cantidad entrante
                    Tbl_BodegaHuevos bodega = (from _bodega in dc.Tbl_BodegaHuevos where (_bodega.ID_BodegaHuevos == detalle.ID_BodegaHuevos) select _bodega).FirstOrDefault();
                    bodega.SALIDAS += (int)detalle.CANTIDADUNIDADES;
                    //actualizamos los precios en caso de un cambio                    
                    dc.SubmitChanges();
                }

                ///////////////[ FINALIZA EL BLOQUE DE TRANSACCION ]////////////////////
                dc.SubmitChanges();
                dc.Transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                dc.Transaction.Rollback();
                return ex.Message;
            }
            finally
            {
                dc.Connection.Close();
            }
        }



    }
}
