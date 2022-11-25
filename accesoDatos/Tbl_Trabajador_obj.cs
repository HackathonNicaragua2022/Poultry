using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class Tbl_Trabajador_obj
    {
        //public tbl_trabajadores getTrabajador_CodeBarr(string CodigoBarra)
        //{
        //    try
        //    {
        //        ayosabdDataContext dc = new ayosabdDataContext();
        //        if (dc.tbl_trabajadores.Any(a => a.CodigoBarra == CodigoBarra))
        //        {
        //            return (from trabajador in dc.tbl_trabajadores where (trabajador.CodigoBarra == CodigoBarra) select trabajador).First();
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        public tbl_trabajadores getTrabajador_byID(int IDTrabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from trabajador in dc.tbl_trabajadores where (trabajador.ID_Trabajador == IDTrabajador) select trabajador).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<usp_BuscarClienteResult> buscarxNombre(string nombre)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                List<usp_BuscarClienteResult> result = dc.usp_BuscarCliente(nombre).ToList();
                return result;
            }
            catch (Exception)
            {
                return new List<usp_BuscarClienteResult>();
            }
        }
        public List<tbl_trabajadores> getAllTrabajador()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from trabajador in dc.tbl_trabajadores select trabajador).ToList();
            }
            catch (Exception)
            {
                return new List<tbl_trabajadores>();
            }
        }
        public List<tbl_trabajadores> getAllTrabajador(bool activos)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from trabajador in dc.tbl_trabajadores where trabajador.Activo == activos select trabajador).ToList();
            }
            catch (Exception)
            {
                return new List<tbl_trabajadores>();
            }
        }
        public List<getAllTrabajadoresResult> getAllTrabajadorUSP()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.getAllTrabajadores().ToList();
            }
            catch (Exception)
            {
                return new List<getAllTrabajadoresResult>();
            }
        }
        public List<getTrabajadoresFiltroResult> getAllTrabajadorFiltro(string nombre)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.getTrabajadoresFiltro(nombre).ToList();
            }
            catch (Exception)
            {
                return new List<getTrabajadoresFiltroResult>();
            }
        }
        public string guardarTrabajador(tbl_trabajadores Trabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.tbl_trabajadores.Any(a => a.Nombre_1.Equals(Trabajador.Nombre_1)))
                {
                    if (!dc.tbl_trabajadores.Any(a => a.CodigoBarra.Equals(Trabajador.CodigoBarra)))
                    {
                        dc.tbl_trabajadores.InsertOnSubmit(Trabajador);
                        dc.SubmitChanges();
                        return "ok";
                    }
                    else
                    {
                        return "El codigo de Barra ya esta asignado!!";
                    }
                }
                else
                {
                    return "Ya existen un empleado con ese nombre!!";
                }
            }
            catch (Exception ex)
            {
                return "Error al guardar, no se ha podido guardar el Trabajador: " + ex.Message;
            }
        }
        public string generarCodigoBarra()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                string ultimoCodigoBarra = (from trabajador in dc.tbl_trabajadores orderby trabajador.CodigoBarra descending select trabajador).FirstOrDefault().CodigoBarra;
                if (ultimoCodigoBarra != null)
                {
                    if (ultimoCodigoBarra.Length > 6)
                    {
                        string nuevoCodigoBarra = "";
                        string primerCifra = "";
                        string segundaCifra = "";
                        int primerGrupo = 0;
                        int segundoGrupo = 0;
                        //----------------------
                        ultimoCodigoBarra = ultimoCodigoBarra.Substring(1);
                        var temp = ultimoCodigoBarra.Split('-');

                        primerCifra = temp[0];
                        segundaCifra = temp[1];

                        primerGrupo = int.Parse(primerCifra);
                        segundoGrupo = int.Parse(segundaCifra);

                        if (segundoGrupo == 999) { primerGrupo++; segundoGrupo = 1; } else { segundoGrupo++; }

                        if (primerGrupo < 10)
                        {
                            primerCifra = "0" + primerGrupo.ToString();
                        }
                        if (segundoGrupo < 999)
                        {
                            segundaCifra = segundoGrupo.ToString().PadLeft(3, '0');
                        }
                        nuevoCodigoBarra = "A" + primerCifra + "-" + segundaCifra;
                        return nuevoCodigoBarra;
                    }
                    else
                    {
                        return "A00-001";
                    }
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception)
            {
                return "Error";
            }
        }
        public string EliminarTrabajador(int IdTrabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                tbl_trabajadores TrabajadorElim = (from p in dc.tbl_trabajadores where (p.ID_Trabajador == IdTrabajador) select p).First();
                dc.tbl_trabajadores.DeleteOnSubmit(TrabajadorElim);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el Trabajador " + ex.Message;
            }
        }
        public tbl_trabajadores findById(int idTrabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.tbl_trabajadores where (p.ID_Trabajador == idTrabajador) select p).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool findByName(string nombreA)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //(from p in dc.tbl_Trabajador where (p.NombreTrabajador == idNombre) select p).First();
                return dc.tbl_trabajadores.Any(a => a.Nombre_1.Equals(nombreA));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string updateTrabajador(tbl_trabajadores TrabajadorMod, int idTrabajador)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                tbl_trabajadores objMod = (from p in dc.tbl_trabajadores where (p.ID_Trabajador == idTrabajador) select p).First();
                objMod.Cedula = TrabajadorMod.Cedula;
                objMod.Nombre_1 = TrabajadorMod.Nombre_1;
                objMod.ID_Area = TrabajadorMod.ID_Area;
                objMod.ID_Cargo = TrabajadorMod.ID_Cargo;
                objMod.ClaveAccesoSistema = TrabajadorMod.ClaveAccesoSistema;
               // objMod.CodigoBarra = TrabajadorMod.CodigoBarra;//desactivado para mantener el codigo de barra como ID unico
                objMod.Activo = TrabajadorMod.Activo;

                objMod.BeneficioDesayuno = TrabajadorMod.BeneficioDesayuno;
                objMod.BeneficioAlmuerzo = TrabajadorMod.BeneficioAlmuerzo;
                objMod.BeneficioCena = TrabajadorMod.BeneficioCena;

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
