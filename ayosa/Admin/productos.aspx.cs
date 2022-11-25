using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos.Catalogos;
using accesoDatos;
using System.IO;
namespace POULTRY 
{
    public partial class productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dr_Categoria.DataSource = new Cat_CategoriaProducto_obj().getAllCategoriaProducto();
                dr_Categoria.DataTextField = "NombreCategoriaProducto";
                dr_Categoria.DataValueField = "ID_CategoriaProducto";
                dr_Categoria.DataBind();
                //---------------------------------------------------------------------------------
                dr_Medida.DataSource = new Cat_MedidaProducto_obj().getAllMedidaProducto();
                dr_Medida.DataTextField = "MedidaProducto";
                dr_Medida.DataValueField = "ID_Medida";
                dr_Medida.DataBind();
                Session["prod_Activos"] = null;
                Session["OrigenAdvertencia_tr"] = null;
            }
            fillData();

        }
        public void fillData()
        {
            bool Activos = true;
            if (Session["prod_Activos"] != null)
            {
                Activos = (bool)Session["prod_Activos"];
            }
            gv_productos.DataSource = new Tbl_Producto_obj().getAllProdcutosResult_USP(Activos);
            gv_productos.DataBind();
            if (gv_productos.Rows.Count > 0)
            {
                this.gv_productos.UseAccessibleHeader = true;
                TableCellCollection cells = gv_productos.HeaderRow.Cells;
                gv_productos.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //cells[1].Attributes.Add("data-class", "expand");
                //cells[3].Attributes.Add("data-hide", "phone");
                //cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                // cells[1].Attributes.Add("data-breakpoints", "all");
                cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                cells[5].Attributes.Add("data-breakpoints", "xs sm md");
                //cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                //cells[7].Attributes.Add("data-breakpoints", "xs sm md");
                // cells[4].Attributes.Add("data-breakpoints", "all");
                gv_productos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (dr_Categoria.Items.Count <= 0 && dr_Medida.Items.Count <= 0)
            {
                advertenciaMsg(true, tiposAdvertencias.advertencia, "Advertencia!!", "No hay elementos en los controles categoria y medida, por favor verifique que ya se han ingresado con anterioridad y vuelva a intentarlo", "Las listas estan vacias!!");
            }
            else
            {
                String RutaImagen=UploadImagen();
                string nombreP = txt_producto.Text;
                int idCategoria = int.Parse(dr_Categoria.SelectedValue);
                int idmedida = int.Parse(dr_Medida.SelectedValue);

                bool activo = chk_activo.Checked;

                Tbl_Producto nProdcuto = new Tbl_Producto();
                nProdcuto.NombreProducto = nombreP;
                nProdcuto.ID_CategoriaProducto = idCategoria;
                nProdcuto.ID_Medida = idmedida;
                nProdcuto.Activo = activo;
                nProdcuto.URLImagen = RutaImagen;
                Tbl_Producto_obj producto_obj = new Tbl_Producto_obj();
                string res;
                if (Session["OrigenAdvertencia_tr"] != null)
                {
                    if (!Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Editar"))
                    {
                        res = producto_obj.guardarProducto(nProdcuto);
                        if (res.Equals("ok"))
                        {
                            modalInfo("El nuevo Producto Fue guardado de manera correcta!", tiposAdvertencias.exito);
                        }
                        else
                        {
                            modalInfo("Error al Guardar el producto!!", tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        int idTrabajador = Convert.ToInt32(Session["ID_producto"]);
                        res = producto_obj.updateProducto(nProdcuto, idTrabajador);
                        if (res.Equals("ok"))
                        {
                            modalInfo("Actualizacion completada con exito!!", tiposAdvertencias.exito);
                        }
                        else
                        {
                            modalAdvertencia("Error al Actualizar el producto");
                        }
                    }
                }
                else
                {
                    res = producto_obj.guardarProducto(nProdcuto);
                }
                fillData();
                Response.Redirect(Request.Url.ToString(), false);
            }
        }
        public void advertenciaMsg(bool visible, tiposAdvertencias tipoa, string cabeza, string cuerpo, string pie)
        {
            advertenciaAlert.Visible = visible;
            switch (tipoa)
            {

                case tiposAdvertencias.informacion:
                    advertenciaAlert.Attributes["class"] = "alert alert-info";
                    break;
                case tiposAdvertencias.exito:
                    advertenciaAlert.Attributes["class"] = "alert alert-success";
                    break;
                case tiposAdvertencias.advertencia:
                    advertenciaAlert.Attributes["class"] = "alert alert-warning";
                    break;
                case tiposAdvertencias.error:
                    advertenciaAlert.Attributes["class"] = "alert alert-danger";
                    break;
                default:
                    advertenciaAlert.Attributes["class"] = "alert alert-info";
                    break;
            }
            lbl_advertenciaHeader.Text = cabeza;
            lbl_CuerpoAdvertencia.Text = cuerpo;
            lbl_piesAdvertencia.Text = pie;
        }

        protected void dr_productop_page_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow pagerRow = gv_productos.BottomPagerRow;
                // Recupera el control DropDownList...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("dr_productop_page");
                // Se Establece la propiedad PageIndex para visualizar la página seleccionada...           
                gv_productos.PageIndex = pageList.SelectedIndex;
                //------------------------------
                fillData();
            }
            catch (Exception)
            {
            }
        }

        protected void gv_productos_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gv_productos.BottomPagerRow;
            if (gv_productos.Rows.Count > 0)
            {
                // Recupera los controles DropDownList y label...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("dr_productop_page");
                Label pageLabel = (Label)pagerRow.Cells[0].FindControl("currentPage_Producto");
                if ((pageList != null))
                {
                    // Se crean los valores del DropDownList tomando el número total de páginas... 
                    int i = 0;
                    for (i = 0; i <= gv_productos.PageCount - 1; i++)
                    {
                        // Se crea un objeto ListItem para representar la �gina...
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());
                        if (i == gv_productos.PageIndex)
                        {
                            item.Selected = true;
                        }
                        // Se añade el ListItem a la colección de Items del DropDownList...
                        pageList.Items.Add(item);
                    }
                }
                if ((pageLabel != null))
                {
                    // Calcula el nº de �gina actual...
                    int currentPage = gv_productos.PageIndex + 1;
                    // Actualiza el Label control con la �gina actual.
                    pageLabel.Text = "Página " + currentPage.ToString() + " de " + gv_productos.PageCount.ToString();
                }
            }
        }

        protected void gv_productos_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "eliminar")
            {
                gv_productos.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Session["OrigenAdvertencia_tr"] = "cmd_Eliminar";
                Session["ID_producto"] = Convert.ToInt32(gv_productos.SelectedValue);
                modalAdvertencia("Este Producto se eliminara por completo del sistema siempre y cuando no se ha usado en ventas<br/>¿Desea continuar?");
            }
            //if (e.CommandName == "activarVacaciones")
            //{

            //    gv_trabajadores.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //    Tbl_trabajadorSucursal trabajadores = new Tbl_TrabajadoresDA().FindeById((int)gv_trabajadores.SelectedValue);
            //    if (trabajadores.Fin_Laborales == null)
            //    {
            //        Session["OrigenAdvertencia_tr"] = "cmd_ActivarVacaciones";
            //        Session["ID_Trabajador"] = Convert.ToInt32(gv_trabajadores.SelectedValue);
            //        modalAdvertencia("Tenga en cuenta que una vez que se active el servicio de 'CALCULO DE VACACIONES'<br/>No podra modificar ningun dato de este trabajador, solo lo podra eliminar<br/>¿Desea continuar?");
            //    }
            //    else
            //    {
            //        modalInfo("No pude activar el calculo de vacaciones por que el trabajador tiene fecha de fin de labores", "info");
            //    }
            //}
            if (e.CommandName == "actualizar")
            {

                gv_productos.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Tbl_Producto prodEdit = new Tbl_Producto_obj().findById((int)gv_productos.SelectedValue);
                if (prodEdit != null)
                {
                    dr_Medida.SelectedValue = prodEdit.ID_Medida.ToString();
                    dr_Categoria.SelectedValue = prodEdit.ID_CategoriaProducto.ToString();

                    Session["OrigenAdvertencia_tr"] = "cmd_Editar";
                    Session["ID_producto"] = Convert.ToInt32(gv_productos.SelectedValue);

                    txt_producto.Text = prodEdit.NombreProducto;
                    //txt_Coste.Text = prodEdit.CosteProducto.ToString();
                    //txt_Precio.Text = prodEdit.PrecioProducto.ToString();
                    chk_activo.Checked = (bool)prodEdit.Activo;

                    btn_Guardar.Attributes.Add("class", "btn btn-warning form-control");
                    btn_Guardar.Text = "Guardar los Cambios";

                    ScriptManager.RegisterStartupScript(this, GetType(), "modalEditar", "ShowModalNuevo();", true);
                }
            }

            //        dr_sucursal.SelectedValue = trabajadores.Id_Sucursal.ToString();
            //        txt_nombreT.Text = trabajadores.Nombre_Completo;

            //        txt_celular.Text = Convert.ToString(trabajadores.Celular);
            //        trabajadores.Calculo_vacaciones = false;
            //        chk_activo.Checked = trabajadores.Activo;
            //        txt_Cedula.Text = trabajadores.Cedula;

            //        Session["ID_Trabajador"] = Convert.ToInt32(gv_trabajadores.SelectedValue);
            //        modalForm("Editar");
            //        btn_Guardar.Visible = false;
            //        btn_Editar.Visible = true;
            //    }
            //    else
            //    {
            //        modalInfo("No pude editar el trabajador por que tiene fecha de fin de labores", "warning");
            //    }
            //}
            //if (e.CommandName == "editarFinLabores")
            //{
            //    gv_trabajadores.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //    Session["ID_Trabajador"] = Convert.ToInt32(gv_trabajadores.SelectedValue);
            //    Tbl_trabajadorSucursal trabajadores = new Tbl_TrabajadoresDA().FindeById((int)gv_trabajadores.SelectedValue);
            //    if (trabajadores.Fin_Laborales != null)
            //    {
            //        if (Convert.ToString(trabajadores.Fin_Laborales).Length > 0)
            //        {
            //            fechaeditar3 = fechaParse((DateTime)trabajadores.Fin_Laborales);
            //        }
            //    }
            //    if (fechaeditar3.Length <= 0)
            //    {
            //        modalFinLabores();
            //    }
            //    else
            //    {
            //        modalInfo("El trabajador ya tiene una fecha de fin de labores, por tanto no puede modificar sus datos", "warning");
            //    }
            //}
        }
        private void modalAdvertencia(String msg)
        {
            lbl_MsgAdvertencia.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalAdvertencia", "ShowModalAdvertencia();", true);
        }
        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["OrigenAdvertencia_tr"] != null)
            {
                // lbl_MsgAdvertencia.Text = "Producto eliminado correctamente";
                // ScriptManager.RegisterStartupScript(this, GetType(), "OcultarModalAdvertencia", "ShowModalAdvertencia();", true);
                //Response.Redirect(Request.Url.ToString(), false);
                if (Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Eliminar"))
                {
                    int idTrabajador = Convert.ToInt32(Session["ID_producto"]);
                    string result = new Tbl_Producto_obj().EliminarProducto(idTrabajador);
                    if (result.Equals("ok"))
                    {
                        modalInfo("El producto fue eliminado correctamente   ", tiposAdvertencias.informacion);
                    }
                    else
                    {
                        modalInfo("Error   " + result
                            , tiposAdvertencias.error);
                    }
                    fillData();
                }
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

        protected void btnRecargar_Click(object sender, EventArgs e)
        {
            // string selected = Request.Form["chk_Mostrar_activos"];
            Session["prod_Activos"] = (bool)false;
            fillData();
        }

        protected void btn_Actvos_Click(object sender, EventArgs e)
        {
            Session["prod_Activos"] = (bool)true;
            fillData();
        }
        public String UploadImagen()
        {
            String url = "http://ayemadeoro.com/imagenes/";
            try
            {
                if (fileupload.PostedFile != null)
                {
                    string name = Server.MapPath("~/imagenes/" + fileupload.PostedFile.FileName);
                    fileupload.PostedFile.SaveAs(Server.MapPath("~/imagenes/" + fileupload.PostedFile.FileName));
                    return url + fileupload.PostedFile.FileName;
                }
                else
                {
                    return "http://ayemadeoro.com/imagenes/no-photos.png";
                }

            }
            catch (Exception)
            {
                return "http://ayemadeoro.com/imagenes/no-photos.png";
            }
            // Display the result of the upload.
            //frmConfirmation.Visible = true;
        }       
    }
}