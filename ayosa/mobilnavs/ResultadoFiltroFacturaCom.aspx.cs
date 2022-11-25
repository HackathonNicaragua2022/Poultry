using accesoDatos;
using accesoDatos.Comedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class ResultadoFiltroFacturaCom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idFactura = (int)Session["IDFacturaFiltroComedor"];
            lbl_NumeroFactura.Text = idFactura.ToString().PadLeft(10, '0');
            lbl_FechaInforme.Text = DateTime.Now.ToLongDateString();
            fillFacturas();
        }
        public void fillFacturas()
        {
            try
            {
                Tbl_FaturaComedor_obj facturasxTrabajador = new Tbl_FaturaComedor_obj();
                List<Tbl_FaturaComedor_obj.facturasComedor> result = facturasxTrabajador.getFacturaxID((int)Session["IDFacturaFiltroComedor"]);
                gv_FacturaEncontrada.DataSource = result;
                gv_FacturaEncontrada.DataBind();
                if (gv_FacturaEncontrada.Rows.Count > 0)
                {
                    this.gv_FacturaEncontrada.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_FacturaEncontrada.HeaderRow.Cells;


                    gv_FacturaEncontrada.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    cells[1].Attributes.Add("data-breakpoints", "all");
                    cells[2].Attributes.Add("data-breakpoints", "all");
                    cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[4].Attributes.Add("data-breakpoints", "xs sm md");           
                    gv_FacturaEncontrada.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception)
            {
                //  throw;
                Response.Redirect("/mobilnavs/loginUsuariosComedor.aspx");
            }

        }
        protected void gv_FacturaEncontrada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int ID_Factura = (int)gv_FacturaEncontrada.DataKeys[e.Row.RowIndex].Value;
                //----------------------------------------------------------------------------------

                GridView gv_DetalleFacturas = (GridView)e.Row.FindControl("gv_Detalle");
                Tbl_FaturaComedor_obj facturasxTrabajador = new Tbl_FaturaComedor_obj();

                List<DetalleFacturaCancelarResult> detalleFacturas = new AdministracionFacturasTrabajadorComedor().DetalleFacturasCancelar(ID_Factura);
                gv_DetalleFacturas.DataSource = detalleFacturas;
                gv_DetalleFacturas.DataBind();
            }
        }
    }
}