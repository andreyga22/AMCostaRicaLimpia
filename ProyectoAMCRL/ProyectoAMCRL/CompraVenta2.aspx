﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="CompraVenta2.aspx.cs" Inherits="ProyectoAMCRL.CompraVenta2" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li runat="server" id="bread" class="breadcrumb-item active">Compra
    </li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        <br />
        <div class="row">
            <div class="col-sm-2">
                <asp:Label ID="empresaLb" runat="server" Text="AM Costa Rica Verde"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="telEmpLb" runat="server" Text="83964649"></asp:Label>
            </div>
            <div class="col-sm-1">
                <asp:Label ID="Label1" runat="server" Text="Bodega: "></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:DropDownList runat="server" ID="bodegasDd" AutoPostBack="True" ViewStateMode="Enabled"></asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <asp:Label ID="Label2" runat="server" Text="Factura No. "></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="numFacturaLb" runat="server" Text="321"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="fechaLb" runat="server" Text="22/09/2019"></asp:Label>
            </div>
        </div>

        <br />
        <div class="row">
            <div class="col-sm-1">
                <asp:Label ID="Label6" runat="server" Text="Encargado:"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="cajeroLb" runat="server" Text="andrye"></asp:Label>
            </div>
        </div>

        <br />
        <div class="row">
            <div class="col-sm-2">
                <asp:LinkButton ID="agregarCP" runat="server">Agregar Cliente/Prov</asp:LinkButton>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="NombreCP" runat="server" Text="Cliente"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="cedulaCP" runat="server" Text="202220222"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="telCP" runat="server" Text="86749512"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="correoCP" runat="server" Text="correo@ejemplo.com"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="direccionCP" runat="server" Text="dirección"></asp:Label>
            </div>
        </div>

        <br />
        <br />
        <div class="row">
            <div class="col-sm-1">
                <asp:DropDownList runat="server" ID="materialesDd" AutoPostBack="True" ViewStateMode="Enabled"></asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <asp:TextBox ID="cantDisponibleTb" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:TextBox ID="cantidadVC" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:TextBox ID="unidadTb" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:TextBox ID="precioCV" runat="server" style="text-align: right"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:TextBox ID="impuestoTb" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:TextBox ID="descuentoTb" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:TextBox ID="precioTotal" runat="server" Enabled="false" style="text-align: right"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:Button ID="agregarBtn" runat="server" Text="Agregar" />
            </div>
        </div>

        <br />
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateDeleteButton="True" OnRowDeleted="GridView1_RowDeleted" OnRowDeleting="GridView1_RowDeleting"></asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <br />
        <div class="row offset-sm-7">
            <div class="col-sm-2">
                <asp:Label ID="Label3" runat="server" Text="Subtotal"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="subtotalTb" runat="server" Text="1000" Enabled="false" style="text-align: right"></asp:TextBox>
            </div>
        </div>

        <div class="row offset-sm-7">
            <div class="col-sm-2">
                <asp:Label ID="Label4" runat="server" Text="Impuesto"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="impuestoTot" runat="server" Text="125" Enabled="false" style="text-align: right"></asp:TextBox>
            </div>
        </div>

        <div class="row offset-sm-7">
            <div class="col-sm-2">
                <asp:Label ID="Label5" runat="server" Text="Descuento"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="descuentoTot" runat="server" Text="0" Enabled="false" style="text-align: right"></asp:TextBox>
            </div>
        </div>

        <div class="row offset-sm-7">
            <div class="col-sm-2">
                <asp:Label ID="Label7" runat="server" Text="Total"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="totalTb" runat="server" Text="1125" Enabled="false" style="text-align: right"></asp:TextBox>
            </div>
        </div>



    </div>
</asp:Content>
