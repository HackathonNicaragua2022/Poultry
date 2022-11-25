using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class AdministracionFacturasTrabajadorComedor
    {
        public List<FacturasPendientesxTrabajadorResult> FacturasPendietesxTrabajador(int IdTrabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.FacturasPendientesxTrabajador(IdTrabajador).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtiene los datos de la Factura a travez de su ID
        /// </summary>
        /// <param name="IdFactura"></param>
        /// <returns></returns>
        public datosFactura datosXFactura(int IdFactura)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                datosFactura datos_Factura = (from facturaComedor in dc.Tbl_FacturacionComedor
                                              join trabajadores in dc.tbl_trabajadores on facturaComedor.ID_Trabajador equals trabajadores.ID_Trabajador
                                              where (facturaComedor.ID_Factura == IdFactura)
                                              select new datosFactura
                                              {
                                                  NombreEmpleado = trabajadores.Nombre_1,
                                                  MontoTotalFactura = facturaComedor.MontoTotalFactura,
                                              }).FirstOrDefault();
                return datos_Factura;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public class datosFactura : Tbl_FacturacionComedor
        {
            private string nombreEmpleado;

            public string NombreEmpleado
            {
                get { return nombreEmpleado; }
                set { nombreEmpleado = value; }
            }
        }
        public List<DetalleFacturaCancelarResult> DetalleFacturasCancelar(int IdFactura)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.DetalleFacturaCancelar(IdFactura).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string CancelarTodaslasFacturas(int IDTrabajador, DateTime fechaCancelacion, string Concepto, Guid CanceladoPor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.CancelarTodasxIDEmpleado(IDTrabajador, fechaCancelacion, Concepto, CanceladoPor);
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Cancela Todas las Facturas por Trabajador
        /// </summary>
        /// <param name="IDTrabajador"></param>
        /// <param name="fechaCancelacion"></param>
        /// <param name="Concepto"></param>
        /// <param name="CanceladoPor"></param>
        /// <returns></returns>
        public string CancelarTodaslasFacturasxTrabajador(int IDTrabajador, DateTime fechaCancelacion, string Concepto, Guid CanceladoPor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();

                try
                {
                    dc.Connection.Open();
                    dc.Transaction = dc.Connection.BeginTransaction();
                    ///////////////[  CANCELA LA FACTURA ]//////////////////////////////                                                   


                    //primero se crea la cuenta para la caja chica
                    // se obtienen todas las facturas pendientes por cancelar
                    List<Tbl_FacturacionComedor> todasLasFacturasCancelar = (from facturaComedor in dc.Tbl_FacturacionComedor where (facturaComedor.ID_Trabajador == IDTrabajador && facturaComedor.Cancelada == false) select facturaComedor).ToList();
                    //Sumamos el total por cancelar
                    decimal TotalPagar = todasLasFacturasCancelar.Sum(a => a.MontoTotalFactura);
                    ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                    Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == DateTime.Now.Date) select _chc).FirstOrDefault();
                    chc.TotalIngresos += TotalPagar;
                    dc.SubmitChanges();

                    string NumeroFacturasConcatenadas = "";
                    foreach (var item in todasLasFacturasCancelar)
                    {
                        NumeroFacturasConcatenadas += ", " + item.ID_Factura.ToString();
                    }

                    tbl_trabajadores Trabajador = (from trabajador in dc.tbl_trabajadores where (trabajador.ID_Trabajador == IDTrabajador) select trabajador).FirstOrDefault();


                    ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE INGRESOS] /////////////
                    Tbl_IngresosCajaChica_Comedor nuevo_ingreso = new Tbl_IngresosCajaChica_Comedor();
                    nuevo_ingreso.ID_CajaChica = chc.ID_CajaChica;
                    nuevo_ingreso.RecibeDe = Trabajador.Nombre_1;
                    nuevo_ingreso.EnConceptoDe = Concepto + " DE FACTURAS= " + NumeroFacturasConcatenadas;
                    nuevo_ingreso.Cantidad = TotalPagar;
                    nuevo_ingreso.FechaIngreso_sy = DateTime.Now;
                    nuevo_ingreso.RegistradoPor = CanceladoPor;
                    nuevo_ingreso.FechaPorUsuario = DateTime.Now;
                    dc.Tbl_IngresosCajaChica_Comedor.InsertOnSubmit(nuevo_ingreso);
                    dc.SubmitChanges();

                    //Por ultimo cancelamos todas las facturas una vez ya se realizo todo el proceso anterior
                    dc.CancelarTodasxIDEmpleado(IDTrabajador, fechaCancelacion, Concepto, CanceladoPor);
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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Cancela la factura por su ID y agrega una una entrada en efectivo a cajaChica
        /// </summary>
        /// <param name="IDFactura"></param>
        /// <param name="fechaCancelacion"></param>
        /// <param name="Concepto"></param>
        /// <param name="CanceladoPor"></param>
        /// <returns></returns>
        public string CancelarFacturas_ID(int IDFactura, string NombreEmpleado, decimal montoTotal, DateTime fechaCancelacion, string Concepto, Guid CanceladoPor)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();                

                ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == DateTime.Now.Date) select _chc).FirstOrDefault();
                chc.TotalIngresos += montoTotal;
                dc.SubmitChanges();

                ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE INGRESOS] /////////////
                Tbl_IngresosCajaChica_Comedor nuevo_ingreso = new Tbl_IngresosCajaChica_Comedor();
                nuevo_ingreso.ID_CajaChica = chc.ID_CajaChica;
                nuevo_ingreso.RecibeDe = NombreEmpleado;
                nuevo_ingreso.EnConceptoDe = "CANCELACION EN EFECTIVO DE #FACTURA=: " + IDFactura.ToString().PadLeft(5);
                nuevo_ingreso.Cantidad = montoTotal;
                nuevo_ingreso.FechaIngreso_sy = DateTime.Now;
                nuevo_ingreso.RegistradoPor = CanceladoPor;
                nuevo_ingreso.FechaPorUsuario = DateTime.Now;
                dc.Tbl_IngresosCajaChica_Comedor.InsertOnSubmit(nuevo_ingreso);
                dc.SubmitChanges();


                ///////////////[  CANCELA LA FACTURA  UNA VEZ SE HAN REALIZADO LAS OPERACIONES ANTERIORES ]//////////////////////////////                               
                dc.CancelarFacturasxID(IDFactura, fechaCancelacion, Concepto, CanceladoPor);
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
