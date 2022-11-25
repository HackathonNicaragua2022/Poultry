<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="RegistroMortalidad.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.RegistroMortalidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registro de parametros</title>
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
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Parametros Lote</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homeGranja.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Parametros Lote</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up_principal" runat="server">
        <ProgressTemplate>
            <div class="progress">
                <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">Cargando...</div>
            </div>
            <br />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up_principal" runat="server">
        <ContentTemplate>
            <!-- Default box -->
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Parametros</h3>
                </div>
                <div class="card-body">
                    <div class="callout callout-info">
                        <h3 class="text-primary">DATOS LOTES EN PRODUCCION
                        </h3>
                        <asp:GridView ID="gv_LotesEnProduccion" DataKeyNames="ID_InventarioBroilers" OnRowDataBound="gv_LotesEnProduccion_RowDataBound" CssClass="table table-bordered table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" OnRowCommand="gv_LotesEnProduccion_RowCommand" EmptyDataText="No se encontro Lote en Produccion en el sistema!">
                            <Columns>
                                <asp:TemplateField HeaderText="N°">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NombreGalera" HeaderText="Galera">
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalIngreso" HeaderText="Total Ingreso" DataFormatString="{0:n} Aves">
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalMortalidad" HeaderText="Total Mortalidad" DataFormatString="{0:n} Aves">
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalEnPie" HeaderText="Total En Pie" DataFormatString="{0:n} Aves">
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalSalidas_RemisionesMatadero" HeaderText="Total Remisiones" DataFormatString="{0:n} Aves">
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EdadLote_Dias" HeaderText="Edad Dias">
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EdadLote_Semanas" HeaderText="Edad Semanas" DataFormatString="{0:n} Semanas">
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PesoPromedio" HeaderText="Peso Promedio (gr)" DataFormatString="{0:n} gr">
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PesoPromedioLibras" HeaderText="Peso Promedio (lbs)" DataFormatString="{0:n} lbs">
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalLibrasPesoVivoMatanza" HeaderText="Total Libras Remisiones" DataFormatString="{0:n} Aves">
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha_IngresoGalera" HeaderText="Fecha Ingreso a Galera" DataFormatString="{0:D}">
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Seleccionar" data-toggle="tooltip" title="Selecciona el lote en inventario para agregar los parametros" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Seleccionar" CssClass="btn btn-primary form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-plus-square"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="form-group row">
                        <label for="txt_FechaRegistro" class="col-4 col-form-label">Fecha Registro</label>
                        <div class="col-8">
                            <asp:TextBox runat="server" ID="txt_FechaRegistro" name="txt_FechaRegistro" class="form-control" aria-describedby="txt_FechaRegistroHelpBlock" TextMode="Date" />
                            <span id="txt_FechaRegistroHelpBlock" class="form-text text-muted">Registre la Mortalidad para la Fecha Indicada</span>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txt_Mortalidad" class="col-4 col-form-label">Mortalidad</label>
                        <div class="col-8">
                            <asp:TextBox runat="server" ID="txt_Mortalidad" name="txt_Mortalidad" type="text" class="form-control" aria-describedby="txt_MortalidadHelpBlock" TextMode="Number" />
                            <span id="txt_MortalidadHelpBlock" class="form-text text-muted">Total de Mortalidad Encontrada</span>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txt_PesoPromedio" class="col-4 col-form-label">Peso Promedio (gr)</label>
                        <div class="col-8">
                            <asp:TextBox runat="server" ID="txt_PesoPromedio" name="txt_PesoPromedio" type="text" class="form-control" aria-describedby="txt_PesoPromedioHelpBlock" TextMode="Number" />
                            <span id="txt_PesoPromedioHelpBlock" class="form-text text-muted">Peso Promedio Calculado para el Lote en Gramos</span>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txt_ComentarioAdicionales" class="col-4 col-form-label">Comentarios</label>
                        <div class="col-8">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txt_ComentarioAdicionales" name="txt_ComentarioAdicionales" cols="40" Rows="5" class="form-control" aria-describedby="txt_ComentarioAdicionalesHelpBlock" />
                            <span id="txt_ComentarioAdicionalesHelpBlock" class="form-text text-muted">Comentarios para el Registro</span>
                        </div>
                    </div>
                    <div class="input-group">
                        <label for="Ip_ImagenMortalida" class="col-4 col-form-label">Imagen Mortalidad</label>
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="label_Circulacion">Imagen</span>
                        </div>
                        <div class="custom-file">
                            <input type="file" class="custom-file-input" id="Ip_ImagenMortalida" aria-describedby="label_Circulacion" runat="server" />
                            <label class="custom-file-label" for="Ip_ImagenMortalida">Seleccione el archivo</label>
                        </div>
                    </div>
                    <br />
                    <div class="form-group row">
                        <label for="btn_guardarParametros" class="col-4 col-form-label">Guardar los Parametros</label>
                        <div class="col-8">
                            <asp:Button Text="GUARDAR PARAMETROS" CssClass="btn btn-primary" UseSubmitBehavior="false" CausesValidation="false" runat="server" ID="btn_guardarParametros" OnClick="btn_guardarParametros_Click" />
                            <span id="btn_guardarParametrosHelpBlock" class="form-text text-muted">Guardar todos los Campos para el lote</span>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $(<%=gv_LotesEnProduccion.ClientID%>).footable();
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(<%=gv_LotesEnProduccion.ClientID%>).footable();
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
    </script>
</asp:Content>
