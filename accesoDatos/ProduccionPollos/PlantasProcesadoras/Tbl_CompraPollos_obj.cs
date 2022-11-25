using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.PlantasProcesadoras
{
    public class Tbl_CompraPollos_obj
    {
        /// <summary>
        /// Guarda un Nuevo Proceso de Compra de Pollos , adicional mente crea un registro en la base de datos para el proceso de pesaje de cortes(Rubros de Aves), esto de manera paralela
        /// </summary>
        /// <param name="IDGranja"></param>
        /// <param name="NumeroReferencia"></param>
        /// <param name="Lote"></param>
        /// <param name="FechaMatanza"></param>
        /// <param name="precioCompraxLibra"></param>
        /// <param name="IDBroilers"></param>
        /// <param name="IdMatadero"></param>
        /// <param name="UsuarioACargo"></param>
        /// <param name="CreadPor"></param>
        /// <returns></returns>
        public string guardarNuevoProcesoCompraPollos(int IDGranja, string NumeroReferencia, int ID_Lote, DateTime FechaMatanza, decimal precioCompraxLibra, int IDBroilers, int IdMatadero, Guid UsuarioACargo, Guid CreadPor)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////        
                Tbl_CompraPollos NuevaCompra = new Tbl_CompraPollos();
                NuevaCompra.ID_Granjas = IDGranja;
                NuevaCompra.ReferenciaComentario = NumeroReferencia;
                NuevaCompra.ID_Lote = ID_Lote;
                NuevaCompra.FechaMatanza = FechaMatanza;
                NuevaCompra.PrecioCompraxLibra = precioCompraxLibra;
                NuevaCompra.TotalLibrasCompradasCalculoBascula = 0;
                NuevaCompra.TotalAvesRemisionCompradas = 0;
                NuevaCompra.TotalAvesConteoAutomatico = 0;
                NuevaCompra.ID_Broilers_Raza = IDBroilers;
                NuevaCompra.CreadoPor = CreadPor;
                NuevaCompra.ID_Matadero = IdMatadero;
                NuevaCompra.ACargoPesoVivo = UsuarioACargo;
                NuevaCompra.FechaCreado = DateTime.Now;
                // NuevaCompra.CostoTotalxLibra = 0;
                NuevaCompra.EnProceso = false;//se establece en false para que seha controlado desde la pagina de procesos
                NuevaCompra.enEspera = true;// Se estaclece en espera true, para crear varios procesos y poder ir dando lugar a uno por uno hasta terminar todos los procesos
                dc.Tbl_CompraPollos.InsertOnSubmit(NuevaCompra);
                dc.SubmitChanges();

                /*
                 * CREAR UN PROCESO DE INGRESO DE DATOS DE PESO FRIO REFERENTE A LOS RUGROS OBTENIDOS DE LOS PESOS VIVOS
                 */
                Tbl_PesoFrio_master PesoFrio = new Tbl_PesoFrio_master();
                PesoFrio.ID_CompraBroilers = NuevaCompra.ID_CompraBroilers;// <- El mismo ID para Trabajar en una Relacion de  1 a 1
                PesoFrio.AvesSacrificadas = 0;// Se incrementara con las remisiones de pesos vivos
                PesoFrio.LibrasVivas = 0;// ""
                PesoFrio.PesoPromedio = 0;// ""
                PesoFrio.TotalLibrasCongeladas = 0;//Se incrementara con el peso obtenido de los pesos congelados
                PesoFrio.TotalLibras_RBD = 0;//Total de libras recividas en Bode de Despacho
                PesoFrio.General_PesoTotalCorte_ECF = 0;
                PesoFrio.General_PesoTotalCorte_SCF = 0;
                PesoFrio.General_PesoTotalEnvioPlantaBodegaH = 0;
                PesoFrio.General_PesoTotalReciboPlantaBodegaH = 0;
                PesoFrio.RendimientoCanal = 0;
                //PesoFrio.RendimientoCanalCaliente=0;    //<- Columnas Calculadas
                //PesoFrio.RendimientoCompañia;   <- este se calculara durante la ejecucion
                PesoFrio.MermaCongelacionTotal = 0;
                dc.Tbl_PesoFrio_master.InsertOnSubmit(PesoFrio);// <-- Ingresa el peso frio al sistema
                //-------------------------------------
                ///////////////[ FINALIZA EL BLOQUE DE TRANSACCION ]//////////////////////////////               
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
        /// Obtiene todaslas compras en estado activo
        /// </summary>
        /// <returns>List<USP_CompraPollosRelResult></returns>
        public USP_CompraPollosRelResult ObtenerLaCompraActiva()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_CompraPollosRel().LastOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Obtener todos los procesos de compra de broilers a las galeras o vendedor externo
        /// </summary>
        /// <param name="EnProceso"></param>
        /// <returns>Devuelte List<USP_CompraPollosRelResult></returns>
        public List<USP_CompraPollos_ProcesosResult> getProcesosCompra(bool EnProceso)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_CompraPollos_Procesos(EnProceso).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<USP_CompraPollos_EnEsperaResult> getProcesosCompraEnEspera(bool EnEspera)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_CompraPollos_EnEspera(EnEspera).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public USP_CompraPollos_ProcesoxIDResult getProcesoCompraxID(int IDCompra)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_CompraPollos_ProcesoxID(IDCompra).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Obtiene una instancia que coincida con el ID Suministrado
        /// </summary>
        /// <param name="IDCompraPollos"></param>
        /// <returns></returns>
        public Tbl_CompraPollos getCompraPolloxID(int IDCompraPollos)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from cp in dc.Tbl_CompraPollos where (cp.ID_CompraBroilers == IDCompraPollos) select cp).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Finaliza la Compra pasanado el valor en proceso a Falso
        /// </summary>
        /// <param name="IDCompraBroilers"></param>
        /// <returns></returns>
        public string finalizarCompra(int IDCompraBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_CompraPollos compraBroilersEnProceso = (from compra in dc.Tbl_CompraPollos where (compra.ID_CompraBroilers == IDCompraBroilers) select compra).FirstOrDefault();
                compraBroilersEnProceso.EnProceso = false;
                compraBroilersEnProceso.enEspera = false;
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public int getTotalPesajesxCompra(int IDCompra)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (int)dc.USP_TotalPesajesxCompra(IDCompra).ToList().First().TotalPesajes;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string eliminarProceso(int IDCompra)
         {           
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA LA TRANSACCION ]//////////////////////////////        
                Tbl_CompraPollos compra = (from _compra in dc.Tbl_CompraPollos where (_compra.ID_CompraBroilers == IDCompra) select _compra).FirstOrDefault();

                Tbl_PesoFrio_master pesoFrio = (from _pesoFrio in dc.Tbl_PesoFrio_master where (_pesoFrio.ID_CompraBroilers == IDCompra) select _pesoFrio).FirstOrDefault();
                dc.Tbl_PesoFrio_master.DeleteOnSubmit(pesoFrio);
                dc.SubmitChanges();
                dc.Tbl_CompraPollos.DeleteOnSubmit(compra);                
                ///////////////[ FINALIZA EL BLOQUE DE TRANSACCION ]//////////////////////////////               
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
        public string ActivarProceso(int IDCompra)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[ COMIENZA TRANSACCION ]//////////////////////////////        
                var res = dc.USP_ActualizarEstadoProcesoInactivos();

                Tbl_CompraPollos compra = (from _compra in dc.Tbl_CompraPollos where (_compra.ID_CompraBroilers == IDCompra) select _compra).FirstOrDefault();
                compra.EnProceso = true;
                compra.enEspera = true;
                // dc.SubmitChanges();
                ///////////////[ COMIENZA FINALIZA ]//////////////////////////////               
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
        public int getIDProcesoActivo()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_CompraPollos result = (from m in dc.Tbl_CompraPollos where (m.EnProceso == true) select m).FirstOrDefault();
                //Tbl_CompraPollos result = (from m in dc.Tbl_CompraPollos where (m.ID_CompraBroilers==11) select m).FirstOrDefault();
                if (result != null)
                {
                    return result.ID_CompraBroilers;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// obtiene el total de procesos en espera o desarrollo
        /// </summary>
        /// <returns></returns>
        public int getTotalProcesosPendientes()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                int total = dc.Tbl_CompraPollos.Count(a => a.enEspera == true || a.EnProceso == true);
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// obtiene el total de los procesos terminados
        /// </summary>
        /// <returns></returns>
        public int getTotalProcesosTerminados()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                int total = dc.Tbl_CompraPollos.Count(a => a.enEspera == false && a.EnProceso == false);
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// Obtiene el ID de la Galera en produccion del lote seleccionado para la compra de pollo por matadero
        /// </summary>
        /// <param name="IDCompraBroilers"></param>
        /// <returns></returns>
        public int getIDGalera_CompraPolloID(int IDCompraBroilers) {
            try
            {
                ayosabdDataContext dc=new ayosabdDataContext();
                return (int)(from compraPollo in dc.Tbl_CompraPollos where (compraPollo.ID_CompraBroilers == IDCompraBroilers) select compraPollo.Tbl_IngresoLotes.Tbl_InventarioGalera).FirstOrDefault().FirstOrDefault().ID_Galeras;
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
