<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="LotesBroilers.aspx.cs" Inherits="POULTRY.LOTES.LotesBroilers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .jumbotron-image {
            background-position: center center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .backCol {
            background-color: #82caff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid py-2">

        <%-------------------------------------------------------------------------------------%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="jumbotron text-white jumbotron-image shadow" style="background-image: url(../imagenes/broilers_tony.jpg)">
                    <h2 class="mb-4">Control de LOTES en Produccion
                    </h2>
                    <p class="mb-4">
                        Cree y administre los lotes en Produccion
                    </p>
                    <div class="float-right text-white">
                        <h1>Granja
                            <asp:DropDownList ID="dr_GranjaSeleccionada" OnSelectedIndexChanged="dr_GranjaSeleccionada_SelectedIndexChanged" CssClass="dropdown" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </h1>
                        <p class="lead">
                            Total Aves en Pie:<b><asp:Label ID="lbl_totalPolloEnPie" runat="server" Text=""></asp:Label></b>
                        </small>
                    </div>
                    <br />
                    <br />
                </div>
                <div class="row">
                    <asp:Repeater ID="R_Galeras" runat="server" OnItemCommand="R_Galeras_ItemCommand">
                        <ItemTemplate>
                            <section class="col-sm-12 col-md-4 col-lg-4">
                                <div class="card card-primary elevation-3">
                                    <div class="card-header">
                                        <h3 class="card-title">Galera en Produccion</h3>

                                        <div class="card-tools">
                                            <!-- This will cause the card to maximize when clicked -->
                                            <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                                            <!-- This will cause the card to collapse when clicked -->
                                            <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                            <!-- This will cause the card to be removed when clicked -->
                                            <%-- <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-times"></i></button>--%>
                                        </div>
                                        <!-- /.card-tools -->
                                    </div>
                                    <!-- /.card-header -->
                                    <div class="card-body">
                                        <%--<div class="card-icon card-icon-large">--%>
                                        <div class="align-content-center">
                                            <img class="elevation-1 img-rounded" src="../imagenes/polloEnGranja.png" width="100%" style="display: block; margin-left: auto; margin-right: auto;" />
                                            <br />
                                        </div>
                                        <h3 class="mb-0">NUMERO DE GALERA <strong><%#Eval("NumeroOrden")%></strong></h3>
                                        <p class="mb-1">Nombre de Galera <%#Eval("NombreApodo")%></p>
                                        <p class="mb-1">Capacidad Instalada:&nbsp; <strong><%#Eval("CapacidadInstalada","{0:n}")%> </strong></p>
                                        <p class="mb-1">Inventario Inicial:&nbsp; <strong><%#Eval("TotalIngreso","{0:n}")%> </strong></p>
                                        <p class="mb-1">Porcentaje de Uso de Galera:&nbsp; <strong><%#Eval("RendimientoGalera","{0:n}")%> %</strong></p>
                                        <p class="mb-1">Total Mortalidad:&nbsp; <strong><%#Eval("Mortalidad","{0:n}")%> Aves&nbsp;&nbsp; (<%#Eval("PorcenjateMortalidad","{0:n}")+" % "%>)</strong></p>
                                        <p class="mb-4">Pollo en Pie:&nbsp; <strong><%#Eval("PolloEnPie","{0:n}")%> Aves &nbsp;&nbsp; (<%#"Vivos:&nbsp;"+Eval("PromedioEnPie","{0:n}")+" % "%>)</strong></p>
                                        <h4 class="mb-2">VIABILIDAD DE LOTE ACTUAL <strong><%#Eval("PorcentajeViabilidadLote","{0:n}")%> %</strong></h4>
                                        <div class="row">
                                            <div class="col">
                                                <asp:Button Text="Actualizar Lote" runat="server" UseSubmitBehavior="false" CausesValidation="false" CommandName="LoteActivo" CommandArgument='<%#Eval("ID_Galeras")%>' CssClass="btn btn-primary form-control elevation-3" />
                                            </div>
                                            <div class="col">
                                                <asp:Button Text="Ver Lote" runat="server" UseSubmitBehavior="false" CausesValidation="false" CommandName="VerActivo" CommandArgument='<%#Eval("ID_Galeras")%>' CssClass="btn btn-success form-control elevation-3" />
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.card-body -->
                                </div>
                                <!-- /.card -->
                            </section>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <section class="col-sm-12 col-md-4 col-lg-4">
                                <div class="card card-blue elevation-3">
                                    <div class="card-header">
                                        <h3 class="card-title">Galera en Produccion</h3>

                                        <div class="card-tools">
                                            <!-- This will cause the card to maximize when clicked -->
                                            <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                                            <!-- This will cause the card to collapse when clicked -->
                                            <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                            <!-- This will cause the card to be removed when clicked -->
                                            <%-- <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-times"></i></button>--%>
                                        </div>
                                        <!-- /.card-tools -->
                                    </div>
                                    <!-- /.card-header -->
                                    <div class="card-body bg-gradient-dark">
                                        <%--<div class="card-icon card-icon-large">--%>
                                        <div class="align-content-center">
                                            <img class="elevation-1 img-rounded" src="../imagenes/polloEnGranja.png" width="100%" style="display: block; margin-left: auto; margin-right: auto;" />
                                            <br />
                                        </div>
                                        <h3 class="mb-0">NUMERO DE GALERA <strong><%#Eval("NumeroOrden")%></strong></h3>
                                        <p class="mb-1">Nombre de Galera <%#Eval("NombreApodo")%></p>
                                        <p class="mb-1">Capacidad Instalada:&nbsp; <strong><%#Eval("CapacidadInstalada","{0:n}")%> </strong></p>
                                        <p class="mb-1">Inventario Inicial:&nbsp; <strong><%#Eval("TotalIngreso","{0:n}")%> </strong></p>
                                        <p class="mb-1">Porcentaje de Uso de Galera:&nbsp; <strong><%#Eval("RendimientoGalera","{0:n}")%> %</strong></p>
                                        <p class="mb-1">Total Mortalidad:&nbsp; <strong><%#Eval("Mortalidad","{0:n}")%> Aves&nbsp;&nbsp; (<%#Eval("PorcenjateMortalidad","{0:n}")+" % "%>)</strong></p>
                                        <p class="mb-4">Pollo en Pie:&nbsp; <strong><%#Eval("PolloEnPie","{0:n}")%> Aves &nbsp;&nbsp; (<%#"Vivos:&nbsp;"+Eval("PromedioEnPie","{0:n}")+" % "%>)</strong></p>
                                        <h4 class="mb-2">VIABILIDAD DE LOTE ACTUAL <strong><%#Eval("PorcentajeViabilidadLote","{0:n}")%> %</strong></h4>
                                          <div class="row">
                                            <div class="col">
                                                <asp:Button Text="Actualizar Lote" runat="server" UseSubmitBehavior="false" CausesValidation="false" CommandName="LoteActivo" CommandArgument='<%#Eval("ID_Galeras")%>' CssClass="btn btn-primary form-control elevation-3" />
                                            </div>
                                            <div class="col">
                                                <asp:Button Text="Ver Lote" runat="server" UseSubmitBehavior="false" CausesValidation="false" CommandName="VerActivo" CommandArgument='<%#Eval("ID_Galeras")%>' CssClass="btn btn-success form-control elevation-3" />
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.card-body -->
                                </div>
                                <!-- /.card -->
                            </section>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </div>
                <div id="id_galerasSinProduccion" runat="server" visible="false">
                    <br />
                    <div class="info-box bg-light">
                        <div class="info-box-content">
                            <h2 class="text-primary">Esta Granja no tiene galeras en produccion aún!
                            </h2>
                            <asp:Image ImageUrl="~/imagenes/galera.svg" runat="server" Width="40%" />
                        </div>
                    </div>
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
