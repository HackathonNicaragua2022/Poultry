<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterControlHuevos.Master" AutoEventWireup="true" CodeBehind="IngresoInventarioHSC.aspx.cs" Inherits="POULTRY.ModuloClasificadoraHuevos.IngresoInventarioHSC" EnableEventValidation="false" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ingreso Huevos sin clasificar</title>
    <!-- daterange picker -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/daterangepicker/daterangepicker.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
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
                    <h5>Ingreso de Huevos a Clasificar</h5>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homebodegahuevo.aspx">Menu</a></li>
                        <li class="breadcrumb-item active">Huevos a Clasificar</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <div class="row align-items-center justify-content-center">
        <div class="col-sm-12 lign-self-center">
            <!-- Profile Image -->
            <div class="card card-success card-outline m-1">
                <div class="card-body box-profile">
                    <div class="text-center">
                        <img class="profile-user-img img-fluid img-circle"
                            src="../imagenes/logop-w.png"
                            alt="User profile picture" />
                    </div>
                    <h3 class="profile-username text-center">POULTRY SYSTEM S.A</h3>

                    <p class="text-muted text-center">Ingreso Huevos a Clasificar</p>
                    <hr />
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="up_principal" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card card-primary">
                                        <div class="card-header">
                                            <h3 class="card-title">Detalle de Ingreso</h3>

                                            <div class="card-tools">
                                                <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                                    <i class="fas fa-minus"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="form-group">
                                                <label for="txt_NumeroOrden">Número Orden</label>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <i class="fa fa-book"></i>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txt_NumeroOrden" name="txt_NumeroOrden" placeholder="123" MaxLength="10" type="text" aria-describedby="txt_NumeroOrdenHelpBlock" class="form-control" />
                                                </div>
                                                <span id="txt_NumeroOrdenHelpBlock" class="form-text text-muted">Numero Orden de la hoja de remisión de la ubicación a la clasificadora</span>
                                            </div>
                                            <div class="form-group">
                                                <label for="dr_TipoHuevoInventarioSC">Tipo de Huevo</label>
                                                <asp:DropDownList ID="dr_TipoHuevo" CssClass="form-control custom-select" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="dr_Ubicacion">Hubicacion Remision del Huevo</label>
                                                <asp:DropDownList ID="dr_Ubicacion" CssClass="form-control custom-select" runat="server"></asp:DropDownList>
                                            </div>
                                            <%--  <div class="form-group">
                                                <label for="dr_Clasificacion">Clasificacion del Huevo</label>
                                                <asp:DropDownList ID="dr_Clasificacion" CssClass="form-control custom-select" runat="server"></asp:DropDownList>                                                
                                            </div>--%>
                                            <div class="form-group">
                                                <label for="txt_CantidadCajillasRecibidas">Cantidad Cajillas Total Recibidas</label>
                                                <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" TextMode="Number" ID="txt_CantidadCajillasRecibidas" />
                                            </div>
                                            <div class="form-group">
                                                <label for="date">Fecha Produccion</label>
                                                <div class="input-group date" data-provide="datepicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="date" name="date" placeholder="" type="text" TextMode="Date" aria-describedby="txt_date_HelpBlock" />
                                                    <div class="input-group-addon">
                                                        <span class="glyphicon glyphicon-th"></span>
                                                    </div>
                                                </div>
                                                <span id="txt_date_HelpBlock" class="form-text text-muted">Fecha de produccion de cada tipo de huevo a ingresar</span>
                                            </div>
                                            <div class="alert alert-" role="alert" runat="server" id="alerta_nuevoVehiculo" visible="false">
                                                <asp:Label Text="Verifique los campos" ID="lbl_NuevoMensaje" runat="server" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button Text="Agregar Cajillas" CssClass="btn btn-primary" UseSubmitBehavior="false" CausesValidation="false" runat="server" ID="btn_IngresarTotalCajillas" OnClick="btn_IngresarTotalCajillas_Click" />
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12">
                                                    <asp:Repeater ID="rp_detalles" runat="server" OnItemCommand="rp_detalles_ItemCommand1">
                                                        <HeaderTemplate>
                                                            <table class="table table-striped table-bordered table-responsive-sm">
                                                                <tr>
                                                                    <td><b>Jaula</b></td>
                                                                    <td><b>Tipo Huevo</b></td>
                                                                    <td><b>Cantidad Cajillas</b></td>
                                                                    <td><b>Fecha Produccion</b></td>
                                                                    <td><b>Remover</b></td>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <tr>
                                                                <td>
                                                                    <%#Eval("NombreJaula")%>   
                                                                </td>
                                                                <td>
                                                                    <%#Eval("TipoHuevo")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("CANTIDADCAJILLA")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("FechaProduccion", "{0:d}") %>
                                                                </td>
                                                                <td>
                                                                    <asp:Button Text="Remover" runat="server" CssClass="btn btn-primary form-control" ID="btn_Seleccionar" CommandArgument='<%#Eval("ID_DetalleIngresoInventarioHSC")%>' CommandName="cmd_remover" />
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
                                            <span>Una vez guardado se sumaran todos los huevos del mismo tipo sin importar la jaula</span>
                                            <br />
                                            <asp:LinkButton ID="btn_guardarNuevoIngreso" runat="server" class="btn btn-lg btn-success" OnClick="btn_guardarNuevoIngreso_Click">
                                                    <i class="fas fa-save"></i>&nbsp GUARDAR NUEVO INGRESO
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_guardarNuevoIngreso" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- /.card -->
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
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
    <script type="text/javascript">
        //CON ESTE CODIGO SE PUEDE CONTROLAR LA SELECCION DE FECHAS FUTURAS O ANTERIORES, USANDO MIN PARA BLOQUEAR ANTERIORES O MAX PARA FUTURAS A PARTUR DE LA FECHA DE HOY
        // SE PUEDE CAMBIAR TODAY PARA INDICAR APARTIR DE QUE FECHA SE COMENZARA A EVALUAR O BIEN SUMAR O RESTAR DIAS A TODAY PARA CONTROLAR LA FECHA
        // TAG: limitar fecha futuras, bloquear fechas futuras, bloquear fechas, limitar fechas futuras, limitar fechas posteriores, bloquear fechas antiguas, limitar fechas antiguas o anteriores
        // tag para busquedas de codigos futuros
        $(function () {
            var today = new Date();
            var month = ('0' + (today.getMonth() + 1)).slice(-2);
            var day = ('0' + today.getDate()).slice(-2);
            var year = today.getFullYear();
            var date = year + '-' + month + '-' + day;
            $('[id*=date]').attr('max', date);
        });
    </script>
</asp:Content>
