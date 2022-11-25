using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.CajaChicaComedor
{
    public class CajaChicaComedor_obj
    {
        public string crearCajaDelDia(Guid AperturadoPor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Tbl_CajaChica_Comedor.Any(a => a.FechaCaja.Date == DateTime.Now.Date))
                {
                    Tbl_CajaChica_Comedor ch = new Tbl_CajaChica_Comedor();
                    ch.FechaCaja = DateTime.Now;
                    ch.AperturaCaja = 0;
                    ch.TotalEgresos = 0;
                    ch.TotalGastos = 0;
                    ch.TotalEgresos = 0;
                    ch.ObservacionesApertura = String.Empty;
                    ch.ObservacionesCierre = String.Empty;
                    ch.Cerrada = false;
                    ch.Aperturada = false;
                    ch.HoraApertura = DateTime.Now;
                    ch.HoraCierre = DateTime.Now;
                    ch.AperturadoPor = AperturadoPor;
                    dc.Tbl_CajaChica_Comedor.InsertOnSubmit(ch);
                    dc.SubmitChanges();
                }
                return "1";//Creada
            }
            catch (Exception ex)
            {
                //Ocurrio un Error y no se Puede Crear la Caja
                return ex.Message;

            }
        }
        /// <summary>
        /// De vuelve un valor Bool una vez verifica que existe una caja creada con la cuenta de hoy
        ///  en caso de existir devuelve True, de lo contrario False
        /// </summary>
        /// <returns>Bool</returns>
        public bool cajaHoy()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.DeferredLoadingEnabled = false;
                return dc.Tbl_CajaChica_Comedor.Any(a => a.FechaCaja.Date == DateTime.Now.Date);
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Verifica que la caja de hoy este aperturada
        /// </summary>
        /// <returns>Bool</returns>
        public bool estaAperturadaCajaHoy()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.DeferredLoadingEnabled = false;
                if (dc.Tbl_CajaChica_Comedor.Any(a => a.FechaCaja.Date == DateTime.Now.Date))
                {
                    Tbl_CajaChica_Comedor ch = (from c in dc.Tbl_CajaChica_Comedor where (c.FechaCaja.Date == DateTime.Now.Date) select c).FirstOrDefault();
                    if (ch != null)
                    {

                        return (bool)ch.Aperturada;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Registra la Apertura de la Caja del Dia
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="observaciones"></param>
        /// <param name="RegistradoPor"></param>
        /// <returns></returns>
        public string IngresarApertura(decimal monto, string observaciones, Guid RegistradoPor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Tbl_CajaChica_Comedor.Any(a => a.FechaCaja.Date == DateTime.Now.Date))
                {
                    Tbl_CajaChica_Comedor ch = (from cj in dc.Tbl_CajaChica_Comedor where (cj.FechaCaja.Date == DateTime.Now.Date) select cj).FirstOrDefault();
                    ch.AperturaCaja = monto;
                    ch.ObservacionesApertura = observaciones;
                    ch.AperturadoPor = RegistradoPor;
                    ch.Aperturada = true;
                    dc.SubmitChanges();
                    return "1";
                }
                else
                {
                    throw new Exception("No se encontro caja en la base de datos con la fecha de hoy para realizar la actualizacion de la apertura");
                }
            }
            catch (Exception ex)
            {
                return "Error actualizando los cambios de apertura de caja ERROR: " + ex.Message;
            }
        }

        /// <summary>
        /// Obtiene la Caja segun la Fecha Indicada en el parametro 
        /// </summary>
        /// <param name="fechaCaja"></param>
        /// <returns></returns>
        public Tbl_CajaChica_Comedor getCaja(DateTime fechaCaja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_CajaChica_Comedor ch = (from chc in dc.Tbl_CajaChica_Comedor where (chc.FechaCaja.Date == fechaCaja.Date) select chc).FirstOrDefault();
                return ch;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Se usa para agregar un nuevo ingreso al sistema
        /// </summary>
        /// <param name="CantidadIngresar"></param>
        /// <param name="recibeDe"></param>
        /// <param name="enConceptoDe"></param>
        /// <param name="fechaCaja"></param>
        /// <param name="fechaxUsuario"></param>
        /// <param name="registradoPor"></param>
        /// <returns></returns>
        public string insertarNevoIngreso(decimal CantidadIngresar, String recibeDe, string enConceptoDe, DateTime fechaCaja, DateTime fechaxUsuario, Guid registradoPor)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == fechaCaja.Date) select _chc).FirstOrDefault();
                chc.TotalIngresos += CantidadIngresar;
                dc.SubmitChanges();

                ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE INGRESOS] /////////////
                Tbl_IngresosCajaChica_Comedor nuevo_ingreso = new Tbl_IngresosCajaChica_Comedor();
                nuevo_ingreso.ID_CajaChica = chc.ID_CajaChica;
                nuevo_ingreso.RecibeDe = recibeDe;
                nuevo_ingreso.EnConceptoDe = enConceptoDe;
                nuevo_ingreso.Cantidad = CantidadIngresar;
                nuevo_ingreso.FechaIngreso_sy = DateTime.Now;
                nuevo_ingreso.RegistradoPor = registradoPor;
                nuevo_ingreso.FechaPorUsuario = fechaxUsuario;
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
        /// Realiza un nuevo ingreso a Caja Chica, Usa un contexto de base de datos
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="CantidadIngresar"></param>
        /// <param name="recibeDe"></param>
        /// <param name="enConceptoDe"></param>
        /// <param name="fechaCaja"></param>
        /// <param name="fechaxUsuario"></param>
        /// <param name="registradoPor"></param>
        /// <returns></returns>
        public string insertarNuevoIngreso(ayosabdDataContext dc,decimal CantidadIngresar, String recibeDe, string enConceptoDe, DateTime fechaCaja, DateTime fechaxUsuario, Guid registradoPor)
        {            
            try
            {             
                ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == fechaCaja.Date) select _chc).FirstOrDefault();
                chc.TotalIngresos += CantidadIngresar;
                dc.SubmitChanges();

                ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE INGRESOS] /////////////
                Tbl_IngresosCajaChica_Comedor nuevo_ingreso = new Tbl_IngresosCajaChica_Comedor();
                nuevo_ingreso.ID_CajaChica = chc.ID_CajaChica;
                nuevo_ingreso.RecibeDe = recibeDe;
                nuevo_ingreso.EnConceptoDe = enConceptoDe;
                nuevo_ingreso.Cantidad = CantidadIngresar;
                nuevo_ingreso.FechaIngreso_sy = DateTime.Now;
                nuevo_ingreso.RegistradoPor = registradoPor;
                nuevo_ingreso.FechaPorUsuario = fechaxUsuario;
                dc.Tbl_IngresosCajaChica_Comedor.InsertOnSubmit(nuevo_ingreso);
                dc.SubmitChanges();               
                return "1";
            }
            catch (Exception ex)
            {             
                return ex.Message;
            }            
        }


        /// <summary>
        /// Registra un Egreso a la Caja Chica del Comedor
        /// </summary>
        /// <param name="CantidadEgresar"></param>
        /// <param name="EntregaA"></param>
        /// <param name="enConceptoDe"></param>
        /// <param name="fechaCaja"></param>
        /// <param name="fechaxUsuario"></param>
        /// <param name="registradoPor"></param>
        /// <returns></returns>
        public string insertarNuevoEgreso(decimal CantidadEgresar, String EntregaA, string enConceptoDe, DateTime fechaCaja, DateTime fechaxUsuario, Guid registradoPor)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == fechaCaja.Date) select _chc).FirstOrDefault();
                chc.TotalEgresos += CantidadEgresar;
                dc.SubmitChanges();

                ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE EGRESOS] /////////////
                Tbl_EgresosCajaChica_Comedor nuevo_Egreso = new Tbl_EgresosCajaChica_Comedor();
                nuevo_Egreso.ID_CajaChica = chc.ID_CajaChica;
                nuevo_Egreso.EntregaA = EntregaA;
                nuevo_Egreso.EnConceptoDe = enConceptoDe;
                nuevo_Egreso.Comentario = enConceptoDe;
                nuevo_Egreso.Cantidad = CantidadEgresar;
                nuevo_Egreso.FechaEgreso_sy = DateTime.Now;
                nuevo_Egreso.RegistradoPor = registradoPor;
                nuevo_Egreso.FechaPorUsuario = fechaxUsuario;
                dc.Tbl_EgresosCajaChica_Comedor.InsertOnSubmit(nuevo_Egreso);
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
        /// SE USA PARA REGISTRAR UN NUEVO GASTO
        /// </summary>
        /// <param name="CantidadEgresar_Gasto"></param>
        /// <param name="EntregaA"></param>
        /// <param name="enConceptoDe"></param>
        /// <param name="fechaCaja"></param>
        /// <param name="fechaxUsuario"></param>
        /// <param name="registradoPor"></param>
        /// <returns></returns>
        public string insertarNuevoGasto(decimal CantidadEgresar_Gasto, String EntregaA, string enConceptoDe, DateTime fechaCaja, DateTime fechaxUsuario, Guid registradoPor)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == fechaCaja.Date) select _chc).FirstOrDefault();
                chc.TotalGastos += CantidadEgresar_Gasto;
                dc.SubmitChanges();

                ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE GASTOS] /////////////
                Tbl_GastosCajaChicaComedor nuevo_Egreso = new Tbl_GastosCajaChicaComedor();
                nuevo_Egreso.ID_CajaChica = chc.ID_CajaChica;
                nuevo_Egreso.EntregaA = EntregaA;
                nuevo_Egreso.EnConceptoDe = enConceptoDe;
                nuevo_Egreso.Comentario = enConceptoDe;
                nuevo_Egreso.Cantidad = CantidadEgresar_Gasto;
                nuevo_Egreso.FechaGasto_sy = DateTime.Now;
                nuevo_Egreso.RegistradoPor = registradoPor;
                nuevo_Egreso.FechaPorUsuario = fechaxUsuario;
                dc.Tbl_GastosCajaChicaComedor.InsertOnSubmit(nuevo_Egreso);
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
        /// Retorna una Lista de Elementos relacionados en base a los registros en la tabla
        /// Tbl_IngresosEnCajaChica del Comedor
        /// constituidos por el Procedimiento Almacenado Ingresos en Caja
        /// </summary>
        /// <param name="FechaCaja"></param>
        /// <returns></returns>
        public List<usp_IngresosEnCajaResult> getAllIngresosEnCaja(DateTime FechaCaja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.usp_IngresosEnCaja(FechaCaja).ToList();
            }
            catch (Exception)
            {
                return new List<usp_IngresosEnCajaResult>();
            }
        }
        /// <summary>
        /// Retorna una Lista de Elementos relacionados en base a los registros en la tabla
        /// Tbl_IngresosEnCajaChica del Comedor
        /// constituidos por el Procedimiento Almacenado Egresos en Caja
        /// </summary>
        /// <param name="FechaCaja"></param>
        /// <returns></returns>
        public List<usp_EgresosEnCajaResult> getAllEgresosEnCaja(DateTime FechaCaja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.usp_EgresosEnCaja(FechaCaja).ToList();
            }
            catch (Exception)
            {
                return new List<usp_EgresosEnCajaResult>();
            }
        }
        /// <summary>
        /// Retorna una Lista de Elementos relacionados en base a los registros en la tabla
        /// Tbl_IngresosEnCajaChica del Comedor
        /// constituidos por el Procedimiento Almacenado Gastos en Caja
        /// </summary>
        /// <param name="FechaCaja"></param>
        /// <returns></returns>
        public List<usp_GastosEnCajaResult> getAllGastosEnCaja(DateTime FechaCaja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.usp_GastosEnCaja(FechaCaja).ToList();
            }
            catch (Exception)
            {
                return new List<usp_GastosEnCajaResult>();
            }
        }

        /// <summary>
        /// Realizar un Cierre de Caja Basado en la Fecha indicada
        /// </summary>
        /// <param name="FechaCaja"></param>
        /// <param name="arqueo"></param>
        /// <param name="CerradoPor"></param>
        /// <param name="Observaciones"></param>
        /// <returns></returns>
        public string CrearCierreDeCaja(DateTime FechaCaja, List<tbl_ArqueoCajaChica_Comedor_obj> arqueo, Guid CerradoPor, string Observaciones)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  ACTUALIZAR LA CAJA CHICA DEL COMEDOR ]//////////////////////////////               
                Tbl_CajaChica_Comedor chc = (from _chc in dc.Tbl_CajaChica_Comedor where (_chc.FechaCaja.Date == FechaCaja.Date) select _chc).FirstOrDefault();
                chc.ObservacionesCierre += Observaciones;
                chc.Cerrada = true;
                chc.HoraCierre = DateTime.Now;
                chc.CerradoPor = CerradoPor;
                dc.SubmitChanges();

                ////////////////////// [ INSERTA UN NUEVO VALOR A LA TABLA DE ARQUEO DE CAJAS] /////////////
                if (arqueo != null)
                {
                    foreach (var denominaciones in arqueo)
                    {
                        Tbl_ArqueoCajaChica_Comedor arqueoCaja = new Tbl_ArqueoCajaChica_Comedor();
                        arqueoCaja.ID_CajaChica = chc.ID_CajaChica;//ID De la Caja en Modificacion
                        arqueoCaja.Denominacion = denominaciones.Denominacion;
                        arqueoCaja.Cantidad_Denominacion = denominaciones.Cantidad_Denominacion;
                        arqueoCaja.RegistradoPor = CerradoPor;
                        dc.Tbl_ArqueoCajaChica_Comedor.InsertOnSubmit(arqueoCaja);
                        dc.SubmitChanges();
                    }
                }
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
        /// Verifica si hay una caja y esta cerrada
        /// </summary>
        /// <returns></returns>
        public bool EstaCerradaHoy()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.DeferredLoadingEnabled = false;
                if (dc.Tbl_CajaChica_Comedor.Any(a => a.FechaCaja.Date == DateTime.Now.Date))
                {
                    Tbl_CajaChica_Comedor ch = (from c in dc.Tbl_CajaChica_Comedor where (c.FechaCaja.Date == DateTime.Now.Date) select c).FirstOrDefault();
                    if (ch != null)
                    {

                        return (bool)ch.Cerrada;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
