<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterControlHuevos.Master" AutoEventWireup="true" CodeBehind="DetalleProduccionxTipoHuevo.aspx.cs" Inherits="POULTRY.ModuloClasificadoraHuevos.DetalleProduccionxTipoHuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Detalle de Produccion por Tipo de Huevo</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Detalle de Produccion de Tipo de Huevo sin Clasificar</h5>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="InventarioHuevoSinClasificar.aspx">Inventario Huevo Sin clasificar</a></li>
                        <li class="breadcrumb-item active">Detalle de Produccion por Tipo de Huevo</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <div class="row">
        <div class="col">
            <asp:LinkButton Text="text" runat="server" PostBackUrl="~/ModuloClasificadoraHuevos/InventarioHuevoSinClasificar.aspx">
                <i class="fa fa-arrow-left" aria-hidden="true" > </i>
                <span>Regresar a Inventario de Huevo sin Clasificar</span>
            </asp:LinkButton>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col">
            <div class="card card-dark">
                <div class="card-header">
                    <h3 class="card-title">Producciones con saldo por tipo de huevo</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                            <i class="fas fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="card-body">
                    <h4>Producciones con saldo para Clasificar por tipo de Huevo</h4>
                    <span class="small text-muted">Total de producciones con saldo para clasificar por tipo</span>
                    <asp:Repeater ID="rp_DetallesIHSC" runat="server">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-responsive-sm">
                                <tr>
                                    <td><b>Tipo Huevo</b></td>
                                    <td><b>Total Entradas</b></td>
                                    <td><b>Total Salidas</b></td>
                                    <td><b>Total Saldo</b></td>
                                    <td><b>Fecha de Produccion</b></td>
                                    <td><b>Dias desde Produccion</b></td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr>
                                <td>
                                    <b><%#Eval("TipoHuevo")%> </b>
                                </td>
                                <td>
                                    <b><%#Eval("TOTALE")%> </b>
                                </td>
                                <td>
                                    <%#Eval("TS")%>
                                </td>
                                <td>
                                    <%#Eval("SALDO")%>
                                </td>
                                <td>
                                    <%#Eval("FechaProduccion", "{0:D}")%>
                                </td>
                                <td>
                                    <%#Eval("PROMEDIO_DIAS")%>
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
