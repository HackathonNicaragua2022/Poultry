using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TC_ConsumoWEB_VB.Servicio;

namespace ultil
{
    public class TazaCambio
    {
        public string getTazaString()
        {
            Tipo_Cambio_BCNSoapClient objServ = new Tipo_Cambio_BCNSoapClient();
            try
            {
                return Convert.ToString(objServ.RecuperaTC_Dia(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public double getTaza()
        {
            Tipo_Cambio_BCNSoapClient objServ = new Tipo_Cambio_BCNSoapClient();
            try
            {
                return objServ.RecuperaTC_Dia(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            catch (Exception)
            {
                return 0.00;
            }
        }
        public List<tazaMes> getTazaMes(int ano, int mes)
        {
            Tipo_Cambio_BCNSoapClient objServ = new Tipo_Cambio_BCNSoapClient();
            List<tazaMes> tazaMEnsual = new List<tazaMes>();
            try
            {
                int daysOfMonth = DateTime.DaysInMonth(ano, mes);
                for (int day = 1; day <= daysOfMonth; day++)
                {
                    double taza = objServ.RecuperaTC_Dia(ano, mes, day);
                    DateTime fecha = new DateTime(ano, mes, day);
                    tazaMes nuevaTaza = new tazaMes();
                    nuevaTaza.Taza = (decimal)taza;
                    nuevaTaza.Fecha = fecha;
                    tazaMEnsual.Add(nuevaTaza);
                }
                return tazaMEnsual;
            }
            catch (Exception)
            {
                return new List<tazaMes>();
            }
        }
        public void getMes(int ano, int Mes)
        {

            Tipo_Cambio_BCNSoapClient objServ = new Tipo_Cambio_BCNSoapClient();
            try
            {
                XElement resul = objServ.RecuperaTC_Mes(ano, Mes);
                List<tazaMes> list = resul.Elements("Tc").Select(sv => new tazaMes()
                {
                    //row = (int)sv.Element("row"),
                    //seat = (int)sv.Element("Chair")
                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public class tazaMes
        {
            private DateTime fecha;

            public DateTime Fecha
            {
                get { return fecha; }
                set { fecha = value; }
            }
            private decimal taza;

            public decimal Taza
            {
                get { return taza; }
                set { taza = value; }
            }
        }
    }
}
