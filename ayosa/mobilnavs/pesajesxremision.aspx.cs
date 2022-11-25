using accesoDatos;
using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class pesajesxremision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Session["idremision"] = int.Parse(Request.QueryString["idremision"]);
                    Session["usuario"] = Request.QueryString["usuario"].ToString();
                    Session["contrasena"] = Request.QueryString["contrasena"].ToString();
                    servicios.ServicioMatadero s = new servicios.ServicioMatadero();
                    if (!s.verifyUser((string)Session["usuario"], (string)Session["contrasena"]))
                    {
                        Response.Redirect("sinacceso.aspx");
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("sinacceso.aspx");
                }
            }

            fill_grid();

        }
        public void fill_grid()
        {
            if (Session["idremision"] != null)
            {
                int ID_ViajeRemision = (int)Session["idremision"];
                //----------------------------------------------------------------------------------                
                Tbl_IngresoPesoVivo_Obj ingresoPesos = new Tbl_IngresoPesoVivo_Obj();

                List<tbl_IngresoPesoVivo> result = ingresoPesos.getAllPesajes(ID_ViajeRemision);

                gv_Pesajes.DataSource = result;
                gv_Pesajes.DataBind();

                if (result.Count > 0)
                {
                    this.gv_Pesajes.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_Pesajes.HeaderRow.Cells;
                    gv_Pesajes.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    // cells[1].Attributes.Add("data-breakpoints", "all");
                    // cells[2].Attributes.Add("data-breakpoints", "all");
                    cells[3].Attributes.Add("data-breakpoints", "all");
                    //cells[4].Attributes.Add("data-breakpoints", "all");
                    cells[5].Attributes.Add("data-breakpoints", "all");
                    cells[6].Attributes.Add("data-breakpoints", "xs sm");
                    cells[7].Attributes.Add("data-breakpoints", "all");
                    gv_Pesajes.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        public void CalcularCambios()
        {
            if (txt_NumeroJavas.Text.Length > 0 && txt_NumeroAvesxJava.Text.Length > 0)
            {
                try
                {
                    int CantidadJavasPesadas = int.Parse(txt_NumeroJavas.Text);
                    int PollosxJavas = int.Parse(txt_NumeroAvesxJava.Text);
                    int TotalPollos = CantidadJavasPesadas * PollosxJavas;
                    tbl_IngresoPesoVivo pesajeVivo = (tbl_IngresoPesoVivo)Session["tbl_IngresoPesoVivo_Edicion"];
                    if (pesajeVivo != null)
                    {
                        decimal pesojavaVacia = (decimal)pesajeVivo.PesoxJavaVacia_libDefault;
                        decimal totalpesoPollos = (decimal)pesajeVivo.PesoJavaConPollo_Libras - (CantidadJavasPesadas * pesojavaVacia);
                        decimal PesoPromedio = totalpesoPollos / (PollosxJavas * CantidadJavasPesadas);

                        //-------------------------------------------------------------
                        lbl_TotalPollo.Text = TotalPollos.ToString();
                        lbl_PesoNetoPollos.Text = totalpesoPollos.ToString();
                        lbl_PesoNetoJavaVacia.Text = (CantidadJavasPesadas * pesojavaVacia).ToString();
                        lbl_PesoPromedio.Text = string.Format("{0:n}", PesoPromedio);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        protected void gv_Pesajes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_Editar")
            {
                gv_Pesajes.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDPesaje = Convert.ToInt32(gv_Pesajes.SelectedValue);
                tbl_IngresoPesoVivo pesajeVivo = new Tbl_IngresoPesoVivo_Obj().getPesajesxID(IDPesaje);
                Session["tbl_IngresoPesoVivo_Edicion"] = (tbl_IngresoPesoVivo)pesajeVivo;

                //-------------------------------------------------------------------------
                lbl_NumeroPesaje.Text = "# " + pesajeVivo.NumPesaje;
                txt_NumeroJavas.Text = pesajeVivo.CantidadJavasPesadas.ToString();
                txt_NumeroAvesxJava.Text = pesajeVivo.PollosxJava.ToString();
                lbl_TotalPollo.Text = pesajeVivo.TotalPollos.ToString();
                lbl_PesoJavasConPollos.Text = pesajeVivo.PesoJavaConPollo_Libras.ToString();
                lbl_PesoJavaVacia.Text = pesajeVivo.PesoxJavaVacia_libDefault.ToString();
                lbl_PesoNetoJavaVacia.Text = pesajeVivo.TotalPesoJavaVacia_libra.ToString();
                lbl_PesoNetoPollos.Text = pesajeVivo.PesoNetoPollosLibra.ToString();
                lbl_PesoPromedio.Text = string.Format("{0:n}", pesajeVivo.PesoPromedioxPollo_lib);


                up_principal.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalEditar", "ShowModal_Editar();", true);
            }
            else if (e.CommandName == "cmd_Eliminar")
            {
                gv_Pesajes.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDPesaje = Convert.ToInt32(gv_Pesajes.SelectedValue);

                tbl_IngresoPesoVivo pesajeVivo = new Tbl_IngresoPesoVivo_Obj().getPesajesxID(IDPesaje);
                Session["tbl_IngresoPesoVivo_Edicion"] = (tbl_IngresoPesoVivo)pesajeVivo;

                modalAdvertencia("Esta seguro de quesea eliminar este pesaje para esta remision?<br/>¿Desea continuar?");
            }
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (txt_NumeroJavas.Text.Length > 0 && txt_NumeroAvesxJava.Text.Length > 0)
            {
                int CantidadJavasPesadas = int.Parse(txt_NumeroJavas.Text);
                int PollosxJavas = int.Parse(txt_NumeroAvesxJava.Text);
                Tbl_IngresoPesoVivo_Obj IngresoPesoVivo = new Tbl_IngresoPesoVivo_Obj();
                tbl_IngresoPesoVivo pesajeVivo = (tbl_IngresoPesoVivo)Session["tbl_IngresoPesoVivo_Edicion"];
                if (pesajeVivo != null)
                {
                    int IDCompra = IngresoPesoVivo.getIDCompraPollosx_IDRemision(pesajeVivo.ID_ViajesRemision);
                    if (IDCompra > 0)
                    {
                        string resul = IngresoPesoVivo.ModificarPesaje(pesajeVivo, CantidadJavasPesadas, PollosxJavas, IDCompra);
                        if (resul.Equals("1"))
                        {
                            fill_grid();
                            up_principal.Update();
                            up_actualizarEditar.Update();
                            modalInfo("Se ha Modificado el Peso Correctamente", tiposAdvertencias.exito);
                        }
                        else
                        {
                            modalInfo("Ha Ocurrido un Error " + resul, tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        modalInfo("No se puede obtener el ID del Proceso relacionado a este pesaje (Resultado= " + IDCompra + ")", tiposAdvertencias.error);
                    }
                }
                else
                {
                    modalInfo("Seleccione un pesaje por favor", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                modalInfo("No deje los campos vacios", tiposAdvertencias.advertencia);
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
        protected void btn_Regarcar1_Click(object sender, EventArgs e)
        {
            CalcularCambios();
            up_actualizarEditar.Update();
        }

        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["tbl_IngresoPesoVivo_Edicion"] != null)
            {
                tbl_IngresoPesoVivo pesajeVivo = (tbl_IngresoPesoVivo)Session["tbl_IngresoPesoVivo_Edicion"];
                if (pesajeVivo != null)
                {
                    int IDPesaje_eliminar = Convert.ToInt32(Session["IDPesaje_eliminar"]);
                    Tbl_IngresoPesoVivo_Obj p = new Tbl_IngresoPesoVivo_Obj();

                    int IDCompra = p.getIDCompraPollosx_IDRemision(pesajeVivo.ID_ViajesRemision);
                    string resul = p.eliminarPesajeRemision(pesajeVivo, IDCompra);
                    if (resul.Equals("1"))
                    {
                        fill_grid();
                        modalInfo("Pesaje Eliminado con Exito!", tiposAdvertencias.exito);
                    }
                    else
                    {
                        modalInfo("Error eliminando el pesaje ERROR:" + resul, tiposAdvertencias.exito);
                    }

                }
            }
            else
            {
                modalInfo("Ocurrio un problema interno, favor volver a intentarlo, si el problema persiste comuniquese con su desarrollador", tiposAdvertencias.error);
            }
        }
        private void modalAdvertencia(String msg)
        {
            lbl_MsgAdvertencia.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalAdvertencia", "ShowModalAdvertencia();", true);
        }
    }
}