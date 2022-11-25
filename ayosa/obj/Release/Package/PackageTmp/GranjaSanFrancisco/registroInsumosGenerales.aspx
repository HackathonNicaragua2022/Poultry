﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="registroInsumosGenerales.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.registroInsumosGenerales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registro de Insumos por Lote</title>
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <%--Footable vercion v3.1.6--%>
    <link href="../footablev316/css/footable.bootstrap.min.css" rel="stylesheet" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
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
    <div class="jumbotron jumbotron-image shadow m-1 border-primary" style="background-image: url(../imagenes/comederosa.jpeg)">
        <h2 class="mb-4 text-white"><b>INSUMOS GENERALES POR LOTE EN PROCESO</b></h2>
        <p class="mb-4 text-white">
            Registre la informacion de insumos por lote seleccionado
        </p>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up_principal" runat="server">
        <ProgressTemplate>
            <div class="progress">
                <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%; height: 40px">Cargando...</div>
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
                <h3 class="text-primary">DATOS DE INSUMOS</h3>
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="dr_CategoriaInsumo">Categorias Insumos</label>
                            <div>
                                <asp:DropDownList ID="dr_CategoriaInsumo" name="dr_CategoriaInsumo" class="custom-select" aria-describedby="dr_CategoriaInsumo_HelpBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dr_CategoriaInsumo_SelectedIndexChanged"></asp:DropDownList>
                                <span id="dr_CategoriaInsumo_HelpBlock" class="form-text text-muted">Seleccione una categoria para mostrar los insumos</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="dr_Insumo">Seleccione un Insumo</label>
                            <div>
                                <asp:DropDownList ID="dr_Insumo" OnSelectedIndexChanged="dr_Insumo_SelectedIndexChanged" name="dr_Insumo" class="custom-select" aria-describedby="dr_Insumo_HelpBlock" runat="server"></asp:DropDownList>
                                <span id="dr_Insumo_HelpBlock" class="form-text text-muted">Seleccione el insumo para su registro</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-12">
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
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="txt_concepto">Concepto de</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fa fa-book"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_concepto" name="txt_concepto" placeholder="" type="text" class="form-control" aria-describedby="txt_concepto_ialHelpBlock" />
                            </div>
                            <span id="txt_concepto_ialHelpBlock" class="form-text text-muted">(Opcional) Concento del Registro del Insumo</span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-sm-12">
                        <div class="form-group">
                            <label for="txt_Cantidad">Cantidad und</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fa fa-money-bill"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_Cantidad" name="txt_Cantidad" placeholder="0" type="text" class="form-control" aria-describedby="txt_Cantidad_ialHelpBlock" />
                            </div>
                            <span id="txt_Cantidad_ialHelpBlock" class="form-text text-muted">Cantidad del Insumo utilizado</span>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <div class="form-group">
                            <label for="txt_CostoPorUnidad">Costo por Unidad C$</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fa fa-money"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_CostoPorUnidad" name="txt_CostoPorUnidad" placeholder="0" type="text" class="form-control" aria-describedby="txt_CostoPorUnidad_ialHelpBlock" />
                            </div>
                            <span id="txt_CostoPorUnidad_ialHelpBlock" class="form-text text-muted">Costo por unidad o bien por la medida, para calculo de coste total</span>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <div class="form-group">
                            <label for="txt_CosteTotal">Costo Total C$</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fa fa-money"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_CosteTotal" name="txt_CosteTotal" placeholder="0" type="text" class="form-control" aria-describedby="txt_CosteTotal_ialHelpBlock" />
                            </div>
                            <span id="txt_CosteTotal_ialHelpBlock" class="form-text text-muted">Coste Total del Insumo</span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button Text="GUARDAR INSUMO DE LOTE SELECCIONADO" OnClick="btn_GuardarInsumo_Click" class="btn btn-primary btn-lg elevation-2 border-dark" ID="btn_GuardarInsumo" CausesValidation="false" UseSubmitBehavior="false" runat="server" />
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../footablev316/js/footable.min.js"></script>
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <%--imprimir seccion de la pagina--%>
    <script>
        document.getElementById("<%=txt_Cantidad.ClientID%>").onkeyup = costoTotal;
        document.getElementById("<%=txt_CostoPorUnidad.ClientID%>").onkeyup = costoTotal;
        function costoTotal() {
            try {
                var cantidad = document.getElementById('<%=txt_Cantidad.ClientID%>').value;
                var costounitario = document.getElementById('<%=txt_CostoPorUnidad.ClientID%>').value;
                var costoTotal = (parseFloat(cantidad) * parseFloat(costounitario));
                document.getElementById('<%=txt_CosteTotal.ClientID%>').value = costoTotal;
            } catch (error) {
                //console.error(error);
                // expected output: ReferenceError: nonExistentFunction is not defined
                // Note - error messages will vary depending on browser
            }
        };



        $(document).ready(function () {
            $('.table').footable();
        })
        function sweetAlertConfirm(btnDelete) {
            if (btnDelete.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnDelete.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                Swal.fire({
                    icon: 'warning',
                    title: '¿Esta completamente seguro que desea eliminar este elemento?',
                    text: "No podra deshacer los cambios una vez eliminado!",
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si, Eliminalo !',
                }).then(function (result) {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        //    // Set data-confirmed attribute to indicate that the action was confirmed
                        btnDelete.dataset.confirmed = true;
                        //    // Trigger button click programmatically
                        btnDelete.click();
                    } else {
                        Swal.fire('Cancelado', 'No se eliminara ningun elemento', 'info')
                    }

                    //.then(function () {
                    //    // Set data-confirmed attribute to indicate that the action was confirmed
                    //    btnDelete.dataset.confirmed = true;
                    //    // Trigger button click programmatically
                    //    btnDelete.click();
                    //}).catch(function (reason) {
                    //    // The action was canceled by the user                   
                    //});
                });
            }
        }
    </script>

    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {


                    document.getElementById("<%=txt_Cantidad.ClientID%>").onkeyup = costoTotal;
                    document.getElementById("<%=txt_CostoPorUnidad.ClientID%>").onkeyup = costoTotal;


                    $('.table').footable();
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
        //ALERTA DE PREGUNTA
        function modal_alert(titulo, mensaje, tipo) {
            $(function () {
                Swal.fire(titulo, mensaje, tipo);
            });
        };
    </script>
</asp:Content>
