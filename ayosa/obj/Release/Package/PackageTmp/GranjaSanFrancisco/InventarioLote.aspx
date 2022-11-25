<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="InventarioLote.aspx.cs" Inherits="POULTRY.LOTES.InventarioLote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inventario Lotes</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <!-- IonIcons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <style>
        .jumbotron-image {
            background-position: center center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .dropdown-menu {
            background-color: #424240;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid py-3">
        <div class="jumbotron jumbotron-image shadow" style="background-image: url(../imagenes/Galera.jpg)">
            <div class="row">
                <div class="col text-white">
                    <h2 class="mb-4">LOTES EN PRODUCCION GALERA NUMERO:&nbsp&nbsp<b><asp:Label Text="" ID="lbl_NUmeroGalera" runat="server" /></b></h2>
                    <p class="mb-4">
                        Control de la produccion de Aves en Crecimiento de la Galera
                    </p>
                </div>
                <div class="col">
                    <div class="float-right">
                        <div class="callout callout-info">
                            <i class="fa fa-building"></i>
                            <h2 class="text-info">Datos Galera</h2>
                            <h5>Capacidad Instalada:
                    <asp:Label Text="" ID="lbl_capacidadInstalada" runat="server" />
                            </h5>
                            <p class="lead">
                                Capacidad Normal&nbsp&nbsp<asp:Label Text="" ID="lbl_CapacidadNormal" runat="server" />
                                el
                        <asp:Label Text="" ID="lblPorcentajeUso" runat="server" />% de la Capacidad
                            </p>
                            <%------------------------------------------------%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%------------------------------------------------%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up_principal" runat="server">
            <ContentTemplate>
              

                <div class="row">
                    <div class="col-md-6">
                        <!-- Profile Image -->
                        <div class="card card-primary card-outline elevation-1">
                            <div class="card-body box-profile">
                                <div class="text-center">
                                    <img class="profile-user-img img-fluid img-circle"
                                        src="../imagenes/logop-full-b.png"
                                        alt="User profile picture" />
                                </div>
                                <h3 class="profile-username text-center">Inventario Pollo en Produccion</h3>
                                <ul class="list-group list-group-unbordered mb-3">
                                    <li class="list-group-item">
                                        <b>Total Pollo Ingresado</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_totalPolloIngresado" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Total Mortalidad Registrada</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_TotalMortalidad" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Total Remisiones</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_TotalRemisiones" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Total Pollo En Pie</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_TotalPolloEnPie" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Porcentaje Mortalidad</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_PorcentajeMortalidad" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Edad Dias</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_EdadDias" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Edad Semanas</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_EdadSemanadas" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Peso Promedio Lote</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_PesoPromedio" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Total Libras Peso Vivo Matadero</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_LibrasPesoMatadero" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Fecha Ingreso a Galera</b> <a class="float-right">
                                            <asp:Label Text="" ID="lbl_FechaIngresoAGalera" runat="server" /></a>
                                    </li>
                                </ul>

                                <div class="card elevation-3 border-info border-dark">
                                    <div class="card-header bg-gradient-dark">
                                        <h2
                                            class="text-white"><i class="fas fa-box"></i>&nbsp;Control de Consumo de Alimentos del Lote</h2>
                                    </div>
                                    <div class="card-body">
                                        <asp:Repeater ID="rp_ControlAlimentos" runat="server" OnItemDataBound="rp_ControlAlimentos_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="d-flex justify-content-between align-items-center border-bottom mb-3">
                                                    <p class="text-success text-xl text-uppercase">
                                                        <%#Eval("NombreAlimento")%>
                                                    </p>
                                                    <p class="d-flex flex-column text-right">
                                                        <span class="font-weight-bold text-xl">
                                                            <i class="fas fa-book text-info"></i>&nbsp;&nbsp;
                                                            <%#Eval("TotalQtlDiario")%>
                                                        </span>
                                                        <span class="text-muted">Total Qtl Recibidos</span>
                                                    </p>
                                                    <p class="d-flex flex-column text-right">
                                                        <span class="font-weight-bold text-xl">
                                                            <i class="fas fa-book text-warning"></i>&nbsp;&nbsp;
                                                            <%#Eval("Consumo_Diario")%>
                                                        </span>
                                                        <span class="text-muted">Total Consumo</span>
                                                    </p>
                                                </div>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <div class="d-flex justify-content-between align-items-center border-bottom mb-3">
                                                    <p class="text-info text-xl text-uppercase">
                                                        <%#Eval("NombreAlimento")%>
                                                    </p>
                                                    <p class="d-flex flex-column text-right">
                                                        <span class="font-weight-bold text-xl">
                                                            <i class="fas fa-book text-success"></i>&nbsp;&nbsp;
                                                            <%#Eval("TotalQtlDiario")%>
                                                        </span>
                                                        <span class="text-muted">Total Qtl Recibidos</span>
                                                    </p>
                                                    <p class="d-flex flex-column text-right">
                                                        <span class="font-weight-bold text-xl">
                                                            <i class="fas fa-book text-primary"></i>&nbsp;&nbsp;
                                                            <%#Eval("Consumo_Diario")%>
                                                        </span>
                                                        <span class="text-muted">Total Consumo</span>
                                                    </p>
                                                </div>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                <!-- small box -->
                                                <div class="small-box bg-white">
                                                    <div class="inner row">
                                                        <div class="col">
                                                            <div class="inner">
                                                                <h3>
                                                                    <asp:Label Text="" ID="lbl_TotalRecibido" runat="server" /></h3>
                                                                <p>Total Qtl Recibidos</p>
                                                            </div>
                                                        </div>
                                                        <div class="col">
                                                            <div class="inner">
                                                                <h3>
                                                                    <asp:Label Text="" ID="lbl_TotalConsumido" runat="server" /></h3>
                                                                <p>Total Qtl Consumidos</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="ion ion-pie-graph"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer text-black">TOTAL DE ALIMENTOS CARGADO AL LOTE</a>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                            <!-- /.card-body -->
                        </div>
                        <!-- /.card -->
                    </div>
                    <div class="col-md-6">
                        <div class="card card-primary elevation-1 collapsed-card">
                            <div class="card-header">
                                <h3 class="card-title">Mortalidad Registrada</h3>

                                <div class="card-tools">
                                    <!-- This will cause the card to maximize when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                                    <!-- This will cause the card to collapse when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button>
                                    <!-- This will cause the card to be removed when clicked -->
                                    <%-- <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-times"></i></button>--%>
                                </div>
                                <!-- /.card-tools -->
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <asp:GridView ID="gv_Parametros" DataKeyNames="ID_ParametrosDiarios" CssClass="table table-bordered table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" OnRowCommand="gv_Parametros_RowCommand1" EmptyDataText="No se han Registrado Parametros para el Lote en produccion!">
                                    <Columns>
                                        <asp:TemplateField HeaderText="N°">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Mortalidad" HeaderText="Mortalidad" DataFormatString="{0:0} Aves">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Peso_Promedio" HeaderText="Peso(gr)" DataFormatString="{0:n} gr">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoPromedioLibras" HeaderText="Peso(lbs)" DataFormatString="{0:n} lbs">

                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Uniformidad" HeaderText="Uniformidad" DataFormatString="{0:n} Aves">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombreRegistrado" HeaderText="Registrado Por">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ComentariosAdicionales" HeaderText="Comentarios">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Anular Registro">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Actualizar" data-toggle="tooltip" title="Actualizar Peso Promedio" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_actualizar" CssClass="btn btn-warning" UseSubmitBehavior="false">
                                                     <i class="fa fa-circle-o-notch text-black"></i>&nbsp;Actualizar Peso Promedio
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anular Registro">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Anular" data-toggle="tooltip" title="Anula el Registro, regresando la Cantidad de avez registradas como muertas, a Ecepcion del peso promedio" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_anular" CssClass="btn btn-danger" UseSubmitBehavior="false">
                                                     <i class="fa fa-trash text-white"></i> Eliminar Registro
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="card card-danger elevation-1  collapsed-card">
                            <div class="card-header">
                                <h3 class="card-title">Consumo de Alimentos</h3>

                                <div class="card-tools">
                                    <!-- This will cause the card to maximize when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                                    <!-- This will cause the card to collapse when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button>
                                    <!-- This will cause the card to be removed when clicked -->
                                    <%-- <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-times"></i></button>--%>
                                </div>
                                <!-- /.card-tools -->
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <asp:GridView ID="gv_ConsumoAlimentos" DataKeyNames="Id_ControlAlimenticio" CssClass="table table-bordered table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" OnRowCommand="gv_ConsumoAlimentos_RowCommand" EmptyDataText="No se ha registrado consumo de alimentos para este Lote en produccion">
                                    <Columns>
                                        <asp:TemplateField HeaderText="N°">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NombreTipoAlimentos" HeaderText="Alimento">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro Sistema" DataFormatString="{0:D}">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InventarioInicialQtl" HeaderText="Inventario Inicial Qtl" DataFormatString="{0:n} Qtl">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InventarioFinal" HeaderText="Inventario Final Qtl" DataFormatString="{0:n} Qtl">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ConsumoDiario" HeaderText="Consumo Diario Qtl" DataFormatString="{0:n} Qtl">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Comentarios" HeaderText="Comentarios">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RegistradoPorUsuario" HeaderText="Registrado Por">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="FechaHoraRegistroConsumo_in" HeaderText="FechaRegistroCI">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaHoraRegistroConsumo_fin" HeaderText="FechaRegistroCI">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QtlRecibidoDiario" HeaderText="Qtl Recibido Diario" DataFormatString="{0:n} Qtl">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Anular Registro">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Anular" data-toggle="tooltip" title="Anula el Registro del consumo de alimentos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_anular" CssClass="btn btn-danger" UseSubmitBehavior="false">
                                                     <i class="fa fa-trash text-white"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="card card-success elevation-1  collapsed-card">
                            <div class="card-header">
                                <h3 class="card-title">Insumos Generales</h3>

                                <div class="card-tools">
                                    <!-- This will cause the card to maximize when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                                    <!-- This will cause the card to collapse when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button>
                                    <!-- This will cause the card to be removed when clicked -->
                                    <%-- <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-times"></i></button>--%>
                                </div>
                                <!-- /.card-tools -->
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <asp:GridView ID="gv_insumosGenerales" DataKeyNames="ID_RegistroInsumoGranja" CssClass="table table-bordered table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" OnRowCommand="gv_insumosGenerales_RowCommand" EmptyDataText="No hay insumos generales registrado aún!!">
                                    <Columns>
                                        <asp:TemplateField HeaderText="N°">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NombreInsumo" HeaderText="Insumo">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CostoUnid" HeaderText="Costo Unidad" DataFormatString="{0:C}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" DataFormatString="{0:C}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Registrado_Por" HeaderText="Registrado Por">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Anular Registro">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Anular" data-toggle="tooltip" title="Anula el Registro del insumo usado en el lote" runat="server" OnClientClick="return sweetAlertConfirm(this);" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_anular" CssClass="btn btn-danger" UseSubmitBehavior="false">
                                                     <i class="fa fa-trash text-white"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="card card-warning elevation-1  collapsed-card">
                            <div class="card-header">
                                <h3 class="card-title">Aplicacion de Medicamentos Generales</h3>

                                <div class="card-tools">
                                    <!-- This will cause the card to maximize when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                                    <!-- This will cause the card to collapse when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button>
                                    <!-- This will cause the card to be removed when clicked -->
                                    <%-- <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-times"></i></button>--%>
                                </div>
                                <!-- /.card-tools -->
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <asp:GridView ID="gv_Medicamentos" DataKeyNames="ID_AplicacionesMedicas" CssClass="table table-bordered table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" OnRowCommand="gv_Medicamentos_RowCommand" EmptyDataText="No hay Medicamentos generales registrado aún!!">
                                    <Columns>
                                        <asp:TemplateField HeaderText="N°">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NombreInsumo" HeaderText="Insumo">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Frascos" HeaderText="Numero de Frascos" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DosisxFrasco" HeaderText="Dosis x Frasco" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalDosis" HeaderText="Total Dosis" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CostoxFrasco" HeaderText="Costo x Frasco" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Registrado_por" HeaderText="Registrado Por">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaRegistroSistema" HeaderText="Fecha Registro">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Anular Registro">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Anular" data-toggle="tooltip" title="Anula el Registro del medicamento usado en el lote" runat="server" OnClientClick="return sweetAlertConfirm(this);" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_anular" CssClass="btn btn-danger" UseSubmitBehavior="false">
                                                     <i class="fa fa-trash text-white"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="card card-fuchsia elevation-1  collapsed-card">
                            <div class="card-header">
                                <h3 class="card-title">Aplicacion de Tratamientos Medicos</h3>

                                <div class="card-tools">
                                    <!-- This will cause the card to maximize when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                                    <!-- This will cause the card to collapse when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button>
                                    <!-- This will cause the card to be removed when clicked -->
                                    <%-- <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-times"></i></button>--%>
                                </div>
                                <!-- /.card-tools -->
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <asp:GridView ID="gv_Tratamientos" DataKeyNames="ID_AplicacionesTratamientos" CssClass="table table-bordered table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" OnRowCommand="gv_Tratamientos_RowCommand" EmptyDataText="No hay Tratamientos Medicamentos registrado aún!!">
                                    <Columns>
                                        <asp:TemplateField HeaderText="N°">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NombreInsumo" HeaderText="Insumo">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="FechaRegistroSistema" HeaderText="Fecha Registro Sistema">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaInicioDosis" HeaderText="Fecha Inicio Dosis">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaFinDosis" HeaderText="Fecha Fin Dosis">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="TotalDiasDosis" HeaderText="Total Dias Dosis" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Aplicacion" HeaderText="Concepto">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CostoMinimo" HeaderText="Costo" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="NombreRegistradoPor" HeaderText="Registrado Por">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Anular Registro">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Anular" data-toggle="tooltip" title="Anula el Registro del Tratamiento medico usado en el lote" runat="server" OnClientClick="return sweetAlertConfirm(this);" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_anular" CssClass="btn btn-danger" UseSubmitBehavior="false">
                                                     <i class="fa fa-trash text-white"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="card card-indigo elevation-1  collapsed-card">
                            <div class="card-header">
                                <h3 class="card-title">Insumos Promediados</h3>

                                <div class="card-tools">
                                    <!-- This will cause the card to maximize when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                                    <!-- This will cause the card to collapse when clicked -->
                                    <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button>
                                    <!-- This will cause the card to be removed when clicked -->
                                    <%-- <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-times"></i></button>--%>
                                </div>
                                <!-- /.card-tools -->
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                            </div>
                        </div>
                    </div>
                </div>
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
                                    <h2 class="text-primary">¿Esta Completamente seguro que deseha eliminar el Registro de Parametros?, la mortalidad se anulara de lote en produccion, pero el peso promedio no se actualizara!!</h2>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12">
                                            <label for="btn_Continuar"></label>

                                            <%--Elimina el fondo negro que queda al cerrar el control--%>
                                            <%--data-dismiss="modal" data-backdrop="false"--%>
                                            <asp:Button ID="btn_Continuar" runat="server" Text="Si!, Continuar" CssClass="btn btn-danger form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />
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
                <%--=========================================================================================================================================--%>
                <div class="modal fade" id="modalAdvertencia_CA" tabindex="-1" role="dialog" aria-labelledby="labeAdvertencia">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="labeAdvertenciaCA">Atencion!!</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <div class="text-center">
                                    <img id="Img2" src="~/imagenes/warning.png" alt="Ok" class="img-responsive center-block" runat="server" /><hr />
                                    <h2 class="text-primary">¿Esta Completamente seguro que deseha eliminar el Registro de Alimentos?</h2>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12">
                                            <label for="btn_Continuar"></label>

                                            <%--Elimina el fondo negro que queda al cerrar el control--%>
                                            <%--data-dismiss="modal" data-backdrop="false"--%>

                                            <asp:Button ID="btn_ControlAlimento" runat="server" Text="Si!, Continuar" CssClass="btn btn-danger form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_ControlAlimento_Click" UseSubmitBehavior="false" />

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
                <%--=========================================================================================================================================--%>
                <div class="modal fade" id="modalpesosProm" tabindex="-1" role="dialog" aria-labelledby="labepesoProm">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="labepesoProm">Actualizacion de Peso Promedio!!</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <h2 class="text-primary">MODIFICACION DE PESOPROMEDIO</h2>
                                <hr />
                                <div class="form-group row">
                                    <label for="txt_pesoPromedioUp" class="col-4 col-form-label">Peso Promedio</label>
                                    <div class="col-8">
                                        <asp:TextBox runat="server" ID="txt_pesoPromedioUp" TextMode="Number" name="txt_pesoPromedioUp" placeholder="0g" type="text" class="form-control" aria-describedby="txt_pesoPromedioUpHelpBlock" />
                                        <span id="txt_pesoPromedioUpHelpBlock" class="form-text text-muted">Peso en gramos</span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="offset-4 col-8">
                                        <asp:CheckBox ID="chk_actualizarLote" Checked="true" Text="&nbsp;Actualizar Peso promedio del Lote" CssClass="checkbox" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="offset-4 col-8">
                                        <asp:Button Text="ACTUALIZAR PESO PROMEDIO" runat="server" UseSubmitBehavior="false" CausesValidation="false" data-dismiss="modal" data-backdrop="false" class="btn btn-primary elevation-1 border-dark" ID="btn_ActualizarPesoProm" OnClick="btn_ActualizarPesoProm_Click" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script>


        function sweetAlertConfirm(btnDelete) {
            if (btnDelete.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnDelete.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                Swal.fire({
                    icon: 'warning',
                    title: '¿Esta completamente seguro que desea eliminar este elemento?',
                    text: "No podra deshacer los cambios una vez eliminado!",
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si, Eliminalo !',
                }).then(function (result) {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        //    // Set data-confirmed attribute to indicate that the action was confirmed
                        btnDelete.dataset.confirmed = true;
                        //    // Trigger button click programmatically
                        btnDelete.click();
                    } else {
                        Swal.fire('Cancelado', 'No se eliminara ningun elemento', 'info')
                    }

                    //.then(function () {
                    //    // Set data-confirmed attribute to indicate that the action was confirmed
                    //    btnDelete.dataset.confirmed = true;
                    //    // Trigger button click programmatically
                    //    btnDelete.click();
                    //}).catch(function (reason) {
                    //    // The action was canceled by the user                   
                    //});
                });
            }
        }


        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $(<%=gv_Parametros.ClientID%>).footable();
                $(<%=gv_ConsumoAlimentos.ClientID%>).footable();
                $(<%=gv_insumosGenerales.ClientID%>).footable();
                $(<%=gv_Medicamentos.ClientID%>).footable();
                $(<%=gv_Tratamientos.ClientID%>).footable();
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(<%=gv_Parametros.ClientID%>).footable();
                    $(<%=gv_ConsumoAlimentos.ClientID%>).footable();
                    $(<%=gv_insumosGenerales.ClientID%>).footable();
                    $(<%=gv_Medicamentos.ClientID%>).footable();
                    $(<%=gv_Tratamientos.ClientID%>).footable();
                }
            });
        };
        //ALERTA DE EXITO
        function success_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'success',
                    title: mensaje
                });
            });
        };
        //ALERTA DE ERROR
        function error_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'error',
                    title: mensaje
                });
            });
        };
        //ALERTA DE INFORMACION
        function info_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'info',
                    title: mensaje
                });
            });
        };
        //ALERTA DE ADVERTENCIA
        function advertencia_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'warning',
                    title: mensaje
                });
            });
        };
        //ALERTA DE PREGUNTA
        function pregunta_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000
                });
                Toast.fire({
                    icon: 'question',
                    title: mensaje
                });
            });
        };
        //ALERTA DE PREGUNTA
        function modal_alert(titulo, mensaje, tipo) {
            $(function () {
                Swal.fire(titulo, mensaje, tipo);
            });
        };
        function ShowModalAdvertencia() {
            $('#modalAdvertencia').modal("show");
        }
        function ShowModalAdvertenciaCA() {
            $('#modalAdvertencia_CA').modal("show");
        }
        function ShowModal_modalpesosProm() {
            $('#modalpesosProm').modal("show");
        }
    </script>
</asp:Content>
