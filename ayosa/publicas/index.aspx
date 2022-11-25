<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master01a.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="POULTRY.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header bg-info">
                        <div class="text-white">
                            Opciones Comedor
                        </div>
                    </div>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item">Ingreso de Inventario a Comedor &nbsp&nbsp <a href="../facturadorescomedor/IngresoComedor.aspx" class="btn btn-info">Click aqui</a></li>
                        <li class="list-group-item">Salida de Inventario a Comedor &nbsp&nbsp<a href="../facturadorescomedor/salidaIventarioComedor.aspx" class="btn btn-info">Click aqui</a></li>
                        <li class="list-group-item">Inventario General Comedor &nbsp&nbsp<a href="../facturadorescomedor/inventarioComedor.aspx" class="btn btn-info">Click aqui</a></li>
                    </ul>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h4 class="card-title">COMEDOR</h4>
                                        <p class="card-text">Administre el comedor y sus servicios</p>
                                        <a href="../facturadorescomedor/comedor.aspx" class="btn btn-primary">Ir a Comedor</a>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="float-right">
                                            <img class="card-img-top" src="../imagenes/blog-comedores-industriales.jpg" style="max-width: 300px;" alt="Card image" />
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-md-6 ">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h4 class="card-title">CAJA CHICA COMEDOR</h4>
                                        <p class="card-text">Administre la Caja Chica del comedor</p>
                                        <a href="../CajaChicaComedor/cajaChicaComedor.aspx" class="btn btn-primary">Ir a CAJA</a>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="float-right">
                                            <img class="card-img-top" src="../imagenes/CajaChica.jpg" style="max-width: 300px;" alt="Card image" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
