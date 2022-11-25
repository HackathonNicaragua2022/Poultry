<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="testsite01.aspx.cs" Inherits="POULTRY.testsite.testsite01" Debug="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../exporttables/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../exporttables/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../exporttables/dataTables.bootstrap4.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- /.row -->
    <br />
    <br />
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Taza de Cambio</h3>

                    <div class="card-tools">
                        <div class="row">
                            <div class="col">
                                <asp:TextBox ID="txt_ano" CssClass="form-control" TextMode="Number" placeHolder="año" runat="server"></asp:TextBox>
                            </div>
                            <div class="col">
                                <asp:TextBox ID="txt_mes" CssClass="form-control" TextMode="Number" placeHolder="mes" runat="server"></asp:TextBox>
                            </div>
                            <div class="col" style="width: 1px;">
                                <asp:Button Text="GO!" runat="server" ID="btn_go" CssClass="btn btn-primary" OnClick="btn_go_Click" />
                            </div>
                            <div class="col">
                                <asp:LinkButton Text="Exportar" runat="server" CssClass="btn btn-success" OnClientClick="exportReportToExcel(this)">
                                    <i class="fas fa-file-excel"></i>&nbsp Exportar
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.card-header -->
                <div class="card-body table-responsive p-0">
                    <table id="tablaTaza" class="table table-hover text-nowrap">
                        <asp:Repeater ID="rp_taza" runat="server">
                            <HeaderTemplate>
                                <thead>
                                    <tr>
                                        <th>Dia</th>
                                        <th>Taza</th>
                                    </tr>
                                </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Fecha","{0:d}")%></td>
                                    <%-- %d para ver solo el dia--%>
                                    <td><%# Eval("Taza","{0:C}")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <!-- /.card-body -->
            </div>
            <!-- /.card -->
        </div>
    </div>
    <!-- /.row -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">    
    <script src="../exporttables/jquery.dataTables.min.js"></script>
    <script src="../exporttables/dataTables.buttons.min.js"></script>
    <script src="../exporttables/buttons.print.min.js"></script>
    <script src="../exporttables/dataTables.bootstrap4.min.js"></script>
    <script src="../exporttables/pdfmake.min.js"></script>
    <script src="../exporttables/vfs_fonts.js"></script>

    <script src="../exporttables/buttons.html5.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tablaTaza').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>
