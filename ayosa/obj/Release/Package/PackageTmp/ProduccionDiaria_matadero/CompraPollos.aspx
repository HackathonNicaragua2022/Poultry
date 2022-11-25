<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="CompraPollos.aspx.cs" Inherits="POULTRY.ProduccionDiaria_matadero.CompraPollos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Programacion de Pecesos</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>PROCESOS MATADERO</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Planta Procesadora</a></li>
                        <li class="breadcrumb-item"><a href="#">Produccion Diaria</a></li>
                        <li class="breadcrumb-item active">Remisiones</li>
                    </ol>
                </div>
            </div>
            <small class="small text-muted">Indique las configuraciones generales de las remisiones entrantes</small>
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <!-- Default box -->
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title">Remisiones Matadero</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div>
                                            <div class="form-group">
                                                <label for="dr_Granjas">Granja Remitente</label>
                                                <div>
                                                    <asp:DropDownList ID="dr_Granjas" AutoPostBack="true" OnSelectedIndexChanged="dr_Granjas_SelectedIndexChanged" name="dr_Granjas" class="custom-select" aria-describedby="dr_GranjaHelpBlock" required="required" runat="server"></asp:DropDownList>
                                                    <span id="dr_GranjaHelpBlock" class="form-text text-muted">La Granja que Remite los Broilers</span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_NumeroReferencia">Comentario</label>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <i class="fa fa-check-circle"></i>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txt_NumeroReferencia" name="txt_NumeroReferencia" placeholder="Numero de Referencia" type="text" class="form-control" aria-describedby="txt_NumeroReferenciaHelpBlock" />
                                                </div>
                                                <span id="txt_NumeroReferenciaHelpBlock" class="form-text text-muted">Comentario sobre el proceso de matanza (*opcional)</span>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_NumeroLote">Lotes Disponibles</label>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <i class="fa fa-inbox"></i>
                                                        </div>
                                                    </div>
                                                    <asp:DropDownList ID="dr_lotesDisponibles" CssClass="dropdown form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <span id="txt_NumeroLoteHelpBlock" class="form-text text-muted">Nombre del Lote en produccion de la granja seleccionada</span>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_fechamatanza">Fecha para matanza</label>
                                                <div class="input-group date" id="datetimepicker1" data-target-input="nearest">
                                                    <div class="input-group-append" data-target="#datetimepicker1" data-toggle="datetimepicker">
                                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txt_FechaMatanza" name="txt_fechamatanza" type="text" aria-describedby="txt_fechamatanzaHelpBlock" class="form-control datetimepicker-input" data-target="#datetimepicker1" EnableViewState="true" />
                                                </div>
                                                <span id="txt_fechamatanzaHelpBlock" class="form-text text-muted">Programe el proceso de Matanza usando el calendario</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div>
                                            <div class="form-group">
                                                <label for="txt_PrecioCompra">Precio de Compra x Libra</label>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <i class="fa fa-money-bill"></i>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox runat="server" TextMode="Number" ID="txt_PrecioCompra" name="txt_PrecioCompra" placeholder="0.00" type="text" class="form-control" aria-describedby="txt_PrecioCompraHelpBlock" />
                                                </div>
                                                <span id="txt_PrecioCompraHelpBlock" class="form-text text-muted">Precio de compra por libra de pollo</span>
                                            </div>
                                            <div class="form-group">
                                                <label for="dr_RazaBroilers">Raza Broilers</label>
                                                <div>
                                                    <asp:DropDownList ID="dr_RazaBroilers" runat="server" name="dr_RazaBroilers" class="custom-select" aria-describedby="dr_RazaBroilersHelpBlock"></asp:DropDownList>
                                                    <span id="dr_RazaBroilersHelpBlock" class="form-text text-muted">Raza de Broilers que recibe</span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="dr_Matadero">Matadero para Procesamiento</label>
                                                <div>
                                                    <asp:DropDownList ID="dr_Matadero" name="dr_Matadero" class="custom-select" aria-describedby="dr_MataderoHelpBlock" runat="server"></asp:DropDownList>
                                                    <span id="dr_MataderoHelpBlock" class="form-text text-muted">Matadero que recibe el lote de Broilers</span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="dr_UsuarioaCargo">Usuario del sistema a cargo de lectura de peso vivo</label>
                                                <div>
                                                    <asp:DropDownList runat="server" ID="dr_UsuarioaCargo" name="dr_UsuarioaCargo" class="custom-select" aria-describedby="dr_UsuarioaCargoHelpBlock"></asp:DropDownList>
                                                    <span id="dr_UsuarioaCargoHelpBlock" class="form-text text-muted">Usuario que estará a cargo y bajo responsabilidad de la lectura de los pesos vivos</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <asp:Button ID="btn_GuardarProcesoRemision" name="submit" type="submit" class="btn btn-primary" runat="server" Text="Preparar nuevo Proceso de Remision" OnClick="btn_GuardarProcesoRemision_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btn_ProcesosCreados" name="submit" type="submit" class="btn btn-info" runat="server" Text="Ver Procesos en el Sistema" OnClick="btn_ProcesosCreados_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.card-body -->
                            <div class="card-footer">
                                Cuando se crea un proceso de remisiones y se selecciona una planta de procesamiento no se puede crear un nuevo proceso hasta que se finalice el primer proceso en esa planta.
                            </div>
                            <!-- /.card-footer-->
                        </div>
                        <!-- /.card -->
                    </div>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script>
        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'L',
                minDate: new Date(),
                format: 'DD/MM/YYYY'
            });
        });

    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#datetimepicker1').datetimepicker({
                        format: 'L',
                        format: 'DD/MM/YYYY',
                        minDate: new Date()
                    });
                }
            });
        };
    </script>
</asp:Content>
