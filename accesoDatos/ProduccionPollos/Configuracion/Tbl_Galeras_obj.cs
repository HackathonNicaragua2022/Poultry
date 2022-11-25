using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.Configuracion
{
    public class Tbl_Galeras_obj
    {
        /// <summary>
        /// Crea una nueva Galera con la granja seleccionada pasa por iD
        /// </summary>
        /// <param name="IDGranja"></param>
        /// <param name="NumeroOrden"></param>
        /// <param name="NombreApodo"></param>
        /// <param name="CapacidadInstalada"></param>
        /// <param name="CapacidadNormal"></param>
        /// <param name="creadoPor"></param>
        /// <returns></returns>
        public string NuevaGalera(int IDGranja, int NumeroOrden, string NombreApodo, int CapacidadInstalada, int CapacidadNormal, Guid creadoPor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Galeras nGalera = new Tbl_Galeras();
                nGalera.ID_Granjas = IDGranja;
                nGalera.NumeroOrden = NumeroOrden;
                nGalera.NombreApodo = NombreApodo;
                nGalera.CapacidadInstalada = CapacidadInstalada;
                nGalera.CapacidadNormal = CapacidadNormal;
                nGalera.CreadoPor = creadoPor;
                nGalera.EnProduccion = false;//Al crear la galera se asigna falto hasta que se cree un nuevo lote y se asigne la galera
                dc.Tbl_Galeras.InsertOnSubmit(nGalera);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Provee una clase para la relacion entre la galera y su granja y el usuario creado, solo se obtienen el nombre de Granja y del usuario que la creo
        /// </summary>
        public class Tbl_GalerasRel : Tbl_Galeras
        {
            string nombreGranja;

            public string NombreGranja
            {
                get { return nombreGranja; }
                set { nombreGranja = value; }
            }
            string creadoPorUser;

            public string CreadoPorUser
            {
                get { return creadoPorUser; }
                set { creadoPorUser = value; }
            }
        }
        /// <summary>
        /// Obtiene todas las Galeras Relacionadas segun el ID
        /// </summary>
        /// <param name="IDGranja"></param>
        /// <returns></returns>
        public List<Tbl_GalerasRel> getAllGalerasxID(int IDGranja)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_GalerasRel> resul = from _galeras in dc.Tbl_Galeras
                                                   join _granjas in dc.Tbl_Granjas on _galeras.ID_Granjas equals _granjas.ID_Granjas
                                                   join _aspnetUser in dc.aspnet_Users on _galeras.CreadoPor equals _aspnetUser.UserId
                                                   where (_galeras.ID_Granjas==IDGranja)
                                                   select new Tbl_GalerasRel
                                                   {
                                                       ID_Galeras = _galeras.ID_Galeras,
                                                       ID_Granjas = _granjas.ID_Granjas,
                                                       NumeroOrden = _galeras.NumeroOrden,
                                                       NombreApodo = _galeras.NombreApodo,
                                                       CapacidadInstalada = _galeras.CapacidadInstalada,
                                                       CapacidadNormal = _galeras.CapacidadNormal,
                                                       CreadoPorUser = _aspnetUser.UserName,
                                                       NombreGranja = _granjas.Nombre,
                                                       EnProduccion = _galeras.EnProduccion
                                                   };
                return resul.ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_GalerasRel>();
            }
        }
    }
}
