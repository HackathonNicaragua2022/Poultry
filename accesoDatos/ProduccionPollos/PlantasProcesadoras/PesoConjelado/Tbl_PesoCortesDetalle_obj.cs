using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.PlantasProcesadoras.PesoConjelado
{
    public class Tbl_PesoCortesDetalle_obj
    {
        public List<PesoCortes> getAllPesoCortesByIDPesoFrio(int IdPesoFrio)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<PesoCortes> resultado = from _pesoCorte in dc.Tbl_PesoCortes_detalle
                                                   join _RubrosAves in dc.Tbl_RubrosAve on _pesoCorte.ID_RubrosAve equals _RubrosAves.ID_RubrosAve
                                                   join _EstadoRubro in dc.Tbl_EstadoRubro on _pesoCorte.ID_EstadoRubro equals _EstadoRubro.ID_EstadoRubro
                                                   select new PesoCortes
                                                   {
                                                       ID_PesoCortes = _pesoCorte.ID_PesoCortes,
                                                       ID_PresoFrio = _pesoCorte.ID_PresoFrio,
                                                       ID_RubrosAve = _pesoCorte.ID_RubrosAve,
                                                       ID_EstadoRubro = _pesoCorte.ID_EstadoRubro,
                                                       Nombre_Rubro = _RubrosAves.Producto,
                                                       Estado_Rubro = _EstadoRubro.Estado,
                                                       PesoTotalCorte_ECF = _pesoCorte.PesoTotalCorte_ECF,
                                                       PesoTotalCorte_SCF = _pesoCorte.PesoTotalCorte_SCF,
                                                       PesoTotalEnvioPlantaBodegaH = _pesoCorte.PesoTotalEnvioPlantaBodegaH,
                                                       PesoTotalReciboPlantaBodegaH = _pesoCorte.PesoTotalReciboPlantaBodegaH,
                                                       MermaCongelacion = _pesoCorte.MermaCongelacion,
                                                       MermaTraslado = _pesoCorte.MermaTraslado,
                                                       MermaTotalDespuesCongelacion = _pesoCorte.MermaTotalDespuesCongelacion
                                                   };
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error al cargar los pesos cortes con ID=" + IdPesoFrio + " \n" + ex.Message);
                return null;
            }
        }
    }
}
