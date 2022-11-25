<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="pesajesxremision.aspx.cs" Inherits="POULTRY.mobilnavs.pesajesxremision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Pesajes por Remision</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Pesajes para la Remision</h3>
    <p>Puede Administrar los pesajes en la nube</p>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up_principal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView
                ID="gv_Pesajes"
                runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="ID_IngresoPesoVivo"
                CssClass="table table-sm table-condensed  table-bordered table-striped" EmptyDataText="Aun no se ingresa pesajes para esta remision - Sin Pesajes"
                OnRowCommand="gv_Pesajes_RowCommand">
                <Columns>
                    <asp:BoundField DataField="NumPesaje" HeaderText="Pesaje">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CantidadJavasPesadas" HeaderText="Javas">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PollosxJava" HeaderText="Pollos x Java">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalPollos" HeaderText="Total Pollos">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PesoJavaConPollo_Libras" HeaderText="PesoBáscula" DataFormatString="{0:n}">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PesoxJavaVacia_libDefault" HeaderText="PesoxJava x Vacia" DataFormatString="{0:n}">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PesoNetoPollosLibra" HeaderText="Peso Neto Pollos" DataFormatString="{0:n}">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PesoPromedioxPollo_lib" HeaderText="Peso Promedio" DataFormatString="{0:n}">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_Editar" data-toggle="tooltip" title="Seleccione este Proceso para agregar Remisiones" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Editar" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-edit"></i>
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_Eliminar" data-toggle="tooltip" title="Seleccione este Proceso para agregar Remisiones" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Eliminar" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-close"></i>
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <%--=========================================================================================================================================--%>
            <!-- Modal -->
            <div class="modal fade" id="Modal_editarPesaje" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Modificar Pesaje</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up_actualizarEditar" runat="server">
                                <ProgressTemplate>
                                    <div class="progress">
                                        <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 100%"></div>
                                    </div>
                                    <h4>Espere por favor...</h4>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="up_actualizarEditar" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txt_NumeroJavas">Cantidad de Javas Pesadas</label>
                                            </div>
                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend">
                                                    <asp:LinkButton ID="btn_refrescar2" data-toggle="tooltip" title="Click para actualizar los valores del pesaje" runat="server" CausesValidation="false" class="btn btn-outline-secondary" UseSubmitBehavior="false" OnClick="btn_Regarcar1_Click">
                                                <i class="fas fa-refresh"></i>
                                                    </asp:LinkButton>
                                                </div>
                                                <asp:TextBox ID="txt_NumeroJavas" runat="server" CssClass="form-control" TextMode="Number" MaxLength="10" required min="1" placeholder="" aria-label="" aria-describedby="basic-addon1"></asp:TextBox>
                                            </div>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="alert alert-danger" ControlToValidate="txt_NumeroJavas" runat="server" ErrorMessage="Ingrese solo Numeros enteros" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txt_NumeroAvesxJava">Numero de Aves por Java </label>
                                            </div>

                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend">
                                                    <asp:LinkButton ID="btn_Regarcar1" data-toggle="tooltip" title="Click para actualizar los valores del pesaje" runat="server" CausesValidation="false" class="btn btn-outline-secondary" UseSubmitBehavior="false" OnClick="btn_Regarcar1_Click">
                                                <i class="fas fa-refresh"></i>
                                                    </asp:LinkButton>
                                                </div>
                                                <asp:TextBox ID="txt_NumeroAvesxJava" runat="server" CssClass="form-control" TextMode="Number" MaxLength="10" required min="1" placeholder="" aria-label="" aria-describedby="basic-addon1"></asp:TextBox>
                                            </div>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="alert alert-danger" ControlToValidate="txt_NumeroAvesxJava" runat="server" ErrorMessage="Ingrese solo Numeros enteros" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btn_Guardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-info form-control" UseSubmitBehavior="false" CausesValidation="false" data-dismiss="modal" data-backdrop="false" OnClick="btn_Guardar_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <h5>Propiedades del pesaje</h5>
                                    <table class="table">
                                        <tbody>
                                            <tr>
                                                <th scope="row">N*</th>
                                                <td>
                                                    <asp:Label ID="lbl_NumeroPesaje" Text="0" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Total Pollos</th>
                                                <td>
                                                    <asp:Label ID="lbl_TotalPollo" Text="0" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Peso Java con Pollos</th>
                                                <td>
                                                    <asp:Label ID="lbl_PesoJavasConPollos" Text="0" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Peso Java Vacia</th>
                                                <td>
                                                    <asp:Label ID="lbl_PesoJavaVacia" Text="0" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Peso Neto Java Vacia</th>
                                                <td>
                                                    <asp:Label ID="lbl_PesoNetoJavaVacia" Text="0" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Peso Neto Pollos</th>
                                                <td>
                                                    <asp:Label ID="lbl_PesoNetoPollos" Text="0" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Peso Promedio</th>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="lbl_PesoPromedio" Text="0" runat="server" /></strong>
                                                </td>
                                            </tr>
                                        </tbody>

                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-backdrop="false" data-dismiss="modal">Cerrar</button>
                            <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                        </div>
                    </div>
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

            <%--=========================================================================================================================================--%>
            <div class="modal fade" id="modalAdvertencia" tabindex="-1" role="dialog" aria-labelledby="labeAdvertencia">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="labeAdvertencia">Atencion!!</h5>
                            <button type="button" class="close" data-dismiss="modal" data-backdrop="false" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <div class="text-center">
                                <img id="Img1" src="~/imagenes/warning.png" alt="Ok" class="img-responsive center-block" runat="server" /><hr />
                                <asp:Label ID="lbl_MsgAdvertencia" runat="server" Text="Label" Width="50%">mensaje</asp:Label>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12">
                                        <label for="btn_Continuar"></label>

                                        <%--Elimina el fondo negro que queda al cerrar el control--%>
                                        <%--data-dismiss="modal" data-backdrop="false"--%>

                                        <asp:Button ID="btn_Continuar" runat="server" Text="Aceptar y Continuar" CssClass="btn btn-warning form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>

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
                $("#<%=gv_Pesajes.ClientID%>").footable();
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
                    $("#<%=gv_Pesajes.ClientID%>").footable();
                    //------------------------------------------                                          
                }
            });
        };
        function ShowModalAdvertencia() {
            //$('#modalAdvertencia').modal({ backdrop: false });
            $('#modalAdvertencia').modal("show");
        }
        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }
        //Modal para editar los pesajes
        function ShowModal_Editar() {
            $('#Modal_editarPesaje').modal('show');
        }
        function ShowtoastMessage() {
            $('.toast').toast('show');
        }
    </script>
</asp:Content>
