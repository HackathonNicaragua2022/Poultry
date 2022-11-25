<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterControlHuevos.Master" AutoEventWireup="true" CodeBehind="~/ModuloClasificadoraHuevos/InventarioHuevoSinClasificar.aspx.cs" Inherits="POULTRY.ModuloClasificadoraHuevos.InventarioHuevoSinClasificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inventario Huevo sin clasificar</title>
    <!-- daterange picker -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/daterangepicker/daterangepicker.css" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <style>
        .img-responsive {
            display: block;
            margin: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Inventario Huevo sin Clasificar</h5>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homebodegahuevo.aspx">Clasificadora de Huevo</a></li>
                        <li class="breadcrumb-item active">Inventario Huevo Sin clasificar</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <div class="row">
        <div class="col-md-4 col-sm-12">
            <div class="card card-primary">
                <div class="card-header">
                    <h3 class="card-title">Inventario Huevo sin clasificar</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                            <i class="fas fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="card-body">
                    <h4>Tipo Huevo</h4>
                    <asp:Repeater ID="rp_inventariohsc" runat="server" OnItemCommand="rp_inventariohsc_ItemCommand">
                        <ItemTemplate>
                            <div class="info-box">
                                <span class="info-box-icon bg-gradient-navy"><i class="ion ion-egg"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-text text-uppercase"><b><%#Eval("TipoHuevo")%></b></span>
                                    <ul class="nav flex-column">
                                        <li class="nav-item">Total Entradas <span class="float-right badge bg-primary"><%#Eval("TOTALE")%> &nbsp;Cajillas</span>
                                        </li>
                                        <li class="nav-item">Total Salidas Clasificados <span class="float-right badge bg-info"><%#Eval("TS")%>&nbsp;Cajillas</span>
                                        </li>
                                        <li class="nav-item">Saldo Pendiente por Clasificar<span class="float-right badge bg-success"><%#Eval("SALDO")%>&nbsp;Cajillas</span>
                                        </li>
                                        <%--<li class="nav-item">Promedio Dias Produccion<span class="float-right badge bg-success"><%#Eval("PROMEDIO_DIAS")%>&nbsp;Dias</span>
                                        </li>--%>
                                    </ul>
                                    <asp:LinkButton ID="btn_Detalles" CommandName="cmd_MostarDetalle" CommandArgument='<%#Eval("ID_TipoHUevo")%>' CssClass="small text-muted link-muted" runat="server">Click para ver las cantidades por produccion</asp:LinkButton>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                            <!-- /.info-box -->
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div class="info-box">
                                <span class="info-box-icon bg-gradient-green"><i class="ion ion-egg"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-text text-uppercase"><b><%#Eval("TipoHuevo")%></b></span>
                                    <ul class="nav flex-column">
                                        <li class="nav-item">Total Entradas <span class="float-right badge bg-primary"><%#Eval("TOTALE")%>&nbsp;Cajillas</span>
                                        </li>
                                        <li class="nav-item">Total Salidas Clasificados <span class="float-right badge bg-info"><%#Eval("TS")%>&nbsp;Cajillas</span>
                                        </li>
                                        <li class="nav-item">Saldo Pendiente por Clasificar<span class="float-right badge bg-success"><%#Eval("SALDO")%>&nbsp;Cajillas</span>
                                        </li>
                                        <%--   <li class="nav-item">Promedio Dias Produccion<span class="float-right badge bg-success"><%#Eval("PROMEDIO_DIAS")%>&nbsp;Dias</span>
                                        </li>--%>
                                    </ul>
                                    <asp:LinkButton ID="btn_Detalles" CommandName="cmd_MostarDetalle" CommandArgument='<%#Eval("ID_TipoHUevo")%>' CssClass="small text-muted link-muted" runat="server">Click para ver las cantidades por produccion</asp:LinkButton>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                            <!-- /.info-box -->
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

        </div>
        <div class="col">
            <div class="card card-dark">
                <div class="card-header">
                    <h3 class="card-title">Cajillas Clasificadas</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                            <i class="fas fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="card-body">
                    <h4>Salidas de Cajillas Clasificadas del Dia</h4>
                    <span class="small text-muted">Consolidado del total de cajillas clasificadas por categoria</span>
                    <asp:Repeater ID="rp_totalClasificadoEnelDia" runat="server">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-responsive-sm">
                                <tr>
                                    <td><b>Tipo Huevo</b></td>
                                    <td><b>Clasificacion</b></td>
                                    <td><b>Total Cajillas</b></td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr>
                                <td>
                                    <b><%#Eval("TipoHuevo")%> </b>
                                </td>
                                <td>
                                    <%#Eval("Clasificacion")%>
                                </td>
                                <td>
                                    <%#Eval("TotalCajillas")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>   
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="card card-indigo">
                <div class="card-header">
                    <h3 class="card-title">Cajillas a Produccion</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                            <i class="fas fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="card-body">
                    <h4>Salidas de Cajillas a produccion del Dia</h4>
                    <span class="small text-muted">Todas las Cajillas que fueron Usadas para producir Bolsa de huevo entre otros.</span>
                    <asp:Repeater ID="rp_HuevoProduccion" runat="server">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-responsive-sm">
                                <tr>
                                    <td><b>Tipo Huevo</b></td>
                                    <td><b>Total Cajillas Usadas</b></td>
                                    <td><b>Producto</b></td>
                                    <td><b>Total Cantida Producida</b></td>
                                    <td><b>Medidad</b></td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <b><%#Eval("TipoHuevo")%> </b>
                                </td>
                                <td>
                                    <%#Eval("totalCajillasUsadas")%>
                                </td>
                                <td>
                                    <%#Eval("Clasificacion")%>
                                </td>
                                <td>
                                    <%#Eval("Total_CantidaProducida")%>
                                </td>
                                <td>
                                    <%#Eval("Medidad")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>   
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
