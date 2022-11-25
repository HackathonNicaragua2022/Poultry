<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterSedeCentral.Master" AutoEventWireup="true" CodeBehind="facturasPorFechaCancelacion.aspx.cs" Inherits="POULTRY.Admin.facturasPorFechaCancelacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Facturas segun fecha Cancelacion</title>
    <!-- daterange picker -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/daterangepicker/daterangepicker.css" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Cancelacion de Facturas Comedor</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Comedor POULTRY </a></li>
                        <li class="breadcrumb-item">Fecha de Cancelacion de Facturas</li>
                        <li class="breadcrumb-item active">Facturas segun fecha Cancelacion</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- Main content -->
    <div class="invoice p-3 mb-3">
        <!-- title row -->
        <div class="row">
            <div class="col-12">
                <h4>
                    <i class="fas fa-book"></i>&nbsp Facturas Por Fecha                    
                </h4>
            </div>
            <!-- /.col -->
        </div>
        <!-- info row -->
        <div class="row">
            <!-- /.col -->
            <div class="col">
                <h2>POULTRY SYSTEM S.A</h2>
                <h5>
                    <asp:Label Text="" ID="lbl_fecha_facturas" runat="server" /></h5>
            </div>
        </div>
        <!-- /.row -->

        <!-- Table row -->
        <div class="row">
            <div class="col-12 table-responsive p-0">
                <asp:Repeater ID="rp_FacturasCanceladas" runat="server" OnItemCommand="rp_FacturasCanceladas_ItemCommand" OnItemDataBound="rp_FacturasCanceladas_ItemDataBound">
                    <HeaderTemplate>
                        <table id="tbl_facturasCanceladas" class="table table-bordered table-hover table-responsive-sm bg-white" data-show-toggle="false" data-expand-first="true" style="border-collapse: collapse">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>CLIENTE</th>
                                    <th>FACTURAS</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width: 1px;">
                                <h5><%#Container.ItemIndex + 1%></h5>
                            </td>
                            <td>
                                <h5><%#Eval("Trabajador")%></h5>
                            </td>
                            <td style="width: 1px;">
                                <asp:Repeater ID="rp_facturas" runat="server" OnItemDataBound="rp_facturas_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="tbl_facturas" class="table table-bordered table-hover table-responsive-sm bg-white" data-show-toggle="false" data-expand-first="true" style="border-collapse: collapse">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Fecha Compra</th>
                                                    <th>Facturado Por </th>
                                                    <th>Monto</th>
                                                    <th>Total Productos</th>
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
                                                <h5><%#Eval("Fecha_Factura_Hora")%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><%#Eval("FacturadoP")%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><%#Eval("MontoTotalFactura")%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><%#Eval("NumerodeProductos")%></h5>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Repeater ID="rp_detalleFacturas" runat="server">
                                                    <HeaderTemplate>
                                                        <table id="tbl_DetalleFacturas" class="table table-bordered table-hover table-responsive-sm bg-white" data-show-toggle="false" data-expand-first="true" style="border-collapse: collapse">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Fecha Compra</th>
                                                                    <th>Facturado Por </th>
                                                                    <th>Monto</th>
                                                                    <th>Total Productos</th>
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
                                                                <h5><%#Eval("Fecha_Factura_Hora")%></h5>
                                                            </td>
                                                            <td style="width: 1px;">
                                                                <h5><%#Eval("FacturadoP")%></h5>
                                                            </td>
                                                            <td style="width: 1px;">
                                                                <h5><%#Eval("MontoTotalFactura")%></h5>
                                                            </td>
                                                            <td style="width: 1px;">
                                                                <h5><%#Eval("NumerodeProductos")%></h5>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                    </table>   
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                    </table>   
                                    </FooterTemplate>
                                </asp:Repeater>
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

        <div class="row">
            <!-- /.col -->
            <div class="col-6">
                <p class="lead">TOTALES DE RANGO DE FECHA</p>
                <div class="table-responsive table-bordered table-dark">
                    <table class="table">
                        <tr>
                            <th>Total Clientes</th>
                            <td>
                                <h3>
                                    <asp:Label Text="" ID="lbl_totalClientes" runat="server" /></h3>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 50%">Total a cobrar:</th>
                            <td>
                                <h3>
                                    <asp:Label Text="" ID="lbl_totalCobrar" runat="server" /></h3>
                            </td>
                        </tr>
                        <tr>
                            <th>Total Productos</th>
                            <td class="align-content-lg-end">
                                <h3>
                                    <asp:Label Text="" ID="lbl_cantidadArticulosTotal" runat="server" /></h3>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->

    </div>
    <!-- /.invoice -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
