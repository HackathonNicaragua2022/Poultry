using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class Vehiculos_lg_obj
    {
        public string nuevoVehiculo(Tbl_Vehiculos nuevoVehiculo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Tbl_Vehiculos.Any(a => a.Placa == nuevoVehiculo.Placa))
                {
                    dc.Tbl_Vehiculos.InsertOnSubmit(nuevoVehiculo);
                    dc.SubmitChanges();
                    return "1";
                }
                else
                {
                    return "La Placa ya esta asignada a otro vehiculo en el sisema!!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<listadoVehiculos> getAllListado()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<listadoVehiculos> resultado = from vehiculos in dc.Tbl_Vehiculos
                                                         select new listadoVehiculos
                                                         {
                                                             Placa = vehiculos.Placa,
                                                             ID_MarcaModelosCarros = vehiculos.ID_MarcaModelosCarros,
                                                             IDCedes = vehiculos.IDCedes,
                                                             IDCombustible = vehiculos.IDCombustible,
                                                             NombreVehiculo = vehiculos.Cat_MarcaModeloCarros.NombreVehiculo + " " + vehiculos.ColorYDetalle,
                                                             Sede = vehiculos.Cat_Cedes.NombreCedes,
                                                             Combustible = vehiculos.Cat_Combustible.TipoCombustible,
                                                             URL_CirculacionImg=vehiculos.URL_CirculacionImg,
                                                             URL_CirculacionImgCara2=vehiculos.URL_CirculacionImgCara2,
                                                             URL_PlacaImg=vehiculos.URL_PlacaImg,
                                                             URL_SeguroVigenteImg=vehiculos.URL_SeguroVigenteImg,
                                                             //ColorYDetalle=vehiculos.ColorYDetalle
                                                         };
                return resultado.ToList();
            }
            catch (Exception)
            {
                return new List<listadoVehiculos>();
            }
        }
        public class listadoVehiculos : Tbl_Vehiculos
        {
            private string nombreVehiculo;

            public string NombreVehiculo
            {
                get { return nombreVehiculo; }
                set { nombreVehiculo = value; }
            }
            private string sede;

            public string Sede
            {
                get { return sede; }
                set { sede = value; }
            }
            private string combustible;

            public string Combustible
            {
                get { return combustible; }
                set { combustible = value; }
            }
        }
    }
}
