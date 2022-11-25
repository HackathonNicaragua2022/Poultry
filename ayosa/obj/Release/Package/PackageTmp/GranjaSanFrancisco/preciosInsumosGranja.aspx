<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="preciosInsumosGranja.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.preciosInsumosGranja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Precios de Insumos Granja</title>
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
                    <h1>Precios de Insumos Granja</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Granja POULTRY</a></li>
                        <li class="breadcrumb-item active">Precios de Insumos</li>
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
                        <div class="col-md-12">
                            <!-- /.card -->
                            <div class="card card-info" runat="server" id="proveedores">
                                <div class="card-header">
                                    <h3 class="card-title">Precios de Insumos Granja</h3>
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div class="form-horizontal">
                                    <div class="card-body">
                                        <div id="partToPrint">
                                            <div class="lead">Los costos o precios de insumos, son actualizados automaticamente a medida que se usen en las ventanas de registro de insumos, cuando un el costo de un insumo cambie, se guardar el costo anterior para su visualizacion y se actualizara el nuevo y asi sucesivamente</div>
                                            <asp:Repeater ID="rp_insumosGranja" runat="server">
                                                <HeaderTemplate>
                                                    <table id="tbl_FechasCancelacion" class="table table-bordered table-hover table-responsive-sm bg-white" style="border-collapse: collapse">
                                                        <thead>
                                                            <tr>
                                                                <th data-class="expand">#</th>
                                                                <th data-breakpoints="xs sm md">Categoria</th>
                                                                <th data-breakpoints="xs sm md">Medida</th>
                                                                <th>Insumo</th>
                                                                <th data-breakpoints="xs sm md">Costo Dolares</th>
                                                                <th>Costo Cordobas</th>
                                                                <th data-breakpoints="xs sm">Taza de Cambio</th>
                                                                <th data-breakpoints="all">Ultimo Costo Dol</th>
                                                                <th data-breakpoints="all">Ultimo Costo Cord</th>
                                                                <th data-breakpoints="all">Ultima Taza</th>
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
                                                            <h5><%#Eval("NombreMedida")%></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><b><%#Eval("NombreInsumo")%> </b></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><b><%#Eval("precioDolares","$ {0:n}")%></b></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><b><%#Eval("precioCordobas","C$ {0:n}")%></b></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><b><%#Eval("tazaCambio","C$ {0:n}")%></b></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><b><%#Eval("ultimoPrecioDolares","$ {0:n}")%></b></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><b><%#Eval("ultimoPrecioCordobas","C$ {0:n}")%></b></h5>
                                                        </td>
                                                        <td style="width: 1px;">
                                                            <h5><b><%#Eval("ultimaTazaCambio","C$ {0:n}")%></b></h5>
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
