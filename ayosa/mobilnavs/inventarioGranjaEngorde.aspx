<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="inventarioGranjaEngorde.aspx.cs" Inherits="POULTRY.mobilnavs.inventarioGranjaEngorde" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid py-2">

        <%-------------------------------------------------------------------------------------%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton Text="Regresar a Menu de Inventarios" PostBackUrl="~/mobilnavs/menuInventarios.aspx" runat="server" />
                <hr />
                <h1 class="text-info">Inventarios de Pollo en crecimiento
                </h1>
                <h3>Granja
                            <asp:DropDownList ID="dr_GranjaSeleccionada" OnSelectedIndexChanged="dr_GranjaSeleccionada_SelectedIndexChanged" CssClass="dropdown" AutoPostBack="true" runat="server"></asp:DropDownList>
                </h3>
                <small>Seleccione la Granja a Operar</small>
                <div class="row">
                    <asp:Repeater ID="R_Galeras" runat="server" OnItemCommand="R_Galeras_ItemCommand">
                        <ItemTemplate>
                            <section class="col-lg-3 connectedSortable">
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
                                        <small class="small">Nombre de Galera <%#Eval("NombreApodo")%></small>

                                        <p>Capacidad Instalada:&nbsp <strong><%#Eval("CapacidadInstalada","{0:n}")%> </strong></p>
                                        <div class="row align-items-center mb-2 d-flex">
                                            <div class="col-8">
                                                <small class="small">Rendimiento de Galera</small>
                                                <h2 class="d-flex align-items-center mb-0">
                                                    <%#Eval("TotalIngreso","{0:n}")%> Aves
                                                </h2>
                                            </div>
                                            <div class="col-4 text-right">
                                                <span>
                                                    <%#Eval("RendimientoGalera","{0:n}")%>
                                                    %<i class="fa fa-arrow-up"></i></span>
                                            </div>
                                        </div>
                                        <%--   <h5><%#"width:"+Eval("TotalIngreso")+"%;"%></h5>
                                        <h5><%#"width:"+Eval("CapacidadInstalada")+"%;"%></h5>
                                        <h5><%#"width:"+Eval("RendimientoGalera")+"%;"%></h5>--%>
                                        <div class="progress">
                                            <div class="progress-bar progress-bar-striped progress-bar-animated" style='<%#"width:"+Eval("RendimientoGalera")+"%;"%>'></div>
                                        </div>
                                        <br />
                                        <div class="row align-items-center mb-2 d-flex">
                                            <div class="col-8">
                                                <small class="small">Pollo en Pie</small>
                                                <h2 class="d-flex align-items-center mb-0">
                                                    <%#string.Format("{0:n}",Eval("PolloEnPie"))%> Aves
                                                </h2>
                                            </div>
                                            <div class="col-4 text-right">
                                                <span>
                                                    <%#string.Format("{0:n}",Eval("PromedioEnPie"))%>
                                                    % <i class="fa fa-arrow-up"></i></span>
                                            </div>
                                        </div>
                                        <div class="progress">
                                            <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" style='<%#"width:"+Eval("PromedioEnPie")+"%;"%>'></div>
                                        </div>
                                        <br />
                                        <asp:Button Text="Ver Inventario en Produccion" runat="server" UseSubmitBehavior="false" CausesValidation="false" CommandName="LoteActivo" CommandArgument='<%#Eval("ID_Galeras")%>' CssClass="btn btn-primary form-control elevation-3" />
                                    </div>
                                    <!-- /.card-body -->
                                </div>
                                <!-- /.card -->
                            </section>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <section class="col-lg-3 connectedSortable">
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
                                        <small class="small">Nombre de Galera <%#Eval("NombreApodo")%></small>

                                        <p>Capacidad Instalada:&nbsp <strong><%#Eval("CapacidadInstalada","{0:n}")%> </strong></p>
                                        <div class="row align-items-center mb-2 d-flex">
                                            <div class="col-8">
                                                <small class="small">Rendimiento de Galera</small>
                                                <h2 class="d-flex align-items-center mb-0">
                                                    <%#Eval("TotalIngreso","{0:n}")%> Aves
                                                </h2>
                                            </div>
                                            <div class="col-4 text-right">
                                                <span>
                                                    <%#Eval("RendimientoGalera","{0:n}")%>
                                                    % <i class="fa fa-arrow-up"></i></span>
                                            </div>
                                        </div>
                                        <%--   <h5><%#"width:"+Eval("TotalIngreso")+"%;"%></h5>
                                        <h5><%#"width:"+Eval("CapacidadInstalada")+"%;"%></h5>
                                        <h5><%#"width:"+Eval("RendimientoGalera")+"%;"%></h5>--%>
                                        <div class="progress">
                                            <div class="progress-bar progress-bar-striped progress-bar-animated bg-danger" style='<%#"width:"+Eval("RendimientoGalera")+"%;"%>'></div>
                                        </div>
                                        <br />
                                        <div class="row align-items-center mb-2 d-flex">
                                            <div class="col-8">
                                                <small class="small">Pollo en Pie</small>
                                                <h2 class="d-flex align-items-center mb-0">
                                                    <%#string.Format("{0:n}",Eval("PolloEnPie"))%> Aves
                                                </h2>
                                            </div>
                                            <div class="col-4 text-right">
                                                <span>
                                                    <%#string.Format("{0:n}",Eval("PromedioEnPie"))%>
                                                    % <i class="fa fa-arrow-up"></i></span>
                                            </div>
                                        </div>
                                        <div class="progress">
                                            <div class="progress-bar progress-bar-striped progress-bar-animated bg-warning" style='<%#"width:"+Eval("PromedioEnPie")+"%;"%>'></div>
                                        </div>
                                        <br />
                                        <asp:Button Text="Ver Inventario en Produccion" runat="server" UseSubmitBehavior="false" CausesValidation="false" CommandName="LoteActivo" CommandArgument='<%#Eval("ID_Galeras")%>' CssClass="btn btn-warning form-control elevation-3" />
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
