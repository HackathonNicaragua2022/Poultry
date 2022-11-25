using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class RegistroInsumoGranjaLote_obj
    {
        public string nuevo(Tbl_RegistroInsumosGranjaLote registroInsumoGranja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Tbl_RegistroInsumosGranjaLote.InsertOnSubmit(registroInsumoGranja);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<RegistroInsumoGranja_vista> getAll(int ID_InventarioBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<RegistroInsumoGranja_vista> result = from _insumosGalera in dc.Tbl_RegistroInsumosGranjaLote
                                                                where (_insumosGalera.ID_InventarioBroilers == ID_InventarioBroilers)
                                                                select new RegistroInsumoGranja_vista
                                                                {
                                                                    ID_InsumosGranjaEngorde = _insumosGalera.ID_InsumosGranjaEngorde,
                                                                    ID_InventarioBroilers = _insumosGalera.ID_InventarioBroilers,
                                                                    ID_RegistroInsumoGranja = _insumosGalera.ID_RegistroInsumoGranja,
                                                                    Registrado_Por = _insumosGalera.aspnet_Users.UserName,
                                                                    FechaRegistro = _insumosGalera.FechaRegistro,
                                                                    Concepto = _insumosGalera.Concepto,
                                                                    Cantidad = _insumosGalera.Cantidad,
                                                                    CostoUnid = _insumosGalera.CostoUnid,
                                                                    CostoTotal = _insumosGalera.CostoTotal,
                                                                    NombreInsumo = _insumosGalera.Tbl_InsumosGranjaEngorde.Cat_CategoriaInsumosGranjaEngorde.CategoriaInsumo + " / " + _insumosGalera.Tbl_InsumosGranjaEngorde.NombreInsumo + " (" + _insumosGalera.Tbl_InsumosGranjaEngorde.Cat_UnidadMedidaInsumoGranja.Simbolo + ")"
                                                                };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<RegistroInsumoGranja_vista>();
            }
        }
        public string eliminarInsumo(int ID_RegistroInsumoGranja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_RegistroInsumosGranjaLote insumo = (from _insumoGranja in dc.Tbl_RegistroInsumosGranjaLote where (_insumoGranja.ID_RegistroInsumoGranja == ID_RegistroInsumoGranja) select _insumoGranja).FirstOrDefault();
                dc.Tbl_RegistroInsumosGranjaLote.DeleteOnSubmit(insumo);
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
