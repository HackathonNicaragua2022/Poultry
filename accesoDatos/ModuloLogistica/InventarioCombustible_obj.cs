using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class InventarioCombustible_obj
    {
        /// <summary>
        /// Obtiene todo el tipo de Combustible en inventario
        /// Si no hay elementos en la table los creara a partr de los valores en la tabla Cat_Combustible, si no los hay devuelve null
        /// </summary>
        /// <returns></returns>
        public List<InventarioCombustibleClass> getAllInventario()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //Verifica  si la tabla esta vacia o no
                if (dc.Tbl_InventarioCombustible.Any())
                {
                    IQueryable<InventarioCombustibleClass> result = from _inventario in dc.Tbl_InventarioCombustible
                                                                    select new InventarioCombustibleClass
                                                                    {
                                                                        ID_InventarioC = _inventario.ID_InventarioC,
                                                                        IDCombustible = _inventario.IDCombustible,
                                                                        TotalEntradas = _inventario.TotalEntradas,
                                                                        TotalDespachos = _inventario.TotalDespachos,
                                                                        SaldoTotal = _inventario.SaldoTotal,
                                                                        UltimoCostoPorGalon = _inventario.UltimoCostoPorGalon,
                                                                        NombreCombustible = _inventario.Cat_Combustible.TipoCombustible
                                                                    };
                    return result.ToList();
                }
                else
                {
                    // la tabla esta vacia y no hay elementos, entonses copiar los datos de combustible y crear nuevos elementos
                    if (dc.Cat_Combustible.Any())//Verificar si hay datos en la tabla
                    {
                        IQueryable<Cat_Combustible> result = from combustible in dc.Cat_Combustible select combustible;
                        foreach (Cat_Combustible _combustible in result)
                        {
                            Tbl_InventarioCombustible inventarioCombustible = new Tbl_InventarioCombustible();
                            inventarioCombustible.IDCombustible = _combustible.IDCombustible;
                            inventarioCombustible.TotalEntradas = 0.0M;
                            inventarioCombustible.TotalDespachos = 0.0M;
                            inventarioCombustible.UltimoCostoPorGalon = 0.0M;
                            dc.Tbl_InventarioCombustible.InsertOnSubmit(inventarioCombustible);
                        }
                        dc.SubmitChanges();
                        return getAllInventario();
                    }
                    else return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public class InventarioCombustibleClass : Tbl_InventarioCombustible
        {
            private string nombreCombustible;

            public string NombreCombustible
            {
                get { return nombreCombustible; }
                set { nombreCombustible = value; }
            }
        }
    }
}
