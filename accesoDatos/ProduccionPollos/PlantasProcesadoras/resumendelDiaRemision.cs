using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.PlantasProcesadoras
{

    public class resumendelDiaRemision
    {
        public datosGenerales getResumenDiaRemision(DateTime fecha)
        {
            try
            {
                datosGenerales dg = new datosGenerales();
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_CompraPollos compra = (from _compra in dc.Tbl_CompraPollos where (_compra.FechaMatanza.Date == fecha.Date) select _compra).FirstOrDefault();
                if (compra != null)
                {
                    dg.Fecha = compra.FechaMatanza;
                    dg.NumeroLote1 = compra.Tbl_IngresoLotes.CodLote;
                    dg.TotalAves = (int)compra.TotalAvesConteoAutomatico;
                    dg.TotalAvesRemision = (int)compra.TotalAvesRemisionCompradas;
                    dg.TotalJavas = new Tbl_ViajesRemisionGranja_obj().getTotalJavasViajesRemisionxCompra(compra.ID_CompraBroilers);
                    dg.PesoPromedio = new Tbl_ViajesRemisionGranja_obj().getPesoPromedio(compra.ID_CompraBroilers);
                    dg.TotalLibrasPesadas = (decimal)compra.TotalLibrasCompradasCalculoBascula;
                    dg.IDCompra = (int)compra.ID_CompraBroilers;
                    dg.EdadDias = (int)new Tbl_ViajesRemisionGranja_obj().getEdadDiasPromedio(compra.ID_CompraBroilers);
                }
                return dg;

            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Obtiene una lista de procesos para la fecha seleccionada
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<datosGenerales> getResumenProcesosxFecha(DateTime fecha)
        {
            try
            {
                datosGenerales dg = new datosGenerales();
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<datosGenerales> compra = from _compra in dc.Tbl_CompraPollos
                                                    where (_compra.FechaMatanza.Date == fecha.Date)
                                                    select new datosGenerales
                                                    {
                                                        Fecha = _compra.FechaMatanza,
                                                        NumeroLote1 = _compra.Tbl_IngresoLotes.CodLote,
                                                        TotalAves = (int)_compra.TotalAvesConteoAutomatico,
                                                        TotalAvesRemision = (int)_compra.TotalAvesRemisionCompradas,
                                                        TotalJavas = new Tbl_ViajesRemisionGranja_obj().getTotalJavasViajesRemisionxCompra(_compra.ID_CompraBroilers),
                                                        PesoPromedio = new Tbl_ViajesRemisionGranja_obj().getPesoPromedio(_compra.ID_CompraBroilers),
                                                        TotalLibrasPesadas = (decimal)_compra.TotalLibrasCompradasCalculoBascula,
                                                        IDCompra = (int)_compra.ID_CompraBroilers,
                                                        EdadDias = (int)new Tbl_ViajesRemisionGranja_obj().getEdadDiasPromedio(_compra.ID_CompraBroilers)
                                                    };

                return compra.ToList();

            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// obtiene todas las compras para la fecha indicada
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public int getTotalComprasFecha(DateTime fecha)
        {
            try
            {
                datosGenerales dg = new datosGenerales();
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from _compra in dc.Tbl_CompraPollos where (_compra.FechaMatanza.Date == fecha.Date) select _compra).Count();

            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// Obtiene el resumen de la compra de pollo a una granja, se pasa el id de la compra de pollo que desea revisar
        /// </summary>
        /// <param name="IDCompraPollo">ID de la Compra que deseha revisar</param>
        /// <returns>datosGenerales Class</returns>
        public datosGenerales getResumenCompraPollosAGranja(int IDCompraPollo)
        {
            try
            {
                datosGenerales dg = new datosGenerales();
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_CompraPollos compra = (from _compra in dc.Tbl_CompraPollos where (_compra.ID_CompraBroilers == IDCompraPollo) select _compra).FirstOrDefault();
                if (compra != null)
                {
                    dg.Fecha = compra.FechaMatanza;
                    dg.NumeroLote1 = compra.Tbl_IngresoLotes.CodLote;
                    dg.TotalAves = (int)compra.TotalAvesConteoAutomatico;
                    dg.TotalAvesRemision = (int)compra.TotalAvesRemisionCompradas;
                    dg.TotalJavas = new Tbl_ViajesRemisionGranja_obj().getTotalJavasViajesRemisionxCompra(compra.ID_CompraBroilers);
                    dg.PesoPromedio = new Tbl_ViajesRemisionGranja_obj().getPesoPromedio(compra.ID_CompraBroilers);
                    dg.TotalLibrasPesadas = (decimal)compra.TotalLibrasCompradasCalculoBascula;
                    dg.IDCompra = (int)compra.ID_CompraBroilers;
                    dg.EdadDias = (int)new Tbl_ViajesRemisionGranja_obj().getEdadDiasPromedio(compra.ID_CompraBroilers);
                }
                return dg;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public class datosGenerales
        {
            private int iDCompra;

            public int IDCompra
            {
                get { return iDCompra; }
                set { iDCompra = value; }
            }
            private DateTime fecha;

            public DateTime Fecha
            {
                get { return fecha; }
                set { fecha = value; }
            }
            private string NumeroLote;

            public string NumeroLote1
            {
                get { return NumeroLote; }
                set { NumeroLote = value; }
            }
            private int totalAves;

            public int TotalAves
            {
                get { return totalAves; }
                set { totalAves = value; }
            }
            private int totalJavas;

            private int totalAvesRemision;

            public int TotalAvesRemision
            {
                get { return totalAvesRemision; }
                set { totalAvesRemision = value; }
            }


            public int TotalJavas
            {
                get { return totalJavas; }
                set { totalJavas = value; }
            }
            private decimal totalLibrasPesadas;

            public decimal TotalLibrasPesadas
            {
                get { return totalLibrasPesadas; }
                set { totalLibrasPesadas = value; }
            }

            private decimal pesoPromedio;

            public decimal PesoPromedio
            {
                get { return pesoPromedio; }
                set { pesoPromedio = value; }
            }
            private int edadDias;

            public int EdadDias
            {
                get { return edadDias; }
                set { edadDias = value; }
            }

        }
    }
}
