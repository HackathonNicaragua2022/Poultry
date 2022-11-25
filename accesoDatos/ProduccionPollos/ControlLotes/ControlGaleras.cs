using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.ControlLotes
{
    public class ControlGaleras
    {
        public Tbl_Galeras getGalera(int IdGalera)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Galeras reslut = (from _galera in dc.Tbl_Galeras where (_galera.ID_Galeras == IdGalera) select _galera).FirstOrDefault();
                System.Console.WriteLine(reslut.ID_Galeras);
                return reslut;
            }
            catch (Exception)
            {
                return new Tbl_Galeras();
            }
        }

        /// <summary>
        /// Obtiene una lista de todas las galeras que no estan en produccion seleccionadas por idGranja
        /// </summary>
        /// <param name="IdGranja"></param>
        /// <returns></returns>
        public List<Tbl_gelera_vista> getAllGalerasxGranja(int IdGranja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_gelera_vista> result = from _galeras in dc.Tbl_Galeras
                                                      where (_galeras.ID_Granjas == IdGranja && _galeras.EnProduccion == false)
                                                      select new Tbl_gelera_vista
                                                      {
                                                          ID_Galeras = _galeras.ID_Galeras,
                                                          ID_Granjas = _galeras.ID_Granjas,
                                                          NumeroOrden = _galeras.NumeroOrden,
                                                          NombreApodo = _galeras.NombreApodo,
                                                          CapacidadInstalada = _galeras.CapacidadInstalada,
                                                          CapacidadNormal = _galeras.CapacidadNormal,
                                                          CreadoPor = _galeras.CreadoPor,
                                                          EnProduccion = _galeras.EnProduccion
                                                      };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_gelera_vista>();
            }
        }

        /// <summary>
        /// Obtiene todas las galeras que no esten en produccion segun la granja
        /// </summary>
        /// <param name="IdGranja"></param>
        /// <returns></returns>
        public List<Tbl_gelera_vista> getAllGalerasxGranjaSinProduccion(int IdGranja, bool EnProduccion)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_gelera_vista> result = from _galeras in dc.Tbl_Galeras
                                                      join inventarioGalera in dc.Tbl_InventarioGalera on _galeras.ID_Galeras equals inventarioGalera.ID_Galeras
                                                      where (_galeras.ID_Granjas == IdGranja && _galeras.EnProduccion == EnProduccion && inventarioGalera.InventarioActivo == true)
                                                      select new Tbl_gelera_vista
                                                      {
                                                          ID_Galeras = _galeras.ID_Galeras,
                                                          ID_Granjas = _galeras.ID_Granjas,
                                                          NumeroOrden = _galeras.NumeroOrden,
                                                          NombreApodo = _galeras.NombreApodo,
                                                          CapacidadInstalada = _galeras.CapacidadInstalada,
                                                          CapacidadNormal = _galeras.CapacidadNormal,
                                                          CreadoPor = _galeras.CreadoPor,
                                                          EnProduccion = _galeras.EnProduccion,
                                                          TotalIngreso = inventarioGalera.TotalIngreso,
                                                          RendimientoGalera = ((decimal)inventarioGalera.TotalIngreso / (decimal)_galeras.CapacidadInstalada) * 100,
                                                          PolloEnPie = (int)inventarioGalera.TotalEnPie,
                                                          PromedioEnPie = ((decimal)inventarioGalera.TotalEnPie / (decimal)inventarioGalera.TotalIngreso) * 100,
                                                          Mortalidad = (int)inventarioGalera.TotalMortalidad,
                                                          PorcenjateMortalidad = (decimal)inventarioGalera.TotalMortalidad / (decimal)inventarioGalera.TotalIngreso * 100,
                                                          PorcentajeViabilidadLote = (decimal)(inventarioGalera.TotalIngreso - inventarioGalera.TotalMortalidad) / (decimal)inventarioGalera.TotalIngreso * 100
                                                      };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_gelera_vista>();
            }
        }


        public Tbl_gelera_vista get_GalerasxGranjaEnProduccion(int ID_Galera)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_gelera_vista result = (from _galeras in dc.Tbl_Galeras
                                           join inventarioGalera in dc.Tbl_InventarioGalera on _galeras.ID_Galeras equals inventarioGalera.ID_Galeras
                                           where (_galeras.ID_Galeras == ID_Galera && _galeras.EnProduccion == true && inventarioGalera.InventarioActivo == true)
                                           select new Tbl_gelera_vista
                                           {
                                               ID_Galeras = _galeras.ID_Galeras,
                                               ID_Granjas = _galeras.ID_Granjas,
                                               NumeroOrden = _galeras.NumeroOrden,
                                               NombreApodo = _galeras.NombreApodo,
                                               CapacidadInstalada = _galeras.CapacidadInstalada,
                                               CapacidadNormal = _galeras.CapacidadNormal,
                                               CreadoPor = _galeras.CreadoPor,
                                               EnProduccion = _galeras.EnProduccion,
                                               TotalIngreso = inventarioGalera.TotalIngreso,
                                               RendimientoGalera = (((decimal)inventarioGalera.TotalIngreso / (decimal)_galeras.CapacidadInstalada) * 100),
                                               PolloEnPie = (int)inventarioGalera.TotalEnPie,
                                               PromedioEnPie = (((decimal)inventarioGalera.TotalEnPie / (decimal)inventarioGalera.TotalIngreso) * 100)
                                           }).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return new Tbl_gelera_vista();
            }
        }


        /// <summary>
        /// Use para mostrar las galeras con variables adicionales a la tabla adicional
        /// </summary>
        public class Tbl_gelera_vista : Tbl_Galeras
        {
            private int polloEnPie;

            public int PolloEnPie
            {
                get { return polloEnPie; }
                set { polloEnPie = value; }
            }
            private decimal promedioEnPie;

            public decimal PromedioEnPie
            {
                get { return promedioEnPie; }
                set { promedioEnPie = value; }
            }
            private int totalIngreso;

            public int TotalIngreso
            {
                get { return totalIngreso; }
                set { totalIngreso = value; }
            }
            private decimal rendimientoGalera;

            public decimal RendimientoGalera
            {
                get { return rendimientoGalera; }
                set { rendimientoGalera = value; }
            }
            private int mortalidad;

            public int Mortalidad
            {
                get { return mortalidad; }
                set { mortalidad = value; }
            }
            private decimal porcenjateMortalidad;

            public decimal PorcenjateMortalidad
            {
                get { return porcenjateMortalidad; }
                set { porcenjateMortalidad = value; }
            }
            private decimal porcentajeViabilidadLote;

            public decimal PorcentajeViabilidadLote
            {
                get { return porcentajeViabilidadLote; }
                set { porcentajeViabilidadLote = value; }
            }
        }
    }
}
