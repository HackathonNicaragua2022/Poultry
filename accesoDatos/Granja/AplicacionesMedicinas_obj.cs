using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class AplicacionesMedicinas_obj
    {
        public string nuevo(Tbl_AplicacionesMedicinas nuevaAplicacionMedicina)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Tbl_AplicacionesMedicinas.InsertOnSubmit(nuevaAplicacionMedicina);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<AplicacionesMedicinas_vista> getAll(int ID_InventarioBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<AplicacionesMedicinas_vista> result = from _aplicacionesMedicas in dc.Tbl_AplicacionesMedicinas
                                                                 where (_aplicacionesMedicas.ID_InventarioBroilers == ID_InventarioBroilers)
                                                                 select new AplicacionesMedicinas_vista
                                                                 {
                                                                     ID_AplicacionesMedicas = _aplicacionesMedicas.ID_AplicacionesMedicas,
                                                                     ID_InsumosGranjaEngorde = _aplicacionesMedicas.ID_InsumosGranjaEngorde,
                                                                     ID_InventarioBroilers = _aplicacionesMedicas.ID_InventarioBroilers,
                                                                     NombreInsumo = _aplicacionesMedicas.Tbl_InsumosGranjaEngorde.NombreInsumo,
                                                                     Registrado_por = _aplicacionesMedicas.aspnet_Users.UserName,
                                                                     RegistradoPor = _aplicacionesMedicas.RegistradoPor,
                                                                     FechaRegistroSistema = _aplicacionesMedicas.FechaRegistroSistema,
                                                                     Numero_de_AplicacionDosis = _aplicacionesMedicas.Numero_de_AplicacionDosis,
                                                                     Frascos = _aplicacionesMedicas.Frascos,
                                                                     DosisxFrasco = _aplicacionesMedicas.DosisxFrasco,
                                                                     TotalDosis = _aplicacionesMedicas.TotalDosis,
                                                                     Concepto = _aplicacionesMedicas.Concepto,
                                                                     CostoxFrasco = _aplicacionesMedicas.CostoxFrasco,
                                                                     CostoTotal = _aplicacionesMedicas.CostoTotal,
                                                                     CostoActualizado = _aplicacionesMedicas.CostoActualizado,
                                                                     UltimoCosto = _aplicacionesMedicas.UltimoCosto,
                                                                     Anulado = _aplicacionesMedicas.Anulado
                                                                 };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<AplicacionesMedicinas_vista>();
            }
        }
        public string delete(int ID_AplicacionesMedicas)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_AplicacionesMedicinas aplicacionesMedicas = (from _aplicacionesMedicas in dc.Tbl_AplicacionesMedicinas where (_aplicacionesMedicas.ID_AplicacionesMedicas == ID_AplicacionesMedicas) select _aplicacionesMedicas).FirstOrDefault();
                dc.Tbl_AplicacionesMedicinas.DeleteOnSubmit(aplicacionesMedicas);
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
