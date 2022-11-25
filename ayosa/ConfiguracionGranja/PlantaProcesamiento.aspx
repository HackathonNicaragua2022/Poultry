<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="PlantaProcesamiento.aspx.cs" Inherits="POULTRY.ConfiguracionGranja.PlantaProcesamiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Planta de Procesamiento</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link href="../css/bootstrap4-toggle.min.css" rel="stylesheet" />
    <style>
        #message {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
        }

        #inner-message {
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">PLANTA DE PROCESAMIENTO</h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Configuraciones</a></li>
                        <li class="breadcrumb-item active">Planta de procesamiento</li>
                    </ol>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-6">
                            <!-- general form elements -->
                            <div class="card card-danger">
                                <div class="card-header">
                                    <h3 class="card-title">Mataderos</h3>
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div class="card-body">
                                    <nav>
                                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                            <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true">CREACION</a>
                                            <a class="nav-item nav-link" id="nav-profile-tab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false">MATADEROS</a>
                                        </div>
                                    </nav>
                                    <div class="tab-content" id="nav-tabContent">
                                        <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                                            <hr />
                                            <div>
                                                <%--Nombre del matadero--%>
                                                <div class="form-group">
                                                    <label for="txt_NombreMatadero">NOMBRE MATADERO</label>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">
                                                                <i class="fa fa-building"></i>
                                                            </div>
                                                        </div>
                                                        <asp:TextBox runat="server" ID="txt_NombreMatadero" name="txt_NombreMatadero" type="text" aria-describedby="txt_NombreMataderoHelpBlock" required="required" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <span id="txt_NombreMataderoHelpBlock" class="form-text text-muted">Nombre del Matadero</span>
                                                </div>
                                                <%--direccion del matadero--%>
                                                <div class="form-group">
                                                    <label for="txt_Ubicacion">Ubicación</label>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">
                                                                <i class="fa fa-map-marker"></i>
                                                            </div>
                                                        </div>
                                                        <asp:TextBox ID="txt_Ubicacion" name="txt_Ubicacion" type="text" class="form-control" aria-describedby="txt_UbicacionHelpBlock" required="required" runat="server"></asp:TextBox>

                                                    </div>
                                                    <span id="txt_UbicacionHelpBlock" class="form-text text-muted">Direccion de la planta o granja</span>
                                                </div>
                                                <%--Nombre del Encargado--%>
                                                <div class="form-group">
                                                    <label for="txt_NombreResponsable">Nombre del Responsable del Matadero</label>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">
                                                                <i class="fa fa-user"></i>
                                                            </div>
                                                        </div>
                                                        <asp:TextBox ID="txt_NombreResponsable" name="txt_NombreResponsable" type="text" class="form-control" aria-describedby="txt_NombreResponsableHelpBlock" runat="server"></asp:TextBox>
                                                    </div>
                                                    <span id="txt_NombreResponsableHelpBlock" class="form-text text-muted">Nombre del responsable actual del Matadero</span>
                                                </div>
                                                <%--Telefono del resposable del matadero--%>
                                                <div class="form-group">
                                                    <label for="txt_TelefonoResponsable">Teléfono del Responsable</label>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">
                                                                <i class="fa fa-phone-square"></i>
                                                            </div>
                                                        </div>
                                                        <asp:TextBox ID="txt_TelefonoResponsable" name="txt_TelefonoResponsable" type="text" class="form-control" aria-describedby="txt_TelefonoResponsableHelpBlock" runat="server"></asp:TextBox>
                                                    </div>
                                                    <span id="txt_TelefonoResponsableHelpBlock" class="form-text text-muted">Número para contacto con el Responsable</span>
                                                </div>
                                                <%--Capcidad produccion--%>
                                                <div class="form-group">
                                                    <label for="txt_CapacidadProduccionMatadero">Capacidad Produccion Pollos por Hora</label>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">
                                                                <i class="fa fa-box-tissue"></i>
                                                            </div>
                                                        </div>
                                                        <asp:TextBox ID="txt_CapacidadProduccionMatadero" TextMode="Number" name="txt_CapacidadProduccionMatadero" type="text" class="form-control" aria-describedby="txt_CapacidadProduccionMataderoHelpBlock" required="required" runat="server"></asp:TextBox>
                                                    </div>
                                                    <span id="txt_CapacidadProduccionMataderoHelpBlock" class="form-text text-muted">Aproximado de pollos procesados por Hora</span>
                                                </div>
                                                <%--Capacidad de libras general por hora--%>
                                                <div class="form-group">
                                                    <label for="txt_CapcidadLibraxHora">Capacidad Produccion Libras x hora</label>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">
                                                                <i class="fa fa-cubes"></i>
                                                            </div>
                                                        </div>
                                                        <asp:TextBox ID="txt_CapcidadLibraxHora" TextMode="Number" name="txt_CapcidadLibraxHora" type="text" class="form-control" aria-describedby="txt_CapcidadLibraxHoraHelpBlock" required="required" runat="server"></asp:TextBox>
                                                    </div>
                                                    <span id="txt_CapcidadLibraxHoraHelpBlock" class="form-text text-muted">Capcidad de la planta para procesar libras por hora</span>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btn_CrearMatadero" runat="server" class="btn btn-primary" Text="Crear Nuevo Matadero" UseSubmitBehavior="false" CausesValidation="false" OnClick="btn_CrearMatadero_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                                            <hr />
                                            <h4>Mataderos
                                            </h4>
                                            <asp:GridView ID="gv_Mataderos" DataKeyNames="ID_Matadero" CssClass="table table-sm  table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_Mataderos_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="NombreMatadero" HeaderText="Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="DireccionMatadero" HeaderText="Ubicacion"></asp:BoundField>
                                                    <asp:BoundField DataField="EncargadoMatadero" HeaderText="Responsable"></asp:BoundField>
                                                    <asp:BoundField DataField="TelefonoMatadero" HeaderText="Telefono Resp"></asp:BoundField>
                                                    <asp:BoundField DataField="CapacidadProduccionPollosxHora" HeaderText="Capacidad Instalada"></asp:BoundField>
                                                    <asp:BoundField DataField="CapacidadProduccionxLibrasHora" HeaderText="Fecha Creacion"></asp:BoundField>

                                                    <asp:TemplateField HeaderText="Remover">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Eliminar el item de la base de datos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="remover" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> 
                                                            </asp:LinkButton>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.card-body -->
                                <div class="card-footer">
                                </div>
                            </div>
                            <!-- /.card -->
                        </div>
                        <div class="col-md-6">
                            <!-- general form elements -->
                            <div class="card card-success">
                                <div class="card-header">
                                    <h3 class="card-title">Configuracion</h3>
                                </div>
                                <!-- /.card-header -->
                                <!-- form start -->
                                <div class="card-body">
                                    <hr />
                                    <h4>Configuraciones Generales
                                    </h4>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txt_librasJavaVacia">Libras por java vacia</label>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <i class="fa fa-dropbox"></i>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txt_librasJavaVacia" TextMode="Number" name="txt_librasJavaVacia" placeholder="ejem: 15.80" type="text" aria-describedby="txt_librasJavaVaciaHelpBlock" class="form-control" OnTextChanged="txt_librasJavaVacia_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <span id="txt_librasJavaVaciaHelpBlock" class="form-text text-muted">Libra por Java Vacia</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txt_LibrasCanastaVacia">Libras por Canasta vacia</label>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <i class="fa fa-dropbox"></i>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txt_LibrasCanastaVacia" TextMode="Number" name="txt_librasJavaVacia" placeholder="ejem: 5.0" type="text" aria-describedby="txt_LibrasCanastaVacia_hb" class="form-control" OnTextChanged="txt_LibrasCanastaVacia_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <span id="txt_LibrasCanastaVacia_hb" class="form-text text-muted">Libras por Canasta Vacia</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.card-body -->
                                <div class="card-footer">
                                </div>
                            </div>
                            <!-- /.card -->
                        </div>
                    </div>
                    <%--=========================================================================================================================================--%>
                    <div class="modal fade" id="modalInfo" tabindex="-1" role="dialog" aria-labelledby="labeInfo">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="labeInfo">Atencion!!</h5>
                                    <button type="button" class="close" data-dismiss="modal" data-backdrop="false" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="modal-body">
                                    <div class="text-center">
                                        <img id="imagenModalInfo" src="~/imagenes/camera_test.png" alt="Ok" class="img-responsive center-block" runat="server" /><hr />
                                        <asp:Label ID="lbl_modalInfo" runat="server" Text="Label" Width="50%">mensaje</asp:Label>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal" data-backdrop="false">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--=========================================================================================================================================--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Toasts -->
            <div aria-live="polite" aria-atomic="true">

                <!-- Position it -->
                <div style="position: fixed; top: 6rem; right: 0;">
                    <div class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-delay="7000">
                        <div class="toast-header bg-success">
                            <img src="../imagenes/logop-full-b.png" class="rounded mr-2" alt="ico" width="32" />
                            <strong class="mr-auto">Bootstrap</strong>
                            <small>11 mins ago</small>
                            <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="toast-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="" ID="lbl_Toast" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="../js/bootstrap4-toggle.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_Mataderos.ClientID%>").footable();
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
                    $("#<%=gv_Mataderos.ClientID%>").footable();
                    //------------------------------------------                                                           
                }
            });
        };
        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }
        function ShowtoastMessage() {
            $('.toast').toast('show');
        }
    </script>
</asp:Content>
