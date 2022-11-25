<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterLogistica.Master" AutoEventWireup="true" CodeBehind="ingresoCombustible.aspx.cs" Inherits="POULTRY.ModuloLogistica.ingresoCombustible" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ingreso de Combustible</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <style>
        .img-responsive {
            display: block;
            margin: auto;
        }
    </style>
    <style>
        .table-bordered th, .table-bordered td {
            border: 1px solid #525252 !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h2>Ingreso de Combustible</h2>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homeLogistica.aspx">Home Logistica</a></li>
                        <li class="breadcrumb-item active">Ingreso de Combustible</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <div class="row align-items-center justify-content-center">
        <div class="col-sm-12 lign-self-center">
            <!-- Profile Image -->
            <asp:ScriptManager ID="sm_principal" runat="server"></asp:ScriptManager>
            <asp:UpdateProgress ID="upr_principal" AssociatedUpdatePanelID="up_principal" runat="server">
                <ProgressTemplate>
                    <div class="spinner-grow text-primary" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                    <div class="spinner-grow text-secondary" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                    <div class="spinner-grow text-success" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                    <div class="spinner-grow text-danger" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                    <div class="spinner-grow text-warning" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                    <div class="spinner-grow text-info" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="up_principal" runat="server">
                <ContentTemplate>
                    <div class="card card-success card-outline m-1">
                        <div class="card-body box-profile">
                            <div class="text-left">
                                <h2><i class="nav-icon fas fa-gas-pump" aria-hidden="true"></i></h2>
                            </div>
                            <h3 class="profile-username text-left">Ingreso de Combustible</h3>
                            <p class="text-muted text-left">Registre facturas de Combustible en el sistema</p>
                            <hr />
                            <div class="callout callout-info">
                                <h4>Ingreso de Nueva Factura de Combustible</h4>
                                <p class="lead">Complete los siguientes campos para agregar el proveedor</p>
                                <div class="form-group row">
                                    <label for="txt_fechaFactura">Fecha Factura</label>
                                    <div class="input-group date" data-provide="datepicker">
                                        <asp:TextBox runat="server" class="form-control" ID="txt_fechaFactura" name="date" placeholder="" type="text" TextMode="Date" aria-describedby="txt_date_HelpBlock" />
                                        <div class="input-group-addon">
                                            <span class="glyphicon glyphicon-th"></span>
                                        </div>
                                    </div>
                                    <span id="txt_date_HelpBlock" class="form-text text-muted">Fecha según factura</span>
                                </div>
                                <div class="form-group row">
                                    <label for="txt_NumeroFactura">Número de Factura</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <i class="fa fa-book"></i>
                                            </div>
                                        </div>
                                        <asp:TextBox runat="server" ID="txt_NumeroFactura" name="txt_NumeroFactura" placeholder="123" MaxLength="10" type="text" aria-describedby="txt_NumeroFactrura_HelpBlock" class="form-control" />
                                    </div>
                                    <span id="txt_NumeroFactrura_HelpBlock" class="form-text text-muted">Factura del Combustible ingresado</span>
                                </div>
                                <div class="form-group row">
                                    <label for="dr_ProvedorCombustible">Proveedor de Combustible</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <i class="fa fa-user"></i>
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="dr_ProvedorCombustible" CssClass="form-control custom-select" runat="server" aria-describedby="dr_ProvedorCombustibleHelpBlock"></asp:DropDownList>
                                    </div>
                                    <span id="dr_ProvedorCombustibleHelpBlock" class="form-text text-muted">Proveedor de Combustible</span>
                                </div>
                                <div class="form-group row">
                                    <label for="txt_observaciones">Observaciones</label>
                                    <asp:TextBox runat="server" class="form-control" MaxLength="250" TextMode="MultiLine" ID="txt_observaciones" />
                                </div>
                            </div>
                            <div class="callout callout-success">
                                <h4>Detalle de la Factura</h4>
                                <p class="lead">Ingrese el Detalle de la Factura</p>
                                <div class="row">
                                    <div class="col-md-3 col-sm-12">
                                        <label for="dr_ProvedorCombustible">Tipo de Combustible</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="nav-icon fas fa-gas-pump"></i>
                                                </div>
                                            </div>
                                            <asp:DropDownList ID="dr_TipoCombustible" CssClass="form-control custom-select" runat="server" aria-describedby="txt_dr_TipoCombustible_HelpBlock"></asp:DropDownList>
                                        </div>
                                        <span id="txt_dr_TipoCombustible_HelpBlock" class="form-text text-muted">Tipo de Combustible</span>
                                    </div>
                                    <div class="col-md-3 col-sm-12">
                                        <label for="txt_NumeroFactura">Total Galones</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="nav-icon fas fa-calculator"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_TotalGalones" TextMode="Number" name="txt_TotalGalones" placeholder="123" MaxLength="10" type="text" aria-describedby="txt_TotalGalones_HelpBlock" class="form-control" />
                                        </div>
                                        <span id="txt_TotalGalones_HelpBlock" class="form-text text-muted">Total de Galones</span>
                                    </div>
                                    <div class="col-md-3 col-sm-12">
                                        <label for="txt_CostoGalon">Costo por Galón</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="nav-icon fas fa-money-bill"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_CostoGalon" name="txt_CostoGalon" TextMode="Number" placeholder="C$ 123" MaxLength="10" type="text" aria-describedby="txt_txt_CostoGalon_HelpBlock" class="form-control" />
                                        </div>
                                        <span id="txt_txt_CostoGalon_HelpBlock" class="form-text text-muted">Costo del Galón</span>
                                    </div>
                                    <div class="col-md-3 col-sm-12">
                                        <label for="txt_TotalxGalones">Costo Total</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="nav-icon fas fa-money-bill"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_TotalxGalones" name="txt_CostoGalon" TextMode="Number" placeholder="123" MaxLength="10" type="text" aria-describedby="txt_TotalxGalones_HelpBlock" class="form-control" />
                                        </div>
                                        <span id="txt_TotalxGalones_HelpBlock" class="form-text text-muted">Costo del Galón</span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-sm-12">
                                        <label for="btn_AgregarDetalle">Agregar Detalle</label><br />
                                        <asp:Button Text="Agregar Detalle" name="submit" type="submit" UseSubmitBehavior="false" CausesValidation="false" class="btn btn-success form-control elevation-2" runat="server" ID="btn_AgregarDetalle" OnClick="btn_AgregarDetalle_Click" />
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12 col-sm-12">
                                        <asp:Repeater ID="rp_detalles" runat="server" OnItemCommand="rp_detalles_ItemCommand">
                                            <HeaderTemplate>
                                                <table class="table table-striped table-bordered table-responsive-sm">
                                                    <tr class="bg-dark">
                                                        <td><b>TipoCombustible</b></td>
                                                        <td><b>Total Galones</b></td>
                                                        <td><b>Costo x Galón</b></td>
                                                        <td><b>Total Costo</b></td>
                                                        <td><b>Remover</b></td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <tr>
                                                    <td>
                                                        <%#Eval("NombreTipoCombustible")%>   
                                                    </td>
                                                    <td>
                                                        <%#Eval("TotalGalones","{0:n}")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("CostoxGalon","{0:n}")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("TotalCostoGalon", "{0:n}") %>
                                                    </td>
                                                    <td>
                                                        <asp:Button Text="Remover" runat="server" CssClass="btn btn-primary form-control" ID="btn_Seleccionar" CommandArgument='<%#Eval("ID_DetalleIngreso")%>' CommandName="cmd_remover" />
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
                            <div class="row">
                                <!-- /.col -->
                                <div class="col-6">
                                    <p class="lead">Total de Ingresos Combustible</p>

                                    <div class="table-responsive">
                                        <table class="table">
                                            <tr>
                                                <th style="width: 50%">Total Galones:</th>
                                                <td>
                                                    <asp:Label Text="0.0" ID="lbl_totalGalones" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <th>Costo Total:</th>
                                                <td>C$<asp:Label Text="0.0" ID="lbl_CostoTotal" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <th>Total Pagado</th>
                                                <td>C$<asp:Label Text="0.0" ID="lbl_TotalPagado" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->
                        </div>
                        <div class="container">
                            <marquee class="news-content"> <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud</p> <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto </p> <p>Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam </p> </marquee>
                        </div>
                        <div class="form-group row">
                            <div class="text-center col-12">
                                <asp:Button Text="Revisar y Continuar" name="submit" type="submit" class="btn btn-lg btn-primary" runat="server" ID="btn_ChekAllBS" OnClick="btn_ChekAllBS_Click" />
                            </div>
                        </div>
                    </div>
                    </div>

                    <%--MODAL PARA ADEVERTIR DE LA ELIMINACION--%>
                    <div class="modal fade" id="advertencia-modal-sm">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Small Modal</h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <h4 class="card-title">Atencion!!
                                    </h4>
                                    <ion-icon name="warning-outline" style="color: orange; font-size: 128px; width: 100%; display: flex; align-items: center; justify-content: center;"></ion-icon>
                                    <p class="lead">Esta completamente seguro que desea eliminar este elemento, si continua no se podra deshacer posteriormente, ademas solo se esta permitido eliminar elementos que no se han usado en el sistema y fueron ingresados por equivocacion!!</br>¿Continuar? </p>
                                    <div class="text-center">
                                        <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btn_eliminarProveedor" runat="server" data-dismiss="modal" data-backdrop="false" UseSubmitBehavior="false" CausesValidation="false" />
                                    </div>
                                </div>
                                <div class="modal-footer justify-content-between">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->
                    <%--MODAL PARA CARGAR LA IMAGEN DE LA FACTURA--%>
                    <div class="modal fade" id="guardar-modal-sm">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Small Modal</h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <h4 class="card-title">Guardar Factura Combustible
                                    </h4>
                                    <ion-icon name="save-outline" style="color: orange; font-size: 128px; width: 100%; display: flex; align-items: center; justify-content: center;"></ion-icon>
                                    <p class="lead">A continuacion Ingrese la imagen de la factura para continuar</p>
                                    <div class="form-group row">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="inputCirculacionC1"><i class="fa fa-file"></i></span>
                                            </div>
                                            <div class="custom-file">
                                                <input type="file" class="custom-file-input" id="ip_facturaFoto" aria-describedby="ip_facturaFoto" runat="server" />
                                                <label class="custom-file-label" for="ip_facturaFoto">Cargue una imagen de la factura original para registro</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="text-center">
                                        <asp:Button Text="Guardar Ingreso de Combustible" name="submit" type="submit" class="btn btn-lg btn-success" runat="server" ID="btn_GuardarIngreso" data-backdrop="false" OnClick="btn_GuardarIngreso_Click" Visible="false" />
                                    </div>
                                </div>
                                <div class="modal-footer justify-content-between">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->

                    <%--MODAL PARA CARGAR LA IMAGEN DE LA FACTURA--%>
                    <div class="modal fade" id="Info-modal-sm">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Small Modal</h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <h4 class="card-title">Guardar Factura Combustible
                                    </h4>
                                    <ion-icon name="save-outline" style="color: orange; font-size: 128px; width: 100%; display: flex; align-items: center; justify-content: center;"></ion-icon>
                                    <p class="lead">
                                        <asp:Label Text="" ID="lbl_mensaje" runat="server" />
                                    </p>
                                    <div class="text-center">
                                        <asp:Button Text="Continuar" name="submit" type="submit" class="btn btn-lg btn-success" runat="server" Visible="false" />
                                    </div>
                                </div>
                                <div class="modal-footer justify-content-between">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_GuardarIngreso" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>

    <%--ESTE SCRIP PERMITE CALCULAR DE LADO DEL CLIENTE UNA MULTIPLICACION DE DOS NUMEROS--%>
    <%-- TAGS: MULTIPLICACION DE INPUT, MULTIPLICACION, VALUE, ONCHANGE --%>
    <script>
        document.getElementById('<%=txt_TotalGalones.ClientID%>').onkeyup = raise;
        document.getElementById('<%=txt_CostoGalon.ClientID%>').onkeyup = raise;
        function raise() {
            var totalGalones = document.getElementById('<%=txt_TotalGalones.ClientID%>');
            var CostoPorGalon = document.getElementById('<%=txt_CostoGalon.ClientID%>');
            var btcount = document.getElementById('<%=txt_TotalxGalones.ClientID%>');
            btcount.value = totalGalones.value * CostoPorGalon.value;
            // btcount.onchange();
        }
    </script>
    <script>
        $(".custom-file-input").on("change", function () {
            var fileName = $(this).val().split("\\").pop();
            $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {

                if (sender._postBackSettings.panelsToUpdate != null) {
                    document.getElementById('<%=txt_TotalGalones.ClientID%>').onkeyup = raise;
                    document.getElementById('<%=txt_CostoGalon.ClientID%>').onkeyup = raise;
                    function raise() {
                        var totalGalones = document.getElementById('<%=txt_TotalGalones.ClientID%>');
                        var CostoPorGalon = document.getElementById('<%=txt_CostoGalon.ClientID%>');
                        var btcount = document.getElementById('<%=txt_TotalxGalones.ClientID%>');
                        btcount.value = totalGalones.value * CostoPorGalon.value;
                        //   btcount.onchange();
                    }
                    /*
                  VERIFICA CUANDO HAY UN CAMBIO EN EL CAMPO DE ENTRADA (INPUT), TOMA EL VALOR Y CAMBIA LA PALABRA EN EL CAMPO FILE SELECT, POR EL NOMBRE DEL ARCHIVO SELECCIONADO
                  */
                    $(".custom-file-input").on("change", function () {
                        var fileName = $(this).val().split("\\").pop();
                        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
                    });
                }

            });
        };
    </script>
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

        //function ShowModal_Advertencia() {
        //    $('#advertencia-modal-sm').modal("show");
        //}
        //function ShowModal_Guardar() {
        //    $('#guardar-modal-sm').modal("show");
        //}
        //function ShowModal_Info() {
        //    $('#Info-modal-sm').modal("show");
        //}

    </script>
</asp:Content>
