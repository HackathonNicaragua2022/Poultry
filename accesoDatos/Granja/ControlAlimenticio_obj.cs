using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class ControlAlimenticio_obj
    {
        public List<ConsumoAlimentos_vista> getAll(int IDInventarioBroilers)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ConsumoAlimentos_vista> result = from ComsumoAlimento in dc.Tbl_ControlAlimenticio
                                                            where (ComsumoAlimento.ID_InventarioBroilers == IDInventarioBroilers)
                                                            select new ConsumoAlimentos_vista
                                                            {
                                                                Id_ControlAlimenticio = ComsumoAlimento.Id_ControlAlimenticio,
                                                                ID_TipoAlimentos = ComsumoAlimento.ID_TipoAlimentos,
                                                                ID_InventarioBroilers = ComsumoAlimento.ID_InventarioBroilers,
                                                                FechaRegistro = ComsumoAlimento.FechaRegistro,
                                                                InventarioInicialQtl = ComsumoAlimento.InventarioInicialQtl,
                                                                QtlRecibidoDiario = ComsumoAlimento.QtlRecibidoDiario,
                                                                FechaHoraRegistroConsumo_in = ComsumoAlimento.FechaHoraRegistroConsumo_in,
                                                                FechaHoraRegistroConsumo_fin = ComsumoAlimento.FechaHoraRegistroConsumo_fin,
                                                                InventarioFinal = ComsumoAlimento.InventarioFinal,
                                                                ConsumoDiario = ComsumoAlimento.ConsumoDiario,
                                                                registroManual = ComsumoAlimento.registroManual,
                                                                RegistradoPor = ComsumoAlimento.RegistradoPor,
                                                                Comentarios = ComsumoAlimento.Comentarios,
                                                                RegistradoPorUsuario = ComsumoAlimento.aspnet_Users.UserName,
                                                                NombreTipoAlimentos = ComsumoAlimento.Cat_TipoAlimentoAves.NombreAlimento
                                                            };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<ConsumoAlimentos_vista>();
            }
        }
    }
}
