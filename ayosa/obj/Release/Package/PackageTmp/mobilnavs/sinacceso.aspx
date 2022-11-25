<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="sinacceso.aspx.cs" Inherits="POULTRY.mobilnavs.sinacceso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>No tiene permiso</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center bg-danger">
        <h3>No tiene Acceso para estar en este Sitio</h3>                
    </div>
    <div  class="d-flex justify-content-center">   
        <h4>No ha proporcionado credenciales validas</h4>
    </div>
    <div  class="d-flex justify-content-center">   
        <asp:Image ImageUrl="~/imagenes/POULTRY.png" runat="server" Width="50%" />
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
