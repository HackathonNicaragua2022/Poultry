<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="configuracionGranja.aspx.cs" Inherits="POULTRY.ConfiguracionGranja.configuracionGranja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Configuraciones</title>
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
                    <h1 class="m-0">Configuración</h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Configuraciones</a></li>
                        <li class="breadcrumb-item active">Configuraciones Generales</li>
                    </ol>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="card card-primary">
                        <div class="card-header">
                            <h3 class="card-title">Creacion Granjas</h3>
                        </div>
                        <!-- /.card-header -->
                        <!-- form start -->
                        <div class="card-body">
                            <nav>
                                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                    <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true">CREACION</a>
                                    <a class="nav-item nav-link" id="nav-profile-tab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false">GRANJAS</a>
                                </div>
                            </nav>
                            <div class="tab-content" id="nav-tabContent">
                                <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                                    <div>
                                        <div class="form-group">
                                            <label for="txt_NombreGranja">NOMBRE GRANJA</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fa fa-building"></i>
                                                    </div>
                                                </div>
                                                <asp:TextBox runat="server" ID="txt_NombreGranja" name="txt_NombreGranja" type="text" aria-describedby="txt_NombreGranjaHelpBlock" required="required" class="form-control"></asp:TextBox>
                                            </div>
                                            <span id="txt_NombreGranjaHelpBlock" class="form-text text-muted">Nombre de la Granja o Sede Central.</span>
                                        </div>
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
                                        <div class="form-group">
                                            <label for="txt_NombreResponsable">Nombre del Responsable de la Granja</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fa fa-address-book"></i>
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txt_NombreResponsable" name="txt_NombreResponsable" type="text" class="form-control" aria-describedby="txt_NombreResponsableHelpBlock" runat="server"></asp:TextBox>
                                            </div>
                                            <span id="txt_NombreResponsableHelpBlock" class="form-text text-muted">Nombre del responsable actual de la Granja</span>
                                        </div>
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
                                        <div class="form-group">
                                            <label for="txt_NumeroTotalGaleras">Numero Total Galeras</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fa fa-building"></i>
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txt_NumeroTotalGaleras" TextMode="Number" name="txt_NumeroTotalGaleras" type="text" class="form-control" aria-describedby="txt_NumeroTotalGalerasHelpBlock" required="required" runat="server"></asp:TextBox>
                                            </div>
                                            <span id="txt_NumeroTotalGalerasHelpBlock" class="form-text text-muted">Numero total de Galeras instaladas</span>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_CapacidadxGaleras">Capacidad por Galera</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fa fa-cubes"></i>
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txt_CapacidadxGaleras" TextMode="Number" name="txt_CapacidadxGaleras" type="text" class="form-control" aria-describedby="txt_CapacidadxGalerasHelpBlock" required="required" runat="server"></asp:TextBox>
                                            </div>
                                            <span id="txt_CapacidadxGalerasHelpBlock" class="form-text text-muted">Capacidad Total Broilers por Galera</span>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btn_CrearGranja" runat="server" class="btn btn-primary" Text="Crear Nueva Granja" UseSubmitBehavior="false" CausesValidation="false" OnClick="btn_CrearGranja_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                                    <h4>Granjas Registradas
                                    </h4>
                                    <asp:GridView ID="gv_Granjas" DataKeyNames="ID_Granjas" CssClass="table table-sm  table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_Granjas_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre"></asp:BoundField>
                                            <asp:BoundField DataField="Hubicacion" HeaderText="Ubicacion"></asp:BoundField>
                                            <asp:BoundField DataField="NombreResposableActivo" HeaderText="Responsable"></asp:BoundField>
                                            <asp:BoundField DataField="TelefonoResponsableActivo" HeaderText="Telefono Resp"></asp:BoundField>
                                            <asp:BoundField DataField="CapacidadInstalada" HeaderText="Capacidad Instalada"></asp:BoundField>
                                            <asp:BoundField DataField="FechaCreacion_sy" HeaderText="Fecha Creacion"></asp:BoundField>
                                            <asp:BoundField DataField="TotalGaleras" HeaderText="Total Galeras"></asp:BoundField>
                                            <asp:BoundField DataField="CapacidadxGalera" HeaderText="Capacidad por Galera"></asp:BoundField>
                                            <asp:BoundField DataField="CreadoPorUser" HeaderText="Creado Por:"></asp:BoundField>

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
                            <h3 class="card-title">Creacion de Galeras</h3>
                        </div>
                        <!-- /.card-header -->
                        <div class="card-body">
                            <div class="form-group">
                                <label for="dr_Granjas">Granjas</label>
                                <div>
                                    <asp:DropDownList ID="dr_Granjas" name="dr_Granjas" class="custom-select" aria-describedby="dr_GranjasHelpBlock" required="required" runat="server"></asp:DropDownList>
                                    <span id="dr_GranjasHelpBlock" class="form-text text-muted">Seleccione la Granja donde esta hubicado esta Galera</span>
                                </div>
                            </div>
                            <input type="checkbox" checked data-toggle="toggle" data-on="NUEVA" data-off="GALERAS" data-onstyle="info" data-offstyle="success" name="checkbox" value="Credito" id="chk_tipoFactura">
                            <hr />
                            <div class="shadow p-3 mb-5 bg-white rounded" id="NuevaGalera">
                                <div class="form-group">
                                    <label for="txt_NumeroOrden">Numero en Orden</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <i class="fa fa-sort-numeric-asc"></i>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="txt_NumeroOrden" name="txt_NumeroOrden" MaxLength="7" TextMode="Number" placeholder="El numero en relación al Orden de Galeras" type="text" class="form-control" aria-describedby="txt_NumeroOrdenHelpBlock" runat="server"></asp:TextBox>
                                    </div>
                                    <span id="txt_NumeroOrdenHelpBlock" class="form-text text-muted">Indica el Numero de Orden en las Galeras de la Granja Seleccionada</span>
                                </div>
                                <div class="form-group">
                                    <label for="txt_NombreApodo">Nombre de la Galera</label>
                                    <asp:TextBox ID="txt_NombreApodo" name="txt_NombreApodo" placeholder="&quot;Galera la Chiquita&quot;" type="text" class="form-control" aria-describedby="txt_NombreApodoHelpBlock" runat="server"></asp:TextBox>
                                    <span id="txt_NombreApodoHelpBlock" class="form-text text-muted">Obcional, Indica un nombre para la Galera</span>
                                </div>
                                <div class="form-group">
                                    <label for="txt_CapacidadInstalada">Capacidad Instalada</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">Capacidad</div>
                                        </div>
                                        <asp:TextBox ID="txt_CapacidadInstalada" TextMode="Number" MaxLength="7" name="txt_CapacidadInstalada" placeholder="ej: 35000" type="text" class="form-control" aria-describedby="txt_CapacidadInstaladaHelpBlock" runat="server"></asp:TextBox>
                                    </div>
                                    <span id="txt_CapacidadInstaladaHelpBlock" class="form-text text-muted">La Capacidad instalada para la Galera(Máxima en el Mejor de los Casos)</span>
                                </div>
                                <div class="form-group">
                                    <label for="txt_CapacidadNormal">Capacidad Normal</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">Capacidad</div>
                                        </div>
                                        <asp:TextBox ID="txt_CapacidadNormal" TextMode="Number" MaxLength="7" name="txt_CapacidadNormal" placeholder="Ej: 31000" type="text" class="form-control" aria-describedby="txt_CapacidadNormalHelpBlock" runat="server"></asp:TextBox>
                                    </div>
                                    <span id="txt_CapacidadNormalHelpBlock" class="form-text text-muted">Capacidad Normal para la Galera</span>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btn_NuevaGalera" class="btn btn-success" runat="server" Text="Nueva Galera" OnClick="btn_NuevaGalera_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="shadow p-3 mb-5 bg-white rounded hide" id="listaGaleras" style="display: none">
                                <small class="small text-muted">Galeras existentes segun la granja seleccionada</small>
                                <asp:GridView ID="gv_Galeras" DataKeyNames="ID_Galeras" CssClass="table table-sm  table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_Galeras_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="NumeroOrden" HeaderText="Numero Orden"></asp:BoundField>
                                        <asp:BoundField DataField="NombreApodo" HeaderText="Nombre"></asp:BoundField>
                                        <asp:BoundField DataField="CapacidadInstalada" HeaderText="Capacidad Instalada"></asp:BoundField>
                                        <asp:BoundField DataField="CapacidadNormal" HeaderText="Capacidad Normal"></asp:BoundField>
                                        <asp:CheckBoxField DataField="EnProduccion" Text="SI" HeaderText="En Produccion?"></asp:CheckBoxField>
                                        <asp:BoundField DataField="CreadoPorUser" HeaderText="Creado Por"></asp:BoundField>
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
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->
                    <!-- general form elements disabled -->
                    <div class="card card-warning">
                        <div class="card-header">
                            <h3 class="card-title">Raza de Broilers</h3>
                        </div>
                        <!-- /.card-header -->
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div>
                                        <div class="form-group">
                                            <label for="txt_NombreRaza_b">Nombre Raza</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fa fa-star"></i>
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txt_NombreRaza_b" name="txt_NombreRaza_b" placeholder="Ej: Cobb 500" type="text" class="form-control" aria-describedby="txt_NombreRaza_bHelpBlock" required="required" runat="server"></asp:TextBox>
                                            </div>
                                            <span id="txt_NombreRaza_bHelpBlock" class="form-text text-muted">Nombre de la Raza de Pollito Broilers</span>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_TotalEdadDiasP_broilers">Total Edad en Días para Producción</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fa fa-calendar-plus-o"></i>
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txt_TotalEdadDiasP_broilers" TextMode="Number" MaxLength="8" name="txt_TotalEdadDiasP_broilers" placeholder="Ej:45" type="text" class="form-control" aria-describedby="txt_TotalEdadDiasP_broilersHelpBlock" runat="server"></asp:TextBox>
                                            </div>
                                            <span id="txt_TotalEdadDiasP_broilersHelpBlock" class="form-text text-muted">Total de Días aproximados para producción</span>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_PesoPromedioIdea_prod">Peso Ideal para Produccion</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fa fa-calculator"></i>
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txt_PesoPromedioIdea_prod" TextMode="Number" MaxLength="8" name="txt_PesoPromedioIdea_prod" placeholder="ej: 4.5" type="text" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btn_nuevaRazaBroilers" runat="server" Text="Crear Nueva Granja" class="btn btn-warning" OnClick="btn_nuevaRazaBroilers_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <small class="small text-muted">Razas existentes en la Base de Datos</small>
                                    <br />  
                                    <small class="small text-muted">*Tedip: Total Edad en Dias, ideal para produccion</small>
                                    <br />  
                                    <small class="small text-muted">*Pipp: Peso Ideal para Produccion</small>
                                    <asp:GridView ID="gv_RazaBroilers" DataKeyNames="ID_Broilers_Raza" CssClass="table table-sm  table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_RazaBroilers_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="NombreRaza" HeaderText="Nombre Raza"></asp:BoundField>
                                            <asp:BoundField DataField="TotalEdadDias_IdealProduccion" HeaderText="Tedip"></asp:BoundField>
                                            <asp:BoundField DataField="PesoIdealparaProduccion" HeaderText="Pipp"></asp:BoundField>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="../js/bootstrap4-toggle.min.js"></script>
    <script>
        $('input[name=checkbox]').change(function () {
            if ($(this).is(':checked')) {
                console.log("Checkbox is checked..")
                $('#listaGaleras').hide();
                $('#NuevaGalera').fadeIn('slow');
            } else {
                console.log("Checkbox is not checked..")
                $('#NuevaGalera').hide();
                $('#listaGaleras').fadeIn('slow');
            }
        });
    </script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_Granjas.ClientID%>").footable();
                $("#<%=gv_Galeras.ClientID%>").footable();
                $("#<%=gv_RazaBroilers.ClientID%>").footable();
            });
        });
    </script>

    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $("#<%=txt_NumeroTotalGaleras.ClientID%>").on("keypress keyup blur", function (event) {
                        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                            event.preventDefault();
                        }
                    });

                    /*
                    Se declara nuevamente footable por tabla, por que una vez hecho u posback dentro de un 
                    update panel, estos pierden las propiedades
                    */
                    //------------------------------------------
                    $("#<%=gv_Granjas.ClientID%>").footable();
                    $("#<%=gv_Galeras.ClientID%>").footable();
                    $("#<%=gv_RazaBroilers.ClientID%>").footable();
                    //------------------------------------------


                    $(function () {
                        $('#chk_tipoFactura').bootstrapToggle({
                            size: "sm"
                        });
                    })

                    $('input[name=checkbox]').change(function () {
                        if ($(this).is(':checked')) {
                            console.log("Checkbox is checked..")
                            $('#listaGaleras').hide();
                            $('#NuevaGalera').fadeIn('slow');
                        } else {
                            console.log("Checkbox is not checked..")
                            $('#NuevaGalera').hide();
                            $('#listaGaleras').fadeIn('slow');
                        }
                    });


                }
            });
        };
        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }
    </script>
</asp:Content>
