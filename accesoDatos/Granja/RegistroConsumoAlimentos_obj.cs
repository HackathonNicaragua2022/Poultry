using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class RegistroConsumoAlimentos_obj
    {
        public string Nuevo(Tbl_ControlAlimenticio NuevoControlA)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Tbl_ControlAlimenticio.InsertOnSubmit(NuevoControlA);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public decimal ultimoInventarioFinal(int IDInventarioBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                decimal UltimoIF = (decimal)(from controlAlimentcion in dc.Tbl_ControlAlimenticio
                                             where (controlAlimentcion.ID_InventarioBroilers == IDInventarioBroilers)
                                             orderby controlAlimentcion.FechaRegistro descending
                                             select controlAlimentcion).FirstOrDefault().InventarioFinal;
                return UltimoIF;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DateTime ultimaFechaInventarioFinal(int IDInventarioBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                DateTime UltimoFIF = (DateTime)(from controlAlimentcion in dc.Tbl_ControlAlimenticio where (controlAlimentcion.ID_InventarioBroilers == IDInventarioBroilers) orderby controlAlimentcion.FechaHoraRegistroConsumo_fin descending select controlAlimentcion).FirstOrDefault().FechaHoraRegistroConsumo_fin;
                return UltimoFIF;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        public string eliminarRegistro(int IDControlAlimento)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_ControlAlimenticio controlAlimenticio = (from controAlimenticio in dc.Tbl_ControlAlimenticio where (controAlimenticio.Id_ControlAlimenticio == IDControlAlimento) select controAlimenticio).First();
                dc.Tbl_ControlAlimenticio.DeleteOnSubmit(controlAlimenticio);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<USP_ResumenConsumoAlimentoLoteResult> getResumenConsumoAlimentoLote(int IDInventarioBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_ResumenConsumoAlimentoLote(IDInventarioBroilers).ToList();
            }
            catch (Exception)
            {
                return new List<USP_ResumenConsumoAlimentoLoteResult>();
            }
        }
        public string getPesoGramos(int IDParametroDiario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from parametrosD in dc.Tbl_ParametrosDiarios where (parametrosD.ID_ParametrosDiarios == IDParametroDiario) select parametrosD).FirstOrDefault().Peso_Promedio.ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}
