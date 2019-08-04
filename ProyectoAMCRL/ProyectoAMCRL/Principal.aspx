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
            <div class="col-sm-6">
                <div class="card bg-light">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Últimos clientes registrados</h5>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subCliente1" runat="server" OnClick="click_SeleccCliente1"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subCliente2" runat="server" OnClick="click_SeleccCliente2"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subCliente3" runat="server" OnClick="click_SeleccCliente3"></asp:LinkButton>

                        <asp:LinkButton Style="color: dodgerblue" ID="clienteNoHay" runat="server" OnClick="click_SeleccSocioNo"></asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <div class="card bg-light">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Últimos proveedores registrados</h5>
                        <asp:LinkButton Style="color: dodgerblue" ID="subProv1" runat="server" OnClick="click_SeleccProveed1"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subProv2" runat="server" OnClick="click_SeleccProveed2"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subProv3" runat="server" OnClick="click_SeleccProveed3"></asp:LinkButton>

                        <asp:LinkButton Style="color: dodgerblue" ID="proveedNoHay" runat="server" OnClick="click_SeleccSocioNo"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-sm-6">
                <div class="card bg-light">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Materiales principales</h5>
                        <asp:LinkButton Style="color: dodgerblue" ID="subMat1" runat="server" OnClick="click_SeleccMat1"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subMat2" runat="server" OnClick="click_SeleccMat2"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subMat3" runat="server" OnClick="click_SeleccMat3"></asp:LinkButton>

                        <asp:LinkButton Style="color: dodgerblue" ID="matNoHay" runat="server" OnClick="click_SeleccMatNo"></asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <div class="card bg-light">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Facturas del mes</h5>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subCompras" runat="server" OnClick="click_BusquedaFactC"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="subVentas" runat="server" OnClick="click_BusquedaFactV"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
         <br />
        <div class="row">
            <div class="col-sm-6">
                <div class="card bg-light">
                    <div class="card-body">
                        <h5 class="card-title text-secondary">Últimas facturas</h5>

                        <asp:LinkButton Style="color: dodgerblue" ID="sub1Fac" runat="server" OnClick="click_SeleccFact1"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="sub2Fac" runat="server" OnClick="click_SeleccFact2"></asp:LinkButton>
                        <br />
                        <asp:LinkButton Style="color: dodgerblue" ID="sub3Fac" runat="server" OnClick="click_SeleccFact3"></asp:LinkButton>
                        <asp:LinkButton Style="color: dodgerblue" ID="factNoHay" runat="server" OnClick="click_NuevaFact"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        </div>
</asp:Content>
