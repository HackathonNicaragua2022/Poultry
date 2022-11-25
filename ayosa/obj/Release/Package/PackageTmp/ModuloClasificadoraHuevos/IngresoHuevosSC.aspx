<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterControlHuevos.Master" AutoEventWireup="true" CodeBehind="~/ModuloClasificadoraHuevos/IngresoHuevosSC.aspx.cs" Inherits="POULTRY.ModuloClasificadoraHuevos.IngresoHuevosSC" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Control de ingreso a Clasificadora de Huevo</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <!-- daterange picker -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/daterangepicker/daterangepicker.css" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <style>
        .img-responsive {
            display: block;
            margin: auto;
        }
    </style>
    <style>
        .table-bordered th, .table-bordered td {
            border: 1px solid #ddd !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h2>Ingresos a Clasificadora</h2>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homebodegahuevo.aspx">Clasificadora de Huevo</a></li>
                        <li class="breadcrumb-item active">Inreso a Clasificadora</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up_principal" runat="server">
        <ProgressTemplate>
            <div class="d-flex align-items-center">
                <strong>Preparando Informe.....</strong>
                <p>Espere porfavor</p>
                <div class="spinner-border ml-auto" role="status" aria-hidden="true"></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up_principal" runat="server">
        <ContentTemplate>
            <section class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">
                            <div class="callout callout-info" runat="server" id="div_seleccionP">
                                <h5><i class="fas fa-calendar"></i>&nbsp;&nbsp;Seleccione una fecha</h5>
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-sm-12 invoice-col m-0">
                                        <div class="form-group">
                                            <label for="text">Seleccione el Periodo de ingresos</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox runat="server" aria-describedby="textHelpBlock" type="text" class="form-control float-right" ID="txt_rangofecha" />
                                            </div>
                                            <span id="textHelpBlock" class="form-text text-muted">Rango de fechas para obtener todos los ingresos</span>
                                            <!-- /.input group -->
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-sm-12 invoice-col m-0">
                                        <label for="btn_CargarIngresos">Seleccione el Periodo de ingresos</label>
                                        <div class="form-group">
                                            <asp:Button Text="Cargar Ingresos" ID="btn_CargarIngresos" CssClass="btn btn-primary" UseSubmitBehavior="false" CausesValidation="false" OnClick="btn_CargarIngresos_Click" runat="server" aria-describedby="btn_CargarIngresosHelpBlock" />
                                        </div>
                                        <span id="btn_CargarIngresosHelpBlock" class="form-text text-muted">Rango de fechas para obtener todos los ingresos</span>
                                        <!-- /.input group -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Table row -->
                    <div class="row">
                        <div class="col-12">
                            <asp:Repeater ID="rp_IngresosPorFecha" runat="server" OnItemCommand="rp_IngresosPorFecha_ItemCommand" OnItemDataBound="rp_IngresosPorFecha_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="tbl_Ingresos" class="table table-bordered table-hover table-responsive-sm bg-white" data-show-toggle="false" data-expand-first="true" style="border-collapse: collapse">
                                        <thead>
                                            <tr>
                                                <th>DETALLES</th>
                                                <th>ORDEN</th>
                                                <th>TOTAL</th>
                                                <th>REGISTRO</th>
                                                <th data-breakpoints="all">ANULADO</th>
                                                <th data-breakpoints="all">FECHA ANULADO</th>
                                                <th data-breakpoints="all">ANULAR</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="Center" style="width: 1px; white-space: nowrap;">
                                            <%--<asp:LinkButton ID="link_Detalles" runat="server"><i class="fa fa-plus-square fa-2x" aria-hidden="true"></i></asp:LinkButton>--%>
                                            <asp:Button ID="btn_Detalle" runat="server" Text="Detalle" CausesValidation="false" UseSubmitBehavior="false" CssClass="btn btn-success form-control" CommandName="cmd_Detalle" CommandArgument='<%#Eval("ID_IngresoInventario")%>' />
                                        </td>
                                        <td style="width: 1px;">
                                            <h3><%#Eval("NumeroOrden")%></h3>
                                        </td>
                                        <td>
                                            <h3><b><%#Eval("TOTALCAJILLAS","{0:n}")%> </b></h3>
                                        </td>
                                        <td>
                                            <%#Eval("FechaIngresoSistema", "{0:D}")%>
                                        </td>
                                        <td style="width: 1px;">
                                            <div class="custom-control custom-checkbox">
                                                <input class="custom-control-input" type="checkbox" id="customCheckbox2" checked>
                                                <%#Eval("Anulado")%>
                                            </div>
                                        </td>
                                        <td style="width: 1px;">
                                            <%#Eval("fechaAnulado","{0:D}")%>
                                        </td>
                                        <td style="width: 1px;">
                                            <asp:LinkButton ID="btn_Anular" CommandName="cmd_Anular" CommandArgument='<%#Eval("ID_IngresoInventario")%>' CssClass="btn btn-danger form-control" runat="server">
                                                                        <i class="fa fa-solid fa-remove"></i>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr style='<%#Eval("MostrarDetalle").ToString().Equals("False")?"display: none;": "display:normal;"%>'>
                                        <td colspan="7">
                                            <asp:Repeater ID="rp_IngresosPorFecha" runat="server">
                                                <HeaderTemplate>
                                                    <table class="table table-hover table-responsive-sm table-success table-bordered table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>Remitido de</th>
                                                                <th>Tipo de Huevo</th>
                                                                <th>Fecha Produccion</th>
                                                                <th>Cantidad Cajillas</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 1px;">
                                                            <%#Eval("NombreJaula")%>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <b><%#Eval("TipoHuevo")%> </b>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <%#Eval("FechaProduccion", "{0:D}")%>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <%#Eval("CANTIDADCAJILLA")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                                                    </table>   
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <asp:HiddenField ID="id_Ingreso_HF" Value='<%#Eval("ID_IngresoInventario")%>' runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                    </table>   
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../js/bootstrap4-toggle.min.js"></script>
    <!-- date-range-picker -->
    <script src="../AdminTemplate/plugins/daterangepicker/daterangepicker.js"></script>

    <script>
        //Date range picker
        $('#<%=txt_rangofecha.ClientID%>').daterangepicker({
            "locale": {
                "separator": " - ",
                "applyLabel": "Aceptar",
                "cancelLabel": "Cancelar",
                "fromLabel": "De",
                "toLabel": "Hasta",
                "customRangeLabel": "Personalizado",
                "daysOfWeek": [
                    "Dom",
                    "Lun",
                    "Mar",
                    "Mie",
                    "Jue",
                    "Vie",
                    "Sab"
                ],
                "monthNames": [
                    "Enero",
                    "Febrero",
                    "Marzo",
                    "Abril",
                    "Mayo",
                    "Junio",
                    "Julio",
                    "Agosto",
                    "Septiembre",
                    "Octubre",
                    "Noviembre",
                    "Diciembre"
                ],
                "firstDay": 1
            }
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
                        //"format": "DD/MM/YYYY",
                        "locale": {
                            "separator": " - ",
                            "applyLabel": "Aceptar",
                            "cancelLabel": "Cancelar",
                            "fromLabel": "De",
                            "toLabel": "Hasta",
                            "customRangeLabel": "Personalizado",
                            "daysOfWeek": [
                                "Dom",
                                "Lun",
                                "Mar",
                                "Mie",
                                "Jue",
                                "Vie",
                                "Sab"
                            ],
                            "monthNames": [
                                "Enero",
                                "Febrero",
                                "Marzo",
                                "Abril",
                                "Mayo",
                                "Junio",
                                "Julio",
                                "Agosto",
                                "Septiembre",
                                "Octubre",
                                "Noviembre",
                                "Diciembre"
                            ],
                            "firstDay": 1
                        }
                    });
                    //------------------------------------------                                          
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
