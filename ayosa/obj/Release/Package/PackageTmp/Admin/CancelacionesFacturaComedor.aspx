<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterSedeCentral.Master" AutoEventWireup="true" CodeBehind="CancelacionesFacturaComedor.aspx.cs" Inherits="POULTRY.Admin.CancelacionesFacturaComedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Fechas de Cancelacion de Facturas Comedor</title>
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
                    <h2>Fechas de Cancelaciones de Facturas</h2>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Comedor POULTRY</a></li>
                        <li class="breadcrumb-item active">Fecha de Cancelacion de Facturas</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="callout callout-info" runat="server" id="div_seleccionP">
                        <h5><i class="fas fa-calendar"></i>&nbsp;&nbsp;Fechas de Cancelacion de Facturas</h5>
                        <div class="form-group row">
                            <label class="col">Incluir Cancelación por Devoluciones</label>
                        </div>
                        <div class="form-group row">
                            <div class="col">
                                <asp:CheckBox name="chk_devoluciones" Text="&nbsp;&nbsp;&nbsp;&nbsp;CANCELACION DE FACTURAS" ID="chk_devoluciones_0" AutoPostBack="true" class="custom-control" aria-describedby="chk_devolucionesHelpBlock" runat="server" />                                
                                <span id="chk_devolucionesHelpBlock" class="form-text text-muted">Agrega todas las facturas de créditos que fueron canceladas por una devolución del producto</span>
                            </div>
                        </div>
                        <!-- Table row -->
                        <div class="row">
                            <div class="col-12">
                                <asp:Repeater ID="rp_FechasCancelacion" runat="server" OnItemCommand="rp_FechasCancelacion_ItemCommand">
                                    <HeaderTemplate>
                                        <table id="tbl_FechasCancelacion" class="table table-bordered table-hover table-responsive-sm bg-white" data-show-toggle="false" data-expand-first="true" style="border-collapse: collapse">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>FECHA CANCELACION</th>
                                                    <th>CANCELADO POR</th>
                                                    <th>MONTO CANCELADO</th>
                                                    <th>CONCEPTO</th>
                                                    <th>FACTURAS</th>
                                                    <th>IMPRIMIR FACT-CANC</th>
                                                    <th>IMPRIMIR CONSOLIDADO-TOT</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width: 1px;">
                                                <h5><%#Container.ItemIndex + 1%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><%#Eval("FECHA")%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><%#Eval("CANCELADOPOR")%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><b><%#Eval("TOTAL","{0:C}")%> </b></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><%#Eval("ConceptoCancelacion")%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <asp:LinkButton ID="btn_Facturas" CommandName="cmd_Facturas" CommandArgument='<%#Eval("FECHA")%>' CssClass="btn btn-primary form-control" runat="server"><i class="fa fa-solid fa-bank"></i></asp:LinkButton>
                                            </td>
                                            <td style="width: 1px;">
                                                <asp:LinkButton ID="btn_ImprimirFacturas" CommandName="cmd_ImprimirFacturas" CommandArgument='<%#Eval("FECHA")%>' CssClass="btn btn-secondary form-control" runat="server"><i class="fa fa-solid fa-print"></i></asp:LinkButton>
                                            </td>
                                            <td style="width: 1px;">
                                                <asp:LinkButton ID="btn_ImprimirConsolidado" CommandName="cmd_ImprimirConsolidado" CommandArgument='<%#Eval("FECHA")%>' CssClass="btn btn-default form-control" runat="server"><i class="fa fa-solid fa-print"></i></asp:LinkButton>
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
                </div>
            </div>
        </div>
    </section>
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
</asp:Content>
