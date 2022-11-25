using accesoDatos;
using accesoDatos.ProduccionPollos.PlantasProcesadoras.PesoConjelado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;

namespace POULTRY.plantaMatadero
{
    public partial class rubrosAvez : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            fill_grid();
        }

        protected void btn_NuevoRubro_Click(object sender, EventArgs e)
        {
            lbl_titulo_ne.Text = "NUEVO RUBRO DE AVES";
            txt_Codigo.Text = "";
            txt_Rubro.Text = "";
            txt_siglas.Text = "";
            up_ne.Update();
            new setCssEstadoTextbox(txt_Codigo).setFormControl();
            new setCssEstadoTextbox(txt_Rubro).setFormControl();
            new setCssEstadoTextbox(txt_siglas).setFormControl();
            btn_GuardarCambios.Visible = false;
            btn_GuardarEditar.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalNuevoEditar", "ShowModal_NuevoEditar();", true);
        }

        protected void gv_rubros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Editar"))
            {
                gv_rubros.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int ID_RubrosAve = Convert.ToInt32(gv_rubros.SelectedValue);

                Tbl_RubroAves_obj rubros = new Tbl_RubroAves_obj();
                Tbl_RubrosAve rubrosSeleccionado = rubros.getRubroAveById(ID_RubrosAve);

                if (rubrosSeleccionado != null)
                {
                    txt_Codigo.Text = rubrosSeleccionado.Codigos;
                    txt_Rubro.Text = rubrosSeleccionado.Producto;
                    txt_siglas.Text = rubrosSeleccionado.Siglas;

                    if (rubrosSeleccionado.VicerasComestibles)
                    {
                        chk_tipoViceras.SelectedValue = "1";
                    }
                    else if (rubrosSeleccionado.EsTitil)
                    {
                        chk_tipoViceras.SelectedValue = "2";
                    }
                    else if (rubrosSeleccionado.EsHigado)
                    {
                        chk_tipoViceras.SelectedValue = "3";
                    }

                    Session["RubroModificar"] = rubrosSeleccionado;
                    btn_GuardarCambios.Visible = true;
                    btn_GuardarEditar.Visible = false;
                    lbl_titulo_ne.Text = "Modificar Rubro N# " + rubrosSeleccionado.ID_RubrosAve;


                    up_grid.Update();
                    up_ne.Update();

                    ScriptManager.RegisterStartupScript(this, GetType(), "ModalNuevoEditar", "ShowModal_NuevoEditar();", true);
                }
                else
                {
                    mensaje("No se pudo Cargar la informacion del Rubro, revise su conexion a internet", tiposAdvertencias.error);
                }
            }
            if (e.CommandName.Equals("cmd_Eliminar"))
            {
                gv_rubros.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int ID_RubrosAve = Convert.ToInt32(gv_rubros.SelectedValue);
                Tbl_RubroAves_obj rubros = new Tbl_RubroAves_obj();
                Tbl_RubrosAve rubrosSeleccionado = rubros.getRubroAveById(ID_RubrosAve);

                Session["ID_RubrosAve_Eliminar"] = ID_RubrosAve;
                up_grid.Update();
                up_ne.Update();
                modalAdvertencia("Esta completamente seguro que deseha eliminar este rubro del sistema</br>Codigo: " + rubrosSeleccionado.Codigos + "</br> Nombre Rubro: " + rubrosSeleccionado.Producto + "</br> NOTA: Nose podra eliminar el rubro del sistema si ya se ha usado en otras partes del mismo </br>¿DESEA CONTINUAR?");
            }
        }
        private void modalAdvertencia(String msg)
        {
            lbl_MsgAdvertencia.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalAdvertencia", "ShowModalAdvertencia();", true);
        }
        public void fill_grid()
        {
            //----------------------------------------------------------------------------------                
            Tbl_RubroAves_obj rubros = new Tbl_RubroAves_obj();

            List<Tbl_RubrosAve> result = rubros.getAllRubros();

            gv_rubros.DataSource = result;
            gv_rubros.DataBind();

            if (result != null)
            {
                if (result.Count > 0)
                {
                    this.gv_rubros.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_rubros.HeaderRow.Cells;
                    gv_rubros.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    // cells[1].Attributes.Add("data-breakpoints", "all");
                    // cells[2].Attributes.Add("data-breakpoints", "all");
                    //cells[3].Attributes.Add("data-breakpoints", "all");
                    //cells[4].Attributes.Add("data-breakpoints", "all");
                    //cells[5].Attributes.Add("data-breakpoints", "all");                    
                    gv_rubros.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void btn_GenerarCodigo_Click(object sender, EventArgs e)
        {
            Tbl_RubroAves_obj rubros = new Tbl_RubroAves_obj();
            String codigo = rubros.generarCodigoRubro();
            if (!codigo.Equals("-1"))
            {
                txt_Codigo.Text = rubros.generarCodigoRubro();
                new setCssEstadoTextbox(txt_Codigo).setValid_FormControl();
            }
            else
            {
                new setCssEstadoTextbox(txt_Codigo).setInValid_FormControl();
            }
        }

        protected void txt_Codigo_TextChanged(object sender, EventArgs e)
        {
            String value = txt_Codigo.Text;
            if (new Tbl_RubroAves_obj().verificarCodigoRubro(value))
            {
                new setCssEstadoTextbox(txt_Codigo).setInValid_FormControl();
            }
            else
            {
                if (value.Length > 0)
                {
                    new setCssEstadoTextbox(txt_Codigo).setValid_FormControl();
                }
                else
                {
                    new setCssEstadoTextbox(txt_Codigo).setFormControl();
                }
            }
        }

        protected void btn_GuardarEditar_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_Codigo, txt_Rubro, txt_siglas))
            {
                Tbl_RubroAves_obj guardarRubro = new Tbl_RubroAves_obj();
                Tbl_RubrosAve nuevoRubro = new Tbl_RubrosAve();
                nuevoRubro.Codigos = txt_Codigo.Text;
                nuevoRubro.Producto = txt_Rubro.Text;
                nuevoRubro.Siglas = txt_siglas.Text;
                nuevoRubro.VicerasComestibles = chk_tipoViceras.SelectedValue.Equals("1") ? true : false;
                nuevoRubro.EsTitil = chk_tipoViceras.SelectedValue.Equals("2") ? true : false;
                nuevoRubro.EsHigado = chk_tipoViceras.SelectedValue.Equals("3") ? true : false;

                String resultado = guardarRubro.guardarNuevoRubro(nuevoRubro);

                if (resultado.Equals("1"))
                {
                    lbl_titulo_ne.Text = "NUEVO RUBRO DE AVES";
                    txt_Codigo.Text = "";
                    txt_Rubro.Text = "";
                    txt_siglas.Text = "";
                    fill_grid();
                    up_ne.Update();
                    up_grid.Update();
                    mensaje("Nuevo Rubro Guardado Correctamente!!", tiposAdvertencias.exito);
                }
                else
                {
                    mensaje("Error no se pudo Guardar el Rubro: ", tiposAdvertencias.error);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalNuevoEditarOcultar", "HideModal_NuevoEditar();", true);
            }
        }
        public void mensaje(string mensaje, tiposAdvertencias alerta)
        {
            switch (alerta)
            {
                case tiposAdvertencias.informacion:
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "info_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.exito:
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "success_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.advertencia:
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "advertencia_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.error:
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "error_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.pregunta:
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "pregunta_alert('" + mensaje + "')", true);
                    break;
                default:
                    break;
            }
        }

        protected void btn_GuardarCambios_Click(object sender, EventArgs e)
        {
            if (new TextboxValidator().validarLargoCadena_mayorCero(txt_Codigo, txt_Rubro, txt_siglas))//Verifica que los campos de texto contengan un valor de lo contrario muestra con css en boostrap 4 un borde rojo
            {
                String value = txt_Codigo.Text;
                if (new Tbl_RubroAves_obj().verificarCodigoRubro(value) || value.Length <= 0)
                {
                    lbl_titulo_ne.Text = "<div class=\"bg-danger\"> El Codigo ya esta en la base de datos</div>";
                    new setCssEstadoTextbox(txt_Codigo).setInValid_FormControl();
                }
                else
                {
                    if (Session["RubroModificar"] != null)
                    {
                        new setCssEstadoTextbox(txt_Codigo).setValid_FormControl();
                        Tbl_RubroAves_obj guardarRubro = new Tbl_RubroAves_obj();
                        Tbl_RubrosAve nuevoRubro = (Tbl_RubrosAve)Session["RubroModificar"];
                        nuevoRubro.Codigos = txt_Codigo.Text;
                        nuevoRubro.Producto = txt_Rubro.Text;
                        nuevoRubro.Siglas = txt_siglas.Text;
                        nuevoRubro.VicerasComestibles = chk_tipoViceras.SelectedValue.Equals("1") ? true : false;
                        nuevoRubro.EsTitil = chk_tipoViceras.SelectedValue.Equals("2") ? true : false;
                        nuevoRubro.EsHigado = chk_tipoViceras.SelectedValue.Equals("3") ? true : false;

                        lbl_titulo_ne.Text = "Guardardando..";
                        /*
                         * 
                        */

                        String resultado = guardarRubro.modificar_Rubro(nuevoRubro);

                        if (resultado.Equals("1"))
                        {
                            lbl_titulo_ne.Text = "NUEVO RUBRO DE AVES";
                            txt_Codigo.Text = "";
                            txt_Rubro.Text = "";
                            txt_siglas.Text = "";
                            fill_grid();
                            up_ne.Update();
                            up_grid.Update();
                            mensaje("Rubro Modificado Correctamente!!", tiposAdvertencias.informacion);
                        }
                        else
                        {
                            mensaje("Error no se pudo Guardar el Rubro: ", tiposAdvertencias.error);
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "ModalNuevoEditarOcultar", "HideModal_NuevoEditar();", true);
                    }
                    else
                    {
                        mensaje("No se cargo correctamente el rubro a editar, intente de nuevo. ", tiposAdvertencias.error);
                    }
                }
            }
        }

        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["ID_RubrosAve_Eliminar"] != null)
            {
                int ID_RubroAve = (int)Session["ID_RubrosAve_Eliminar"];
                Tbl_RubroAves_obj guardarRubro = new Tbl_RubroAves_obj();

                String resultado = guardarRubro.Eliminar_Rubro(ID_RubroAve);
                if (resultado.Equals("1"))
                {
                    lbl_titulo_ne.Text = "NUEVO RUBRO DE AVES";
                    txt_Codigo.Text = "";
                    txt_Rubro.Text = "";
                    txt_siglas.Text = "";
                    fill_grid();
                    up_ne.Update();
                    up_grid.Update();
                    mensaje("Rubro Eliminado Correctamente!!", tiposAdvertencias.informacion);
                }
                else
                {
                    mensaje("Error no se pudo Eliminar el Rubro: ", tiposAdvertencias.error);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalNuevoEditarOcultar", "HideModal_NuevoEditar();", true);
            }
            else
            {
                mensaje("No se pudo cargar el ID del Rubro a eliminar, por favor intentelo una vez más", tiposAdvertencias.error);
            }
        }
    }
}