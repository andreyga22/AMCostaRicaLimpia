<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroMateriales.aspx.cs" Inherits="ProyectoAMCRL.RegistroMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarMateriales.aspx" style="color: dodgerblue">Materiales</a></li>
    <li class="breadcrumb-item">
        <asp:Label ID="breadLabel" runat="server">Nuevo</asp:Label></li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="row" style="float: right; margin-right: 0.5%; margin-top: 0%">
        <label class="h6">Bodega:</label>
        <asp:Label runat="server" CssClass="h6" Text=" B01"></asp:Label>
    </div>
    <asp:Label runat="server" ID="labelAccion" CssClass="h5">Nuevo material</asp:Label>
    <br>
    <br>
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-5" style="margin-left: 0%;">
            <asp:HiddenField runat="server" ID="escondidillo" Value="" />
            <div class="row">
                <asp:TextBox type="text" ID="codigoMTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código">
                </asp:TextBox>
            </div>
            <br />
            <div class="row">
                <asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Nombre">
                </asp:TextBox>
            </div>
            <br>
            <div class="row">
                <asp:TextBox type="number" ID="precioKgTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Precio(Kg)">
                </asp:TextBox>
            </div>
            <br>
            <div class="row">
                <asp:DropDownList Width="100%" ID="unidadDD" runat="server" CssClass="btn btn-light"></asp:DropDownList>
            </div>
            <br />
            <div class="row justify-content-start" style="">
                <asp:Button runat="server" CssClass="btn btn-info" ID="btnGuardarActualizar" Text="Guardar" OnClick="btnGuardarActualizar_Click" />
            </div>
            <br>
            <a href="DetalleAjuste.aspx" class="btn btn-link" style="float: right">Registrar Stock</a>
        </div>

    </div>
</asp:Content>
