<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="ReporteIngreoLotes.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.ReporteIngreoLotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Lotes Ingresados</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1 class="text-primary">Ingreso de Nuevo Lote</h1>
                            <p>Todos los Lotes ingresados a la Granja</p>
                        </div>
                        <div class="col-sm-6">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="homeGranja.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Reporte de Lotes Ingresados</li>
                            </ol>
                        </div>
                    </div>
                </div>
                <!-- /.container-fluid -->
            </section>
            <div class="callout callout-info">
                <h5>Lotes Ingresados</h5>
                <p>
                    TotaL de Lotes Ingresados en las Galeras
                </p>
                <asp:GridView ID="gv_Lotes" DataKeyNames="ID_Lote" OnDataBound="gv_Lotes_DataBound" CssClass="table table-bordered table-hover table-responsive-sm bg-white" runat="server" EnableViewState="False" AutoGenerateColumns="false" PageSize="50" AllowPaging="True" Style="max-width: 100%" OnRowCommand="gv_Lotes_RowCommand" OnRowDataBound="gv_Lotes_RowDataBound" EmptyDataText="No se encontro Lote en el sistema!">
                    <Columns>
                        <asp:TemplateField HeaderText="N°">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CodLote" HeaderText="Lote">
                            <ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Galera" HeaderText="Galera">
                            <ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NumeroFactura" HeaderText="Factura">
                            <ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PesoPromedioxAveRecibida" HeaderText="Peso Promedio" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CantidadAvesPesadasparaPesoProm" HeaderText="Aves Pesadas" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CantidadComprada" HeaderText="Aves Factura" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Bonificacion" HeaderText="Bonificacion" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NetoFactura" HeaderText="Neto Factura" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MortalidadRecibida" HeaderText="Mortalidad Recibida" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ConteoFisico" HeaderText="Conteo Fisico" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DiferenciaConFactura" HeaderText="Diferencia Factura" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalInicialGalera" HeaderText="Inventario Inicial" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ApruebaConteo" HeaderText="Aprobado Por"></asp:BoundField>
                        <asp:BoundField DataField="CostoxAveNIO" HeaderText="Costo Ave NIO" DataFormatString="{0:C}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CostoxAveUSD" HeaderText="Costo Ave USD" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TazaConvercion" HeaderText="Taza Cambio" DataFormatString="{0:C}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CostoTotal_CompraLoteNIO" HeaderText="Costo Total Lote NIO" DataFormatString="{0:C}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CostoTotal_CompraLoteUSD" HeaderText="Costo Total Lote USD" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NombrePollo" HeaderText="Raza"></asp:BoundField>
                        <asp:BoundField DataField="Proveedor" HeaderText="Proveedor"></asp:BoundField>
                        <asp:BoundField DataField="FechaEntradaGalera" HeaderText="Fecha Entrada Galera" DataFormatString="{0:D}"></asp:BoundField>
                        <asp:BoundField DataField="Ingresado_Por" HeaderText="Ingresado Por"></asp:BoundField>
                        <asp:BoundField DataField="FechaAproximadaparaMatarLote" HeaderText="Fecha Sugeridad para Liquidar" DataFormatString="{0:D}"></asp:BoundField>
                        <asp:BoundField DataField="Temperatura_InicialGalera" HeaderText="Temperatura Galera" DataFormatString="{0:C}">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaEntradaSistema" HeaderText="Fecha Sistema" DataFormatString="{0:D}"></asp:BoundField>
                        <asp:TemplateField HeaderText="Personal en Recibo">
                            <ItemTemplate>
                                <div id="Lotes_<%# Eval("ID_Lote") %>">
                                    <asp:GridView
                                        ID="gv_Personal"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="ID_PersonalEnEntrada"
                                        CssClass="table table-sm table-success table-condensed  table-bordered table-striped">
                                        <Columns>
                                            <asp:BoundField DataField="NombrePersona" HeaderText="Personal">
                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FuncionDuranteRecibo" HeaderText="Funcion Durante Recibo">
                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FuncionDuranteCrecimiento" HeaderText="Funcion Durante Crecimiento">
                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remover">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Elimina el lote del sistema, solo se podra eliminar si no se ha usado" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="eliminar" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <%--MODAL PARA ADEVERTIR DE LA ELIMINACION--%>
            <div class="modal fade" id="advertencia-modal-sm">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Eliminacion de Lote</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <h4 class="card-title">Atencion!!
                            </h4>
                            <ion-icon name="warning-outline" style="color: orange; font-size: 128px; width: 100%; display: flex; align-items: center; justify-content: center;"></ion-icon>
                            <p class="lead">
                                Seguro que deha eliminar el lote ingresado, si hay un proceso de compra de parte de Mataderos, este proceso sera eliminado para proceder con la eliminacion, una vez eliminado no se podra deshacer la eliminacion<br />
                                ¿Continuar?
                            </p>
                            <div class="text-center">
                                <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btn_EliminarLote" OnClick="btn_EliminarLote_Click" runat="server" data-dismiss="modal" data-backdrop="false" UseSubmitBehavior="false" CausesValidation="false" />
                            </div>
                        </div>
                        <div class="modal-footer justify-content-between">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {

            $(document).ready(function () {

                $(<%=gv_Lotes.ClientID%>).footable();
            });

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {

                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(<%=gv_Lotes.ClientID%>).footable();
                }

            });
        };
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

        function ShowModal_Advertencia() {
            $('#advertencia-modal-sm').modal("show");
        }
        //function ShowModal_Guardar() {
        //    $('#guardar-modal-sm').modal("show");
        //}
        //function ShowModal_Info() {
        //    $('#Info-modal-sm').modal("show");
        //}

    </script>
</asp:Content>
