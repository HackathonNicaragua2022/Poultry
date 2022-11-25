using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Catalogos
{
    public class Cat_CategoriaProducto_obj
    {
        /// <summary>
        /// Obtiene todas las categorias registradas en el sistema de la base de datos
        /// </summary>
        /// <returns></returns>
        public List<Cat_CategoriaProducto> getAllCategoriaProducto()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from categoriaproduc in dc.Cat_CategoriaProducto select categoriaproduc).ToList();
            }
            catch (Exception)
            {
                return new List<Cat_CategoriaProducto>();
            }
        }
        /// <summary>
        /// Guardar una nueva categoria en la base de datos
        /// </summary>
        /// <param name="nuevaCategoria_Producto"></param>
        /// <returns></returns>
        public string guardarCategoria_Producto(Cat_CategoriaProducto nuevaCategoria_Producto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Cat_CategoriaProducto.InsertOnSubmit(nuevaCategoria_Producto);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al guardar, no se ha podido guardar la Categoria: " + ex.Message;
            }
        }
        /// <summary>
        /// Elimina una categoria de la base de datos
        /// </summary>
        /// <param name="IdCategoria_Producto"></param>
        /// <returns></returns>
        public string EliminarCategoria_Producto(int IdCategoria_Producto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_CategoriaProducto Categoria_ProductoElim = (from p in dc.Cat_CategoriaProducto where (p.ID_CategoriaProducto == IdCategoria_Producto) select p).First();
                dc.Cat_CategoriaProducto.DeleteOnSubmit(Categoria_ProductoElim);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la Categoria Producto " + ex.Message;
            }
        }
        /// <summary>
        /// Obtiene una categoria por ID
        /// </summary>
        /// <param name="idCategoria_Producto"></param>
        /// <returns></returns>
        public Cat_CategoriaProducto findById(int idCategoria_Producto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.Cat_CategoriaProducto where (p.ID_CategoriaProducto == idCategoria_Producto) select p).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Busca por nombre de la categoria y devuelve un valor booleano
        /// </summary>
        /// <param name="nombreC"></param>
        /// <returns></returns>
        public bool findByName(string nombreC)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //(from p in dc.Cat_CategoriaProducto where (p.NombreCategoria_Producto == idNombre) select p).First();
                return dc.Cat_CategoriaProducto.Any(a => a.NombreCategoriaProducto.Equals(nombreC));
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// actualiza la categoria con id
        /// </summary>
        /// <param name="Categoria_ProductoMod"></param>
        /// <param name="idCategoria_Producto"></param>
        /// <returns></returns>
        public string updateCategoria_Producto(Cat_CategoriaProducto Categoria_ProductoMod, int idCategoria_Producto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_CategoriaProducto areUp = (from p in dc.Cat_CategoriaProducto where (p.ID_CategoriaProducto == idCategoria_Producto) select p).First();
                areUp.NombreCategoriaProducto = Categoria_ProductoMod.NombreCategoriaProducto;
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
