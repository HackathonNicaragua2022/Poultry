<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="ConteoPollos.aspx.cs" Inherits="POULTRY.Admin_gerencial.ConteoPollos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CONTEO DE POLLOS</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
    <br />  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer runat="server" Interval="2500" ID="tm_conteo" OnTick="tm_conteo_Tick"></asp:Timer>
            <div class="row">
                <div class="col-sm-12 col-lg-6">
                    <!-- small box -->
                    <div class="small-box bg-info">
                        <div class="inner">
                            <h3>
                                <asp:Label Text="" ID="lbl_patas" runat="server" /></h3>
                            <p>Aves Contadas Automaticamente</p>
                        </div>
                        <div class="icon">
                               <i class="nav-icon fas fa-feather-alt"></i>
                        </div>
                        <div class="small-box-footer">
                            <p>Actualizacion automatica cada 2.5 segundos</p>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
