<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Bodega.aspx.cs" Inherits="ProyectoAMCRL.Bodega" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Principal.aspx">Principal</a></li>
                <li class="breadcrumb-item active" aria-current="page">Registro Bodega</li>
            </ol>
        </nav>
    <div class="container">

        <form id="form1" runat="server">
            <br />
            <div class="row justify-content-center">
                <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
            </div>
            <br />
            <br />
            <div class="row">
                <h4>Registro de bodega</h4>
            </div>
            <br />
            <br />
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
            <br />
            <br />
            <br />
            <%-- SUBMMIT BUTTON --%>
            <div class="row justify-content-center">
                <div class="form-group">
                    <asp:Button ID="btnGuardar" type="submit" runat="server" Text="Guardar" class="btn btn-outline-info" Width="15%"/>
                </div>
            </div>
            <br />
            <br />
            <br />













        </form>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
