<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="cajaChicaComedor.aspx.cs" Inherits="POULTRY.CajaChicaComedor.cajaChicaComedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Caja Chica Comedor</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />

    <!-- Bootstrap DatePicker -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Roboto&display=swap');

        body {
            /*background-color: #ffffff;*/
            font-family: 'Roboto', sans-serif;
        }

        .card {
            width: 260px;
            border: none;
            border-radius: 10px;
            background: #4E68C7;
        }

        p.top {
            font-size: 14px;
        }

        .discount {
            background-color: #1BC5DF;
            border: none;
            border-top-left-radius: 25px;
            border-bottom-left-radius: 25px;
            padding: 5px 15px;
            transform: translateX(24px);
            height: 35px;
        }

            .discount span {
                font-size: 15px;
            }

        h2 {
            letter-spacing: 2px;
        }

        .fa-money-check {
            font-size: 27px;
            color: #B3C4FA;
        }

        .fa-bank {
            font-size: 27px;
            color: #B3C4FA;
        }

        .card-content p {
            line-height: 18px;
            font-size: 11px;
            color: #abbef6;
        }

        .btn-primary {
            border: none;
            border-radius: 6px;
            background-color: #647EDF;
            padding-top: 0;
            height: 46px;
        }

            .btn-primary span {
                font-size: 13px;
                color: #D1E2FF;
                margin-right: 10px;
            }

        .fa-arrow-right {
            font-size: 12px;
            color: #D1E2FF;
        }

        .btn-primary:hover {
            background-color: #728ef3;
            box-shadow: none;
        }

        .btn-primary:focus {
            background-color: #647EDF;
            box-shadow: none;
        }

        @import url('https://fonts.googleapis.com/css2?family=Roboto&display=swap');

        body {
            /*background-color: #E6EAF5;*/
            font-family: 'Roboto', sans-serif;
        }

        h6 {
            color: #E5F3FF;
        }

        .text {
            color: #B2C9FF;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../publicas/index.aspx">Home</a></li>
                <li class="breadcrumb-item"><a href="#">Administracion</a></li>
                <li class="breadcrumb-item active" aria-current="page">Caja Chica Comedor</li>
            </ol>
        </nav>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="alerta_smg" runat="server" visible="false">
                    <div class="alert" role="alert" runat="server" id="alert_attrib">
                        <strong>
                            <asp:Label ID="lbl_titleAlert" runat="server" Text=""></asp:Label></strong>
                        <asp:Label ID="lbl_bodyAlert" runat="server" Text=""></asp:Label>
                    </div>

                </div>
                <div id="contenidoApertura" runat="server" visible="false">
                    <div>
                        <h5>Apertura de Caja</h5>
                        <div class="form-group">
                            <label for="txt_montoApertura">Monto de Apertura de Caja</label>
                            <asp:TextBox ID="txt_montoApertura" runat="server" class="form-control" aria-describedby="montAp" placeholder="0.00" TextMode="Number"></asp:TextBox>
                            <small id="montAp" class="form-text text-muted">Ingrese el Monto de Apertura para la Caja de Hoy</small>
                        </div>
                        <div class="form-group">
                            <label for="txt_observaciones">Observaciones</label>
                            <asp:TextBox ID="txt_observaciones" runat="server" class="form-control" aria-describedby="obap" placeholder="Observaciones" MaxLength="70"></asp:TextBox>
                            <small id="obap" class="form-text text-muted">Ingrese el Monto de Apertura para la Caja de Hoy</small>
                        </div>
                        <asp:Button Text="Guardar Apertura de Caja" ID="btn_AperturaC" runat="server" UseSubmitBehavior="false" CausesValidation="false" OnClick="btn_AperturaC_Click" CssClass="btn btn-info" />
                    </div>
                </div>
                <div id="contenidoCuerpo" runat="server">
                    <div class="row no-gutters">
                        <div class="col-md-3">
                            <div class="container d-flex flex-fill">
                                <div class="card mt-5 p-3 d-inline-block" style="border: none; border-radius: 20px; background-color: #597AFD;">
                                    <div class="media">
                                        <div class="mr-3"><i class="fas fa-bank"></i></div>
                                        <div class="media-body">
                                            <h6 class="mt-2 mb-0">Total Apertura C&dollar;
                                                <asp:Label ID="lbl_totalApertura" runat="server" Text="0"></asp:Label></h6>
                                            <p class="text-white-50">
                                                <asp:Label ID="lbl_DescripcionApCaja" runat="server" Text=""></asp:Label>
                                            </p>
                                            <small class="text">
                                                <asp:Label ID="lbl_fechaCaja" runat="server" Text=""></asp:Label></small>
                                            <hr />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="container d-flex">
                                <div class="card mt-5 p-3 d-inline-block" style="border: none; border-radius: 20px; background-color: #59adfd;">
                                    <div class="media">
                                        <div class="mr-3"><i class="fas fa-bank" style="font-size: 27px; color: #124d8d;"></i></div>
                                        <div class="media-body">
                                            <h6 class="mt-2 mb-0 text-black-50">Registrar un nuevo Ingreso</h6>
                                            <br />
                                            <asp:Button ID="Button1" runat="server" Text="Nuevo Ingreso" OnClick="Button1_Click" UseSubmitBehavior="false" CausesValidation="false" CssClass="form-control btn btn-primary" data-dismiss="modal" data-backdrop="false" />
                                            <small class="text  text-black-50">Registre un nuevo Ingreso</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="container d-flex">
                                <div class="card mt-5 p-3 d-inline-block" style="border: none; border-radius: 20px; background-color: #fdd159;">
                                    <div class="media">
                                        <div class="mr-3"><i class="fas fa-bank" style="font-size: 27px; color: #8d6512;"></i></div>
                                        <div class="media-body">
                                            <h6 class="mt-2 mb-0 text-black-50">Registrar un nuevo Egreso</h6>
                                            <br />
                                            <asp:Button ID="btn_NuevoEgreso" runat="server" Text="Nuevo Egreso" OnClick="btn_NuevoEgreso_Click" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-warning form-control" data-dismiss="modal" data-backdrop="false" />
                                            <small class="text  text-black-50">Registre un nuevo Egreso</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="container d-flex">
                                <div class="card mt-5 p-3 d-inline-block" style="border: none; border-radius: 20px; background-color: #79fd59;">
                                    <div class="media">
                                        <div class="mr-3"><i class="fas fa-bank" style="font-size: 27px; color: #2a8d12;"></i></div>
                                        <div class="media-body">
                                            <h6 class="mt-2 mb-0 text-black-50">Registrar un Nuevo Gasto</h6>
                                            <br />
                                            <asp:Button ID="btn_NuevoGasto" runat="server" Text="Nuevo Gasto" OnClick="btn_NuevoGasto_Click" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn btn-success form-control" data-dismiss="modal" data-backdrop="false" />
                                            <small class="text text-black-50">Registre un nuevo Gasto</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row no-gutters">
                        <div class="col-md-3">
                            <div class="container d-flex justify-content-center">
                                <div class="card mt-5 p-4 text-white">
                                    <p class="top mb-1 text-white">TOTAL EN CAJA CHICA COMEDOR</p>
                                    <div class="d-flex flex-row justify-content-between text-align-center xyz">
                                        <h2><i class="fas fa-money-check"></i><span>
                                            <asp:Label ID="lbl_TotalCaja" runat="server" Text=""></asp:Label>
                                        </span></h2>
                                        <%--<div class="discount"><span>5:30AM</span></div>--%>
                                    </div>
                                    <div class="card-content mt-auto">
                                        <p>Muestra el Total En Caja Chica del Comedor, Apertura+Ingresos-Gastos+Egresos</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="container d-flex justify-content-center">
                                <div class="card mt-5 p-4 text-white" style="background-color: #59adfd;">
                                    <p class="top mb-1 text-black-50">TOTAL INGRESOS EN CAJA</p>
                                    <div class="d-flex flex-row justify-content-between text-align-center xyz">
                                        <h2 class="text-black-50"><i class="fas fa-money-check" style="color: #1e588a"></i><span>
                                            <asp:Label ID="lbl_TotalIngresos" runat="server" Text="Label"></asp:Label>
                                        </span></h2>
                                        <%--<div class="discount" style="background-color: #1e738a"><span>5:30AM</span></div>--%>
                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btn_MasDetallesIngreso" runat="server" OnClick="btn_MasDetallesIngreso_Click" Style="background-color: #1e558a" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-block btn-lg btn-primary">
                                            <span>Ver mas Detalles</span><i class="fas fa-arrow-right"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="container d-flex justify-content-center">
                                <div class="card mt-5 p-4 text-white" style="background-color: #fdd159;">
                                    <p class="top mb-1 text-black-50">TOTAL EGRESOS EN CAJA</p>
                                    <div class="d-flex flex-row justify-content-between text-align-center xyz">
                                        <h2 class="text-black-50"><i class="fas fa-money-check" style="color: #8d6512"></i><span>
                                            <asp:Label ID="lbl_TotalEgresos" runat="server" Text="Label"></asp:Label></span></h2>
                                        <%--<div class="discount" style="background-color: #8d6512"><span>5:30AM</span></div>--%>
                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btn_MasDetallesEgreso" runat="server" OnClick="btn_MasDetallesEgreso_Click" Style="background-color: #1e558a" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-block btn-lg btn-primary">
                                            <span>Ver mas Detalles</span><i class="fas fa-arrow-right"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="container d-flex justify-content-center">
                                <div class="card mt-5 p-4 text-white" style="background-color: #79fd59;">
                                    <p class="top mb-1 text-black-50">TOTAL GASTOS EN CAJA</p>
                                    <div class="d-flex flex-row justify-content-between text-align-center xyz">
                                        <h2 class="text-black-50"><i class="fas fa-money-check" style="color: #338a1e"></i><span>
                                            <asp:Label ID="lbl_TotalGastosC" runat="server" Text=""></asp:Label></span></h2>
                                        <%--<div class="discount" style="background-color: #338a1e"><span>5:30AM</span></div>--%>
                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btn_MasDetallesGastos" runat="server" OnClick="btn_MasDetallesGastos_Click" Style="background-color: #1e558a" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-block btn-lg btn-primary">
                                            <span>Ver mas Detalles</span><i class="fas fa-arrow-right"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-4">
                            <asp:Button Text="CIERRE DE CAJA CHICA" ID="btn_CierreCaja" OnClick="btn_CierreCaja_Click" CssClass="btn btn-primary form-control" runat="server" UseSubmitBehavior="false" CausesValidation="false" />

                        </div>
                    </div>
                    <div class="row" id="card_Cierre" runat="server" visible="false">
                        <div class="col-12">
                            <div class="card card-body mt-20 p-3 bg-light" style="width: 100%">
                                <h5>Cierre de Caja</h5>
                                <p>Antes de Cerrar la Caja debe indicar Todas las denominaciones junto con la cantidad que tiene en su Caja Fisica</p>
                                <p>Una vez indicado las cantidades puede proceder a Cerrar la caja, una vez cerrada la Caja No podra Realizar mas movimientos a la caja, de ser necesario tendra que registrarlos como un monto de apertura indicando debidamente en las observaciones de la apertura el concepto del monto</p>
                                <div class="row">
                                    <div class="col-6">
                                        <div class="row">
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label for="txt_denominacion">Denominación</label>
                                                    <asp:TextBox ID="txt_denominacion" TextMode="Number" data-toggle="tooltip" title="Ej: 1, 5, 10, 20, en caso de haber monedas con la misma denominacíon que en billete, puede repetirlo." runat="server" class="form-control" aria-describedby="deno_Help" placeholder="00" required="true"></asp:TextBox>
                                                    <small id="deno_Help" class="form-text text-muted">ingrese la denominacion en Moneda </small>
                                                </div>
                                            </div>
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label for="txt_Cantidad">Cantidad</label>
                                                    <asp:TextBox ID="txt_Cantidad" runat="server" class="form-control" aria-describedby="cant_Help" placeholder="00" required="true" TextMode="Number"></asp:TextBox>
                                                    <small id="cant_Help" class="form-text text-muted">Indique la Catidad por la Denominacion Ej: de 5 hay 10</small>
                                                </div>
                                            </div>
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label for="agregarDen">Agregar a la Lista</label>
                                                    <asp:Button Text="Agregar" aria-describedby="agre_Help" runat="server" ID="agregarDen" OnClick="agregarDen_Click" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-success form-control" />
                                                    <small id="agre_Help" class="form-text text-muted">Agrega la Denominacion a la Lista</small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12">
                                                <asp:GridView ID="gv_Denominaciones" DataKeyNames="ID_ArqueoCajaChica_Comedor" CssClass="table  table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_Denominaciones_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="Denominacion" HeaderText="Denominacion">
                                                            <ItemStyle Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Cantidad_Denominacion" DataFormatString="{0:C2}" HeaderText="Monto">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="True"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TotalDenominacion_local" DataFormatString="{0:C2}" HeaderText="Total">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="True"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Remover">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Eliminar el item de la lista" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="remover" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> 
                                                                </asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="container d-flex align-self-stretch">
                                            <div class="card  shadow-sm mb-5 p-3 text-white" style="width: 100%">
                                                <p class="top mb-1 text-white">TOTAL EN CAJA CHICA COMEDOR</p>
                                                <div class="d-flex flex-row justify-content-between text-align-center xyz">
                                                    <h2><i class="fas fa-money-check"></i><span>
                                                        <asp:Label ID="lbl_TotalCajaParaCierre" runat="server" Text=""></asp:Label>
                                                    </span></h2>
                                                    <%--<div class="discount"><span>5:30AM</span></div>--%>
                                                </div>
                                                <div class="d-flex flex-row justify-content-between text-align-center xyz">
                                                    <h2><i class="fas fa-bank"></i><span>
                                                        <asp:Label ID="lbl_TotalArqueo" runat="server" Text=""></asp:Label>
                                                    </span></h2>
                                                    <%--<div class="discount"><span>5:30AM</span></div>--%>
                                                </div>
                                                <div class="card-content mt-auto">
                                                    <p>Muestra una Comparativa entre lo que hay en caja junto al arqueo fisico</p>
                                                    <h5>Cerrar Caja</h5>
                                                    <div class="form-group">
                                                        <label for="txt_ComentarioCierre">Observaciones Cierre</label>
                                                        <asp:TextBox ID="txt_ComentarioCierre" runat="server" class="form-control" aria-describedby="coment_Help"></asp:TextBox>
                                                        <small id="coment_Help" class="form-text text-white">Si hay una observacion que desee reflejar puede ingresarlo en este campo</small>
                                                    </div>
                                                    <button type="button" class="btn btn-warning form-control" data-toggle="modal" data-target="#ModalConfirmacion">
                                                        Realizar Cierre de Caja
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
                <div id="CuerpoCajaCerrada" runat="server" visible="false">
                    <!-- Jumbotron -->
                    <div
                        class="p-5 text-center bg-image rounded-3 jumbotron-fluid"                        
                        style="background-image: url('../imagenes/alimentos_comedor.jpg'); height: 400px;">
                        <div class="mask" style="background-color: rgba(0, 0, 0, 0.6);">
                            <div class="d-flex justify-content-center align-items-center h-100">
                                <div class="text-white">
                                    <h1 class="mb-3">Caja Chica Comedor Cerrada!</h1>
                                    <h4 class="mb-3">La caja Chica del Comedor del Dia de Hoy se Encuentra cerrada, no se puede realizar mas movimientos dentro de la misma</h4>
                                    <a class="btn btn-outline-light btn-lg" href="#!" role="button">Call to action</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Jumbotron -->
                </div>


                <%--=========================================================================================================================================--%>
                <div class="modal fade" id="modalRegistro" tabindex="-1" role="dialog" aria-labelledby="labeInfo">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="labeInfo">Movimiento de Caja!</h5>
                                <button type="button" class="close" data-dismiss="modal" data-backdrop="false" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">

                                <div>
                                    <div class="form-group">
                                        <label for="txt_recibedeentrega">
                                            <asp:Label ID="lbl_titulo_REcibeEntrega" runat="server" Text=""></asp:Label></label>
                                        <asp:TextBox ID="txt_recibedeentrega" runat="server" class="form-control" placeholder="Nombre" MaxLength="70"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txt_Concepto">En Consepto de</label>
                                        <asp:TextBox ID="txt_Concepto" runat="server" class="form-control" placeholder="Concepto" MaxLength="70"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txt_Monto">Monto</label>
                                        <asp:TextBox ID="txt_Monto" runat="server" class="form-control" placeholder="0.00" TextMode="Number"></asp:TextBox>
                                    </div>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1"><i class="fas fa-calendar"></i></span>
                                        </div>
                                        <asp:TextBox ID="txt_fechaRegistro" runat="server" class="form-control" ReadOnly="true" aria-describedby="basic-addon1"></asp:TextBox>
                                    </div>
                                    <asp:Button ID="btn_GuardarIngreso" runat="server" Text="Guardar Ingreso a Caja" OnClick="btn_GuardarIngreso_Click" UseSubmitBehavior="false" CausesValidation="false" CssClass="form-control btn btn-primary" data-dismiss="modal" data-backdrop="false" />
                                    <asp:Button ID="btn_GuardarEgreso" runat="server" Text="Guardar Egreso a Caja" OnClick="btn_GuardarEgreso_Click" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-warning form-control" data-dismiss="modal" data-backdrop="false" />
                                    <asp:Button ID="btn_GuardarGasto" runat="server" Text="Guardar Gasto de Caja" OnClick="btn_GuardarGasto_Click" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-success form-control" data-dismiss="modal" data-backdrop="false" />
                                </div>
                                <small class="text-muted small">Registrado Por:
                                        <asp:LoginName ID="LoginName1" runat="server" />
                                </small>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal" data-backdrop="false">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <%--=========================================================================================================================================--%>

                <%--=================================================[DETALLES]==============================================================================--%>
                <div class="modal fade" id="ModalDetallesMob" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Movimientos en Caja</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <h5>DETALLES</h5>
                                <%--GRID PARA MOSTRAR TODOS LOS INGRESOS--%>

                                <asp:GridView ID="gv_IngresosEnCaja" DataKeyNames="ID_Ingresos_CC_Comedor" CssClass="table  table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_IngresosEnCaja_RowCommand">
                                    <Columns>

                                        <asp:BoundField DataField="RecibeDe" HeaderText="Recibe De">
                                            <ItemStyle Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EnConceptoDe" HeaderText="En Concepto De"></asp:BoundField>
                                        <asp:BoundField DataField="Cantidad" DataFormatString="{0:C2}" HeaderText="Monto">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaIngreso_sy" HeaderText="Fecha Ingreso sy"></asp:BoundField>
                                        <asp:BoundField DataField="UserName" HeaderText="RegiadoPor"></asp:BoundField>
                                        <asp:BoundField DataField="FechaPorUsuario" DataFormatString="{0:d}" HeaderText="Fecha Por Usuario"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Remover">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Eliminar el item de la base de datos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="remover" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> 
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <%----------------------------------------------------------------%>
                                <%--GRID PARA MOSTRAR TODOS LOS EGRESOS--%>

                                <asp:GridView ID="gv_EgresosEnCaja" DataKeyNames="ID_Egresos_CC_Comedor" CssClass="table  table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_Egresos_RowCommand">
                                    <Columns>

                                        <asp:BoundField DataField="EntregaA" HeaderText="Entrega A">
                                            <ItemStyle Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EnConceptoDe" HeaderText="En Concepto De"></asp:BoundField>
                                        <asp:BoundField DataField="Cantidad" DataFormatString="{0:C2}" HeaderText="Monto">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaEgreso_sy" HeaderText="Fecha Egreso sy"></asp:BoundField>
                                        <asp:BoundField DataField="UserName" HeaderText="RegiadoPor"></asp:BoundField>
                                        <asp:BoundField DataField="FechaPorUsuario" DataFormatString="{0:d}" HeaderText="Fecha Por Usuario"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Remover">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Eliminar el item de la base de datos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="remover" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> 
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <%----------------------------------------------------------------%>
                                <%--GRID PARA MOSTRAR TODOS LOS GASTOS--%>

                                <asp:GridView ID="gv_GastosEnCaja" DataKeyNames="ID_Gasto_CC_Comedor" CssClass="table  table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_GastosEnCaja_RowCommand">
                                    <Columns>

                                        <asp:BoundField DataField="EntregaA" HeaderText="Entrega A">
                                            <ItemStyle Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EnConceptoDe" HeaderText="En Concepto De"></asp:BoundField>
                                        <asp:BoundField DataField="Cantidad" DataFormatString="{0:C2}" HeaderText="Monto">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaGasto_sy" HeaderText="Fecha Gasto sy"></asp:BoundField>
                                        <asp:BoundField DataField="UserName" HeaderText="RegiadoPor"></asp:BoundField>
                                        <asp:BoundField DataField="FechaPorUsuario" DataFormatString="{0:d}" HeaderText="Fecha Por Usuario"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Remover">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Eliminar el item de la base de datos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="remover" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> 
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <%----------------------------------------------------------------%>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <%--=========================================================================================================================================--%>


                <%--=================================================[CONFIRMACION DE CIERRE]==============================================================================--%>
                <div class="modal fade" id="ModalConfirmacion" tabindex="-1" role="dialog" aria-labelledby="confirmacionlbl" aria-hidden="true">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="confirmacionlbl">Atencion!!</h5>
                                <button type="button" class="close" data-dismiss="modal" data-backdrop="false" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <h5>Confirmacion de Cierre de caja</h5>
                                ¿Esta seguro de desea cerrar la Caja del Dia?
                                <p>Si procede no podra agregar mas elementos a la caja chica, ni realizar ningun otro movimiento dentro de la caja</p>
                                <asp:Button ID="btn_ConfirmarCierre" UseSubmitBehavior="false" data-dismiss="modal" data-backdrop="false" CausesValidation="false" runat="server" Text="Continuar con el Cierre" OnClick="btn_ConfirmarCierre_Click" CssClass="btn btn-warning form-control" />
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal" data-backdrop="false">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <%--=========================================================================================================================================--%>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_IngresosEnCaja.ClientID%>").footable();
                $("#<%=gv_EgresosEnCaja.ClientID%>").footable();
                $("#<%=gv_Denominaciones.ClientID%>").footable();
            });
        });
    </script>



    <!-- Bootstrap DatePicker -->
    <script type="text/javascript">
        $(function () {
            $('[id*=txt_fechaRegistro]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "es-Es"
            });
        });
    </script>
    <script>
        jQuery(document).ready(function ($) {

            $(document).ready(function () {
                /*
                */
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {

                if (sender._postBackSettings.panelsToUpdate != null) {

                    $(function () {
                        $('[id*=txt_fechaRegistro]').datepicker({
                            changeMonth: true,
                            changeYear: true,
                            format: "dd/mm/yyyy",
                            language: "es-Es"
                        });
                    });
                    $("#<%=gv_IngresosEnCaja.ClientID%>").footable();
                    $("#<%=gv_EgresosEnCaja.ClientID%>").footable();
                    $("#<%=gv_Denominaciones.ClientID%>").footable();
                }
            });
        };

        function ShowModalRegistro() {
            $('#modalRegistro').modal('show');
        }
        function ShowModalDetalles_mb() {
            $('#ModalDetallesMob').modal('show');
        }
        function quitarAlerta() {
            setTimeout(function () {
                $('#alerta_smg').hide()
            }, 4000);
        }
        //function opencierrecaja() {
        //    $('#acorCierre').collapse({
        //        toggle: true
        //    });
        //}
        //function closecierrecaja() {
        //    $('#acorCierre').collapse({
        //        toggle: false
        //    });
        //}
    </script>
</asp:Content>

