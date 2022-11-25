using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.Catalogos;
namespace POULTRY 
{
    public partial class cancelarfacturas : System.Web.UI.Page
    {
        //public String fechaeditar1 = "";
        //public String fechaeditar2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //fechaeditar1 = hf_fecha.Value;
            //fechaeditar2 = hf_fecha2.Value;
            FillProductos();
        }
        public void FillProductos()
        {
            if (Session["FacturandoA"] != null)
            {
                tbl_trabajadores trabajador = (tbl_trabajadores)Session["FacturandoA"];
                List<FacturasPendientesxTrabajadorResult> _FacturasPendietesxTrabajador = new AdministracionFacturasTrabajadorComedor().FacturasPendietesxTrabajador(trabajador.ID_Trabajador);
                gv_FacturasPendientes.DataSource = _FacturasPendietesxTrabajador;
                gv_FacturasPendientes.DataBind();
                if (gv_FacturasPendientes.Rows.Count > 0)
                {
                    this.gv_FacturasPendientes.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_FacturasPendientes.HeaderRow.Cells;
                    gv_FacturasPendientes.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                    //cells[1].Attributes.Add("data-sortable", "true");
                    cells[0].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[5].Attributes.Add("data-breakpoints", "all");
                    //cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[7].Attributes.Add("data-breakpoints", "xs sm md");
                    // cells[4].Attributes.Add("data-breakpoints", "all");
                    gv_FacturasPendientes.HeaderRow.TableSection = TableRowSection.TableHeader;

                    //
                    lbl_facturasTotales.Text = _FacturasPendietesxTrabajador.Count.ToString();
                    lbl_TotalProductos.Text = _FacturasPendietesxTrabajador.Sum(a => a.NumerodeProductos).ToString();
                    string totaCobrar = _FacturasPendietesxTrabajador.Sum(a => a.MontoTotalFactura).ToString();
                    lbl_Cobrar.Text = totaCobrar;
                    lbl_TotalCobrar.Text = totaCobrar;
                    //
                }
            }
        }
        protected void btn_FacturasPendientes_Click(object sender, EventArgs e)
        {
            FillProductos();
            //if (fechaeditar1.Length <= 0 && fechaeditar2.Length <= 0)
            //{
            lbl_TituloModo.Text = "Mostrando Facturas Pendientes";
            //}
            //else
            //{
            //lbl_TituloModo.Text = "Mostrando Facturas Pendientes de la Fecha " + fechaeditar1 + " a la Fecha " + fechaeditar2;
            //}
        }

