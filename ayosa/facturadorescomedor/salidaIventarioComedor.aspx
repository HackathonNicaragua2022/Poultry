<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="~/facturadorescomedor/salidaIventarioComedor.aspx.cs" Inherits="POULTRY.facturadorescomedor.salidaIventarioComedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Salida Inventario Comedor</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">Home</a></li>
                <li class="breadcrumb-item"><a href="#">Administracion</a></li>
                <li class="breadcrumb-item active" aria-current="page">Salida Inventario Comedor</li>
            </ol>
        </nav>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="card text-white bg-dark mb-3" style="max-width: 100%;">
                    <div class="card-header">Salida Administrador</div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <h5 class="card-title">Salida de productos del inventario comedor</h5>
                                <p class="card-text">Administre los productos en el inventario del comedor</p>
                                <table class="table table-striped table-dark">
                                    <thead>
                                        <tr>
                                            <th scope="col">Datos</th>
                                            <th scope="col">Valor</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Fecha Ingreso al Sistema</td>
                                            <td>
                                                <asp:Label Text="" ID="lbl_fechaIngreso" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>Usuario que Registra el Egreso
                                            </td>
                                            <td>
                                                <asp:LoginName ID="LoginName1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cantidad total a Egresar
                                            </td>
                                            <td>
                                                <asp:Label Text="" ID="lbl_CantidadTotalEgresar" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Costos Totales
                                            </td>
                                            <td>
                                                <asp:Label Text="" ID="lbl_CostoTotales" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Precios de Ventas Totales
                                            </td>
                                            <td>
                                                <asp:Label Text="" ID="lbl_PreciosVentaTotal" runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <div class="card">
                                    <h5 class="card-header bg-dark">Agregue productos a la lista</h5>
                                    <div class="card-body text-black-50">
                                        <h5 class="card-title">Productos a sacar del inventario</h5>
                                        <p class="card-text">Indique los productos a sacar del inventario</p>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <label for="dr_pInventaario">Productos en Inventario</label>
                                                    <asp:DropDownList ID="dr_pInventaario" CssClass="dropdown form-control" runat="server" aria-describedby="drHelp"></asp:DropDownList>
                                                    <small id="drHelp" class="form-text text-muted">Solo se muestran los productos en inventario positivo</small>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="txt_Cantidad">Cantidad</label>
                                                    <asp:TextBox ID="txt_Cantidad" CssClass="form-control" runat="server" aria-describedby="txtHelp"></asp:TextBox>
                                                    <small id="txtHelp" class="form-text text-muted">Cantidad del producto a sacar del inventario</small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label for="txt_Cantidad">Obsaervaciones</label>
                                                    <asp:TextBox ID="txt_Obeservaciones" CssClass="form-control" runat="server" aria-describedby="txtob_Help"></asp:TextBox>
                                                    <small id="txtob_Help" class="form-text text-muted">Cualquier observacion para indicar la salida del producto</small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="btn_Agregar" CssClass="btn btn-info form-control" runat="server" Text="Agregar a la lista" UseSubmitBehavior="false" CausesValidation="false" OnClick="btn_Agregar_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Button ID="btn_Guardar" CssClass="btn btn-success form-control" runat="server" Text="Guardar Salida" UseSubmitBehavior="false" CausesValidation="false" OnClick="btn_Guardar_Click" />
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
                                        <asp:Label runat="server" Text='<%# Bind("CostoUnidad") %>' ID="lbl_coCom"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio Venta">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("PrecioVUnidad") %>' ID="lbl_preve"></asp:Label>
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
                    </div>
                </div>
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
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {

            $(document).ready(function () {
                //Valida solo numeros enteros
                $("#<%=txt_Cantidad.ClientID%>").on("keypress keyup blur", function (event) {
                    $(this).val($(this).val().replace(/[^\d].+/, ""));
                    if ((event.which < 48 || event.which > 57)) {
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
                    $("#<%=txt_Cantidad.ClientID%>").on("keypress keyup blur", function (event) {
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

        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }
    </script>
</asp:Content>
