<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="ResumenPesoVivo.aspx.cs" Inherits="POULTRY.Admin_gerencial.ResumenPesoVivo" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Resumen Proceso del Dia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h3>Resumen Remisiones</h3>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Planta Procesadora</a></li>
                        <li class="breadcrumb-item active">Resumen Remisiones</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
        <div class="container">

            <div class="row">
                <div class="col-md-12">
                    <!-- Widget: user widget style 1 -->
                    <div class="card card-widget widget-user">
                        <!-- Add the bg color to the header using any of the bg-* classes -->
                        <div class="widget-user-header bg-info">
                            <h3 class="widget-user-username">Remision a Matadero</h3>
                            <h5 class="widget-user-desc">Se actualiza automaticamente</h5>
                        </div>
                        <div class="widget-user-image">
                            <img class="img-circle elevation-2" src="../imagenes/logop-full-b.png" alt="User Avatar" />
                        </div>
                        <div class="card-footer">

                            <div class="row">
                                <div class="col-md-6 col-sm-12">
                                    <label for="txt_fechamatanza">Fecha de Procesos</label>
                                    <div class="input-group date" id="datetimepicker1" data-target-input="nearest">
                                        <div class="input-group-append" data-target="#datetimepicker1" data-toggle="datetimepicker">
                                            <div class="input-group-text">Seleccionar Fecha&nbsp;<i class="fa fa-calendar"></i></div>
                                        </div>
                                        <asp:TextBox runat="server" ID="txt_FechaMatanza" name="txt_fechamatanza" type="text" aria-describedby="txt_fechamatanzaHelpBlock" class="form-control datetimepicker-input" data-target="#datetimepicker1" />
                                        <%-- AutoPostBack="true" OnTextChanged="txt_FechaMatanza_TextChanged" oninput="javascript:RefreshIt(this);"--%>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-12">
                                    <label for="btn_CargarDatos">Click para Cargar</label>
                                    <asp:Button Text="Cargar Procesos para la Fecha" runat="server" CssClass="btn btn-success form-control" ID="btn_CargarDatos" OnClick="btn_CargarDatos_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12 col-sm-12">
                                    <asp:Repeater ID="rp_ProcesosEn_elDia" runat="server" OnItemCommand="rp_ProcesosEn_elDia_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-bordered table-responsive-sm">
                                                <tr>
                                                    <td><b>Lote</b></td>
                                                    <td><b>Total Javas Recibidas</b></td>
                                                    <td><b>Edad(Dias)</b></td>
                                                    <td><b>Total Aves Sacrificadas</b></td>
                                                    <td><b>Total Aves por Remision</b></td>
                                                    <td><b>Total Libras Peso Vivo</b></td>
                                                    <td><b>Peso Promedio Por Aves</b></td>
                                                    <td><b>Seleccionar</b></td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <tr>
                                                <td>
                                                    <%#Eval("NumeroLote1")%>   
                                                </td>
                                                <td>
                                                    <%#Eval("TotalJavas")%>
                                                </td>
                                                <td>
                                                    <%#Eval("EdadDias")%>
                                                </td>
                                                <td>
                                                    <%#Eval("TotalAves")%>
                                                </td>
                                                <td>
                                                    <%#Eval("TotalAvesRemision")%>
                                                </td>
                                                <td>
                                                    <%#Eval("TotalLibrasPesadas")%>
                                                </td>
                                                <td>
                                                    <%#Eval("PesoPromedio")%>
                                                </td>
                                                <td>
                                                    <asp:Button Text="Seleccionar Proceso" runat="server" CssClass="btn btn-primary form-control" ID="btn_Seleccionar" CommandArgument='<%#Eval("IDCompra")%>' CommandName="cmd_ProcesoSeleccionado" /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>   
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <hr />

                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                <ProgressTemplate>
                                    <div class="d-flex align-items-center">
                                        <strong>Actualizando...</strong>
                                        <div class="spinner-border ml-auto" role="status" aria-hidden="true"></div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Timer ID="Timer1" Interval="10000" OnTick="Timer1_Tick" runat="server"></asp:Timer>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-6">
                                            <!-- About Me Box -->
                                            <div class="card card-primary">
                                                <div class="card-header">
                                                    <h3 class="card-title">Datos Generales</h3>
                                                </div>
                                                <!-- /.card-header -->
                                                <div class="card-body">
                                                    <ul class="list-group list-group-unbordered mb-3">
                                                        <li class="list-group-item">
                                                            <b>Fecha de Matanza</b> <a class="float-right">
                                                                <asp:Label Text="" ID="lbl_fechaMatanza" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Nombre Lote</b> <a class="float-right">
                                                                <asp:Label Text="" ID="lbl_lote" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Total Javas Recibidas</b> <a class="float-right">
                                                                <asp:Label Text="" ID="lbl_TotalJavas" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Edad (Dias)</b> <a class="float-right">
                                                                <asp:Label Text="" ID="lbl_EdadDias" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Total Aves Sacrificadas</b> <a class="float-right">
                                                                <asp:Label Text="" ID="lbl_TotalAves" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Total Aves por Remisión</b> <a class="float-right">
                                                                <asp:Label Text="" ID="lbl_totalAvesRemision" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Total Libras Peso Vivo</b> <a class="float-right">
                                                                <asp:Label Text="" ID="lbl_LibrasPesadas" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Peso Promedio x Ave</b> <a class="float-right">
                                                                <asp:Label Text="" ID="lbl_TotalPesoPromedio" runat="server" /></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <!-- /.card-body -->
                                            </div>
                                            <!-- /.card -->
                                        </div>
                                        <div class="col-md-6 col-sm-6">
                                            <div class="card">
                                                <div class="card-header bg-primary">
                                                    <h3 class="card-title">Pesos Recibidos</h3>
                                                </div>
                                                <!-- /.card-header -->
                                                <div class="card-body p-0">
                                                    <asp:GridView ID="gv_Remisiones" CssClass="table table-sm table-striped" runat="server" AutoGenerateColumns="false" EnableViewState="false">
                                                        <Columns>
                                                            <asp:BoundField DataFormatString="{0:n}" DataField="TotalAvesEnviadas" HeaderText="Cant.Pollo"></asp:BoundField>
                                                            <asp:BoundField DataFormatString="{0:n}" DataField="Total_javas" HeaderText="Nº Javas"></asp:BoundField>
                                                            <asp:BoundField DataFormatString="{0:n}" DataField="TotalLibrasxRemision" HeaderText="Cant.Libras"></asp:BoundField>
                                                            <asp:BoundField DataFormatString="{0:n}" DataField="PesoAcumulado" HeaderText="Peso Acumulado">
                                                                <ControlStyle Font-Bold="True"></ControlStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <!-- /.card-body -->
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <!-- /.widget-user -->
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'L',
                //minDate: new Date(),
                format: 'DD/MM/YYYY'
            });
        });
        <%--   function RefreshIt(selectObj) {
            __doPostBack('<%= Page.ClientID %>', selectObj.name);
        }--%>
    </script>
</asp:Content>
