using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class GaleriaImagenesPorVehiculo
    {
        public string subirImagen(String Placa, String URL, Guid Usuario, String Descripcion)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_DetallesEsteticos nuevoDetalle = new Tbl_DetallesEsteticos();
                nuevoDetalle.Placa = Placa;
                nuevoDetalle.URLImagen = URL;
                nuevoDetalle.DescripcionEstetica = Descripcion;
                nuevoDetalle.FechaGrupoImagenes = DateTime.Now;
                nuevoDetalle.CreadoPor = Usuario;
                nuevoDetalle.Eliminada = false;
                dc.Tbl_DetallesEsteticos.InsertOnSubmit(nuevoDetalle);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<Tbl_DetallesEsteticos> getAll(string placa)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_DetallesEsteticos> resul = from detalles in dc.Tbl_DetallesEsteticos where (detalles.Placa.Equals(placa) && !(bool)detalles.Eliminada) select detalles;
                return resul.ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_DetallesEsteticos>();
            }
        }

        public string eliminarImagenDetalle(int IDDetalleEstetico)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_DetallesEsteticos detalleAEliminar = (from detalleE in dc.Tbl_DetallesEsteticos where (detalleE.ID_DetallesEsteticos == IDDetalleEstetico) select detalleE).FirstOrDefault();
                if (detalleAEliminar != null)
                {
                    detalleAEliminar.Eliminada = true;
                    detalleAEliminar.FechaEliminacion = DateTime.Now;
                    dc.SubmitChanges();
                    return "1";
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
