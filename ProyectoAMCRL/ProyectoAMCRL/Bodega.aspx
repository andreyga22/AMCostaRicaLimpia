<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Bodega.aspx.cs" Inherits="ProyectoAMCRL.Bodega" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Registro Bodega</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <h4>Registro de bodega</h4>
        </div>
        <br />
        <div class="col-10 offset-1 justify-content-center">

            <div class="form-group">
                <label for="codigoTb">Código Bodega</label>
                <asp:TextBox type="text" ID="codigoTb" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="nombreTb">Nombre Bodega</label>
                <asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="provinciaTb">Provincia</label>
                <asp:TextBox type="text" ID="provinciaTb" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="cantonTb">Cantón</label>
                <asp:TextBox type="text" ID="cantonTb" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="distritoTb">Distrito</label>
                <asp:TextBox type="text" ID="distritoTb" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="otrasTb">Otras Señas</label>
                <asp:TextBox type="text" ID="otrasTb" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="activaCb">Activado</label>
                <asp:CheckBox ID="activaCb" type="checkbox" runat="server" />
            </div>
        </div>
        <%-- SUBMMIT BUTTON --%>
        <div class="row justify-content-center">
            <div class="form-group">
                <asp:Button ID="btnGuardar" type="submit" runat="server" Text="Guardar" class="btn btn-info" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>
</asp:Content>

