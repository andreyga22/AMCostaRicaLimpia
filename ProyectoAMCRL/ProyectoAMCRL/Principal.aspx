<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="ProyectoAMCRL.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item active" aria-current="page">Registrar y Modificar Socio</li>
        </ol>
    </nav>
    <div class="container">
        <form id="form1" runat="server">
            <br />
            <br />
            <br />
            <div class="row justify-content-center">
                <div class="col-sm-5 offset-1">
                    <div class="card" style="width: 18rem;">
                        <asp:Image ID="imagenSocios" class="card-img-top" runat="server" ImageUrl="~/images/socios.png" />
                        <div class="card-body">
                            <h5 class="card-title">Socios de negocio</h5>
                            <p class="card-text">Agregue o busque los socios de negocio.</p>
                            <asp:DropDownList style="background-color:#8DAA9D" class="btn btn-primary dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" AutoPostBack="True" ID="sociosDrop" runat="server">
                                <asp:ListItem>Agregar Nuevo</asp:ListItem>
                                <asp:ListItem>Buscar Socios</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-sm-5 offset-1">
                    <div class="card" style="width: 18rem;">
                        <asp:Image ID="imagenCompraVenta" class="card-img-top" runat="server" ImageUrl="~/images/compra.png" />
                        <div class="card-body">
                            <h5 class="card-title">Compras o Ventas</h5>
                            <p class="card-text">Realice compras y ventas</p>
                            <asp:DropDownList style="background-color:#8DAA9D" class="btn btn-primary dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" AutoPostBack="True" ID="compraVentaDrop" runat="server">
                                <asp:ListItem>Realizar compra</asp:ListItem>
                                <asp:ListItem>Realizar venta</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-5 offset-1">
                    <div class="card" style="width: 18rem;">
                        <img src="..." class="card-img-top" alt="...">
                        <asp:Image ID="imagenInventario" class="card-img-top" runat="server" ImageUrl="~/images/inventario.png" />
                        <div class="card-body">
                            <h5 class="card-title">Inventario</h5>
                            <p class="card-text">Maneje bodegas, materiales y realice ajustes de inventario.</p>
                            <asp:DropDownList style="background-color:#8DAA9D" class="btn btn-primary dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" AutoPostBack="True" ID="inventarioDrop" runat="server">
                                <asp:ListItem>Busqueda de bodegas</asp:ListItem>
                                <asp:ListItem>Materiales</asp:ListItem>
                                <asp:ListItem>Ajustes de inventario</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-sm-5 offset-1">
                    <div class="card" style="width: 18rem;">
                        <asp:Image ID="imagenFacturas" class="card-img-top" runat="server" ImageUrl="~/images/facturas.png" />
                        <div class="card-body">
                            <h5 class="card-title">Facturas</h5>
                            <p class="card-text">Busque y visualice facturas de compra o venta</p>
                            <asp:DropDownList style="background-color:#8DAA9D" class="btn btn-primary dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" AutoPostBack="True" ID="facturasDrop" runat="server">
                                <asp:ListItem>Busqueda de facturas</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-5 offset-1">
                    <div class="card" style="width: 18rem;">
                        <asp:Image ID="imagenReportes" class="card-img-top" runat="server" ImageUrl="~/images/reportes.png" />
                        <div class="card-body">
                            <h5 class="card-title">Reportes</h5>
                            <p class="card-text">Genere reportes de las entradas y salidas del negocio.</p>
                            <asp:DropDownList style="background-color:#8DAA9D" class="btn btn-primary dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" AutoPostBack="True" ID="reportesDrop" runat="server">
                                <asp:ListItem>Reporte general</asp:ListItem>
                                <asp:ListItem>Gráfico</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-sm-5 offset-1">
                    <div class="card" style="width: 18rem;">
                        <asp:Image ID="imagenOtros" class="card-img-top" runat="server" ImageUrl="~/images/moneda.jpg" />
                        <div class="card-body">
                            <h5 class="card-title">Otros</h5>
                            <p class="card-text">Maneje monedas y unidades de medida del sistema.</p>
                            <asp:DropDownList style="background-color:#8DAA9D" class="btn btn-primary dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" AutoPostBack="True" ID="DropDownList1" runat="server">
                                <asp:ListItem>Monedas</asp:ListItem>
                                <asp:ListItem>Unidades de medida</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />


        </form>
    </div>
</asp:Content>
