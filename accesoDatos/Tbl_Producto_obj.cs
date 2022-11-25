using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class Tbl_Producto_obj
    {
        public List<Tbl_Producto> getAllProducto()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from producto in dc.Tbl_Producto select producto).ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_Producto>();
            }
        }
        public List<getAllProdcutosResult> getAllProdcutosResult_USP(bool activos)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.getAllProdcutos(activos).ToList();
            }
            catch (Exception)
            {
                return new List<getAllProdcutosResult>();
            }
        }

        public string guardarProducto(Tbl_Producto nuevoPro)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!this.Existe(nuevoPro.NombreProducto))
                {
                    dc.Tbl_Producto.InsertOnSubmit(nuevoPro);
                    dc.SubmitChanges();
                    Tbl_Inventario inv = new Tbl_Inventario();
                    inv.ID_Producto = nuevoPro.ID_Producto;
                    inv.CantidadEntrante = 0;
                    inv.CantidadSaliente = 0;
                    inv.CantidadPerdida = 0;
                    inv.CantidadDanada = 0;
                    inv.CantidadVencida = 0;
                    inv.CostoCompra = 0;
                    inv.PrecioVenta = 0;
                    inv.HabilitadoParaVenta = true;
                    dc.Tbl_Inventario.InsertOnSubmit(inv);
                    dc.SubmitChanges();
                }
                else
                {
                    throw new Exception("El Producto " + nuevoPro.NombreProducto + ", ya existen en el sistema");
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al guardar, no se ha podido guardar el nuevo producto: " + ex.Message;
            }
        }
        public string EliminarProducto(int IdProducto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Producto prodElim = (from p in dc.Tbl_Producto where (p.ID_Producto == IdProducto) select p).First();
                dc.Tbl_Producto.DeleteOnSubmit(prodElim);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el producto " + ex.Message;
            }
        }
        public Tbl_Producto findById(int idProd)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.Tbl_Producto where (p.ID_Producto == idProd) select p).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Existe(String NombreP)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Tbl_Producto.Any(a => a.NombreProducto.Equals(NombreP)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Tbl_Producto> findByIdCategoria(int idCategoria)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return ((from p in dc.Tbl_Producto where (p.ID_CategoriaProducto == idCategoria) select p)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<getProdcutosxCategoriaComedorResult> findByIdCategoriaParaComedor(int idCategoria)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.getProdcutosxCategoriaComedor(idCategoria).ToList();
            }
            catch (Exception)
            {
                return new List<getProdcutosxCategoriaComedorResult>();
            }
        }





        public string updateProducto(Tbl_Producto prod, int idPro)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Producto prUp = (from p in dc.Tbl_Producto where (p.ID_Producto == idPro) select p).First();
                prUp.ID_CategoriaProducto = prod.ID_CategoriaProducto;
                prUp.ID_Medida = prod.ID_Medida;
                prUp.NombreProducto = prod.NombreProducto;
                prUp.Activo = prod.Activo;
                prUp.URLImagen = prod.URLImagen;
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
