<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="CatalogoInsumoGranja.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.CatalogoInsumoGranja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Catalogo de insumos</title>
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
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
                    <h1>Catalogo de Insumos de Granja</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Catalogos</a></li>
                        <li class="breadcrumb-item active">Insumos Granja</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <section class="content">
        <div class="container-fluid">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up_principal" runat="server">
                <ProgressTemplate>
                    <div class="progress">
                        <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">Espere por favor...</div>
                    </div>
                    <br />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="up_principal" runat="server">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <!-- left column -->
                        <div class="col-md-6">
                            <div class="card bg-gradient-lightblue" runat="server" id="nuevoInsumo" visible="false">
                                <div class="card-header">
                                    <h3 class="card-title">Nuevo insumo de Granja</h3>
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div class="form-horizontal">
                                    <div class="card-body">
                                        <div class="form-group row">
                                            <label for="dr_CategoriaInsumo" class="col-sm-2 col-form-label">Categoria Insumos</label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="dr_CategoriaInsumo" CssClass="dropdown form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="dr_UnidadMedida" class="col-sm-2 col-form-label">Unidad de Medida</label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="dr_UnidadMedida" CssClass="dropdown form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="txt_nombreInsumo" class="col-sm-2 col-form-label">Insumo</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txt_nombreInsumo" MaxLength="150" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.card-body -->
                                    <div class="card-footer">
                                        <asp:Button Text="Guardar Nuevo Insumo" class="btn btn-info  btn-lg elevation-2" runat="server" UseSubmitBehavior="false" CausesValidation="false" ID="btn_GuardarNuevoInsumo" OnClick="btn_GuardarNuevoInsumo_Click" />
                                        <asp:Button Text="Guardar Cambio en Insumo" class="btn btn-warning btn-lg" runat="server" UseSubmitBehavior="false" CausesValidation="false" ID="btn_GuardarCambiosEnInsumo" OnClick="btn_GuardarCambiosEnInsumo_Click" />
                                        <asp:Button Text="Cancelar" class="btn btn-default float-right" UseSubmitBehavior="false" CausesValidation="false" ID="btn_CancelarVentanaEdicion" runat="server" OnClick="btn_CancelarVentanaEdicion_Click" />
                                    </div>
                                    <!-- /.card-footer -->
                                </div>
                            </div>
                            <div id="div_alertMarquee" style="padding: 0px 15px 0px 15px;" runat="server">
                            </div>
                            <!-- /.card -->
                            <div class="card bg-gradient-gray-dark" runat="server" id="insumos">
                                <div class="card-header">
                                    <h3 class="card-title">Lista de Insumos</h3>
                                    <asp:Button Text="Agregar Nuevo Insumo" CssClass="btn btn-info float-right" CausesValidation="false" UseSubmitBehavior="false" runat="server" OnClick="btn_NuevoInsumo_Click" ID="btn_NuevoInsumo" />
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div class="form-horizontal">
                                    <div class="card-body bg-blue">
                                        <div id="partToPrint">
                                            <div class="lead">Lista de insumos de granja</div>
                                            <asp:Repeater ID="rp_insumos" runat="server" OnItemCommand="rp_insumos_ItemCommand">
                                                <HeaderTemplate>
                                                    <table id="tbl_Insumos" class="table table-bordered table-hover bg-white" style="width: 100%">
                                                        <thead class="bg-gradient-gray">
                                                            <tr>
                                                                <th data-class="expand">#</th>
                                                                <th data-breakpoints="xs sm md lg">Categoria </th>
                                                                <th>Nombre Insumo</th>
                                                                <th data-breakpoints="xs sm md lg">Medida</th>
                                                                <th data-breakpoints="xs sm md">Editar</th>
                                                                <th data-breakpoints="xs sm md">Eliminar</th>
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
                                                            <h5><%#Eval("NombreCategoria")%></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><%#Eval("NombreInsumo")%></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><%#Eval("NombreMedida")%></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <asp:LinkButton ID="btn_Editar" CommandName="cmd_Editar" CommandArgument='<%#Eval("ID_InsumosGranjaEngorde")%>' CssClass="btn btn-warning form-control" runat="server"><i class="fa fa-solid fa-edit"></i></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <asp:LinkButton ID="btn_Eliminar" CommandName="cmd_Eliminar" OnClientClick="return sweetAlertConfirm(this);" CommandArgument='<%#Eval("ID_InsumosGranjaEngorde")%>' CssClass="btn btn-danger form-control" runat="server"><i class="fa fa-solid fa-trash-o"></i></asp:LinkButton>
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
                        <div class="col-md-6">
                            <h3 class="text-info">Lista de Categorias y Medidas para los insumos</h3>
                            <div class="row">
                                <div class="col">
                                    <h5>Categorias de Insumos</h5>
                                    <asp:LinkButton ID="btn_nuevo" runat="server" OnClick="btn_nuevo_Click"><i class="fa fa-plus-circle text-success" aria-hidden="true"></i>&nbsp;Agregar </asp:LinkButton>
                                    <asp:TextBox runat="server" Visible="false" ID="txt_nuevaCategoria" CssClass="form-control" MaxLength="150" AutoPostBack="true" OnTextChanged="txt_nuevaCategoria_TextChanged" />
                                    <asp:Repeater ID="rp_Categorias" runat="server" OnItemCommand="rp_Categorias_ItemCommand">
                                        <HeaderTemplate>
                                            <ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li><%#Eval("CategoriaInsumo")%> &nbsp;<asp:LinkButton ID="btn_remover" runat="server" CommandName="cmd_Remover" CommandArgument='<%#Eval("ID_CategoriaInsumosGranjaEngorde")%>'><i class="fa fa-trash text-danger" aria-hidden="true"></i></asp:LinkButton></li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col">
                                    <asp:LinkButton ID="btn_nuevaMedida" runat="server" OnClick="btn_nuevaMedida_Click"><i class="fa fa-plus-circle text-success" aria-hidden="true"></i>&nbsp;Agregar </asp:LinkButton>
                                    <asp:TextBox runat="server" placeholder="Unidad de Medida" Visible="false" ID="txt_nuevaMedida" CssClass="form-control" MaxLength="150" AutoPostBack="true" OnTextChanged="txt_nuevaMedida_TextChanged" />
                                    <asp:TextBox runat="server" placeholder="Simbolo" Visible="false" ID="txt_Simbolo" CssClass="form-control" MaxLength="5" AutoPostBack="true" OnTextChanged="txt_Simbolo_TextChanged" />
                                    <asp:Repeater ID="rp_Medidas" runat="server" OnItemCommand="rp_Medidas_ItemCommand">
                                        <HeaderTemplate>
                                            <h5>Medidas de Insumos</h5>
                                            <ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li><%#Eval("UnidadMedidaInsumo")%>&nbsp;(<%#Eval("Simbolo")%>)&nbsp;<asp:LinkButton ID="btn_remover" runat="server" CommandName="cmd_Remover" CommandArgument='<%#Eval("ID_unidadMedida")%>'><i class="fa fa-trash text-danger" aria-hidden="true"></i></asp:LinkButton></li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../footablev316/js/footable.min.js"></script>
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <%--imprimir seccion de la pagina--%>
    <script>
        $(document).ready(function () {
            $('.table').footable();
        })
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
        //ALERTA DE PREGUNTA
        function modal_alert(titulo, mensaje, tipo) {
            $(function () {
                Swal.fire(titulo, mensaje, tipo);
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
