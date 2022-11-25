using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos.BodegaPrincipal
{
    public class ingresoHuevos
    {
        /// <summary>
        /// OBTIENE TODOS LOS INGRESOS DE LA BODEGA
        /// </summary>
        /// <returns></returns>
        public List<InresosDeHuevos> getAllIngresos()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<InresosDeHuevos> resultado = from _ingresos in dc.Tbl_IngresoHuevos
                                                        join usuarios in dc.Cat_Usuarios on _ingresos.IDUsuarios equals usuarios.IDUsuarios
                                                        join _tipoIngreso in dc.Cat_TipoIngreso on _ingresos.ID_TIPOINGRESO equals _tipoIngreso.ID_TIPOINGRESO
                                                        select new InresosDeHuevos
                                                        {
                                                            ID_IngresoHuevos = _ingresos.ID_IngresoHuevos,
                                                            IngresadoPor = usuarios.Usuario,
                                                            IDUsuarios = _ingresos.IDUsuarios,
                                                            RESPONSABLE_BODEGA = _ingresos.RESPONSABLE_BODEGA,
                                                            TOTAl_CAJIAS = _ingresos.TOTAl_CAJIAS,
                                                            TOTAL_HUEVOS = _ingresos.TOTAL_HUEVOS,
                                                            ANULADO = _ingresos.ANULADO,
                                                            OBSERVACIONES = _ingresos.OBSERVACIONES,
                                                            FECHA_INGRESO = _ingresos.FECHA_INGRESO,
                                                            N_ORDEN_INGRESO = _ingresos.N_ORDEN_INGRESO,
                                                            ID_TIPOINGRESO = _ingresos.ID_TIPOINGRESO,
                                                            FECHA_INGRESOSISTEMA = _ingresos.FECHA_INGRESOSISTEMA,
                                                            NombreTipoIngreso = _tipoIngreso.TIPO_INGRESO
                                                        };
                return resultado.ToList();
            }
            catch (Exception)
            {
                return new List<InresosDeHuevos>();
            }
        }

        /// <summary>
        /// OBTIENE TODOS LOS DETALLES POR ID DE INGRESO
        /// </summary>
        /// <param name="IDIngreso"></param>
        /// <returns></returns>
        public List<DetalleIngreso> getDetalleByID(int IDIngreso)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<DetalleIngreso> result = from _DetalleIngreso in dc.Tbl_DetalleCajias
                                                    join _bodega in dc.Tbl_BodegaHuevos on _DetalleIngreso.ID_BodegaHuevos equals _bodega.ID_BodegaHuevos
                                                    join _tipoHuevo in dc.Cat_TipoHuevo on _bodega.ID_TipoHUevo equals _tipoHuevo.ID_TipoHUevo
                                                    join _clasificacion in dc.Cat_ClasificacionHuevo on _bodega.IDClasificacionHuevo equals _clasificacion.IDClasificacionHuevo
                                                    where (_DetalleIngreso.ID_IngresoHuevos == IDIngreso)
                                                    select new DetalleIngreso
                                                    {
                                                        Producto = _tipoHuevo.TipoHuevo + " " + _clasificacion.Clasificacion,
                                                        CANTIDAD_CAJIAS = _DetalleIngreso.CANTIDAD_CAJIAS,
                                                        MULTIPLICADOR = _DetalleIngreso.MULTIPLICADOR,
                                                        TOTAL_HUEVOS = _DetalleIngreso.TOTAL_HUEVOS,
                                                        PRECIO_VENTA_MAYORISTA = _DetalleIngreso.PRECIO_VENTA_MAYORISTA,
                                                        PRECIO_VENTA_ALDETALLE = _DetalleIngreso.PRECIO_VENTA_ALDETALLE
                                                    };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<DetalleIngreso>();
            }
        }

        public string anularIngreso(int IDIngreso)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////

                // establece el ingreso en anulado
                Tbl_IngresoHuevos ingreso = (from _ingreso in dc.Tbl_IngresoHuevos where (_ingreso.ID_IngresoHuevos == IDIngreso) select _ingreso).FirstOrDefault();
                ingreso.ANULADO = true;
                //-------------------------------
                // quitar todos los detalles de productos de la bodega
                IQueryable<Tbl_DetalleCajias> detalles = from _Detalles in dc.Tbl_DetalleCajias where (_Detalles.ID_IngresoHuevos == IDIngreso) select _Detalles;
                foreach (Tbl_DetalleCajias _detalle in detalles)
                {
                    Tbl_BodegaHuevos bodega = (from _bodega in dc.Tbl_BodegaHuevos where (_bodega.ID_BodegaHuevos == _detalle.ID_BodegaHuevos) select _bodega).FirstOrDefault();
                    bodega.ENTRADAS -=(int) _detalle.TOTAL_HUEVOS;
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
