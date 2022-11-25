using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.PlantasProcesadoras
{
    public class Tbl_IngresoPesoVivo_Obj
    {
        /// <summary>
        /// Ingresa un nuevo pesaje a la base de Datos
        /// </summary>
        /// <param name="pesoVivo"></param>
        /// <returns></returns>
        public string NuevoPesoVivo(tbl_IngresoPesoVivo pesoVivo, int IDCompraPollo, DateTime HoraLLegadaPlanta)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();

                Tbl_CompraPollos compraPollo = (from compra in dc.Tbl_CompraPollos where (compra.ID_CompraBroilers == IDCompraPollo) select compra).FirstOrDefault();
                //Actualizar el numero de pollos segun remisiones
                compraPollo.TotalAvesRemisionCompradas += (int)(pesoVivo.CantidadJavasPesadas * pesoVivo.PollosxJava);
                //Actualizar el numero Total de Libras pesadas de pollos
                decimal TotalNetoLibrasPollo = (decimal)(pesoVivo.PesoJavaConPollo_Libras - (pesoVivo.PesoxJavaVacia_libDefault * pesoVivo.CantidadJavasPesadas));
                compraPollo.TotalLibrasCompradasCalculoBascula += (decimal)TotalNetoLibrasPollo;
                dc.SubmitChanges();

                /// Ahora actualizamos la Hora de entrada al Matadero y el pesoTotal por la remision
                Tbl_ViajesRemisionGranja remisio = (from rem in dc.Tbl_ViajesRemisionGranja where (rem.ID_ViajesRemision == pesoVivo.ID_ViajesRemision) select rem).FirstOrDefault();
                remisio.HoraLlegadaPlanta = HoraLLegadaPlanta;
                remisio.TotalLibrasxRemision += (decimal)TotalNetoLibrasPollo;
                dc.SubmitChanges();

                // SE ACTUALIZA EL PESO DE LIBRAS OBTENIDAS EN MATADERO DE LA TABLA DE INVENTARIO GALERA
                Tbl_InventarioGalera inventarioGalera = (from _inventarioGaleraSeleccionado in dc.Tbl_InventarioGalera where (_inventarioGaleraSeleccionado.ID_InventarioBroilers == remisio.ID_InventarioBroilers) select _inventarioGaleraSeleccionado).FirstOrDefault();
                inventarioGalera.TotalLibrasPesoVivoMatanza += (decimal)TotalNetoLibrasPollo;
                //Actualiza el peso promedio basado en la remision
                // combertir en gramos, ya que asi se maneja este campo
                inventarioGalera.PesoPromedio = (compraPollo.TotalLibrasCompradasCalculoBascula / compraPollo.TotalAvesRemisionCompradas) * (decimal)453.592;
                dc.SubmitChanges();
                // *************************************************************************************


                ///INsertar nuevo pesaje
                dc.tbl_IngresoPesoVivo.InsertOnSubmit(pesoVivo);
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
        /// Obtiene todos los pesajes para una remision
        /// </summary>
        /// <param name="IDViajesRemision"></param>
        /// <returns></returns>
        public List<tbl_IngresoPesoVivo> getAllPesajes(int IDViajesRemision)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<tbl_IngresoPesoVivo> result = from pesajes in dc.tbl_IngresoPesoVivo where (pesajes.ID_ViajesRemision == IDViajesRemision) select pesajes;
                return result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }



        /// <summary>
        /// Obtiene un objeto de tipo pesaje 
        /// </summary>
        /// <param name="IDPesajeRemision"></param>
        /// <returns></returns>
        public tbl_IngresoPesoVivo getPesajesxID(int IDPesajeRemision)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from pesajes in dc.tbl_IngresoPesoVivo where (pesajes.ID_IngresoPesoVivo == IDPesajeRemision) select pesajes).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Obtiene el numero de pesajes totales en la remision seleccionada
        /// </summary>
        /// <param name="IDViajesRemision"></param>
        /// <returns></returns>
        public int getTotalPesajesxRemision(int IDViajesRemision)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                int TotalPesajes = dc.tbl_IngresoPesoVivo.Count(a => a.ID_ViajesRemision == IDViajesRemision) + 1;
                if (TotalPesajes <= 0)
                {
                    return 1;
                }
                return TotalPesajes;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        /// <summary>
        /// Cierra el ingreso de pesajes para un remision
        /// </summary>
        /// <param name="IDRemision"></param>
        /// <returns></returns>
        public string finalizarPesajeRemision(int IDRemision)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_ViajesRemisionGranja viajeRemision = (from viajeR in dc.Tbl_ViajesRemisionGranja where (viajeR.ID_ViajesRemision == IDRemision) select viajeR).FirstOrDefault();
                viajeRemision.Finalizada = true;
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Elimina un pesaje previamente ingresado en el sitema de la remision
        /// </summary>
        /// <param name="IdIngresoPesoVivi"></param>
        /// <returns></returns>
        public string eliminarPesajeRemision(tbl_IngresoPesoVivo pesoVivo, int IDCompraPollo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();

                try
                {
                    dc.Connection.Open();
                    dc.Transaction = dc.Connection.BeginTransaction();

                    Tbl_CompraPollos compraPollo = (from compra in dc.Tbl_CompraPollos where (compra.ID_CompraBroilers == IDCompraPollo) select compra).FirstOrDefault();
                    //Actualizar el numero de pollos segun remisiones
                    compraPollo.TotalAvesRemisionCompradas -= (int)(pesoVivo.CantidadJavasPesadas * pesoVivo.PollosxJava);
                    //Actualizar el numero Total de Libras pesadas de pollos
                    decimal TotalNetoLibrasPollo = (decimal)(pesoVivo.PesoJavaConPollo_Libras - (pesoVivo.PesoxJavaVacia_libDefault * pesoVivo.CantidadJavasPesadas));
                    compraPollo.TotalLibrasCompradasCalculoBascula -= (decimal)TotalNetoLibrasPollo;
                    dc.SubmitChanges();

                    /// Ahora actualizamos la Hora de entrada al Matadero y el pesoTotal por la remision
                    Tbl_ViajesRemisionGranja remisio = (from rem in dc.Tbl_ViajesRemisionGranja where (rem.ID_ViajesRemision == pesoVivo.ID_ViajesRemision) select rem).FirstOrDefault();
                    //remisio.HoraLlegadaPlanta = HoraLLegadaPlanta;
                    remisio.TotalLibrasxRemision -= (decimal)TotalNetoLibrasPollo;
                    dc.SubmitChanges();


                    // SE ACTUALIZA EL PESO DE LIBRAS OBTENIDAS EN MATADERO DE LA TABLA DE INVENTARIO GALERA
                    Tbl_InventarioGalera inventarioGalera = (from _inventarioGaleraSeleccionado in dc.Tbl_InventarioGalera where (_inventarioGaleraSeleccionado.ID_InventarioBroilers == remisio.ID_InventarioBroilers) select _inventarioGaleraSeleccionado).FirstOrDefault();
                    inventarioGalera.TotalLibrasPesoVivoMatanza -= (decimal)TotalNetoLibrasPollo;
                    // *************************************************************************************

                    ///ELiminar el Pesaje                    
                    tbl_IngresoPesoVivo pesoEliminar = (from _pesoEliminar in dc.tbl_IngresoPesoVivo where (_pesoEliminar.ID_IngresoPesoVivo == pesoVivo.ID_IngresoPesoVivo) select _pesoEliminar).FirstOrDefault();
                    dc.tbl_IngresoPesoVivo.DeleteOnSubmit(pesoEliminar);

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
        /// Modifica las javas y el numero de aves del pesaje
        /// </summary>
        /// <param name="pesoVivo"></param>
        /// <param name="IDCompraPollo"></param>
        public string ModificarPesaje(tbl_IngresoPesoVivo pesoVivo, int TotalJavas, int TotalPollosxJava, int IDCompraPollo)
        {

            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();

                try
                {
                    dc.Connection.Open();
                    dc.Transaction = dc.Connection.BeginTransaction();

                    Tbl_CompraPollos compraPollo = (from compra in dc.Tbl_CompraPollos where (compra.ID_CompraBroilers == IDCompraPollo) select compra).FirstOrDefault();
                    //Actualizar el numero de pollos segun remisiones restando el anterior valor
                    compraPollo.TotalAvesRemisionCompradas -= (int)(pesoVivo.CantidadJavasPesadas * pesoVivo.PollosxJava);
                    // Posteriormente se suma el numero correcto de pollos
                    compraPollo.TotalAvesRemisionCompradas += (TotalJavas * TotalPollosxJava);

                    //Actualizar el numero Total de Libras pesadas de pollos restando el anterior valor
                    decimal TotalNetoLibrasPollo = (decimal)(pesoVivo.PesoJavaConPollo_Libras - (pesoVivo.PesoxJavaVacia_libDefault * pesoVivo.CantidadJavasPesadas));
                    compraPollo.TotalLibrasCompradasCalculoBascula -= (decimal)TotalNetoLibrasPollo;

                    decimal NUevo_TotalNetoLibrasPollo = (decimal)(pesoVivo.PesoJavaConPollo_Libras - (pesoVivo.PesoxJavaVacia_libDefault * TotalPollosxJava));
                    compraPollo.TotalLibrasCompradasCalculoBascula += (decimal)NUevo_TotalNetoLibrasPollo;


                    dc.SubmitChanges();

                    /// Ahora actualizamos la Hora de entrada al Matadero y el pesoTotal por la remision
                    Tbl_ViajesRemisionGranja remisio = (from rem in dc.Tbl_ViajesRemisionGranja where (rem.ID_ViajesRemision == pesoVivo.ID_ViajesRemision) select rem).FirstOrDefault();
                    //remisio.HoraLlegadaPlanta = HoraLLegadaPlanta;
                    remisio.TotalLibrasxRemision -= (decimal)TotalNetoLibrasPollo;
                    remisio.TotalLibrasxRemision += (decimal)NUevo_TotalNetoLibrasPollo;
                    dc.SubmitChanges();


                    // SE ACTUALIZA EL PESO DE LIBRAS OBTENIDAS EN MATADERO DE LA TABLA DE INVENTARIO GALERA
                    Tbl_InventarioGalera inventarioGalera = (from _inventarioGaleraSeleccionado in dc.Tbl_InventarioGalera where (_inventarioGaleraSeleccionado.ID_InventarioBroilers == remisio.ID_InventarioBroilers) select _inventarioGaleraSeleccionado).FirstOrDefault();
                    inventarioGalera.TotalLibrasPesoVivoMatanza -= (decimal)TotalNetoLibrasPollo;
                    inventarioGalera.TotalLibrasPesoVivoMatanza += (decimal)NUevo_TotalNetoLibrasPollo;
                    // *************************************************************************************


                    ///Actualizar el Pesaje
                    tbl_IngresoPesoVivo pesaje = (from _pesoVivo in dc.tbl_IngresoPesoVivo where (_pesoVivo.ID_IngresoPesoVivo == pesoVivo.ID_IngresoPesoVivo) select _pesoVivo).FirstOrDefault();
                    pesaje.CantidadJavasPesadas = TotalJavas;
                    pesaje.PollosxJava = TotalPollosxJava;
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


        public int getIDCompraPollosx_IDRemision(int IDRemision)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_ViajesRemisionGranja viajeRemision = (from viajeR in dc.Tbl_ViajesRemisionGranja where (viajeR.ID_ViajesRemision == IDRemision) select viajeR).FirstOrDefault();
                return viajeRemision.ID_CompraBroilers;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
