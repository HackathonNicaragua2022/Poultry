<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="inventariomob.aspx.cs" Inherits="POULTRY.mobilnavs.inventariomob" %>

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
                        <li class="breadcrumb-item"><a href="menuInventarios.aspx">Menu de Inventarios</a></li>
                        <li class="breadcrumb-item active">Inventario Huevo Sin clasificar</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up_principal" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="100000"></asp:Timer>
            <div class="callout callout-info" runat="server" id="ingresoProveedorPanel">
                <!-- /.col -->
                <div class="col-12">
                    <p class="lead">Resumen Total</p>

                    <div class="table-responsive">
                        <table class="table table-responsive-sm">
                            <asp:Repeater runat="server" ID="rp_Tabla" OnItemCommand="rp_inventariohsc_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <th style="width: 50%"><span class="info-box-text text-uppercase">
                                            <asp:LinkButton ID="btn_Detalles" CommandName="cmd_MostarDetalle" CommandArgument='<%#Eval("ID_TipoHUevo")%>' runat="server"><h5><%#Eval("TipoHuevo")%></h5></asp:LinkButton>
                                        </span>
                                        </th>
                                        <td>
                                            <h5><%#Eval("SALDO","{0:n}")%> &nbsp;Cajillas&nbsp;&nbsp;(<%#Eval("Porcentaje","{0:n}")%>%)</h5>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <th>
                                    <h5>TOTAL:</h5>
                                </th>
                                <td>
                                    <h5>
                                        <asp:Label Text="0" ID="lbl_TotalGeneral" runat="server" />
                                        &nbsp;Cajillas&nbsp;&nbsp;(100%)</h5>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <%--  <div class="row">
                <div class="col">
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
                                                <li class="nav-item">Total Entradas <span class="float-right badge bg-primary">
                                                    <h4><%#Eval("TOTALE")%> &nbsp;Cajillas</h4>
                                                </span>
                                                </li>
                                                <li class="nav-item">Total Salidas Clasificados <span class="float-right badge bg-info">
                                                    <h4><%#Eval("TS")%>&nbsp;Cajillas</h4>
                                                </span>
                                                </li>
                                                <li class="nav-item">Saldo Pendiente por Clasificar<span class="float-right badge bg-success"><h4><%#Eval("SALDO")%>&nbsp;Cajillas</h4>
                                                </span>
                                                </li>--%>
            <%--<li class="nav-item">Promedio Dias Produccion<span class="float-right badge bg-success"><%#Eval("PROMEDIO_DIAS")%>&nbsp;Dias</span>
                                        </li>--%>
            <%-- </ul>
                                            <asp:LinkButton ID="btn_Detalles" CommandName="cmd_MostarDetalle" CommandArgument='<%#Eval("ID_TipoHUevo")%>' CssClass="lead" runat="server">Click para ver las cantidades por produccion</asp:LinkButton>
                                        </div>--%>
            <!-- /.info-box-content -->
            <%--</div>--%>
            <!-- /.info-box -->
            <%--   </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div class="info-box">
                                        <span class="info-box-icon bg-gradient-green"><i class="ion ion-egg"></i></span>
                                        <div class="info-box-content">
                                            <span class="info-box-text text-uppercase"><b><%#Eval("TipoHuevo")%></b></span>
                                            <ul class="nav flex-column">
                                                <li class="nav-item">Total Entradas <span class="float-right badge bg-primary">
                                                    <h4><%#Eval("TOTALE")%>&nbsp;Cajillas</h4>
                                                </span>
                                                </li>
                                                <li class="nav-item">Total Salidas Clasificados <span class="float-right badge bg-info">
                                                    <h4><%#Eval("TS")%>&nbsp;Cajillas</h4>
                                                </span>
                                                </li>
                                                <li class="nav-item">Saldo Pendiente por Clasificar<span class="float-right badge bg-success"><h4><%#Eval("SALDO")%>&nbsp;Cajillas</h4>
                                                </span>
                                                </li>--%>
            <%--   <li class="nav-item">Promedio Dias Produccion<span class="float-right badge bg-success"><%#Eval("PROMEDIO_DIAS")%>&nbsp;Dias</span>
                                        </li>--%>
            <%--  </ul>
                                            <asp:LinkButton ID="btn_Detalles" CommandName="cmd_MostarDetalle" CommandArgument='<%#Eval("ID_TipoHUevo")%>' CssClass="lead" runat="server">Click para ver las cantidades por produccion</asp:LinkButton>
                                        </div>--%>
            <!-- /.info-box-content -->
            <%--</div>--%>
            <!-- /.info-box -->
            <%-- </AlternatingItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                </div>--%>

            </div>
            <div class="row">
                <div class="col">
                    <asp:Button Text="Mostrar Clasificaciones y Produccion del Dia" CssClass="btn bg-gradient-lightblue border-dark" OnClick="btn_Mostrar_Click" ID="btn_Mostrar" runat="server" />
                    <asp:Button Text="Ordenes del dia" CssClass="btn bg-gradient-green border-dark" PostBackUrl="~/mobilnavs/OrdenesIngreso.aspx" ID="btn_OrdenesDia" runat="server" />
                </div>
            </div>
            <br />
            <div id="Cards" runat="server" visible="false">
                <div class="row">
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
