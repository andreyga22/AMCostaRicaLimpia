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
                        <h5 class="card-title text-secondary">Últimos clientes registrados</h5>
                        <p runat="server" id="sub1Clientes" class="card-text"></p>
                        <br />
                        <p runat="server" id="sub2Clientes" class="card-text"></p>
                        <br />
                        <p runat="server" id="sub3Clientes" class="card-text"></p>
                        <br />
                        <a href="" id="prueba" runat="server" class="card-link text-primary"></a>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card bg-light" style="width: 18rem; height: 14rem;">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Últimos proveedores registrados</h5>
                        <p runat="server" id="subProveed1" class="card-text"></p>
                        <br />
                        <p runat="server" id="subProveed2" class="card-text"></p>
                        <br />
                        <p runat="server" id="subProveed3" class="card-text"></p>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card bg-light" style="width: 18rem; height: 14rem;">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Materiales más vendidos</h5>
                        <p runat="server" id="subMater1" class="card-text"></p>
                        <br />
                        <p runat="server" id="subMater2" class="card-text"></p>
                        <br />
                        <p runat="server" id="subMater3" class="card-text"></p>
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
                        <a href="BusquedaFacturas.aspx" class="card-link text-primary">Búsqueda facturas</a>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card bg-light" style="width: 18rem; height: 14rem;">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Últimas Facturas</h5>
                        <br />
                        <%--<a onclick="click_SeleccFact1"  id="sub1Fact" runat="server" class="card-link text-primary"></a>--%>
                  
                            <%--<asp:LinkButton ID="sub1Fact" runat="server" ></asp:LinkButton>--%>
                        <asp:LinkButton ID="sub1Fac" runat="server" OnClick="click_SeleccFact1"></asp:LinkButton>
                        <br />
                        <a onclick="click_SeleccFact2" id="sub2Fact" runat="server" class="card-link text-primary"></a>
                        <br />
                        <a onclick="click_SeleccFact3" id="sub3Fact" runat="server" class="card-link text-primary"></a>
                        <br />
                    </div>
                </div>
            </div>

            <%--   <div class="col-lg-4">
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
            </div>--%>
        </div>
    </div>

</asp:Content>
