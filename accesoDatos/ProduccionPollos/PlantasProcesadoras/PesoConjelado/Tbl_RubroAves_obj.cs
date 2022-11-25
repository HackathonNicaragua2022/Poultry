using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.PlantasProcesadoras.PesoConjelado
{
    public class Tbl_RubroAves_obj
    {
        private static Random random = new Random();


        /// <summary>
        /// Obtiene todos los Rubros del sistema
        /// </summary>
        /// <returns></returns>
        public List<Tbl_RubrosAve> getAllRubros()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from ra in dc.Tbl_RubrosAve select ra).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Genera un codigo para ser usado en barra impresas, devuelve el codigo en caso
        /// de no encontrar existencia en el sistema, de los contrario devuelve -1 en caso de error
        /// </summary>
        /// <returns></returns>
        public string generarCodigoRubro()
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                bool Exist = true;
                string codigo = "-1";
                while (Exist)
                {
                    codigo = RandomString(4);
                    if (!dc.Tbl_RubrosAve.Any(a => a.Codigos.Equals(codigo)))
                    {
                        Exist = false;
                        break;
                    }
                }
                return codigo;
            }
            catch (Exception)
            {
                return "-1";
            }
        }
        /// <summary>
        /// Verificar el codigo del rubro ingresado en la base de datos
        /// devuelve true en caso de encontrar el codido en la base de datos
        /// </summary>
        /// <returns></returns>
        public Boolean verificarCodigoRubro(String codigo)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                if (dc.Tbl_RubrosAve.Any(a => a.Codigos.Equals(codigo)))
                    return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Guarda el Rubro en el sistema sin verifivar ni validar
        /// </summary>
        /// <param name="rubro"></param>
        /// <returns></returns>
        public String guardarNuevoRubro(Tbl_RubrosAve rubro)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Tbl_RubrosAve.InsertOnSubmit(rubro);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Modifica un rubro identificado por el ID
        /// </summary>
        /// <param name="rubro_cambio"></param>
        /// <returns></returns>
        public String modificar_Rubro(Tbl_RubrosAve rubro_cambio)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                Tbl_RubrosAve rubroEditar = dc.Tbl_RubrosAve.Where(a => a.ID_RubrosAve == rubro_cambio.ID_RubrosAve).FirstOrDefault();
                if(rubroEditar!=null){
                    rubroEditar.Codigos = rubro_cambio.Codigos;
                    rubroEditar.Producto = rubro_cambio.Producto;
                    rubroEditar.Siglas = rubro_cambio.Siglas;
                    rubroEditar.VicerasComestibles = rubro_cambio.VicerasComestibles;
                    rubroEditar.EsTitil= rubro_cambio.EsTitil;
                    rubroEditar.EsHigado = rubro_cambio.EsHigado;                    
                    dc.SubmitChanges();
                    return "1";
                }
                return "Error, no se encotro el Rubro con el ID= " + rubroEditar;
            }
            catch (Exception ex)
            {
                return "Error Modificando el rubro. Error: "+ex.Message;
            }
        }

        /// <summary>
        /// Elimina de la base de datos un registro con el ID Especificado, devuelve 1 en caso de exito en caso contrario devuelve el valor del error 
        /// </summary>
        /// <param name="idRubro"></param>
        /// <returns>String</returns>
        public String Eliminar_Rubro(int idRubro)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                Tbl_RubrosAve rubroEditar = dc.Tbl_RubrosAve.Where(a => a.ID_RubrosAve == idRubro).FirstOrDefault();
                
                if (rubroEditar != null){
                    dc.Tbl_RubrosAve.DeleteOnSubmit(rubroEditar);
                    dc.SubmitChanges();
                    return "1";
                }
                return "Error al Eliminar, no se encontro el Rubro con el ID= " + rubroEditar;
            }
            catch (Exception ex)
            {
                return "Error al eliminar el rubro. Error: " + ex.Message;
            }
        }


        /// <summary>
        /// Obtiene el rubro con Id 
        /// </summary>
        /// <param name="ID_RubroAve"></param>
        /// <returns></returns>
        public Tbl_RubrosAve getRubroAveById(int ID_RubroAve)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.Tbl_RubrosAve.Where(a => a.ID_RubrosAve == ID_RubroAve).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
