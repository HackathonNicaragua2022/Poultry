using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class Tbl_ConfiguracionesGenerales
    {
        /// <summary>
        /// Guarda el peso por java vacia en la tabla de configuraciones del sistema
        /// </summary>
        /// <param name="peso"></param>
        /// <returns></returns>
        public string guardarPesoJava(string peso)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_Configuraciones> result = from var_ in dc.Tbl_Configuraciones select var_;
                if (result != null)
                {
                    Tbl_Configuraciones conf = result.FirstOrDefault();
                    if (conf != null)
                    {
                        conf.PesoJavaVacia = decimal.Parse(peso);
                        dc.SubmitChanges();
                    }
                    else
                    {
                        Tbl_Configuraciones config = new Tbl_Configuraciones();
                        config.PesoJavaVacia = decimal.Parse(peso);
                        config.PesoCanastaVacia = 5;
                        dc.Tbl_Configuraciones.InsertOnSubmit(config);
                        dc.SubmitChanges();
                    }
                }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Obtiene el Peso de la Java Vacia
        /// </summary>
        /// <returns></returns>
        public string getPesoJavaVacia()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.Tbl_Configuraciones.FirstOrDefault().PesoJavaVacia.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// Guarda el Peso de la Canasta Vacia usada en la sala de congelamiento
        /// </summary>
        /// <param name="peso"></param>
        /// <returns>String</returns>
        public string guardarPesoCanasta(string peso)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Tbl_Configuraciones> result = from var_ in dc.Tbl_Configuraciones select var_;
                if (result != null)
                {
                    Tbl_Configuraciones conf = result.FirstOrDefault();
                    if (conf != null)
                    {
                        conf.PesoCanastaVacia = decimal.Parse(peso);
                        dc.SubmitChanges();
                    }
                    else
                    {
                        Tbl_Configuraciones config = new Tbl_Configuraciones();
                        config.PesoJavaVacia = 15.8M;
                        config.PesoCanastaVacia = decimal.Parse(peso);
                        dc.Tbl_Configuraciones.InsertOnSubmit(config);
                        dc.SubmitChanges();
                    }
                }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Obtiene el Peso de la Canasta Vacia
        /// </summary>
        /// <returns>String</returns>
        public string getPesoCanastaVacia()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.Tbl_Configuraciones.FirstOrDefault().PesoCanastaVacia.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// guardar el sikn seleccionado por el usuario de la aplicacion de escritorio
        /// </summary>
        /// <param name="NombreUsuario"></param>
        /// <param name="NombreSkin"></param>
        public void guardarSkin(String NombreUsuario, String NombreSkin)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (NombreSkin.Length > 0 && NombreUsuario.Length > 0)
                {
                    if (dc.Tbl_SkinApp.Any(a => a.NOMBREUSUARIO == NombreUsuario.ToUpper()))
                    {
                        Tbl_SkinApp usuario = (from _skinapp in dc.Tbl_SkinApp where (_skinapp.NOMBREUSUARIO.Equals(NombreUsuario)) select _skinapp).FirstOrDefault();
                        if (usuario != null)
                        {
                            usuario.NOMBRESKIN = NombreSkin;
                            dc.SubmitChanges();
                        }
                    }
                    else
                    {
                        Tbl_SkinApp nuevoRegistro = new Tbl_SkinApp();
                        nuevoRegistro.NOMBRESKIN = NombreSkin;// <<--- PARA MEJOR COINCIDENCIA GRAVAR EN MAYUSCULAS
                        nuevoRegistro.NOMBREUSUARIO = NombreUsuario.ToUpper();
                        dc.Tbl_SkinApp.InsertOnSubmit(nuevoRegistro);
                        dc.SubmitChanges();
                    }
                }
            }
            catch (Exception)
            {
                //ignorar
            }
        }
        public String getNombreSkin(String nombreUsuario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Tbl_SkinApp.Any(a => a.NOMBREUSUARIO.Equals(nombreUsuario)))
                {
                    Tbl_SkinApp usuario = (from _skinapp in dc.Tbl_SkinApp where (_skinapp.NOMBREUSUARIO.Equals(nombreUsuario.ToUpper())) select _skinapp).FirstOrDefault();
                    return usuario.NOMBRESKIN;
                }
                else
                {                    
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
