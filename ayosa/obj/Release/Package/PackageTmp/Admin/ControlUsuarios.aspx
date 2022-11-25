<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="ControlUsuarios.aspx.cs" Inherits="POULTRY.ControlUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>USUARIOS</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />

    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />


    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h3>Asignar miembro a un Roll de Trabajo o Restablecer</h3>
        <hr />
        <div class="row">
            <div class="col-xs-6 col-sm-3">
                <label for="dr_Usuarios">Usuarios</label>
                <asp:DropDownList ID="dr_Usuarios" runat="server" CssClass="form-control" OnSelectedIndexChanged="dr_Usuarios_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-xs-6 col-sm-3">
                <label for="dr_roles">Roles de Usuarios</label>
                <asp:DropDownList ID="dr_roles" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-xs-6 col-sm-3">
                <label for="btn_guardar">Guardar Cambios</label>
                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="btn btn-primary form-control" OnClick="btn_guardar_Click" />
            </div>

        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-3">
                <label for="btn_Suspender"></label>
                <asp:Button ID="btn_Suspender" runat="server" Text="Suspender" CssClass="btn btn-primary form-control" OnClick="btn_Suspender_Click" />
            </div>
            <div class="col-xs-6 col-sm-3">
                <label for="btn_reactivar"></label>
                <asp:Button ID="btn_reactivar" runat="server" Text="Restablecer" CssClass="btn btn-primary form-control" OnClick="btn_reactivar_Click" />
            </div>
            <div class="col-xs-6 col-sm-3">
                <label for="btn_Eliminar"></label>
                <asp:Button ID="btn_Eliminar" runat="server" Text="Eliminar Usuario" CssClass="btn btn-danger form-control" OnClick="btn_Eliminar_Click" />
            </div>
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

        <h2>Usuarios Miembros de POULTRY WEB</h2>
        <hr />
        <asp:GridView ID="gv_miembros" runat="server" CssClass="table table-dark table-striped table-hover" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="Nombre"></asp:BoundField>
                <asp:BoundField DataField="Email" HeaderText="E-mail"></asp:BoundField>
                <asp:BoundField DataField="PasswordQuestion" HeaderText="Pregunta de seguridad"></asp:BoundField>
                <asp:CheckBoxField DataField="IsApproved" Text="Aprobado" HeaderText="Esta Aprobado"></asp:CheckBoxField>
                <asp:CheckBoxField DataField="IsLockedOut" Text="Suspendida" HeaderText="Cuenta Suspendida"></asp:CheckBoxField>
                <asp:BoundField DataField="LastLockoutDate" DataFormatString="{0:f}" HeaderText="Fecha de Suspenci&#243;n"></asp:BoundField>
                <asp:BoundField DataField="CreationDate" DataFormatString="{0:f}" HeaderText="Fecha de Creaci&#243;n"></asp:BoundField>
                <asp:BoundField DataField="LastLoginDate" DataFormatString="{0:f}" HeaderText="Ultimo Inicion de Sesi&#243;n"></asp:BoundField>
                <asp:BoundField DataField="LastActivityDate" DataFormatString="{0:f}" HeaderText="Ultima Actividad"></asp:BoundField>
                <asp:BoundField DataField="LastPasswordChangedDate" DataFormatString="{0:f}" HeaderText="fecha del ultimo cambio de contrase&#241;a"></asp:BoundField>
                <asp:CheckBoxField DataField="IsOnline" Text="OnLine" HeaderText="Esta Conectado?"></asp:CheckBoxField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $('#ContentPlaceHolder1_gv_miembros').footable();
            });
        });
    </script>
</asp:Content>
