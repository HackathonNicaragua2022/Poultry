<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="inventarioComedor.aspx.cs" Inherits="POULTRY.inventarioComedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inventario Comedor</title>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="jumbotron">
            <h1>Inventario de Productos Comedor POULTRY</h1>
            <p>
                Actualice los productos y verifique el flujo desde esta ventana
            </p>
            <div id="accordion">
                <div class="card">
                    <div class="card-header" id="headingOne">
                        <h5 class="mb-0">
                            <button class="btn btn-primary" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseExample">
                                Click aqui para mostrar mas Contenido 
                            </button>
                        </h5>
                    </div>

                    <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="card text-white bg-primary mb-3" style="max-width: 18rem;">
                                        <div class="card-header">Filtrar por Categoria</div>
                                        <div class="card-body">
                                            <h5 class="card-title">Filtrar por Categoria</h5>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:DropDownList ID="dr_Categorias" runat="server" CssClass="dropdown form-control" AutoPostBack="true" OnSelectedIndexChanged="dr_Categorias_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <p class="card-text" style="color: white;">Ver los productos segun la categoria seleccionada</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="card text-white bg-info mb-3" style="max-width: 18rem;">
                                        <div class="card-header">Productos Total</div>
                                        <div class="card-body">
                                            <%--  <h5 class="card-title text-center">Productos Total comedor</h5>--%>
                                            <!-- first count item -->
                                            <div class="col-md-12 col-sm-12 col-xs-12 text-center text-white">
                                                <div class="counters-item">
                                                    <div class="text-white">
                                                        <span class="counter" runat="server" id="dataCount_span" data-count="150">0</span>
                                                    </div>
                                                    <h3 class="text-white">TOTAL PRODUCTOS</h3>
                                                </div>
                                            </div>
                                            <!-- end first count item -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="card text-white bg-success mb-3" style="max-width: 18rem;">
                                        <div class="card-header">Costo , ventas y ganancias</div>
                                        <div class="card-body">
                                            <h5 class="card-title text-center">Costo, precios y ganancias Totales</h5>
                                            <!-- first count item -->
                                            <div class="col-md-12 col-sm-12 col-xs-12 text-center text-white">
                                                <div class="table-responsive-sm">
                                                    <table class="table table-striped  text-white">
                                                        <thead>
                                                            <tr>
                                                                <th>Valor</th>
                                                                <th class="right">Total</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td class="left strong">Costos Totales*</td>
                                                                <td class="right">C$
                                                    <asp:Label ID="lbl_costoTotales" Text="0" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="left strong">Ventas Totales</td>
                                                                <td class="right">C$
                                                    <asp:Label ID="lbl_VentasTotales" Text="0" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="left strong">Utilidad</td>
                                                                <td class="right">C$
                                                    <asp:Label ID="lbl_Utilidad" Text="0" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <span style="font-size: 9px;">*Solo se incluyen los productos habilitados para la venta</span>
                                            </div>
                                            <!-- end first count item -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <asp:LoginView ID="LoginView1" runat="server">
                    <RoleGroups>
                        <asp:RoleGroup Roles="Admin">
                            <%--Solo administradores pueden ver esto--%>
                            <ContentTemplate>
                            </ContentTemplate>
                        </asp:RoleGroup>
                    </RoleGroups>
                </asp:LoginView>

                <span style="font-size: 9px;">*Para habilitar y deshabilitar un producto para su venta puede hacerlo desde el menu Administracion/Productos, o bien dando click a este enlace <a href="http://grupojimenez.co/POULTRY /productos.aspx">Productos</a> </span>
                <div id="panelMensaje" runat="server" visible="false" class="card text-white bg-dark mb-3" style="max-width: 100%;">
                    <div class="card-header">Sin Productos</div>
                    <div class="card-body">
                        <h5 class="card-title">Resultados de  Filtro</h5>
                        <p class="card-text">Esta Categoria no contiene productos, puede ingresarlos dando click en el menu Administracion/Productos, o bien dando click a este enlace <a href="http://grupojimenez.co/POULTRY /productos.aspx">Productos</a></p>
                    </div>
                </div>
                <asp:GridView ID="gv_invetario" DataKeyNames="ID_Inventario" runat="server" CssClass="table table-dark table-striped table-hover" AutoGenerateColumns="false" EnableViewState="False" OnRowCommand="gv_invetario_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="MedidaProducto" HeaderText="Medida Producto"></asp:BoundField>
                        <asp:BoundField DataField="NombreProducto" HeaderText="Producto"></asp:BoundField>
                        <asp:BoundField DataField="CantidadEntrante" HeaderText="Cantidad Entrante"></asp:BoundField>
                        <asp:BoundField DataField="CantidadSaliente" HeaderText="Cantidad Saliente"></asp:BoundField>
                        <asp:BoundField DataField="CantidadPerdida" HeaderText="Cantidad Perdida"></asp:BoundField>
                        <asp:BoundField DataField="CantidadDanada" HeaderText="Cantidad Dañada"></asp:BoundField>
                        <asp:BoundField DataField="CantidadVencida" HeaderText="Cantidad Vencida"></asp:BoundField>
                        <asp:BoundField DataField="ExistenciaActual" HeaderText="Existencia"></asp:BoundField>

                        <asp:BoundField DataField="CostoCompra" HeaderText="Costo Compra"></asp:BoundField>
                        <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta"></asp:BoundField>

                        <asp:CheckBoxField DataField="HabilitadoParaVenta" Text="Habilitado" HeaderText="Habilitado"></asp:CheckBoxField>

                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_Perdidos" data-toggle="tooltip" title="Registre los productos Perdidos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="perdidos" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-location-arrow"></i>Perdidos
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_Danados" data-toggle="tooltip" title="Registre los productos Dañados" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="danados" CssClass="btn btn-warning form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-procedures"></i>Dañados
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_Vencidos" data-toggle="tooltip" title="Registre los productos Vencidos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="vencidos" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-calendar-check"></i>Vencidos
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>

            <%--=========================================================================================================================================--%>
            <div class="modal fade" id="modalRegistroProductos" tabindex="-1" role="dialog" aria-labelledby="labeInfo2">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="labeInfo2">Registrar Producto</h5>
                            <button type="button" class="close" data-dismiss="modal" data-backdrop="false" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <h4>
                                <asp:Label Text="Tipo de Registro" ID="lbl_TipoRegistro" runat="server" />
                            </h4>
                            <h5>ID de Inventario:
                                <asp:Label Text="0" runat="server" ID="lbl_IDInventario" /></h5>
                            <hr />
                            <div>
                                <div class="form-group">
                                    <label for="txt_FechaRegistro">Fecha Registro</label>
                                    <asp:TextBox ID="txt_FechaRegistro" CssClass="form-control" runat="server" aria-describedby="fechaIngreso" Enabled="false"></asp:TextBox>
                                    <small id="fechaIngreso" class="form-text text-muted">Es la fecha de ingreso al sistema</small>
                                </div>
                                <div class="form-group">
                                    <label for="txt_CantidadRegistrar">Cantidad a Registrar</label>
                                    <asp:TextBox ID="txt_CantidadRegistrar" CssClass="form-control" runat="server" aria-describedby="cantidadRegistrar"></asp:TextBox>                                    
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        ControlToValidate="txt_CantidadRegistrar" runat="server"
                                        ErrorMessage="Solo se haceptan numeros enteros positivos!"
                                        ValidationExpression="\d+">
                                    </asp:RegularExpressionValidator>
                                    <small id="cantidadRegistrar" class="form-text text-muted">La cantidad a Registrar en el sistema.</small>
                                </div>
                                <div class="form-group">
                                    <label for="btn_Registrar">Registrar Producto</label>
                                    <asp:Button ID="btn_Registrar" runat="server" CssClass="btn btn-info form-control" UseSubmitBehavior="false" CausesValidation="false" Text="Registrar Productos Perdidos" OnClick="btn_Registrar_Click" data-dismiss="modal" data-backdrop="false"/>
                                    <asp:Button ID="btn_RegistrarDanado" runat="server" CssClass="btn btn-warning form-control" UseSubmitBehavior="false" CausesValidation="false" Text="Registrar Productos Dañado" OnClick="btn_RegistrarDanado_Click" Visible="false" data-dismiss="modal" data-backdrop="false" />
                                    <asp:Button ID="btn_RegistrarVencido" runat="server" CssClass="btn btn-danger form-control" UseSubmitBehavior="false" CausesValidation="false" Text="Registrar Productos Vencido" OnClick="btn_RegistrarVencido_Click" Visible="false" data-dismiss="modal" data-backdrop="false" />
                                    <small id="registrar" class="form-text text-muted">Guarda el producto en el sistema, actualizando el inventario</small>
                                </div>
                            </div>
                            <span style="font-size: 10px;">*Este dato se registrara bajo su nombre de usuario (<asp:LoginName ID="LoginName1" runat="server" />
                                )</a> </span>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal" data-backdrop="false">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>


            <%--=========================================================================================================================================--%>
            <div class="modal fade" id="modalInfo" tabindex="-1" role="dialog" aria-labelledby="labeInf3o">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="labeInf3o">Atencion!!</h5>
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
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $('#ContentPlaceHolder1_gv_invetario').footable();
            });
        });
        function modal_RegistroProductos() {
            $('#modalRegistroProductos').modal('show');
        }
        function modal_RegistroProductos_hide() {
            $('#modalRegistroProductos').modal('hide');
        }
        function ShowModalInfo() {
            $('#modalInfo').appendTo("body").modal('show');
        }
        /*Footable 
            Cuando footable boostrap esta dentro de un updatepanel  
            y se habre un modal, la abla pierde las propiedades del footable,
            al recargar fotable en el proximo postback del update panel con una funcion llamda desde codebehide
            se puede volver a asignar la propiedad de footable y mantener el valor
        */

        function reformatiarTabla() {
            $('#ContentPlaceHolder1_gv_invetario').footable();
        }
        //---------------------------------------------------------
    
    </script>
</asp:Content>
