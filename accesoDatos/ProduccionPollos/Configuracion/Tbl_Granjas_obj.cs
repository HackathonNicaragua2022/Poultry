using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionPollos.Configuracion
{
    public class Tbl_Granjas_obj
    {
        /// <summary>
        /// Crea una NUeva Granja en la base de Datos
        /// </summary>
        /// <param name="nombreGranja"></param>
        /// <param name="ubicacion"></param>
        /// <param name="nombreEncargado"></param>
        /// <param name="telefonoEncargado"></param>
        /// <param name="fechaingreso"></param>
        /// <param name="TotalGaleras"></param>
        /// <param name="CapacidadxGaleras"></param>
        /// <param name="CreadoX"></param>
        /// <returns></returns>
        public string NuevaGranja(string nombreGranja, string ubicacion, string nombreEncargado, string telefonoEncargado, DateTime fechaingreso, int TotalGaleras, int CapacidadxGaleras, Guid CreadoX)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Granjas granja = new Tbl_Granjas();
                granja.Nombre = nombreGranja;
                granja.Hubicacion = ubicacion;
                granja.NombreResposableActivo = nombreEncargado;
                granja.TelefonoResponsableActivo = telefonoEncargado;
                granja.FechaCreacion_sy = fechaingreso;
                granja.TotalGaleras = TotalGaleras;
                granja.CapacidadxGalera = CapacidadxGaleras;
                granja.CreadoPor = CreadoX;
                dc.Tbl_Granjas.InsertOnSubmit(granja);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Obtiene todas las granjas de la base de datos relacionadas con su tablas de Usuario
        /// </summary>
        /// <returns></returns>
        public List<GranjasRel> getAllGranjas_Rel() {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.DeferredLoadingEnabled = false;
                IQueryable<GranjasRel> granjas = from _granjas in dc.Tbl_Granjas
                                                 join _usuarios in dc.aspnet_Users on _granjas.CreadoPor equals _usuarios.UserId
                                                 select new GranjasRel
                                                 {
                                                     ID_Granjas=_granjas.ID_Granjas,
                                                     Nombre=_granjas.Nombre,
                                                     Hubicacion=_granjas.Hubicacion,
                                                     NombreResposableActivo=_granjas.NombreResposableActivo,
                                                     TelefonoResponsableActivo=_granjas.TelefonoResponsableActivo,
                                                     CapacidadInstalada=_granjas.CapacidadInstalada,
                                                     FechaCreacion_sy=_granjas.FechaCreacion_sy,
                                                     TotalGaleras=_granjas.TotalGaleras,
                                                     CapacidadxGalera=_granjas.CapacidadxGalera,
                                                     CreadoPorUser=_usuarios.UserName.ToUpper()
                                                 };
                return granjas.ToList();
            }
            catch (Exception)
            {
                return new List<GranjasRel>();
            }
        }

        /// <summary>
        /// Clase para obtener todas las granjas relacionadas con los usuarios que la crearon
        /// </summary>
        public class GranjasRel : Tbl_Granjas
        {
            private string creadoPorUser;

            public string CreadoPorUser
            {
                get { return creadoPorUser; }
                set { creadoPorUser = value; }
            }
        }
    }
}
