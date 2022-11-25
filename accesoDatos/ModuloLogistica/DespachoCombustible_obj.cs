using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class DespachoCombustible_obj
    {
        public string nuevoDespacho(List<DetalleDesapachoCombustible_obj.DetalleDespacho> detalleDespacho, DateTime FechaDespacho, Guid IngresadoPor, decimal totalGalones, string observaciones, int TipoDespacho)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                ///////////////[  INICIO DE TRANSACCION ]/////////

                // Guardar en la Tabla de Despachos el nuevo despacho
                Tbl_DespachoCombustible nuevo = new Tbl_DespachoCombustible();
                nuevo.FechaIngresoFactura = FechaDespacho;
                nuevo.FechaIngresoSistema = DateTime.Now;
                nuevo.RegistradPor = IngresadoPor;

                ///////////////[  FIN DE TRANSACCION ]/////////
                dc.SubmitChanges();
                dc.Transaction.Commit();// <-- Hacegura la transaccion si todo fue correcto
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
