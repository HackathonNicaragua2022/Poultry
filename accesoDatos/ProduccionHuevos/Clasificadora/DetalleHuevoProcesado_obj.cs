using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class DetalleHuevoProcesado_obj
    {
        public string nuevoDetalle(ayosabdDataContext dc, int ID_HuevoProcesado, int ID_HuevoSinClasificar, decimal CantidadCajillasUsadas, decimal CantidadProducidas, String Observaciones)
        {
            try
            {
                Tbl_DetalleHuevoProcesado nuevoDetalle = new Tbl_DetalleHuevoProcesado();
                nuevoDetalle.ID_HuevoProcesado = ID_HuevoProcesado;
                nuevoDetalle.ID_HuevoSinClasificar = ID_HuevoSinClasificar;
                nuevoDetalle.CantidadCajillasUsadas = CantidadCajillasUsadas;
                nuevoDetalle.CantidadProducida = CantidadProducidas;
                nuevoDetalle.Observaciones = Observaciones;
                dc.Tbl_DetalleHuevoProcesado.InsertOnSubmit(nuevoDetalle);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