        protected void btn_FacturasCanceladas_Click(object sender, EventArgs e)
        {

        }
        private void modalAdvertencia(String msg)
        {
            lbl_MsgAdvertencia.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalAdvertencia", "ShowModalAdvertencia();", true);
        }
        protected void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            string nombreCliente = txt_buscar.Text;
            if (nombreCliente.Length > 0)
            {
                List<usp_BuscarClienteResult> trabajadores = new Tbl_Trabajador_obj().buscarxNombre(nombreCliente);
                if (trabajadores != null)
                {
                    listCliente.DataSource = trabajadores;
                    listCliente.DataBind();
                    Empleados.Visible = true;
                }
            }
        }

        protected void btn_SeleccionarEmpleado_Click(object sender, EventArgs e)
        {
            ListViewItem item = (sender as Button).NamingContainer as ListViewItem;
            int ID = (int)listCliente.DataKeys[item.DataItemIndex].Values["ID_Trabajador"];
            //string groupName = (string)lvGroups.DataKeys[item.DataItemIndex].Values["GroupName"];
            tbl_trabajadores trabajador = new Tbl_Trabajador_obj().getTrabajador_byID(ID);
            if (trabajador != null)
            {
                Session["FacturandoA"] = trabajador;
                lbl_Cliente.Text = trabajador.Nombre_1;
                txt_buscar.Text = "";
                Empleados.Visible = false;
            }
        }

        protected void gv_FacturasPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detalle")
            {
                //Primero capturas la fila
                //int numFila = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
                gv_FacturasPendientes.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int idFactura = Convert.ToInt32(gv_FacturasPendientes.SelectedValue);


                List<DetalleFacturaCancelarResult> detalleFacturas = new AdministracionFacturasTrabajadorComedor().DetalleFacturasCancelar(idFactura);
                gv_Detalles.DataSource = detalleFacturas;
                gv_Detalles.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalDetalle", "ShowModalDetalle();", true);
            }
            if (e.CommandName == "cmd_CancelarFactura")
            {
                btn_Continuar.Visible = false;
                btn_CancelarIndividual.Visible = true;

                gv_FacturasPendientes.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int idFactura = Convert.ToInt32(gv_FacturasPendientes.SelectedValue);
                Session["idFactura_cancelar"] = idFactura;
                modalAdvertencia("Esta seguro que desea Cancelar esta Factura " + idFactura.ToString().PadLeft(5) + "</br>Se Generara una entrada en la Caja Chica con el Monto de la Factura!, ¿Seguro que deseha Continuar?");
            }
        }

        protected void btn_CancelarTodasLasPendientes_Click(object sender, EventArgs e)
        {
            btn_Continuar.Visible = true;
            btn_CancelarIndividual.Visible = false;
            modalAdvertencia("Esta Apunto de Cancelar Todas las Facturas</br>Se Generara una entrada en la Caja Chica con el Monto Total de las Facturas! </br>¿Esta Seguro que deseha Continuar?");
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

        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            tbl_trabajadores trabajador = (tbl_trabajadores)Session["FacturandoA"];
            var membershipUser = System.Web.Security.Membership.GetUser();
            Guid userId = (Guid)membershipUser.ProviderUserKey;
            AdministracionFacturasTrabajadorComedor adm = new AdministracionFacturasTrabajadorComedor();
            string result = adm.CancelarTodaslasFacturasxTrabajador(trabajador.ID_Trabajador, DateTime.Now, "CANCELACION DE TODAS LAS FACTURAS POR PAGO EN EFECTIVO", userId);
            FillProductos();
            if (result.Equals("1"))
            {
                //
                lbl_facturasTotales.Text = String.Empty;
                lbl_TotalProductos.Text = String.Empty;
                string totaCobrar = String.Empty;
                lbl_Cobrar.Text = String.Empty;
                lbl_TotalCobrar.Text = String.Empty;
                //
                FillProductos();
                modalInfo("Todas las Facturas Fueron Canceladas correctamente", tiposAdvertencias.exito);
            }
            else
            {
                modalInfo("Ocurrio un Error al Intentar Cancelar Todas las Facturas: " + result, tiposAdvertencias.error);
            }
        }

        protected void btn_CancelarIndividual_Click(object sender, EventArgs e)
        {
            if (Session["idFactura_cancelar"] != null)
            {
                var membershipUser = System.Web.Security.Membership.GetUser();
                Guid userId = (Guid)membershipUser.ProviderUserKey;
                int IdFactura = (int)Session["idFactura_cancelar"];

                AdministracionFacturasTrabajadorComedor adm = new AdministracionFacturasTrabajadorComedor();

                AdministracionFacturasTrabajadorComedor.datosFactura datosFactura = adm.datosXFactura(IdFactura);

                string result = adm.CancelarFacturas_ID(IdFactura, datosFactura.NombreEmpleado, datosFactura.MontoTotalFactura, DateTime.Now, "CANCELACION POR PAGO EN EFECTIVO", userId);
                FillProductos();
                if (result.Equals("1"))
                {
                    //
                    lbl_facturasTotales.Text = String.Empty;
                    lbl_TotalProductos.Text = String.Empty;
                    string totaCobrar = String.Empty;
                    lbl_Cobrar.Text = String.Empty;
                    lbl_TotalCobrar.Text = String.Empty;
                    //
                    FillProductos();
                    modalInfo("La Factura Fue Cancelada Correctamente", tiposAdvertencias.exito);
                }
                else
                {
                    modalInfo("Ocurrio un Error al Intentar Cancelar Todas las Facturas: " + result, tiposAdvertencias.error);
                }
            }
            else
            {
                modalInfo("El ID de la Factura es NULL, No se Puede Continuar", tiposAdvertencias.error);
            }
        }
    }
}