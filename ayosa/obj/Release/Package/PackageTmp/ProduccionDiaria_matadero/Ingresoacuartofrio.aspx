<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="Ingresoacuartofrio.aspx.cs" Inherits="POULTRY.ProduccionDiaria_matadero.Ingresoacuartofrio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ingreso de Rugros a Cuarto Frio</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <!-- DataTables -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/datatables-buttons/css/buttons.bootstrap4.min.css" />

    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up_principal" runat="server">
        <ContentTemplate>
            <div class="jumbotron m-1 bg-gradient-dark">
                <h3 class="display-4">Ingreso de Rubros a Cuarto Frios</h3>
                <p class="lead">Control de entrada a cuartos Frios para el proceso seleccionado</p>
                <hr class="my-1" />
                <div class="row">
                    <div class="col-md-4 col-sm-12">
                        <label for="txt_fechamatanza">Fecha de Procesos</label>
                        <div class="input-group date" id="datetimepicker1" data-target-input="nearest">
                            <div class="input-group-append" data-target="#datetimepicker1" data-toggle="datetimepicker">
                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                            </div>
                            <asp:TextBox runat="server" ID="txt_FechaMatanza" name="txt_fechamatanza" type="text" aria-describedby="txt_fechamatanzaHelpBlock" class="form-control datetimepicker-input" data-target="#datetimepicker1" EnableViewState="true" />
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12">
                        <label for="btn_CargarProceso">Cargar Proceso</label>
                        <asp:Button ID="btn_CargarProceso" Text="Cargar Pesajes de Rubros" CssClass="btn btn-primary form-control" UseSubmitBehavior="false" CausesValidation="false" runat="server" />
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="row bg-gradient-navy rounded-lg border-white border">
                    <div class="col-md-6 col-sm-12">
                        <p class="lead">Resumen Ingreso de Rubros</p>

                        <div class="table-responsive">
                            <table class="table">
                                <tr>
                                    <th style="width: 50%">AVES SACRIFICADAS</th>
                                    <td>
                                        <asp:Label ID="lbl_AvesTotalesSacrificadas" Text="0" runat="server" />
                                        AVES</td>
                                </tr>
                                <tr>
                                    <th>Libras Peso Vivo</th>
                                    <td>
                                        <asp:Label ID="lbl_LibrasPesoVivo" Text="0" runat="server" />
                                        lbs</td>
                                </tr>
                                <tr>
                                    <th>Peso Promedio</th>
                                    <td>
                                        <asp:Label Text="0" ID="lbl_PesoPromedio" runat="server" />
                                        lbs.</td>
                                </tr>
                                <tr>
                                    <th>Total Libras Congeladas</th>
                                    <td>
                                        <asp:Label Text="0" runat="server" ID="lbl_TotalLibrasCongeladas" />
                                        lbs</td>
                                </tr>

                                <tr>
                                    <th>Total Libras Recibidas en Bodega de Despacho</th>
                                    <td>
                                        <asp:Label Text="0" runat="server" ID="lbl_TotalLibrasRBD" />
                                        lbs</td>
                                </tr>
                                <tr>
                                    <th>Peso Total Entrada Cuarto Frio</th>
                                    <td>
                                        <asp:Label Text="0" runat="server" ID="lbl_TotalEntradaCuartoFrio" />
                                        lbs</td>
                                </tr>
                                <tr>
                                    <th>Peso Total Salida Cuarto Frio</th>
                                    <td>
                                        <asp:Label Text="0" runat="server" ID="lbl_TotalSalidaCuartoFrio" />
                                        lbs</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <p class="lead">&nbsp;</p>

                        <div class="table-responsive">
                            <table class="table">
                                <tr>
                                    <th style="width: 50%">Peso General Total Envio a Planta BH</th>
                                    <td>
                                        <asp:Label Text="0" ID="lbl_PesoGeneralTotalEnvioPBH" runat="server" />
                                        lbs.</td>
                                </tr>
                                <tr>
                                    <th>Peso General Total Recivo a Planta BH</th>
                                    <td>
                                        <asp:Label Text="0" ID="lbl_PesoGeneralTotalRecivoPBH" runat="server" /></td>
                                </tr>
                                <tr>
                                    <th>Rendimiento Canal</th>
                                    <td>
                                        <asp:Label Text="0" ID="lbl_RendimientoCanal" runat="server" />
                                        %</td>
                                </tr>
                                <tr>
                                    <th>Rendimiento Canal Caliente</th>
                                    <td>
                                        <asp:Label Text="0" ID="lbl_RendimientoCanalCaliente" runat="server" />
                                        %</td>
                                </tr>
                                <tr>
                                    <th>Rendimiento Compañia</th>
                                    <td>
                                        <asp:Label Text="0" ID="lbl_RendimientoCompañia" runat="server" />
                                        %</td>
                                </tr>
                                <tr>
                                    <th>Merma de Congelacion Total</th>
                                    <td>
                                        <asp:Label Text="0" ID="lbl_MermaCongelacionTotal" runat="server" />
                                        %</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <br />  
                <div class="row  rounded-lg border-dark border">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title">Rubros por Ave</h3>
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <asp:GridView ID="gv_PesoCortes" runat="server" AutoGenerateColumns="false" DataKeyNames="ID_PesoCortes"
                                    CssClass="table table-bordered table-striped" EmptyDataText="Aun no se agregan pesos de cortes, cuando se agreguen cortes los podra ver aqui"
                                    OnRowCommand="gv_PesoCortes_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_Rubro" HeaderText="Rubro">
                                            <ItemStyle HorizontalAlign="Left" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estado_Rubro" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="Left" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoTotalCorte_ECF" HeaderText="Total Cortes ECF"  DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoTotalCorte_SCF" HeaderText="Total Cortes SCF"  DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoTotalEnvioPlantaBodegaH" HeaderText="Total Envio a PBH" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoTotalReciboPlantaBodegaH" HeaderText="Total Recivo a PBH" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>


                                        <asp:BoundField DataField="MermaCongelacion" HeaderText="Merma de congelacion" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MermaBodegaPlanta" HeaderText="Merma de Planta Bodega" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MermaTraslado" HeaderText="Merma de Traslado" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MermaTotalDespuesCongelacion" HeaderText="Merma despues de conjelacion" DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" CssClass="align-middle" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Pesajes ECF">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_PesajesECF" data-toggle="tooltip" title="Puede ver el total de pesasjes a entrada de cuarto frio" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_ECF" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-cubes"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pesajes SCF">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_PesajesSCF" data-toggle="tooltip" title="Puede ver el total de pesasjes a la salida de cuarto frio" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_SCF" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-cubes"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Pesajes EPBH">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_PesajesEPBH" data-toggle="tooltip" title="Puede ver el total de pesasjes en Envio a Entrada de planta bodega Holdin" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_EPBH" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-cubes"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Pesajes RPBH">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_PesajesRPBH" data-toggle="tooltip" title="Puede ver el total de pesasjes en Recivos a Entrada de planta bodega Holdin" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_RPBH" CssClass="btn btn-info form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-cubes"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reporte">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Reporte" data-toggle="tooltip" title="Genera un reporte para este Rubro" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="cmd_Reporte" CssClass="btn btn-success form-control" UseSubmitBehavior="false">
                                                <i class="fas fa-print"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <!-- /.card-body -->
                        </div>
                        <!-- /.card -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'L',
                //minDate: new Date(),
                format: 'DD/MM/YYYY'
            });
        });

    </script>
</asp:Content>
