<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="ProyectoAMCRL.Cuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
    <script src="jquery-3.4.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarCuentas.aspx" style="color: dodgerblue">Administrar cuentas</a></li>
    <li class="breadcrumb-item active" id="breadObj" runat="server">Cuenta</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <h4 id="tituloCuenta" runat="server">Creación de cuenta</h4>
        </div>
        <br />
        <div class="justify-content-center offset-1 col-10">
            <div class="form-row">
                <div class="form-group col-md-6" id="identi" runat="server" visible="false">
                    <label for="idTb">Identificador*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="idTB" ErrorMessage="Campo requerido" ForeColor="Red" ValidationGroup="cuentaG" Display="Dynamic"></asp:RequiredFieldValidator>
                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="idTB" Display="Dynamic" ErrorMessage="Máxima cantidad de letras permitida es: 75" ForeColor="Red" ValidationExpression="^.{1,75}$" ValidationGroup="bodegaG"></asp:RegularExpressionValidator>--%>
                    <asp:TextBox type="email" ID="idTB" class="form-control" placeholder="jessica28@ejemplo.com" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-6" id="contra" runat="server" visible="false">
                    <label for="contraTb" style="text-decoration: underline" id="contraLb" runat="server">Contraseña Temporal*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="contraTb" ErrorMessage="Campo requerido" ForeColor="Red" ValidationGroup="cuentaG" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="contraTb" Display="Dynamic" ErrorMessage="Máxima cantidad de letras permitida es: 200" ForeColor="Red" ValidationExpression="^.{1,200}$" ValidationGroup="bodegaG"></asp:RegularExpressionValidator>
                    <asp:TextBox type="password" ID="contraTb" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6" id="nombre" runat="server" visible="false">
                    <label for="nombreTb">Nombre completo*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="nombreTB" ErrorMessage="Campo requerido" ForeColor="Red" ValidationGroup="cuentaG" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="nombreTB" Display="Dynamic" ErrorMessage="Máxima cantidad de letras permitida es: 100" ForeColor="Red" ValidationExpression="^[a-zA-Z\s]{1,100}$" ValidationGroup="bodegaG"></asp:RegularExpressionValidator>
                    <asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-6" id="estado" runat="server" visible="false">
                    <label for="estadoRb">Estado*</label>
                    <asp:RadioButtonList ID="estadoRb" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Activado</asp:ListItem>
                        <asp:ListItem>Desactivado</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="form-group col-md-12" id="rol" runat="server" visible="false">
                    <label for="rolDd">Rol de cuenta</label>
                    <asp:DropDownList ID="rolDd" runat="server" class="form-control">
                        <asp:ListItem Selected="True">Regular</asp:ListItem>
                        <asp:ListItem>Administrador</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
            <div class="form-row">
                <div class="form-group offset-1 col-md-10" id="nueva" runat="server" visible="false">
                    <label for="nuevaTb">Nueva contraseña*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="nuevaTb" ErrorMessage="Campo requerido" ForeColor="Red" ValidationGroup="cuentaG" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="nuevaTb" Display="Dynamic" ErrorMessage="Máxima cantidad de letras permitida es: 200" ForeColor="Red" ValidationExpression="^.{1,200}$" ValidationGroup="bodegaG"></asp:RegularExpressionValidator>
                    <asp:TextBox type="password" ID="nuevaTb" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group offset-1 col-md-10" id="repetir" runat="server" visible="false">
                    <label for="repetirTb">Repetir contraseña*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="repetirTb" ErrorMessage="Campo requerido" ForeColor="Red" ValidationGroup="cuentaG" Display="Dynamic"></asp:RequiredFieldValidator>
                    &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="nuevaTb" ControlToValidate="repetirTb" ErrorMessage="Las contraseñas no coinciden" ForeColor="Red" ValidationGroup="cuentaG" Display="Dynamic"></asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="repetirTb" Display="Dynamic" ErrorMessage="Máxima cantidad de letras permitida es: 200" ForeColor="Red" ValidationExpression="^.{1,200}$" ValidationGroup="bodegaG"></asp:RegularExpressionValidator>
                    <asp:TextBox type="password" ID="repetirTb" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <%-- SUBMMIT BUTTON --%>
        <div class="row justify-content-center">
            <div class="form-group">
                <asp:Button ID="btnGuardar" type="submit" runat="server" Text="Guardar" class="btn btn-info" OnClick="btnGuardar_Click" ValidationGroup="cuentaG" />
            </div>
        </div>
    </div>
</asp:Content>
