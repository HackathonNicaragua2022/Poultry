using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using accesoDatos.ProduccionPollos.PlantasProcesadoras.PesoConjelado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.ProduccionDiaria_matadero
{
    public partial class Ingresoacuartofrio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fill_grid();
            //----------------------------------------------------------------------------------                
            try
            {
                DateTime fechaMatanza = Convert.ToDateTime(txt_FechaMatanza.Text, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat);
                resumendelDiaRemision resumenGeneral = new resumendelDiaRemision();
                resumendelDiaRemision.datosGenerales datosGenerales = resumenGeneral.getResumenDiaRemision(fechaMatanza);
                Session["DatosCompra"] = datosGenerales;
            }
            catch (Exception)
            {
            }
        }
        public resumendelDiaRemision.datosGenerales getDatosCompra()
        {
            if (Session["DatosCompra"] != null)
            {
                return (resumendelDiaRemision.datosGenerales)Session["DatosCompra"];
            }
            else
            {
                return null;
            }
        }
        public void fill_grid()
        {

            if (Session["DatosCompra"] != null)
            {
                resumendelDiaRemision.datosGenerales datosCompra = getDatosCompra();
                if (datosCompra != null)
                {
                    Tbl_PesoCortesDetalle_obj cortes = new Tbl_PesoCortesDetalle_obj();

                    var resul = cortes.getAllPesoCortesByIDPesoFrio(datosCompra.IDCompra);
                    gv_PesoCortes.DataSource = resul;
                    gv_PesoCortes.DataBind();

                    if (cortes != null)
                    {
                        if (resul.Count > 0)
                        {
                            this.gv_PesoCortes.UseAccessibleHeader = true;
                            TableCellCollection cells = gv_PesoCortes.HeaderRow.Cells;
                            gv_PesoCortes.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                            // cells[1].Attributes.Add("data-breakpoints", "all");
                            // cells[2].Attributes.Add("data-breakpoints", "all");
                            //cells[3].Attributes.Add("data-breakpoints", "all");
                            //cells[4].Attributes.Add("data-breakpoints", "all");
                            //cells[5].Attributes.Add("data-breakpoints", "all");                    
                            gv_PesoCortes.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                }
            }
        }
        protected void gv_PesoCortes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}