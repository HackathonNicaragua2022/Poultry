using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.Catalogos;
namespace POULTRY.facturadorescomedor
{
    public partial class salidaIventarioComedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbl_fechaIngreso.Text = DateTime.Now.ToLongDateString();

                dr_pInventaario.DataSource = new Tbl_Inventario_obj().ProductosEnInventario(0);
                dr_pInventaario.DataTextField = "NombreProducto";
                dr_pInventaario.DataValueField = "ID_Inventario";
                dr_pInventaario.DataBind();
                //---------------------------------------------------------------------
                Session["Tbl_DetalleSalida_Facturando"] = new List<Tbl_DetalleSalida_Facturando>();
                //---------------------------------------------------------------------               
            }
            fillDetalle();
        }
        public void fillDetalle()
        {
            List<Tbl_DetalleSalida_Facturando> listaDetalle = (List<Tbl_DetalleSalida_Facturando>)Session["Tbl_DetalleSalida_Facturando"];
            gv_productos.DataSource = listaDetalle;
            gv_productos.DataBind();
            if (gv_productos.Rows.Count > 0)
            {
                this.gv_productos.UseAccessibleHeader = true;
                TableCellCollection cells = gv_productos.HeaderRow.Cells;
                gv_productos.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //  cells[1].Attributes.Add("data-breakpoints", "all");
                //   cells[2].Attributes.Add("data-breakpoints", "all");
                //   cells[5].Attributes.Add("data-breakpoints", "all");
                gv_productos.HeaderRow.TableSection = TableRowSection.TableHeader;
                lbl_CantidadTotalEgresar.Text = listaDetalle.Sum(a => a.Cantidad).ToString();
                lbl_CostoTotales.Text = listaDetalle.Sum(a => a.CostoUnidad).ToString();
                lbl_PreciosVentaTotal.Text = listaDetalle.Sum(a => a.PrecioVUnidad).ToString();
            }

        }
        private void modalInfo(String msg, tiposAdvertencias advertencias)
        {
            lbl_modalInfo.Text = msg;
            switch (advertencias)
            {
                case tiposAdvertencias.advertencia:
                    imagenModalInfo.Src = "~/Imagenes/warning.png";
                    break;
                case tiposAdvertencias.error:
                    imagenModalInfo.Src = "~/Imagenes/error.png";
                    break;
                case tiposAdvertencias.informacion:
                    imagenModalInfo.Src = "~/Imagenes/camera_test.png";
                    break;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "ModalInfo", "ShowModalInfo();", true);
        }
        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (dr_pInventaario.Items.Count > 0)
            {
                if (txt_Cantidad.Text.Length > 0)
                {
                    //Variables
                    int ID_Inventario = int.Parse(dr_pInventaario.SelectedValue);
                    string nombreProducto = dr_pInventaario.SelectedItem.ToString();
                    int Cantidad = int.Parse(txt_Cantidad.Text);
                    int existencia_actual = new Tbl_Inventario_obj().getExistenciaByIdInventario(ID_Inventario);
                    if (existencia_actual >= Cantidad)
                    {
                        decimal CostoCompra = new Tbl_Inventario_obj().findByIdInventario(ID_Inventario).CostoCompra;
                        decimal VentaCompra = new Tbl_Inventario_obj().findByIdInventario(ID_Inventario).PrecioVenta;
                        string comentario = txt_Obeservaciones.Text;

                        Tbl_DetalleSalida_Facturando dsalidaInv = new Tbl_DetalleSalida_Facturando();
                        dsalidaInv.ID_Inventario = ID_Inventario;
                        dsalidaInv.Cantidad = Cantidad;
                        dsalidaInv.CostoUnidad = CostoCompra;//Costo de Compra
                        dsalidaInv.PrecioVUnidad = VentaCompra;//Costo de Compra
                        dsalidaInv.Observaciones = comentario;
                        //dfac.NombreProducto = new Tbl_Inventario_obj().getProdcutoByInventarioID(ID_Inventario).NombreProducto;
                        dsalidaInv.NombreProducto = nombreProducto;
                        List<Tbl_DetalleSalida_Facturando> detalle = (List<Tbl_DetalleSalida_Facturando>)Session["Tbl_DetalleSalida_Facturando"];
                        detalle.Add(dsalidaInv);
                        fillDetalle();
                        ///---------------------------------
                        txt_Cantidad.Text = "";
                        txt_Obeservaciones.Text = "";
                        btn_Guardar.Visible = true;
                    }
                    else
                    {
                        modalInfo("<strong>LA CANTIDAD QUE INDICÓ, ES MAYOR A LA EXISTENCIA ACTUAL (" + existencia_actual + ")</strong>", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    modalInfo("debe ingresar una cantidad para el producto", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modalInfo("No hay productos en Inventario", tiposAdvertencias.advertencia);
            }
        }

        protected void gv_productos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "remover")
            {

                gv_productos.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IdInventario = Convert.ToInt32(gv_productos.SelectedValue);
                List<Tbl_DetalleSalida_Facturando> detalle = (List<Tbl_DetalleSalida_Facturando>)Session["Tbl_DetalleSalida_Facturando"];
                Tbl_DetalleSalida_Facturando remover = detalle.Where(s => s.ID_Inventario == IdInventario).FirstOrDefault();
                detalle.Remove(remover);
                fillDetalle();
                if (detalle.Count <= 0)
                {
                    btn_Guardar.Visible = false;
                }
            }
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (Session["Tbl_DetalleSalida_Facturando"] != null)
            {
                List<Tbl_DetalleSalida_Facturando> detalle = (List<Tbl_DetalleSalida_Facturando>)Session["Tbl_DetalleSalida_Facturando"];
                if (detalle.Count > 0)
                {
                    // Variales                    
                    DateTime fechaIngreso = DateTime.Now;
                    var membershipUser = System.Web.Security.Membership.GetUser();
                    Guid userId = (Guid)membershipUser.ProviderUserKey;
                    
                    //-------------------------------------------
                    egreso_InventarioComedor egresoInventario = new egreso_InventarioComedor();
                    string result = egresoInventario.guardarSalida_InventarioComedor(fechaIngreso,
                        userId, detalle);
                    if (result.Equals("1"))
                    {
                        Session["Tbl_DetalleSalida_Facturando"] = null;
                        fillDetalle();
                        ///---------------------------------
                        txt_Cantidad.Text = "";
                        txt_Obeservaciones.Text = "";
                        lbl_CantidadTotalEgresar.Text = "";
                        lbl_CostoTotales.Text = "";
                        lbl_PreciosVentaTotal.Text = string.Empty;
                        btn_Guardar.Visible = false;
                        modalInfo("La actualizacion del inventario se ha realizado de manera exitosa!!", tiposAdvertencias.informacion);
                    }
                    else
                    {
                        modalInfo("Ocurrio un error: <br/>" + result, tiposAdvertencias.error);
                    }
                }
                else
                {
                    modalInfo("Debe agregar productos primero, antes de guardar", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modalInfo("Debe agregar productos primero antes de guardar", tiposAdvertencias.advertencia);
            }
        }
    }
}