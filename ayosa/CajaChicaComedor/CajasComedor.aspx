<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="CajasComedor.aspx.cs" Inherits="POULTRY.CajaChicaComedor.CajasComedor" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reporte Cajas Chicas</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <!-- Bootstrap DatePicker -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-12">
                            <h2>Reporte Cajas Chicas Comedor
                            </h2>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:RadioButtonList ID="lista_Opciones" runat="server" CssClass="form-check form-check-inline" AutoPostBack="true">
                                <asp:ListItem Value="1" Selected="True">&nbsp Todas las Cajas</asp:ListItem>
                                <asp:ListItem Value="2">&nbsp Caja para la Fecha</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div runat="server" id="fechaDiv" visible="false">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon1"><i class="fas fa-calendar"></i></span>
                                    </div>
                                    <asp:TextBox ID="txt_fechaRegistro" runat="server" class="form-control" ReadOnly="true" aria-describedby="basic-addon1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div class="spinner-border text-success" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Button Text="Cargar Reporte" ID="btn_recargarRepo" runat="server" CssClass="btn btn-primary" OnClick="btn_recargarRepo_Click" />
                    <hr />  
                    <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" Theme="Youthful" runat="server"></dx:ASPxDocumentViewer>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScript" runat="server">
    <script src="../fooTable/js/moment.js"></script>
    <script src="../fooTable/js/footable.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <!-- Bootstrap DatePicker -->
    <script type="text/javascript">
        $(function () {
            $('[id*=txt_fechaRegistro]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "es-Es"
            });
        });
    </script>
    <script>
        jQuery(document).ready(function ($) {

            $(document).ready(function () {
                /*
                */
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {

                if (sender._postBackSettings.panelsToUpdate != null) {

                    $(function () {
                        $('[id*=txt_fechaRegistro]').datepicker({
                            changeMonth: true,
                            changeYear: true,
                            format: "dd/mm/yyyy",
                            language: "es-Es"
                        });
                    });
                }
            });
        };
    </script>
</asp:Content>
