<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="cancelarfacturas.aspx.cs" Inherits="POULTRY.cancelarfacturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Cancelacion de Facturas</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link href="../css/personal.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="container">
                <!-- Jumbotron -->
                <div class="p-5 text-center bg-light">
                    <h1 class="mb-3">Cancelacion de facturas de Crédito en Comedor</h1>
                    <h4 class="mb-3">Facturas por Empleado</h4>
                    <h5 class="mb-3">
                        <asp:Label ID="lbl_TituloModo" runat="server" Text=""></asp:Label></h5>
                    <p class="text-lg-center">Esta vetana Cancela Facturas de Credito y Genera una entrada de efectivo en la CajaChica aun si esta Cerrada</p>
                </div>
                <!-- Jumbotron -->
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label for="txt_buscar">BUSCAR CLIENTE</label>
                            <asp:TextBox ID="txt_buscar" runat="server" class="form-control" MaxLength="30" OnTextChanged="txt_buscar_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lbl_Cliente">CLIENTE</label>
                            <h3>
                                <asp:Label ID="lbl_Cliente" runat="server" Text=""></asp:Label>
                            </h3>
                        </div>
                    </div>
                </div>
                <asp:Panel runat="server" ID="Empleados" Visible="false">
                    <h3>Empleados encontrados</h3>
                    <asp:ListView runat="server" ID="listCliente" DataKeyNames="ID_Trabajador">
                        <LayoutTemplate>
                            <ul class="list-group" id="itemPlaceholder" runat="server">
                                <li class="list-group-item">
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <asp:Label Text='<%# Eval("trabajador") %>' runat="server" ID="Label1" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="Button1" runat="server" Text="Seleccionar" CssClass="btn btn-info" OnClick="btn_SeleccionarEmpleado_Click" />
                                        </div>
                                    </div>
                                </li>
                                <asp:Button ID="btn_SeleccionarEmpleado" runat="server" Text="Seleccionar" CssClass="btn btn-info" OnClick="btn_SeleccionarEmpleado_Click" />
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <ul class="col-md-12 list-group">
                                <li class="list-group-item">
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <asp:Label Text='<%# Eval("trabajador") %>' runat="server" ID="Label1" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btn_SeleccionarEmpleado" runat="server" Text="Seleccionar" CssClass="btn btn-info" OnClick="btn_SeleccionarEmpleado_Click" />
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </ItemTemplate>
                    </asp:ListView>
                </asp:Panel>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label for="btn_FacturasPendientes">Cargar Facturas Pendientes</label>
                            <asp:Button ID="btn_FacturasPendientes" runat="server" Text="Mostrar Facturas Pendientes" CssClass="btn btn-success  form-control" OnClick="btn_FacturasPendientes_Click" />
                        </div>
                    </div>
                    <%--  <div class="col-sm-3">
                        <div class="form-group">
                            <label for="btn_FacturasCanceladas">Cargar Facturas Canceladas</label>
                            <asp:Button ID="btn_FacturasCanceladas" runat="server" Text="Mostrar Facturas Canceladas" CssClass="btn btn-warning  form-control" OnClick="btn_FacturasCanceladas_Click" />
                        </div>
                    </div>--%>
                    <div class="col pull-right">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="btn_CancelarTodasLasPendientes">Cancelar Todas las Facturas</label>
                                    <asp:Button ID="btn_CancelarTodasLasPendientes" runat="server" Text="Cancelar Totas las Facturas" CssClass="btn btn-danger  form-control" OnClick="btn_CancelarTodasLasPendientes_Click" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                            </div>
                        </div>
                    </div>
                    <%--  <div class="col-sm-3">
                        <label for="date">Fecha Inicial</label>
                        <div class="input-group date" data-provide="datepicker">
                            <input class="form-control" id="date" name="date" placeholder="" type="text" readonly="true" onchange="fechashange()" value="<%=this.fechaeditar1%>" />
                            <div class="input-group-addon">
                                <span class="glyphicon glyphicon-th"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <label for="date2">Fecha final</label>
                        <div class="input-group date" data-provide="datepicker">
                            <input class="form-control" id="date2" name="date" placeholder="" type="text" readonly="true" onchange="fechashange()" value="<%=this.fechaeditar1%>" />
                            <div class="input-group-addon">
                                <span class="glyphicon glyphicon-th"></span>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col">
                        <table class="table table-clear">
                            <tbody>
                                <tr>
                                    <td class="left">
                                        <strong class="text-dark">Facturas Totales</strong>
                                    </td>
                                    <td class="right">
                                        <asp:Label ID="lbl_facturasTotales" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <strong class="text-dark">Productos Total</strong>
                                    </td>
                                    <td class="right">
                                        <asp:Label ID="lbl_TotalProductos" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <strong class="text-dark">Monto Total a Cobrar</strong>
                                    </td>
                                    <td class="right">C$<asp:Label ID="lbl_TotalCobrar" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <strong class="text-dark">Cobrar</strong> </td>
                                    <td class="right">
                                        <strong class="text-dark">C$<asp:Label ID="lbl_Cobrar" runat="server" Text="0"></asp:Label></strong>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:GridView ID="gv_FacturasPendientes" DataKeyNames="ID_Factura" CssClass="table table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" OnRowCommand="gv_FacturasPendientes_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="FACTURA">
                                    <ItemTemplate>
                                        <b><asp:Label runat="server" Text='<%# Bind("ID_Factura") %>' CssClass="headShake" ID="lbl_numeroFactura"></asp:Label></b>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FECHA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("fecha") %>' ID="lbl_fecha"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EMPLEADO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("Trabajador") %>' ID="lbl_Empleado"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Facturado Por">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("FacturadoPor") %>' ID="lbl_Facturadox"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monto Total Factura">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("MontoTotalFactura") %>' ID="lbl_MontoTotalFactura"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Productos">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("NumerodeProductos") %>' ID="lbl_TotalProductos"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CANCELAR">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Cancelar" data-toggle="tooltip" title="Cancela Individualmente la Factura de este Trabajador" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_CancelarFactura" CssClass="btn btn-success form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-check-double"></i> 
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DETALLE">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Detalles" data-toggle="tooltip" title="Muestra Los detalles de la factura" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="detalle" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-list"></i> 
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>



                <%--=========================================================================================================================================--%>
                <div class="modal fade" id="modalInfo" tabindex="-1" role="dialog" aria-labelledby="labeInfo">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="labeInfo">Atencion!!</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <div class="text-center">
                                    <img id="imagenModalInfo" src="~/imagenes/camera_test.png" alt="Ok" class="img-responsive center-block" runat="server" /><hr />
                                    <asp:Label ID="lbl_modalInfo" runat="server" Text="Label" Width="50%">mensaje</asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <%--=========================================================================================================================================--%>

                <%--=========================================================================================================================================--%>
                <div class="modal fade" id="modalDetalle" tabindex="-1" role="dialog" aria-labelledby="lblMOdalDetalle">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="lblMOdalDetalle">Detalle de la Factura</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">

                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gv_Detalles" CssClass="table table-dark table-striped table-hover" runat="server" EnableViewState="False">
                                        </asp:GridView>
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
                                    <asp:Label ID="lbl_MsgAdvertencia" runat="server" Text="Label" Width="50%">mensaje</asp:Label>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12">
                                            <label for="btn_Continuar"></label>
                                            <asp:Button ID="btn_Continuar" runat="server" Text="Continuar" CssClass="btn btn-warning form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />
                                            <asp:Button ID="btn_CancelarIndividual" Visible="false" runat="server" Text="Cancelar Factura" CssClass="btn btn-danger form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_CancelarIndividual_Click" UseSubmitBehavior="false" />
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
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:HiddenField ID="hf_fecha" runat="server" />
    <asp:HiddenField ID="hf_fecha2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <%--<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>--%>
    <%--  <script>
        $(document).ready(function () {
            var date_input = $('input[name="date"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            var options = {
                format: 'yyyy-mm-dd',
                container: container,
                todayHighlight: true,
                autoclose: true,
                monthsFull: ['enero', 'febrero', 'marzo', 'abril', 'mayo', 'junio', 'julio', 'agosto', 'septiembre', 'octubre', 'noviembre', 'diciembre'],
                monthsShort: ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic'],
                weekdaysFull: ['domingo', 'lunes', 'martes', 'miércoles', 'jueves', 'viernes', 'sábado'],
                weekdaysShort: ['dom', 'lun', 'mar', 'mié', 'jue', 'vie', 'sáb'],
                today: 'hoy',
                clear: 'borrar',
                close: 'cerrar',
                firstDay: 1,
                format: 'dddd d !de mmmm !de yyyy',
                formatSubmit: 'yyyy/mm/dd'
            };
            date_input.datepicker(options);
        })
        function fechashange() {
            var fecha = document.getElementById('date').value;
            document.getElementById('<%= hf_fecha.ClientID %>').value = fecha;
            var fecha = document.getElementById('date2').value;
            document.getElementById('<%= hf_fecha2.ClientID %>').value = fecha;
        }
    </script>--%>

    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_FacturasPendientes.ClientID%>").footable();
            });
        });

        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }
        function ShowModalDetalle() {
            $('#modalDetalle').modal('show');
        }
        function ShowModalAdvertencia() {
            $('#modalAdvertencia').modal("show");
        }
    </script>
</asp:Content>
