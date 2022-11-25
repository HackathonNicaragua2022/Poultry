<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="IngresoLote.aspx.cs" Inherits="POULTRY.GranjaSanFrancisco.IngresoLote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ingreso de Lote</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Ingreso de Nuevo Lote</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="homeGranja.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Ingreso Nuevo Lote</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up_principal" runat="server">
        <ProgressTemplate>
            <div class="progress">
                <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">Cargando...</div>
            </div>
            <br />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up_principal" runat="server">
        <ContentTemplate>
            <!-- Main content -->
            <section class="content">

                <!-- Default box -->
                <div class="card">
                    <div class="card-header">
                        <h3 class="card-title">Detalles del Lote</h3>

                        <div class="card-tools">
                            <div>
                                <asp:CheckBox Text="&nbsp&nbsp Usar 2 % Adicional" ID="chk_dosPorciento" Checked="true" CssClass="custom-checkbox" runat="server" EnableViewState="true" />
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4 col-lg-3 col-sm-12">
                                <label for="txt_NumeroFactura">Numero Factura</label>
                                <asp:TextBox runat="server" ID="txt_NumeroFactura" CssClass="form-control border-info" MaxLength="150" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-lg-4 col-sm-12">
                                <label for="dr_Granja">Granja</label>
                                <asp:DropDownList ID="dr_Granja" CssClass="form-control dropdown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dr_Granja_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-4 col-lg-4 col-sm-12">
                                <label for="dr_Galeras">Galeras Disponibles</label>
                                <asp:DropDownList ID="dr_Galeras" CssClass="form-control dropdown" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div id="galerasEnProduccion" runat="server" visible="false">
                            <h2 class="text-warning">
                                <i class="fas fa-building"></i>&nbsp&nbsp Todas las Galeras de esta Granja se encuentran en produccion, debe esperar a que se desocupe una galera o bien  cancelar el proceso de una de ellas para ingresar un nuevo lote
                            </h2>
                            <asp:Image ImageUrl="~/imagenes/galera.svg" runat="server" />
                        </div>
                        <div class="row" runat="server" id="row_principal">
                            <%--para poder ocultar todo el contenido de ser necesario--%>
                            <div class="col-12 col-md-12 col-lg-8 order-2 order-md-1">
                                <h3 class="text-primary"><i class="fas fa-database"></i>&nbsp&nbsp Datos de Aves Recibidas</h3>
                                <p class="text-muted">Datos del Nuevo lote de ingreso</p>
                                <br />
                                <div class="row">
                                    <div class="col-md-4 col-lg-4 col-sm-12">
                                        <label for="dr_Proveedor">Proveedor Aves</label>
                                        <asp:DropDownList ID="dr_Proveedor" CssClass="form-control dropdown" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 col-lg-4 col-sm-12">
                                        <label for="dr_RazaAve">Raza Aves</label>
                                        <asp:DropDownList ID="dr_RazaAve" CssClass="form-control dropdown" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 col-lg-4 col-sm-12">
                                        <label for="dr_RazaAve">Fecha entrada Glr</label>
                                        <asp:TextBox runat="server" ID="txt_FechaEntradaGalera" CssClass="form-control" TextMode="Date" OnTextChanged="txt_FechaEntradaGalera_TextChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-lg-4 col-sm-12">
                                        <label for="txt_FechaLiquidar">Fecha para Liquidar</label>
                                        <asp:TextBox runat="server" ID="txt_FechaLiquidar" Enabled="false" CssClass="form-control bg-white border-info" />
                                        <p class="lead">Fecha ideal para liquidar lote, basado en la raza del ave seleccionada</p>
                                    </div>
                                </div>
                                <br />
                                <div class="callout callout-info">
                                    <h3 class="text-primary">Factura de Broilers de Engorde
                                    </h3>
                                    <div class="row">
                                        <div class="col-md-4 col-lg-3 col-sm-12">
                                            <label for="txt_TotalAvesCompradas">Cantidad Aves</label>
                                            <asp:TextBox runat="server" ID="txt_TotalAvesCompradas" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-3 col-sm-12">
                                            <label for="txt_PorcentajeExtra">Bonificacion</label>
                                            <asp:TextBox runat="server" ID="txt_PorcentajeExtra" Text="0" Enabled="false" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-3 col-sm-12">
                                            <label for="txt_TotalRecibido">Neto Factura</label>
                                            <asp:TextBox runat="server" ID="txt_TotalRecibido" Enabled="false" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-6 col-sm-12">
                                            <label for="txt_ConteoFisicoFac">Conteo Fisico</label>
                                            <asp:TextBox runat="server" ID="txt_ConteoFisicoFac" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-6 col-sm-12">
                                            <label for="txt_DiferenciaFactura">Diferencia Factura</label>
                                            <asp:TextBox runat="server" ID="txt_DiferenciaFactura" Enabled="false" CssClass="form-control" TextMode="Number" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="callout callout-warning">
                                    <h3 class="text-primary">Revicion en Granja</h3>
                                    <div class="row">
                                        <p class="lead">La diferencia es basado en conteo físico contra total en factura más Bonificacion incluye la mortalidad</p>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4 col-lg-3 col-sm-12">
                                            <label for="txt_ConteoFisico">Conteo Fisico</label>
                                            <asp:TextBox runat="server" ID="txt_ConteoFisico" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-3 col-sm-12">
                                            <label for="txt_MortalidadRecibida">Mortalidad Recibida</label>
                                            <asp:TextBox runat="server" ID="txt_MortalidadRecibida" CssClass="form-control" TextMode="Number" />
                                        </div>

                                        <div class="col-md-4 col-lg-3 col-sm-12">
                                            <label for="txt_DiferenciaFactura">Cantidad Inicial Galera</label>
                                            <asp:TextBox runat="server" ID="txt_CantidadInicialGalera" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-3 col-sm-12">
                                            <label for="txt_verificadoPor">Conteo Verificado Por:</label>
                                            <asp:TextBox runat="server" ID="txt_verificadoPor" CssClass="form-control" MaxLength="150" />
                                        </div>
                                    </div>
                                </div>

                                <br />
                                <div class="callout callout-success">
                                    <h3 class="text-primary">Costes Factura</h3>
                                    <div class="row">
                                        <div class="col-md-4 col-lg-4 col-sm-12">
                                            <label for="txt_CostoPorAveUSD">Costo Ave Und (USD)</label>
                                            <asp:TextBox runat="server" ID="txt_CostoPorAveUSD" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-4 col-sm-12">
                                            <label for="txt_EquivalenteNIO">En NIO</label>
                                            <asp:TextBox runat="server" ID="txt_EquivalenteNIO" Enabled="false" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-4 col-sm-12">
                                            <label for="txt_PorcentajeExtra">Taza BCN</label>
                                            <asp:TextBox runat="server" ID="txt_tazaCambio"  CssClass="form-control" TextMode="Number" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4 col-lg-4 col-sm-12">
                                            <label for="txt_CostoTotalUSD">Costo Total (USD)</label>
                                            <asp:TextBox runat="server" ID="txt_CostoTotalUSD" CssClass="form-control" Enabled="false" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-4 col-sm-12">
                                            <label for="txt_EquivalenteNIO">Costo Total (NIO)</label>
                                            <asp:TextBox runat="server" ID="txt_CostoTotalNIO" Enabled="false" CssClass="form-control" TextMode="Number" />
                                        </div>

                                    </div>
                                    <br />
                                </div>
                                <br />
                                <div class="callout callout-warning">
                                    <h3 class="text-warning">Parametros Iniciales</h3>
                                    <div class="row">
                                        <div class="col-md-4 col-lg-4 col-sm-12">
                                            <label for="txt_TemperaturaInicial">Temperatura Inicial  (&nbsp;°C)</label>
                                            <asp:TextBox runat="server" ID="txt_TemperaturaInicial" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-4 col-lg-4 col-sm-12">
                                            <label for="txt_CantidadAvesPesadasPesoProme">Cant Aves pesadas</label>
                                            <asp:TextBox runat="server" ID="txt_CantidadAvesPesadasPesoProme" CssClass="form-control" TextMode="Number" />
                                        </div>
                                        <div class="col-md-4 col-lg-4 col-sm-12">
                                            <label for="txt_PesoPromedioAve">Peso Promedio (Gramos)</label>
                                            <asp:TextBox runat="server" ID="txt_PesoPromedioAve" CssClass="form-control" TextMode="Number" />
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div class="col-12 col-md-12 col-lg-4 order-1 order-md-2">
                                <h3 class="text-primary"><i class="fas fa-user"></i>&nbsp&nbsp Personal Presente</h3>
                                <p class="text-muted">Personas que estuvieron durante el recibo de ave.</p>
                                <br />
                                <div class="form-group">
                                    <label for="txt_NombrePersonal">Nombre Personal</label>
                                    <asp:TextBox runat="server" class="form-control" ID="txt_NombrePersonal" placeholder="Nombre" />
                                </div>
                                <div class="form-group">
                                    <label for="txt_CargoEnRecibo">Cargo Durante Recibo</label>
                                    <asp:TextBox runat="server" class="form-control" ID="txt_CargoEnRecibo" placeholder="Cargo en Recibo" />
                                </div>
                                <div class="form-group">
                                    <label for="txt_CargoDuranteCrecimiento">Cargo Durante Crecimiento</label>
                                    <asp:TextBox runat="server" class="form-control" ID="txt_CargoDuranteCrecimiento" placeholder="Nombre" />
                                </div>
                                <div runat="server" id="alertaIngresoPersonal">
                                </div>
                                <div class="form-group">
                                    <asp:Button Text="Agregar" ID="ID_AgregarPersonaRecibo" runat="server" CssClass="btn btn-success" UseSubmitBehavior="false" CausesValidation="false" OnClick="ID_AgregarPersonaRecibo_Click" />
                                </div>
                                <asp:Repeater ID="rp_personalPrecente" runat="server" OnItemCommand="rp_personalPrecente_ItemCommand">
                                    <HeaderTemplate>
                                        <table id="tbl_FechasCancelacion" class="table table-bordered table-hover table-responsive-sm bg-white" data-show-toggle="false" data-expand-first="true" style="border-collapse: collapse">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Nombre</th>
                                                    <th>Cargo Recibo</th>
                                                    <th>Cargo Crecimiento</th>
                                                    <th>Eliminar</th>
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
                                                <h5><%#Eval("NombrePersona")%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><%#Eval("FuncionDuranteRecibo")%></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <h5><b><%#Eval("FuncionDuranteCrecimiento")%> </b></h5>
                                            </td>
                                            <td style="width: 1px;">
                                                <asp:LinkButton ID="btn_Eliminar" CommandName="cmd_Eliminar" CommandArgument='<%#Eval("ID_PersonalEnEntrada")%>' CssClass="btn btn-danger form-control" runat="server"><i class="fa fa-solid fa-trash"></i></asp:LinkButton>
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

                    </div>
                    <!-- /.card-body -->
                </div>
                <!-- /.card -->

            </section>
            <div id="alerta_Ingreso" runat="server">
            </div>
            <div class="callout callout-info">
                <h5>Guardar Nuevo Ingreso de Lote</h5>
                <p>
                    Ingresar un nuevo lote pone en produccion la galera y crea un inventario para la misma
                </p>
                <asp:Button Text="GUARDAR INGRESO DE LOTE" UseSubmitBehavior="false" CausesValidation="false" ID="btn_IngresoLote" CssClass="btn btn-info btn-lg" runat="server" OnClick="btn_IngresoLote_Click" />
            </div>
            <!-- /.content -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script>
        document.getElementById('<%=txt_TotalAvesCompradas.ClientID%>').onkeyup = dosPorciento;
        document.getElementById('<%=txt_CostoPorAveUSD.ClientID%>').onkeyup = costos;
        document.getElementById('<%=txt_MortalidadRecibida.ClientID%>').onkeyup = cantidadInicial;
        document.getElementById('<%=txt_ConteoFisicoFac.ClientID%>').onkeyup = diferencia;
        document.getElementById('<%=txt_ConteoFisico.ClientID%>').onkeyup = cantidadInicial;
        function dosPorciento() {
            var dosPorcientoChk = document.getElementById('<%=chk_dosPorciento.ClientID%>');
            var cantidad = document.getElementById('<%=txt_TotalAvesCompradas.ClientID%>').value;

            var cantidadExtra = document.getElementById('<%=txt_PorcentajeExtra.ClientID%>');
            var totalRecibido = document.getElementById('<%=txt_TotalRecibido.ClientID%>');
            var cantidadPollos = cantidad;
            if (dosPorcientoChk.checked == true) {
                cantidad = (cantidadPollos * 0.02);
                cantidadExtra.value = cantidad;
                totalRecibido.value = (parseFloat(cantidad) + parseFloat(cantidadPollos));
            } else {
                totalRecibido.value = parseFloat(cantidad);
            }
        };
        function diferencia() {
            var conteoFisico = document.getElementById('<%=txt_ConteoFisicoFac.ClientID%>').value;
            var totalFactura = document.getElementById('<%=txt_TotalRecibido.ClientID%>').value;

            var diferencia = Math.abs(parseInt(totalFactura) - parseInt(conteoFisico));
            document.getElementById('<%=txt_DiferenciaFactura.ClientID%>').value = diferencia;
            document.getElementById('<%=txt_ConteoFisico.ClientID%>').value = conteoFisico;
            cantidadInicial();
        }
        function cantidadInicial() {
            var conteoFisico = document.getElementById('<%=txt_ConteoFisico.ClientID%>').value;
            var MortalidadRecibida = document.getElementById('<%=txt_MortalidadRecibida.ClientID%>').value;
            var NetoGalera = parseInt(conteoFisico) - parseInt(MortalidadRecibida);
            document.getElementById('<%=txt_CantidadInicialGalera.ClientID%>').value = NetoGalera;
        }
        function costos() {
            var taza = parseFloat(document.getElementById('<%=txt_tazaCambio.ClientID%>').value);
            var costoPorUnd = parseFloat(document.getElementById('<%=txt_CostoPorAveUSD.ClientID%>').value);
            var cantidad = document.getElementById('<%=txt_TotalAvesCompradas.ClientID%>').value;

            document.getElementById('<%=txt_EquivalenteNIO.ClientID%>').value = parseFloat(costoPorUnd) * parseFloat(taza);
            document.getElementById('<%=txt_CostoTotalUSD.ClientID%>').value = parseFloat(cantidad) * parseFloat(costoPorUnd);
            document.getElementById('<%=txt_CostoTotalNIO.ClientID%>').value = (parseFloat(cantidad) * parseFloat(costoPorUnd)) * parseFloat(taza);
        }



        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {

                if (sender._postBackSettings.panelsToUpdate != null) {
                    document.getElementById('<%=txt_TotalAvesCompradas.ClientID%>').onkeyup = dosPorciento;
                    document.getElementById('<%=txt_CostoPorAveUSD.ClientID%>').onkeyup = costos;
                    document.getElementById('<%=txt_MortalidadRecibida.ClientID%>').onkeyup = cantidadInicial;
                    document.getElementById('<%=txt_ConteoFisicoFac.ClientID%>').onkeyup = diferencia;
                    document.getElementById('<%=txt_ConteoFisico.ClientID%>').onkeyup = cantidadInicial;
                    function dosPorciento() {
                        var dosPorcientoChk = document.getElementById('<%=chk_dosPorciento.ClientID%>');
                        var cantidad = document.getElementById('<%=txt_TotalAvesCompradas.ClientID%>').value;

                        var cantidadExtra = document.getElementById('<%=txt_PorcentajeExtra.ClientID%>');
                        var totalRecibido = document.getElementById('<%=txt_TotalRecibido.ClientID%>');
                        var cantidadPollos = cantidad;
                        if (dosPorcientoChk.checked == true) {
                            cantidad = (cantidadPollos * 0.02);
                            cantidadExtra.value = cantidad;
                            totalRecibido.value = (parseFloat(cantidad) + parseFloat(cantidadPollos));
                        } else {
                            totalRecibido.value = parseFloat(cantidad);
                        }
                    };
                    function diferencia() {
                        var conteoFisico = document.getElementById('<%=txt_ConteoFisicoFac.ClientID%>').value;
                        var totalFactura = document.getElementById('<%=txt_TotalRecibido.ClientID%>').value;

                        var diferencia = Math.abs(parseInt(totalFactura) - parseInt(conteoFisico));
                        document.getElementById('<%=txt_DiferenciaFactura.ClientID%>').value = diferencia;
                        document.getElementById('<%=txt_ConteoFisico.ClientID%>').value = conteoFisico;
                        cantidadInicial();
                    }
                    function cantidadInicial() {
                        var conteoFisico = document.getElementById('<%=txt_ConteoFisico.ClientID%>').value;
                        var MortalidadRecibida = document.getElementById('<%=txt_MortalidadRecibida.ClientID%>').value;
                        var NetoGalera = parseInt(conteoFisico) - parseInt(MortalidadRecibida);
                        document.getElementById('<%=txt_CantidadInicialGalera.ClientID%>').value = NetoGalera;
                    }
                    function costos() {
                        var taza = parseFloat(document.getElementById('<%=txt_tazaCambio.ClientID%>').value);
                        var costoPorUnd = parseFloat(document.getElementById('<%=txt_CostoPorAveUSD.ClientID%>').value);
                        var cantidad = document.getElementById('<%=txt_TotalAvesCompradas.ClientID%>').value;

                        document.getElementById('<%=txt_EquivalenteNIO.ClientID%>').value = parseFloat(costoPorUnd) * parseFloat(taza);
                        document.getElementById('<%=txt_CostoTotalUSD.ClientID%>').value = parseFloat(cantidad) * parseFloat(costoPorUnd);
                        document.getElementById('<%=txt_CostoTotalNIO.ClientID%>').value = (parseFloat(cantidad) * parseFloat(costoPorUnd)) * parseFloat(taza);
                    }
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
    </script>
</asp:Content>
