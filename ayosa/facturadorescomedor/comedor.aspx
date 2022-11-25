<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="comedor.aspx.cs" Inherits="POULTRY.comedor" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Comedor POULTRY</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link href="../css/personal.css" rel="stylesheet" />
    <link href="../css/bootstrap4-toggle.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <%--<div style="background-image: url('imagenes/texturaFrutas.jpg'); background-repeat: repeat-y; background-size: auto; height: 60px; margin-top: -50px;">--%>
        <%-- </div>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <fieldset>
                    <div class="row">
                        <div class="col-sm-6">
                            <h1>COMEDOR POULTRY</h1>
                            <small class="text-muted small">Cambie el tipo de venta al finalizar la misma</small><br />                            
                            <input type="checkbox" checked data-toggle="toggle" data-on="CREDITO" data-off="CONTADO" data-onstyle="info" data-offstyle="success" name="tipoFactura" value="Credito" id="chk_tipoFactura" onclick="checkAddress(this)"/>
                        </div>

                        <div class="col-sm-6">
                            <table style="width: 100%;" class="table table-striped table-dark">
                                <thead>
                                    <tr>
                                        <td>TOTAL ITEMS</td>
                                        <td>TOTAL A PAGAR</td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_totalItem" runat="server" Text="00"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_PrecioTotal" runat="server" Text="00"></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="alert alert-primary" role="alert">
                                <h2>
                                    <asp:Label ID="lbl_NombreTrabajador" runat="server" Text=""></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="row">                     
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txt_buscar">BUSCAR CLIENTE</label>
                                <asp:TextBox ID="txt_buscar" runat="server" class="form-control" MaxLength="30" OnTextChanged="txt_buscar_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="btn_Guardar">GUARDAR FACTURA</label>
                                <asp:Button ID="btn_Guardar" runat="server" Text="GUARDAR FACTURA" CssClass="btn btn-success  form-control" OnClick="btn_Guardar_Click" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="btn_Factura">VER FACTURA</label>

                                <button type="button" class="btn btn-primary form-control" data-toggle="modal" data-target="#modalFacturado">
                                    PRODUCTOS FACTURADOS
                                </button>
                            </div>
                        </div>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                        <ProgressTemplate>
                            <div class="progress">
                                <div class="progress-bar progress-bar-striped progress-bar-animated active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%"></div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Panel ID="InfoPanel" runat="server" class="alert alert-success alert-dismissable" Visible="false">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                        <i class="fa-lg fa fa-bullhorn"></i>
                        <asp:Label ID="lblMessage" runat="server" Text="El item fue Agregado Correctamente"></asp:Label>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Empleados" Visible="false">
                        <h3>Empleados encontrados</h3>
                        <asp:ListView runat="server" ID="listCliente" DataKeyNames="ID_Trabajador">
                            <LayoutTemplate>
                                <ul class="list-group" id="itemPlaceholder" runat="server">
                                    <li class="list-group-item">
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <asp:Label Text='<%# Eval("trabajador") %>' runat="server" ID="Label1" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Button ID="Button1" runat="server" Text="Seleccionar" CssClass="btn btn-info" OnClick="btn_SeleccionarEmpleado_Click" />
                                            </div>
                                        </div>
                                    </li>
                                    <asp:Button ID="btn_SeleccionarEmpleado" runat="server" Text="Seleccionar" CssClass="btn btn-info" OnClick="btn_SeleccionarEmpleado_Click" />
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <ul class="col-md-12 list-group">
                                    <li class="list-group-item">
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <asp:Label Text='<%# Eval("trabajador") %>' runat="server" ID="Label1" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Button ID="btn_SeleccionarEmpleado" runat="server" Text="Seleccionar" CssClass="btn btn-info" OnClick="btn_SeleccionarEmpleado_Click" />
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-md-6 col-sm-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                            <div class="shadow p-3 mb-5 bg-white rounded">
                                                <asp:ImageButton ImageUrl="~/imagenes/desayunos.jpg" ID="img_desayunos" Width="100%" CssClass="menusImagen" runat="server" OnClick="desayunos_id_Click" />
                                            </div>
                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                            <div class="shadow p-3 mb-5 bg-white rounded">
                                                <asp:ImageButton ImageUrl="~/imagenes/almuerzos.jpg" ID="img_almuerzo" Width="100%" CssClass="menusImagen" runat="server" OnClick="img_almuerzo_Click" />
                                            </div>
                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                            <div class="shadow p-3 mb-5 bg-white rounded">
                                                <asp:ImageButton ImageUrl="~/imagenes/Cenas.jpg" ID="img_cenas" Width="100%" CssClass="menusImagen" runat="server" OnClick="img_cenas_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                            <div class="shadow p-3 mb-5 bg-white rounded">
                                                <asp:ImageButton ImageUrl="~/imagenes/Reposteria.jpg" ID="img_reposteria" Width="100%" CssClass="menusImagen" runat="server" OnClick="img_reposteria_Click" />
                                            </div>
                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                            <div class="shadow p-3 mb-5 bg-white rounded">
                                                <asp:ImageButton ImageUrl="~/imagenes/Extras.png" ID="img_extras" Width="100%" CssClass="menusImagen" runat="server" OnClick="img_extras_Click" />
                                            </div>
                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                            <div class="shadow p-3 mb-5 bg-white rounded">
                                                <asp:ImageButton ImageUrl="~/imagenes/BebidasVariadas.jpg" ID="img_bebidas" Width="100%" CssClass="menusImagen" runat="server" OnClick="img_bebidas_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- general form elements -->
                                    <div class="card bg-info">
                                        <div class="card-header text-white">
                                            <h3 class="card-title">Todas las Categorias</h3>
                                        </div>
                                        <!-- /.card-header -->
                                        <!-- form start -->
                                        <div>
                                            <div class="card-body text-white">
                                                <h5>Seleccione la categoria para cargar sus productos</h5>
                                                <dx:ASPxGridLookup ID="glu_TodasCategorias" GridViewStylesEditors-Native="true" CssClass="dropdown form-control" runat="server" OnValueChanged="glu_TodasCategorias_ValueChanged" Theme="PlasticBlue" AutoPostBack="true"></dx:ASPxGridLookup>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.card -->
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <asp:GridView ID="gv_productos" DataKeyNames="ID_Producto" CssClass="table table-dark table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_productos_RowCommand1">
                                <Columns>
                                    <asp:TemplateField HeaderText="MEDIDA">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("MedidaProducto") %>' ID="lbl_med"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRODUCTO">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("NombreProducto") %>' ID="lbl_nom"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRECIO">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("PrecioVenta") %>' ID="lbl_Precio"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_cantidad" runat="server" Text="1" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AGREGAR">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_Agregar" data-toggle="tooltip" title="Agrega a la lista de compra el producto" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="agregar" CssClass="btn btn-success form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-plus-square"></i> 
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <%--=========================================================================================================================================--%>
                    <div class="modal fade" id="modalFacturado" tabindex="-1" role="dialog" aria-labelledby="lblFacturando">
                        <div class="modal-dialog modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="lblFacturando">Atencion!!</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="modal-body">
                                    <h2>FACTURADO HASTA EL MOMENTO</h2>
                                    <div class="container">
                                        <asp:GridView ID="gv_DetalleFactura" data-filtering="true" data-sorting="true" CssClass="table table-dark table-striped table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" PageSize="50" AllowPaging="True" OnRowCommand="gv_DetalleFactura_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="N°">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="NombreProducto">
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Bind("NombreProducto") %>' ID="txt_cob"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("NombreProducto") %>' ID="lbl_cob"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Bind("Cantidad") %>' ID="txt_ced"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("Cantidad") %>' ID="lbl_ced"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PrecioUnidad">
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Bind("PrecioUnidad") %>' ID="txt_nom"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("PrecioUnidad") %>' ID="lbl_nom"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PrecioTotal">
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Bind("PrecioTotal") %>' ID="txt_ape"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("PrecioTotal") %>' ID="lbl_ape"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <%--<asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_actualizar" data-toggle="tooltip" title="Actualiza los datos de la fila" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="actualizar" CssClass="btn btn-warning form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-edit"></i> 
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Remover">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_eliminar" data-dismiss="modal" data-backdrop="false" data-toggle="tooltip" title="Eliminar el item de la base de datos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="eliminar" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> 
                                                        </asp:LinkButton>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <div class="row" style="margin-top: 20px;">
                                                    <div class="col-lg-1" style="text-align: right;">
                                                        <h5>
                                                            <asp:Label ID="MessageLabel" Text="Pagina:" CssClass="label label-info" runat="server" /></h5>
                                                    </div>
                                                    <div class="col-lg-1" style="text-align: left;">
                                                        <asp:DropDownList ID="dr_Page_Trabajador" Width="70px" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-10" style="text-align: right;">
                                                        <h3>
                                                            <asp:Label ID="currentPage_Trabajador" runat="server" CssClass="label label-warning" />
                                                        </h3>
                                                    </div>
                                                </div>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--=========================================================================================================================================--%>


                    <%--=========================================================================================================================================--%>
                    <div class="modal fade" id="modalInfo" tabindex="-1" role="dialog" aria-labelledby="labeInfo">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="labeInfo">Atencion!!</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="modal-body">
                                    <div class="text-center">
                                        <img id="imagenModalInfo" src="~/imagenes/camera_test.png" alt="Ok" class="img-responsive center-block" runat="server" /><hr />
                                        <asp:Label ID="lbl_modalInfo" runat="server" Text="Label" Width="50%">mensaje</asp:Label>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--=========================================================================================================================================--%>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="../js/bootstrap4-toggle.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_DetalleFactura.ClientID%>").footable();

                $("#<%=gv_productos.ClientID%>").footable();
            });
        });
        function ocultarAlerta() {
            $(document).ready(function () {
                $("#<%=InfoPanel.ClientID%>").delay(5000).fadeTo(500, 0);
            });
        }
        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }




        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(function () { $('#chk_tipoFactura').bootstrapToggle() });                    
                }
            });
        };

        $(document).on('change', '.custom-control', function (e) {
            let test = e.target.checked;
            console.log(test);
        });
    </script>
</asp:Content>
