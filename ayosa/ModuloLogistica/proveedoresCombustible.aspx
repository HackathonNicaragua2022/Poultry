<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterLogistica.Master" AutoEventWireup="true" CodeBehind="proveedoresCombustible.aspx.cs" Inherits="POULTRY.ModuloLogistica.proveedoresCombustible" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Proveedores Combustible</title>
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
                    <h2>Proveedores de Combustible</h2>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homeLogistica.aspx">Home Logistica</a></li>
                        <li class="breadcrumb-item active">Proveedores Logistica</li>
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
                                <img class="profile-user-img img-fluid img-circle"
                                    src="../imagenes/logop-w.png"
                                    alt="User profile picture" />
                            </div>
                            <h3 class="profile-username text-left">POULTRY SYSTEM S.A</h3>
                            <asp:Button Text="Ingresar Nuevo Proveedor" ID="mostrarPanel" OnClick="mostrarPanel_Click" CssClass="btn btn-primary" UseSubmitBehavior="false" CausesValidation="false" runat="server" />
                            <p class="text-muted text-left">Proveedores de Combustible</p>
                            <hr />
                            <div class="callout callout-info" runat="server" id="ingresoProveedorPanel" visible="false">
                                <h4 id="tituloTipo" runat="server">Ingreso de un Nuevo Proveedor</h4>
                                <p id="subtituloIngreso" runat="server">Complete los siguientes campos para agregar el proveedor</p>
                                <div class="form-group row">
                                    <label for="txt_NombreProveedor" class="col-4 col-form-label">Nombre de Proveedor</label>
                                    <div class="col-8">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="fa fa-car"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_NombreProveedor" name="txt_NombreProveedor" placeholder="nombre" type="text" class="form-control" aria-describedby="txt_NombreProveedorHelpBlock" />
                                        </div>
                                        <span id="txt_NombreProveedorHelpBlock" class="form-text text-muted">ingrese el nombre de la empresa proveedora de combustible</span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="txt_Direccion" class="col-4 col-form-label">Dirección</label>
                                    <div class="col-8">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="fa fa-map-marker"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_Direccion" name="txt_Direccion" placeholder="Direccion de Proveedor" type="text" class="form-control" aria-describedby="txt_DireccionHelpBlock" />
                                        </div>
                                        <span id="txt_DireccionHelpBlock" class="form-text text-muted">Dirección de la ubicación del proveedor</span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-4">Activo</label>
                                    <div class="col-8">
                                        <asp:CheckBox Text="Activo" runat="server" name="chk_Activo" ID="chk_Activo_0" CssClass="custom-checkbox" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="offset-4 col-8">
                                        <asp:Button Text="Guardar Nuevo Proveedor" name="submit" type="submit" class="btn btn-primary" runat="server" ID="btn_GuardarNuevo" OnClick="btn_GuardarNuevo_Click" />
                                        <asp:Button Text="Guardar Cambios en Proveedor" name="submit" type="submit" class="btn btn-warning" runat="server" Visible="false" ID="btn_GuardarCambios" OnClick="btn_GuardarCambios_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12">
                                    <asp:Repeater ID="rp_Proveedores" runat="server" OnItemCommand="rp_Proveedores_ItemCommand1">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-bordered table-responsive-sm border border-info rounded">
                                                <tr>
                                                    <td><b>Proveedor</b></td>
                                                    <td><b>Direccion</b></td>
                                                    <td><b>Activo</b></td>
                                                    <td><b>Editar</b></td>
                                                    <td><b>Eliminar</b></td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("NombreProveedor")%>   
                                                </td>
                                                <td>
                                                    <%#Eval("DireccionProveedor")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Activo")%>
                                                </td>
                                                <td style="width: 1px;">
                                                    <asp:LinkButton Text="Editar" runat="server" CssClass="btn btn-warning elevation-2" ID="btn_Editar" CommandArgument='<%#Eval("ID_proveedorCombustible")%>' CommandName="cmd_Editar" />
                                                </td>
                                                <td style="width: 1px;">
                                                    <asp:LinkButton Text="Remover" runat="server" CssClass="btn btn-danger elevation-2" ID="btn_Eliminar" CommandArgument='<%#Eval("ID_proveedorCombustible")%>' CommandName="cmd_remover" />
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
                                        <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btn_eliminarProveedor" runat="server" OnClick="btn_eliminarProveedor_Click" data-dismiss="modal" data-backdrop="false" UseSubmitBehavior="false" CausesValidation="false" />
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

            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
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


        function ShowModal_Advertencia() {
            $('#advertencia-modal-sm').modal("show");
        }
    </script>
</asp:Content>
