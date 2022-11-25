<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="trabajadores.aspx.cs" Inherits="POULTRY.trabajadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Trabajadores en la Empresa</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <fieldset>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-12">
                            <h1>Mantenimiento de Trabajadores</h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#formulario01">
                                Agregar Trabajador <i class="fas fa-plus-circle"></i>
                            </button>
                        </div>
                        <div class="col-sm-3 ">
                            <asp:LinkButton ID="Btn_ExportarExcel" runat="server" CssClass="btn btn-success form-control" OnClick="Btn_ExportarExcel_Click" data-toggle="tooltip" data-placement="top" title="Exporta Todos los empleados de la base de datos de POULTRY WEB">
                                Exportar a Excel &nbsp<i class="fas fa-file-excel"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="btn_ExportarPDF" runat="server" CssClass="btn btn-warning form-control" OnClick="btn_ExportarPDF_Click" data-toggle="tooltip" data-placement="top" title="Exporta Todos los empleados de la base de datos de POULTRY WEB">
                                Exportar a PDF&nbsp<i class="fas fa-file-pdf"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <br />
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


                            <asp:GridView ID="gv_Trabajador" DataKeyNames="ID_Trabajador" OnDataBound="gv_Trabajador_DataBound" CssClass="table table-dark table-striped table-hover" runat="server" EnableViewState="False" AutoGenerateColumns="false" PageSize="50" AllowPaging="True" Style="max-width: 100%" OnRowCommand="gv_Trabajador_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="N°">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:BoundField DataField="CodigoBarra" HeaderText="Codigo Barra"></asp:BoundField>
                                    <asp:BoundField DataField="Cedula" HeaderText="Cedula"></asp:BoundField>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombres">
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="True"></ItemStyle>
                                    </asp:BoundField>                                    
                                    <asp:BoundField DataField="CreadoPor" HeaderText="Creado Por"></asp:BoundField>
                                    <asp:BoundField DataField="NombreArea" HeaderText="Area"></asp:BoundField>
                                    <asp:BoundField DataField="NombreCargo" HeaderText="Cargo"></asp:BoundField>
                                    <asp:CheckBoxField DataField="Activo" HeaderText="Activo"></asp:CheckBoxField>
                                    <asp:TemplateField HeaderText="Editar">
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
                                    <div>
                                        <asp:Label ID="MessageLabel" Text="Ir a la pág." runat="server" /></h5>
                                        <asp:DropDownList ID="PageDropDownList" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="dr_Page_Trabajador_SelectedIndexChanged" runat="server" CssClass="form-control" /></h3>
                                        <asp:Label ID="currentPage_Trabajador" runat="server" CssClass="label label-warning" />
                                    </div>
                                </PagerTemplate>
                                <PagerStyle HorizontalAlign="Center" />
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
                                    <h5 class="modal-title" id="exampleModalLabel">Trabajador</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">                                        
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txt_Cedula">Codigo Barra</label>
                                                <asp:TextBox ID="txt_CodigoBarra" runat="server" class="form-control" required MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">                                        
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txt_Cedula">Cedula</label>
                                                <asp:TextBox ID="txt_Cedula" runat="server" class="form-control" required MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txt_PrimerNOmbre">Nombre Completo</label>
                                                <asp:TextBox ID="txt_PrimerNOmbre" runat="server" class="form-control" required MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>                                    
                                    </div>                                    
                                    <div class="row">
                                        <div class="col-sm-6">

                                            <div class="form-group">
                                                <label for="dr_Categoria">Area Laboral</label>
                                                <asp:DropDownList ID="dr_Area" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">

                                            <div class="form-group">
                                                <label for="dr_Categoria">Puesto Laboral</label>
                                                <asp:DropDownList ID="dr_Cargo" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txt_Clave">Clave inicial de Acesso al Sistema</label>
                                                <asp:TextBox ID="txt_Clave" runat="server" Text="POULTRY 2021" class="form-control" required MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txt_Clave">Clave inicial de Acesso al Sistema</label>
                                                <asp:CheckBox ID="chk_activo" runat="server" Text="Trabajador Activo" Checked="true" class="form-control checkbox"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btn_Guardar" runat="server" Text="Nuevo Trabajador" CssClass="btn btn-info form-control" OnClick="btn_Guardar_Click" />
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

                $('#ContentPlaceHolder1_gv_Trabajador').footable();
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
