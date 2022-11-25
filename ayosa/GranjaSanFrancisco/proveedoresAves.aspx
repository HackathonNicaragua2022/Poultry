<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="proveedoresAves.aspx.cs" Inherits="POULTRY.GranjaAvicola.proveedoresAves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Proveedores de Aves</title>
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
      <%--Footable vercion v3.1.6--%>
    <link href="../footablev316/css/footable.bootstrap.min.css" rel="stylesheet" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Proveedores de Aves de Engorde</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Granja POULTRY</a></li>
                        <li class="breadcrumb-item active">Proveedores de Aves</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="up_principal" runat="server">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <!-- left column -->
                        <div class="col-md-6">
                            <div class="card card-info" runat="server" id="nuevoProveedor" visible="false">
                                <div class="card-header">
                                    <h3 class="card-title">Nuevo Proveedor de Aves de Engorde</h3>
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div class="form-horizontal">
                                    <div class="card-body">
                                        <div class="form-group row">
                                            <label for="txt_NombreProveedor" class="col-sm-2 col-form-label">Nombre Proveedor</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txt_NombreProveedor" MaxLength="50" class="form-control" focused />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="txt_Procedencia" class="col-sm-2 col-form-label">Pais o Procedencia</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txt_Procedencia" MaxLength="50" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="txt_Representante" class="col-sm-2 col-form-label">Representante o contacto</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txt_Representante" MaxLength="50" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="txt_TelefonoContacto" class="col-sm-2 col-form-label">Telefono de Contacto</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txt_TelefonoContacto" MaxLength="12" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="offset-sm-2 col-sm-10">
                                                <div class="form-check">
                                                    <asp:CheckBox Text="&nbsp;&nbsp;Activo" Checked="true" runat="server" class="form-check-input" ID="chk_activo" />
                                                    <label class="form-check-label" for="chk_activo"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.card-body -->
                                    <div class="card-footer">
                                        <asp:Button Text="Guardar Nuevo Proveedor" class="btn btn-info btn-lg" runat="server" UseSubmitBehavior="false" CausesValidation="false" ID="btn_GuardarNuevoProveedor" OnClick="btn_NuevoProveedor_Click" />
                                        <asp:Button Text="Guardar Cambio Proveedor" class="btn btn-warning btn-lg" runat="server" UseSubmitBehavior="false" CausesValidation="false" ID="btn_GuardarCambiosEnProveedor" OnClick="btn_GuardarCambiosEnProveedor_Click" Visible="false" />
                                        <asp:Button Text="Cancelar" class="btn btn-default float-right" UseSubmitBehavior="false" CausesValidation="false" ID="btn_CancelarVentanaEdicion" runat="server" OnClick="btn_CancelarVentanaEdicion_Click" />
                                    </div>
                                    <!-- /.card-footer -->
                                </div>
                            </div>
                            <div id="div_alertMarquee" style="padding: 0px 15px 0px 15px;" runat="server">
                            </div>
                            <!-- /.card -->
                            <div class="card card-info" runat="server" id="proveedores">
                                <div class="card-header">
                                    <h3 class="card-title">Proveedores </h3>
                                    <asp:Button Text="Agregar Nuevo Proveedor" CssClass="btn btn-warning float-right" CausesValidation="false" UseSubmitBehavior="false" runat="server" OnClick="btn_AgregarProv_Click" ID="btn_NuevoProveedor" />
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div class="form-horizontal">
                                    <div class="card-body">
                                        <div id="partToPrint">
                                            <div class="lead">Proveedores de aves de engorde </div>
                                            <asp:Repeater ID="rp_proveedoresAves" runat="server" OnItemCommand="rp_proveedoresAves_ItemCommand">
                                                <HeaderTemplate>
                                                    <table id="tbl_FechasCancelacion" class="table table-bordered table-hover table-responsive-sm bg-white"  style="border-collapse: collapse">
                                                        <thead>
                                                            <tr>
                                                                <th data-class="expand">#</th>
                                                                <th>Nombre Proveredor</th>
                                                                <th data-breakpoints="xs sm md lg">Procedencia</th>
                                                                <th>Representante</th>
                                                                <th>Telefono Contacto</th>
                                                                <th data-breakpoints="xs sm md lg">Activo</th>
                                                                <th data-breakpoints="all">Editar</th>
                                                                <th data-breakpoints="all">Eliminar</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 1px;">
                                                            <h5><%#Container.ItemIndex + 1%></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><%#Eval("Nombre_Proveedor")%></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><%#Eval("Procedencia")%></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><b><%#Eval("Representante")%> </b></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <%#Eval("TelefonoContacto")%>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <%#Eval("Activo")%>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <asp:LinkButton ID="btn_Editar" CommandName="cmd_Editar" CommandArgument='<%#Eval("ID_ProveedoresAves")%>' CssClass="btn btn-warning form-control" runat="server"><i class="fa fa-solid fa-user"></i></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <asp:LinkButton ID="btn_Eliminar" CommandName="cmd_Eliminar" CommandArgument='<%#Eval("ID_ProveedoresAves")%>' CssClass="btn btn-danger form-control" runat="server"><i class="fa fa-solid fa-trash-o"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                                  
                                                    </table>   
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                    <!-- /.card-body -->
                                    <div class="card-footer">
                                        <button type="submit" class="btn btn-info" onclick="printContent('partToPrint');">Imprimir Reporte</button>
                                    </div>
                                    <!-- /.card-footer -->
                                </div>
                            </div>
                            <!-- /.card -->
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
       <script src="../footablev316/js/footable.min.js"></script>

    <%--imprimir seccion de la pagina--%>
    <script>
        $(document).ready(function () {
            $('.table').footable();
        })
        //$(document).ready(function () {
        //    $('#tbl_FechasCancelacion').DataTable();
        //    $('.dataTables_length').addClass('bs-select');
        //});


        function printContent(el) {
            var restorepage = $('body').html();
            var printcontent = $('#' + el).clone();
            $('body').empty().html(printcontent);
            window.print();
            $('body').html(restorepage);
        }
    </script>

    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    $('.table').footable();
                }
            });
        };
    </script>
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
