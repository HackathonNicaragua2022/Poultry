using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class DetalleSalidaHuevoClasificado_obj
    {
        public string nuevo(ayosabdDataContext dc, int ID_SalidaHC, int ID_Clasificacion, decimal CantidadCajillas, int ID_HuevoSinClasificar)
        {
            try
            {
                Tbl_DetalleSalidaHuevoClasificado nuevoDetalle = new Tbl_DetalleSalidaHuevoClasificado();
                nuevoDetalle.ID_SalidaHuevoClasificado = ID_SalidaHC;
                nuevoDetalle.IDClasificacionHuevo = ID_Clasificacion;
                nuevoDetalle.CANTIDADCAJILLAS = CantidadCajillas;
                nuevoDetalle.ID_HuevoSinClasificar = ID_HuevoSinClasificar;
                dc.Tbl_DetalleSalidaHuevoClasificado.InsertOnSubmit(nuevoDetalle);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public class DetalleSaidaHuevoClasificado : Tbl_DetalleSalidaHuevoClasificado
        {
            private string nombreClasificacion;
            private string tipoHuevo;
            private string fechaProduccion;

            public string FechaProduccion
            {
                get { return fechaProduccion; }
                set { fechaProduccion = value; }
            }

            public string NombreClasificacion
            {
                get { return nombreClasificacion; }
                set { nombreClasificacion = value; }
            }

            public string TipoHuevo
            {
                get { return tipoHuevo; }
                set { tipoHuevo = value; }
            }
        }
    }
}
