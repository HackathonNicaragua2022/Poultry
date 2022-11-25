<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterControlHuevos.Master" AutoEventWireup="true" CodeBehind="procesarHuevoSinClasificar.aspx.cs" Inherits="POULTRY.ModuloClasificadoraHuevos.procesarHuevoSinClasificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Procesar Huevos sin clasificar</title>
    <!-- daterange picker -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/daterangepicker/daterangepicker.css" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
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
                    <h5>Procesar Huevo sin clasificar</h5>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homebodegahuevo.aspx">Menu</a></li>
                        <li class="breadcrumb-item active">Procesar Huevo Sin Clasificar</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <div class="row align-items-center justify-content-center">
        <div class="col-sm-12 lign-self-center">
            <!-- Profile Image -->
            <div class="card card-dark card-outline m-1">
                <div class="card-body box-profile">
                    <div class="text-center">
                        <i class="nav-icon fas fa-project-diagram fa-5x"></i>
                    </div>

                    <h3 class="profile-username text-center">Procesar Huevos sin Clasificar</h3>

                    <p class="text-muted text-center">Procesar cajilas de Huevos en un nuevo productos</p>
                    <hr />
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="up_principal" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card card-dark">
                                        <div class="card-header">
                                            <h3 class="card-title">Datos para Procesar las Cajillas de Huevos</h3>
                                            <div class="card-tools">
                                                <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                                    <i class="fas fa-minus"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <p>Indique la fecha de proceso del huevo y seleccione la Clasificacion generada</p>
                                            <div class="row">
                                                <div class="col">
                                                    <label for="date">Fecha Produccion</label>
                                                    <div class="input-group date" data-provide="datepicker">
                                                        <asp:TextBox runat="server" class="form-control" ID="date" name="date" placeholder="" type="text" TextMode="Date" aria-describedby="txt_date_HelpBlock" />
                                                        <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-th"></span>
                                                        </div>
                                                    </div>
                                                    <span id="txt_date_HelpBlock" class="form-text text-muted">Fecha de produccion de cada tipo de huevo a ingresar</span>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group">
                                                        <label for="dr_Clasificacion">Tipo de Produccion</label>
                                                        <asp:DropDownList ID="dr_Clasificacion" CssClass="form-control custom-select" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <div class="form-group">
                                                        <label for="txt_Medidas">Medida</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="50" ID="txt_Medidas" aria-describedby="txt_medida_HelpBlock" />
                                                        <span id="txt_medida_HelpBlock" class="form-text text-muted">Especifique la medida usada para el producto terminado (Ejemplo: Cajilla, Bolsa, Caja, etc)</span>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group">
                                                        <label for="txt_Observaciones">Observaciones</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="250" ID="txt_Observaciones" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <!-- /.col -->
                                                <div class="col-md-6 col-lg-4 col-sm-12">
                                                    <p class="lead">Totales</p>
                                                    <div class="table-responsive">
                                                        <table class="table">
                                                            <tr>
                                                                <th style="width: 50%">Total Cajillas Usadas</th>
                                                                <td>
                                                                    <asp:Label Text="0" ID="lbl_totalCajillasUsadas" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <th>Total Cantidad Producida</th>
                                                                <td>
                                                                    <asp:Label Text="0" ID="lbl_TotalCantidaProducida" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <!-- /.col -->
                                            </div>
                                            <hr />
                                            <div class="col-md-12">
                                                <div class="card card-secondary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Huevo en Inventario Sin clasificar</h3>

                                                        <div class="card-tools">
                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                                                <i class="fas fa-minus"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-md-6 col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label for="dr_TipoHuevoInventarioSC">Tipo de Huevo</label>
                                                                    <asp:DropDownList ID="dr_TipoHuevoInventarioSC" CssClass="form-control custom-select" runat="server" aria-describedby="text1HelpBlock" OnSelectedIndexChanged="dr_TipoHuevoInventarioSC_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    <span id="text1HelpBlock" class="form-text text-muted">Se muestran tipos de Huevos con existencia en inventario por clasificar</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <asp:Repeater ID="rp_DetallesIHSC" runat="server" OnItemCommand="rp_DetallesIHSC_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table id="tablaDetalles" class="table table-striped table-bordered table-responsive-sm table-hover" data-show-toggle="false" data-expand-first="true">
                                                                            <tr>
                                                                                <td><b>Tipo Huevo</b></td>
                                                                                <td><b>Total Entradas</b></td>
                                                                                <td><b>Total Salidas</b></td>
                                                                                <td><b>Total Saldo</b></td>
                                                                                <td><b>Fecha de Produccion</b></td>
                                                                                <td data-breakpoints="all"><b>Dias desde Produccion</b></td>
                                                                                <td data-breakpoints="xs"><b>Seleccionar</b></td>
                                                                            </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>

                                                                        <tr class='<%#(bool)Eval("Seleccionado")==true?"bg-gradient-green":""%>'>
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
                                                                            <td>
                                                                                <asp:LinkButton ID="btn_Seleccionar" CommandName="cmd_Seleccionar" CommandArgument='<%#Eval("ID_HuevoSinClasificar")%>' CssClass='<%#(bool)Eval("Seleccionado")==true?"btn btn-primary":"btn btn-success"%>' runat="server">
                                                                        <i class="fa fa-solid fa-check-square"></i>
                                                                        '<%#(bool)Eval("Seleccionado")==true?"En Uso":"Seleccionar"%>'</asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>   
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>

                                                        <hr />
                                                        <div class="row">
                                                            <!-- /.col -->
                                                            <div class="col-md-6 col-lg-4 col-sm-12">
                                                                <p class="lead">Produccion seleccionada</p>

                                                                <div class="table-responsive">
                                                                    <table class="table">
                                                                        <tr>
                                                                            <th style="width: 50%">Fecha Produccion:</th>
                                                                            <td>
                                                                                <asp:Label Text="0" ID="lbl_FechaProduccion" runat="server" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>Edad Huevo</th>
                                                                            <td>
                                                                                <asp:Label Text="0" ID="lbl_EdadDias" runat="server" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>Total Entradas</th>
                                                                            <td>
                                                                                <asp:Label Text="0" ID="lbl_TotalEntradas" runat="server" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>Total Salidas</th>
                                                                            <td>
                                                                                <asp:Label Text="0" ID="lbl_TotalSalidas" runat="server" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>Saldo disponible para clasificar</th>
                                                                            <td>
                                                                                <asp:Label Text="0" ID="lbl_SaltoTotal" runat="server" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <!-- /.col -->
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col">
                                                                <div class="form-group bg-info p-2">
                                                                    <label for="txt_TotalCajillasUsadas">Total Cajillas Usadas</label>
                                                                    <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" TextMode="Number" ID="txt_TotalCajillasUsadas" />
                                                                </div>
                                                            </div>
                                                            <div class="col">
                                                                <div class="form-group bg-gradient-blue p-2">
                                                                    <label for="txt_CantidadCajillasRecibidas">Cantidad Producto Terminado Generado</label>
                                                                    <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" TextMode="Number" ID="txt_CantidadProductoTerminado" />
                                                                </div>
                                                            </div>
                                                            <div class="col">
                                                                <div class="form-group bg-gradient-dark p-2">
                                                                    <label for="txt_ObservacionesDetalle">Observaciones para el Tipo de huevo seleccionado</label>
                                                                    <asp:TextBox runat="server" CssClass="form-control" MaxLength="150" ID="txt_ObservacionesDetalle" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="alert alert-" role="alert" runat="server" id="alerta_flotante" visible="false">
                                                            <asp:Label Text="Verifique los campos" ID="lbl_NuevoMensaje" runat="server" />
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Button Text="Agregar Huevo Procesado" CssClass="btn btn-lg btn-primary" UseSubmitBehavior="false" CausesValidation="false" runat="server" ID="btn_IngresarTotalCajillas" OnClick="btn_IngresarTotalCajillas_Click" />
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12">
                                                                <asp:Repeater ID="rp_detalles" runat="server" OnItemCommand="rp_detalles_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-striped table-bordered table-responsive-sm">
                                                                            <tr>
                                                                                <td><b>Fecha Produccion</b></td>
                                                                                <td><b>Huevo</b></td>
                                                                                <td><b>Cantidad Cajillas Usadas</b></td>
                                                                                <td><b>Cantidad Producida</b></td>
                                                                                <td><b>Observaciones</b></td>
                                                                                <td><b>Remover Elemento</b></td>
                                                                            </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%#Eval("FechaProduccion")%>   
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("TipoHuevo")%>   
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("CantidadCajillasUsadas")%>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("CantidadProducida")%>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("Observaciones")%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button Text="Remover" runat="server" CssClass="btn btn-primary form-control" ID="btn_Seleccionar" CommandArgument='<%#Eval("ID_DetalleHuevoProcesado")%>' CommandName="cmd_remover" />
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>   
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up_principal" runat="server">
                                                            <ProgressTemplate>
                                                                <div class="d-flex align-items-center">
                                                                    <strong>Guardando.....</strong>
                                                                    <p>Espere porfavor</p>
                                                                    <div class="spinner-border ml-auto" role="status" aria-hidden="true"></div>
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <asp:LinkButton ID="btn_NuevoHuevoProcesado" runat="server" class="btn btn-lg btn-success" OnClick="btn_NuevoHuevoProcesado_Click">
                                                    <i class="fas fa-save"></i>&nbsp GUARDAR SALIDA HUEVO CLASIFICADO
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/footable.min.js"></script>
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <script>
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
    </script>
</asp:Content>
