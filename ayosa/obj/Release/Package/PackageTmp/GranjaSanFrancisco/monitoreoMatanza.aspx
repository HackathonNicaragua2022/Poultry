<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="monitoreoMatanza.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.monitoreoMatanza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Monitoreo Matanza y peso Vivo</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <style>
        .table-bordered th, .table-bordered td {
            border: 1px solid #ddd !important;
        }
    </style>
    <%--CSS Necesario para tener una impresion perfevta--%>
    <style>
        * {
            -webkit-print-color-adjust: exact !important; /* Chrome, Safari, Edge */
            /*-webkit-filter: drop-shadow(4px 4px 1px #ccc);*/
            color-adjust: exact !important; /*Firefox*/
            /*-webkit-transform: translate3d(0,0,0) !important;*/
            /*-webkit-filter: blur(0);*/
            /*-webkit-filter:opacity(1);*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Monitoreo de Remisiones y Peso Vivo</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Planta Procesadora</a></li>
                        <li class="breadcrumb-item active">Monitoreo Matanza</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <%--<asp:UpdatePanel ID="up_1" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">--%>
                    <%--<ContentTemplate>--%>
                    <asp:LinkButton ID="btn_mostrarSeleccion" Visible="false" runat="server" CssClass="btn btn-link" OnClick="btn_mostrarSeleccion_Click">Mostrar Panel de Seleccion de Procesos</asp:LinkButton>
                    <div class="callout callout-info" runat="server" id="div_seleccionP">
                        <h5><i class="fas fa-check"></i>Seleccionar Ingreso:</h5>
                        Seleccione el ingreso que desea ver o Monitoriar
                                <!-- /.row -->
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:RadioButtonList ID="rb_obciones" EnableViewState="true" runat="server" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="1">&nbsp;&nbsp;En proceso</asp:ListItem>
                                    <asp:ListItem Value="2">&nbsp;&nbsp;Procesos Terminados</asp:ListItem>
                                </asp:RadioButtonList>
                                <%--CssClass="table table-sm  table-light table-condensed table-bordered table-striped"--%>
                                <asp:GridView ID="gv_adquisicion" DataKeyNames="ID_CompraBroilers" runat="server" OnRowCommand="gv_adquisicion_RowCommand" EnableViewState="false" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="ID_CompraBroilers" HeaderText="ID"></asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Granja"></asp:BoundField>
                                        <asp:BoundField DataField="ReferenciaComentario" HeaderText="Comentarios"></asp:BoundField>
                                        <asp:BoundField DataField="CodLote" HeaderText="Lote"></asp:BoundField>
                                        <asp:BoundField DataField="FechaMatanza" HeaderText="Fecha Matanza"></asp:BoundField>
                                        <asp:BoundField DataField="PrecioCompraxLibra" DataFormatString="{0:C2}" HeaderText="Precio Compra x Libra"></asp:BoundField>
                                        <asp:BoundField DataField="TotalLibrasCompradasCalculoBascula" DataFormatString="{0:n}" HeaderText="Total Lbs Bascula"></asp:BoundField>
                                        <asp:BoundField DataField="TotalAvesRemisionCompradas" HeaderText="TotalAves x Remision"></asp:BoundField>
                                        <asp:BoundField DataField="TotalAvesConteoAutomatico" HeaderText="ContadorAutomatico(Aves)"></asp:BoundField>
                                        <asp:BoundField DataField="NombreRaza" HeaderText="Raza"></asp:BoundField>
                                        <asp:BoundField DataField="NombreMatadero" HeaderText="Matadero"></asp:BoundField>
                                        <asp:BoundField DataField="CostoTotalxLibra" DataFormatString="{0:C2}" HeaderText="CostoTotal x Libra"></asp:BoundField>
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

                                <asp:LinkButton ID="btn_ocultar" runat="server" CssClass="btn btn-link" OnClick="btn_ocultar_Click">Ocultar este Panel</asp:LinkButton>
                            </div>
                            <!-- /.col-md-6 -->
                        </div>
                    </div>
                    <%--   </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <%--<asp:UpdatePanel ID="up_monitoreo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>--%>
                    <%--TIEMPO PARA RECARGAR LOS CONTENIDOS DEL MONITOREO--%>
                    <%--<asp:Timer ID="tm_monitoreo" runat="server" Interval="10000" OnTick="tm_monitoreo_Tick"></asp:Timer>--%>
                    <asp:Button Text="Actualizar Datos" CssClass="btn btn-info" OnClick="Unnamed_Click" runat="server" UseSubmitBehavior="false" CausesValidation="false" />
                    <hr />
                    <!-- Main content -->
                    <div class="invoice p-3 mb-3" id="impresionReporte">
                        <!-- title row -->
                        <div class="row">
                            <div class="col-12">
                                <h4>
                                    <i class="fas fa-car"></i>&nbsp Remisiones y Peso Vivo                   
                                    <small class="float-right">Fecha Reporte:
                                        <asp:Label Text="fecha" ID="lbl_FechaReporte" runat="server" />
                                    </small>
                                </h4>
                            </div>
                            <!-- /.col -->
                        </div>
                        <hr />
                        <!-- info row -->
                        <div class="row invoice-info">
                            <div class="col-sm-4 invoice-col">
                                <div class="small-box bg-info" style="height=100%;">
                                    <div class="inner">
                                        <h3>
                                            <asp:Label Text="0" ID="lbl_AvesConteoAutomatico" runat="server" /></h3>

                                        <p>Aves Contadas automaticamente</p>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-clock"></i>
                                    </div>
                                    <a href="ConteoPollos.aspx" class="small-box-footer">Conteo Automatico<i class="fas fa-arrow-circle-right"></i></a>
                                </div>
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-4 invoice-col">
                                Detalles Ingreso                 
                                <address>
                                    <%--<strong>John Doe</strong><br>--%>
                                    <b>Ingreso Codigo:&nbsp</b>
                                    <asp:Label Text="0" ID="lbl_IDCompra" runat="server" /><br>
                                    <br>
                                    <b>Granja:&nbsp</b><asp:Label Text="NombreGranja" ID="lbl_NombreGranja" runat="server" /><br>
                                    <b>Número de Referencia:&nbsp</b>
                                    <asp:Label Text="0" ID="lbl_NumeroReferencia" runat="server" /><br>
                                    <b>Número de Lote:&nbsp</b><asp:Label Text="#Lote" ID="lbl_NumeroLote" runat="server" /><br>
                                    <b>Fecha Matanza:&nbsp</b>
                                    <asp:Label Text="Fecha" ID="lbl_FechaMatanza" runat="server" /><br>
                                    <b>Precio Compra por Libra:&nbsp</b>
                                    <asp:Label Text="Fecha" ID="lbl_PrecioCompraxLibra" runat="server" /><br>
                                </address>
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-4 invoice-col">
                                <b>Total Libras:&nbsp</b>
                                <asp:Label Text="Libras" ID="lbl_TotalLibras" runat="server" /><br>
                                <b>Total Aves por Remision:&nbsp</b>
                                <asp:Label Text="Libras" ID="lbl_AvesxRemision" runat="server" /><br>
                                <b>Raza Broilers:&nbsp</b>
                                <asp:Label Text="Raza" ID="lbl_RazaBroilers" runat="server" /><br>
                                <b>Proceso Terminado?:&nbsp</b>
                                <asp:Label Text="Si/No" ID="lbl_enProceso" runat="server" /><br>
                                <br>
                                <div class="info-box">
                                    <span class="info-box-icon bg-info"><i class="far fa-money-bill-alt"></i></span>

                                    <div class="info-box-content">
                                        <span class="info-box-text">TOTAL COSTO LIBRAS</span>
                                        <span class="info-box-number text-lg">
                                            <asp:Label Text="C$ 0.00" ID="lbl_TotalCostoLibras" runat="server" /></span>
                                    </div>
                                    <!-- /.info-box-content -->
                                </div>
                                <!-- /.info-box -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                        <br />
                        <!-- Table row -->
                        <div class="row">
                            <div class="col-12 table-responsive">
                                <asp:GridView ID="gv_Remisiones" DataKeyNames="ID_ViajesRemision" CssClass="table table-sm  table-light table-condensed  table-bordered table-striped" runat="server" OnRowCommand="gv_Remisiones_RowCommand" EnableViewState="false" AutoGenerateColumns="false" OnRowDataBound="gv_Remisiones_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="NumeroViaje" HeaderText="Numero Viaje">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" ControlStyle-CssClass="align-middle">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreConductor" HeaderText="Nombre Conductor">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PlacaCamion" HeaderText="PlacaCamion">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total_javas" DataFormatString="{0:n}" HeaderText="Total javas">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HoraAyuno" HeaderText="Hora Ayuno">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Edad" HeaderText="Edad">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NumeroGalera" HeaderText="Numero Galera">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Destino" HeaderText="Destino">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalAvesEnviadas" DataFormatString="{0:n}" HeaderText="TotalAves Enviadas">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HoraSalidaGranja" DataFormatString="{0:hh:mm tt}" HeaderText="Hora Salida Granja">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HoraLlegadaPlanta" DataFormatString="{0:hh:mm tt}" HeaderText="Hora Llegada Planta">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EntregaConforme" HeaderText="Entrega Conforme">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreadoPor" HeaderText="Recibido Por">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EstadoSaludAve" HeaderText="Estado Salud Ave">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalLibrasxRemision" HeaderText="Total Libras" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NUFDMADEAS" HeaderText="NUFDMADEAS">
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="JAVAS ENVIADAS">
                                            <ItemTemplate>
                                                <div id="gvjavasEnviadas<%# Eval("ID_ViajesRemision") %>">
                                                    <asp:GridView
                                                        ID="gv_javasEnviadas"
                                                        runat="server"
                                                        AutoGenerateColumns="false"
                                                        DataKeyNames="ID_JavasEnviadas"
                                                        CssClass="table table-sm table-success table-condensed  table-bordered table-striped">
                                                        <Columns>
                                                            <asp:BoundField DataField="Cantidad_Javas" HeaderText="Cantidad de Javas">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PollosPorJavas" HeaderText="Pollos Por Java">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SubTotalAves" HeaderText="Subtotal Aves">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PESAJES DE REMISION">
                                            <ItemTemplate>
                                                <%-- <a href="JavaScript:divexpandcollapse('div<%# Eval("ID_ViajesRemision") %>');">
                                                            <img alt="Details" id="imgdiv<%# Eval("ID_ViajesRemision") %>" src="../imagenes/plus.png" />
                                                        </a>--%>
                                                <!-- Adding the Div container -->
                                                <div id="div<%# Eval("ID_ViajesRemision") %>">
                                                    <%--style="display: none;"--%>
                                                    <!-- Adding Child GridView -->
                                                    <asp:GridView
                                                        ID="gv_Pesajes"
                                                        runat="server"
                                                        AutoGenerateColumns="false"
                                                        DataKeyNames="ID_IngresoPesoVivo"
                                                        CssClass="table table-sm table-info table-condensed  table-bordered table-striped">
                                                        <Columns>
                                                            <asp:BoundField DataField="NumPesaje" HeaderText="Numero Pesaje">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CantidadJavasPesadas" HeaderText="Cantidad Javas">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PollosxJava" HeaderText="Pollos x Java">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TotalPollos" HeaderText="Total Pollos">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PesoJavaConPollo_Libras" HeaderText="PesoJavaConPollo_Libras" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PesoxJavaVacia_libDefault" HeaderText="PesoxJava x Vacia" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PesoNetoPollosLibra" HeaderText="Peso Neto Pollos Libra" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PesoPromedioxPollo_lib" HeaderText="Peso Promedio" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                        <div style="break-after: page"></div>
                        <%--ARA QUE ESTE PIE DE PAGINA SIEMPRE APAREZCA EN UNA NUEVA HOJA--%>
                        <div class="row">
                            <!-- accepted payments column -->
                            <div class="col-md-6 col-sm-12">
                                <p class="lead">PESO PROMEDIO POR REMISION</p>
                                <p class="text-muted well well-sm shadow-none" style="margin-top: 10px;">
                                    Pesos promedios entre remisiones
                                </p>
                                <!-- Line chart -->
                                <div class="card card-primary card-outline">
                                    <div class="card-header">
                                        <h3 class="card-title"><i class="far fa-chart-bar"></i>Pesos promedios </h3>

                                        <div class="card-tools">
                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                <i class="fas fa-minus"></i>
                                            </button>
                                            <button type="button" class="btn btn-tool" data-card-widget="remove">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div id="line-chart" style="height: 300px;"></div>
                                    </div>
                                    <!-- /.card-body-->
                                </div>
                                <!-- /.card -->
                            </div>
                            <!-- /.col -->
                            <div class="col-md-6 col-sm-12">
                                <p class="lead">RESUMEN DE INGRESO DE PESO VIVO</p>

                                <div class="table-responsive">
                                    <table class="table">
                                        <tr>
                                            <th style="width: 50%">PESO PROMEDIO</th>
                                            <td>
                                                <asp:Label Text="0.00" ID="lbl_resumenPesoPromedio" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <th>LIBRAS TOTAL</th>
                                            <td>
                                                <asp:Label Text="0.00" ID="lbl_resumenLibrasTotal" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <th>TOTAL POLLOS ENVIADOS</th>
                                            <td>
                                                <asp:Label Text="0.00" ID="lbl_resumen_PollosProcesados" runat="server" /></td>
                                        </tr>
                                         <tr>
                                            <th>TOTAL POLLOS CONTADOS (AUT)</th>
                                            <td>
                                                <asp:Label Text="0.00" ID="lbl_totalPollosConteoAuto" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <th>TOTAL JAVAS PESADAS</th>
                                            <td>
                                                <asp:Label Text="0.00" ID="lbl_resumen_TotalJavasPesadas" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <th>TOTAL REMISIONES</th>
                                            <td>
                                                <asp:Label Text="0.00" ID="lbl_resumen_TotalRemisiones" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <th>TOTAL PESAJES BÁSCULA</th>
                                            <td>
                                                <asp:Label Text="0.00" ID="lbl_resumen_TotalPesajesBascula" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <th>PRECICION DEL CONTADOR*</th>
                                            <td>
                                                <asp:Label Text="0.00" ID="lbl_PresicionContador" runat="server" /></td>
                                        </tr>
                                    </table>
                                    <small class="small text-muted">El resumen esta Calculado en Base a las remisiones y los pesos obtenidos de la bascula, de esa manera podra comparar con los datos introducidos en la adquisicion de los Broilers
                                    </small>
                                    <small class="small text-muted">*Porcentaje de precisión del contador en relacion a la informacion enviada en la remision y calculo en relacion con el pesaje en la bascula. NOTA: Algunos factores de momento pueden interferir en el conteo automatico tales como patas muy juntas, plumas obtrullendo el sensor infrarojo entre otros. </small>
                                </div>
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->

                        <!-- this row will not appear when printing -->
                        <div class="row no-print">
                            <div class="col-12">
                                <button type="button" class="btn btn-primary float-right" style="margin-right: 5px;" onclick="printContent('impresionReporte');">
                                    <i class="fas fa-print"></i>&nbsp Imprimir Reporte              
                                </button>
                            </div>
                        </div>
                    </div>
                    <!-- /.invoice -->
                    <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
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
                            <%--   <asp:UpdatePanel runat="server">
                                <ContentTemplate>--%>
                            <asp:Label Text="" ID="lbl_Toast" runat="server" />
                            <%--   </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
                <%--Toas Alert--%>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
      <!-- FLOT CHARTS -->
    <script src="../AdminTemplate/plugins/flot/jquery.flot.js"></script>
    <!-- FLOT RESIZE PLUGIN - allows the chart to redraw when the window is resized -->
    <script src="../AdminTemplate/plugins/flot/plugins/jquery.flot.resize.js"></script>
    <!-- FLOT PIE PLUGIN - also used to draw donut charts -->
    <script src="../AdminTemplate/plugins/flot/plugins/jquery.flot.pie.js"></script>

    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="../js/bootstrap4-toggle.min.js"></script>
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

        /*
                 * LINE CHART
                 * ----------
                 */
        //LINE randomly generated data
        var pesaje=<%=PromedioPesos%>;
        //var sin = [],
        //    cos = []
        //for (var i = 0; i < 4; i += 0.5) {
        //    sin.push([i, Math.sin(i)])
        //    cos.push([i, Math.cos(i)])
        //}
        var line_data1 = {
            //data: sin,
            label: "Remision= ",
            data:pesaje,
            color: '#3c8dbc'
        }
        //var line_data2 = {
        //    data: cos,
        //    color: '#00c0ef'
        //}
        //$.plot('#line-chart', [line_data1, line_data2], {
        $.plot('#line-chart', [line_data1], {
            grid: {
                hoverable: true,
                borderColor: '#f3f3f3',
                borderWidth: 1,
                tickColor: '#f3f3f3',
                            
            },
            series: {
                shadowSize: 0,
                lines: {
                    show: true
                },
                points: {
                    show: true
                }
            },
            lines: {
                fill: true,
                color: ['#3c8dbc', '#f56954']
            },
            yaxis: {
                show: true
            },
            xaxis: {
                show: true
            }
        })
        //Initialize tooltip on hover
        $('<div class="tooltip-inner" id="line-chart-tooltip"></div>').css({
            position: 'absolute',
            display: 'none',
            opacity: 0.8
        }).appendTo('body')
        $('#line-chart').bind('plothover', function (event, pos, item) {

            if (item) {
                var x = item.datapoint[0].toFixed(2),
                    y = item.datapoint[1].toFixed(2)

                $('#line-chart-tooltip').html(item.series.label + x + ', Peso Promedio= ' + y)
                  .css({
                      top: item.pageY + 5,
                      left: item.pageX + 5
                  })
                  .fadeIn(200)
            } else {
                $('#line-chart-tooltip').hide()
            }

        })
        /* END LINE CHART */  

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    /*
                   * LINE CHART
                   * ----------
                   */
                    //LINE randomly generated data
                    var pesaje=<%=PromedioPesos%>;
                    //var sin = [],
                    //    cos = []
                    //for (var i = 0; i < 4; i += 0.5) {
                    //    sin.push([i, Math.sin(i)])
                    //    cos.push([i, Math.cos(i)])
                    //}
                    var line_data1 = {
                        //data: sin,
                        label: "Remision= ",
                        data:pesaje,
                        color: '#3c8dbc'
                    }
                    //var line_data2 = {
                    //    data: cos,
                    //    color: '#00c0ef'
                    //}
                    //$.plot('#line-chart', [line_data1, line_data2], {
                    $.plot('#line-chart', [line_data1], {
                        grid: {
                            hoverable: true,
                            borderColor: '#f3f3f3',
                            borderWidth: 1,
                            tickColor: '#f3f3f3',
                            
                        },
                        series: {
                            shadowSize: 0,
                            lines: {
                                show: true
                            },
                            points: {
                                show: true
                            }
                        },
                        lines: {
                            fill: true,
                            color: ['#3c8dbc', '#f56954']
                        },
                        yaxis: {
                            show: true
                        },
                        xaxis: {
                            show: true
                        }
                    })
                    //Initialize tooltip on hover
                    $('<div class="tooltip-inner" id="line-chart-tooltip"></div>').css({
                        position: 'absolute',
                        display: 'none',
                        opacity: 0.8
                    }).appendTo('body')
                    $('#line-chart').bind('plothover', function (event, pos, item) {

                        if (item) {
                            var x = item.datapoint[0].toFixed(2),
                                y = item.datapoint[1].toFixed(2)

                            $('#line-chart-tooltip').html(item.series.label + x + ', Peso Promedio= ' + y)
                              .css({
                                  top: item.pageY + 5,
                                  left: item.pageX + 5
                              })
                              .fadeIn(200)
                        } else {
                            $('#line-chart-tooltip').hide()
                        }

                    })
                    /* END LINE CHART */                                        
                }
            });
        };
    </script>


    <script>
        //$(function () {
        //    var Toast = Swal.mixin({
        //        toast: true,
        //        position: 'top-end',
        //        showConfirmButton: false,
        //        timer: 3000
        //    });
        //    function Toastinfo() {
        //        Toast.fire({
        //            icon: 'success',
        //            title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
        //        })
        //    }
        //});
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_adquisicion.ClientID%>").footable();
                $("#<%=gv_Remisiones.ClientID%>").footable();
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
                    $("#<%=gv_Remisiones.ClientID%>").footable();
                    //------------------------------------------                                          
                }
            });
        };
        function ShowtoastMessage() {
            $('.toast').toast('show');
        }
    </script>

    <script type="text/javascript">
        function divexpandcollapse(divname) {
            var img = "img" + divname;
            if ($("#" + img).attr("src") == "images/plus.png") {
                $("#" + img)
                    .closest("tr")
                    .after("<tr><td></td><td colspan ='100%'>" + $("#" + divname)
                    .html() + "</td></tr>")
                $("#" + img).attr("src", "../imagenes/minus.png");
            } else {
                $("#" + img).closest("tr").next().remove();
                $("#" + img).attr("src", "../imagenes/plus.png");
            }
        }


        $(document).ready(function () {
            $('.counter-value').each(function () {
                $(this).prop('Counter', 0).animate({
                    Counter: $(this).text()
                }, {
                    duration: 3500,
                    easing: 'swing',
                    step: function (now) {
                        $(this).text(Math.ceil(now));
                    }
                });
            });
        });
    </script>
</asp:Content>
