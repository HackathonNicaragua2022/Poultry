<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="RegistroAlimentos.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.RegistroAlimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registro Alimentos</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <style>
        .jumbotron-image {
            background-position: center center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .dropdown-menu {
            background-color: #424240;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron jumbotron-image shadow m-1 border-primary" style="background-image: url(../imagenes/silos.png)">
        <div class="row">
            <div class="col text-white">
                <h2 class="mb-4"><b>CONTROL DE ALIMENTOS PARA EL LOTE SELECCIONADO</b></h2>
                <p class="mb-4">
                    Ingrese la informacion del consumo de alimentos de la galera en crecimiento
                </p>
            </div>
            <div class="col text-white  float-right text-right">
                <asp:Image CssClass="float-right" ImageAlign="Right" ImageUrl="~/imagenes/POULTRY.png" runat="server" Width="150px" />
            </div>
        </div>
    </div>
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
                                                <i class="fas fa-plus-square text-white"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="callout callout-warning">
                <h3 class="text-primary">DATOS DE CONSUMO
                </h3>
                <div class="form-group">
                    <label for="dr_TipoAlimento">Tipo de Alimento</label>
                    <div>
                        <asp:DropDownList ID="dr_TipoAlimento" name="dr_TipoAlimento" class="custom-select" aria-describedby="dr_TipoAlimentoHelpBlock" runat="server"></asp:DropDownList>
                        <span id="dr_TipoAlimentoHelpBlock" class="form-text text-muted">Seleccione el Tipo de Alimento que registrara para el consumo</span>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txt_FechaRegistro">Fecha de Registro</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <i class="fa fa-calendar"></i>
                            </div>
                        </div>
                        <asp:TextBox runat="server" ID="txt_FechaRegistro" name="txt_FechaRegistro" TextMode="DateTimeLocal" class="form-control" aria-describedby="txt_FechaRegistroHelpBlock" />
                    </div>
                    <span id="txt_FechaRegistroHelpBlock" class="form-text text-muted">Ingrese la fecha del registro del consumo de alimento, esta fecha y hora se usara como dato inicializador del siguiente registro (Fecha Inicial de Consumo)</span>
                </div>
                <div class="form-group">
                    <label for="txt_InventarioInicial">Inventario Inicial</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <i class="fa fa-calculator"></i>
                            </div>
                        </div>
                        <asp:TextBox runat="server" ID="txt_InventarioInicial" name="txt_InventarioInicial" placeholder="0 Qtl" type="text" class="form-control" aria-describedby="txt_InventarioInicialHelpBlock" />
                    </div>
                    <span id="txt_InventarioInicialHelpBlock" class="form-text text-muted">Quintales iniciales en los Silos, se toma automaticamente el Registro de Inventario Final anterior como Inicial</span>
                </div>
                <div class="form-group">
                    <label for="txt_QuintalesRecibidos">Quintales Recibidos</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <i class="fa fa-calculator"></i>
                            </div>
                        </div>
                        <asp:TextBox runat="server" ID="txt_QuintalesRecibidos" name="txt_QuintalesRecibidos" placeholder="0 Qtl" type="text" class="form-control" aria-describedby="txt_QuintalesRecibidosHelpBlock" />
                    </div>
                    <span id="txt_QuintalesRecibidosHelpBlock" class="form-text text-muted">Quintales Recibidos en la Fecha seleccionada</span>
                </div>
                <div class="form-group">
                    <label for="txt_InventarioFinal">Inventario Final</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <i class="fa fa-inbox"></i>
                            </div>
                        </div>
                        <asp:TextBox runat="server" ID="txt_InventarioFinal" name="txt_InventarioFinal" placeholder="0" type="text" class="form-control" aria-describedby="txt_InventarioFinalHelpBlock" />
                    </div>
                    <span id="txt_InventarioFinalHelpBlock" class="form-text text-muted">Total de Inventario Final</span>
                </div>
                <div class="form-group">
                    <label for="txt_ConsumoDiario">Consumo Diario</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <i class="fa fa-apple"></i>
                            </div>
                        </div>
                        <asp:TextBox runat="server" ID="txt_ConsumoDiario" name="txt_ConsumoDiario" placeholder="Consumo Diario" type="text" class="form-control" aria-describedby="txt_ConsumoDiarioHelpBlock" />
                    </div>
                    <span id="txt_ConsumoDiarioHelpBlock" class="form-text text-muted">Consumo Diario</span>
                </div>
                <div class="form-group">
                    <label for="txt_ComentariosAdicionales">Comentarios</label>
                    <asp:TextBox runat="server" ID="txt_ComentariosAdicionales" name="txt_ComentariosAdicionales" cols="40" Rows="5" class="form-control" aria-describedby="txt_ComentariosAdicionalesHelpBlock" TextMode="MultiLine" />
                    <span id="txt_ComentariosAdicionalesHelpBlock" class="form-text text-muted">Comentarios Adicionales sobre el Registro</span>
                </div>
                <div class="form-group">
                    <asp:Button Text="GUARDAR CONSUMO DE ALIMENTO" OnClick="btn_GuardarConsumo_Click" class="btn btn-primary" ID="btn_GuardarConsumo" CausesValidation="false" UseSubmitBehavior="false" runat="server" />
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
        document.getElementById('<%=txt_InventarioInicial.ClientID%>').onkeyup = ConsumoDiario;
        document.getElementById('<%=txt_QuintalesRecibidos.ClientID%>').onkeyup = ConsumoDiario;
        document.getElementById('<%=txt_InventarioFinal.ClientID%>').onkeyup = ConsumoDiario;
        function ConsumoDiario() {
            try {
                var inventarioI = document.getElementById('<%=txt_InventarioInicial.ClientID%>').value;
                var quintalesRecibidos = document.getElementById('<%=txt_QuintalesRecibidos.ClientID%>').value;
                var InventarioFinal = document.getElementById('<%=txt_InventarioFinal.ClientID%>').value;
                var ConsumoDiario = (parseFloat(inventarioI) + parseFloat(quintalesRecibidos)) - parseFloat(InventarioFinal);
                document.getElementById('<%=txt_ConsumoDiario.ClientID%>').value = ConsumoDiario;
            } catch (error) {
                //console.error(error);
                // expected output: ReferenceError: nonExistentFunction is not defined
                // Note - error messages will vary depending on browser
            }
        };
    </script>
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




                    //---------------------------------------------------------------
                    document.getElementById('<%=txt_InventarioInicial.ClientID%>').onkeyup = ConsumoDiario;
                    document.getElementById('<%=txt_QuintalesRecibidos.ClientID%>').onkeyup = ConsumoDiario;
                    document.getElementById('<%=txt_InventarioFinal.ClientID%>').onkeyup = ConsumoDiario;
                    function ConsumoDiario() {
                        try {
                            var inventarioI = document.getElementById('<%=txt_InventarioInicial.ClientID%>').value;
                            var quintalesRecibidos = document.getElementById('<%=txt_QuintalesRecibidos.ClientID%>').value;
                            var InventarioFinal = document.getElementById('<%=txt_InventarioFinal.ClientID%>').value;
                            var ConsumoDiario = (parseFloat(inventarioI) + parseFloat(quintalesRecibidos)) - parseFloat(InventarioFinal);
                            document.getElementById('<%=txt_ConsumoDiario.ClientID%>').value = ConsumoDiario;
                        } catch (error) {
                            //console.error(error);
                            // expected output: ReferenceError: nonExistentFunction is not defined
                            // Note - error messages will vary depending on browser
                        }
                    };
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
