<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="FacturasCreditosComedor.aspx.cs" Inherits="POULTRY.mobilnavs.FacturasCreditosComedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Facturas de Creditos</title>
    <!-- daterange picker -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/daterangepicker/daterangepicker.css" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Facturas de Credito</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="menuComedorTrabajadores.aspx">Menu</a></li>
                        <li class="breadcrumb-item active">Facturas de Credito</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up_contenido" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-4 col-md-6 col-sm-12 invoice-col">
                        <div class="form-group">
                            <label for="text">Seleccione el Periodo de Facturacion</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">
                                        <i class="far fa-calendar-alt"></i>
                                    </span>
                                </div>
                                <asp:TextBox runat="server" aria-describedby="textHelpBlock" type="text" class="form-control float-right" ID="txt_rangofecha" />
                            </div>
                            <span id="textHelpBlock" class="form-text text-muted">seleccione el periodo de facturación para cargar los empleados y sus facturas</span>
                            <!-- /.input group -->
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <label for="btn_cargar">Cargar Facturas</label>
                        <div class="form-group">
                            <asp:Button ID="btn_cargar" Text="Cargar Facturas" runat="server" class="btn btn-primary form-control" aria-describedby="btncargar" OnClick="btn_cargar_Click" />
                            <span id="btncargar" class="form-text text-muted">Cargar las facturas para ese rango de fechas</span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <button type="button" class="btn btn-primary" onclick="printContent('FacturasCredito');">
                            <i class="fas fa-print"></i>&nbsp Imprimir Cuentas de Crédito
                        </button>
                    </div>
                </div>
                <br />
                <!-- /.row -->
                <!-- Main content -->
                <div class="invoice p-3 mb-3" id="FacturasCredito">
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
                            Facturas De Crédito    
                        </div>
                        <!-- /.col -->
                        <div class="col-sm-6 invoice-col">
                            <b>ID Usuario&nbsp;&nbsp;</b><asp:Label Text="IDUsuario" ID="lbl_IDUsuario" runat="server" /><br />
                            <br />
                            <b>Cliente:&nbsp;</b>
                            <asp:Label Text="Cliente" ID="lbl_Nombre" runat="server" /><br />
                            <b>Area&nbsp;&nbsp;&nbsp: </b>
                            <asp:Label Text="Area" ID="lbl_Area" runat="server" /><br />
                            <b>Puesto Laboral: &nbsp;</b>
                            <asp:Label Text="Puesto" ID="lbl_Puesto" runat="server" />

                        </div>
                        <!-- /.col -->
                    </div>
                    <br />
                    <!-- Table row -->
                    <div class="row">
                        <div class="col-12 table-responsive">
                            <asp:GridView ID="gv_Facturas_Pendientes" runat="server" AutoGenerateColumns="false" DataKeyNames="ID_Factura" OnRowDataBound="gv_Facturas_Pendientes_RowDataBound" RowStyle-Wrap="true"
                                CssClass="table table-sm table-condensed  table-bordered table-striped" OnPreRender="gv_Facturas_Pendientes_PreRender" EmptyDataText="Felicidades No tienes Facturas Pendientes para ese rango de fechas seleccionado">
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
                                                <asp:GridView ID="gv_Detalle" runat="server" AutoGenerateColumns="false" DataKeyNames="ID_Factura"
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
                    <!-- /.row -->
                    <br />
                    <div class="row">
                        <!-- /.col -->
                        <div class="col-6">
                            <p class="lead">TOTALES</p>
                            <div class="table-responsive">
                                <table class="table">
                                    <tr>
                                        <th style="width: 50%">Total a cobrar:</th>
                                        <td>
                                            <asp:Label Text="" ID="lbl_totalCobrar" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <th>Total Articulos</th>
                                        <td>
                                            <asp:Label Text="" ID="lbl_cantidadArticulosTotal" runat="server" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="../js/bootstrap4-toggle.min.js"></script>

    <!-- date-range-picker -->
    <script src="../AdminTemplate/plugins/daterangepicker/daterangepicker.js"></script>

    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>

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
                $("#<%=gv_Facturas_Pendientes.ClientID%>").footable();
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
                    $("#<%=gv_Facturas_Pendientes.ClientID%>").footable();
                  //  $("ContentPlaceHolder1_gv_Facturas_Pendientes_gv_Detalle_0").footable();
                    //------------------------------------------                                          
                }
            });
        };
    </script>
    <script>
        //Date range picker
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
        function ShowtoastMessage() {
            $('.toast').toast('show');
        }
    </script>
</asp:Content>
