using accesoDatos;
using accesoDatos.Granja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;

namespace POULTRY.GranjaSanFrancisco
{
    public partial class ajutesInventarioLoteGranja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadLotesPRoduccion();

            }
            fillLotesProduccion();
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
                AjusteInventarioLoteGranja_obj ajusteInventario = new AjusteInventarioLoteGranja_obj();
                //int IDLote = new InventarioGaleras_obj().getIDLotexIDInventario(IDSeleccionado);
                int Dias = ajusteInventario.getDias(IDSeleccionado);
                Session["EdadDiasEntreIngresos"] = Dias;
                txt_DiasActualizar.Text = Dias.ToString();
                modal_mensaje("Lote Seleccionado COD= " + IDSeleccionado.ToString(), "Se ha seleccionado correctamente el lote con Codigo: " + IDSeleccionado + "<br/>Se ha obtenido la diferencia de dias para la gestion de la actualizacion", tiposAdvertencias.informacion);
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

        protected void btn_IngresoAjuste_Click(object sender, EventArgs e)
        {
            if (Session["LoteEditar"] != null)
            {
                if (new TextboxValidator().validarLargoCadena_mayorCero(txt_DiasActualizar, txt_CantidadIngresada, txt_MortalidadRegistrada, txt_motivoAjuste))
                {

                    int cantidadRecibida = int.Parse(txt_cantidadRecibida.Text);
                    int cantidadMortalida = int.Parse(txt_MortalidadRegistrada.Text);
                    string motivoAjuste = txt_motivoAjuste.Text;
                    //int edadDias = int.Parse(txt_DiasActualizar.Text);
                    int IDInventarioGranja = (int)Session["LoteEditar"];
                    int EdadDiasEntreIngreso = (int)Session["EdadDiasEntreIngresos"];
                    int dias = int.Parse(txt_Dias.Text);
                    if (cantidadRecibida >= 0)
                    {
                        if (cantidadMortalida < cantidadRecibida)
                        {
                            if (dias > 0)
                            {

                                Tbl_ajusteInventarioInicialLoteGranja nuevoAjuste = new Tbl_ajusteInventarioInicialLoteGranja();
                                nuevoAjuste.ID_InventarioBroilers = IDInventarioGranja;
                                nuevoAjuste.CantidadRecibida = cantidadRecibida;
                                nuevoAjuste.MortalidadRegistrada = cantidadMortalida;
                                nuevoAjuste.CantidadIngresada = cantidadRecibida - cantidadMortalida;
                                nuevoAjuste.MotivoAjuste = motivoAjuste;
                                nuevoAjuste.fechaIngresoAjuste = DateTime.Now;
                                nuevoAjuste.DiasEntreLoteInicialYAjuste = EdadDiasEntreIngreso;
                                AjusteInventarioLoteGranja_obj nuevoAjusteInventario = new AjusteInventarioLoteGranja_obj();


                                string result = nuevoAjusteInventario.ingresarAjuste(nuevoAjuste, dias);
                                if (result.Equals("1"))
                                {
                                    txt_CantidadIngresada.Text = "";
                                    txt_cantidadRecibida.Text = "";
                                    txt_DiasActualizar.Text = "";
                                    txt_MortalidadRegistrada.Text = "";
                                    txt_motivoAjuste.Text = "";
                                    Session["LoteEditar"] = null;
                                    Session["EdadDiasEntreIngresos"] = null;
                                    new TextboxValidator().setFormControl(txt_DiasActualizar, txt_CantidadIngresada, txt_MortalidadRegistrada, txt_motivoAjuste);

                                    modal_mensaje("Ajuste Ingresado Correctamente!!", "El ajuste fue ingresado correctamente, el inventario de la galera se modifico y se actualizo la edad", tiposAdvertencias.exito);
                                }
                                else
                                {
                                    mensaje("Erro agregando el ajuste: " + result, tiposAdvertencias.error);
                                }
                            }
                            else
                            {
                                mensaje("La edad no puede ser menor o igual 0 Dias", tiposAdvertencias.advertencia);
                            }
                        }
                        else
                        {
                            mensaje("La mortalidad no es Coherente, no puede ser igual a la Cantidad Recibida!!, verifiquelo he intentelo nuevamente", tiposAdvertencias.advertencia);
                        }
                    }
                    else
                    {
                        mensaje("La cantidad Ingresada en <strong>Cantidad Recibida</strong>, no puede ser 0", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    mensaje("Debe Completar los campos Marcados", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modal_mensaje("Seleccione un Lote en Crecimiento!", "Para continuar es necesario que seleccione un lote en crecimiento!", tiposAdvertencias.advertencia);
            }
        }
    }
}