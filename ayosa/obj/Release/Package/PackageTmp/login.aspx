<%@ Page Title="" Language="C#" MasterPageFile="~/master_login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="POULTRY.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="inner-bg">
        <div class="container">
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3 form-box">
                    <div class="form-top">
                        <div class="form-top">
                            <h1>POULTRY</h1>
                            <h3>Inicie sesion para continuar</h3>                           
                            <img src="imagenes/logop-full.png" style="width:100%"/>
                        </div>                      
                    </div>
                    <div class="form-bottom">
                        <div class="login-form">
                            <asp:Login ID="Login1" class="login-form" runat="server" Width="100%" OnLoggedIn="Login1_LoggedIn" CreateUserUrl="~/registrarusuario.aspx">
                                <LayoutTemplate>
                                    <div class="form-group">
                                        <label class="sr-only" for="UserName">Nombre de usuario</label>
                                        <asp:TextBox runat="server" Text="HKNicaragua" ID="UserName" placeholder="Nombre de Usuario" class="form-username form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" ErrorMessage="El nombre de usuario es obligatorio." ValidationGroup="Login1" ToolTip="El nombre de usuario es obligatorio." ID="UserNameRequired">El nombre de usuario es obligatorio.</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label class="sr-only" for="Password">Password</label>
                                        <asp:TextBox runat="server" placeholder="password" class="form-password form-control" TextMode="Password" ID="Password"></asp:TextBox>
                                        <p>usar @HKNicaragua como contraseña</p>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" ErrorMessage="La contrase&#241;a es obligatoria." ValidationGroup="Login1" ToolTip="La contrase&#241;a es obligatoria." ID="PasswordRequired">La contrase&#241;a es obligatoria.</asp:RequiredFieldValidator>
                                    </div>
                                    <asp:CheckBox runat="server" Visible="false" Text="Record&#225;rmelo la pr&#243;xima vez." ID="RememberMe"></asp:CheckBox>

                                    <asp:Literal runat="server" ID="FailureText" EnableViewState="False"></asp:Literal>                                    
                                    <asp:Button runat="server" class="btn btn-success form-control" CommandName="Login" Text="Inicio de sesi&#243;n" ValidationGroup="Login1" ID="LoginButton"></asp:Button>
                                    <br />  
                                    <br />  
                                    <asp:Button runat="server" class="btn btn-danger form-control" CommandName="Login" Text="Menu Comedor" ValidationGroup="Login1" UseSubmitBehavior="false" CausesValidation="false" PostBackUrl="~/mobilnavs/menuComedorTrabajadores.aspx"></asp:Button>
                                </LayoutTemplate>
                            </asp:Login>                            
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3 social-login">
                    <h3>...o Inicia Sesion con:</h3>
                    <div class="social-login-buttons">
                        <a class="btn btn-link-2" href="registrarusuario.aspx">
                            <i class="fa fa-user-plus"></i>CREAR CUENTA
	                        	</a>
                        <a class="btn btn-link-2" href="#">
                            <i class="fa fa-twitter"></i>Twitter
	                        	</a>
                        <a class="btn btn-link-2" href="#">
                            <i class="fa fa-google-plus"></i>Google Plus
	                        	</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
