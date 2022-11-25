using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos.BodegaPrincipal
{
    public class salidasHuevos
    {
        /// <summary>
        /// OBTIENE TODOS LOS INGRESOS DE LA BODEGA
        /// </summary>
        /// <returns></returns>
        public List<EgresosDeHuevos> getAllSalidas()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<EgresosDeHuevos> resultado = from _egresos in dc.Tbl_SalidaBodega
                                                        join usuarios in dc.Cat_Usuarios on _egresos.RegistradoPor equals usuarios.IDUsuarios
                                                        join _rutas in dc.Cat_Rutas on _egresos.ID_Rutas equals _rutas.ID_Rutas
                                                        join _Vendedor in dc.Cat_Vendedores on _egresos.ID_VENDEDOR equals _Vendedor.ID_VENDEDOR
                                                        join _clientes in dc.Cat_Clientes on _egresos.ID_CLIENTES equals _clientes.ID_CLIENTES
                                                        select new EgresosDeHuevos
                                                        {
                                                            IngresadoPor=usuarios.Usuario,
                                                            NombreRuta = _rutas.NOMBRE_RUTA,
                                                            NombreVendedor = _Vendedor.NOMBRE_VENDEDOR,
                                                            NombreCliente = _clientes.NOMBRE_CLIENTE,
                                                            ID_SalidaBodega = _egresos.ID_SalidaBodega,
                                                            ID_Rutas=_egresos.ID_Rutas,
                                                            ID_VENDEDOR=_egresos.ID_VENDEDOR,
                                                            ID_CLIENTES=_egresos.ID_CLIENTES,
                                                            N_ORDEN=_egresos.N_ORDEN,
                                                            FECHA=_egresos.FECHA,
                                                            TOTAL_CAJIAS=_egresos.TOTAL_CAJIAS,
                                                            TOTALHUEVOS=_egresos.TOTALHUEVOS,
                                                            RegistradoPor=_egresos.RegistradoPor,
                                                            ANULADO=_egresos.ANULADO
                                                        };
                return resultado.ToList();
            }
            catch (Exception)
            {
                return new List<EgresosDeHuevos>();
            }
        }

        /// <summary>
        /// OBTIENE TODOS LOS DETALLES POR ID DE SALIDA
        /// </summary>
        /// <param name="IDSalida"></param>
        /// <returns></returns>
        public List<DetalleEgreso> getDetalleByID(int IDSalida)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<DetalleEgreso> result = from _DetalleEgreso in dc.Tbl_DetalleSalida
                                                   join _bodega in dc.Tbl_BodegaHuevos on _DetalleEgreso.ID_BodegaHuevos equals _bodega.ID_BodegaHuevos
                                                    join _tipoHuevo in dc.Cat_TipoHuevo on _bodega.ID_TipoHUevo equals _tipoHuevo.ID_TipoHUevo
                                                    join _clasificacion in dc.Cat_ClasificacionHuevo on _bodega.IDClasificacionHuevo equals _clasificacion.IDClasificacionHuevo
                                                   where (_DetalleEgreso.ID_SalidaBodega == IDSalida)
                                                   select new DetalleEgreso
                                                    {
                                                        Producto = _tipoHuevo.TipoHuevo + " " + _clasificacion.Clasificacion,
                                                        ID_DetalleSalida=_DetalleEgreso.ID_DetalleSalida,
                                                        ID_SalidaBodega=_DetalleEgreso.ID_SalidaBodega,
                                                        ID_BodegaHuevos=_DetalleEgreso.ID_BodegaHuevos,
                                                        CANTIDAD_CAJIAS=_DetalleEgreso.CANTIDAD_CAJIAS,
                                                        CANTIDADUNIDADES=_DetalleEgreso.CANTIDADUNIDADES,
                                                        PRECIO_VENTA_CAJIA=_DetalleEgreso.PRECIO_VENTA_CAJIA,
                                                        TOTALCOBRAR=_DetalleEgreso.TOTALCOBRAR,
                                                        OBSERVACIONES=_DetalleEgreso.OBSERVACIONES
                                                    };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<DetalleEgreso>();
            }
        }

        public string anularSalida(int IDSalida)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////

                // establece el ingreso en anulado
                Tbl_SalidaBodega salidaH = (from _Salida in dc.Tbl_SalidaBodega where (_Salida.ID_SalidaBodega == IDSalida) select _Salida).FirstOrDefault();
                salidaH.ANULADO = true;
                //-------------------------------
                // quitar todos los detalles de productos de la bodega
                IQueryable<Tbl_DetalleSalida> detalles = from _Detalles in dc.Tbl_DetalleSalida where (_Detalles.ID_SalidaBodega == IDSalida) select _Detalles;
                foreach (Tbl_DetalleSalida _detalle in detalles)
                {
                    Tbl_BodegaHuevos bodega = (from _bodega in dc.Tbl_BodegaHuevos where (_bodega.ID_BodegaHuevos == _detalle.ID_BodegaHuevos) select _bodega).FirstOrDefault();
                    bodega.SALIDAS -= (int)_detalle.CANTIDADUNIDADES;
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
