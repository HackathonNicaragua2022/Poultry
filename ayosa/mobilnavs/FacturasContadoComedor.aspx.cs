using accesoDatos;
using accesoDatos.Comedor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class FacturasContadoComedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //***********************************                        
            try
            {
                tbl_trabajadores trabajador = new Tbl_Trabajador_obj().findById((int)Session["IDTrabajador"]);
                lbl_Nombre.Text = trabajador.Nombre_1;
                lbl_Puesto.Text = trabajador.Cat_Cargo.NombreCargo;
                lbl_Area.Text = trabajador.Cat_Area.NombreArea;
                lbl_IDUsuario.Text = trabajador.CodigoBarra + "(" + trabajador.ID_Trabajador + ")";
            }
            catch (Exception)
            {
                Response.Redirect("/mobilnavs/loginUsuariosComedor.aspx");
            }
            //***********************************  
            Session["TotalCobrar"] = (decimal)0;
            Session["TotalItem"] = (int)0;
            lbl_FechaInforme.Text = DateTime.Now.ToLongDateString();
        }
        public void fillFacturas()
        {
            try
            {
                Tbl_FaturaComedor_obj facturasxTrabajador = new Tbl_FaturaComedor_obj();

                DateTime fecha1 = (DateTime)Session["fechaInicial"];
                DateTime fecha2 = (DateTime)Session["fechaFinal"];


                List<Tbl_FaturaComedor_obj.facturasComedor> result = facturasxTrabajador.getFacturasContado_Comedor_xidTrabajador((int)Session["IDTrabajador"], fecha1, fecha2);
                //Tbl_FaturaComedor_obj.facturasComedor resumen = new Tbl_FaturaComedor_obj.facturasComedor();
                //resumen.FacturadoP = "RESUMEN FACTURAS ===>";
                //resumen.ID_Factura = 0;
                //resumen.Fecha_Factura_Hora = DateTime.Now;
                //resumen.ID_Trabajador = 0;
                //resumen.FacturadoPor = new Guid();
                //resumen.MontoTotalFactura = result.Sum(a => a.MontoTotalFactura);
                //resumen.NumerodeProductos = result.Sum(a => a.NumerodeProductos);
                //resumen.Cancelada = true;
                //resumen.Fecha_Cancelacion = DateTime.Now;
                //resumen.ConceptoCancelacion = "";

                List<Tbl_FaturaComedor_obj.facturasComedor> listafacturas = result.ToList();
                //listafacturas.Add(resumen);
                gv_Facturas_contado.DataSource = listafacturas;
                gv_Facturas_contado.DataBind();
                if (gv_Facturas_contado.Rows.Count > 0)
                {
                    this.gv_Facturas_contado.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_Facturas_contado.HeaderRow.Cells;


                    gv_Facturas_contado.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    cells[1].Attributes.Add("data-breakpoints", "all");
                    cells[2].Attributes.Add("data-breakpoints", "all");
                    cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[4].Attributes.Add("data-breakpoints", "xs sm md");           
                    gv_Facturas_contado.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                //decimal totalCobrar = (decimal)Session["TotalCobrar"];
                //int totalItem = (int)Session["TotalItem"];
                //totalCobrar += result.Sum(a => a.MontoTotalFactura);
                //totalItem += result.Sum(a => a.NumerodeProductos);

                //Session["TotalCobrar"] = totalCobrar;
                //Session["TotalItem"] = totalItem;

              //  lbl_totalCobrar.Text = string.Format("{0:C}", totalCobrar);
               // lbl_cantidadArticulosTotal.Text = string.Format("{0:n}", totalItem);
            }
            catch (Exception)
            {
              //  throw;
               Response.Redirect("/mobilnavs/loginUsuariosComedor.aspx");
            }

        }
        protected void gv_Facturas_contado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int ID_Factura = (int)gv_Facturas_contado.DataKeys[e.Row.RowIndex].Value;
                //----------------------------------------------------------------------------------

                GridView gv_DetalleFacturas = (GridView)e.Row.FindControl("gv_Detalle");
                Tbl_FaturaComedor_obj facturasxTrabajador = new Tbl_FaturaComedor_obj();

                List<DetalleFacturaCancelarResult> detalleFacturas = new AdministracionFacturasTrabajadorComedor().DetalleFacturasCancelar(ID_Factura);
                gv_DetalleFacturas.DataSource = detalleFacturas;
                gv_DetalleFacturas.DataBind();                 
            }
        }
        protected void btn_cargar_Click(object sender, EventArgs e)
        {
            string[] fechas = txt_rangofecha.Text.Trim().Split('-');
            CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
            DateTime fecha1 = DateTime.Parse(fechas[0], provider);
            Session["fechaInicial"] = fecha1;
            DateTime fecha2 = DateTime.Parse(fechas[1], provider);
            Session["fechaFinal"] = fecha2;
            fillFacturas();
        }
    }
}