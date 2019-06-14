<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="ProyectoAMCRL.Cuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
    <script src="jquery-3.4.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarCuentas.aspx">Administrar cuentas</a></li>
    <li class="breadcrumb-item active">Cuenta</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <h4>Creación de cuenta</h4>
        </div>
        <br />
        <div class="justify-content-center">
            <div class="form-row">
                <div class="form-group col-md-6" id="identi" runat="server" visible="false">
                    <label for="idTb">Identificador</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="idTB" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;<asp:TextBox type="text" ID="idTB" class="form-control" placeholder="jessica28" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-6" id="contra" runat="server" visible="false">
                    <label for="contraTb" style="text-decoration: underline" runat="server">Contraseña Temporal</label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="contraTb" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;<asp:TextBox type="password" ID="contraTb" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" id="nombre" runat="server" visible="false">
                <label for="nombreTb">Nombre completo</label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="nombreTB" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                &nbsp;<asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6" id="rol" runat="server" visible="false">
                    <label for="rolDd">Rol de cuenta</label>
                    <asp:DropDownList ID="rolDd" runat="server" class="form-control">
                        <asp:ListItem Selected="True">Regular</asp:ListItem>
                        <asp:ListItem>Administrador</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-6" id="estado" runat="server" visible="false">
                    <label for="estadoRb">Estado</label>
                    <asp:RadioButtonList ID="estadoRb" runat="server" class="form-control" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Activado</asp:ListItem>
                        <asp:ListItem>Desactivado</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group" id="nueva" runat="server" visible="false">
                <label for="nuevaTb" style="text-decoration: underline">Nueva contraseña</label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="nuevaTb" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                &nbsp;<asp:TextBox type="password" ID="nuevaTb" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group" id="repetir" runat="server" visible="false">
                <label for="repetirTb" style="text-decoration: underline">Repetir contraseña</label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="repetirTb" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="nuevaTb" ControlToValidate="repetirTb" ErrorMessage="Las contraseñas no coinciden" ForeColor="Red"></asp:CompareValidator>
                <asp:TextBox type="password" ID="repetirTb" class="form-control" runat="server"></asp:TextBox>
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
