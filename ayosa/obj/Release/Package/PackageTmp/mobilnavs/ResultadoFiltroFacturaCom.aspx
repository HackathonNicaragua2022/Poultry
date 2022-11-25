<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="ResultadoFiltroFacturaCom.aspx.cs" Inherits="POULTRY.mobilnavs.ResultadoFiltroFacturaCom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Resultado</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Busqueda de Facturas</h1>
                    <h3>Numero Factura:
                            <asp:Label Text="00" ID="lbl_NumeroFactura" runat="server" />
                    </h3>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="menuComedorTrabajadores.aspx">Menu</a></li>
                        <li class="breadcrumb-item active">Busqueda de Facturas</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <div class="container-fluid">

        <br />
        <!-- /.row -->
        <!-- Main content -->
        <div class="invoice p-3 mb-3 bg-gradient-dark" id="FacturasCredito">
            <!-- title row -->
            <div class="row">
                <div class="col-12">
                    <h4>
                        <i class="fas fa-egg"></i>&nbsp;&nbsp;POULTRY SYSTEM S.A                   
                                        <small class="float-right">
                                            <asp:Label Text="0" ID="lbl_FechaInforme" runat="server" /></small>
                    </h4>
                </div>
                <!-- /.col -->
            </div>
            <!-- info row -->
            <div class="row invoice-info">
                <div class="col-sm-6 invoice-col">
                    Resultado de Factura   
                </div>
            </div>
            <br />
            <!-- Table row -->
            <div class="row">
                <div class="col-12 table-responsive">
                    <asp:GridView ID="gv_FacturaEncontrada" runat="server" AutoGenerateColumns="false" DataKeyNames="ID_Factura" OnRowDataBound="gv_FacturaEncontrada_RowDataBound" RowStyle-Wrap="true"
                        CssClass="table table-sm table-condensed table-dark  table-bordered table-striped" EmptyDataText="No se encontraron Facturas con el Condigo ingresado">
                        <Columns>
                            <asp:TemplateField HeaderText="N#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Fecha_Factura_Hora" HeaderText="Fecha">
                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FacturadoP" HeaderText="Facturado x">
                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ID_Factura" HeaderText="Factura">
                                <ItemStyle HorizontalAlign="Left" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MontoTotalFactura" HeaderText="Monto Total" DataFormatString="{0:C}">
                                <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NumerodeProductos" HeaderText="Total Productos" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Detalle">
                                <ItemTemplate>
                                    <div id="div<%# Eval("ID_Factura") %>">
                                        <asp:GridView ID="gv_Detalle" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-sm table-dark table-condensed  table-bordered table-striped">
                                            <Columns>
                                                <%--  <asp:TemplateField HeaderText="N#">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <asp:BoundField DataField="MedidaProducto" HeaderText="Medida">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NombreProducto" HeaderText="Producto">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" DataFormatString="{0:n}">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PrecioUnidad" HeaderText="Precio Und" DataFormatString="{0:C}">
                                                    <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PrecioTotal" HeaderText="SubTotal" DataFormatString="{0:C}">
                                                    <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <!-- /.col -->
            </div>


        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <%--PERMITE IMPRIMIR UNA PORCION DE LA PAGINA--%>
    <script>
        function printContent(el) {
            var restorepage = $('body').html();
            var printcontent = $('#' + el).clone();
            $('body').empty().html(printcontent);
            window.print();
            $('body').html(restorepage);
        }
    </script>

    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_FacturaEncontrada.ClientID%>").footable();
                //    $("ContentPlaceHolder1_gv_Facturas_Pendientes_gv_Detalle_0").footable();
            });
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
                    $("#<%=gv_FacturaEncontrada.ClientID%>").footable();
                    //  $("ContentPlaceHolder1_gv_Facturas_Pendientes_gv_Detalle_0").footable();
                    //------------------------------------------                                          
                }
            });
        };
    </script>
</asp:Content>
