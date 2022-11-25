<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="IngresoComedor.aspx.cs" Inherits="POULTRY.facturadorescomedor.IngresoComedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ingreso Productos Comedor</title>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">

                <div class="card text-center bg-light">
                    <div class="card-header">
                        INGRESO A INVENTARIO DE PRODUCTOS COMEDOR
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">Fecha de Ingreso al Sistema</h5>
                        <h6 class="card-subtitle">
                            <asp:Label Text="" ID="lbl_FechaIngreso" runat="server" /></h6>
                        <div class="row">
                            <div class="col-sm-12 col-md-6">
                                <div class="form-group  text-left">
                                    <label for="dr_Proveedores">Proveedor</label>
                                    <asp:DropDownList ID="dr_Proveedores" CssClass="dropdown form-control" runat="server" aria-describedby="drProve"></asp:DropDownList>
                                    <small id="drProve" class="form-text">Seleccione un Proveedor para el/los productos a ingresar</small>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 col-md-12">
                                <div class="form-group text-left">
                                    <label for="txt_Referencia">Referencia de Ingreso</label>
                                    <asp:TextBox ID="txt_Referencia" CssClass="form-control" runat="server" TextMode="MultiLine" aria-describedby="txtRef"></asp:TextBox>
                                    <small id="txtRef" class="form-text">Ingrese un comentario de referencia para este ingreso a inventario (Opcional)</small>
                                </div>
                            </div>
                        </div>
                        <div class="card text-white bg-dark mb-12" runat="server" id="panelP">
                            <div class="card-header">Seleccione el producto en invetario</div>
                            <div class="card-body">
                                <h5 class="card-title">PRODUCTOS EN INVENTARIO</h5>
                                <div class="row">
                                    <div class="col-sm-6 col-md-6">
                                        <div class="form-group  text-left">
                                            <label for="dr_ProductosEninventario">Producto en inventario</label>
                                            <asp:DropDownList ID="dr_ProductosEninventario" CssClass="dropdown form-control" runat="server" aria-describedby="drProInv" AutoPostBack="true" OnSelectedIndexChanged="dr_ProductosEninventario_SelectedIndexChanged"></asp:DropDownList>
                                            <small id="drProInv" class="form-text">Todos los productos en inventario</small>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-6">
                                        <div class="form-group  text-left">
                                            <label for="dr_ProductosEninventario">Producto en inventario</label>
                                            <asp:Button ID="btn_DetallesP" runat="server" CssClass="btn btn-primary form-control" UseSubmitBehavior="false" CausesValidation="false" Text="Detalles de este Producto" OnClick="btn_DetallesP_Click" aria-describedby="btnDetallp" />
                                            <small id="btnDetallp" class="form-text">Muestra detalles adicionales como: Existencia, salidas y entradas.</small>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group  text-left">
                                            <label for="txt_CantidadIngresar">Cantidad a Ingresar</label>
                                            <asp:TextBox ID="txt_CantidadIngresar" CssClass="form-control" runat="server" aria-describedby="txtcant"></asp:TextBox>
                                            <small id="txtcant" class="form-text">Indique la cantidad para este producto</small>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group  text-left">
                                            <label for="txt_costodeCompra">Costo del Producto</label>
                                            <asp:TextBox ID="txt_costodeCompra" CssClass="form-control" runat="server" aria-describedby="txtcosto"></asp:TextBox>
                                            <small id="txtcosto" class="form-text">Costo del producto durante su adquisicion</small>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group  text-left">
                                            <label for="txt_PrecioVenta">Precio Venta Producto</label>
                                            <asp:TextBox ID="txt_PrecioVenta" CssClass="form-control" runat="server" aria-describedby="txtpven"></asp:TextBox>
                                            <small id="txtpven" class="form-text">Precio de venta al publico</small>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-12">
                                        <div class="form-group text-left">
                                            <label for="txt_Comentario">Comentario</label>
                                            <asp:TextBox ID="txt_Comentario" CssClass="form-control" runat="server" TextMode="MultiLine" aria-describedby="txtcom"></asp:TextBox>
                                            <small id="txtcom" class="form-text">Comentario sobre este producto</small>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group text-left">
                                            <asp:CheckBox ID="chk_habilitar" Checked="true" runat="server" CssClass="checkbox" Text="&nbsp Habilitado para la Venta" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <asp:Button ID="btn_AgregarALista" runat="server" CssClass="btn btn-info form-control" UseSubmitBehavior="false" CausesValidation="false" Text="Agregar Este producto" OnClick="btn_AgregarALista_Click" />
                                    </div>
                                </div>

                                <br />
                                <asp:GridView ID="gv_productos" DataKeyNames="ID_Inventario" CssClass="table  table-light table-striped table-hover" AutoGenerateColumns="false" runat="server" EnableViewState="False" PageSize="50" AllowPaging="True" OnRowCommand="gv_productos_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nombre Producto">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Bind("NombreProducto") %>' ID="lbl_nom"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Bind("Cantidad") %>' ID="lbl_can"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Costo Compra">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Bind("CosteTotal") %>' ID="lbl_coCom"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Precio Venta">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Bind("PrecioUnidad") %>' ID="lbl_preve"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observaciones">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Bind("Observaciones") %>' ID="lbl_ob"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remover">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Eliminar el item de la base de datos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="remover" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> 
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <div class="row">
                                    <div class="col-sm-6">
                                        <asp:Button ID="btn_Guardar" runat="server" CssClass="btn btn-success form-control" UseSubmitBehavior="false" CausesValidation="false" Text="Guardar los productos en el inventario" OnClick="btn_Guardar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer ">
                        Ingreso a Inventario bajo cuenta de <strong>
                            <asp:LoginName ID="LoginName1" runat="server" />
                        </strong>
                    </div>
                </div>
            </div>
            <%--================================================[DETALLES ADICIONALES]===========================================================================--%>
            <div class="modal fade" id="modalDetalle" tabindex="-1" role="dialog" aria-labelledby="labeInf3o">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="labeInf3o">Detalle Producto</h5>
                            <button type="button" class="close" data-dismiss="modal" data-backdrop="false" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <h6>Detalle del producto en Inventario</h6>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group text-left">
                                        <label for="txt_EntradasTotales">Compras Totales</label>
                                        <asp:TextBox ID="txt_EntradasTotales" CssClass="form-control" runat="server" Enabled="false" aria-describedby="txtEt"></asp:TextBox>
                                        <small id="txtEt" class="form-text">Total de comprar en el sistema para este producto</small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group text-left">
                                        <label for="txt_VentasTotales">Ventas Totales</label>
                                        <asp:TextBox ID="txt_VentasTotales" CssClass="form-control" runat="server" Enabled="false" aria-describedby="txtvt"></asp:TextBox>
                                        <small id="txtvt" class="form-text">Total de Ventas en el sistema para este producto</small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group text-left">
                                        <label for="txt_ExistenciaActual">Existencia Totales</label>
                                        <asp:TextBox ID="txt_ExistenciaActual" CssClass="form-control" runat="server" Enabled="false" aria-describedby="txtte"></asp:TextBox>
                                        <small id="txtte" class="form-text">Total de Existencia en el sistema para este producto</small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal" data-backdrop="false">Cerrar</button>
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
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">
    <%-- <script type="text/javascript" src="../fooTable/js/footable.js"></script>--%>
    <%--<script src="../fooTable/js/footable.js"></script>--%>
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {

            $(document).ready(function () {
                //$('#ContentPlaceHolder1_gv_area').footable();
                //Valida solo numeros enteros
                $("#<%=txt_CantidadIngresar.ClientID%>").on("keypress keyup blur", function (event) {
                    $(this).val($(this).val().replace(/[^\d].+/, ""));
                    if ((event.which < 48 || event.which > 57)) {
                        event.preventDefault();
                    }
                });
                $("#<%=txt_PrecioVenta.ClientID%>").on("keypress keyup blur", function (event) {
                    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                        event.preventDefault();
                    }
                });
                $("#<%=txt_costodeCompra.ClientID%>").on("keypress keyup blur", function (event) {
                    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                        event.preventDefault();
                    }
                });
            });

        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    /*
// Codigo Jquery para validar solo numeros con decimal
$("#approach1").on("keypress keyup blur",function (e) {
$(this).val($(this).val().replace(/[^0-9\.]/g,''));
if ((e.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
event.preventDefault();
}
});
*/

                    //Valida solo numeros enteros
                    $("#<%=txt_CantidadIngresar.ClientID%>").on("keypress keyup blur", function (event) {
                        $(this).val($(this).val().replace(/[^\d].+/, ""));
                        if ((event.which < 48 || event.which > 57)) {
                            event.preventDefault();
                        }
                    });
                    $("#<%=txt_PrecioVenta.ClientID%>").on("keypress keyup blur", function (event) {
                        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                            event.preventDefault();
                        }
                    });
                    $("#<%=txt_costodeCompra.ClientID%>").on("keypress keyup blur", function (event) {
                        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                            event.preventDefault();
                        }
                    });
                }
            });
        };



        //Crea una funcion para controlar el modal desde el codigo
        //function ShowModalAdvertencia() {            
        //    $('#modalAdvertencia').modal("show");
        //}
        //function HideModalAdvertencia() {
        //    $('#modalAdvertencia').modal('hide');
        //}

        //function ShowModalInfo() {
        //    $('#modalInfo').modal('show');
        //}

        function ShowModalDet() {
            $('#modalDetalle').modal('show');
        }
        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }
    </script>
</asp:Content>
