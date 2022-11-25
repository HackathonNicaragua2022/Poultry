<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="inventarioBodegaHuevo.aspx.cs" Inherits="POULTRY.mobilnavs.inventarioBodegaHuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inventario Bodea de Huevos</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <asp:UpdatePanel ID="up_principal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="10000"></asp:Timer>--%>
            <div class="card border-dark mb-3">
                <div class="card-header bg-dark">TOTALES&nbsp&nbsp&nbsp&nbsp<asp:CheckBox ID="chk_pararActualizacion" CssClass="checkbox" Text="&nbsp Para Actualizaccion" runat="server" OnCheckedChanged="Unnamed_CheckedChanged" AutoPostBack="true" /></div>
                <div class="card-body text-dark">
                    <h5 class="card-title">Total en Bodega de Huevo</h5>
                    <p class="card-text">Control de Bodega Huevo</p>
                    <%--<p class="card-text">Este contenido se actualiza en tiempo real</p>--%>
                    <div class="row">
                        <div class="col-12">
                            <h1>TOTAl DE CAJILLAS:&nbsp <strong>
                                <asp:Label Text="00" ID="lbl_TotalCajias" runat="server" /></strong></h1>
                        </div>
                    </div>
                    <br />
                    <%--  <div class="row">
                        <div class="col-12">
                            TOTA DE HUEVOS:&nbsp <strong>
                                <asp:Label Text="00" ID="lbl_TotalHuevos" runat="server" /></strong>
                        </div>
                    </div>--%>
                </div>
            </div>
            <hr />
            <asp:GridView
                ID="gv_BodegaHuevo"
                runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="ID_BodegaHuevos"
                CssClass="table table-sm  table-bordered table-dark table-striped" EmptyDataText="No hay productos en bodega">
                <Columns>
                    <asp:BoundField DataField="Producto" HeaderText="Huevo">
                        <ItemStyle HorizontalAlign="Left" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <%-- <asp:BoundField DataField="ENTRADAS" HeaderText="EntradasT" DataFormatString="{0:N}">
                        <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SALIDAS" HeaderText="SalidasT" DataFormatString="{0:N}">
                        <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>--%>
                    <%--<asp:BoundField DataField="EXISTENCIA" HeaderText="Total Huevos(und)" DataFormatString="{0:N}">
                        <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="TOTAL_CAJIAS" HeaderText="Total Cajillas" DataFormatString="{0:N}">
                        <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="PRECIO_VENTA_ALDETALLE" HeaderText="Precio Venta General Detalle" DataFormatString="{0:C}">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PRECIO_VENTA_MAYORISTA" HeaderText="Precio Venta General Mayoristas" DataFormatString="{0:C}">
                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>--%>
                </Columns>
            </asp:GridView>

            <%--=========================================================================================================================================--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>

    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_BodegaHuevo.ClientID%>").footable();
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
                    $("#<%=gv_BodegaHuevo.ClientID%>").footable();
                    //------------------------------------------                                          
                }
            });
        };
    </script>
</asp:Content>
