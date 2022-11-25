<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="procesosEnSistema.aspx.cs" Inherits="POULTRY.ProduccionDiaria_matadero.procesosEnSistema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Procesos en el Sistema</title>
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>PROCESOS EN EL SISTEMA</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Planta Procesadora</a></li>
                        <li class="breadcrumb-item"><a href="#">Produccion Diaria</a></li>
                        <li class="breadcrumb-item active">Procesos en el Sistema</li>
                    </ol>
                </div>
            </div>
            <small class="small text-muted">Muestra todos los Procesos en el sistema</small>
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- solid sales graph -->
    <div class="card bg-gradient-info" runat="server" id="detallesProcesoActivo" visible="false">
        <div class="card-header border-0">
            <h3 class="card-title">
                <i class="fas fa-th mr-1"></i>
                PROCESO DE MATANZA ACTIVO
            </h3>
        </div>
        <div class="card-body">
            <!-- info row -->
            <div class="row invoice-info">
                <div class="col-sm-4 invoice-col">
                    <div class="small-box bg-info" style="height=100%;">
                        <div class="inner">
                            <h3>
                                <asp:Label Text="0" ID="lbl_AvesConteoAutomatico" runat="server" /></h3>
                            <p>Aves Contadas automaticamente</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-clock"></i>
                        </div>
                        <a href="ConteoPollos.aspx" class="small-box-footer">Conteo Automatico<i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <!-- /.col -->
                <div class="col-sm-4 invoice-col">
                    Detalles Ingreso                 
                                <address>
                                    <%--<strong>John Doe</strong><br>--%>
                                    <b>Ingreso Codigo:&nbsp</b>
                                    <asp:Label Text="0" ID="lbl_IDCompra" runat="server" /><br/>
                                    <br/>
                                    <b>Granja:&nbsp</b><asp:Label Text="NombreGranja" ID="lbl_NombreGranja" runat="server" /><br/>
                                    <b>Número de Referencia:&nbsp</b>
                                    <asp:Label Text="0" ID="lbl_NumeroReferencia" runat="server" /><br/>
                                    <b>Número de Lote:&nbsp</b><asp:Label Text="#Lote" ID="lbl_NumeroLote" runat="server" /><br/>
                                    <b>Fecha Matanza:&nbsp</b>
                                    <asp:Label Text="Fecha" ID="lbl_FechaMatanza" runat="server" /><br/>
                                    <b>Precio Compra por Libra:&nbsp</b>
                                    <asp:Label Text="Fecha" ID="lbl_PrecioCompraxLibra" runat="server" /><br/>
                                </address>
                </div>
                <!-- /.col -->
                <div class="col-sm-4 invoice-col">
                    <b>Total Libras:&nbsp</b>
                    <asp:Label Text="Libras" ID="lbl_TotalLibras" runat="server" /><br />
                    <b>Total Aves por Remision:&nbsp</b>
                    <asp:Label Text="Libras" ID="lbl_AvesxRemision" runat="server" /><br />
                    <b>Raza Broilers:&nbsp</b>
                    <asp:Label Text="Raza" ID="lbl_RazaBroilers" runat="server" /><br />
                    <b>Proceso Terminado?:&nbsp</b>
                    <asp:Label Text="Si/No" ID="lbl_enProceso" runat="server" /><br />
                    <br />
                    <div class="info-box bg-dark">
                        <span class="info-box-icon bg-info"><i class="far fa-money-bill-alt"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text text-black">TOTAL COSTO LIBRAS</span>
                            <span class="info-box-number text-lg">
                                <asp:Label Text="C$ 0.00" ID="lbl_TotalCostoLibras" runat="server" /></span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <!-- /.info-box -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.card-body -->
        <div class="card-footer bg-transparent">
        </div>
        <!-- /.card-footer -->
    </div>
    <!-- /.card -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up_procesos">
        <ProgressTemplate>
            <div class="spinner-grow text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
            <div class="spinner-grow text-secondary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
            <div class="spinner-grow text-success" role="status">
                <span class="sr-only">Loading...</span>
            </div>
            <div class="spinner-grow text-danger" role="status">
                <span class="sr-only">Loading...</span>
            </div>
            <div class="spinner-grow text-warning" role="status">
                <span class="sr-only">Loading...</span>
            </div>
            <div class="spinner-grow text-info" role="status">
                <span class="sr-only">Loading...</span>
            </div>
            <div class="spinner-grow text-light" role="status">
                <span class="sr-only">Loading...</span>
            </div>
            <div class="spinner-grow text-dark" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up_procesos" runat="server">
        <ContentTemplate>
            <!-- Main content -->
            <section class="content">
                <div class="container-fluid">
                    <!-- Small boxes (Stat box) -->
                    <div class="row">
                        <div class="col-lg-4 col-md-6 col-xl-6 col-sm-12">
                            <!-- small box -->
                            <div class="small-box bg-info">
                                <div class="inner">
                                    <h3>
                                        <asp:Label Text="00" ID="lbl_TotalProcesosPendientes" runat="server" /></h3>

                                    <p>Procesos Pendientes</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-stats-bars"></i>
                                </div>
                                <div class="small-box-footer">
                                    <asp:Button Text="Cargar Procesos Pendientes" CssClass="btn btn-info" runat="server" ID="btn_ProcesosPendientes" OnClick="btn_ProcesosPendientes_Click" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-4 col-md-6 col-xl-6 col-sm-12">
                            <!-- small box -->
                            <div class="small-box bg-success">
                                <div class="inner">
                                    <h3>
                                        <asp:Label Text="00" ID="lbl_TotalprocesosTerminados" runat="server" /></h3>
                                    <p>Procesos Terminados</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-stats-bars"></i>
                                </div>
                                <div class="small-box-footer">
                                    <asp:Button Text="Cargar Procesos Terminados" CssClass="btn btn-success" runat="server" ID="btn_ProcesosTerminados" OnClick="btn_ProcesosTerminados_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.container-fluid -->
            </section>
            <!-- /.content -->
            <div class="card" runat="server" id="card_Procesos">
                <div class="card-header">
                    <h3 class="card-title">
                        <asp:Label Text="Procesos de Matanza Pendientes" ID="TituloGrid" runat="server" /></h3>
                </div>
                <!-- /.card-header -->
                <div class="card-body p-0">
                    <asp:GridView ID="gv_adquisicion" DataKeyNames="ID_CompraBroilers" runat="server" OnRowCommand="gv_adquisicion_RowCommand" EnableViewState="false" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="ID_CompraBroilers" HeaderText="ID"></asp:BoundField>
                            <asp:BoundField DataField="Nombre" HeaderText="Granja"></asp:BoundField>
                            <asp:BoundField DataField="ReferenciaComentario" HeaderText="Comentario"></asp:BoundField>
                            <asp:BoundField DataField="CodLote" HeaderText="Lote"></asp:BoundField>
                            <asp:BoundField DataField="FechaMatanza" HeaderText="Fecha Matanza"></asp:BoundField>
                            <asp:BoundField DataField="PrecioCompraxLibra" DataFormatString="{0:C2}" HeaderText="Precio Compra x Libra"></asp:BoundField>
                            <asp:BoundField DataField="TotalLibrasCompradasCalculoBascula" DataFormatString="{0:n}" HeaderText="Total Lbs Bascula"></asp:BoundField>
                            <asp:BoundField DataField="TotalAvesRemisionCompradas" HeaderText="TotalAves x Remision"></asp:BoundField>
                            <asp:BoundField DataField="TotalAvesConteoAutomatico" HeaderText="ContadorAutomatico(Aves)"></asp:BoundField>
                            <asp:BoundField DataField="NombreRaza" HeaderText="Raza"></asp:BoundField>
                            <asp:BoundField DataField="NombreMatadero" HeaderText="Matadero"></asp:BoundField>
                            <asp:BoundField DataField="CostoTotalxLibra" DataFormatString="{0:C2}" HeaderText="CostoTotal x Libra"></asp:BoundField>
                            <asp:BoundField DataField="CreadoPor" HeaderText="CreadoPor"></asp:BoundField>
                            <asp:BoundField DataField="ACargoPEsoVivo" HeaderText="A Cargo de Peso Vivo"></asp:BoundField>
                            <asp:BoundField DataField="FechaCreado" HeaderText="FechaCreado"></asp:BoundField>

                            <asp:TemplateField HeaderText="Imprimir Reporte">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_ImprimirReportes" data-toggle="tooltip" title="Imprima el reporte del proceso de compra de libras de pollo vivo" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_imprimir" CssClass="btn btn-primary form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-print"></i>
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Poner en Proceso">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_Seleccionar" data-toggle="tooltip" title="Selecciona Este Proceso de Matanza para ponerlo en modo -En Proceso-" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Seleccionar_EnEspera" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-building"></i>
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Finalizar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_Finalizar" data-toggle="tooltip" title="Haga clic aqui para finalizar el proceso y ponerlo en terminado" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_finalizarProceso" CssClass="btn btn-warning form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-square-full"></i>
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Borrar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_borrar" data-toggle="tooltip" title="Borra el proceso creado, siempre y cuando no se ha usado" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Borrar_Procesos" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash-alt"></i>
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
                <!-- /.card-body -->
            </div>
            <%--=========================================================================================================================================--%>
            <div class="modal fade" id="modalAdvertencia" tabindex="-1" role="dialog" aria-labelledby="labeAdvertencia">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="labeAdvertencia">Atencion!!</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <div class="text-center">
                                <img id="Img1" src="~/imagenes/warning.png" alt="Ok" class="img-responsive center-block" runat="server" /><hr />
                                <asp:Label ID="lbl_MsgAdvertencia" runat="server" Text="Label" Width="50%">mensaje</asp:Label>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12">
                                        <label for="btn_Continuar"></label>
                                        <asp:Button ID="btn_Continuar" runat="server" Text="Si, Continuar" CssClass="btn btn-danger form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
            <%--=========================================================================================================================================--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/footable.min.js"></script>
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_adquisicion.ClientID%>").footable();
            });
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    // $('.toast').toast('show');
                    /*
                    Se declara nuevamente footable por tabla, por que una vez hecho u posback dentro de un 
                    update panel, estos pierden las propiedades
                    */
                    //------------------------------------------
                    $("#<%=gv_adquisicion.ClientID%>").footable();
                    //------------------------------------------                                          
                }
            });
        };

        //ALERTA DE EXITO
        function success_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,                   
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'success',
                    title: mensaje
                });
            });
        };
        //ALERTA DE ERROR
        function error_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,                  
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'error',
                    title: mensaje
                });
            });
        };
        //ALERTA DE INFORMACION
        function info_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,               
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'info',
                    title: mensaje
                });
            });
        };
        //ALERTA DE ADVERTENCIA
        function advertencia_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,                 
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'warning',
                    title: mensaje
                });
            });
        };
        //ALERTA DE PREGUNTA
        function pregunta_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,                   
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'question',
                    title: mensaje
                });
            });
        };
        function ShowModalAdvertencia() {
            $('#modalAdvertencia').modal("show");
        }
    </script>
</asp:Content>
