using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using accesoDatos;
using accesoDatos.Granja;

namespace POULTRY .GranjaSanFrancisco
{
    public class Tbl_InsumoGranjaEngorde_obj
    {
        public List<InsumoGranjaEncorde_vista> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<InsumoGranjaEncorde_vista> result = from _insumos in dc.Tbl_InsumosGranjaEngorde
                                                               select new InsumoGranjaEncorde_vista
                                                               {
                                                                   ID_InsumosGranjaEngorde = _insumos.ID_InsumosGranjaEngorde,
                                                                   ID_CategoriaInsumosGranjaEngorde = _insumos.ID_CategoriaInsumosGranjaEngorde,
                                                                   ID_unidadMedida = _insumos.ID_unidadMedida,
                                                                   NombreInsumo = _insumos.NombreInsumo,
                                                                   NombreCategoria = _insumos.Cat_CategoriaInsumosGranjaEngorde.CategoriaInsumo,
                                                                   NombreMedida = _insumos.Cat_UnidadMedidaInsumoGranja.UnidadMedidaInsumo
                                                               };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<InsumoGranjaEncorde_vista>();
            }
        }
        public List<InsumoGranjaEncorde_vista> getAllByIdCategoria(int IDCategoria)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<InsumoGranjaEncorde_vista> result = from _insumos in dc.Tbl_InsumosGranjaEngorde
                                                               where (_insumos.ID_CategoriaInsumosGranjaEngorde == IDCategoria)
                                                               select new InsumoGranjaEncorde_vista
                                                               {
                                                                   ID_InsumosGranjaEngorde = _insumos.ID_InsumosGranjaEngorde,
                                                                   ID_CategoriaInsumosGranjaEngorde = _insumos.ID_CategoriaInsumosGranjaEngorde,
                                                                   ID_unidadMedida = _insumos.ID_unidadMedida,
                                                                   NombreInsumo = _insumos.NombreInsumo + " (" + _insumos.Cat_UnidadMedidaInsumoGranja.Simbolo+")"
                                                               };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<InsumoGranjaEncorde_vista>();
            }
        }
        public string nuevoInsumo(int IDCategoria, int IDUnidadMedida, string NombreInsumo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_InsumosGranjaEngorde nuevo = new Tbl_InsumosGranjaEngorde();
                nuevo.ID_CategoriaInsumosGranjaEngorde = IDCategoria;
                nuevo.ID_unidadMedida = IDUnidadMedida;
                nuevo.NombreInsumo = NombreInsumo;
                dc.Tbl_InsumosGranjaEngorde.InsertOnSubmit(nuevo);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string editar(int IDInsumoGranja, String NombreInsumo, int IDCategoria, int IDMedida)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_InsumosGranjaEngorde insumoEditar = (from _insumoGranja in dc.Tbl_InsumosGranjaEngorde where (_insumoGranja.ID_InsumosGranjaEngorde == IDInsumoGranja) select _insumoGranja).FirstOrDefault();
                insumoEditar.NombreInsumo = NombreInsumo;
                insumoEditar.ID_unidadMedida = IDMedida;
                insumoEditar.ID_CategoriaInsumosGranjaEngorde = IDCategoria;
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string delete(int IDInsumoGranja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_InsumosGranjaEngorde insumoEditar = (from _insumoGranja in dc.Tbl_InsumosGranjaEngorde where (_insumoGranja.ID_InsumosGranjaEngorde == IDInsumoGranja) select _insumoGranja).FirstOrDefault();
                dc.Tbl_InsumosGranjaEngorde.DeleteOnSubmit(insumoEditar);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Tbl_InsumosGranjaEngorde getByID(int IDInsumoGranja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from _insumoGranja in dc.Tbl_InsumosGranjaEngorde where (_insumoGranja.ID_InsumosGranjaEngorde == IDInsumoGranja) select _insumoGranja).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}