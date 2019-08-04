<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroMateriales.aspx.cs" Inherits="ProyectoAMCRL.RegistroMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarMateriales.aspx" style="color: dodgerblue">Administrar Materiales</a></li>
    <li class="breadcrumb-item active">Material</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <asp:Label runat="server" ID="labelAccion" CssClass="h5">Registro de material</asp:Label>
        <br>
        <br>
        <div class="row">
            <div class="col-lg-4"></div>
            <div class="col-lg-5" style="margin-left: 0%;">
                <asp:HiddenField runat="server" ID="escondidillo" Value="" />
                <div class="row">
                     <label for="nombreTB">Código*</label>
                    <asp:TextBox type="text" ID="codigoMTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código">
                    </asp:TextBox>

                </div>
                <br />
                <div class="row">
                    <label for="nombreTB">Nombre*</label>
                    <asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Nombre">
                    </asp:TextBox>
                </div>
                <br>
                <div class="row">
                    <label for="lblPV">Precio Venta*</label>
                    <asp:TextBox type="text" ID="precioKgV" class="form-control" runat="server" TextMode="SingleLine" placeholder="Precio Venta(Kg)">
                    </asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" display="Dynamic" runat="server" ErrorMessage="Monto inválido." ControlToValidate="precioKgV" ForeColor="Red" ValidationExpression="^(?=[0-9.]{1,8}$)[0-9]+(.[0-9]+)*$" ValidationGroup="registroMG"></asp:RegularExpressionValidator>
                </div>
                <br />
                <div class="row">
                    <label for="lblPC">Precio Compra*</label>
                    <asp:TextBox type="text" ID="precioKgC" class="form-control" runat="server" TextMode="SingleLine" placeholder="Precio Compra(Kg)">
                    </asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" display="Dynamic" runat="server" ErrorMessage="Monto inválido." ControlToValidate="precioKgC" ForeColor="Red" ValidationExpression="^(?=[0-9.]{1,8}$)[0-9]+(.[0-9]+)*$" ValidationGroup="registroMG"></asp:RegularExpressionValidator>
                </div>
                <br>
                <div class="row">
                    <asp:DropDownList Width="100%" ID="unidadDD" runat="server" CssClass="btn btn-light"></asp:DropDownList>
                </div>
                <br />
                <div class="form-group" id="estado" runat="server">
                    <label runat="server" id="estadoLb" for="estadoRb">Estado</label>
                    <asp:RadioButtonList ID="estadoRb" runat="server" class="form-control" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Activado</asp:ListItem>
                        <asp:ListItem>Desactivado</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="row justify-content-start" style="">
                    <asp:Button runat="server" CssClass="btn btn-info" ID="btnGuardarActualizar" Text="Guardar" OnClick="btnGuardarActualizar_Click" ValidationGroup="registroMG"/>
                </div>
                <br>
               
            </div>

        </div>
    </div>
</asp:Content>
