<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="menuInventarios.aspx.cs" Inherits="POULTRY.mobilnavs.menuInventarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card border-dark mb-3">
        <div class="card-header">APLICACION POULTRY</div>
        <div class="card-body text-dark">
            <h5 class="card-title">MONITOREO DE INVENTARIOS</h5>
            <p class="card-text">Seleccione una opcion para ver el estado en tiempo real del inventario</p>
            <div class="row">
                <div class="col-12">
                    <asp:Button Text="BODEGA DE HUEVOS" CssClass="btn btn-primary form-control" UseSubmitBehavior="false" CausesValidation="false" ID="btn_BodegaHuevo" PostBackUrl="~/mobilnavs/inventarioBodegaHuevo.aspx" runat="server" />
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-12">
                    <asp:Button Text="INVENTARIO HUEVO SIN CLASIFICAR" CssClass="btn btn-success form-control" UseSubmitBehavior="false" CausesValidation="false" ID="Button3" PostBackUrl="~/mobilnavs/inventariomob.aspx" runat="server" />
                </div>
            </div>                     
            <hr />
            <div class="row">
                <div class="col-12">
                    <asp:Button Text="INVENTARIO COMEDOR POULTRY" CssClass="btn btn-warning form-control" UseSubmitBehavior="false" CausesValidation="false" ID="btn_Comedor" PostBackUrl="~/mobilnavs/inventarioComedorExistencia.aspx" runat="server" />
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-12">
                    <asp:Button Text="RESUMEN DE PROCESO MATANZA" CssClass="btn btn-success form-control" UseSubmitBehavior="false" CausesValidation="false" ID="Button1" PostBackUrl="~/Admin-gerencial/ResumenPesoVivo.aspx" runat="server" />
                </div>
            </div>
              <hr />
            <div class="row">
                <div class="col-12">
                    <asp:Button Text="GRANJA SAN FRANCISCO" CssClass="btn btn-info form-control" UseSubmitBehavior="false" CausesValidation="false" ID="Button2" PostBackUrl="~/mobilnavs/inventarioGranjaEngorde.aspx" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
