<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterLogistica.Master" AutoEventWireup="true" CodeBehind="InventarioVehicular.aspx.cs" Inherits="POULTRY.ModuloLogistica.InventarioVehicular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inventario Vehicular</title>
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-0 ">
                <div class="col-sm-6">
                    <h1>Inventario Vehicular</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Departamento Logistica</a></li>
                        <li class="breadcrumb-item active">Inventario Vehicular</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up_principal">
        <ProgressTemplate>
            <div class="align-content-center">
                <h2>
                    <marquee>Espere por favor..</marquee>
                </h2>
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
                <div class="spinner-grow text-dark" role="status">
                    <span class="sr-only">Loading...</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up_principal" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-4">
                    <label for="btn_cargar">Nuevo Vehiculo</label>
                    <div class="form-group">
                        <asp:Button ID="btn_Nuevo" Text="Nuevo Vehiculo" runat="server" class="btn btn-primary  form-control" aria-describedby="btnNUevo" OnClick="btn_Nuevo_Click" CausesValidation="false" UseSubmitBehavior="false" />
                        <span id="btnNUevo" class="form-text text-muted">Registrar Nuevo Vehiculo</span>
                    </div>
                </div>
            </div>
            <hr />

            <!-- Table row -->
            <div class="row">
                <div class="col-12 table-responsive p-0">
                    <asp:GridView ID="gv_vehiculos" DataKeyNames="Placa" CssClass="table table-sm  table-light table-condensed  table-bordered table-striped" runat="server" OnRowCommand="gv_vehiculos_RowCommand" EnableViewState="false" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="N#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Placa" HeaderText="Placa">
                                <ItemStyle HorizontalAlign="Left" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="NombreVehiculo" HeaderText="Vehiculo">
                                <ItemStyle HorizontalAlign="Left" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Sede" HeaderText="Asignado A">
                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Combustible" HeaderText="Tipo Combustible">
                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>

                            <%--<asp:ImageField AlternateText="CirculacionImg" DataImageUrlField="URL_CirculacionImg" HeaderText="Circulacion"></asp:ImageField>--%>

                            <%--  <asp:ImageField AlternateText="Placa Imagen" DataImageUrlField="URL_PlacaImg" HeaderText="Placa"></asp:ImageField>

                            <asp:ImageField AlternateText="Seguro Imagen" DataImageUrlField="URL_SeguroVigenteImg" HeaderText="Seguro"></asp:ImageField>--%>

                            <asp:TemplateField HeaderText="Circulacion C1">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_Circulacionc1" runat="server" ImageUrl='<%#Eval("URL_CirculacionImg")%>' Width="156" Height="92" CommandArgument='<%#Eval("URL_CirculacionImg")%>' CommandName="cmd_circulacionC1" ImageAlign="Middle" align="Center" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Circulacion C2">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_Circulacionc2"  runat="server" ImageUrl='<%#Eval("URL_CirculacionImgCara2")%>' Width="156" Height="92" CommandArgument='<%#Eval("URL_CirculacionImgCara2")%>' CommandName="cmd_circulacionC2" ImageAlign="Middle" align="Center" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Galeria de Imagenes">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_Seleccionar"  data-toggle="tooltip" title="Seleccione aqui para ver las galerias de imagenes relacionadas con el vehiculo" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_galeria" CssClass="btn btn-info btn-lg form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-images"></i>
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->


            <%--=========================================================================================================================================--%>
            <div class="modal fade" id="modalNuevoVehiculo" tabindex="-1" role="dialog" aria-labelledby="labenuevo">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="labenuevo">Nuevo Vehiculo</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="up_contenido" runat="server">
                                <ContentTemplate>
                                    <div runat="server" id="panelNuevoVehiculo">
                                        <div class="row">
                                            <div class="col-lg-12 col-sm-12">
                                                <!-- small card -->
                                                <div class="small-box bg-gray-dark">
                                                    <div class="inner">
                                                        <h2>Nuevo Vehiculo</h2>
                                                        <p>Crear un Nuevo Vehiculo</p>
                                                        <br />
                                                        <div class="form-group">
                                                            <label for="txt_placa">Numero de Placa *</label>
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <div class="input-group-text">
                                                                        <i class="fa fa-cubes"></i>
                                                                    </div>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txt_placa" name="txt_placa" placeholder="MY-0000" MaxLength="150" type="text" class="form-control" aria-describedby="text_placa" />
                                                            </div>
                                                            <span id="text_placa" class="form-text">Ingrese el Número de placa del Vehiculo</span>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="txt_placa">Color y Detalles *</label>
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <div class="input-group-text">
                                                                        <i class="fa fa-car"></i>
                                                                    </div>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txt_ColorDetalle" name="txt_ColorDetalle" placeholder="Blanco rayas negras" MaxLength="250" type="text" class="form-control" aria-describedby="text_ColorDetalle" />
                                                            </div>
                                                            <span id="text_ColorDetalle" class="form-text">Especifique el Color y los detalles Visuales del Vehiculo</span>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="dr_Granjas">Nombre Vehiculo</label>
                                                            <div>
                                                                <div class="row mx-0">
                                                                    <div class="col-8 px-0">
                                                                        <asp:DropDownList ID="dr_NombreVehiculos" name="dr_NombreVehiculos" class="custom-select" aria-describedby="text_nombreVehiculo" required="required" runat="server"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-4 px-0">
                                                                        <asp:Button Text="Nuevo..." CssClass="btn btn-warning form-control" runat="server" UseSubmitBehavior="false" CausesValidation="false" ID="btn_NuevoModelosNombre" OnClick="btn_NuevoModelosNombre_Click" />
                                                                    </div>
                                                                </div>
                                                                <span id="text_nombreVehiculo" class="form-text">Seleccione el nombre del vehiculo a agregar</span>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="dr_Granjas">Asignar a la Sede</label>
                                                            <div>
                                                                <asp:DropDownList ID="dr_Sedes" name="dr_sedes" class="custom-select" aria-describedby="text_sedes" required="required" runat="server"></asp:DropDownList>
                                                                <span id="text_sedes" class="form-text">Seleccione la sede que se asignara al vehiculo</span>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="dr_Granjas">Tipo de Combustible</label>
                                                            <div>
                                                                <asp:DropDownList ID="dr_TipoCombustible" name="dr_TipoCombustible" class="custom-select" aria-describedby="text_TipoCombustible" required="required" runat="server"></asp:DropDownList>
                                                                <span id="text_TipoCombustible" class="form-text">Seleccione el tipo de combustible que se asignara al vehiculo</span>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text" id="inputCirculacionC1">Circulacion Cara 1</span>
                                                                    </div>
                                                                    <div class="custom-file">
                                                                        <input type="file" class="custom-file-input" id="ip_circulacionc1" aria-describedby="inputCirculacionC1" runat="server" />
                                                                        <label class="custom-file-label" for="ip_circulacionc1">Seleccione el archivo</label>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text" id="inputCirculacionC2">Circulacion Cara 2</span>
                                                                    </div>
                                                                    <div class="custom-file">
                                                                        <input type="file" class="custom-file-input" id="ip_circulacionc2" aria-describedby="inputCirculacionC2" runat="server" />
                                                                        <label class="custom-file-label" for="ip_circulacionc2">Seleccione el archivo</label>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <br />
                                                        <asp:UpdateProgress ID="up_progress" AssociatedUpdatePanelID="up_contenido" runat="server">
                                                            <ProgressTemplate>
                                                                <h5>Espere por favor a que se suba el archivo...</h5>
                                                                <p>Guardando archivo por favor no cierre esta ventana hasta que termine el proceso</p>
                                                                <div class="spinner-grow text-muted"></div>
                                                                <div class="spinner-grow text-primary"></div>
                                                                <div class="spinner-grow text-success"></div>
                                                                <div class="spinner-grow text-info"></div>
                                                                <div class="spinner-grow text-warning"></div>
                                                                <div class="spinner-grow text-danger"></div>
                                                                <div class="spinner-grow text-secondary"></div>
                                                                <div class="spinner-grow text-dark"></div>
                                                                <br />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <br />
                                                        <div class="alert alert-" role="alert" runat="server" id="alerta_nuevoVehiculo" visible="false">
                                                            <asp:Label Text="El Nombre ya esta en el Sistema" ID="lbl_NuevoMensaje" runat="server" />
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <asp:Button ID="btn_GuardarNuevo" OnClientClick="showProgress()" runat="server" Text="Nuevo Vehiculo" CssClass="btn btn-info btn-lg form-control" UseSubmitBehavior="false" CausesValidation="false" data-backdrop="false" OnClick="btn_GuardarNuevo_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-car"></i>
                                                    </div>
                                                    <div class="small-box-footer">Crear un Nuevo Vehiculo</div>
                                                    </a>
                                                </div>
                                            </div>
                                            <!-- ./col -->
                                        </div>
                                    </div>
                                    <div runat="server" id="panelNuevoNombre" visible="false">
                                        <div class="row">
                                            <div class="col-lg-12 col-sm-12">
                                                <!-- small card -->
                                                <div class="small-box bg-gradient-navy">
                                                    <div class="inner">
                                                        <h2>Nuevo Modelo Vehicular</h2>
                                                        <p>Crear un Nuevo nombre de modelo</p>
                                                        <br />
                                                        <div class="form-group">
                                                            <label for="txt_NombreModelo">Nombre Modelo*</label>
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <div class="input-group-text">
                                                                        <i class="fa fa-car"></i>
                                                                    </div>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txt_NombreModelo" name="txt_NombreModelo" placeholder="TOYOTA" MaxLength="250" type="text" class="form-control" aria-describedby="text_nombreModelo" />
                                                            </div>
                                                            <span id="text_nombreModelo" class="form-text">Ingrese el Nombre de Modelo</span>
                                                        </div>
                                                        <div class="alert alert-" role="alert" runat="server" id="alertaNombre" visible="false">
                                                            <asp:Label Text="El Nombre ya esta en el Sistema" ID="lbl_alertaNombre" runat="server" />
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col">
                                                                    <asp:Button ID="btn_GuardarNuevoNombre" name="btn_GuardarNuevoNombre" type="submit" class="btn btn-primary" runat="server" Text="Guardar nuevo nombre de modelo en el sistema" OnClick="btn_GuardarNuevoNombre_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                                                </div>
                                                                <div class="col">
                                                                    <asp:Button ID="btn_cancelar" name="btn_cancelar" type="submit" class="btn btn-danger" runat="server" Text="Cancelar" OnClick="btn_cancelar_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-car"></i>
                                                    </div>
                                                    <div class="small-box-footer">Crear un Nuevo Modelo</div>
                                                    </a>
                                                </div>
                                            </div>
                                            <!-- ./col -->
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btn_GuardarNuevo" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
            <%--=========================================================================================================================================--%>


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
                                <asp:Label ID="lbl_MsgAdvertencia" runat="server" Text="Label" Width="50%">mensaje</asp:Label>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12">
                                        <label for="btn_Continuar"></label>

                                        <%--Elimina el fondo negro que queda al cerrar el control--%>
                                        <%--data-dismiss="modal" data-backdrop="false"--%>

                                        <asp:Button ID="btn_Continuar" Visible="false" runat="server" Text="Si!, Continuar" CssClass="btn btn-warning form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />

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
            <div class="modal fade" id="modalImage" tabindex="-1" role="dialog" aria-labelledby="lbl_Imagen_mod">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="lbl_Imagen_mod">Imagen</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <div class="text-center">
                                <img id="img_Imagen" src="~/imagenes/warning.png" alt="Imagen" style='height: 100%; width: 100%; object-fit: contain' class="img-responsive center-block" runat="server" /><hr />
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12">
                                        <label for="btn_Descargar"></label>
                                        <asp:Button ID="btn_Descargar" runat="server" Text="Descargar Imagen" CssClass="btn btn-warning form-control" data-backdrop="false" OnClick="btn_Descargar_Click" UseSubmitBehavior="false" />
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_Descargar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="../js/bootstrap4-toggle.min.js"></script>
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>
    <%--PERMITE IMPRIMIR UNA PORCION DE LA PAGINA--%>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_vehiculos.ClientID%>").footable();
            });
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    // $('.toast').toast('show');
                    /*
                    Se declara nuevamente footable por tabla, por que una vez hecho u posback dentro de un 
                    update panel, estos pierden las propiedades
                    */
                    //------------------------------------------
                    $("#<%=gv_vehiculos.ClientID%>").footable();
                    //------------------------------------------                                          




                    /*
                    SIEMPRE QUE SE USA UN FILEUPLOAD, DENTRO DE UN UPDATE PANEL, ESTE NO RESPONDERA A LOS CONGIDOS JQUERY O JAVASCRIP
                    SI NO SON COLOCADOS DENTRO DEL BLOQUE DE VERIFICACION DE UN PONEL POR ACTUALIZAR
                    */

                    //var input = $("#inputGroupFile01").change(function () {
                    //    alert(this.value.split("\\").pop())
                    //})
                    /*
                    VERIFICA CUANDO HAY UN CAMBIO EN EL CAMPO DE ENTRADA (INPUT), TOMA EL VALORY CAMBIA LA PALABRA EN EL CAMPO FILE SELECT, POR EL NOMBRE DEL ARCHIVO SELECCIONADO
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
        function printContent(el) {
            var restorepage = $('body').html();
            var printcontent = $('#' + el).clone();
            $('body').empty().html(printcontent);
            window.print();
            $('body').html(restorepage);
        }
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
        function ShowModalAdvertencia() {
            $('#modalAdvertencia').modal("show");
        }
        function ShowModalNuevo() {
            $('#modalNuevoVehiculo').modal("show");
        }
        function ShowImagen() {
            $('#modalImage').modal("show");
        }

        function showProgress() {
            var updateProgress = $get("<%= up_progress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
</asp:Content>
