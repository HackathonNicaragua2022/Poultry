using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class DetalleIngresoHSC_obj
    {
        /// <summary>
        /// INgresa un nuevo detalle al sistema 
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="fechaProduccion"></param>
        /// <param name="ID_IngresoInventario"></param>
        /// <param name="ID_Jaulas"></param>
        /// <param name="CantidadCajillas"></param>
        /// <param name="ID_TipoHuevo"></param>
        /// <returns> devuelve 1 si se ha ingresado correctamente o en caso contrario el error</returns>
        public string nuevo(ayosabdDataContext dc, DateTime fechaProduccion, int ID_IngresoInventario, int ID_Jaulas, decimal CantidadCajillas, int ID_TipoHuevo)
        {
            try
            {
                Tbl_DetalleIngreso_HSC nuevoDetalle = new Tbl_DetalleIngreso_HSC();
                nuevoDetalle.ID_IngresoInventario = ID_IngresoInventario;
                nuevoDetalle.ID_Jaulas = ID_Jaulas;
                nuevoDetalle.CANTIDADCAJILLA = CantidadCajillas;
                nuevoDetalle.ID_TipoHUevo = ID_TipoHuevo;
                nuevoDetalle.FechaProduccion = fechaProduccion;
                dc.Tbl_DetalleIngreso_HSC.InsertOnSubmit(nuevoDetalle);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<DetalleIngresoHSC> getAllDetallesXIDIngreso(int ID_Ingreso)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<DetalleIngresoHSC> resultado = from _detalle in dc.Tbl_DetalleIngreso_HSC
                                                          where (_detalle.ID_IngresoInventario == ID_Ingreso)
                                                          select new DetalleIngresoHSC
                                                          {
                                                              ID_DetalleIngresoInventarioHSC = _detalle.ID_DetalleIngresoInventarioHSC,
                                                              ID_IngresoInventario = _detalle.ID_DetalleIngresoInventarioHSC,
                                                              ID_Jaulas = _detalle.ID_Jaulas,
                                                              CANTIDADCAJILLA = _detalle.CANTIDADCAJILLA,
                                                              FechaProduccion = _detalle.FechaProduccion,
                                                              ID_TipoHUevo = _detalle.ID_TipoHUevo,
                                                              NombreJaula = _detalle.Cat_Jaulas.Hubicacion,
                                                              TipoHuevo = _detalle.Cat_TipoHuevo.TipoHuevo
                                                          };
                return resultado.ToList();
            }
            catch (Exception)
            {
                return new List<DetalleIngresoHSC>();
            }
        }

        public class DetalleIngresoHSC : Tbl_DetalleIngreso_HSC
        {
            private string nombreJaula;
            private string tipoHuevo;

            public string TipoHuevo
            {
                get { return tipoHuevo; }
                set { tipoHuevo = value; }
            }
            public string NombreJaula
            {
                get { return nombreJaula; }
                set { nombreJaula = value; }
            }
        }
    }
}
