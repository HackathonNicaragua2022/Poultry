using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class SalidaHuevoClasificado_obj
    {
        public string nuevaClasificacion(Tbl_SalidaHuevoClasificado salidaHuevoClasificado, List<DetalleSalidaHuevoClasificado_obj.DetalleSaidaHuevoClasificado> detalles)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////

                //Ingresar en la Tabla ingreso de inventarios HSC
                dc.Tbl_SalidaHuevoClasificado.InsertOnSubmit(salidaHuevoClasificado);
                dc.SubmitChanges();
                int ID_SalidaHuevoClasificado = salidaHuevoClasificado.ID_SalidaHuevoClasificado;
                //---------------------------------------------------------
                //Recorer todos los detalles para ingresarlos al inventario
                foreach (var _detalle in detalles)
                {
                    DetalleSalidaHuevoClasificado_obj nuevoDetalle = new DetalleSalidaHuevoClasificado_obj();
                    string resultDetalle = nuevoDetalle.nuevo(dc, ID_SalidaHuevoClasificado, _detalle.IDClasificacionHuevo, _detalle.CANTIDADCAJILLAS, _detalle.ID_HuevoSinClasificar);
                    if (resultDetalle.Equals("1"))
                    {
                        // GUARDAR EN LA TABLA DE INVENTARIO DE HUEVO SIN CLASIFICAR
                        // SUMAR LAS CANTIDADES DE ENTRADA
                        Tbl_Inventario_HuevoSinClasificar inventarioHSC = (from _inventariohsc in dc.Tbl_Inventario_HuevoSinClasificar where (_inventariohsc.ID_HuevoSinClasificar == _detalle.ID_HuevoSinClasificar) select _inventariohsc).FirstOrDefault();
                        if (inventarioHSC != null)
                        {
                            inventarioHSC.TotalSalidasEmpacadas += _detalle.CANTIDADCAJILLAS;
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
