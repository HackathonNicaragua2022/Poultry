using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class ParametrosDiarios_obj
    {
        public string nuevoParametro(Tbl_ParametrosDiarios parametrosDiarios)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  INICIA BLOQUE DE TRANSACCION ]//////////////////////////////         
                dc.Tbl_ParametrosDiarios.InsertOnSubmit(parametrosDiarios);
                dc.SubmitChanges();

                Tbl_InventarioGalera inventario = (from _inventario in dc.Tbl_InventarioGalera where (_inventario.ID_InventarioBroilers == parametrosDiarios.ID_InventarioBroilers) select _inventario).FirstOrDefault();
                inventario.TotalMortalidad += parametrosDiarios.Mortalidad;
                inventario.PesoPromedio = parametrosDiarios.Peso_Promedio;
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
        public string ActualizarPesoPromedio(int IDParametroDiario, decimal PesoPromedio, bool actualizarLote)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  INICIA BLOQUE DE TRANSACCION ]//////////////////////////////                         
                Tbl_ParametrosDiarios paramd = (from parametrosdiarios in dc.Tbl_ParametrosDiarios where (parametrosdiarios.ID_ParametrosDiarios == IDParametroDiario) select parametrosdiarios).FirstOrDefault();
                paramd.Peso_Promedio = PesoPromedio;
                dc.SubmitChanges();
                if (actualizarLote)
                {
                    Tbl_InventarioGalera inventario = (from _inventario in dc.Tbl_InventarioGalera where (_inventario.ID_InventarioBroilers == paramd.ID_InventarioBroilers) select _inventario).FirstOrDefault();
                    inventario.PesoPromedio = PesoPromedio;
                }
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
        public string EliminarParametro(int idParametro)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  INICIA BLOQUE DE TRANSACCION ]//////////////////////////////         
                Tbl_ParametrosDiarios parametroEliminar = (from _parametro in dc.Tbl_ParametrosDiarios where (_parametro.ID_ParametrosDiarios == idParametro) select _parametro).FirstOrDefault();


                Tbl_InventarioGalera inventario = (from _inventario in dc.Tbl_InventarioGalera where (_inventario.ID_InventarioBroilers == parametroEliminar.ID_InventarioBroilers) select _inventario).FirstOrDefault();
                inventario.TotalMortalidad -= parametroEliminar.Mortalidad;

                dc.Tbl_ParametrosDiarios.DeleteOnSubmit(parametroEliminar);
                dc.SubmitChanges();
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
        public List<ParametrosVistaRel> getParametrosDiarios(int IDInventarioGalera)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ParametrosVistaRel> result = from _parametrosDiarios in dc.Tbl_ParametrosDiarios
                                                        where (_parametrosDiarios.ID_InventarioBroilers == IDInventarioGalera)
                                                        select new ParametrosVistaRel
                                                        {
                                                            ID_ParametrosDiarios = _parametrosDiarios.ID_ParametrosDiarios,
                                                            ID_InventarioBroilers = _parametrosDiarios.ID_InventarioBroilers,
                                                            FechaRegistro = _parametrosDiarios.FechaRegistro,
                                                            Mortalidad = _parametrosDiarios.Mortalidad,
                                                            Peso_Promedio = _parametrosDiarios.Peso_Promedio,
                                                            Uniformidad = _parametrosDiarios.Uniformidad,
                                                            RegistradoPor = _parametrosDiarios.RegistradoPor,
                                                            NombreRegistrado = _parametrosDiarios.aspnet_Users.UserName,
                                                            ComentariosAdicionales = _parametrosDiarios.ComentariosAdicionales,
                                                            URLImagenMortalidad = _parametrosDiarios.URLImagenMortalidad,
                                                            PesoPromedioLibras = (double)_parametrosDiarios.Peso_Promedio / 453.59
                                                        };
                return result.ToList();

            }
            catch (Exception)
            {
                return new List<ParametrosVistaRel>();
            }
        }
    }
}
