using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class InventarioGaleras_obj
    {
        /// <summary>
        /// Obtiene el Inventario de la Galera en Produccion
        /// </summary>
        /// <param name="ID_Galera"></param>
        /// <returns></returns>
        public Tbl_InventarioGalera get_InventarioGalera(int ID_Galera)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_InventarioGalera result = (from inventarioGalera in dc.Tbl_InventarioGalera
                                               where (inventarioGalera.ID_Galeras == ID_Galera && inventarioGalera.InventarioActivo == true)
                                               select inventarioGalera).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return new Tbl_InventarioGalera();
            }
        }
        public List<InventarioGaleras_vistaParametros> getInventariosRel()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<InventarioGaleras_vistaParametros> result = from inventarioGalera in dc.Tbl_InventarioGalera
                                                                       where (inventarioGalera.InventarioActivo == true)
                                                                       select new InventarioGaleras_vistaParametros
                                                                       {
                                                                           ID_InventarioBroilers = inventarioGalera.ID_InventarioBroilers,
                                                                           ID_Galeras = inventarioGalera.ID_Galeras,
                                                                           NombreGalera = inventarioGalera.Tbl_Galeras.NumeroOrden.ToString(),
                                                                           TotalIngreso = inventarioGalera.TotalIngreso,
                                                                           TotalMortalidad = inventarioGalera.TotalMortalidad,
                                                                           TotalEnPie = inventarioGalera.TotalEnPie,
                                                                           TotalSalidas_RemisionesMatadero = inventarioGalera.TotalSalidas_RemisionesMatadero,
                                                                           EdadLote_Dias = inventarioGalera.EdadLote_Dias,
                                                                           EdadLote_Semanas = inventarioGalera.EdadLote_Semanas,
                                                                           ID_Lote = inventarioGalera.ID_Lote,
                                                                           InventarioActivo = inventarioGalera.InventarioActivo,
                                                                           PesoPromedio = inventarioGalera.PesoPromedio,
                                                                           TotalLibrasPesoVivoMatanza = inventarioGalera.TotalLibrasPesoVivoMatanza,
                                                                           Fecha_IngresoGalera = inventarioGalera.Fecha_IngresoGalera,
                                                                           PesoPromedioLibras = (double)inventarioGalera.PesoPromedio / 453.59
                                                                       };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<InventarioGaleras_vistaParametros>();
            }
        }

        /// <summary>
        /// Obtiene todos los lotes de la Granja segun su ID
        /// </summary>
        /// <returns></returns>
        public List<Tbl_IngresoLotes> getInventariosActivos(int IDGranja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_IngresoLotes> result = from LotesActivo in dc.Tbl_InventarioGalera
                                                      join ingresoLote in dc.Tbl_IngresoLotes on LotesActivo.ID_Lote equals ingresoLote.ID_Lote
                                                      where (LotesActivo.Tbl_Galeras.Tbl_Granjas.ID_Granjas == IDGranja && LotesActivo.InventarioActivo == true)
                                                      select ingresoLote;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_IngresoLotes>();
            }
        }
        /// <summary>
        /// Obtiene el ID Del Lote ligado al INventario seleccionado por el ID
        /// </summary>
        /// <param name="IDInventarioLote"></param>
        /// <returns></returns>
        public int getIDLotexIDInventario(int ID_InventarioBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_InventarioGalera InventarioGalera = (from _InventarioGalera in dc.Tbl_InventarioGalera where _InventarioGalera.ID_InventarioBroilers == ID_InventarioBroilers select _InventarioGalera).FirstOrDefault();
                return InventarioGalera.Tbl_IngresoLotes.ID_Lote;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
