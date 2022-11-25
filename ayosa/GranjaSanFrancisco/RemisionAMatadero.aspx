<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="RemisionAMatadero.aspx.cs" Inherits="POULTRY.Granja.RemisionAMatadero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Remision Matadero</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link href="../AdminTemplate/open-iconic-master/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <style>
        .table-bordered th, .table-bordered td {
            border: 1px solid #ddd !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Remisiones Matadero</h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="home-admin.aspx">Inicio</a></li>
                        <li class="breadcrumb-item"><a href="homeGranja.aspx">Granja Produccion</a></li>
                        <li class="breadcrumb-item active">Remisiones Matadero</li>
                    </ol>
                </div>
                <!-- /.col -->
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- /.row -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card card-primary card-outline">
                                <div class="card-header">
                                    <h5 class="card-title m-0">Envio de Aves a Matadero</h5>
                                </div>
                                <div class="card-body">
                                    <p class="lead">Datos de compra de pesos de Broilers en proceso de Matanza, Puede Ver los Procesos terminados dando click en el check de procesos terminados</p>
                                    <hr />
                                    <asp:RadioButtonList ID="rb_obciones" EnableViewState="true" runat="server" OnSelectedIndexChanged="rb_obciones_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Value="1">&nbsp&nbspAdquisicion de Broilers en proceso</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp&nbspProcesos Terminados</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--CssClass="table table-sm  table-light table-condensed table-bordered table-striped"--%>
                                    <asp:GridView ID="gv_adquisicion" DataKeyNames="ID_CompraBroilers" runat="server" OnRowCommand="gv_adquisicion_RowCommand" EnableViewState="false" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="ID_CompraBroilers" HeaderText="ID"></asp:BoundField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Granja"></asp:BoundField>
                                            <asp:BoundField DataField="ReferenciaComentario" HeaderText="Comentario"></asp:BoundField>
                                            <asp:BoundField DataField="CodLote" HeaderText="Lote"></asp:BoundField>
                                            <asp:BoundField DataField="FechaMatanza" HeaderText="Fecha Matanza"></asp:BoundField>
                                            <%--<asp:BoundField DataField="PrecioCompraxLibra" DataFormatString="{0:C2}" HeaderText="Precio Compra x Libra"></asp:BoundField>--%>
                                            <asp:BoundField DataField="TotalLibrasCompradasCalculoBascula" DataFormatString="{0:n}" HeaderText="Total Lbs Bascula"></asp:BoundField>
                                            <asp:BoundField DataField="TotalAvesRemisionCompradas" HeaderText="TotalAves x Remision"></asp:BoundField>
                                            <asp:BoundField DataField="TotalAvesConteoAutomatico" HeaderText="ContadorAutomatico(Aves)"></asp:BoundField>
                                            <asp:BoundField DataField="NombreRaza" HeaderText="Raza"></asp:BoundField>
                                            <asp:BoundField DataField="NombreMatadero" HeaderText="Matadero"></asp:BoundField>
                                            <%--<asp:BoundField DataField="CostoTotalxLibra" DataFormatString="{0:C2}" HeaderText="CostoTotal x Libra"></asp:BoundField>--%>
                                            <asp:BoundField DataField="CreadoPor" HeaderText="CreadoPor"></asp:BoundField>
                                            <asp:BoundField DataField="ACargoPEsoVivo" HeaderText="A Cargo de Peso Vivo"></asp:BoundField>
                                            <asp:BoundField DataField="FechaCreado" HeaderText="FechaCreado"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Seleccionar">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_Seleccionar" data-toggle="tooltip" title="Seleccione este Proceso para agregar Remisiones" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Seleccionar_Procesos" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-check-circle"></i>
                                                    </asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-md-6 -->
                    </div>
                    <div id="row_NuevaRemisions" visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card card-primary card-outline">
                                    <div class="card-header">
                                        <h5 class="card-title m-0">Nueva Remision</h5>
                                    </div>
                                    <div class="card-body">
                                        <h6 class="card-title">Cree una nueva Remision para la adquisición de broilers seleccionada</h6>
                                        <br />
                                        <h3>
                                            <asp:Label ID="lbl_compraSeleccionada" Text="Seleccione un proceso de la lista de arriba" runat="server" /></h3>
                                        <hr />
                                        <div class="col-12 d-flex align-items-stretch flex-column">
                                            <div class="card bg-light d-flex flex-fill">
                                                <div class="card-header border-bottom-0 text-info">
                                                    Lote seleccionado para Remision
                                                </div>
                                                <div class="card-body pt-0">
                                                    <h2 class="lead"><b>Datos Lotes seleccionado para envio de aves</b></h2>
                                                    <p class="text-muted text-sm"><b>Nota: </b>Se reducira el inventario del lote seleccionado</p>
                                                    <div class="row">
                                                        <div class="col-lg-4 col-sm-12">
                                                            <ul class="ml-4 mb-0 fa-ul text-muted">
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>TOTAL POLLO INGRESADO: <b>
                                                                    <asp:Label Text="0" ID="lbl_TotalPolloIngresado" runat="server" /></b></li>
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>TOTAL MORTALIDAD REGISTRADA: <b>
                                                                    <asp:Label Text="0" ID="lbl_mortalidadr" runat="server" /></b></li>
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>TOTAL REMISIONES: <b>
                                                                    <asp:Label Text="0" ID="lbl_TotalRemisiones" runat="server" /></b></li>
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>TOTAL POLLO EN PIE: <b>
                                                                    <asp:Label Text="0" ID="lbl_TotalPolloEnPie" runat="server" /></b></li>
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>PORCENTAJE MORTALIDAD: <b>
                                                                    <asp:Label Text="0" ID="lbl_PorcentajeMortalidad" runat="server" /></b></li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-4 col-sm-12">
                                                            <ul class="ml-4 mb-0 fa-ul text-muted">
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>*EDAD DIAS: <b>
                                                                    <asp:Label Text="0" ID="lbl_EdadDias" runat="server" /></b></li>
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>EDAD SEMANAS: <b>
                                                                    <asp:Label Text="0" ID="lbl_EdadSemanas" runat="server" /></b></li>
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>PESO PROMEDIO LOTE: <b>
                                                                    <asp:Label Text="0" ID="lbl_PesoPromedioLote" runat="server" /></b></li>
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>TOTAL LIBRAS PESADAS MATADERO: <b>
                                                                    <asp:Label Text="0" ID="lbl_TotalLibrasPesadasMatadero" runat="server" /></b></li>
                                                                <li class="small"><span class="fa-li"><i class="fas fa-lg fa-circle-notch"></i></span>FECHA INGRESO A GALERA: <b>
                                                                    <asp:Label Text="0" ID="lbl_FechaIngresoGalera" runat="server" /></b></li>
                                                            </ul>
                                                        </div>
                                                        <%--  <div class="col-4 text-center">
                                                            <img src="../imagenes/logop-full-b.png" alt="user-avatar" style="width: 40%; max-width: 40%; min-width: 100px;" class="img-circle img-fluid">
                                                        </div>--%>
                                                    </div>
                                                </div>
                                                <div class="card-footer">
                                                    <%--   <div class="text-right">
                                                        <a href="#" class="btn btn-sm bg-teal">
                                                            <i class="fas fa-comments"></i>
                                                        </a>
                                                        <a href="#" class="btn btn-sm btn-primary">
                                                            <i class="fas fa-user"></i>View Profile
                    </a>
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <div class="card card-success">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Datos generales en esta Remision</h3>
                                                        </div>
                                                        <!-- /.card-header -->
                                                        <!-- form start -->
                                                        <div class="card-body">
                                                            <div class="form-group">
                                                                <label for="txt_NombreConductor">Nombre Conductor *</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fa fa-user"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txt_NombreConductor" name="txt_NombreConductor" type="text" class="form-control" aria-describedby="txt_NombreConductorHelpBlock" />
                                                                </div>
                                                                <span id="txt_NombreConductorHelpBlock" class="form-text text-muted">Nombre del Conductor que entregara los Broilers</span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_PlacaCamion">Placa del Camion *</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fa fa-truck"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txt_PlacaCamion" name="txt_PlacaCamion" type="text" class="form-control" aria-describedby="txt_PlacaCamionHelpBlock" />
                                                                </div>
                                                                <span id="txt_PlacaCamionHelpBlock" class="form-text text-muted">Placa del camión que hará entrega de los Broilers</span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_HoraAyuno">Hora de Ayuno *</label>
                                                                <div class="input-group date" id="datetimepicker1" data-target-input="nearest">
                                                                    <div class="input-group-append" data-target="#datetimepicker1" data-toggle="datetimepicker">
                                                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txt_HoraAyuno" name="txt_HoraAyuno" type="text" aria-describedby="txt_HoraAyunoHelpBlock" class="form-control datetimepicker-input" data-target="#datetimepicker1" EnableViewState="false" />
                                                                </div>
                                                                <span id="txt_HoraAyunoHelpBlock" class="form-text text-muted">Hora de inicio de Ayuno de los Broilers -Use el boton</span>
                                                            </div>
                                                            <%--<div class="form-group">
                                                    <label for="txt_EdadDias">Edad en Dias *</label>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">
                                                                <i class="fa fa-birthday-cake"></i>
                                                            </div>
                                                        </div>
                                                        <asp:TextBox TextMode="Number" runat="server" ID="txt_EdadDias" name="txt_EdadDias" type="text" class="form-control" aria-describedby="txt_EdadDiasHelpBlock" />
                                                    </div>
                                                    <span id="txt_EdadDiasHelpBlock" class="form-text text-muted">Edad en Días de los Broilers</span>
                                                </div>--%>
                                                            <%--     <div class="form-group">
                                                    <label for="dr_Galera">Galera *</label>
                                                    <div>
                                                        <asp:DropDownList ID="dr_Galera" name="dr_Galera" class="custom-select" aria-describedby="dr_GaleraHelpBlock" runat="server" EnableViewState="true"></asp:DropDownList>
                                                        <span id="dr_GaleraHelpBlock" class="form-text text-muted">Galera donde están siendo Obtenidos los Broilers</span>
                                                    </div>
                                                </div>--%>
                                                            <div class="form-group">
                                                                <label for="txt_Destino">Destino *</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fa fa-map-marker"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txt_Destino" name="txt_Destino" type="text" class="form-control" aria-describedby="txt_DestinoHelpBlock" />
                                                                </div>
                                                                <span id="txt_DestinoHelpBlock" class="form-text text-muted">Destino donde será la Remisión de los Broilers</span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_HoraSalidaGranja">Hora Salida de Granja *</label>
                                                                <div class="input-group">
                                                                    <div class="input-group date" id="datetimepicker3" data-target-input="nearest">
                                                                        <div class="input-group-append" data-target="#datetimepicker3" data-toggle="datetimepicker">
                                                                            <div class="input-group-text"><i class="fa fa-clock-o"></i></div>
                                                                        </div>
                                                                        <asp:TextBox runat="server" ID="txt_HoraSalidaGranja" name="txt_HoraSalidaGranja" type="text" aria-describedby="txt_HoraSalidaGranjaHelpBlock" class="form-control datetimepicker-input" data-target="#datetimepicker3" EnableViewState="true" />
                                                                    </div>
                                                                </div>
                                                                <span id="txt_HoraSalidaGranjaHelpBlock" class="form-text text-muted">Hora Salida de Granja</span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_EntregaConforme">Entrega conforme *</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fa fa-user"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txt_EntregaConforme" name="txt_EntregaConforme" type="text" class="form-control" aria-describedby="txt_EntregaConformeHelpBlock" />
                                                                </div>
                                                                <span id="txt_EntregaConformeHelpBlock" class="form-text text-muted">Quien Entrega los Broilers a Chofer o Encargado.</span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_EstadoSaludAves">Estado de Salud Aves</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fa fa-heart-o"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txt_EstadoSaludAves" name="txt_EstadoSaludAves" type="text" class="form-control" aria-describedby="txt_EstadoSaludAvesHelpBlock" />
                                                                </div>
                                                                <span id="txt_EstadoSaludAvesHelpBlock" class="form-text text-muted">Estado de Salud de las Aves enviadas</span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_Nufdmadeas">Medicamento</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fa fa-medkit"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txt_Nufdmadeas" name="txt_Nufdmadeas" type="text" class="form-control" aria-describedby="txt_NufdmadeasHelpBlock" />
                                                                </div>
                                                                <span id="txt_NufdmadeasHelpBlock" class="form-text text-muted">Nombre y ultima Fecha del medicamento antes del envío a sacrificio</span>
                                                            </div>
                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                                <ProgressTemplate>
                                                                    <div class="progress">
                                                                        <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 100%">por favor espere... guardando</div>
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <div class="form-group">
                                                                <p>* La edad en Dias se restara a -1 por el tiempo de ayuno del Ave</p>
                                                                <label for="btn_GuardarNuevaRemision">Guardar Nueva Remision</label>
                                                                <asp:Button ID="btn_GuardarNuevaRemision" Text="Guardar Nueva Remision" class="btn btn-primary form-control" UseSubmitBehavior="false" CausesValidation="false" runat="server" aria-describedby="txt_button" OnClick="btn_GuardarNuevaRemision_Click" />
                                                                <span id="txt_button" class="form-text text-muted">Nombre y ultima Fecha del medicamento antes del envío a sacrificio</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <div class="card card-primary">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Aves por Java en esta Remision</h3>
                                                        </div>
                                                        <!-- /.card-header -->
                                                        <!-- form start -->
                                                        <div class="card-body">
                                                            <div class="form-group">
                                                                <label for="txt_NombreConductor">Numero de Aves</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fas fa-calculator"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" class="form-control" ID="txt_AvesporJava" placeHolder="Total Aves x Java" TextMode="Number" MaxLength="10" aria-describedby="txt_AvesporJavaHelpBlock" />
                                                                </div>
                                                                <span id="txt_AvesporJavaHelpBlock" class="form-text text-muted">Total de Aves por Java</span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_NombreConductor">Cantidad de Javas</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <div class="input-group-text">
                                                                            <i class="fas fa-box"></i>
                                                                        </div>
                                                                    </div>
                                                                    <asp:TextBox runat="server" class="form-control" ID="txt_javas" placeHolder="Numero de Javas" TextMode="Number" MaxLength="10" aria-describedby="txt_javasHelpBlock" />
                                                                </div>
                                                                <span id="txt_javasHelpBlock" class="form-text text-muted">Numero de javas que contiene esta cantidad de aves</span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="btn_AgregarAvesxJava">Agregar aves x java</label>
                                                                <asp:Button ID="btn_AgregarAvesxJava" CausesValidation="false" UseSubmitBehavior="false" Text="Agregar a la Lista" class="btn btn-info form-control" runat="server" OnClick="btn_AgregarAvesxJava_Click" aria-describedby="btn_Agregar_label" />
                                                                <span id="btn_Agregar_label" class="form-text text-muted">Agregar la distribucion de aves por java</span>
                                                            </div>
                                                            <asp:GridView ID="gv_avesxjava" DataKeyNames="ID_JavasEnviadas" CssClass="table table-sm table-bordered" runat="server" AutoGenerateColumns="false" EnableViewState="false" OnRowCommand="gv_avesxjava_RowCommand">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Cantidad_Javas" HeaderText="Cantidad Javas"></asp:BoundField>
                                                                    <asp:BoundField DataField="PollosPorJavas" HeaderText="Pollos Por Javas"></asp:BoundField>
                                                                    <asp:BoundField DataField="SubTotalAves" DataFormatString="{0:n}" HeaderText="Subtotal Aves"></asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Remover">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Eliminar el item de la base de datos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="remover" CssClass="btn btn-sm btn-warning" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> 
                                                                            </asp:LinkButton>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div class="info-box">
                                                                <span class="info-box-icon bg-info elevation-1"><i class="fas fa-flag"></i></span>
                                                                <div class="info-box-content">
                                                                    <span class="info-box-text">TOTAL AVES EN ESTE VIAJE</span>
                                                                    <span class="info-box-number text-lg">
                                                                        <asp:Label Text="0,00" ID="lbl_TotalAvesViaje" runat="server" />
                                                                    </span>
                                                                </div>
                                                                <!-- /.info-box-content -->
                                                            </div>
                                                            <!-- /.info-box -->
                                                        </div>
                                                        <!-- /.card-body -->
                                                    </div>
                                                    <!-- /.card -->
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.col-md-6 -->
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div aria-live="polite" aria-atomic="true">

                <!-- Position it -->
                <div style="position: fixed; top: 3rem; right: 0;">
                    <div class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-delay="7000">
                        <div class="toast-header bg-success">
                            <img src="../imagenes/logop-full-b.png" class="rounded mr-2" alt="ico" width="32" />
                            <strong class="mr-auto">Bootstrap</strong>
                            <small>ATENCION!!</small>
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
                <%--Toas Alert--%>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="../js/bootstrap4-toggle.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_adquisicion.ClientID%>").footable();
                $("#<%=gv_avesxjava.ClientID%>").footable();
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
                    $("#<%=gv_adquisicion.ClientID%>").footable();
                    $("#<%=gv_avesxjava.ClientID%>").footable();
                    //------------------------------------------    


                    $('#datetimepicker3').datetimepicker({
                        format: 'LT',
                        locale: 'es-NI'
                    });
                    $('#datetimepicker1').datetimepicker({
                        locale: 'es-NI'
                    });
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
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker3').datetimepicker({
                format: 'LT',
                locale: 'es-NI'
            });
        });
        $(function () {
            $('#datetimepicker1').datetimepicker({
                locale: 'es-NI'
            });
        });
    </script>
</asp:Content>
