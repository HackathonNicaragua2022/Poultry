using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos.BodegaPrincipal
{
    public class Rutas_obj
    {
        public List<Cat_Rutas> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_Rutas> result = from _rutas in dc.Cat_Rutas select _rutas;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Cat_Rutas>();
            }
        }
        /// <summary>
        /// Obtiene las rutas relacionadas con el vendedor asignado por defecto
        /// </summary>
        /// <returns></returns>
        public List<RutasVendedorAsig> getAllvendedorAsg()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<RutasVendedorAsig> result = from _rutas in dc.Cat_Rutas
                                                       join _vendedores in dc.Cat_Vendedores on _rutas.VENDEDOR_ASIGNADO equals _vendedores.ID_VENDEDOR
                                                       select new RutasVendedorAsig
                                                       {
                                                           ID_Rutas = _rutas.ID_Rutas,
                                                           NOMBRE_RUTA = _rutas.NOMBRE_RUTA,
                                                           Vendedor = _vendedores.NOMBRE_VENDEDOR,
                                                           ACTIVA = _rutas.ACTIVA
                                                       };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<RutasVendedorAsig>();
            }
        }
        public class RutasVendedorAsig : Cat_Rutas
        {
            private string vendedor;

            public string Vendedor
            {
                get { return vendedor; }
                set { vendedor = value; }
            }
        }
        public int getIDVendedorAsigandoxIDRuta(int idRuta)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Cat_Rutas.Any(a => a.ID_Rutas == idRuta))
                {
                    return (int)(from _Rutas in dc.Cat_Rutas where (_Rutas.ID_Rutas == idRuta) select _Rutas).ToList().FirstOrDefault().VENDEDOR_ASIGNADO;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int getIDRutaxIDVendedor(int IDVendedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Cat_Rutas.Any(a => a.VENDEDOR_ASIGNADO == IDVendedor))
                {
                    return (int)(from _Rutas in dc.Cat_Rutas where (_Rutas.VENDEDOR_ASIGNADO == IDVendedor) select _Rutas).ToList().FirstOrDefault().ID_Rutas;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
