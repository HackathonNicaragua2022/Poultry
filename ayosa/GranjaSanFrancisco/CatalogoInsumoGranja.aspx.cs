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
    public partial class CatalogoInsumoGranja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillCategoriasInsumosGranja();
                fillUnidadMedidaInsumosGranja();
                btn_GuardarCambiosEnInsumo.Visible = false;
            }
            fillInsumoEnSistema();
        }
        public void fillInsumoEnSistema()
        {
            Tbl_InsumoGranjaEngorde_obj insumos = new Tbl_InsumoGranjaEngorde_obj();
            rp_insumos.DataSource = insumos.getAll();
            rp_insumos.DataBind();
        }
        public void fillCategoriasInsumosGranja()
        {
            Cat_CategoriaInsumosGranjaEngorde_obj categorias = new Cat_CategoriaInsumosGranjaEngorde_obj();
            List<Cat_CategoriaInsumosGranjaEngorde> result = categorias.getAll();
            rp_Categorias.DataSource = result;
            rp_Categorias.DataBind();

            //------------------------------------------
            dr_CategoriaInsumo.DataSource = result;
            dr_CategoriaInsumo.DataTextField = "CategoriaInsumo";
            dr_CategoriaInsumo.DataValueField = "ID_CategoriaInsumosGranjaEngorde";
            dr_CategoriaInsumo.DataBind();
        }
        public void fillUnidadMedidaInsumosGranja()
        {
            Cat_MediasInsumosGranja_obj unidadMedida = new Cat_MediasInsumosGranja_obj();
            List<Cat_UnidadMedidaInsumoGranja> result = unidadMedida.getAll();
            rp_Medidas.DataSource = unidadMedida.getAll();
            rp_Medidas.DataBind();
            //------------------------------------------           
            dr_UnidadMedida.DataSource = result;
            dr_UnidadMedida.DataTextField = "UnidadMedidaInsumo";
            dr_UnidadMedida.DataValueField = "ID_unidadMedida";
            dr_UnidadMedida.DataBind();
        }


        protected void btn_GuardarCambiosEnInsumo_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_nombreInsumo))
            {
                if (dr_CategoriaInsumo.Items.Count > 0 && dr_UnidadMedida.Items.Count > 0)
                {
                    if (Session["IDInsumoGranja_Modificar"] != null)
                    {
                        int IDInsumoGranja = (int)Session["IDInsumoGranja_Modificar"];
                        int IDCategoria = int.Parse(dr_CategoriaInsumo.SelectedItem.Value.ToString());
                        int IDMedida = int.Parse(dr_UnidadMedida.SelectedItem.Value.ToString());
                        string nombreInsumo = txt_nombreInsumo.Text;
                        string result = new Tbl_InsumoGranjaEngorde_obj().editar(IDInsumoGranja, nombreInsumo, IDCategoria, IDMedida);
                        if (result.Equals("1"))
                        {
                            btn_GuardarNuevoInsumo.Visible = true;
                            btn_GuardarCambiosEnInsumo.Visible = false;
                            nuevoInsumo.Visible = false;
                            txt_nombreInsumo.Text = "";
                            fillInsumoEnSistema();
                            mensaje("El insumo fue modificado correctamente!!", tiposAdvertencias.informacion);
                        }
                        else
                        {
                            mensaje("No se pudo modificar el insumo, debido al siguiente error: " + result, tiposAdvertencias.advertencia);
                        }
                    }
                }
                else
                {
                    mensaje("Verifique que las listas de Categoria y unidad de medida, no se encuentren vacias!", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("No deje el campo nombre insumo vacio!", tiposAdvertencias.advertencia);
            }
        }

        protected void btn_GuardarNuevoInsumo_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_nombreInsumo))
            {
                if (dr_CategoriaInsumo.Items.Count > 0 && dr_UnidadMedida.Items.Count > 0)
                {
                    int IDCategoria = int.Parse(dr_CategoriaInsumo.SelectedItem.Value.ToString());
                    int IDMedida = int.Parse(dr_UnidadMedida.SelectedItem.Value.ToString());
                    string nombreInsumo = txt_nombreInsumo.Text;
                    string result = new Tbl_InsumoGranjaEngorde_obj().nuevoInsumo(IDCategoria, IDMedida, nombreInsumo);
                    if (result.Equals("1"))
                    {
                        nuevoInsumo.Visible = false;
                        txt_nombreInsumo.Text = "";
                        fillInsumoEnSistema();
                        mensaje("El nuevo insumo fue agregado a lista correctamente!!", tiposAdvertencias.informacion);
                    }
                    else
                    {
                        mensaje("No se pudo agregar el insumo a la lista debido al siguiente error: " + result, tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    mensaje("Verifique que las listas de Categoria y unidad de medida, no se encuentren vacias!", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("No deje el campo nombre insumo vacio!", tiposAdvertencias.advertencia);
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
            ScriptManager.RegisterStartupScript(up_principal, GetType(), "modalAlert", "modal_alert('" + mensaje + "','" + titulo + "','" + tipoMensaje + "')", true);
        }

        protected void btn_CancelarVentanaEdicion_Click(object sender, EventArgs e)
        {
            nuevoInsumo.Visible = false;
        }

        protected void rp_insumos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int IDInsumoGranja = int.Parse(e.CommandArgument.ToString());
            Session["IDInsumoGranja_Modificar"] = IDInsumoGranja;
            if (e.CommandName.Equals("cmd_Editar"))
            {
                Tbl_InsumoGranjaEngorde_obj insumosGranja = new Tbl_InsumoGranjaEngorde_obj();
                Tbl_InsumosGranjaEngorde insumoEditar = insumosGranja.getByID(IDInsumoGranja);
                dr_CategoriaInsumo.SelectedValue = insumoEditar.ID_CategoriaInsumosGranjaEngorde.ToString();
                dr_UnidadMedida.SelectedValue = insumoEditar.ID_unidadMedida.ToString();
                txt_nombreInsumo.Text = insumoEditar.NombreInsumo;
                txt_nombreInsumo.Focus();
                btn_GuardarNuevoInsumo.Visible = false;
                btn_GuardarCambiosEnInsumo.Visible = true;
                nuevoInsumo.Visible = true;
            }
            else if (e.CommandName.Equals("cmd_Eliminar"))
            {
                Tbl_InsumoGranjaEngorde_obj insumosGranja = new Tbl_InsumoGranjaEngorde_obj();
                string result = insumosGranja.delete(IDInsumoGranja);
                if (result.Equals("1"))
                {
                    fillInsumoEnSistema();
                    modal_mensaje("Se ha eliminado correctamente el insumo", "Eliminacion de insumo completado!!", tiposAdvertencias.informacion);
                }
                else
                {
                    modal_mensaje("Error eliminando el insumo, Error: " + result, "Error Eliminando insumo!!", tiposAdvertencias.error);
                }

                //mensaje("Eliminado correctamente", tiposAdvertencias.error);
            }
        }

        protected void txt_nuevaCategoria_TextChanged(object sender, EventArgs e)
        {
            Cat_CategoriaInsumosGranjaEngorde_obj categorias = new Cat_CategoriaInsumosGranjaEngorde_obj();
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_nuevaCategoria))
            {
                if (categorias.nuevaCategoria(txt_nuevaCategoria.Text.ToUpper()))
                {
                    txt_nuevaCategoria.Text = "";
                    fillCategoriasInsumosGranja();
                }
                else
                {
                    mensaje("Error ingresando la categoria!!", tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("Campos Vacios!!", tiposAdvertencias.error);
            }
        }

        protected void btn_nuevo_Click(object sender, EventArgs e)
        {
            txt_nuevaCategoria.Visible = !txt_nuevaCategoria.Visible;
            if (txt_nuevaCategoria.Visible)
            {
                btn_nuevo.Text = "<i class=\"fa fa-minus-circle text-danger\" aria-hidden=\"true\"></i>&nbsp;Ocultar";
            }
            else
            {
                btn_nuevo.Text = "<i class=\"fa fa-plus-circle text-success\" aria-hidden=\"true\"></i>&nbsp;Agregar";
            }
        }

        protected void rp_Categorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Cat_CategoriaInsumosGranjaEngorde_obj categorias = new Cat_CategoriaInsumosGranjaEngorde_obj();
            if (e.CommandName.Equals("cmd_Remover"))
            {
                int ID_InsumoGranja = int.Parse(e.CommandArgument.ToString());
                if (categorias.delete(ID_InsumoGranja))
                {
                    fillCategoriasInsumosGranja();
                }
            }
        }

        protected void rp_Medidas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Cat_MediasInsumosGranja_obj unidadMedida = new Cat_MediasInsumosGranja_obj();
            if (e.CommandName.Equals("cmd_Remover"))
            {
                int IDUnidadMedida = int.Parse(e.CommandArgument.ToString());
                if (unidadMedida.delete(IDUnidadMedida))
                {
                    fillUnidadMedidaInsumosGranja();
                }
            }
        }

        protected void btn_nuevaMedida_Click(object sender, EventArgs e)
        {
            txt_nuevaMedida.Visible = !txt_nuevaMedida.Visible;
            txt_Simbolo.Visible = !txt_Simbolo.Visible;
            if (txt_nuevaMedida.Visible)
            {
                btn_nuevaMedida.Text = "<i class=\"fa fa-minus-circle text-danger\" aria-hidden=\"true\"></i>&nbsp;Ocultar";
            }
            else
            {
                btn_nuevaMedida.Text = "<i class=\"fa fa-plus-circle text-success\" aria-hidden=\"true\"></i>&nbsp;Agregar";
            }
        }

        protected void txt_Simbolo_TextChanged(object sender, EventArgs e)
        {
            agrgarNuevaMedida();
        }

        protected void txt_nuevaMedida_TextChanged(object sender, EventArgs e)
        {
            agrgarNuevaMedida();
        }
        public void agrgarNuevaMedida()
        {
            Cat_MediasInsumosGranja_obj unidadMedida = new Cat_MediasInsumosGranja_obj();
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_Simbolo, txt_nuevaMedida))
            {
                if (unidadMedida.nuevaUnidadMedida(txt_nuevaMedida.Text, txt_Simbolo.Text))
                {
                    txt_Simbolo.Text = "";
                    txt_nuevaMedida.Text = "";
                    fillUnidadMedidaInsumosGranja();
                }
                else
                {
                    mensaje("Error!!", tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("Campos Vacios!!", tiposAdvertencias.error);
            }
        }

        protected void btn_NuevoInsumo_Click(object sender, EventArgs e)
        {
            nuevoInsumo.Visible = !nuevoInsumo.Visible;
            btn_GuardarNuevoInsumo.Visible = true;
            btn_GuardarCambiosEnInsumo.Visible = false;
            txt_nombreInsumo.Focus();
        }
    }
}