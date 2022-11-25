<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterSedeCentral.Master" AutoEventWireup="true" CodeBehind="galeriaVehiculos.aspx.cs" Inherits="POULTRY.ModuloLogistica.galeriaVehiculos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Galeria Vehicular</title>
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />

    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback/">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/fontawesome-free/css/all.min.css" />
    <!-- Ekko Lightbox -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/ekko-lightbox/ekko-lightbox.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-0 ">
                <div class="col-sm-6">
                    <h1>Galeria de Imagenes para el Vehiculo
                        <asp:Label Text="" ID="lbl_NombreVehiculo" runat="server" /></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Departamento Logistica</a></li>
                        <li class="breadcrumb-item"><a href="InventarioVehicular.aspx">Inventario Vehicular</a></li>
                        <li class="breadcrumb-item active">Galeria Vehicular</li>
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
            <asp:UpdatePanel ID="up_principal" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-12">
                            <div class="card card-navy">
                                <div class="card-header">
                                    <h4 class="card-title">Geleria de Imagenes</h4>
                                    <asp:Button Text="Agregar Nueva Imagen" ID="btn_NuevaImagen" UseSubmitBehavior="true" CausesValidation="false" CssClass="btn btn-warning pull-right" runat="server" OnClick="btn_NuevaImagen_Click" />
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <asp:Repeater ID="rp_imagenes" runat="server" OnItemCommand="rp_imagenes_ItemCommand">
                                            <ItemTemplate>

                                                <div class="col-sm-2">
                                                    <a href='<%#Eval("URLImagen")%>' data-toggle="lightbox" data-title='<%#Eval("DescripcionEstetica")%>' data-gallery="gallery">
                                                        <asp:LinkButton ID="btn_Eliminar" type="button" class="close pull-left" aria-label="Close" runat="server" CommandArgument='<%#Eval("URLImagen")%>' CausesValidation="false" CommandName="cmd_Bajar" UseSubmitBehavior="false">
                                                          <i class="fas fa-download"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton1" type="button" class="close" aria-label="Close" runat="server" CommandArgument='<%#Eval("ID_DetallesEsteticos")%>' CausesValidation="false" CommandName="cmd_Eliminar" UseSubmitBehavior="false">
                                                            <span aria-hidden="true">&times;</span>
                                                        </asp:LinkButton>
                                                        <img src='<%#Eval("URLImagen")%>' class="img-fluid mb-2" alt='<%#Eval("DescripcionEstetica")%>' />
                                                    </a>
                                                    <p class="align-content-center text-center"><%#Eval("DescripcionEstetica").ToString().ToUpperInvariant()%></p>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--=========================================================================================================================================--%>
                    <div class="modal fade" id="modalNuevaImagen" tabindex="-1" role="dialog" aria-labelledby="labenuevo">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="labenuevo">Subir Nueva Imagen</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="up_contenido" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-12 col-sm-12">
                                                    <!-- small card -->
                                                    <div class="small-box bg-gray-dark">
                                                        <div class="inner">
                                                            <h2>Subir Nueva Imagen</h2>
                                                            <p>Subir Nueva Imagen a la Galeria</p>
                                                            <br />
                                                            <div class="form-group">
                                                                <label for="txt_placa">Descripcion de la Imagen</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fa fa-image"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txt_descripcion" name="txt_descripcion" placeholder="Descripcion" MaxLength="150" type="text" class="form-control" aria-describedby="text_desc" />
                                                                </div>
                                                                <span id="text_desc" class="form-text">Descripcion de la Imagen</span>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text" id="inputCirculacionC1">Imagen</span>
                                                                        </div>
                                                                        <div class="custom-file">
                                                                            <input type="file" class="custom-file-input" id="ip_circulacionc1" aria-describedby="inputCirculacionC1" runat="server" />
                                                                            <label class="custom-file-label" for="ip_circulacionc1">Seleccione el archivo</label>
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
                                                            <div class="alert alert-" role="alert" runat="server" id="alerta_nuevaImagen" visible="false">
                                                                <asp:Label Text="El Nombre ya esta en el Sistema" ID="lbl_NuevoMensaje" runat="server" />
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <asp:Button ID="btn_GuardarNuevo" OnClientClick="showProgress()" runat="server" Text="Subir Imagen" CssClass="btn btn-info btn-lg form-control" UseSubmitBehavior="false" CausesValidation="false" data-backdrop="false" OnClick="btn_GuardarNuevo_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="fas fa-image"></i>
                                                        </div>
                                                        <div class="small-box-footer">Subir Nueva Imagen para el Vehiculo</div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <!-- ./col -->
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- /.content -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <!-- Ekko Lightbox -->
    <script src="../AdminTemplate/plugins/ekko-lightbox/ekko-lightbox.min.js"></script>

    <!-- Filterizr-->
    <script src="../AdminTemplate/plugins/filterizr/jquery.filterizr.min.js"></script>


    <script>
        $(function () {
            $(document).on('click', '[data-toggle="lightbox"]', function (event) {
                event.preventDefault();
                $(this).ekkoLightbox({
                    alwaysShowClose: true
                });
            });

            $('.filter-container').filterizr({ gutterPixels: 3 });
            $('.btn[data-filter]').on('click', function () {
                $('.btn[data-filter]').removeClass('active');
                $(this).addClass('active');
            });
        })

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
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
        function ShowModalNuevo() {
            $('#modalNuevaImagen').modal("show");
        }
        function showProgress() {
            var updateProgress = $get("<%= up_progress.ClientID %>");
            updateProgress.style.display = "block";
        }
</script>

</asp:Content>
