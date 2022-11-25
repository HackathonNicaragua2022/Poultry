<%@ Page Title="" Language="C#" MasterPageFile="~/master_login.Master" AutoEventWireup="true" CodeBehind="registrarusuario.aspx.cs" Inherits="POULTRY.registrarusuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            align-items: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container align-items-center">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h1 class="panel-title">NUEVO USUARIO</h1>
                    </div>
                    <div class="panel-body bg-info">
                        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser" ContinueDestinationPageUrl="~/login.aspx" FinishDestinationPageUrl="~/login.aspx">
                            <WizardSteps>
                                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" >
                                    <ContentTemplate>
                                  

                                        <table>
                                            <tr>
                                                <td align="center" colspan="2">Regístrese para obtener una nueva cuenta</td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" AssociatedControlID="UserName" ID="UserNameLabel">Nombre de usuario:</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="UserName"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" ErrorMessage="El nombre de usuario es obligatorio." ValidationGroup="CreateUserWizard1" ToolTip="El nombre de usuario es obligatorio." ID="UserNameRequired">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" AssociatedControlID="Password" ID="PasswordLabel">Contraseña:</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" ErrorMessage="La contrase&#241;a es obligatoria." ValidationGroup="CreateUserWizard1" ToolTip="La contrase&#241;a es obligatoria." ID="PasswordRequired">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" AssociatedControlID="ConfirmPassword" ID="ConfirmPasswordLabel">Confirmar contraseña:</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="ConfirmPassword"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirmar contrase&#241;a es obligatorio." ValidationGroup="CreateUserWizard1" ToolTip="Confirmar contrase&#241;a es obligatorio." ID="ConfirmPasswordRequired">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" AssociatedControlID="Email" ID="EmailLabel">Correo electrónico:</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="Email"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" ErrorMessage="El correo electr&#243;nico es obligatorio." ValidationGroup="CreateUserWizard1" ToolTip="El correo electr&#243;nico es obligatorio." ID="EmailRequired">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" AssociatedControlID="Question" ID="QuestionLabel">Pregunta de seguridad:</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="Question"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Question" ErrorMessage="La pregunta de seguridad es obligatoria." ValidationGroup="CreateUserWizard1" ToolTip="La pregunta de seguridad es obligatoria." ID="QuestionRequired">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" AssociatedControlID="Answer" ID="AnswerLabel">Respuesta de seguridad:</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="Answer"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Answer" ErrorMessage="La respuesta de seguridad es obligatoria." ValidationGroup="CreateUserWizard1" ToolTip="La respuesta de seguridad es obligatoria." ID="AnswerRequired">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" ErrorMessage="Contrase&#241;a y Confirmar contrase&#241;a deben coincidir." Display="Dynamic" ValidationGroup="CreateUserWizard1" ID="PasswordCompare"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color: Red;">
                                                    <asp:Literal runat="server" ID="ErrorMessage" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:CreateUserWizardStep>
                            </WizardSteps>
                        </asp:CreateUserWizard>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
