using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class egreso_InventarioComedor
    {
        public string guardarSalida_InventarioComedor(DateTime fechaEgreso, Guid Usuario, List<Tbl_DetalleSalida_Facturando> detalle)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////////////[ GUARDAR EN LA TABLA DE EGRESO DE PRODUCTOS COMEDOR]//////////////////////               
                Tbl_SalidaComedor salidaComedor = new Tbl_SalidaComedor();
                salidaComedor.Fecha_SalidaInventario_sy = fechaEgreso;
                salidaComedor.UsuarioQueRegistra = Usuario;
                salidaComedor.CantidadTotalSalida = (int)detalle.Sum(z => z.Cantidad);
                salidaComedor.CostosTotales = (decimal)detalle.Sum(n => n.CostoUnidad);
                salidaComedor.PrecioVentasTotales = (decimal)detalle.Sum(n => n.PrecioVUnidad);
                dc.Tbl_SalidaComedor.InsertOnSubmit(salidaComedor);
                dc.SubmitChanges();
                //------------------------------------------------------------------------------------------------
                int ID_SalidaComedor = salidaComedor.ID_SalidaComedor;

                foreach (Tbl_DetalleSalida_Facturando _detalle in detalle)
                {
                    Tbl_Inventario _inventario = (from _inv in dc.Tbl_Inventario where _inv.ID_Inventario == _detalle.ID_Inventario select _inv).First();

                    //----------------------[ACTUALIZA LAS SALIDAS]---------
                    _inventario.CantidadSaliente += (int)_detalle.Cantidad;
                    //-------------------------------------------------------
                    dc.SubmitChanges();//  Se guardan los cambio en inventario
                    //-------------------------------------------------------
                    Tbl_DetalleSalidaInv_Comedor nuevoDetalle = new Tbl_DetalleSalidaInv_Comedor();
                    nuevoDetalle.ID_SalidaComedor = ID_SalidaComedor;
                    nuevoDetalle.ID_Inventario = _detalle.ID_Inventario;
                    nuevoDetalle.Cantidad = _detalle.Cantidad;
                    nuevoDetalle.CostoUnidad = (decimal)_detalle.CostoUnidad;
                    nuevoDetalle.PrecioVUnidad = (decimal)_detalle.PrecioVUnidad;
                    nuevoDetalle.Observaciones = _detalle.Observaciones;
                    dc.Tbl_DetalleSalidaInv_Comedor.InsertOnSubmit(nuevoDetalle);
                    dc.SubmitChanges();
                }
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
