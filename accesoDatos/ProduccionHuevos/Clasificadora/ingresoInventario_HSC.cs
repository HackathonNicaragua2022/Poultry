using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class ingresoInventario_HSC
    {
        /// <summary>
        /// Ingresa un nuevo lote de huevos al sistema de base de datos
        /// </summary>
        /// <param name="ingresoInvHSC"></param>
        /// <param name="detalles"></param>
        /// <returns>Devuelve 1 si todo marcha con exito o el error en caso contrario </returns>
        public string nuevoIngreso(Tbl_IngresoInventario_HSC ingresoInvHSC, List<DetalleIngresoHSC_obj.DetalleIngresoHSC> detalles)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////

                //Ingresar en la Tabla ingreso de inventarios HSC
                dc.Tbl_IngresoInventario_HSC.InsertOnSubmit(ingresoInvHSC);
                dc.SubmitChanges();
                int ID_Ingreso_HSC = ingresoInvHSC.ID_IngresoInventario;
                //---------------------------------------------------------
                //Recorer todos los detalles para ingresarlos al inventario
                foreach (var _detalle in detalles)
                {
                    DetalleIngresoHSC_obj nuevoDetalle = new DetalleIngresoHSC_obj();
                    string resultDetalle = nuevoDetalle.nuevo(dc, _detalle.FechaProduccion, ID_Ingreso_HSC, _detalle.ID_Jaulas, _detalle.CANTIDADCAJILLA, _detalle.ID_TipoHUevo);
                    if (resultDetalle.Equals("1"))
                    {
                        // GUARDAR EN LA TABLA DE INVENTARIO DE HUEVO SIN CLASIFICAR
                        /*
                         * SE GUARDARA UN TIPO DE HUEVO Y UNA FECHA Y SOLO SE SUMARAN LAS CANTIDADES ENTRANTRES
                         */

                        if (dc.Tbl_Inventario_HuevoSinClasificar.Any(a => a.FechaProduccion.Value.Date.Equals(_detalle.FechaProduccion.Date) && a.ID_TipoHUevo == _detalle.ID_TipoHUevo))
                        {
                            Tbl_Inventario_HuevoSinClasificar inventarioHSC = (from _inventariohsc in dc.Tbl_Inventario_HuevoSinClasificar where (_inventariohsc.ID_TipoHUevo == _detalle.ID_TipoHUevo && _inventariohsc.FechaProduccion.Value.Date.Equals(_detalle.FechaProduccion.Date)) select _inventariohsc).FirstOrDefault();
                            inventarioHSC.TotalEntradasJaulas += _detalle.CANTIDADCAJILLA;
                            dc.SubmitChanges();
                        }
                        else
                        {
                            Tbl_Inventario_HuevoSinClasificar nuevoIventarioHSC = new Tbl_Inventario_HuevoSinClasificar();
                            nuevoIventarioHSC.ID_TipoHUevo = _detalle.ID_TipoHUevo;
                            nuevoIventarioHSC.TotalEntradasJaulas = _detalle.CANTIDADCAJILLA;
                            nuevoIventarioHSC.TotalSalidasEmpacadas = 0;
                            nuevoIventarioHSC.FechaProduccion = _detalle.FechaProduccion;
                            dc.Tbl_Inventario_HuevoSinClasificar.InsertOnSubmit(nuevoIventarioHSC);
                            dc.SubmitChanges();
                        }

                    }
                    else
                    {
                        throw new Exception(resultDetalle);
                    }
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


        public List<ingresoInventarioHCSObj> getAllIngresoHSCxFecha(DateTime fecha1, DateTime fecha2)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ingresoInventarioHCSObj> result = from _ingresoInventarioHSC in dc.Tbl_IngresoInventario_HSC
                                                             join usuarios in dc.Cat_Usuarios on _ingresoInventarioHSC.IngresadoPor equals usuarios.IDUsuarios
                                                             where (_ingresoInventarioHSC.FechaIngresoSistema.Date >= fecha1.Date && _ingresoInventarioHSC.FechaIngresoSistema.Date <= fecha2.Date)
                                                             select new ingresoInventarioHCSObj
                                                             {
                                                                 ID_IngresoInventario = _ingresoInventarioHSC.ID_IngresoInventario,
                                                                 TOTALCAJILLAS = _ingresoInventarioHSC.TOTALCAJILLAS,
                                                                 IngresadoPor = _ingresoInventarioHSC.IngresadoPor,
                                                                 NombreUsuario = usuarios.Usuario,
                                                                 FechaIngresoSistema = _ingresoInventarioHSC.FechaIngresoSistema,
                                                                 NumeroOrden = _ingresoInventarioHSC.NumeroOrden,
                                                                 Anulado = _ingresoInventarioHSC.Anulado,
                                                                 fechaAnulado = _ingresoInventarioHSC.fechaAnulado,
                                                                 MostrarDetalle = false
                                                             };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<ingresoInventarioHCSObj>();
            }
        }


        public class ingresoInventarioHCSObj : Tbl_IngresoInventario_HSC
        {
            private bool mostrarDetalle;

            public bool MostrarDetalle
            {
                get { return mostrarDetalle; }
                set { mostrarDetalle = value; }
            }
            private string nombreUsuario;

            public string NombreUsuario
            {
                get { return nombreUsuario; }
                set { nombreUsuario = value; }
            }
        }
    }
}
