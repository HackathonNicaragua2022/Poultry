using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class AplicacionesTratamientos_obj
    {
        public string nuevo(Tbl_AplicacionesTratamientos nuevoAplicacionTratamiento)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Tbl_AplicacionesTratamientos.InsertOnSubmit(nuevoAplicacionTratamiento);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string delete(int IDTratamiento)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_AplicacionesTratamientos tratamiento = (from _tratamiento in dc.Tbl_AplicacionesTratamientos where _tratamiento.ID_AplicacionesTratamientos == IDTratamiento select _tratamiento).FirstOrDefault();
                dc.Tbl_AplicacionesTratamientos.DeleteOnSubmit(tratamiento);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<AplicacionesTratamientos_vista> getAll(int IDLoteProduccion)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<AplicacionesTratamientos_vista> result = from _aplicacionesTratamientos in dc.Tbl_AplicacionesTratamientos
                                                                    where _aplicacionesTratamientos.ID_InventarioBroilers == IDLoteProduccion
                                                                    select new AplicacionesTratamientos_vista
                                                                    {
                                                                        ID_AplicacionesTratamientos = _aplicacionesTratamientos.ID_AplicacionesTratamientos,
                                                                        ID_InventarioBroilers = _aplicacionesTratamientos.ID_InventarioBroilers,
                                                                        ID_InsumosGranjaEngorde = _aplicacionesTratamientos.ID_InsumosGranjaEngorde,
                                                                        RegistradoPor = _aplicacionesTratamientos.RegistradoPor,
                                                                        FechaRegistroSistema = _aplicacionesTratamientos.FechaRegistroSistema,
                                                                        FechaInicioDosis = _aplicacionesTratamientos.FechaInicioDosis,
                                                                        FechaFinDosis = _aplicacionesTratamientos.FechaFinDosis,
                                                                        TotalDiasDosis = _aplicacionesTratamientos.TotalDiasDosis,
                                                                        Aplicacion = _aplicacionesTratamientos.Aplicacion,
                                                                        Cantidad = _aplicacionesTratamientos.Cantidad,
                                                                        CostoMinimo = _aplicacionesTratamientos.CostoMinimo,
                                                                        CostoTotal = _aplicacionesTratamientos.CostoTotal,
                                                                        CostoActualizado = _aplicacionesTratamientos.CostoActualizado,
                                                                        UltimoCosto = _aplicacionesTratamientos.UltimoCosto,
                                                                        Anulado = _aplicacionesTratamientos.Anulado,
                                                                        NombreRegistradoPor = _aplicacionesTratamientos.aspnet_Users.UserName,
                                                                        NombreInsumo = _aplicacionesTratamientos.Tbl_InsumosGranjaEngorde.NombreInsumo
                                                                    };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<AplicacionesTratamientos_vista>();
            }
        }
    }
}
