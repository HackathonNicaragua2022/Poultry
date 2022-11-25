<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="AvesEnvidasMatadero.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.AvesEnvidasMatadero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Remisiones a  Matadero</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link href="../AdminTemplate/open-iconic-master/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <style>
        .table-bordered th, .table-bordered td {
            border: 1px solid #ddd !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row mb-2">
            <div class="col-sm-6">
                <h1 class="m-1">Remisiones enviadas a Matadero</h1>
            </div>
            <!-- /.col -->
            <div class="col-sm-6 m-0">
                <ol class="breadcrumb float-sm-right">
                    <li class="breadcrumb-item"><a href="home-admin.aspx">Inicio</a></li>
                    <li class="breadcrumb-item"><a href="homeGranja.aspx">Granja Produccion</a></li>
                    <li class="breadcrumb-item active">Remisiones Matadero</li>
                </ol>
            </div>
            <!-- /.col -->
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up_principal">
            <ProgressTemplate>
                <div class="progress">
                    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 100%">Cargando por favor espere</div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="up_principal" runat="server">
            <ContentTemplate>
                <!-- /.row -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card card-primary card-outline">
                            <div class="card-header">
                                <h5 class="card-title m-0">Procesos de compra activos</h5>
                            </div>
                            <div class="card-body">
                                <%--CssClass="table table-sm  table-light table-condensed table-bordered table-striped"--%>
                                <asp:GridView ID="gv_adquisicion" DataKeyNames="ID_CompraBroilers" runat="server" OnRowCommand="gv_adquisicion_RowCommand" EnableViewState="false" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="ID_CompraBroilers" HeaderText="ID"></asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Granja"></asp:BoundField>
                                        <asp:BoundField DataField="ReferenciaComentario" HeaderText="Comentario"></asp:BoundField>
                                        <asp:BoundField DataField="CodLote" HeaderText="Lote"></asp:BoundField>
                                        <asp:BoundField DataField="FechaMatanza" HeaderText="Fecha Matanza"></asp:BoundField>
                                        <%--<asp:BoundField DataField="PrecioCompraxLibra" DataFormatString="{0:C2}" HeaderText="Precio Compra x Libra"></asp:BoundField>--%>
                                        <asp:BoundField DataField="TotalLibrasCompradasCalculoBascula" DataFormatString="{0:n}" HeaderText="Total Lbs Bascula"></asp:BoundField>
                                        <asp:BoundField DataField="TotalAvesRemisionCompradas" HeaderText="TotalAves x Remision"></asp:BoundField>
                                        <asp:BoundField DataField="TotalAvesConteoAutomatico" HeaderText="ContadorAutomatico(Aves)"></asp:BoundField>
                                        <asp:BoundField DataField="NombreRaza" HeaderText="Raza"></asp:BoundField>
                                        <asp:BoundField DataField="NombreMatadero" HeaderText="Matadero"></asp:BoundField>
                                        <%--<asp:BoundField DataField="CostoTotalxLibra" DataFormatString="{0:C2}" HeaderText="CostoTotal x Libra"></asp:BoundField>--%>
                                        <asp:BoundField DataField="CreadoPor" HeaderText="CreadoPor"></asp:BoundField>
                                        <asp:BoundField DataField="ACargoPEsoVivo" HeaderText="A Cargo de Peso Vivo"></asp:BoundField>
                                        <asp:BoundField DataField="FechaCreado" HeaderText="FechaCreado"></asp:BoundField>
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
                        </div>
                    </div>
                    <!-- /.col-md-6 -->
                </div>

                <!-- Main content -->
                <div class="invoice p-3 mb-3">
                    <!-- title row -->
                    <div class="row">
                        <div class="col-12">
                            <h4>
                                <i class="fas fa-car-side"></i>&nbsp; Remisiones de aves enviadas a Matadero
                   
                                <small class="float-right">Total de Remisiones: <b>
                                    <asp:Label Text="" ID="lbl_TotalRemisiones" runat="server" /></b></small>
                            </h4>
                        </div>
                        <!-- /.col -->
                    </div>

                    <!-- Table row -->
                    <div class="row">
                        <div class="col-12">
                            <asp:GridView ID="gv_Remisiones" DataKeyNames="ID_ViajesRemision" OnRowDataBound="gv_Remisiones_RowDataBound" runat="server" OnRowCommand="gv_Remisiones_RowCommand" EnableViewState="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="NumeroViaje" HeaderText="Numero Viaje"></asp:BoundField>
                                    <asp:BoundField DataField="PesoPromedio" HeaderText="Peso Promedio" DataFormatString="{0:n} lbs"></asp:BoundField>
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha"></asp:BoundField>
                                    <asp:BoundField DataField="NombreConductor" HeaderText="Nombre Conductor"></asp:BoundField>
                                    <asp:BoundField DataField="PlacaCamion" HeaderText="Placa Camion"></asp:BoundField>
                                    <asp:BoundField DataField="Total_javas" HeaderText="Total javas"></asp:BoundField>
                                    <asp:BoundField DataField="HoraAyuno" HeaderText="Hora Ayuno"></asp:BoundField>
                                    <asp:BoundField DataField="Edad" HeaderText="Edad" DataFormatString="{0:n} Dias"></asp:BoundField>
                                    <asp:BoundField DataField="NumeroGalera" HeaderText="Numero Galera"></asp:BoundField>
                                    <asp:BoundField DataField="Destino" HeaderText="Destino"></asp:BoundField>
                                    <asp:BoundField DataField="TotalAvesEnviadas" HeaderText="Total Aves Enviadas" DataFormatString="{0:n} Aves"></asp:BoundField>
                                    <asp:BoundField DataField="HoraSalidaGranja" HeaderText="Hora Salida Granja"></asp:BoundField>
                                    <asp:BoundField DataField="HoraLlegadaPlanta" HeaderText="Hora Llegada Planta"></asp:BoundField>
                                    <asp:BoundField DataField="EntregaConforme" HeaderText="Entrega Conforme"></asp:BoundField>
                                    <asp:BoundField DataField="CreadoPor" HeaderText="Creado Por"></asp:BoundField>
                                    <asp:BoundField DataField="EstadoSaludAve" HeaderText="Estado Salud Ave"></asp:BoundField>
                                    <asp:BoundField DataField="NUFDMADEAS" HeaderText="Medicamentos"></asp:BoundField>
                                    <asp:BoundField DataField="Finalizada" HeaderText="Finalizada"></asp:BoundField>
                                    <asp:BoundField DataField="JavasPendientesxPesar" HeaderText="Javas Pendientes x Pesar"></asp:BoundField>
                                    <asp:BoundField DataField="TotalLibrasxRemision" HeaderText="Total Libras x Remision"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Eliminar Envio">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Elimina el envio, devolviendo todas las aves al inventario" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_EliminarEnvio" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-check-circle"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JAVAS ENVIADAS">
                                        <ItemTemplate>
                                            <div id="gvjavasEnviadas<%# Eval("ID_ViajesRemision") %>">
                                                <asp:GridView
                                                    ID="gv_javasEnviadas"
                                                    runat="server"
                                                    AutoGenerateColumns="false"
                                                    EmptyDataText="Ocurrio un error, no se puede mostrar las javas enviadas"
                                                    DataKeyNames="ID_JavasEnviadas"
                                                    CssClass="table table-sm table-success table-condensed  table-bordered table-striped">
                                                    <Columns>
                                                        <asp:BoundField DataField="Cantidad_Javas" HeaderText="Cantidad de Javas">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PollosPorJavas" HeaderText="Pollos Por Java">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SubTotalAves" HeaderText="Subtotal Aves">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PESAJES DE REMISION">
                                        <ItemTemplate>
                                            <%-- <a href="JavaScript:divexpandcollapse('div<%# Eval("ID_ViajesRemision") %>');">
                                                            <img alt="Details" id="imgdiv<%# Eval("ID_ViajesRemision") %>" src="../imagenes/plus.png" />
                                                        </a>--%>
                                            <!-- Adding the Div container -->
                                            <div id="div<%# Eval("ID_ViajesRemision") %>">
                                                <%--style="display: none;"--%>
                                                <!-- Adding Child GridView -->
                                                <asp:GridView
                                                    ID="gv_Pesajes"
                                                    runat="server"
                                                    AutoGenerateColumns="false"
                                                    DataKeyNames="ID_IngresoPesoVivo"
                                                    EmptyDataText="Aun no se registran pesos"
                                                    CssClass="table table-sm table-info table-condensed  table-bordered table-striped">
                                                    <Columns>
                                                        <asp:BoundField DataField="NumPesaje" HeaderText="Numero Pesaje">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CantidadJavasPesadas" HeaderText="Cantidad Javas">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PollosxJava" HeaderText="Pollos x Java">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TotalPollos" HeaderText="Total Pollos">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PesoJavaConPollo_Libras" HeaderText="PesoJavaConPollo_Libras" DataFormatString="{0:n}">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PesoxJavaVacia_libDefault" HeaderText="PesoxJava x Vacia" DataFormatString="{0:n}">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PesoNetoPollosLibra" HeaderText="Peso Neto Pollos Libra" DataFormatString="{0:n}">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PesoPromedioxPollo_lib" HeaderText="Peso Promedio" DataFormatString="{0:n}">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
                                    <h2 class="text-primary">¿Esta Completamente seguro que deseha eliminar el Registro de Remision?, Todos los datos relacionados como, registro de pesos, javas enviadas entre otros seran eliminados tambien</h2>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12">
                                            <label for="btn_Continuar"></label>

                                            <%--Elimina el fondo negro que queda al cerrar el control--%>
                                            <%--data-dismiss="modal" data-backdrop="false"--%>

                                            <asp:Button ID="btn_Continuar" runat="server" Text="Si!, Continuar" CssClass="btn btn-danger form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />

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
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_adquisicion.ClientID%>").footable();
                $("#<%=gv_Remisiones.ClientID%>").footable();
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
                    $("#<%=gv_Remisiones.ClientID%>").footable();
                    //------------------------------------------    
                }
            });
        };

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
