using accesoDatos;
using accesoDatos.ModuloLogistica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;
using System.Globalization;

namespace POULTRY.ModuloLogistica
{
    public partial class ingresoCombustible : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillProveedores();
                fillInventarioCombustible();
                //Crear una lista de deatlle que servira para mostrar el detalle ingresado durante el registro
                Session["lst_DetalleIngresoCompraCombustible"] = new List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class>();
                FillDetalle();
            }
        }
        public void fillProveedores()
        {
            Proveedor_obj proveedores = new Proveedor_obj();
            dr_ProvedorCombustible.DataSource = proveedores.getAll();
            dr_ProvedorCombustible.DataValueField = "ID_proveedorCombustible";
            dr_ProvedorCombustible.DataTextField = "NombreProveedor";
            dr_ProvedorCombustible.DataBind();
        }
        public void fillInventarioCombustible()
        {
            InventarioCombustible_obj inventarios = new InventarioCombustible_obj();
            dr_TipoCombustible.DataSource = inventarios.getAllInventario();
            dr_TipoCombustible.DataValueField = "ID_InventarioC";
            dr_TipoCombustible.DataTextField = "NombreCombustible";
            dr_TipoCombustible.DataBind();
        }
        public void FillDetalle()
        {
            if (Session["lst_DetalleIngresoCompraCombustible"] != null)
            {
                List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class> lsitaDetalle = (List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class>)Session["lst_DetalleIngresoCompraCombustible"];
                rp_detalles.DataSource = lsitaDetalle;
                rp_detalles.DataBind();
                lbl_totalGalones.Text = string.Format("{0:n}", lsitaDetalle.Sum(a => a.TotalGalones));
                lbl_CostoTotal.Text = string.Format("{0:n}", lsitaDetalle.Sum(a => a.CostoxGalon));
                lbl_TotalPagado.Text = string.Format("{0:n}", lsitaDetalle.Sum(a => a.TotalCostoGalon));
            }
        }
        protected void btn_GuardarIngreso_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_fechaFactura, txt_NumeroFactura))
            {
                if (Session["lst_DetalleIngresoCompraCombustible"] != null)
                {
                    List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class> lsitaDetalle = (List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class>)Session["lst_DetalleIngresoCompraCombustible"];
                    if (lsitaDetalle.Count > 0)
                    {
                        if (dr_ProvedorCombustible.Items.Count > 0)
                        {
                            try
                            {
                                string numeroFactura = txt_NumeroFactura.Text;
                                string fecha = txt_fechaFactura.Text;

                                CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                                DateTime fechaIngreso = DateTime.Parse(fecha, provider);

                                var membershipUser = System.Web.Security.Membership.GetUser();
                                Guid userId = (Guid)membershipUser.ProviderUserKey;

                                int id_ProveedorCombustible = int.Parse(dr_ProvedorCombustible.SelectedItem.Value.ToString());
                                decimal totalGalonesGasolina = lsitaDetalle.Sum(a => a.TotalGalones);//Se usara este en lugar de rellenar los campos de la tabla como total gasolina, total disel, total lubricantes
                                string Observaciones = txt_observaciones.Text;
                                if (ip_facturaFoto.PostedFile != null)
                                {
                                    if (ip_facturaFoto.PostedFile.FileName.Length > 0)
                                    {
                                    }
                                    else
                                    {
                                        lbl_mensaje.Text = "Porfavor Cargue el Archivo de imagen de la Factura!!";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Show_Modal_info", "ShowModal_Info();", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Show_Modal_info", "ShowModal_Info();", true);
                                }
                            }
                            catch (Exception)
                            {
                                mensaje("Revice que la fecha es correcta", tiposAdvertencias.advertencia);
                            }
                        }
                        else
                        {
                            mensaje("La lista de proveedores de combustible esta vacia, verifiquela para poder continuar!", tiposAdvertencias.advertencia);
                        }
                    }
                    else
                    {
                        mensaje("La lista de detalles esta vacia!", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    mensaje("La lista de detalles esta vacia!", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("Hay Campos Vacios, verifique la fecha y el numero de factura antes de continuar!", tiposAdvertencias.advertencia);
            }
        }
        public void mensaje(string mensaje, tiposAdvertencias alerta)
        {
            switch (alerta)
            {
                case tiposAdvertencias.informacion:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "info_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.exito:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "success_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.advertencia:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "advertencia_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.error:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "error_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.pregunta:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "pregunta_alert('" + mensaje + "')", true);
                    break;
                default:
                    break;
            }
        }
        protected void btn_AgregarDetalle_Click(object sender, EventArgs e)
        {
            if (dr_TipoCombustible.Items.Count > 0)
            {
                if (new TextboxValidator().validarLargoCadena_mayorCero(txt_TotalGalones, txt_CostoGalon))
                {
                    if (Session["lst_DetalleIngresoCompraCombustible"] != null)
                    {
                        List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class> lsitaDetalle = (List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class>)Session["lst_DetalleIngresoCompraCombustible"];
                        string nombreTipoGasolina = dr_TipoCombustible.SelectedItem.Text;
                        int IdTipoCombustible_Inventario = int.Parse(dr_TipoCombustible.SelectedItem.Value.ToString());
                        decimal totalGalones = decimal.Parse(txt_TotalGalones.Text);
                        decimal CostoxGalon = decimal.Parse(txt_CostoGalon.Text);
                        decimal TotalCosto = decimal.Parse(txt_TotalxGalones.Text);

                        if (totalGalones > 0 && CostoxGalon > 0)
                        {
                            if (!lsitaDetalle.Any(a => a.ID_InventarioC == IdTipoCombustible_Inventario))
                            {
                                DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class detalleIngreso = new DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class();
                                int IDTemporal = DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                                detalleIngreso.ID_DetalleIngreso = IDTemporal;
                                detalleIngreso.ID_InventarioC = IdTipoCombustible_Inventario;
                                detalleIngreso.TotalGalones = totalGalones;
                                detalleIngreso.CostoxGalon = CostoxGalon;
                                detalleIngreso.TotalCostoGalon = totalGalones * CostoxGalon;
                                detalleIngreso.NombreTipoCombustible = nombreTipoGasolina;
                                lsitaDetalle.Add(detalleIngreso);
                                txt_CostoGalon.Text = "";
                                txt_TotalGalones.Text = "";
                                txt_TotalxGalones.Text = "";
                                new TextboxValidator().setFormControl(txt_TotalGalones, txt_CostoGalon);
                                FillDetalle();
                            }
                            else
                            {
                                mensaje(nombreTipoGasolina + " ya esta en la lista de detalles, no se puede duplicar!!", tiposAdvertencias.advertencia);
                            }
                        }
                        else
                        {
                            mensaje("Los Valores no pueden ser igual a 0!", tiposAdvertencias.advertencia);
                        }
                    }
                }
                else
                {
                    mensaje("Hay Campos Vacios, verifique antes de continuar!", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("La lista de Tipo de Combustible esta vacia, verifique antes de continuar", tiposAdvertencias.advertencia);
            }
        }

        protected void rp_detalles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_remover"))
            {
                int idDetalle = int.Parse(e.CommandArgument.ToString());
                List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class> lsitaDetalle = (List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class>)Session["lst_DetalleIngresoCompraCombustible"];
                lsitaDetalle.Remove(lsitaDetalle.Where(a => a.ID_DetalleIngreso == idDetalle).FirstOrDefault());
                FillDetalle();
                mensaje("Detalle Removido de la lista Correctamente!!", tiposAdvertencias.exito);
            }
        }

        protected void btn_ChekAllBS_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_fechaFactura, txt_NumeroFactura))
            {
                if (Session["lst_DetalleIngresoCompraCombustible"] != null)
                {
                    List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class> lsitaDetalle = (List<DetalleIngresoCombustible_obj.DetalleIngresoCombustible_Class>)Session["lst_DetalleIngresoCompraCombustible"];
                    if (lsitaDetalle.Count > 0)
                    {
                        if (dr_ProvedorCombustible.Items.Count > 0)
                        {
                            try
                            {
                                btn_GuardarIngreso.Visible = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Show_Modal_Guardar", "ShowModal_Guardar();", true);
                            }
                            catch (Exception)
                            {
                                mensaje("Revice que la fecha es correcta", tiposAdvertencias.advertencia);
                            }
                        }
                        else
                        {
                            mensaje("La lista de proveedores de combustible esta vacia, verifiquela para poder continuar!", tiposAdvertencias.advertencia);
                        }
                    }
                    else
                    {
                        mensaje("La lista de detalles esta vacia!", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    mensaje("La lista de detalles esta vacia!", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("Hay Campos Vacios, verifique la fecha y el numero de factura antes de continuar!", tiposAdvertencias.advertencia);
            }
        }
    }
}