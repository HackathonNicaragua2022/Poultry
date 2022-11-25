<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="inventarioComedorExistencia.aspx.cs" Inherits="POULTRY.mobilnavs.inventarioComedorExistencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inventario Comedor</title>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>POULTRY COMEDOR TE OFRECE LOS SIGUIENTES PRODUCTOS</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="menuComedorTrabajadores.aspx">Menu</a></li>
                        <li class="breadcrumb-item active">PRODUCTOS COMEDOR</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <div class="container-fluid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row  align-items-center justify-content-center">
                    <div class="col-12">
                        <h1>Productos Comedor POULTRY</h1>
                        <div class="card text-white bg-dark mb-3">
                            <div class="card-header">Filtrar por Categoria</div>
                            <div class="card-body">
                                <h5 class="card-title">Filtrar por Categoria</h5>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="dr_Categorias" runat="server" CssClass="dropdown form-control" AutoPostBack="true" OnSelectedIndexChanged="dr_Categorias_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <p class="card-text" style="color: white;">Ver los productos segun la categoria seleccionada</p>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gv_invetario" DataKeyNames="ID_Inventario" runat="server" CssClass="table table-dark table-striped table-hover" AutoGenerateColumns="false" EnableViewState="False">
                    <Columns>
                        <asp:BoundField DataField="MedidaProducto" HeaderText="Medida Producto"></asp:BoundField>
                        <asp:BoundField DataField="NombreProducto" HeaderText="Producto"></asp:BoundField>
                        <asp:BoundField DataField="ExistenciaActual" HeaderText="Existencia"></asp:BoundField>
                        <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script src="../fooTable/js/footable.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $(document).ready(function () {
                $("#<%=gv_invetario.ClientID%>").footable();
                //    $("ContentPlaceHolder1_gv_Facturas_Pendientes_gv_Detalle_0").footable();
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
                    $("#<%=gv_invetario.ClientID%>").footable();
                    //  $("ContentPlaceHolder1_gv_Facturas_Pendientes_gv_Detalle_0").footable();
                    //------------------------------------------                                          
                }
            });
        };
    </script>
</asp:Content>
