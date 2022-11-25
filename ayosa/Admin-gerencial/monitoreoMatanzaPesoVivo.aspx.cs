using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.ProduccionPollos;
namespace POULTRY.Admin_gerencial
{
    public partial class monitoreoMatanzaPesoVivo : System.Web.UI.Page
    {
        public string PromedioPesos = "new Array([1,1],[2,1],[3,1])";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.DataBind();
            fill_Procesos();
        }
        public void fill_Procesos()
        {
            Tbl_CompraPollos_obj compraPollo = new Tbl_CompraPollos_obj();
            int opcion = int.Parse(rb_obciones.SelectedValue);
            bool EnProceso = opcion == 1 ? true : false;
            gv_adquisicion.DataSource = compraPollo.getProcesosCompra(EnProceso);
            gv_adquisicion.DataBind();
            if (gv_adquisicion.Rows.Count > 0)
            {
                this.gv_adquisicion.UseAccessibleHeader = true;
                TableCellCollection cells = gv_adquisicion.HeaderRow.Cells;

                if (!EnProceso)
                {
                    gv_adquisicion.Attributes.Remove("class");
                    gv_adquisicion.Attributes["class"] = "table table-sm  table-dark table-condensed table-bordered table-striped";
                    //gv_adquisicion.Columns[15].Visible = false;
                }
                else
                {
                    gv_adquisicion.Attributes["class"] = "table table-sm  table-light table-condensed table-bordered table-striped";
                    // gv_adquisicion.Columns[15].Visible = true;
                }


                gv_adquisicion.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                cells[1].Attributes.Add("data-breakpoints", "xs sm md");//Numero Referencia
                //cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                //cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                cells[5].Attributes.Add("data-breakpoints", "xs sm md");
                cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                cells[7].Attributes.Add("data-breakpoints", "all");
                cells[8].Attributes.Add("data-breakpoints", "all");
                cells[9].Attributes.Add("data-breakpoints", "all");
                cells[10].Attributes.Add("data-breakpoints", "all");
                cells[11].Attributes.Add("data-breakpoints", "all");
                cells[12].Attributes.Add("data-breakpoints", "all");
                cells[13].Attributes.Add("data-breakpoints", "all");
                cells[14].Attributes.Add("data-breakpoints", "all");
                gv_adquisicion.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        public void fill_ViajesRemision(int IDCompra)
        {
            Tbl_ViajesRemisionGranja_obj viajes = new Tbl_ViajesRemisionGranja_obj();
            List<Tbl_ViajesRemisionGranja_obj.ViajesRemision> remisiones = viajes.getAllViajesRemision(IDCompra);

            try
            {
                //******************** CALCULO RESUMEN                            
                decimal _librasTotal = (decimal)(remisiones.Sum(a => a.TotalLibrasxRemision));
                int _TotalPollos = (int)(remisiones.Sum(a => a.TotalAvesEnviadas));
                decimal _pesoPromedio = _librasTotal / _TotalPollos;
                int _TotalJavas = (int)(remisiones.Sum(a => a.Total_javas));
                int _TotalRemisiones = (int)remisiones.Count;
                int _TotalPesajes = new Tbl_CompraPollos_obj().getTotalPesajesxCompra(remisiones.FirstOrDefault().ID_CompraBroilers);

                int ID_CompraBroilers = (int)Session["IDCompraBroilers_Remision"];
                Tbl_CompraPollos_obj compraSelect = new Tbl_CompraPollos_obj();
                USP_CompraPollos_ProcesoxIDResult resultado = compraSelect.getProcesoCompraxID(ID_CompraBroilers);

                decimal TotalAvesContadorAut = (Convert.ToDecimal((resultado.TotalAvesConteoAutomatico)) / Convert.ToDecimal(_TotalPollos)) * 100;
                // decimal TotalAvesContadorAut = (Convert.ToDecimal((resultado.TotalAvesConteoAutomatico))/Convert.ToDecimal(resultado.TotalAvesRemisionCompradas) ) * 100;

                lbl_resumenPesoPromedio.Text = String.Format("{0:n}", _pesoPromedio) + " lbs.";
                lbl_resumenLibrasTotal.Text = String.Format("{0:n}", _librasTotal) + " lbs.";
                lbl_resumen_PollosProcesados.Text = String.Format("{0:n}", _TotalPollos) + " Aves.";
                lbl_totalPollosConteoAuto.Text = String.Format("{0:n}", (resultado.TotalAvesConteoAutomatico)) + " Aves.";
                lbl_resumen_TotalJavasPesadas.Text = string.Format("{0:n}", _TotalJavas) + " Und.";
                lbl_resumen_TotalRemisiones.Text = _TotalRemisiones + " Remin.";
                lbl_resumen_TotalPesajesBascula.Text = _TotalPesajes + " Veces.";
                lbl_PresicionContador.Text = String.Format("{0:n}", TotalAvesContadorAut) + " %.";
                //********************

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error calculando el Resumen: " + ex.Message);
            }


            //========= CREAR LOS DATOS PARA LA GRAFICA
            try
            {
                PromedioPesos = "new Array(";
                int Remisiones = 1;
                foreach (Tbl_ViajesRemisionGranja_obj.ViajesRemision _Remision in remisiones)
                {
                    decimal pesoPromedio = (decimal)(_Remision.TotalLibrasxRemision / _Remision.TotalAvesEnviadas);
                    string PesoPromedioRedondeado = String.Format("{0:0.##}", pesoPromedio);
                    PromedioPesos += "[" + Remisiones + "," + PesoPromedioRedondeado + "],";
                    Remisiones++;
                }
                if (PromedioPesos.Contains(','))
                {
                    PromedioPesos = PromedioPesos.Remove(PromedioPesos.Length - 1);
                }
                PromedioPesos += ")";
                System.Console.WriteLine(PromedioPesos);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error creando la cadena de pesos promedios: ERROR= " + ex.Message);
            }

            //=========================================

            gv_Remisiones.DataSource = remisiones;
            gv_Remisiones.DataBind();
            if (gv_Remisiones.Rows.Count > 0)
            {
                this.gv_Remisiones.UseAccessibleHeader = true;
                TableCellCollection cells = gv_Remisiones.HeaderRow.Cells;


                gv_Remisiones.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                cells[1].Attributes.Add("data-breakpoints", "all");
                cells[2].Attributes.Add("data-breakpoints", "all");
                cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                cells[5].Attributes.Add("data-breakpoints", "all");
                cells[6].Attributes.Add("data-breakpoints", "all");
                cells[7].Attributes.Add("data-breakpoints", "all");
                cells[8].Attributes.Add("data-breakpoints", "all");
                //cells[9].Attributes.Add("data-breakpoints", "all");
                cells[10].Attributes.Add("data-breakpoints", "all");
                cells[11].Attributes.Add("data-breakpoints", "all");
                cells[12].Attributes.Add("data-breakpoints", "all");
                cells[13].Attributes.Add("data-breakpoints", "all");
                cells[14].Attributes.Add("data-breakpoints", "all");
                cells[15].Attributes.Add("data-breakpoints", "all");
                cells[15].Attributes.Add("data-breakpoints", "all");
                cells[16].Attributes.Add("data-breakpoints", "all");
                //cells[17].Attributes.Add("data-breakpoints", "all");
                cells[18].Attributes.Add("data-breakpoints", "all");
                //cells[19].Attributes.Add("data-breakpoints", "all");
                gv_Remisiones.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void gv_adquisicion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_Seleccionar_Procesos")
            {
                //Primero capturas la fila
                int numFila = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
                gv_adquisicion.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDCompraBroilers = Convert.ToInt32(gv_adquisicion.SelectedValue);
                //lbl_compraSeleccionada.Text = "Proceso seleccionado " + IDCompraBroilers.ToString().PadLeft(5, '0');
                Session["IDCompraBroilers_Remision"] = IDCompraBroilers;
                CargarDatosCompra();
                //  up_monitoreo.Update();
                showToast("Proceso Cargado Correctamente, comenzara a monitorea los Datos en Caso que el proceso este activo ID de Ingreso(" + IDCompraBroilers.ToString().PadLeft(5) + ")");
            }
        }
        public void showToast(string Mensaje)
        {
            lbl_Toast.Text = Mensaje;
            ScriptManager.RegisterStartupScript(this, GetType(), "Modal_toast", "ShowtoastMessage();", true);
        }

        protected void tm_monitoreo_Tick(object sender, EventArgs e)
        {
            CargarDatosCompra();
            //  up_monitoreo.Update();
        }

        public void CargarDatosCompra()
        {
            if (Session["IDCompraBroilers_Remision"] != null)
            {
                int ID_CompraBroilers = (int)Session["IDCompraBroilers_Remision"];
                if (ID_CompraBroilers > 0)
                {
                    Tbl_CompraPollos_obj compraSelect = new Tbl_CompraPollos_obj();
                    USP_CompraPollos_ProcesoxIDResult resultado = compraSelect.getProcesoCompraxID(ID_CompraBroilers);
                    //-------------------------------
                    lbl_IDCompra.Text = " #" + ID_CompraBroilers.ToString().PadLeft(5);
                    lbl_FechaReporte.Text = DateTime.Now.ToLongDateString() + ", Hora: " + DateTime.Now.ToString("hh:mm tt");
                    lbl_NombreGranja.Text = resultado.Nombre;
                    lbl_NumeroReferencia.Text = resultado.ReferenciaComentario;
                    lbl_NumeroLote.Text = resultado.CodLote;
                    lbl_FechaMatanza.Text = resultado.FechaMatanza.ToLongDateString();
                    lbl_PrecioCompraxLibra.Text = string.Format("{0:C2}", resultado.PrecioCompraxLibra);
                    lbl_TotalLibras.Text = string.Format("{0:n}", resultado.TotalLibrasCompradasCalculoBascula);
                    lbl_AvesxRemision.Text = string.Format("{0:n}", resultado.TotalAvesRemisionCompradas);
                    lbl_AvesConteoAutomatico.Text = string.Format("{0:n}", resultado.TotalAvesConteoAutomatico);
                    lbl_RazaBroilers.Text = resultado.NombreRaza;
                    lbl_enProceso.Text = (bool)resultado.EnProceso ? "No" : "Si";
                    lbl_TotalCostoLibras.Text = string.Format("{0:C2}", resultado.CostoTotalxLibra);
                    //-------------------------------
                    fill_ViajesRemision(ID_CompraBroilers);
                }
            }
        }

        protected void btn_ocultar_Click(object sender, EventArgs e)
        {
            div_seleccionP.Visible = false;
            btn_mostrarSeleccion.Visible = true;
            //  up_1.Update();
        }

        protected void btn_mostrarSeleccion_Click(object sender, EventArgs e)
        {
            div_seleccionP.Visible = true;
            btn_mostrarSeleccion.Visible = false;
            //    up_1.Update();
        }

        protected void gv_Remisiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            CargarDatosCompra();
            //     up_monitoreo.Update();
        }

        protected void gv_Remisiones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID_ViajeRemision = (int)gv_Remisiones.DataKeys[e.Row.RowIndex].Value;
                //----------------------------------------------------------------------------------

                GridView gv_Pesajes = (GridView)e.Row.FindControl("gv_Pesajes");
                Tbl_IngresoPesoVivo_Obj ingresoPesos = new Tbl_IngresoPesoVivo_Obj();
                gv_Pesajes.DataSource = ingresoPesos.getAllPesajes(ID_ViajeRemision);
                gv_Pesajes.DataBind();

                //---------------------------------------------------------------------------
                GridView gv_JavasEnviadas = (GridView)e.Row.FindControl("gv_javasEnviadas");
                Tbl_JavasEnviadas_obj javasEnviadas = new Tbl_JavasEnviadas_obj();
                gv_JavasEnviadas.DataSource = javasEnviadas.getAllJavasEnviadas(ID_ViajeRemision);
                gv_JavasEnviadas.DataBind();
            }
        }

    }
}