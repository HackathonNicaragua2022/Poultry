using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos.ProduccionPollos;
using accesoDatos;
using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using accesoDatos.ProduccionPollos.Configuracion;
using System.Globalization;
using ultil;
using accesoDatos.Granja;
namespace POULTRY.Granja
{
    public partial class RemisionAMatadero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                row_NuevaRemisions.Visible = false;
                //Fill_Galeras();
            }
            fill_Procesos(true, false);
            Fill_avesxJava();
        }
        //public void Fill_Galeras()
        //{
        //    if (Session["IDCompraBroilers_Remision"] != null)
        //    {
        //        int iDCompraBroilers_Remision = (int)Session["IDCompraBroilers_Remision"];

        //        Tbl_CompraPollos_obj compraP = new Tbl_CompraPollos_obj();

        //        int IDGranja = compraP.getCompraPolloxID(iDCompraBroilers_Remision).ID_Granjas;

        //        //Tbl_Galeras_obj galeras = new Tbl_Galeras_obj();
        //        //dr_Galera.DataSource = galeras.getAllGalerasxID(IDGranja);
        //        //dr_Galera.DataTextField = "NumeroOrden";
        //        //dr_Galera.DataValueField = "ID_Galeras";
        //        //dr_Galera.DataBind();
        //    }
        //}
        public void fill_Procesos(bool EnProceso, bool hideSelectButton)
        {
            Tbl_CompraPollos_obj compraPollo = new Tbl_CompraPollos_obj();
            gv_adquisicion.DataSource = compraPollo.getProcesosCompra(EnProceso);
            gv_adquisicion.DataBind();
            if (gv_adquisicion.Rows.Count > 0)
            {
                this.gv_adquisicion.UseAccessibleHeader = true;
                TableCellCollection cells = gv_adquisicion.HeaderRow.Cells;

                if (hideSelectButton)
                {
                    gv_adquisicion.Attributes.Remove("class");
                    gv_adquisicion.Attributes["class"] = "table table-sm  table-dark table-condensed table-bordered table-striped";
                    gv_adquisicion.Columns[13].Visible = false;
                    row_NuevaRemisions.Visible = false;
                }
                else
                {
                    //row_NuevaRemisions.Visible = true;
                    gv_adquisicion.Attributes["class"] = "table table-sm  table-light table-condensed table-bordered table-striped";
                    gv_adquisicion.Columns[13].Visible = true;
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
                //cells[13].Attributes.Add("data-breakpoints", "all");
                //cells[14].Attributes.Add("data-breakpoints", "all");
                gv_adquisicion.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        public void fillInventarioGalera(int IDGalera)
        {
            InventarioGaleras_obj iGalera = new InventarioGaleras_obj();
            Tbl_InventarioGalera inventario = iGalera.get_InventarioGalera(IDGalera);
            //------------------------------------
            Session["InventarioSeleccionado"] = inventario;
            lbl_TotalPolloIngresado.Text = string.Format("{0:n}", inventario.TotalIngreso) + " Pollitos";
            lbl_mortalidadr.Text = string.Format("{0:n}", inventario.TotalMortalidad) + " Aves";
            lbl_TotalRemisiones.Text = string.Format("{0:n}", inventario.TotalSalidas_RemisionesMatadero) + " Aves";
            lbl_TotalPolloEnPie.Text = string.Format("{0:n}", inventario.TotalEnPie) + " Aves";
            lbl_EdadDias.Text = string.Format("{0:n}", inventario.EdadLote_Dias) + " Días";
            lbl_EdadSemanas.Text = string.Format("{0:n}", ((double)inventario.EdadLote_Dias / 7)) + " Semanas";
            lbl_PesoPromedioLote.Text = string.Format("{0:n}", ((double)inventario.PesoPromedio / 453.59)) + " Libras  (" + inventario.PesoPromedio + "gr)";
            lbl_PorcentajeMortalidad.Text = string.Format("{0:n}", ((double)inventario.TotalMortalidad / (double)inventario.TotalIngreso) * 100) + " %";
            lbl_TotalLibrasPesadasMatadero.Text = string.Format("{0:n}", inventario.TotalLibrasPesoVivoMatanza) + " Libras";
            lbl_FechaIngresoGalera.Text = string.Format("{0:D}", inventario.Fecha_IngresoGalera);

        }
        protected void gv_adquisicion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_Seleccionar_Procesos")
            {
                //Primero capturas la fila
                int numFila = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
                gv_adquisicion.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDCompraBroilers = Convert.ToInt32(gv_adquisicion.SelectedValue);
                lbl_compraSeleccionada.Text = "Proceso seleccionado " + IDCompraBroilers.ToString().PadLeft(5, '0');
                Session["IDCompraBroilers_Remision"] = IDCompraBroilers;
                //Fill_Galeras();
                row_NuevaRemisions.Visible = true;
                Tbl_CompraPollos_obj compraPollos = new Tbl_CompraPollos_obj();
                int IdGalera = compraPollos.getIDGalera_CompraPolloID(IDCompraBroilers);
                fillInventarioGalera(IdGalera);
            }
        }
        public void showToast(string Mensaje)
        {
            lbl_Toast.Text = Mensaje;
            ScriptManager.RegisterStartupScript(this, GetType(), "Modal_toast", "ShowtoastMessage();", true);
        }
        protected void rb_obciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int opcion = int.Parse(rb_obciones.SelectedValue);
                switch (opcion)
                {
                    case 1://Seleccionar todo los procesos activos                        
                        fill_Procesos(true, false);
                        // Fill_Galeras();
                        break;
                    case 2: // selccionar todos los procesos terminados                        
                        fill_Procesos(false, true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                showToast("Error al seleccionar el valor" + ex.Message);
            }
        }

        protected void btn_GuardarNuevaRemision_Click(object sender, EventArgs e)
        {
            //if (dr_Galera.Items.Count > 0)
            //{
            if (Session["IDCompraBroilers_Remision"] != null)
            {
                if (
                    new TextboxValidator().validarLargoCadena_mayorCero(
                    txt_NombreConductor,
                txt_PlacaCamion,
                txt_HoraAyuno,
                txt_Destino,
                txt_HoraSalidaGranja,
                txt_EntregaConforme))//txt_EdadDias.Text.Length > 0 &&
                {
                    if (Session["InventarioSeleccionado"] != null)
                    {
                        Tbl_InventarioGalera inventario = (Tbl_InventarioGalera)Session["InventarioSeleccionado"];
                        string NombreConductor = txt_NombreConductor.Text;
                        string placaCamion = txt_PlacaCamion.Text;
                        string HoraAyuno = txt_HoraAyuno.Text;
                        //La edad se restara menos 1 por reglas internas, por tiempo de ayuno del ave
                        int EdadDias = (int)(inventario.EdadLote_Dias - 1);// txt_EdadDias.Text;
                        string Destino = txt_Destino.Text;
                        string HoraSalidaGranja = txt_HoraSalidaGranja.Text;
                        string EntregaConforme = txt_EntregaConforme.Text;
                        int IDGalera = inventario.ID_Galeras;//int.Parse(dr_Galera.SelectedValue.ToString());
                        int iDCompraBroilers_Remision = (int)Session["IDCompraBroilers_Remision"];
                        int IDInventarioBroilers = inventario.ID_InventarioBroilers;
                        if (Session["AvesxJava_list"] != null)
                        {
                            List<Tbl_JavasEnviadas> javasEnviadas = (List<Tbl_JavasEnviadas>)Session["AvesxJava_list"];
                            try
                            {

                                Tbl_ViajesRemisionGranja NuevaRemision = new Tbl_ViajesRemisionGranja();
                                NuevaRemision.ID_CompraBroilers = iDCompraBroilers_Remision;
                                NuevaRemision.ID_InventarioBroilers = IDInventarioBroilers;
                                NuevaRemision.Fecha = DateTime.Now;
                                //NuevaRemision.NumeroRemision = new Tbl_ViajesRemisionGranja_obj().getNumeroRemisionxIDCompraBroilers(iDCompraBroilers_Remision);
                                NuevaRemision.NumeroViaje = new Tbl_ViajesRemisionGranja_obj().getNumeroViajexIDCompraBroilers(iDCompraBroilers_Remision);
                                NuevaRemision.NombreConductor = NombreConductor;
                                NuevaRemision.PlacaCamion = placaCamion;
                                //NuevaRemision.Total_javas = int.Parse(totalJavas); // Se Calculara automaticamente

                                CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                                NuevaRemision.HoraAyuno = DateTime.Parse(HoraAyuno, provider);//DateTime.ParseExact(HoraAyuno, "mm/dd/yyyy hh:mm aa", provider);
                                NuevaRemision.Edad = EdadDias;
                                NuevaRemision.ID_Galeras = IDGalera;
                                NuevaRemision.Destino = Destino;
                                //NuevaRemision.PollosxJaba = int.Parse(PollosPorJava);
                                String fechaHoraSalidaGranja = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + " " + HoraSalidaGranja;
                                NuevaRemision.HoraSalidaGranja = DateTime.Parse(fechaHoraSalidaGranja, provider);// DateTime.ParseExact(fechaHoraSalidaGranja, "mm/dd/yyyy hh:mm aa", provider);
                                NuevaRemision.HoraLlegadaPlanta = DateTime.Parse(fechaHoraSalidaGranja, provider);//DateTime.ParseExact(fechaHoraSalidaGranja, "mm/dd/yyyy hh:mm aa", provider);
                                NuevaRemision.EntregaConforme = EntregaConforme;

                                var membershipUser = System.Web.Security.Membership.GetUser();
                                Guid userId = (Guid)membershipUser.ProviderUserKey;

                                NuevaRemision.RecibidoPor = userId;
                                NuevaRemision.EstadoSaludAve = txt_EstadoSaludAves.Text;
                                NuevaRemision.NUFDMADEAS = txt_Nufdmadeas.Text;
                                NuevaRemision.TotalLibrasxRemision = 0;
                                NuevaRemision.Finalizada = false;

                                Tbl_ViajesRemisionGranja_obj GuardarViaje = new Tbl_ViajesRemisionGranja_obj();
                                int Result = GuardarViaje.NuevaRemision(NuevaRemision, javasEnviadas);
                                if (Result > 0)
                                {
                                    //txt_NombreConductor.Text;
                                    //txt_PlacaCamion.Text;                                
                                    //txt_HoraAyuno.Text;
                                    //txt_EdadDias.Text;
                                    //txt_Destino.Text;                                
                                    txt_HoraSalidaGranja.Text = string.Empty; ;
                                    //txt_EntregaConforme.Text;
                                    Session["AvesxJava_list"] = null;
                                    new TextboxValidator().setFormControl(txt_NombreConductor, txt_PlacaCamion, txt_HoraAyuno, txt_Destino, txt_HoraSalidaGranja, txt_EntregaConforme);
                                    showToast("La nueva remision se ha Guardado Correctamente!!");
                                    row_NuevaRemisions.Visible = false;
                                }
                                else
                                {
                                    showToast("NO se ha Podido guardar la nueva Remision, revice los datos he intente nuevamente");
                                }
                            }
                            catch (Exception ex)
                            {
                                showToast("Hay un problema y no se puede guardar la remision, favor verifique que no a puesto una fecha o horas en un formato incorrecto</hr> " + ex.Message);
                            }
                        }
                        else
                        {
                            string alerta = " <div class=\"info-box mb-3 bg-warning\"><span class=\"oi oi-warning\" title=\"warning\" aria-hidden=\"true\"></span><div class=\"info-box-content\"><span class=\"info-box-text\">Advertencia!</span><span class=\"info-box-number\">Ingrese el numero de javas y aves por Javas en este viaje para continuar</span></div><!-- /.info-box-content --></div>";
                            showToast("Hacen falta campos por completar </br>" + alerta);
                        }
                    }
                    else
                    {
                        showToast("No se logro cargar correctamente los datos del Inventario de galera seleccionado");
                    }
                }
                else
                {
                    showToast("Hacen falta campos por completar, por favor verifique los campos marcados con <strong>*</strong> y acontinuacion completelos");
                }
            }
            else
            {
                showToast("Seleccione un proceso primero");
            }
            //}
            //else
            //{
            //    showToast("No hay Galeras en la lista, seleccione un proceso primero");

            //}
        }

        protected void btn_AgregarAvesxJava_Click(object sender, EventArgs e)
        {
            if (txt_AvesporJava.Text.Length > 0 && txt_javas.Text.Length > 0)
            {
                if (Session["AvesxJava_list"] != null)
                {
                    List<Tbl_JavasEnviadas> javasEnviadas = (List<Tbl_JavasEnviadas>)Session["AvesxJava_list"];

                    int cantidadJavas = int.Parse(txt_javas.Text);
                    int avesxJava = int.Parse(txt_AvesporJava.Text);
                    int TotalAves = cantidadJavas * avesxJava;

                    //-------------------------------------------------------
                    Tbl_JavasEnviadas nuevaEnviadas = new Tbl_JavasEnviadas();
                    nuevaEnviadas.ID_JavasEnviadas = javasEnviadas.Count() + 1;
                    nuevaEnviadas.Cantidad_Javas = cantidadJavas;
                    nuevaEnviadas.PollosPorJavas = avesxJava;
                    nuevaEnviadas.SubTotalAves = TotalAves;
                    javasEnviadas.Add(nuevaEnviadas);
                    //-------------------------------------------------------
                    Fill_avesxJava();

                }
                else
                {
                    List<Tbl_JavasEnviadas> javasEnviadas = new List<Tbl_JavasEnviadas>();
                    int cantidadJavas = int.Parse(txt_javas.Text);
                    int avesxJava = int.Parse(txt_AvesporJava.Text);
                    int TotalAves = cantidadJavas * avesxJava;

                    //-------------------------------------------------------
                    Tbl_JavasEnviadas nuevaEnviadas = new Tbl_JavasEnviadas();
                    nuevaEnviadas.ID_JavasEnviadas = javasEnviadas.Count() + 1;
                    nuevaEnviadas.Cantidad_Javas = cantidadJavas;
                    nuevaEnviadas.PollosPorJavas = avesxJava;
                    nuevaEnviadas.SubTotalAves = TotalAves;
                    javasEnviadas.Add(nuevaEnviadas);

                    Session["AvesxJava_list"] = javasEnviadas;
                    //-------------------------------------------------------
                    Fill_avesxJava();
                }
            }
            else
            {
                showToast("Para agregar a la lista ingrese los valores correspondientes en Total Aves x Java y Numero Javas");
            }
        }
        public void Fill_avesxJava()
        {
            if (Session["AvesxJava_list"] != null)
            {
                List<Tbl_JavasEnviadas> javasEnviadas = (List<Tbl_JavasEnviadas>)Session["AvesxJava_list"];
                lbl_TotalAvesViaje.Text = string.Format("{0:n}", javasEnviadas.Sum(s => s.SubTotalAves)) + " Aves";
                gv_avesxjava.DataSource = javasEnviadas;
                gv_avesxjava.DataBind();
                if (gv_avesxjava.Rows.Count > 0)
                {
                    this.gv_avesxjava.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_avesxjava.HeaderRow.Cells;
                    gv_avesxjava.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    cells[3].Attributes.Add("data-breakpoints", "all");
                    gv_avesxjava.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void gv_avesxjava_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "remover")
            {
                //Primero capturas la fila
                int numFila = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
                gv_avesxjava.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int idAvesxJava = Convert.ToInt32(gv_avesxjava.SelectedValue);
                List<Tbl_JavasEnviadas> javasEnviadas = (List<Tbl_JavasEnviadas>)Session["AvesxJava_list"];
                javasEnviadas.RemoveAll(r => r.ID_JavasEnviadas == idAvesxJava);
                Fill_avesxJava();
            }
        }
    }
}