using accesoDatos.CajaChicaComedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class Tbl_Comedor_Obj
    {
        /// <summary>
        /// Guardar una Venta Como Credito, almacenandolo en la tabla de facturas
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <param name="FacturadoPor"></param>
        /// <param name="monoTotalFacturado"></param>
        /// <param name="CantidadArticulos"></param>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public string guardarVenta(int IDTrabajador, Guid FacturadoPor, decimal monoTotalFacturado, int CantidadArticulos, List<DetalleFacturado> detalle)
        {

            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  GUARDAR EN FACTURA COMEDOR ]//////////////////////////////               
                Tbl_FacturacionComedor factura = new Tbl_FacturacionComedor();
                factura.Fecha_Factura_Hora = DateTime.Now;
                factura.ID_Trabajador = IDTrabajador;
                factura.FacturadoPor = FacturadoPor;
                factura.MontoTotalFactura = monoTotalFacturado;
                factura.NumerodeProductos = CantidadArticulos;
                factura.Cancelada = false;
                dc.Tbl_FacturacionComedor.InsertOnSubmit(factura);
                dc.SubmitChanges();

                int IdFctura = factura.ID_Factura;

                foreach (Tbl_DetalleFactura _detalle in detalle)
                {
                    Tbl_Inventario _inventario = (from _inv in dc.Tbl_Inventario where _inv.ID_Inventario == _detalle.ID_Inventario select _inv).First();
                    _inventario.CantidadSaliente += _detalle.Cantidad;// Reduce el Inventario
                    Tbl_DetalleFactura nuevoDetalle = new Tbl_DetalleFactura();
                    nuevoDetalle.ID_Factura = IdFctura;
                    nuevoDetalle.ID_Inventario = _inventario.ID_Inventario;
                    nuevoDetalle.Cantidad = _detalle.Cantidad;
                    nuevoDetalle.PrecioUnidad = (decimal)_inventario.PrecioVenta;
                    nuevoDetalle.CosteUnidad = (decimal)_inventario.CostoCompra;
                    dc.Tbl_DetalleFactura.InsertOnSubmit(nuevoDetalle);
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


        /// <summary>
        /// Guardar una Venta Como Credito, almacenandolo en la tabla de facturas y retornando el ID de la Factura
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <param name="FacturadoPor"></param>
        /// <param name="monoTotalFacturado"></param>
        /// <param name="CantidadArticulos"></param>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public string guardarFactura(int IDTrabajador, Guid FacturadoPor, decimal monoTotalFacturado, int CantidadArticulos, List<DetalleFacturado> detalle)
        {

            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  GUARDAR EN FACTURA COMEDOR ]//////////////////////////////               
                Tbl_FacturacionComedor factura = new Tbl_FacturacionComedor();
                factura.Fecha_Factura_Hora = DateTime.Now;
                factura.ID_Trabajador = IDTrabajador;
                factura.FacturadoPor = FacturadoPor;
                factura.MontoTotalFactura = monoTotalFacturado;
                factura.NumerodeProductos = CantidadArticulos;
                factura.Cancelada = false;
                dc.Tbl_FacturacionComedor.InsertOnSubmit(factura);
                dc.SubmitChanges();

                int IdFctura = factura.ID_Factura;

                foreach (Tbl_DetalleFactura _detalle in detalle)
                {
                    Tbl_Inventario _inventario = (from _inv in dc.Tbl_Inventario where _inv.ID_Inventario == _detalle.ID_Inventario select _inv).First();
                    _inventario.CantidadSaliente += _detalle.Cantidad;// Reduce el Inventario
                    Tbl_DetalleFactura nuevoDetalle = new Tbl_DetalleFactura();
                    nuevoDetalle.ID_Factura = IdFctura;
                    nuevoDetalle.ID_Inventario = _inventario.ID_Inventario;
                    nuevoDetalle.Cantidad = _detalle.Cantidad;
                    nuevoDetalle.PrecioUnidad = (decimal)_inventario.PrecioVenta;
                    nuevoDetalle.CosteUnidad = (decimal)_inventario.CostoCompra;
                    dc.Tbl_DetalleFactura.InsertOnSubmit(nuevoDetalle);
                    dc.SubmitChanges();
                }
                dc.SubmitChanges();
                dc.Transaction.Commit();
                return factura.ID_Factura.ToString();
            }
            catch (Exception ex)
            {
                dc.Transaction.Rollback();
                return "-1"+ex.Message;
            }
            finally
            {
                dc.Connection.Close();
            }

        }


        /// <summary>
        /// Se guardara como factura de Contado
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <param name="FacturadoPor"></param>
        /// <param name="monoTotalFacturado"></param>
        /// <param name="CantidadArticulos"></param>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public string guardarVenta_DeContado(int IDTrabajador, Guid FacturadoPor, decimal monoTotalFacturado, int CantidadArticulos, List<DetalleFacturado> detalle)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  GUARDAR EN FACTURA COMEDOR ]//////////////////////////////               
                Tbl_FacturacionComedor factura = new Tbl_FacturacionComedor();
                factura.Fecha_Factura_Hora = DateTime.Now;
                factura.ID_Trabajador = IDTrabajador;
                factura.FacturadoPor = FacturadoPor;
                factura.MontoTotalFactura = monoTotalFacturado;
                factura.NumerodeProductos = CantidadArticulos;
                factura.Cancelada = true;
                factura.Fecha_Cancelacion = DateTime.Now;
                factura.ConceptoCancelacion = "FACTURA PAGADA DE CONTADO";
                factura.CanceladaPor = FacturadoPor;
                factura.isVentaAlContado = true;
                dc.Tbl_FacturacionComedor.InsertOnSubmit(factura);
                dc.SubmitChanges();

                int IdFctura = factura.ID_Factura;
                ///////////////[  GUARDAR EN DETALLE - FACTURA COMEDOR ]//////////////////////////////               
                foreach (Tbl_DetalleFactura _detalle in detalle)
                {
                    Tbl_Inventario _inventario = (from _inv in dc.Tbl_Inventario where _inv.ID_Inventario == _detalle.ID_Inventario select _inv).First();
                    _inventario.CantidadSaliente += _detalle.Cantidad;// Reduce el Inventario se suma para aumentar la cantidad sliente
                    Tbl_DetalleFactura nuevoDetalle = new Tbl_DetalleFactura();
                    nuevoDetalle.ID_Factura = IdFctura;
                    nuevoDetalle.ID_Inventario = _inventario.ID_Inventario;
                    nuevoDetalle.Cantidad = _detalle.Cantidad;
                    nuevoDetalle.PrecioUnidad = (decimal)_inventario.PrecioVenta;
                    nuevoDetalle.CosteUnidad = (decimal)_inventario.CostoCompra;
                    dc.Tbl_DetalleFactura.InsertOnSubmit(nuevoDetalle);
                    dc.SubmitChanges();
                }
                dc.SubmitChanges();

                ///////////////[  REALIZAMOS UN NUEVO REGISTRO DE INGRESO A LA CAJA DEL DIA ]//////////////////////////////               
                
                tbl_trabajadores Trabajador=(from trabajador in dc.tbl_trabajadores where(trabajador.ID_Trabajador==IDTrabajador) select trabajador).FirstOrDefault();
                string NombreTrabajador=Trabajador.Nombre_1;

                ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == DateTime.Now.Date) select _chc).FirstOrDefault();
                chc.TotalIngresos += monoTotalFacturado;
                dc.SubmitChanges();

                ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE INGRESOS] /////////////
                Tbl_IngresosCajaChica_Comedor nuevo_ingreso = new Tbl_IngresosCajaChica_Comedor();
                nuevo_ingreso.ID_CajaChica = chc.ID_CajaChica;
                nuevo_ingreso.RecibeDe = NombreTrabajador;
                nuevo_ingreso.EnConceptoDe = "COMPRA DE PRODUCTO COMEDOR AL CONTADO #FACTURA=: " + factura.ID_Factura.ToString().PadLeft(5);
                nuevo_ingreso.Cantidad = monoTotalFacturado;
                nuevo_ingreso.FechaIngreso_sy = DateTime.Now;
                nuevo_ingreso.RegistradoPor =FacturadoPor;
                nuevo_ingreso.FechaPorUsuario = DateTime.Now;
                dc.Tbl_IngresosCajaChica_Comedor.InsertOnSubmit(nuevo_ingreso);
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
        /// Se guardara como factura de Contado y retornara la factura generada
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <param name="FacturadoPor"></param>
        /// <param name="monoTotalFacturado"></param>
        /// <param name="CantidadArticulos"></param>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public string guardarVenta_DeContado_factura(int IDTrabajador, Guid FacturadoPor, decimal monoTotalFacturado, int CantidadArticulos, List<DetalleFacturado> detalle)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  GUARDAR EN FACTURA COMEDOR ]//////////////////////////////               
                Tbl_FacturacionComedor factura = new Tbl_FacturacionComedor();
                factura.Fecha_Factura_Hora = DateTime.Now;
                factura.ID_Trabajador = IDTrabajador;
                factura.FacturadoPor = FacturadoPor;
                factura.MontoTotalFactura = monoTotalFacturado;
                factura.NumerodeProductos = CantidadArticulos;
                factura.Cancelada = true;
                factura.Fecha_Cancelacion = DateTime.Now;
                factura.ConceptoCancelacion = "FACTURA PAGADA DE CONTADO";
                factura.CanceladaPor = FacturadoPor;
                factura.isVentaAlContado = true;
                dc.Tbl_FacturacionComedor.InsertOnSubmit(factura);
                dc.SubmitChanges();

                int IdFctura = factura.ID_Factura;
                ///////////////[  GUARDAR EN DETALLE - FACTURA COMEDOR ]//////////////////////////////               
                foreach (Tbl_DetalleFactura _detalle in detalle)
                {
                    Tbl_Inventario _inventario = (from _inv in dc.Tbl_Inventario where _inv.ID_Inventario == _detalle.ID_Inventario select _inv).First();
                    _inventario.CantidadSaliente += _detalle.Cantidad;// Reduce el Inventario  Se suma para obener negativo
                    Tbl_DetalleFactura nuevoDetalle = new Tbl_DetalleFactura();
                    nuevoDetalle.ID_Factura = IdFctura;
                    nuevoDetalle.ID_Inventario = _inventario.ID_Inventario;
                    nuevoDetalle.Cantidad = _detalle.Cantidad;
                    nuevoDetalle.PrecioUnidad = (decimal)_inventario.PrecioVenta;
                    nuevoDetalle.CosteUnidad = (decimal)_inventario.CostoCompra;
                    dc.Tbl_DetalleFactura.InsertOnSubmit(nuevoDetalle);
                    dc.SubmitChanges();
                }
                dc.SubmitChanges();

                ///////////////[  REALIZAMOS UN NUEVO REGISTRO DE INGRESO A LA CAJA DEL DIA ]//////////////////////////////               

                tbl_trabajadores Trabajador = (from trabajador in dc.tbl_trabajadores where (trabajador.ID_Trabajador == IDTrabajador) select trabajador).FirstOrDefault();
                string NombreTrabajador = Trabajador.Nombre_1;

                ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == DateTime.Now.Date) select _chc).FirstOrDefault();
                chc.TotalIngresos += monoTotalFacturado;
                dc.SubmitChanges();

                ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE INGRESOS] /////////////
                Tbl_IngresosCajaChica_Comedor nuevo_ingreso = new Tbl_IngresosCajaChica_Comedor();
                nuevo_ingreso.ID_CajaChica = chc.ID_CajaChica;
                nuevo_ingreso.RecibeDe = NombreTrabajador;
                nuevo_ingreso.EnConceptoDe = "COMPRA DE PRODUCTO COMEDOR AL CONTADO #FACTURA=: " + factura.ID_Factura.ToString().PadLeft(5);
                nuevo_ingreso.Cantidad = monoTotalFacturado;
                nuevo_ingreso.FechaIngreso_sy = DateTime.Now;
                nuevo_ingreso.RegistradoPor = FacturadoPor;
                nuevo_ingreso.FechaPorUsuario = DateTime.Now;
                dc.Tbl_IngresosCajaChica_Comedor.InsertOnSubmit(nuevo_ingreso);
                dc.SubmitChanges();

                dc.Transaction.Commit();
                return factura.ID_Factura.ToString();
            }
            catch (Exception ex)
            {
                dc.Transaction.Rollback();
                return "-1"+ex.Message; // <-- devuelve el -1 para indicar que ocurrio un error
            }
            finally
            {
                dc.Connection.Close();
            }

        }
    }
}
