using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class AjusteInventarioLoteGranja_obj
    {
        /// <summary>
        /// Devuelte el total de dias de diferencia entre la fecha de ingreso del lote Con el ID y la fecha actual
        /// </summary>
        /// <returns></returns>
        public int getDias(int ID_InventarioBroiler)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                DateTime fechaIngresoLote = (DateTime)(from _inventarioGalera in dc.Tbl_InventarioGalera where _inventarioGalera.ID_InventarioBroilers == ID_InventarioBroiler select _inventarioGalera).FirstOrDefault().Fecha_IngresoGalera;
                int Dias = (DateTime.Now - fechaIngresoLote).Days;
                return Dias;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DateTime getFechaIngresoGalera(int ID_Lote)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (DateTime)(from _lote in dc.Tbl_IngresoLotes where _lote.ID_Lote == ID_Lote select _lote).FirstOrDefault().FechaEntradaGalera;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        public string ingresarAjuste(Tbl_ajusteInventarioInicialLoteGranja ajusteInventario, int NuevaEdadLote)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  INICIA BLOQUE DE TRANSACCION ]//////////////////////////////         

                //se ingresa el objeto a la tabla
                dc.Tbl_ajusteInventarioInicialLoteGranja.InsertOnSubmit(ajusteInventario);

                //se actualiza el inventario
                //en este caso como ya se habia diseñado la base de datos para realizar el cambio solo se actualiza la fecha de ingreso para restar los dias necesarios
                Tbl_InventarioGalera inventarioGalera = (from _inventarioGalera in dc.Tbl_InventarioGalera where _inventarioGalera.ID_InventarioBroilers == ajusteInventario.ID_InventarioBroilers select _inventarioGalera).FirstOrDefault();
                DateTime nuevafechaEntrada = inventarioGalera.Fecha_IngresoGalera.Value.AddDays(-(double)NuevaEdadLote);
                //ingrementamos el numero de veces que se ha ajustado el inventario
                inventarioGalera.vecesInventarioAjustado += 1;
                //se actualiza la fecha de ingreso para que el sistema cuente automaticamente la edad en dia
                inventarioGalera.Fecha_IngresoGalera = nuevafechaEntrada;
                // ahora se actualiza la cantidad entrante
                inventarioGalera.TotalIngreso += (int)ajusteInventario.CantidadIngresada;
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
    }
}
