<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Monedas.aspx.cs" Inherits="ProyectoAMCRL.Monedas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarMonedas.aspx" style="color: dodgerblue">Administrar Monedas</a></li>
    <li class="breadcrumb-item active" id="breadObj" runat="server">Moneda</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">


    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <div class="form-group">
                <h4>Moneda</h4>
            </div>
        </div>
        <br />
        <div class="form-row justify-content-center">
            <div class="form-group col-sm-6">
                <label for="codigoTb">Código Moneda*</label><asp:RequiredFieldValidator ID="valCodigo" runat="server" ErrorMessage="Campo requerido" ControlToValidate="codigoTb" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:TextBox type="text" ID="codigoTb" class="form-control" placeholder="USD" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-row justify-content-center">
            <div class="form-group col-sm-6">
                <label for="detalleTb">Detalle Moneda*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="detalleTb" ErrorMessage="Campo Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                &nbsp;<asp:TextBox type="text" ID="detalleTb" class="form-control" placeholder="Dólares" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-row justify-content-center">
            <div class="form-group col-md-6">
                <label for="equivalenciaTb">Equivalencia en colones*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="equivalenciaTb" ErrorMessage="Campo Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                &nbsp;<asp:TextBox type="text" ID="equivalenciaTb" class="form-control" runat="server" placeholder="595"></asp:TextBox>
            </div>
        </div>
        <div class="form-row justify-content-center">
            <div class="form-group col-sm-6">
                <asp:RadioButtonList ID="estadoRb" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Activado</asp:ListItem>
                    <asp:ListItem>Desactivado</asp:ListItem>
                </asp:RadioButtonList>
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
