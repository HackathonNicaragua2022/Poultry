using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.PlantasProcesadoras
{
    public class Tbl_ViajesRemisionGranja_obj
    {
        public string eliminarRemision(int ID_ViajesRemision)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                //------------------------------------------------                                
                //Obtener el objeto de remision para utilizar sus valores
                Tbl_ViajesRemisionGranja viajeRemisionGranja = (from _viajeRemisionGranja in dc.Tbl_ViajesRemisionGranja where _viajeRemisionGranja.ID_ViajesRemision == ID_ViajesRemision select _viajeRemisionGranja).FirstOrDefault();


                //Eliminar todos los registro de javas enviadas de la tabla Tblk_JavasEnviadas
                IQueryable<Tbl_JavasEnviadas> javasEnviadas = from _javasEnviadas in dc.Tbl_JavasEnviadas where (_javasEnviadas.ID_ViajesRemision == ID_ViajesRemision) select _javasEnviadas;
                dc.Tbl_JavasEnviadas.DeleteAllOnSubmit(javasEnviadas);
                dc.SubmitChanges();

                //Si ya se registro peso pero aun asi se va a eliminar la remision
                //entonces eliminar todos los registro de pesajes de la tabla Tbl_IngresoPesoVivo
                IQueryable<tbl_IngresoPesoVivo> ingresoPesovivos = from _ingresosPesoVivos in dc.tbl_IngresoPesoVivo where (_ingresosPesoVivos.ID_ViajesRemision == ID_ViajesRemision) select _ingresosPesoVivos;

                //Verificar si hay registros, si hay se reduce el total de aves compradas de la tabla compra
                if (ingresoPesovivos != null)
                {
                    if (ingresoPesovivos.ToList().Count > 0)
                    {
                        Tbl_CompraPollos compraPollo = (from compra in dc.Tbl_CompraPollos where (compra.ID_CompraBroilers == viajeRemisionGranja.ID_CompraBroilers) select compra).FirstOrDefault();
                        //Actualizar el numero de pollos segun remisiones, restar
                        compraPollo.TotalAvesRemisionCompradas -= ingresoPesovivos.Sum(a => a.TotalPollos);
                        //Actualizar el numero Total de Libras pesadas de pollos                        
                        compraPollo.TotalLibrasCompradasCalculoBascula -= ingresoPesovivos.Sum(a => a.PesoNetoPollosLibra);
                        dc.SubmitChanges();
                        dc.tbl_IngresoPesoVivo.DeleteAllOnSubmit(ingresoPesovivos);
                        dc.SubmitChanges();
                    }
                }


                //Actualiza el Inventario del lote de broilers en crecimiento
                Tbl_InventarioGalera inventario = (from _inventarioGalera in dc.Tbl_InventarioGalera where _inventarioGalera.ID_InventarioBroilers == viajeRemisionGranja.ID_InventarioBroilers select _inventarioGalera).FirstOrDefault();
                //Quitar el total de aves enviadas en la remision
                inventario.TotalSalidas_RemisionesMatadero -= (int)viajeRemisionGranja.TotalAvesEnviadas;
                // reducir el peso en libras
                inventario.TotalLibrasPesoVivoMatanza -= (decimal)viajeRemisionGranja.TotalLibrasxRemision;
                dc.SubmitChanges();

                // De ultimo se elimina el viaje de remision
                dc.Tbl_ViajesRemisionGranja.DeleteOnSubmit(viajeRemisionGranja);
                dc.SubmitChanges();
                //------------------------------------------------               
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
        /// Registra una nueva remision en la compra seleccionada con el ID
        /// </summary>
        /// <param name="remision"></param>
        /// <returns></returns>
        public int NuevaRemision(Tbl_ViajesRemisionGranja remision, List<Tbl_JavasEnviadas> _javasEnviadas)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                //------------------------------------------------                
                remision.Total_javas = _javasEnviadas.Sum(a => a.Cantidad_Javas);
                remision.TotalAvesEnviadas = _javasEnviadas.Sum(a => a.SubTotalAves);
                remision.JavasPendientesxPesar = _javasEnviadas.Sum(a => a.Cantidad_Javas);
                dc.Tbl_ViajesRemisionGranja.InsertOnSubmit(remision);
                dc.SubmitChanges();



                //Actualiza las javas con el ID de la remision previamente guardada
                _javasEnviadas.ForEach(a => a.ID_ViajesRemision = remision.ID_ViajesRemision);
                dc.Tbl_JavasEnviadas.InsertAllOnSubmit(_javasEnviadas);
                dc.SubmitChanges();
                //Actualiza el Inventario del lote de broilers en crecimiento
                Tbl_InventarioGalera inventario = (from _inventarioGalera in dc.Tbl_InventarioGalera where _inventarioGalera.ID_InventarioBroilers == remision.ID_InventarioBroilers select _inventarioGalera).FirstOrDefault();
                //incrementar el total de aves enviadas
                inventario.TotalSalidas_RemisionesMatadero += (int)remision.TotalAvesEnviadas;

                //------------------------------------------------               
                dc.SubmitChanges();
                dc.Transaction.Commit();
                return remision.ID_ViajesRemision;
            }
            catch (Exception)
            {
                dc.Transaction.Rollback();
                return 0;
            }
            finally
            {
                dc.Connection.Close();
            }
        }

        /// <summary>
        /// Obtiene todos los viajes de Remisiones basado en la
        /// </summary>
        /// <param name="terminados"></param>
        /// <param name="IdCompra"></param>
        /// <returns></returns>
        public List<ViajesRemision> getAllViajesRemision(bool terminados, int IdCompra)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ViajesRemision> Result = from ViajesRemisionGranja in dc.Tbl_ViajesRemisionGranja
                                                    join galerasGranja in dc.Tbl_Galeras on ViajesRemisionGranja.ID_Galeras equals galerasGranja.ID_Galeras
                                                    join aspnetUsers in dc.aspnet_Users on ViajesRemisionGranja.RecibidoPor equals aspnetUsers.UserId
                                                    where (ViajesRemisionGranja.Finalizada == terminados && ViajesRemisionGranja.ID_CompraBroilers == IdCompra)
                                                    select new ViajesRemision
                                                    {
                                                        ID_ViajesRemision = ViajesRemisionGranja.ID_ViajesRemision,
                                                        ID_CompraBroilers = ViajesRemisionGranja.ID_CompraBroilers,
                                                        Fecha = ViajesRemisionGranja.Fecha,
                                                        //NumeroRemision = ViajesRemisionGranja.NumeroRemision,
                                                        NumeroViaje = ViajesRemisionGranja.NumeroViaje,
                                                        NombreConductor = ViajesRemisionGranja.NombreConductor,
                                                        PlacaCamion = ViajesRemisionGranja.PlacaCamion,
                                                        Total_javas = ViajesRemisionGranja.Total_javas,
                                                        HoraAyuno = ViajesRemisionGranja.HoraAyuno,
                                                        Edad = ViajesRemisionGranja.Edad,
                                                        ID_Galeras = ViajesRemisionGranja.ID_Galeras,
                                                        NumeroGalera = (int)galerasGranja.NumeroOrden,
                                                        Destino = ViajesRemisionGranja.Destino,
                                                        //PollosxJaba = ViajesRemisionGranja.PollosxJaba,
                                                        TotalAvesEnviadas = ViajesRemisionGranja.TotalAvesEnviadas,
                                                        HoraSalidaGranja = ViajesRemisionGranja.HoraSalidaGranja,
                                                        HoraLlegadaPlanta = ViajesRemisionGranja.HoraLlegadaPlanta,
                                                        EntregaConforme = ViajesRemisionGranja.EntregaConforme,
                                                        RecibidoPor = ViajesRemisionGranja.RecibidoPor,
                                                        CreadoPor = aspnetUsers.UserName,
                                                        EstadoSaludAve = ViajesRemisionGranja.EstadoSaludAve,
                                                        NUFDMADEAS = ViajesRemisionGranja.NUFDMADEAS,
                                                        Finalizada = ViajesRemisionGranja.Finalizada,
                                                        JavasPendientesxPesar = ViajesRemisionGranja.JavasPendientesxPesar,
                                                        TotalLibrasxRemision = ViajesRemisionGranja.TotalLibrasxRemision
                                                    };
                return Result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene todos los viajes de la compra incluyendo los viajes termianos y los no terminados
        /// </summary>
        /// <param name="IdCompra"></param>
        /// <returns></returns>
        public List<ViajesRemision> getAllViajesRemision(int IdCompra)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ViajesRemision> Result = from ViajesRemisionGranja in dc.Tbl_ViajesRemisionGranja
                                                    join galerasGranja in dc.Tbl_Galeras on ViajesRemisionGranja.ID_Galeras equals galerasGranja.ID_Galeras
                                                    join aspnetUsers in dc.aspnet_Users on ViajesRemisionGranja.RecibidoPor equals aspnetUsers.UserId
                                                    where (ViajesRemisionGranja.ID_CompraBroilers == IdCompra)
                                                    select new ViajesRemision
                                                    {
                                                        ID_ViajesRemision = ViajesRemisionGranja.ID_ViajesRemision,
                                                        ID_CompraBroilers = ViajesRemisionGranja.ID_CompraBroilers,
                                                        Fecha = ViajesRemisionGranja.Fecha,
                                                        //NumeroRemision = ViajesRemisionGranja.NumeroRemision,
                                                        NumeroViaje = ViajesRemisionGranja.NumeroViaje,//
                                                        NombreConductor = ViajesRemisionGranja.NombreConductor,
                                                        PlacaCamion = ViajesRemisionGranja.PlacaCamion,
                                                        Total_javas = ViajesRemisionGranja.Total_javas,
                                                        HoraAyuno = ViajesRemisionGranja.HoraAyuno,
                                                        Edad = ViajesRemisionGranja.Edad,
                                                        ID_Galeras = ViajesRemisionGranja.ID_Galeras,
                                                        NumeroGalera = (int)galerasGranja.NumeroOrden,
                                                        Destino = ViajesRemisionGranja.Destino,
                                                        //PollosxJaba = ViajesRemisionGranja.PollosxJaba,
                                                        TotalAvesEnviadas = ViajesRemisionGranja.TotalAvesEnviadas,
                                                        HoraSalidaGranja = ViajesRemisionGranja.HoraSalidaGranja,
                                                        HoraLlegadaPlanta = ViajesRemisionGranja.HoraLlegadaPlanta,
                                                        EntregaConforme = ViajesRemisionGranja.EntregaConforme,
                                                        RecibidoPor = ViajesRemisionGranja.RecibidoPor,
                                                        CreadoPor = aspnetUsers.UserName,
                                                        EstadoSaludAve = ViajesRemisionGranja.EstadoSaludAve,
                                                        NUFDMADEAS = ViajesRemisionGranja.NUFDMADEAS,
                                                        Finalizada = ViajesRemisionGranja.Finalizada,
                                                        TotalLibrasxRemision = ViajesRemisionGranja.TotalLibrasxRemision,
                                                        PesoAcumulado = 0,
                                                        PesoPromedio = (decimal)ViajesRemisionGranja.TotalLibrasxRemision / (decimal)ViajesRemisionGranja.TotalAvesEnviadas
                                                    };
                return Result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Obtiene un viaje de remision por el ID que lo identifica
        /// </summary>
        /// <param name="IdCompra"></param>
        /// <returns></returns>
        public ViajesRemision getRemisionByID(int IDRemision)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ViajesRemision> Result = from ViajesRemisionGranja in dc.Tbl_ViajesRemisionGranja
                                                    join galerasGranja in dc.Tbl_Galeras on ViajesRemisionGranja.ID_Galeras equals galerasGranja.ID_Galeras
                                                    join aspnetUsers in dc.aspnet_Users on ViajesRemisionGranja.RecibidoPor equals aspnetUsers.UserId
                                                    where (ViajesRemisionGranja.ID_ViajesRemision == IDRemision)
                                                    select new ViajesRemision
                                                    {
                                                        ID_ViajesRemision = ViajesRemisionGranja.ID_ViajesRemision,
                                                        ID_CompraBroilers = ViajesRemisionGranja.ID_CompraBroilers,
                                                        Fecha = ViajesRemisionGranja.Fecha,
                                                        //NumeroRemision = ViajesRemisionGranja.NumeroRemision,
                                                        NumeroViaje = ViajesRemisionGranja.NumeroViaje,//
                                                        NombreConductor = ViajesRemisionGranja.NombreConductor,
                                                        PlacaCamion = ViajesRemisionGranja.PlacaCamion,
                                                        Total_javas = ViajesRemisionGranja.Total_javas,
                                                        HoraAyuno = ViajesRemisionGranja.HoraAyuno,
                                                        Edad = ViajesRemisionGranja.Edad,
                                                        ID_Galeras = ViajesRemisionGranja.ID_Galeras,
                                                        NumeroGalera = (int)galerasGranja.NumeroOrden,
                                                        Destino = ViajesRemisionGranja.Destino,
                                                        //PollosxJaba = ViajesRemisionGranja.PollosxJaba,
                                                        TotalAvesEnviadas = ViajesRemisionGranja.TotalAvesEnviadas,
                                                        HoraSalidaGranja = ViajesRemisionGranja.HoraSalidaGranja,
                                                        HoraLlegadaPlanta = ViajesRemisionGranja.HoraLlegadaPlanta,
                                                        EntregaConforme = ViajesRemisionGranja.EntregaConforme,
                                                        RecibidoPor = ViajesRemisionGranja.RecibidoPor,
                                                        CreadoPor = aspnetUsers.UserName,
                                                        EstadoSaludAve = ViajesRemisionGranja.EstadoSaludAve,
                                                        NUFDMADEAS = ViajesRemisionGranja.NUFDMADEAS,
                                                        Finalizada = ViajesRemisionGranja.Finalizada,
                                                        TotalLibrasxRemision = ViajesRemisionGranja.TotalLibrasxRemision,
                                                        PesoAcumulado = 0
                                                    };
                return Result.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// clase para obtener una consulta relacionada con los viajes de Remison 
        /// </summary>
        public class ViajesRemision : Tbl_ViajesRemisionGranja
        {
            private int numeroGalera;

            public int NumeroGalera
            {
                get { return numeroGalera; }
                set { numeroGalera = value; }
            }
            private string creadoPor;

            public string CreadoPor
            {
                get { return creadoPor; }
                set { creadoPor = value; }
            }
            private decimal pesoAcumulado;

            public decimal PesoAcumulado
            {
                get { return pesoAcumulado; }
                set { pesoAcumulado = value; }
            }
            private decimal pesoPromedio;

            public decimal PesoPromedio
            {
                get { return pesoPromedio; }
                set { pesoPromedio = value; }
            }
        }
        /// <summary>
        /// Cada Viaje de Remisiones debe tener un numero autoIncrementable, con esta funcion se puede obtener el total de viajes a remisiones y sumar uno para tener el siguiente numero
        /// </summary>
        /// <param name="IDCompraBroilers"></param>
        /// <returns></returns>
        public int getNumeroRemisionxIDCompraBroilers(int IDCompraBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                int TotalRemisiones = dc.Tbl_ViajesRemisionGranja.Count(a => a.ID_CompraBroilers == IDCompraBroilers) + 1;
                if (TotalRemisiones <= 0)
                {
                    return 1;
                }
                return TotalRemisiones;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public int getNumeroViajexIDCompraBroilers(int IDCompraBroilers)
        {
            return getNumeroRemisionxIDCompraBroilers(IDCompraBroilers);
        }
        /// <summary>
        /// Devuelve el numero total de javas enviadas por una compra especificada por el ID
        /// </summary>
        /// <param name="IdCompra"></param>
        /// <returns></returns>
        public int getTotalJavasViajesRemisionxCompra(int IdCompra)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                int totalJavas = (int)dc.Tbl_ViajesRemisionGranja.Where(a => a.ID_CompraBroilers == IdCompra).Sum(a => a.Total_javas);
                return totalJavas;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// Obtiene el peso Promedio de toda la compra en proceso
        /// </summary>
        /// <param name="IdCompra"></param>
        /// <returns></returns>
        public decimal getPesoPromedio(int IdCompra)
        {
            try
            {
                Tbl_ViajesRemisionGranja_obj viajes = new Tbl_ViajesRemisionGranja_obj();
                List<Tbl_ViajesRemisionGranja_obj.ViajesRemision> remisiones = viajes.getAllViajesRemision(IdCompra);
                decimal _librasTotal = (decimal)(remisiones.Sum(a => a.TotalLibrasxRemision));
                int _TotalPollos = (int)(remisiones.Sum(a => a.TotalAvesEnviadas));
                decimal _pesoPromedio = _librasTotal / _TotalPollos;

                return _pesoPromedio;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        /// <summary>
        /// Obtiene la edad en Dias Promedio
        /// </summary>
        /// <param name="IdCompra"></param>
        /// <returns></returns>
        public decimal getEdadDiasPromedio(int IdCompra)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                int edadDias = (int)dc.Tbl_ViajesRemisionGranja.Where(a => a.ID_CompraBroilers == IdCompra).Average(d => d.Edad);
                return edadDias;
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }

}
