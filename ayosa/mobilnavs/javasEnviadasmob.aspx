<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="javasEnviadasmob.aspx.cs" Inherits="POULTRY.mobilnavs.javasEnviadasmob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Javas enviadas en la Remision</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Javas enviadas en la Remision</h3>
    <p>Muestra como estan distribuidas las javas enviadas en esta remision</p>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up_principal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView
                ID="gv_Pesajes"
                runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="ID_JavasEnviadas"
                CssClass="table table-sm table-condensed  table-bordered table-striped" EmptyDataText="Ho hay contenido para esta remision, favor notificar al desarrollador">
                <Columns>
                    <asp:BoundField DataField="Cantidad_Javas" HeaderText="Cantidad Javas">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PollosPorJavas" HeaderText="Broilers por Java">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SubTotalAves" DataFormatString="{0:n}" HeaderText="SubTotal">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>                                                
                </Columns>
            </asp:GridView>       
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
