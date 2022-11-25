<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="roles.aspx.cs" Inherits="POULTRY.Usuarios.roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Usuarios en Roles</title>
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Administracion de Roles de Usuario en el sistema</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Configuraciones</a></li>
                        <li class="breadcrumb-item active">Roles de Usuario</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Main content -->
            <section class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-6">
                            <!-- general form elements -->
                            <div class="card card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Usuarios en Roles</h3>
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="dr_Usuarios">Usuarios</label>
                                                <asp:DropDownList ID="dr_Usuarios" runat="server" CssClass="form-control" OnSelectedIndexChanged="dr_Usuarios_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <hr />
                                                <h5>Roles
                                                </h5>
                                                <asp:Repeater ID="rp_roles" runat="server">
                                                    <ItemTemplate>
                                                        <div class="custom-control custom-checkbox">
                                                            <asp:CheckBox ID="chk_roles" Text="<%#Container.DataItem %>" runat="server" AutoPostBack="true" CssClass="custom-radio" OnCheckedChanged="chk_roles_CheckedChanged" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.card-body -->

                                    <div class="card-footer">
                                        <button type="submit" class="btn btn-primary">Submit</button>
                                    </div>
                                </div>
                            </div>
                            <!-- /.card -->
                            <!-- general form elements -->
                            <div class="card card-warning">
                                <div class="card-header">
                                    <h3 class="card-title">Acceso a las paginas</h3>
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div>
                                    <div class="card-body">
                                        <h3>Control de Acceso por Pagina</h3>
                                        <p>Seleccione una pagina para configurar su acceso </p>
                                        <div>
                                            <div class="form-group">
                                                <label for="txt_Ruta">Ruta</label>
                                                <div class="input-group">                                                    
                                                    <asp:TextBox ID="txt_Ruta"  name="txt_Ruta" placeholder="Ej:Admin/controldeVacaciones.aspx" type="text" class="form-control" aria-describedby="txt_RutaHelpBlock" runat="server"></asp:TextBox>
                                                </div>
                                                <span id="txt_RutaHelpBlock" class="form-text text-muted">Seleccione una de las paginas en el árbol de abajo</span>
                                            </div>
                                            <div class="form-group">
                                                <label for="dr_RolesPaginas">Rol para  configurar la pagina</label>
                                                <asp:DropDownList ID="dr_RolesPaginas" name="dr_RolesPaginas" class="form-control" aria-describedby="dr_RolesPaginasHelpBlock" runat="server"></asp:DropDownList>
                                                <span id="dr_RolesPaginasHelpBlock" class="form-text text-muted">Seleccion el rol para controlar el acceso a la pagina, todos los roles anteriores se eliminaran</span>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Button ID="btn_Guardar" runat="server" class="btn btn-primary form-control" Text="Guardar" OnClick="btn_Guardar_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Button ID="btn_Acutalizar" runat="server" class="btn btn-warning form-control" Text="Actualizar Directorio" OnClick="btn_Acutalizar_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="w-20  nav-treeview">
                                            <h6 class="pt-3 pl-3">Carpetas de POULTRY ADMIN</h6>
                                            <small>Tenga cuidado con esta utilidad, si se quita un rol o otro roles con altos privilegio en esta lista solo vera el contenido admitidos para ese rol, de igual manera podria quedar sin acceso a otros sitios</small>
                                            <ul class="mb-1 pl-3 pb-2">
                                                <asp:Repeater ID="rptMenu" runat="server" EnableViewState="false">
                                                    <ItemTemplate>
                                                        <li>
                                                            <span><i class="far fa-folder-open ic-w mx-1"></i>
                                                                <asp:LinkButton ID="btn_padresRoot" CommandArgument='<%#Eval("url")%>' ToolTip='<%#Eval("description")%>' runat="server" OnCommand="btn_padresRoot_Command" data-toggle="tooltip"><%#Eval("Title")%></asp:LinkButton></span>
                                                            <ul class="nested">
                                                                <asp:Repeater runat="server" ID="SecondLevel" DataSource='<%#((SiteMapNode)Container.DataItem).ChildNodes%>'>
                                                                    <ItemTemplate>
                                                                        <li>
                                                                            <span><i class="far fa-folder-open ic-w mx-1"></i>
                                                                                <asp:LinkButton ID="btn_padreschildrens" CommandArgument='<%#Eval("url")%>' ToolTip='<%#Eval("description")%>' runat="server" OnCommand="btn_padreschildrens_Command" data-toggle="tooltip"><%#Eval("Title")%></asp:LinkButton></span>
                                                                            </span>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ul>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                    <!-- /.card-body -->
                                </div>
                            </div>
                            <!-- /.card -->
                        </div>
                        <div class="col-md-6">
                            <!-- general form elements -->
                            <div class="card card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Creacion de Roles</h3>
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="txt_NombreNUevoRole">Nombre Nuevo Role</label>
                                                <asp:TextBox ID="txt_NombreNUevoRole" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="btn_NuevoRole" runat="server" Text="Crear Role" OnClick="btn_NuevoRole_Click" CssClass="btn btn-info" />
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <label for="dr_Roles">Roles</label>
                                            <div>
                                                <asp:DropDownList ID="dr_Roles" name="dr_Roles" class="custom-select" aria-describedby="dr_RolesHelpBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dr_Roles_SelectedIndexChanged"></asp:DropDownList>
                                                <span id="dr_RolesHelpBlock" class="form-text text-muted">Roles en el sistema</span>
                                            </div>
                                        </div>
                                        <h4>Personas en el Rol</h4>
                                        <asp:GridView ID="gv_UsuariosenRoles" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" EmptyDataText="No hay usuarios en el Role Seleccionado" OnRowDeleting="gv_UsuariosenRoles_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>.
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Usuarios">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="UserNameLabel"
                                                            Text='<%# Container.DataItem %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!-- /.card-body -->
                                </div>
                            </div>
                            <!-- /.card -->
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>

    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[data-toggle="tooltip"]').tooltip();
                }
            });
        };
    </script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
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
    </script>
</asp:Content>
