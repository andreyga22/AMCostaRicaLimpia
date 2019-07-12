<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Bodega.aspx.cs" Inherits="ProyectoAMCRL.Bodega" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarBodegas.aspx" style="color:dodgerblue">Administrar bodegas</a></li>
    <li class="breadcrumb-item active">Bodega</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <div class="form-group">
                <h4>Bodega</h4>
            </div>
        </div>
        <br />
        <div class="justify-content-center">

            <div class="form-group">
                <label for="codigoTb">Código Bodega*</label><asp:RequiredFieldValidator ID="valCodigo" runat="server" ErrorMessage="Campo requerido" ControlToValidate="codigoTb" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:TextBox type="text" ID="codigoTb" class="form-control" placeholder="B01" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="nombreTb">Nombre Bodega*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="nombreTB" ErrorMessage="Campo Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
&nbsp;<asp:TextBox type="text" ID="nombreTB" class="form-control" placeholder="Naranjo-1" runat="server"></asp:TextBox>
            </div>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <label for="provinciaTb">Provincia*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="provinciaTb" ErrorMessage="Campo Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
&nbsp;<asp:TextBox type="text" ID="provinciaTb" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="cantonTb">Cantón*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cantonTb" ErrorMessage="Campo Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
&nbsp;<asp:TextBox type="text" ID="cantonTb" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="distritoTb">Distrito*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="distritoTb" ErrorMessage="Campo Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
&nbsp;<asp:TextBox type="text" ID="distritoTb" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="otrasTb">Otras Señas*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="otrasTb" ErrorMessage="Campo Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
&nbsp;<asp:TextBox type="text" ID="otrasTb" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group" id="estado" runat="server">
                <label for="estadoRb">Estado</label>
                <asp:RadioButtonList ID="estadoRb" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Activado</asp:ListItem>
                    <asp:ListItem>Desactivado</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <%--<div class="form-group">
                <label for="activaCb">Activado</label>
                <asp:CheckBox ID="activaCb" type="checkbox" runat="server" />
            </div>--%>
        </div>
        <%-- SUBMMIT BUTTON --%>
        <div class="row justify-content-center">
            <div class="form-group">
                <asp:Button ID="btnGuardar" type="submit" runat="server" Text="Guardar" class="btn btn-info" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>
</asp:Content>

