<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobilnav.Master" AutoEventWireup="true" CodeBehind="menuComedorTrabajadores.aspx.cs" Inherits="POULTRY.mobilnavs.menuComedorTrabajadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .img-responsive {
            display: block;
            margin: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center justify-content-center">
        <div class="col-sm-6 lign-self-center">
            <!-- Profile Image -->
            <div class="card card-primary card-outline m-1">
                <div class="card-body box-profile">
                    <div class="text-center">
                        <img class="profile-user-img img-fluid img-circle"
                            src="../imagenes/logop-w.png"
                            alt="User profile picture" />
                    </div>

                    <h3 class="profile-username text-center">POULTRY SYSTEM S.A</h3>

                    <p class="text-muted text-center">Menú de Comedor</p>


                    <div class="row">
                        <div class="col-lg-6 col-sm-12">
                            <!-- small card -->
                            <div class="small-box bg-info">
                                <div class="inner">
                                    <h3>
                                        <asp:Label Text="00" ID="lbl_TotalFacturaContado" runat="server" /></h3>

                                    <p>Total de Facturas de contado</p>
                                </div>
                                <div class="icon">
                                    <i class="fas fa-shopping-cart"></i>
                                </div>
                                <a href="FacturasContadoComedor.aspx" class="small-box-footer">Click para Detalle <i class="fas fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-sm-12">
                            <!-- small card -->
                            <div class="small-box bg-success">
                                <div class="inner">
                                    <h3>
                                        <asp:Label Text="00" ID="lbl_TotalFacturasCredito" runat="server" /></h3>
                                    <p>Total de Facturas al Credito</p>
                                </div>
                                <div class="icon">
                                    <i class="fas fa-shopping-cart"></i>
                                </div>
                                <a href="FacturasCreditosComedor.aspx" class="small-box-footer">Click para Detalle <i class="fas fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div>
                        <!-- ./col -->
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-sm-12">
                            <!-- small card -->
                            <div class="small-box bg-gradient-indigo">
                                <div class="inner">
                                    <h3>Desayuno</h3>
                                    <h4>
                                        <asp:Label Text="Esperando..." ID="lbl_Desayuno" runat="server" /></h4>
                                </div>
                                <div class="icon">
                                    <asp:Image ImageUrl="~/imagenes/desayuno.png" runat="server" class="img-responsive" />
                                </div>
                                <a href="#" class="small-box-footer">Desayuno del Dia
                                </a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-4 col-sm-12">
                            <!-- small card -->
                            <div class="small-box bg-gradient-yellow">
                                <div class="inner">
                                    <h3>Almuerzo</h3>
                                    <h4>
                                        <asp:Label Text="Esperando..." ID="lbl_Almuerzo" runat="server" /></h4>
                                </div>
                                <div class="icon">
                                    <asp:Image ImageUrl="~/imagenes/almuerzo.png" runat="server" class="img-responsive" />
                                </div>
                                <a href="#" class="small-box-footer">Almuerzo del Dia
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <!-- small card -->
                            <div class="small-box bg-gradient-navy">
                                <div class="inner">
                                    <h3>Cena</h3>
                                    <h4>
                                        <asp:Label Text="Esperando..." ID="lbl_Cena" runat="server" /></h4>
                                </div>
                                <div class="icon">
                                    <asp:Image ImageUrl="~/imagenes/cena-de-navidad.png" runat="server" class="img-responsive" />
                                </div>
                                <a href="#" class="small-box-footer">Cena del Dia
                                </a>
                            </div>
                        </div>
                        <!-- ./col -->
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-sm-12">
                            <!-- small card -->
                            <div class="small-box bg-fuchsia">
                                <div class="inner">
                                    <h5 style="word-wrap:hyphenate;">Productos POULTRY</h5>

                                    <p>Todos los Productos que pone a su dispocicion el Comedor</p>
                                </div>
                                <div class="icon">
                                    <i class="fas fa-cube"></i>
                                </div>
                                <a href="inventarioComedorExistencia.aspx" class="small-box-footer">Click para Detalle <i class="fas fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>


                <nav class="navbar navbar-expand navbar-primary navbar-dark">
                    <!-- Left navbar links -->
                    <ul class="navbar-nav">
                        <li class="nav-item d-none d-sm-inline-block">
                            <a href="#" class="nav-link">HOLA
                                <asp:Label ID="lbl_Usuario" runat="server" Text="Usuario"></asp:Label>
                                !!</a>
                        </li>
                    </ul>

                    <!-- Right navbar links -->
                    <ul class="navbar-nav ml-auto">
                        <!-- Navbar Search -->
                        <li class="nav-item">
                            <a class="nav-link" data-widget="navbar-search" data-target="#navbar-search3" href="#" role="button">
                                <i class="fas fa-search"></i>
                            </a>
                            <div class="navbar-search-block" id="navbar-search3">
                                <div class="input-group input-group-sm">
                                    <asp:TextBox runat="server" class="form-control form-control-navbar" type="search" placeholder="ingrese el número de Factura a buscar" aria-label="Search" ID="txt_BuscarFactura" AutoPostBack="true" OnTextChanged="txt_BuscarFactura_TextChanged" />
                                    <div class="input-group-append">
                                        <button class="btn btn-navbar" type="submit">
                                            <i class="fas fa-search"></i>
                                        </button>
                                        <button class="btn btn-navbar" type="button" data-widget="navbar-search">
                                            <i class="fas fa-times"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </li>

                        <!-- Messages Dropdown Menu -->
                        <li class="nav-item dropdown">
                            <a class="nav-link" data-toggle="dropdown" href="#">
                                <i class="far fa-user"></i>
                                <span class="badge badge-danger navbar-badge"></span>
                            </a>
                            <div class="dropdown-menu dropdown-menu-lg dropdown-menu-right">
                                <a href="#" class="dropdown-item">
                                    <!-- Message Start -->
                                    <div class="media">
                                        <img src="../imagenes/team.png" alt="User Avatar" class="img-size-50 mr-3 img-circle" />
                                        <div class="media-body">
                                            <h3 class="dropdown-item-title">
                                                <asp:Label Text="Nombre Usuario" ID="lbl_nombreUsuario" runat="server" />

                                                <span class="float-right text-sm text-danger"><i class="fas fa-star"></i></span>
                                            </h3>
                                            <p class="text-sm">
                                                <asp:Label Text="Cargo" runat="server" ID="lbl_Cargo" />
                                            </p>

                                        </div>
                                    </div>
                                    <!-- Message End -->
                                    <!-- Message Start -->
                                    <div class="media">
                                        <img src="../imagenes/iniciar-sesion.png" alt="User Avatar" class="img-size-50 mr-3 img-circle" />
                                        <div class="media-body">
                                            <h3 class="dropdown-item-title">
                                                <asp:LinkButton Text="CERRAR SESION" runat="server" ID="btn_CerrarSession" OnClick="btn_CerrarSession_Click" />
                                                <span class="float-right text-sm text-danger"><i class="fas fa-star"></i></span>
                                            </h3>
                                        </div>
                                    </div>
                                    <!-- Message End -->
                                </a>
                            </div>
                        </li>
                        <!-- Notifications Dropdown Menu -->
                        <%-- <li class="nav-item dropdown">
                            <a class="nav-link" data-toggle="dropdown" href="#">
                                <i class="far fa-bell"></i>
                                <span class="badge badge-warning navbar-badge">15</span>
                            </a>
                            <div class="dropdown-menu dropdown-menu-lg dropdown-menu-right">
                                <span class="dropdown-item dropdown-header">15 Notifications</span>
                                <div class="dropdown-divider"></div>
                                <a href="#" class="dropdown-item">
                                    <i class="fas fa-envelope mr-2"></i>4 new messages
                             
                                    <span class="float-right text-muted text-sm">3 mins</span>
                                </a>
                                <div class="dropdown-divider"></div>
                                <a href="#" class="dropdown-item">
                                    <i class="fas fa-users mr-2"></i>8 friend requests
                             
                                    <span class="float-right text-muted text-sm">12 hours</span>
                                </a>
                                <div class="dropdown-divider"></div>
                                <a href="#" class="dropdown-item">
                                    <i class="fas fa-file mr-2"></i>3 new reports
                             
                                    <span class="float-right text-muted text-sm">2 days</span>
                                </a>
                                <div class="dropdown-divider"></div>
                                <a href="#" class="dropdown-item dropdown-footer">See All Notifications</a>
                            </div>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-widget="fullscreen" href="#" role="button">
                                <i class="fas fa-expand-arrows-alt"></i>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-widget="control-sidebar" data-slide="true" href="#" role="button">
                                <i class="fas fa-th-large"></i>
                            </a>
                        </li>--%>
                    </ul>
                </nav>
                <!-- /.card-body -->
            </div>
            <!-- /.card -->
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
