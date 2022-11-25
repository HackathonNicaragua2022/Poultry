<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterSedeCentral.Master" AutoEventWireup="true" CodeBehind="CancelacionFacturasComedor.aspx.cs" Inherits="POULTRY.Admin.CancelacionFacturasComedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Cancelacion de Facturas</title>
    <!-- daterange picker -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/daterangepicker/daterangepicker.css" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Cancelacion de Facturas Comedor</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Comedor POULTRY </a></li>
                        <li class="breadcrumb-item active">Cancelacion de Facturas</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up_principal">
            <ProgressTemplate>
                <div class="align-content-center">                    
                    <h2><marquee>Espere por favor.. (Recolectando Datos)</marquee></h2>
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
                    <div class="spinner-grow text-dark" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="up_principal" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="text">Seleccione el Periodo de Facturacion</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">
                                        <i class="far fa-calendar-alt"></i>
                                    </span>
                                </div>
                                <asp:TextBox runat="server" aria-describedby="textHelpBlock" type="text" class="form-control float-right" ID="txt_rangofecha" />
                            </div>
                            <span id="textHelpBlock" class="form-text text-muted">seleccione el periodo de facturación para cargar los empleados y sus facturas</span>
                            <!-- /.input group -->
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label for="btn_cargar">Cargar Facturas</label>
                        <div class="form-group">
                            <asp:Button ID="btn_cargar" Text="Cargar Facturas" runat="server" class="btn btn-primary  form-control" aria-describedby="btncargar" OnClick="btn_cargar_Click" />
                            <span id="btncargar" class="form-text text-muted">Cargar Rango Seleccionado</span>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label for="btn_cargar">Cargar Todo</label>
                        <div class="form-group">
                            <asp:Button ID="btn_CargarTodas" Text="Cargar Todas las Facturas" runat="server" class="btn btn-primary form-control" aria-describedby="btncargar" OnClick="btn_CargarTodasLasFacturas_Click" />
                            <span id="sp_CargarTodas" class="form-text text-muted">Cargar todas las Facturas pendientes</span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-sm-12">
                        <!-- small card -->
                        <div class="small-box bg-warning">
                            <div class="inner">
                                <asp:Button Text="Cancelar Todas las Facturas en el Rango" CssClass="btn btn-outline-danger btn-lg" ID="btn_CancelarFacturasRango" runat="server" OnClick="btn_CancelarFacturasRango_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                <p>Cancela Todas las Facturas en el rango de fechas seleccionado, se estableceran en Canceladas</p>
                            </div>
                            <div class="icon">
                                <i class="fas fa-calendar"></i>
                            </div>
                            <div class="small-box-footer">Cancela todas las factura de los Trabajadores</div>
                            </a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-6 col-sm-12">
                        <!-- small card -->
                        <div class="small-box bg-danger">
                            <div class="inner">
                                <asp:Button Text="Cancelar todas las Facturas en el sistema" CssClass="btn btn-outline-warning btn-lg" ID="btn_CancelarTodo" runat="server" OnClick="btn_CancelarTodo_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                <p>Cancela Todas las Facturas de credito en el sistema sin importar el rango de fecha</p>
                            </div>
                            <div class="icon">
                                <i class="fas fa-book-open"></i>
                            </div>
                            <div class="small-box-footer">Cancela todas las factura de credito en el sistema</div>
                        </div>
                    </div>
                    <!-- ./col -->
                </div>
                <br />
                <div class="row">
                    <div class="col d-flex">
                        <button type="button" class="btn btn-primary" onclick="printContent('trabajadoresPendientes');">
                            <i class="fas fa-print"></i>&nbsp Imprimir la Lista Cargada en esta Pagina
                        </button>
                    </div>
                    <div class="col">
                        <asp:LinkButton Text="text" class="btn btn-success" runat="server"  ID="btn_reportePorRango" OnClick="btn_reportePorRango_Click" CausesValidation="false">
                            <i class="fas fa-print"></i>&nbsp Reporte de las Facturas segun el Rango Seleccionado
                        </asp:LinkButton>
                    </div>
                    <div class="col">
                        <asp:LinkButton Text="text" class="btn btn-secondary" runat="server" ID="btn_reporteTodas" OnClick="btn_reporteTodas_Click" CausesValidation="false">
                            <i class="fas fa-print"></i>&nbsp Reporte de Todas las Facturas Pendientes en el sistema
                        </asp:LinkButton>
                    </div>
                </div>
                <hr />
                <!-- Main content -->
                <div class="invoice p-3 mb-3" id="trabajadoresPendientes">
                    <!-- title row -->
                    <div class="row">
                        <div class="col-12">
                            <h4>
                                <i class="fas fa-book"></i>&nbsp Facturas Pendientes
                   
                                <small class="float-right">Fecha Reporte:
                                    <asp:Label Text="" ID="lbl_fechaReporte" runat="server" /></small>
                            </h4>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- info row -->
                    <div class="row invoice-info">
                        <div class="col-sm-4 invoice-col float-right">
                            <asp:Image ImageUrl="~/imagenes/logo.jpg" Width="50%" runat="server" />
                        </div>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">
                            <h2>POULTRY SYSTEM S.A</h2>
                            <address>
                                <strong>POULTRY COMEDOR</strong><br>
                                Km 36 1/2 Carretera Masaya, Granada<br>
                                Tel.:(505) 2523-1459<br>
                                Cel.:(505) 8590-3192<br>
                                Email: administracion@ayemadeoro.com.ni
                            </address>
                        </div>                     
                    </div>
                    <!-- /.row -->

                    <!-- Table row -->
                    <div class="row">
                        <div class="col-12 table-responsive p-0">
                            <asp:GridView ID="gv_facturas_por_trabajadador" DataKeyNames="ID_Trabajador" CssClass="table table-sm  table-light table-condensed  table-bordered table-striped" runat="server" OnRowCommand="gv_facturas_por_trabajadador_RowCommand" EnableViewState="false" AutoGenerateColumns="false" OnRowDataBound="gv_facturas_por_trabajadador_RowDataBound" EmptyDataText="NO SE ENCONTRO FACTURAS EN EL RANGO SOLICITADO O BIEN TODAS LAS FACTURAS ESTAN CANCELADAS">
                                <Columns>
                                    <asp:TemplateField HeaderText="N#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Trabajador" HeaderText="Trabajador">
                                        <ItemStyle HorizontalAlign="Left" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Facturas Pendientes">
                                        <ItemTemplate>
                                            <div id="div<%# Eval("ID_Trabajador") %>">
                                                <asp:GridView ID="gv_Facturas" runat="server" AutoGenerateColumns="false" DataKeyNames="ID_Factura"
                                                    CssClass="table table-sm table-primary table-condensed  table-bordered table-striped" OnPreRender="gv_Facturas_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="N#">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Fecha_Factura_Hora" HeaderText="Fecha">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FacturadoP" HeaderText="Facturado x">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MontoTotalFactura" HeaderText="Monto Total" DataFormatString="{0:n}">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NumerodeProductos" HeaderText="Total Productos" DataFormatString="{0:n}">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_Seleccionar" data-toggle="tooltip" title="Seleccione este Proceso para agregar Remisiones" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Seleccionar_Procesos" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-check-circle"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->

                    <div class="row">
                        <!-- /.col -->
                        <div class="col-6">
                            <p class="lead">TOTALES DE RANGO DE FECHA</p>
                            <div class="table-responsive table-bordered table-dark">
                                <table class="table">
                                    <tr>
                                        <th>Total Clientes</th>
                                        <td>
                                            <h3>
                                                <asp:Label Text="" ID="lbl_totalClientes" runat="server" /></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="width: 50%">Total a cobrar:</th>
                                        <td>
                                            <h3>
                                                <asp:Label Text="" ID="lbl_totalCobrar" runat="server" /></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Total Productos</th>
                                        <td class="align-content-lg-end">
                                            <h3>
                                                <asp:Label Text="" ID="lbl_cantidadArticulosTotal" runat="server" /></h3>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->

                </div>
                <!-- /.invoice -->

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

                                            <%--Elimina el fondo negro que queda al cerrar el control--%>
                                            <%--data-dismiss="modal" data-backdrop="false"--%>

                                            <asp:Button ID="btn_Continuar" Visible="false" runat="server" Text="Si!, Continuar" CssClass="btn btn-warning form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />
                                            <asp:Button ID="btn_ContinuarTodo" Visible="false" runat="server" Text="Si!, Continuar" CssClass="btn btn-danger form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_ContinuarTodo_Click" UseSubmitBehavior="false" />
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="../js/bootstrap4-toggle.min.js"></script>

    <!-- date-range-picker -->
    <script src="../AdminTemplate/plugins/daterangepicker/daterangepicker.js"></script>

    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>


    <%--PERMITE IMPRIMIR UNA PORCION DE LA PAGINA--%>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_facturas_por_trabajadador.ClientID%>").footable();
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
                    $("#<%=gv_facturas_por_trabajadador.ClientID%>").footable();
                    //------------------------------------------                                          
                }
            });
        };
    </script>
    <script>
        function printContent(el) {
            var restorepage = $('body').html();
            var printcontent = $('#' + el).clone();
            $('body').empty().html(printcontent);
            window.print();
            $('body').html(restorepage);
        }
    </script>


    <script>
        //Date range picker
        $('#<%=txt_rangofecha.ClientID%>').daterangepicker({
            language: 'es-NI'
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
                    $('#<%=txt_rangofecha.ClientID%>').daterangepicker({
                        language: 'es-NI'
                    });
                    //------------------------------------------                                          
                }
            });
        };
        function ShowtoastMessage() {
            $('.toast').toast('show');
        }
    </script>

    <script>
        //ALERTA DE EXITO
        function success_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
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
                    position: 'top-end',
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
                    position: 'top-end',
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
                    position: 'top-end',
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
                    position: 'top-end',
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
