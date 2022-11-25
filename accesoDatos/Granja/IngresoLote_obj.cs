using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class IngresoLote_obj
    {
        public string IngresarLote(Tbl_IngresoLotes lote, List<Tbl_PersonalEnEntrada> personalEntrada, int IdGalera)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  INICIA BLOQUE DE TRANSACCION ]//////////////////////////////         


                //ingresar los datos a la tabla de lotes
                dc.Tbl_IngresoLotes.InsertOnSubmit(lote);
                dc.SubmitChanges();
                int IDLote = lote.ID_Lote;
                //---------------------------------------
                //ingresar los datos a la able de personal durante el recibo con el id generado
                foreach (Tbl_PersonalEnEntrada personal in personalEntrada)
                {
                    personal.ID_Lote = IDLote;
                    dc.Tbl_PersonalEnEntrada.InsertOnSubmit(personal);
                }
                //---------------------------------------
                //INGRESAR EL LOTE A LA TABLA DE INVENTARIO DE GALERAS PARA SU CONTROL
                Tbl_InventarioGalera galera = new Tbl_InventarioGalera();
                galera.ID_Galeras = IdGalera;
                galera.TotalIngreso = (int)lote.TotalInicialGalera;
                galera.TotalMortalidad = 0;
                galera.TotalSalidas_RemisionesMatadero = 0;
                galera.ID_Lote = IDLote;
                galera.InventarioActivo = true;
                galera.PesoPromedio = lote.PesoPromedioxAveRecibida;
                galera.TotalLibrasPesoVivoMatanza = 0;
                galera.Fecha_IngresoGalera = lote.FechaEntradaGalera;
                galera.fechaIngresoGaleraInicial = lote.FechaEntradaGalera;
                galera.vecesInventarioAjustado = 0;
                dc.Tbl_InventarioGalera.InsertOnSubmit(galera);
                //---------------------------------------
                //Actualizar estado de Galera
                Tbl_Galeras galeraactualizar = (from _galera in dc.Tbl_Galeras where (_galera.ID_Galeras == IdGalera) select _galera).FirstOrDefault();
                galeraactualizar.EnProduccion = true;
                //---------------------------------------
                ///////////////[  FINALIZA BLOQUE DE TRANSACCION ]//////////////////////////////               
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
        public List<IngresoLotes_Class> getLotesIngresados()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<IngresoLotes_Class> result = from _ingresoLotes in dc.Tbl_IngresoLotes
                                                        select new IngresoLotes_Class
                                                        {
                                                            ID_Lote = _ingresoLotes.ID_Lote,
                                                            CodLote = _ingresoLotes.CodLote,
                                                            NumeroFactura = _ingresoLotes.NumeroFactura,
                                                            PesoPromedioxAveRecibida = _ingresoLotes.PesoPromedioxAveRecibida,
                                                            CantidadComprada = _ingresoLotes.CantidadComprada,
                                                            Bonificacion = _ingresoLotes.Bonificacion,
                                                            NetoFactura = _ingresoLotes.NetoFactura,
                                                            MortalidadRecibida = _ingresoLotes.MortalidadRecibida,

                                                            ConteoFisico = _ingresoLotes.ConteoFisico,
                                                            DiferenciaConFactura = _ingresoLotes.DiferenciaConFactura,
                                                            TotalInicialGalera = _ingresoLotes.TotalInicialGalera,
                                                            ApruebaConteo = _ingresoLotes.ApruebaConteo,
                                                            CostoTotal_CompraLoteNIO = _ingresoLotes.CostoTotal_CompraLoteNIO,
                                                            CostoTotal_CompraLoteUSD = _ingresoLotes.CostoTotal_CompraLoteUSD,
                                                            TazaConvercion = _ingresoLotes.TazaConvercion,
                                                            NombrePollo = _ingresoLotes.Tbl_Broilers_Raza.NombreRaza,
                                                            FechaEntradaGalera = _ingresoLotes.FechaEntradaGalera,
                                                            Ingresado_Por = _ingresoLotes.aspnet_Users.UserName,
                                                            FechaAproximadaparaMatarLote = _ingresoLotes.FechaAproximadaparaMatarLote,
                                                            Proveedor = _ingresoLotes.tbl_ProveedoresAvesEngorde.Nombre_Proveedor,
                                                            Temperatura_InicialGalera = _ingresoLotes.Temperatura_InicialGalera,
                                                            CostoxAveNIO = _ingresoLotes.CostoxAveNIO,
                                                            CostoxAveUSD = _ingresoLotes.CostoxAveUSD,
                                                            CantidadAvesPesadasparaPesoProm = _ingresoLotes.CantidadAvesPesadasparaPesoProm,
                                                            fechaEntradaSistema = _ingresoLotes.fechaEntradaSistema,
                                                            Galera = _ingresoLotes.CodLote.Substring(0, 2)
                                                        };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<IngresoLotes_Class>();
            }
        }
        public string eliminarLote(int iDLoteINgreso)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  INICIA BLOQUE DE TRANSACCION ]//////////////////////////////   

                //Eliminar el personal relacionado con el lote
                Tbl_PersonalEnEntrada personalEnEntrada = (from _personalEntrada in dc.Tbl_PersonalEnEntrada where (_personalEntrada.ID_Lote == iDLoteINgreso) select _personalEntrada).FirstOrDefault();
                dc.Tbl_PersonalEnEntrada.DeleteOnSubmit(personalEnEntrada);

                //Eliminar el inventario relacionado con el inventario
                Tbl_InventarioGalera inventarioGalera = (from _inventarioGalera in dc.Tbl_InventarioGalera where (_inventarioGalera.ID_Lote == iDLoteINgreso) select _inventarioGalera).FirstOrDefault();
                dc.Tbl_InventarioGalera.DeleteOnSubmit(inventarioGalera);

                //La galera se estable en produccion false para que este disponible
                Tbl_Galeras galeraEnUso = (from _galera in dc.Tbl_Galeras where (_galera.ID_Galeras == inventarioGalera.ID_Galeras) select _galera).FirstOrDefault();
                galeraEnUso.EnProduccion = false;
                dc.SubmitChanges();

                // si hay proceso de matanza para el lote eliminarlo
                try
                {
                    Tbl_CompraPollos compraPollos = (from _compraPollos in dc.Tbl_CompraPollos where _compraPollos.ID_Lote == iDLoteINgreso select _compraPollos).FirstOrDefault();

                    //Eliminar el Compra de matadero
                    Tbl_PesoFrio_master pesoFrioMaster = (from _pesoFrioMaster in dc.Tbl_PesoFrio_master where _pesoFrioMaster.ID_CompraBroilers == compraPollos.ID_CompraBroilers select _pesoFrioMaster).FirstOrDefault();
                    dc.Tbl_PesoFrio_master.DeleteOnSubmit(pesoFrioMaster);
                    dc.SubmitChanges();

                    //Eliminar el Compra de matadero                
                    dc.Tbl_CompraPollos.DeleteOnSubmit(compraPollos);
                    dc.SubmitChanges();
                }
                catch (Exception)
                {
                    //ignore
                }


                //Eliminar el Lote ingresado
                Tbl_IngresoLotes loteIngreso = (from Lote in dc.Tbl_IngresoLotes where Lote.ID_Lote == iDLoteINgreso select Lote).FirstOrDefault();
                dc.Tbl_IngresoLotes.DeleteOnSubmit(loteIngreso);
                dc.SubmitChanges();

                /* 
                 * si en la tabla de inventario de galera ya se hace uso no se debera eliminar solo desahaprobar
                 * 
                 * */

                ///////////////[  FINALIZA BLOQUE DE TRANSACCION ]//////////////////////////////                             
                dc.Transaction.Commit();
                return "1";
            }
            catch (Exception)
            {
                dc.Transaction.Rollback();
                return "No se ha podido eliminar el Lote";
            }
            finally
            {
                dc.Connection.Close();
            }
        }

        public List<Tbl_PersonalEnEntrada> getPersonalEntrada(int ID_Lote)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_PersonalEnEntrada> Reslt = from PersonalEntrada in dc.Tbl_PersonalEnEntrada where (PersonalEntrada.ID_Lote == ID_Lote) select PersonalEntrada;
                return Reslt.ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_PersonalEnEntrada>();
            }
        }

   
    }
}
