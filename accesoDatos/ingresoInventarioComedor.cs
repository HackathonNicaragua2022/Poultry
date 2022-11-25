using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class ingresoInventarioComedor
    {
        public string guardarCompraComedor(int IDProveedor, DateTime fechaIngreso, Guid Usuario, string referencia, List<Tbl_DetalleCompraFacturando> detalle)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////////////[ GUARDAR EN LA TABLA DE COMRPA DE PRODUCTOS COMEDOR]//////////////////////               
                Tbl_CompraProductoComedor cpComedor = new Tbl_CompraProductoComedor();
                cpComedor.FechaCompra = fechaIngreso;
                cpComedor.UsuarioQueIngreso = Usuario;
                cpComedor.ID_Proveedores = IDProveedor;
                cpComedor.MontoTotalFactura = (decimal)detalle.Sum(z => z.CosteTotal);
                cpComedor.NumerodeProductos = (int)detalle.Sum(n => n.Cantidad);
                cpComedor.Referencia = referencia;
                dc.Tbl_CompraProductoComedor.InsertOnSubmit(cpComedor);
                dc.SubmitChanges();
                //------------------------------------------------------------------------------------------------
                int ID_CompraProductoComedor = cpComedor.ID_CompraProductoComedor;

                foreach (Tbl_DetalleCompraFacturando _detalle in detalle)
                {
                    Tbl_Inventario _inventario = (from _inv in dc.Tbl_Inventario where _inv.ID_Inventario == _detalle.ID_Inventario select _inv).First();


                    //=====[ ACTUALIZA LOS PRECIOS EN EL INVENTARIO ]
                    _inventario.CostoCompra = (decimal)_detalle.CosteUnidad;
                    _inventario.PrecioVenta = (decimal)_detalle.PrecioUnidad;
                    //-------------------------------------------------------
                    _inventario.HabilitadoParaVenta = _detalle.HabilatoVenta;
                    //----------------------[ACTUALIZA LAS ENTRADAS]---------
                    _inventario.CantidadEntrante += _detalle.Cantidad;
                    //-------------------------------------------------------
                    dc.SubmitChanges();//  Se guardan los cambio en inventario
                    //-------------------------------------------------------
                    Tbl_DetalleFacturaCompra nuevoDetalle = new Tbl_DetalleFacturaCompra();
                    nuevoDetalle.ID_CompraProductoComedor = ID_CompraProductoComedor;
                    nuevoDetalle.ID_Inventario = _detalle.ID_Inventario;
                    nuevoDetalle.Cantidad = _detalle.Cantidad;
                    nuevoDetalle.PrecioUnidad = (decimal)_detalle.CosteUnidad;
                    nuevoDetalle.CosteUnidad = (decimal)_detalle.PrecioUnidad;
                    nuevoDetalle.Observaciones = _detalle.Observaciones;
                    dc.Tbl_DetalleFacturaCompra.InsertOnSubmit(nuevoDetalle);
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
