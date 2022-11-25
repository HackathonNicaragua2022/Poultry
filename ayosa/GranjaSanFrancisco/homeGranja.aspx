<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/masterGranja.Master" AutoEventWireup="true" CodeBehind="homeGranja.aspx.cs" Inherits="POULTRY.Granja.homeGranja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home Granja</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@200&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.9.0/css/all.min.css" />
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../AdminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link href="../fooTable/css/footable.bootstrap.min.css" rel="stylesheet" />
    <style>
        .jumbotron-image {
            background-position: center center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .dropdown-menu {
            background-color: #424240;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron jumbotron-image shadow m-1 border-primary " style="background-image: url(../imagenes/pollos01.jpeg)">
        <div class="row">
            <div class="col text-white">
                <h1 class="mb-4">POULTRY <b>GRANJA DE AVES ENGORDE</b></h1>
                <h3 class="mb-4 lead">El sabor de los Nicas
                </h3>
            </div>
            <div class="col text-white  float-right text-right">
                <asp:Image CssClass="float-right" ImageAlign="Right" ImageUrl="~/imagenes/logop-newfuul.png" runat="server" Width="150px" />
            </div>
        </div>
    </div>
    <br />
    <h1 class="text-info"><i class="fas fa-database"></i>&nbsp; DASHBOARD</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
