<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="OrdenesIngreso.aspx.cs" Inherits="POULTRY.mobilnavs.OrdenesIngreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="CardEntradasOrdenes" runat="server">
        <div class="lead">
            Ingreso de ordenes en el dia
        </div>
        <div class="card card-blue">
            <div class="card-header">
                <h3 class="card-title">Ordenes de Ingreso del dia</h3>
                <div class="card-tools">
                    <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                        <i class="fas fa-minus"></i>
                    </button>
                </div>
            </div>
            <div class="card-body">
                <h4>Ordenes de Ingreso en el dia</h4>
                <span class="small text-muted">Muestra las ordenes ingresadas</span>
                <div class="form-group row">
                    <label for="txt_Dias" class="col-4 col-form-label">Días a Visualizar</label>
                    <div class="col-8">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <div class="input-group-text">
                                    <i class="fa fa-calendar"></i>
                                </div>
                            </div>
                            <asp:TextBox ID="txt_NumeroDias" ViewStateMode="Enabled" runat="server" TextMode="Number" AutoPostBack="true" OnTextChanged="txt_NumeroDias_TextChanged" name="txt_Dias" Text="1" type="text" class="form-control" aria-describedby="txt_DiasHelpBlock"></asp:TextBox>
                        </div>
                        <span id="txt_DiasHelpBlock" class="form-text text-muted">numero des días de ingresos a visualizar tomando la fecha actual como inicio</span>
                    </div>
                </div>
                <br />
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
                                </tr>
                                <tr style='<%#Eval("MostrarDetalle").ToString().Equals("False")?"display: none;": "display:normal;"%>'>
                                    <td colspan="4">
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
