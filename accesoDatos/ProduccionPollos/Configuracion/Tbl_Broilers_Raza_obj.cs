using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.Configuracion
{
    public class Tbl_Broilers_Raza_obj
    {
        /// <summary>
        /// Crea una nueva raza de Broilers en la Base De Datos
        /// </summary>
        /// <param name="nombreRaza"></param>
        /// <param name="TotalDiasParaProduccion"></param>
        /// <param name="PesoIdealParaProduccion"></param>
        /// <returns></returns>
        public string nuevaRazaBroilers(string nombreRaza, int TotalDiasParaProduccion, decimal PesoIdealParaProduccion)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Broilers_Raza nuevaRaza = new Tbl_Broilers_Raza();
                nuevaRaza.NombreRaza = nombreRaza;
                nuevaRaza.TotalEdadDias_IdealProduccion = TotalDiasParaProduccion;
                nuevaRaza.PesoIdealparaProduccion = PesoIdealParaProduccion;
                dc.Tbl_Broilers_Raza.InsertOnSubmit(nuevaRaza);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Obtiene todos los Broilers en la Base de Datos
        /// </summary>
        /// <returns></returns>
        public List<Tbl_Broilers_Raza> getAllRazaBroilers()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_Broilers_Raza> result = from broilers in dc.Tbl_Broilers_Raza select broilers;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_Broilers_Raza>();
            }
        }
        /// <summary>
        /// Obtiene el total de dias ideal para matanza
        /// </summary>
        /// <returns></returns>
        public int getDiasIdealLiquidar(int IDRaza)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                int result = (int)(from broilers in dc.Tbl_Broilers_Raza where (broilers.ID_Broilers_Raza == IDRaza) select broilers).FirstOrDefault().TotalEdadDias_IdealProduccion;
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
