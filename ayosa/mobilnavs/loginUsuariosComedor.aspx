<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="loginUsuariosComedor.aspx.cs" Inherits="POULTRY.mobilnavs.loginUsuariosComedor" %>

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
                    <div class="card card-primary card-outline m-1">
                        <div class="card-body box-profile">
                            <div class="text-center">
                                <img class="profile-user-img img-fluid img-circle"
                                    src="../imagenes/logop-w.png"
                                    alt="User profile picture">
                            </div>

                            <h3 class="profile-username text-center">POULTRY SYSTEM S.A</h3>

                            <p class="text-muted text-center">Iniciar sesion</p>

                            <ul class="list-group list-group-unbordered mb-3">
                                <li class="list-group-item">
                                    <b>Ingresa tus Credenciales</b>
                                </li>
                                <li class="list-group-item">
                                    <div class="form-group">
                                        <label for="txt_Codigo">Código</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="fa fa-barcode"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_codigo" name="txt_Codigo" placeholder="A00-000" type="text" class="form-control" aria-describedby="txt_CodigoHelpBlock" />
                                        </div>
                                        <span id="txt_CodigoHelpBlock" class="form-text text-muted">Ingrese el código que se le asigno a su cuenta</span>
                                    </div>
                                    <div class="form-group">
                                        <label for="text">Contraseña</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <i class="fa fa-key"></i>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="txt_contrasena" name="text" type="password" class="form-control" aria-describedby="textHelpBlock" />
                                        </div>
                                        <span id="textHelpBlock" class="form-text text-muted">contraseña de usuario</span>
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
                            <asp:Button Text="Ingresar" runat="server" ID="btn_Ingresar" class="btn btn-primary btn-block" OnClick="btn_Ingresar_Click" />
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
