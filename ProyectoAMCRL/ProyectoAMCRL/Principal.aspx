<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="ProyectoAMCRL.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="container">
        <%--<form id="form1" runat="server">--%>
        <br />


        <div class="row">
            <div class="col-lg-4">
                <div class="card bg-light" style="width: 18rem; height: 14rem;">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Socios de negocio</h5>
                        <p class="card-text">Agregue o busque los socios de negocio.</p>
                        <a href="RegistrosSocioUI.aspx" class="card-link text-primary">Agregar Socio</a>
                        <br />
                        <a href="BusquedaSocios.aspx" class="card-link text-primary">Búsqueda de Socios</a>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card bg-light" style="width: 18rem; height: 14rem;">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Compras o Ventas</h5>
                        <p class="card-text">Realice compras y ventas</p>
                        <br />
                        <a href="#" class="card-link text-primary">Realizar Compra</a>
                        <br />
                        <a href="#" class="card-link text-primary">Realice Venta</a>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card bg-light" style="width: 18rem; height: 14rem;">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Inventario</h5>
                        <p class="card-text">Maneje bodegas, materiales y realice ajustes de inventario.</p>
                        <a href="AdministrarBodegas.aspx" class="card-link text-primary">Administrar bodegas</a>
                        <br />
                        <a href="AdministrarMateriales.aspx" class="card-link text-primary">Materiales</a>
                        <br />
                        <a href="Ajustes.aspx" class="card-link text-primary">Ajustes de inventario</a>
                        <br />

                    </div>
                </div>
            </div>
        </div>

        <br />
        <br />
        <br />
        <div class="row">
            <div class="col-lg-4">
                <div class="card  bg-light" style="width: 18rem; height: 14rem;">
                <div class="card-body">
                        <h5 class="card-title text-secondary">Facturas</h5>
                        <p class="card-text">Busque y visualice facturas de compra o venta</p> 
                       <a href="BusquedaFacturas.aspx" class="card-link text-primary">Búsqueda de facturas</a>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card bg-light" style="width: 18rem; height: 14rem;">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Reportes</h5>
                        <p class="card-text">Genere reportes de las entradas y salidas del negocio.</p>
                          <a href="Reporte_General.aspx" class="card-link text-primary">Reporte General</a>
                        <br />
                        <a href="ReporteGrafico.aspx" class="card-link text-primary">Reporte Gráfico</a>
                        <br />
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card bg-light" style="width: 18rem; height: 14rem;">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Otros</h5>
                        <p class="card-text">Maneje monedas y unidades de medida del sistema.</p>
                              <a href="Monedas.aspx" class="card-link text-primary">Monedas</a>
                        <br />
                        <a href="#" class="card-link text-primary">Unidades de medida</a>
                        <br />
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
