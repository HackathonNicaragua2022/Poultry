using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Catalogos
{
    public class Cat_MedidaProducto_obj
    {
        public List<Cat_MedidaProducto> getAllMedidaProducto()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from medida in dc.Cat_MedidaProducto select medida).ToList();
            }
            catch (Exception)
            {
                return new List<Cat_MedidaProducto>();
            }
        }
        public string guardarMedidaProducto(Cat_MedidaProducto nuevaMedida)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Cat_MedidaProducto.InsertOnSubmit(nuevaMedida);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al guardar, no se ha podido guardar la Medida del Producto: " + ex.Message;
            }
        }
        public string EliminarMedidaProducto(int IdMedidaProducto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_MedidaProducto MedidaProductoElim = (from p in dc.Cat_MedidaProducto where (p.ID_Medida == IdMedidaProducto) select p).First();
                dc.Cat_MedidaProducto.DeleteOnSubmit(MedidaProductoElim);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la Medida del Producto " + ex.Message;
            }
        }
        public Cat_MedidaProducto findById(int idMedidaProducto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.Cat_MedidaProducto where (p.ID_Medida== idMedidaProducto) select p).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool findByName(string nombreC)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //(from p in dc.Cat_MedidaProducto where (p.NombreMedidaProducto == idNombre) select p).First();
                return dc.Cat_MedidaProducto.Any(a => a.MedidaProducto.Equals(nombreC));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string updateMedidaProducto(Cat_MedidaProducto MedidaProductoMod, int idMedidaProducto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_MedidaProducto medidaP = (from p in dc.Cat_MedidaProducto where (p.ID_Medida == idMedidaProducto) select p).First();
                medidaP.MedidaProducto= MedidaProductoMod.MedidaProducto;
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al actualizar " + ex.Message;
            }
        }
    }
}
