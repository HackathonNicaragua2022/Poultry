<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="ajutesInventarioLoteGranja.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.ajutesInventarioLoteGranja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ajuste de Inventario de lote en Crecimiento</title>
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
    <div class="jumbotron jumbotron-image shadow m-1 border-primary" style="background-image: url(../imagenes/galera-exterior.png);">
        <div class="row">
            <div class="col-10">
                <h2 class="mb-4 text-white"><b>AJUSTE DE INVENTARIOS DE LOTES INGRESADOS</b></h2>
                <p class="mb-4 text-white">
                    Ajuste del inventario ingresado previamente, los parametros generales seran los mismo del lote inicilizado, solo se agregara ina entrada al inventario inicial
                </p>
            </div>
            <div class="col-md-2 col-sm-12 float-right">
                <img src="../imagenes/granjaicono.png" width="128" alt="corazon medico" />
            </div>
        </div>
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
                <h3 class="text-primary">DATOS PARA AJUSTE DE LOTE</h3>
                <div class="row">
                    <div class="col-md-12 col-sm-12">
                        <div class="form-group">
                            <label for="txt_motivoAjuste">NOTA DE AJUSTE</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txt_motivoAjuste" name="txt_Cantidad" TextMode="MultiLine" MaxLength="520" placeholder="" type="text" class="form-control" aria-describedby="txt_motivoAjuste_ialHelpBlock" />
                            </div>
                            <span id="txt_motivoAjuste_ialHelpBlock" class="form-text text-muted">Ingrese una nota espeficicando el motivo de ingreso de aves al lote ya iniciado</span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-sm-12">
                        <div class="form-group">
                            <label for="txt_cantidadRecibida">Cantidad Recibida</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fas fa-feather"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_cantidadRecibida" TextMode="Number" name="txt_CostoPorFrasco" placeholder="0" type="text" class="form-control" aria-describedby="txt_cantidadRecibida_HelpBlock" />
                            </div>
                            <span id="txt_cantidadRecibida_HelpBlock" class="form-text text-muted">Cantidad de aves recibidas</span>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <div class="form-group">
                            <label for="txt_MortalidadRegistrada">Mortalidad Registrada</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fas fa-feather"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_MortalidadRegistrada" TextMode="Number" name="txt_MortalidadRegistrada" placeholder="0" type="text" class="form-control" aria-describedby="txt_MortalidadRegistrada_HelpBlock_HelpBlock" />
                            </div>
                            <span id="txt_MortalidadRegistrada_HelpBlock" class="form-text text-muted">Mortalidad encotrada durante la revicion</span>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <div class="form-group">
                            <label for="txt_CantidadIngresada">Cantidad a Ingresar</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fas fa-feather"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_CantidadIngresada" name="txt_CantidadIngresada" placeholder="0" type="text" class="form-control" aria-describedby="txt_CantidadIngresada_HelpBlock" />
                            </div>
                            <span id="txt_CantidadIngresada_HelpBlock" class="form-text text-muted">Cantidad Total a Ingresar al Lote</span>
                        </div>
                    </div>

                </div>
            </div>
            <div class="callout callout-info">
                <h3 class="text-primary">DATOS LOTE SELECCIONADO PAR AJUSTE</h3>
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="txt_DiasActualizar">Dias Lote</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fas fa-calendar-alt"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_DiasActualizar" name="txt_CantidadIngresada"  placeholder="0" type="text" class="form-control" aria-describedby="txt_DiasActualizar_HelpBlock" />
                            </div>
                            <span id="txt_DiasActualizar_HelpBlock" class="form-text text-muted">Muestra la edad en Dias de lote a Ajustar, puede modificar la edad para ajustar el lote</span>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="txt_Dias">Numero Dias actualizar</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">
                                        <i class="fas fa-calendar-alt"></i>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txt_Dias" name="txt_Dias" TextMode="Number" placeholder="0" type="text" class="form-control" aria-describedby="txt_Dias_HelpBlock" />
                            </div>
                            <span id="txt_Dias_HelpBlock" class="form-text text-muted">Numero de Dias para ajustar la Edad</span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button Text="INGRESAR AVES A INVENTARIO SELECCIONADO" OnClick="btn_IngresoAjuste_Click" class="btn btn-primary btn-lg elevation-2 border-dark" ID="btn_IngresoAjuste" CausesValidation="false" UseSubmitBehavior="false" runat="server" />
                </div>
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
        document.getElementById("<%=txt_cantidadRecibida.ClientID%>").onkeyup = cantidadIngresada;
        document.getElementById("<%=txt_MortalidadRegistrada.ClientID%>").onkeyup = cantidadIngresada;
        function cantidadIngresada() {
            try {
                var cantidadRecibida = document.getElementById('<%=txt_cantidadRecibida.ClientID%>').value;
                var mortalidadRecibida = document.getElementById('<%=txt_MortalidadRegistrada.ClientID%>').value;
                var cantidadIngresar = (parseInt(cantidadRecibida) - parseInt(mortalidadRecibida));
                document.getElementById('<%=txt_CantidadIngresada.ClientID%>').value = cantidadIngresar;
            } catch (error) {
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
                    document.getElementById("<%=txt_cantidadRecibida.ClientID%>").onkeyup = cantidadIngresada;
                    document.getElementById("<%=txt_MortalidadRegistrada.ClientID%>").onkeyup = cantidadIngresada;
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
