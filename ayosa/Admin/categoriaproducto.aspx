<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="categoriaproducto.aspx.cs" Inherits="POULTRY.categoriaproducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Categoria Producto</title>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <%--<link href="../fooTable/css/footable.bootstrap.css" rel="stylesheet" />--%>
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <%--    <script src="../fooTable/js/footable.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>--%>

    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <%--  <link href="../fooTable/css/footable.bootstrap.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <fieldset>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-6">
                            <h1>Mantenimiento de Categorias de Productos</h1>
                        </div>
                        <div class="col-sm-4">
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#formulario01">
                                Agregar Categoria <i class="fas fa-plus-circle"></i>
                            </button>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="alert alert-info" role="alert" runat="server" id="advertenciaAlert" visible="false">
                                <h4 class="alert-heading">
                                    <asp:Label ID="lbl_advertenciaHeader" runat="server" Text="Advertencia!"></asp:Label>
                                </h4>
                                <p>
                                    <marquee>                                                       
                                <asp:Label ID="lbl_CuerpoAdvertencia" runat="server" Text="Algo va mal"></asp:Label>
                                    </marquee>
                                </p>
                                <hr />
                                <p class="mb-0">
                                    <asp:Label ID="lbl_piesAdvertencia" runat="server" Text="Advertencia!"></asp:Label>
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gv_Categoria" DataKeyNames="ID_CategoriaProducto" OnDataBound="gv_Categoria_DataBound" CssClass="table table-dark table-striped table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" PageSize="50" AllowPaging="True" Style="max-width: 100%" OnRowCommand="gv_Categoria_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="N°">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Categoria Producto">
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" Text='<%# Bind("NombreCategoriaProducto") %>' ID="TextBox1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("NombreCategoriaProducto") %>' ID="Label1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Editar"><%--ShowHeader="False"--%>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_actualizar" data-toggle="tooltip" title="Actualiza los datos de la fila" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="actualizar" CssClass="btn btn-warning form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-edit"></i> Editar
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remover">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_eliminar" data-toggle="tooltip" title="Eliminar el item de la base de datos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="eliminar" CssClass="btn btn-danger form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-trash"></i> Eliminar
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
                                            <asp:DropDownList ID="dr_Categoria_Page" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="dr_Categoria_Page_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-10" style="text-align: right;">
                                            <h3>
                                                <asp:Label ID="currentPage_Categoria" runat="server" CssClass="label label-warning" />
                                            </h3>
                                        </div>
                                    </div>
                                </PagerTemplate>
                            </asp:GridView>

                        </div>
                    </div>

                    <%--=========================================================================================================================================--%>
                    <div class="modal fade" id="modalAdvertencia" tabindex="-1" role="dialog" aria-labelledby="labeAdvertencia">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="labeAdvertencia">Atencion!!</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
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

                                                <asp:Button ID="btn_Continuar" runat="server" Text="Continuar" CssClass="btn btn-warning form-control" data-dismiss="modal" data-backdrop="false" OnClick="btn_Continuar_Click" UseSubmitBehavior="false" />
                                            </div>
                                        </div>
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
                    <!-- Modal -->
                    <div class="modal fade" id="formulario01" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Categoria de Productos</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txt_Categoria">Nombre de la Categoria</label>
                                                <asp:TextBox ID="txt_Categoria" runat="server" class="form-control" required MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btn_Guardar" runat="server" Text="Nueva categoria" CssClass="btn btn-info form-control" OnClick="btn_Guardar_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">

    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {

            $(document).ready(function () {

                $('#ContentPlaceHolder1_gv_Categoria').footable();
            });

        });
        //Crea una funcion para controlar el modal desde el codigo
        function ShowModalAdvertencia() {
            //$('#modalAdvertencia').modal({ backdrop: false });
            $('#modalAdvertencia').modal("show");
        }
        function ShowModalInfo() {
            $('#modalInfo').modal('show');
        }
        //El mismo de nuevo y editar
        function ShowModalNuevo() {
            $('#formulario01').modal('show');
        }
    </script>


</asp:Content>
