<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="cambioContrasenausuarioscomedor.aspx.cs" Inherits="POULTRY.mobilnavs.cambioContrasenausuarioscomedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <hr />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row align-items-center justify-content-center">
                <div class="col-sm-6 lign-self-center">
                    <!-- Profile Image -->
                    <div class="card card-success card-outline m-1">
                        <div class="card-body box-profile">
                            <div class="text-center">
                                <img class="profile-user-img img-fluid img-circle"
                                    src="../imagenes/programador.png"
                                    alt="User profile picture">
                            </div>

                            <h3 class="profile-username text-center">CAMBIO DE CONTRASEÑA</h3>

                            <p class="text-muted text-center">Es primera vez que usa el sistema POULTRY, es necesario que actualice su contraseña</p>

                            <ul class="list-group list-group-unbordered mb-3">
                                <li class="list-group-item">
                                    <b>Nueva Contraseña</b>
                                </li>
                                <li class="list-group-item">
                                    <div class="form-group">
                                        <label for="txt_Codigo">Nueva Contraseña</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="fa fa-barcode"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_nuevacontrasena" name="txt_Codigo" placeholder="contraseña" type="password" class="form-control" aria-describedby="txt_CodigoHelpBlock" />
                                        </div>
                                        <span id="txt_CodigoHelpBlock" class="form-text text-muted">Ingrese una contraseña minimo de 5 caracteres alfanumerico</span>
                                    </div>
                                    <div class="form-group">
                                        <label for="text">Repita la contraseña</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="fa fa-key"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_repetircontrasena"  placeholder="contraseña" name="text" type="password" class="form-control" aria-describedby="textHelpBlock" />
                                        </div>
                                        <span id="textHelpBlock" class="form-text text-muted">verifique la contraseña ingresandola otra vez</span>
                                    </div>
                                </li>
                            </ul>
                            <asp:Label Text="" Visible="false" ID="lbl_mensaje" runat="server" CssClass="text-red" />
                            <hr />
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <div class="progress">
                                        <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="100" title="Espere.." aria-valuemin="0" aria-valuemax="100" style="width: 100%">Espere...</div>
                                    </div>
                                    <hr />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:Button Text="Guardar nueva contraseña" runat="server" ID="btn_GuardarContrasena" class="btn btn-success btn-block" OnClick="btn_GuardarContrasena_Click" />
                        </div>
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
