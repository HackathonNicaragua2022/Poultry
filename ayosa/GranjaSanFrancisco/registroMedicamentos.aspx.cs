using accesoDatos;
using accesoDatos.Granja;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;

namespace POULTRY.GranjaSanFrancisco
{
    public partial class registroMedicamentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadLotesPRoduccion();
                fillCategoriaElementos();
            }
            fillLotesProduccion();
        }
        public void fillCategoriaElementos()
        {
            Cat_CategoriaInsumosGranjaEngorde_obj CategoriaInsumos = new Cat_CategoriaInsumosGranjaEngorde_obj();
            dr_CategoriaInsumo.DataSource = CategoriaInsumos.getAll();
            dr_CategoriaInsumo.DataTextField = "CategoriaInsumo";
            dr_CategoriaInsumo.DataValueField = "ID_CategoriaInsumosGranjaEngorde";
            dr_CategoriaInsumo.DataBind();
            fillInsumosxCategoria();
        }
        public void fillInsumosxCategoria()
        {
            Tbl_InsumoGranjaEngorde_obj insumosxCategorias = new Tbl_InsumoGranjaEngorde_obj();
            if (dr_CategoriaInsumo.Items.Count > 0)
            {
                int ID_Categoria = int.Parse(dr_CategoriaInsumo.SelectedItem.Value);
                dr_Insumo.DataSource = insumosxCategorias.getAllByIdCategoria(ID_Categoria);
                dr_Insumo.DataTextField = "NombreInsumo";
                dr_Insumo.DataValueField = "ID_InsumosGranjaEngorde";
                dr_Insumo.DataBind();

                int ID_InsumosGranjaEngorde = int.Parse(dr_Insumo.SelectedItem.Value);
                txt_CostoPorFrasco.Text = getPrecioInsumoCordobas(ID_InsumosGranjaEngorde).ToString();
            }
        }
        public void loadLotesPRoduccion()
        {
            InventarioGaleras_obj inventarioGaleras = new InventarioGaleras_obj();
            Session["ListaLotesEnProduccion"] = inventarioGaleras.getInventariosRel();
        }
        public void fillLotesProduccion()
        {
            if (Session["ListaLotesEnProduccion"] != null)
            {
                List<InventarioGaleras_vistaParametros> listaLP = (List<InventarioGaleras_vistaParametros>)Session["ListaLotesEnProduccion"];

                gv_LotesEnProduccion.DataSource = listaLP;
                gv_LotesEnProduccion.DataBind();
                if (gv_LotesEnProduccion.Rows.Count > 0)
                {
                    this.gv_LotesEnProduccion.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_LotesEnProduccion.HeaderRow.Cells;
                    gv_LotesEnProduccion.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    //cells[1].Attributes.Add("data-breakpoints", "md");
                    cells[2].Attributes.Add("data-breakpoints", "xs sm");
                    cells[3].Attributes.Add("data-breakpoints", "xs sm");
                    cells[4].Attributes.Add("data-breakpoints", "xs sm");
                    cells[5].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[7].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[8].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[9].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[10].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[11].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[26].Attributes.Add("data-breakpoints", "all");//xs sm md
                    gv_LotesEnProduccion.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        protected void gv_LotesEnProduccion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                List<InventarioGaleras_vistaParametros> listaLP = (List<InventarioGaleras_vistaParametros>)Session["ListaLotesEnProduccion"];
                if (listaLP.ElementAt(e.Row.RowIndex).Seleccionado == true)
                {
                    e.Row.Attributes.Add("class", "table-info");
                }
            }
            catch (Exception)
            {
            }
        }

        protected void gv_LotesEnProduccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Seleccionar"))
            {
                gv_LotesEnProduccion.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDSeleccionado = Convert.ToInt32(gv_LotesEnProduccion.SelectedValue);

                List<InventarioGaleras_vistaParametros> listaLP = (List<InventarioGaleras_vistaParametros>)Session["ListaLotesEnProduccion"];
                listaLP.ForEach(c => c.Seleccionado = false);
                listaLP.Where(a => a.ID_InventarioBroilers == IDSeleccionado).FirstOrDefault().Seleccionado = true;
                Session["LoteEditar"] = IDSeleccionado;
                fillLotesProduccion();
                modal_mensaje("Lote Seleccionado COD= " + IDSeleccionado.ToString(), "Se ha seleccionado correctamente el lote con Codigo: " + IDSeleccionado, tiposAdvertencias.informacion);
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
        public void modal_mensaje(string titulo, string mensaje, tiposAdvertencias alerta)
        {
            string tipoMensaje = "";
            switch (alerta)
            {
                case tiposAdvertencias.informacion:
                    tipoMensaje = "success";
                    break;
                case tiposAdvertencias.exito:
                    tipoMensaje = "info";
                    break;
                case tiposAdvertencias.advertencia:
                    tipoMensaje = "warning";
                    break;
                case tiposAdvertencias.error:
                    tipoMensaje = "error";
                    break;
                case tiposAdvertencias.pregunta:
                    tipoMensaje = "question";
                    break;
                default:
                    break;
            }
            ScriptManager.RegisterStartupScript(up_principal, GetType(), "modalAlert", "modal_alert('" + titulo + "','" + mensaje + "','" + tipoMensaje + "')", true);
        }

        protected void dr_CategoriaInsumo_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillInsumosxCategoria();
        }

        protected void btn_GuardarMedicamento_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_CostoPorFrasco, txt_DocisFrasco, txt_Fascos))
            {
                if (dr_Insumo.Items.Count > 0)
                {
                    if (Session["LoteEditar"] != null)
                    {


                        int IDInventarioGranja = (int)Session["LoteEditar"];
                        int IDInsumuloGranjaBroilers = int.Parse(dr_Insumo.SelectedItem.Value);

                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid ingresadoPor = (Guid)membershipUser.ProviderUserKey;

                        CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                        DateTime fechaRegistro = DateTime.Parse(txt_FechaRegistro.Text, provider);

                        string ConceptoAplicacion = txt_ConceptoAplicacion.Text;
                        decimal Frasco = decimal.Parse(txt_Fascos.Text);
                        decimal DosisxFrasco = decimal.Parse(txt_DocisFrasco.Text);
                        decimal costoxFrasco = decimal.Parse(txt_CostoPorFrasco.Text);


                        Tbl_AplicacionesMedicinas nuevoRegistro = new Tbl_AplicacionesMedicinas();
                        nuevoRegistro.ID_InventarioBroilers = IDInventarioGranja;
                        nuevoRegistro.ID_InsumosGranjaEngorde = IDInsumuloGranjaBroilers;
                        nuevoRegistro.RegistradoPor = ingresadoPor;
                        nuevoRegistro.FechaRegistroSistema = fechaRegistro;
                        nuevoRegistro.Numero_de_AplicacionDosis = -1;
                        nuevoRegistro.Frascos = Frasco;
                        nuevoRegistro.DosisxFrasco = DosisxFrasco;
                        nuevoRegistro.Concepto = ConceptoAplicacion;
                        nuevoRegistro.CostoxFrasco = costoxFrasco;
                        nuevoRegistro.CostoActualizado = false;
                        nuevoRegistro.UltimoCosto = costoxFrasco;
                        nuevoRegistro.Anulado = false;
                        //--------------------------------------------
                        AplicacionesMedicinas_obj nuevaAplicacionMedicina = new AplicacionesMedicinas_obj();
                        string result = nuevaAplicacionMedicina.nuevo(nuevoRegistro);
                        if (result.Equals("1"))
                        {
                            txt_FechaRegistro.Text = "";
                            txt_Fascos.Text = "";
                            txt_DocisFrasco.Text = "";
                            txt_TotalDocisAplicada.Text = "";
                            txt_ConceptoAplicacion.Text = "";
                            txt_CostoPorFrasco.Text = "";
                            txt_CostoTotal.Text = "";
                            //----------------------------------

                            //actualizar el precio del insumo
                            int ID_InsumosGranjaEngorde = int.Parse(dr_Insumo.SelectedItem.Value);
                            PreciosInsumoGranja_obj preciosInsumosGranjas = new PreciosInsumoGranja_obj();
                            bool resultado = preciosInsumosGranjas.actualizarPrecio(ID_InsumosGranjaEngorde, costoxFrasco);
                            System.Console.WriteLine("Se instancio preciosInsumosGranja_obj y se ejecuto el metodo actualizarPrecio(int, decimal) con resultado= " + resultado);
                            //-------------------------------


                            loadLotesPRoduccion();
                            fillLotesProduccion();
                            Session["LoteEditar"] = null;
                            new TextboxValidator().setFormControl(txt_CostoPorFrasco, txt_DocisFrasco, txt_Fascos);
                            modal_mensaje("Registrado Corrrectamente!", "El nuevo Medicamento fue registrado correctamente, ahora puede verlo desde el control de Lotes en Crecimiento!", tiposAdvertencias.informacion);
                        }
                        else
                        {
                            modal_mensaje("Error registrando el Medicamento!", "El Medicamento no se ha podido registrar correctamente debido al siguiente error: !" + result, tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        modal_mensaje("Seleccione un Lote en Crecimiento!", "Para continuar es necesario que seleccione un lote en crecimiento!", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    modal_mensaje("Lista vacia!", "La lista de insumos esta vacia, por favor veriquelo primero e itentelo nuevamente!", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modal_mensaje("Campos Vacios", "Por favor complete los campos señalados!", tiposAdvertencias.advertencia);
            }
        }

        protected void dr_Insumo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_InsumosGranjaEngorde = int.Parse(dr_Insumo.SelectedItem.Value);
            txt_CostoPorFrasco.Text = getPrecioInsumoCordobas(ID_InsumosGranjaEngorde).ToString();
        }
        private decimal getPrecioInsumoCordobas(int ID_InsumosGranjaEngorde)
        {
            PreciosInsumoGranja_obj preciosInsumosGranjas = new PreciosInsumoGranja_obj();
            return preciosInsumosGranjas.getPrecioCordobas_InsumoGranja(ID_InsumosGranjaEngorde);
        }
    }
}