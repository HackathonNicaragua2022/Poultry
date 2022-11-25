<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="rubrosAvez.aspx.cs" Inherits="POULTRY.plantaMatadero.rubrosAvez" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Rubros Por Aves</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />

    <!-- DataTables -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/datatables-buttons/css/buttons.bootstrap4.min.css" />

    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up_ne" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
        <ContentTemplate>
            <div class="jumbotron m-1">
                <h1 class="display-3">Rubros de Aves!</h1>
                <p class="lead">Cree Rubros de Avez para el uso en los procesos de pesajes y envio a cuartos congelados</p>
                <hr class="my-3" />
                <div class="row">
                    <div class="col-sm-13 col-md-3">
                        <asp:Button Text="Nuevo Rugro de Ave" CssClass="btn btn-block bg-gradient-primary btn-sm" ID="btn_NuevoRugro" runat="server" OnClick="btn_NuevoRubro_Click" UseSubmitBehavior="false" CausesValidation="false" />
                    </div>
                </div>
            </div>
            <%--=========================================================================================================================================--%>
            <!-- Modal -->
            <div class="modal fade" id="modal_NuevoEditar" tabindex="-1" aria-labelledby="ModalLabel_editar" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="ModalLabel_editar">
                                <asp:Label Text="Rubro" ID="lbl_titulo_ne" runat="server" /></h5>
                            <button type="button" class="close" data-dismiss="modal" data-backdrop="false" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
                            <asp:UpdatePanel ID="up_contenidoNuevoEditar" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="form-group row">
                                            <label for="txt_Codigo" class="col-4 col-form-label">CODIGO</label>
                                            <div class="col-8">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <i class="fa fa-barcode"></i>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox ID="txt_Codigo" required runat="server" name="txt_Codigo" placeholder="1001" type="text" class="form-control" aria-describedby="txt_CodigoHelpBlock" AutoPostBack="true" OnTextChanged="txt_Codigo_TextChanged"></asp:TextBox>
                                                    <div class="input-group-append">
                                                        <asp:LinkButton ID="btn_GenerarCodigo" data-toggle="tooltip" title="Generar Codigo" runat="server" CausesValidation="false" CssClass="input-group-text" UseSubmitBehavior="false" OnClick="btn_GenerarCodigo_Click">
                                                <i class="fa fa-refresh"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <span id="txt_CodigoHelpBlock" class="form-text text-muted">Indique el código asociado a este rubro o bien haga clic en el botón para generar uno automaticamente</span>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="txt_Rubro" class="col-4 col-form-label">RUBRO</label>
                                            <div class="col-8">
                                                <asp:TextBox runat="server" required ID="txt_Rubro" name="txt_Rubro" placeholder="Pechuga sin alas" type="text" class="form-control" aria-describedby="txt_RubroHelpBlock" />
                                                <span id="txt_RubroHelpBlock" class="form-text text-muted">Nombre que tiene lugar el rubro</span>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="txt_siglas" class="col-4 col-form-label">SIGLAS</label>
                                            <div class="col-8">
                                                <asp:TextBox runat="server" required ID="txt_siglas" name="txt_siglas" placeholder="PSA" type="text" class="form-control" aria-describedby="txt_siglasHelpBlock" />
                                                <span id="txt_siglasHelpBlock" class="form-text text-muted">Siglas que ayudaran a identificar el rubro</span>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-4 col-form-label">TIPO DE MENUDO</label>
                                            <div class="col-8">
                                                <div class="custom-controls-stacked">
                                                    <asp:CheckBoxList ID="chk_tipoViceras" CssClass="custom-control custom-checkbox" runat="server" SelectMethod>
                                                        <asp:ListItem Value="1" onclick="MutExChkList(this);">Es Viceras Comestibles</asp:ListItem>
                                                        <asp:ListItem Value="2" onclick="MutExChkList(this);">Es Titil</asp:ListItem>
                                                        <asp:ListItem Value="3" onclick="MutExChkList(this);">Es Higado</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <span id="chk_tiposMenudosHelpBlock" class="form-text text-muted">Seleccione una casilla segun el tipo de menudo que quiere registrar, de lo contrario déjelas vacías para indicar un corte del canal.</span>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-12">
                                                <asp:LinkButton ID="btn_GuardarEditar" Text="Crear Nuevo Rubro" data-toggle="tooltip" title="Crear Nuevo Rubro" runat="server" CausesValidation="false" CssClass="form-control btn btn-primary" UseSubmitBehavior="false" OnClick="btn_GuardarEditar_Click">
                                                <i class="fa fa-save"></i> Guardar Nuevo Rubro de Aves
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btn_GuardarCambios" Text="Guardar los Cambios" data-toggle="tooltip" title="Guardar los Cambios" runat="server" CausesValidation="false" CssClass="form-control btn  btn-warning" UseSubmitBehavior="false" OnClick="btn_GuardarCambios_Click">
                                                <i class="fa fa-refresh"></i> Guardar Cambios
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-backdrop="false" data-dismiss="modal">Cerrar</button>
                            <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
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
                                <asp:Label ID="lbl_MsgAdvertencia" runat="server" Text="Label" Width="90%">mensaje</asp:Label>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12">
                                        <label for="btn_Continuar"></label>
                                        <asp:Button ID="btn_Continuar" runat="server" Text="Si, Continuar" CssClass="btn btn-danger form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />
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
    </asp:UpdatePanel>

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Rubros por Ave</h3>
                        </div>
                        <!-- /.card-header -->
                        <div class="card-body">

                            <asp:UpdatePanel ID="up_grid" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                <ContentTemplate>
                                    <asp:GridView
                                        ID="gv_rubros"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="ID_RubrosAve"
                                        CssClass="table table-bordered table-striped" EmptyDataText="Aun no se crean rubros, por favor hacer click en el boton azul de arriba para comenzar a agregar los rubros"
                                        OnRowCommand="gv_rubros_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="Codigos" HeaderText="Codigos">
                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Producto" HeaderText="Producto">
                                                <ItemStyle HorizontalAlign="Left" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Siglas" HeaderText="Siglas">
                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VicerasComestibles" HeaderText="VicerasComestibles">
                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EsTitil" HeaderText="EsTitil" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EsHigado" HeaderText="EsHigado" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>


                                            <asp:TemplateField HeaderText="Editar">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_Editar" data-toggle="tooltip" title="Puede realizar Cambios en este Rubro" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Editar" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_Eliminar" data-toggle="tooltip" title="Elimine este Rubro" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Eliminar" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-close"></i>
                                                    </asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- /.content -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <!-- DataTables  & Plugins -->
    <script src="../AdminTemplate/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../AdminTemplate/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
    <script src="../AdminTemplate/plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
    <script src="../AdminTemplate/plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>
    <script src="../AdminTemplate/plugins/datatables-buttons/js/dataTables.buttons.min.js"></script>
    <script src="../AdminTemplate/plugins/datatables-buttons/js/buttons.bootstrap4.min.js"></script>
    <script src="../AdminTemplate/plugins/jszip/jszip.min.js"></script>
    <script src="../AdminTemplate/plugins/pdfmake/pdfmake.min.js"></script>
    <script src="../AdminTemplate/plugins/pdfmake/vfs_fonts.js"></script>
    <script src="../AdminTemplate/plugins/datatables-buttons/js/buttons.html5.min.js"></script>
    <script src="../AdminTemplate/plugins/datatables-buttons/js/buttons.print.min.js"></script>
    <script src="../AdminTemplate/plugins/datatables-buttons/js/buttons.colVis.min.js"></script>
    <!-- SweetAlert2 -->
    <script src="../AdminTemplate/plugins/sweetalert2/sweetalert2.min.js"></script>




    <script>
        $(function () {

            $('#<%= gv_rubros.ClientID %>').DataTable({
                "responsive": true, "lengthChange": false, "autoWidth": false,
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            }).buttons().container().appendTo('#<%= gv_rubros.ClientID %>_wrapper .col-md-6:eq(0)');

        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                //if (sender._postBackSettings.panelsToUpdate != null) {
                var table = $('#<%= gv_rubros.ClientID %>').DataTable();
                table.destroy();

                $('#<%= gv_rubros.ClientID %>').DataTable({
                        "responsive": true, "lengthChange": false, "autoWidth": false,
                        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
                    }).buttons().container().appendTo('#<%= gv_rubros.ClientID %>_wrapper .col-md-6:eq(0)');
                //}
            });
            };




            function ShowModal_NuevoEditar() {
                $('#modal_NuevoEditar').modal('show');
            }
            function HideModal_NuevoEditar() {
                $('#modal_NuevoEditar').modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
            }
    </script>
    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>

    <script>
        //ALERTA DE EXITO
        function success_alert(mensaje) {
            $(function () {
                var Toast = Swal.mixin({
                    toast: true,
                    position: 'center',
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
                    position: 'center',
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
                    position: 'center',
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
                    position: 'center',
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
                    position: 'center',
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
    </script>
</asp:Content>
