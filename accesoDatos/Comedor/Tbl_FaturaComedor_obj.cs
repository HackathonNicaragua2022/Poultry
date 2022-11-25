using accesoDatos.CajaChicaComedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Comedor
{
    public class Tbl_FaturaComedor_obj
    {
        public List<USP_FechasCancelacionFacturasResult> ObtenerFechasFacturasCanceladas(bool incluirDevoluciones)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_FechasCancelacionFacturas(incluirDevoluciones).ToList();
            }
            catch (Exception)
            {
                return new List<USP_FechasCancelacionFacturasResult>();
            }
        }
        /// <summary>
        /// obtiene todas las facturas pendientes por cancelar
        /// </summary>
        /// <returns></returns>
        public List<USP_TodasLasFacturasPendientesResult> ObtenerTodasLasFacturasPendientes()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_TodasLasFacturasPendientes().ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene todas las facturas pendientes por cancelar en base a el rango de fechas suministrado
        /// las facturas cargadas seran exclusivas de Credito
        /// </summary>
        /// <param name="fecha1"></param>
        /// <param name="fecha2"></param>
        /// <returns></returns>
        public List<USP_TrabajadoresConFacturasPendientesxRangoFechaResult> ObtenerFacturasPendientesPorRangoFecha(DateTime fecha1, DateTime fecha2)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_TrabajadoresConFacturasPendientesxRangoFecha(fecha1, fecha2).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<USP_TrabajadoresConFacturasCred_Can_XFechaResult> ObtenerTrabajadoresConFacturasCanceladasxFecha(DateTime fecha)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_TrabajadoresConFacturasCred_Can_XFecha(fecha).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Cancela todas las facturas entre el rango seleccionado
        /// </summary>
        /// <param name="fecha1"></param>
        /// <param name="fecha2"></param>
        /// <returns>Long</returns>
        public long CancelarTodaslasFacturasxRango(DateTime fecha1, DateTime fecha2, Guid IdUsuario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_CancelarTodasLasFacturasPorRango(fecha1, fecha2, IdUsuario).FirstOrDefault().TotalCancelado;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public long CancelarTodaslasFacturasCredito(Guid IdUsuario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_CancelarTodasDeCredito(IdUsuario).FirstOrDefault().TotalCancelado;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Obtiene todas las facturas de contado del comedor por id de usuario y rango de fecha
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns>Listado de Facturas</returns>
        public List<facturasComedor> getFacturasContado_Comedor_xidTrabajador(int IDTrabajador, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<facturasComedor> result = from _facturasComedor in dc.Tbl_FacturacionComedor
                                                     join _usuarios in dc.aspnet_Users on _facturasComedor.FacturadoPor equals _usuarios.UserId
                                                     where (_facturasComedor.ID_Trabajador == IDTrabajador && _facturasComedor.Cancelada == true && _facturasComedor.isVentaAlContado == true && (_facturasComedor.Fecha_Factura_Hora.Date >= fechaInicial.Date && _facturasComedor.Fecha_Factura_Hora.Date <= fechaFinal.Date))
                                                     select new facturasComedor
                                                     {
                                                         ID_Factura = _facturasComedor.ID_Factura,
                                                         Fecha_Factura_Hora = _facturasComedor.Fecha_Factura_Hora,
                                                         FacturadoP = _usuarios.UserName,
                                                         MontoTotalFactura = _facturasComedor.MontoTotalFactura,
                                                         NumerodeProductos = _facturasComedor.NumerodeProductos
                                                     };

                return result.ToList();
            }
            catch (Exception)
            {
                return new List<facturasComedor>();
            }
        }
        /// <summary>
        /// Obtiene una factura por su iD
        /// </summary>
        /// <param name="IDFactura"></param>
        /// <returns></returns>
        public List<facturasComedor> getFacturaxID(int IDFactura)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<facturasComedor> result = from _facturasComedor in dc.Tbl_FacturacionComedor
                                                     join _usuarios in dc.aspnet_Users on _facturasComedor.FacturadoPor equals _usuarios.UserId
                                                     where (_facturasComedor.ID_Factura == IDFactura)
                                                     select new facturasComedor
                                                     {
                                                         ID_Factura = _facturasComedor.ID_Factura,
                                                         Fecha_Factura_Hora = _facturasComedor.Fecha_Factura_Hora,
                                                         FacturadoP = _usuarios.UserName,
                                                         MontoTotalFactura = _facturasComedor.MontoTotalFactura,
                                                         NumerodeProductos = _facturasComedor.NumerodeProductos
                                                     };

                return result.ToList();
            }
            catch (Exception)
            {
                return new List<facturasComedor>();
            }
        }

        /// <summary>
        /// Obtiene todas las facturas de credito del cliente por rando de fecha
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns>Listado de facturas</returns>
        public List<facturasComedor> getFacturasComedorxidTrabajador(int IDTrabajador, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<facturasComedor> result = from _facturasComedor in dc.Tbl_FacturacionComedor
                                                     join _usuarios in dc.aspnet_Users on _facturasComedor.FacturadoPor equals _usuarios.UserId
                                                     where (_facturasComedor.ID_Trabajador == IDTrabajador && _facturasComedor.Cancelada == false && _facturasComedor.isVentaAlContado == false && (_facturasComedor.Fecha_Factura_Hora.Date >= fechaInicial.Date && _facturasComedor.Fecha_Factura_Hora.Date <= fechaFinal.Date))
                                                     select new facturasComedor
                                                     {
                                                         ID_Factura = _facturasComedor.ID_Factura,
                                                         Fecha_Factura_Hora = _facturasComedor.Fecha_Factura_Hora,
                                                         FacturadoP = _usuarios.UserName,
                                                         MontoTotalFactura = _facturasComedor.MontoTotalFactura,
                                                         NumerodeProductos = _facturasComedor.NumerodeProductos
                                                     };

                return result.ToList();
            }
            catch (Exception)
            {
                return new List<facturasComedor>();
            }
        }

        public List<facturasComedorConDetalle> getFacturasCredito_ComedorxidTrabajador(int IDTrabajador, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<facturasComedorConDetalle> result = from _facturasComedor in dc.Tbl_FacturacionComedor
                                                               join _usuarios in dc.aspnet_Users on _facturasComedor.FacturadoPor equals _usuarios.UserId
                                                               where (_facturasComedor.ID_Trabajador == IDTrabajador && _facturasComedor.isVentaAlContado == false && (_facturasComedor.Fecha_Factura_Hora.Date >= fechaInicial.Date && _facturasComedor.Fecha_Factura_Hora.Date <= fechaFinal.Date))
                                                               select new facturasComedorConDetalle
                                                               {
                                                                   ID_Factura = _facturasComedor.ID_Factura,
                                                                   Fecha_Factura_Hora = _facturasComedor.Fecha_Factura_Hora,
                                                                   FacturadoP = _usuarios.UserName,
                                                                   MontoTotalFactura = _facturasComedor.MontoTotalFactura,
                                                                   NumerodeProductos = _facturasComedor.NumerodeProductos
                                                               };

                return result.ToList();
            }
            catch (Exception)
            {
                return new List<facturasComedorConDetalle>();
            }
        }
        public List<facturasComedorConDetalle> getFacturasCredito_ComedorxidTrabajadorFiltros(int IDTrabajador, DateTime fechaInicial, DateTime fechaFinal, bool _canceladas)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<facturasComedorConDetalle> result = from _facturasComedor in dc.Tbl_FacturacionComedor
                                                               join _usuarios in dc.aspnet_Users on _facturasComedor.FacturadoPor equals _usuarios.UserId
                                                               where (_facturasComedor.ID_Trabajador == IDTrabajador && _facturasComedor.isVentaAlContado == false && _facturasComedor.Cancelada == _canceladas && (_facturasComedor.Fecha_Factura_Hora.Date >= fechaInicial.Date && _facturasComedor.Fecha_Factura_Hora.Date <= fechaFinal.Date))
                                                               select new facturasComedorConDetalle
                                                               {
                                                                   ID_Factura = _facturasComedor.ID_Factura,
                                                                   Fecha_Factura_Hora = _facturasComedor.Fecha_Factura_Hora,
                                                                   FacturadoP = _usuarios.UserName,
                                                                   MontoTotalFactura = _facturasComedor.MontoTotalFactura,
                                                                   NumerodeProductos = _facturasComedor.NumerodeProductos,
                                                                   ConceptoCancelacion = _facturasComedor.ConceptoCancelacion
                                                               };

                return result.ToList();
            }
            catch (Exception)
            {
                return new List<facturasComedorConDetalle>();
            }
        }
        /// <summary>
        /// Obtiene todas las facturas del trabajador sin rango de tiempo especificado
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <returns></returns>
        public List<facturasComedor> getFacturasComedorxidTrabajador(int IDTrabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<facturasComedor> result = from _facturasComedor in dc.Tbl_FacturacionComedor
                                                     join _usuarios in dc.aspnet_Users on _facturasComedor.FacturadoPor equals _usuarios.UserId
                                                     where (_facturasComedor.ID_Trabajador == IDTrabajador && _facturasComedor.Cancelada == false && _facturasComedor.isVentaAlContado == false)
                                                     select new facturasComedor
                                                     {
                                                         ID_Factura = _facturasComedor.ID_Factura,
                                                         Fecha_Factura_Hora = _facturasComedor.Fecha_Factura_Hora,
                                                         FacturadoP = _usuarios.UserName,
                                                         MontoTotalFactura = _facturasComedor.MontoTotalFactura,
                                                         NumerodeProductos = _facturasComedor.NumerodeProductos
                                                     };

                return result.ToList();
            }
            catch (Exception)
            {
                return new List<facturasComedor>();
            }
        }
        /// <summary>
        /// obtiene el total de facturas de contado del cliente
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <returns></returns>
        public int getTotalFacturasContadoxTrabajador(int IDTrabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from _facturasComedor in dc.Tbl_FacturacionComedor
                        where (_facturasComedor.ID_Trabajador == IDTrabajador && _facturasComedor.Cancelada == true && _facturasComedor.isVentaAlContado == true)
                        select _facturasComedor).ToList().Count();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// Obtiene el total de facturas pendientes por pagar para el cliente
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <returns></returns>
        public int getTotalFacturasCreditoxTrabajador(int IDTrabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from _facturasComedor in dc.Tbl_FacturacionComedor
                        where (_facturasComedor.ID_Trabajador == IDTrabajador && _facturasComedor.Cancelada == false && _facturasComedor.isVentaAlContado == false)
                        select _facturasComedor).ToList().Count();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Obtiene los detalles de la factura por medio del ID
        /// </summary>
        /// <param name="IDFACT"></param>
        /// <returns></returns>
        public facturasComedor getFacturasComedorxNumFac(int IDFACT)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                facturasComedor result = (from _facturasComedor in dc.Tbl_FacturacionComedor
                                          join _usuarios in dc.aspnet_Users on _facturasComedor.FacturadoPor equals _usuarios.UserId
                                          join _trabajadores in dc.tbl_trabajadores on _facturasComedor.ID_Trabajador equals _trabajadores.ID_Trabajador
                                          where (_facturasComedor.ID_Factura == IDFACT && _facturasComedor.Cancelada == false)
                                          select new facturasComedor
                                          {
                                              ID_Factura = _facturasComedor.ID_Factura,
                                              Fecha_Factura_Hora = _facturasComedor.Fecha_Factura_Hora,
                                              FacturadoP = _usuarios.UserName,
                                              MontoTotalFactura = _facturasComedor.MontoTotalFactura,
                                              NumerodeProductos = _facturasComedor.NumerodeProductos,
                                              NombreTrabajador = _trabajadores.Nombre_1
                                          }).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<USP_DETALEFACTURAXIDFACTURAResult> detallexFactura(int idfactura)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_DETALEFACTURAXIDFACTURA(idfactura).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string anularFactura(int idFactura, Guid registradoPor)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  INICIA BLOQUE DE TRANSACCION ]//////////////////////////////         

                /*
                 * PRIMERO ACTUALIZAMOS LA FACTURA Y ESTABLECEMOS SUS VALORES A CANCELADA, AGREGAMOS UN COMENTARIO REFERENTE A LA ANULACION
                 */
                Tbl_FacturacionComedor factura = (from _factura in dc.Tbl_FacturacionComedor where (_factura.ID_Factura == idFactura) select _factura).FirstOrDefault();
                factura.Fecha_Cancelacion = DateTime.Now;
                factura.Cancelada = true;
                factura.CanceladaPor = registradoPor;
                factura.ConceptoCancelacion = "FACTURA ANULADA POR DEVOLUCION";
                dc.SubmitChanges();
                //---------------------------------------------------------------------------------------------------------------------------
                // SI LA FACTURA ES DE CREDITO SE DEVOLVERA EL DINERO EN CAJA COMO UN EGRESO SI NO SOLO SE DEVUELVE EL PRODUCTO A INVENTARIO
                if (factura.isVentaAlContado)
                {
                    CajaChicaComedor_obj cajac = new CajaChicaComedor_obj();
                    Tbl_Trabajador_obj trabajadores = new Tbl_Trabajador_obj();
                    String nombreTrabajador = trabajadores.findById(factura.ID_Trabajador).Nombre_1;
                    string cajaEgreso = cajac.insertarNuevoEgreso(factura.MontoTotalFactura, "DUEÑO DE LA FACTURA " + factura.ID_Factura.ToString() + " " + nombreTrabajador, "DEVOLUCION DE FACTURA", DateTime.Now.Date, DateTime.Now.Date, registradoPor);
                    if (!cajaEgreso.Equals("1")) throw new Exception("ERROR GUARDANDO EL EGRESO DE LA DEVOLUCION EN LA CAJA CHICA: " + cajaEgreso);
                }
                //---------------------------------------------------------------------------------------------------------------------------

                /*
                 * DEVOLVEMOS TODOS SUS ARTICULOS AL INVENTARIO
                 */
                List<Tbl_DetalleFactura> detalledelaFactura = (from detalleEnFactura in dc.Tbl_DetalleFactura where (detalleEnFactura.ID_Factura == factura.ID_Factura) select detalleEnFactura).ToList();
                foreach (Tbl_DetalleFactura _detalle in detalledelaFactura)
                {
                    Tbl_Inventario _inventario = (from _inv in dc.Tbl_Inventario where _inv.ID_Inventario == _detalle.ID_Inventario select _inv).FirstOrDefault();
                    _inventario.CantidadEntrante += _detalle.Cantidad;// Incrementamos el inventario sumando a la entrada el producto que se abia sacado                    
                    dc.SubmitChanges();
                }

                /*
                 * SE INGRESA LOS DATOS EN LA TABLA TBL_DEVOLUCIONES PARA TENER UN REGISTRO DE TODAS LAS DEVOLUCIONES
                 */
                Tbl_Devoluciones devolucion = new Tbl_Devoluciones();
                devolucion.ID_Factura = factura.ID_Factura;
                devolucion.FechaDevolucion = DateTime.Now;
                devolucion.RegistradoPor = registradoPor;
                dc.Tbl_Devoluciones.InsertOnSubmit(devolucion);
                dc.SubmitChanges();
                ///////////////[  FINALIZA BLOQUE DE TRANSACCION ]//////////////////////////////               
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
        public List<USP_FACTURASANULADASXRANGOFECHAResult> getFacturasAnuladas(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_FACTURASANULADASXRANGOFECHA(fechaInicial.Date, fechaFinal.Date).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public class facturasComedor : Tbl_FacturacionComedor
        {
            private string nombreTrabajador;

            public string NombreTrabajador
            {
                get { return nombreTrabajador; }
                set { nombreTrabajador = value; }
            }
            private string facturadoP;

            public string FacturadoP
            {
                get { return facturadoP; }
                set { facturadoP = value; }
            }
        }
        public class facturasComedorConDetalle : Tbl_FacturacionComedor
        {
            private string nombreTrabajador;

            public string NombreTrabajador
            {
                get { return nombreTrabajador; }
                set { nombreTrabajador = value; }
            }
            private string facturadoP;

            public string FacturadoP
            {
                get { return facturadoP; }
                set { facturadoP = value; }
            }
            private List<DetalleFacturaCancelarResult> detalleFactura;

            public List<DetalleFacturaCancelarResult> DetalleFactura
            {
                get { return detalleFactura; }
                set { detalleFactura = value; }
            }

        }
    }
}
