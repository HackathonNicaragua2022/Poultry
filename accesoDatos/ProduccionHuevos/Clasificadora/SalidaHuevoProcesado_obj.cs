using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class SalidaHuevoProcesado_obj
    {
        public class DetalleSalidaHuevoProcesado : Tbl_DetalleHuevoProcesado
        {
            private string fechaProduccion;

            public string FechaProduccion
            {
                get { return fechaProduccion; }
                set { fechaProduccion = value; }
            }
            private string tipoHuevo;
            public string TipoHuevo
            {
                get { return tipoHuevo; }
                set { tipoHuevo = value; }
            }
        }
        public string NuevoProductoProcesado(Tbl_HuevoProcesado huevoProcesado, List<DetalleSalidaHuevoProcesado> detalles)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////

                //Ingresar en la Tabla HuevoProcesado
                dc.Tbl_HuevoProcesado.InsertOnSubmit(huevoProcesado);
                dc.SubmitChanges();
                int ID_HuevoProcesado = huevoProcesado.ID_HuevoProcesado;
                //---------------------------------------------------------
                //Recorer todos los detalles para ingresarlos al inventario
                foreach (var _detalle in detalles)
                {
                    //Guardar en la tabla de Detalle de Huevo Procesado
                    DetalleHuevoProcesado_obj nuevoDetalle = new DetalleHuevoProcesado_obj();
                    string resultDetalle = nuevoDetalle.nuevoDetalle(dc, ID_HuevoProcesado, _detalle.ID_HuevoSinClasificar, _detalle.CantidadCajillasUsadas, _detalle.CantidadProducida, _detalle.Observaciones);
                    if (resultDetalle.Equals("1"))
                    {
                        // GUARDAR EN LA TABLA DE INVENTARIO DE HUEVO SIN CLASIFICAR
                        // SUMAR LAS CANTIDADES DE ENTRADA
                        Tbl_Inventario_HuevoSinClasificar inventarioHSC = (from _inventariohsc in dc.Tbl_Inventario_HuevoSinClasificar where (_inventariohsc.ID_HuevoSinClasificar == _detalle.ID_HuevoSinClasificar) select _inventariohsc).FirstOrDefault();
                        if (inventarioHSC != null)
                        {
                            inventarioHSC.TotalSalidasEmpacadas += _detalle.CantidadCajillasUsadas;
                            dc.SubmitChanges();
                        }
                        else
                        {
                            throw new Exception("No se encontro Inventario de Huevo sin Clasificar en la Tabla Tbl_Inventario_HuevoSinclasificar con ID " + _detalle.ID_HuevoSinClasificar.ToString());
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
    }
}
