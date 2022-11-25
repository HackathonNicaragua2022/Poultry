<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterControlHuevos.Master" AutoEventWireup="true" CodeBehind="clasificarhuevos.aspx.cs" Inherits="POULTRY.ModuloClasificadoraHuevos.clasificarhuevos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Clasificacion de Huevo por Categoria</title>
    <!-- daterange picker -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/daterangepicker/daterangepicker.css" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <style>
        .img-responsive {
            display: block;
            margin: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Huevos a Clasificar</h5>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homebodegahuevo.aspx">Menu</a></li>
                        <li class="breadcrumb-item active">Clasificar Huevos</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <div class="row align-items-center justify-content-center">
        <div class="col-sm-12 lign-self-center">
            <!-- Profile Image -->
            <div class="card card-danger card-outline m-1">
                <div class="card-body box-profile">
                    <div class="text-center">
                        <img class="profile-user-img img-fluid img-circle"
                            src="../imagenes/logop-w.png"
                            alt="POULTRY" />
                    </div>

                    <h3 class="profile-username text-center">Clasificar Huevos</h3>

                    <p class="text-muted text-center">Clasificacion de las cajillas de huevos</p>
                    <hr />
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="up_principal" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card card-danger">
                                        <div class="card-header">
                                            <h3 class="card-title">Datos para Clasificacion</h3>

                                            <div class="card-tools">
                                                <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                                    <i class="fas fa-minus"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="form-group">
                                                <label for="txt_Observaciones">Observaciones Generales</label>
                                                <asp:TextBox runat="server" CssClass="form-control" MaxLength="250" ID="txt_Observaciones" />
                                            </div>
                                            <br />
                                            <hr />
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6 col-lg-4 col-sm-12">
                                                    <div class="form-group">
                                                        <label for="dr_TipoHuevoInventarioSC">Tipo de Huevo</label>
                                                        <asp:DropDownList ID="dr_TipoHuevoInventarioSC" CssClass="form-control custom-select" runat="server" aria-describedby="text1HelpBlock" OnSelectedIndexChanged="dr_TipoHuevoInventarioSC_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <span id="text1HelpBlock" class="form-text text-muted">Se muestran tipos de Huevos con existencia en inventario por clasificar</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:Repeater ID="rp_DetallesIHSC" runat="server" OnItemCommand="rp_DetallesIHSC_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table id="tablaDetalles" class="table table-striped table-bordered table-responsive-sm table-hover" data-show-toggle="false" data-expand-first="true">
                                                                <tr>
                                                                    <td><b>Tipo Huevo</b></td>
                                                                    <td><b>Total Entradas</b></td>
                                                                    <td><b>Total Salidas</b></td>
                                                                    <td><b>Total Saldo</b></td>
                                                                    <td><b>Fecha de Produccion</b></td>
                                                                    <td data-breakpoints="all"><b>Dias desde Produccion</b></td>
                                                                    <td data-breakpoints="xs"><b>Seleccionar</b></td>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <tr  class='<%#(bool)Eval("Seleccionado")==true?"bg-gradient-green":""%>'>
                                                                <td>
                                                                    <b><%#Eval("TipoHuevo")%> </b>
                                                                </td>
                                                                <td>
                                                                    <b><%#Eval("TOTALE")%> </b>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("TS")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("SALDO")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("FechaProduccion", "{0:D}")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("PROMEDIO_DIAS")%>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="btn_Seleccionar" CommandName="cmd_Seleccionar" CommandArgument='<%#Eval("ID_HuevoSinClasificar")%>' CssClass='<%#(bool)Eval("Seleccionado")==true?"btn btn-primary":"btn btn-success"%>' runat="server">
                                                                        <i class="fa fa-solid fa-check-square"></i>
                                                                        '<%#(bool)Eval("Seleccionado")==true?"En Uso":"Seleccionar"%>'</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>   
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>

                                            <hr />
                                            <div class="row">
                                                <!-- /.col -->
                                                <div class="col-md-6 col-lg-4 col-sm-12">
                                                    <p class="lead">Produccion seleccionada</p>

                                                    <div class="table-responsive">
                                                        <table class="table">
                                                            <tr>
                                                                <th style="width: 50%">Fecha Produccion:</th>
                                                                <td>
                                                                    <asp:Label Text="0" ID="lbl_FechaProduccion" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <th>Edad Huevo</th>
                                                                <td>
                                                                    <asp:Label Text="0" ID="lbl_EdadDias" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <th>Total Entradas</th>
                                                                <td>
                                                                    <asp:Label Text="0" ID="lbl_TotalEntradas" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <th>Total Salidas</th>
                                                                <td>
                                                                    <asp:Label Text="0" ID="lbl_TotalSalidas" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <th>Saldo disponible para clasificar</th>
                                                                <td>
                                                                    <asp:Label Text="0" ID="lbl_SaltoTotal" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <!-- /.col -->
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-6 col-lg-4 col-sm-12">
                                                    <div class="form-group">
                                                        <label for="dr_Clasificacion">Clasificacion</label>
                                                        <asp:DropDownList ID="dr_Clasificacion" CssClass="form-control custom-select" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_CantidadCajillasRecibidas">Cantidad Cajillas Clasificadas</label>
                                                <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" TextMode="Number" ID="txt_CantidadCajillasRecibidas" />
                                            </div>
                                            <div class="alert alert-" role="alert" runat="server" id="alerta_nuevoVehiculo" visible="false">
                                                <asp:Label Text="Verifique los campos" ID="lbl_NuevoMensaje" runat="server" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button Text="Agregar Salida" CssClass="btn btn-primary" UseSubmitBehavior="false" CausesValidation="false" runat="server" ID="btn_IngresarTotalCajillas" OnClick="btn_IngresarTotalCajillas_Click" />
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12">
                                                    <asp:Repeater ID="rp_detalles" runat="server" OnItemCommand="rp_detalles_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table class="table table-striped table-bordered table-responsive-sm">
                                                                <tr>
                                                                    <td><b>Fecha Produccion</b></td>
                                                                    <td><b>Huevo</b></td>
                                                                    <td><b>Clasificacion</b></td>
                                                                    <td><b>Cantidad Cajillas</b></td>
                                                                    <td><b>Remover</b></td>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                 <td>
                                                                    <%#Eval("FechaProduccion")%>   
                                                                </td>
                                                                <td>
                                                                    <%#Eval("TipoHuevo")%>   
                                                                </td>
                                                                <td>
                                                                    <%#Eval("NombreClasificacion")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("CANTIDADCAJILLAS")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Button Text="Remover" runat="server" CssClass="btn btn-primary form-control" ID="btn_Seleccionar" CommandArgument='<%#Eval("ID_DetalleSalidaHuevoClasificado")%>' CommandName="cmd_remover" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>   
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.card-body -->
                                    </div>
                                    <!-- /.card -->
                                    <br />
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up_principal" runat="server">
                                        <ProgressTemplate>
                                            <div class="d-flex align-items-center">
                                                <strong>Guardando.....</strong>
                                                <p>Espere porfavor</p>
                                                <div class="spinner-border ml-auto" role="status" aria-hidden="true"></div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <br />
                                    <div class="row">
                                        <div class="col-12">
                                            <asp:LinkButton ID="btn_guardarNuevoIngreso" runat="server" class="btn btn-lg btn-success" OnClick="btn_guardarNuevoIngreso_Click">
                                                    <i class="fas fa-save"></i>&nbsp GUARDAR SALIDA HUEVO CLASIFICADO
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- /.card -->
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(function ($) {
            $('.tablaDetalles').footable();
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    jQuery(function ($) {
                        $('.tablaDetalles').footable();
                    });
                }
            });
        };
    </script>
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
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
    </script>
</asp:Content>
