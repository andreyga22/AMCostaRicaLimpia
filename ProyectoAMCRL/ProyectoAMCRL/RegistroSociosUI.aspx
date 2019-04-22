<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroSociosUI.aspx.cs" Inherits="ProyectoAMCRL.RegistroSociosUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">


        <div class="container row_registroForm">
            <h4>Registro cliente/proveedor</h4>
            <br>

            <div class="row">
                <%-- SECCION 1 --%>
                <div class="col-sm-4">
                    <%-- ROL --%>
                    <asp:RadioButtonList id="rolRadios" runat="server" RepeatDirection="Horizontal" CellPadding="5" CssClass="d-inline">
                        <asp:ListItem>Cliente</asp:ListItem>
                        <asp:ListItem>Proveedor</asp:ListItem>
                    </asp:RadioButtonList>

                    <div class="form-group">
                         Identificacion<br>
                        <asp:TextBox ID="idTB" runat="server" CssClass="cajaTexto form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        Nombre<br>
                        <asp:TextBox ID="nombreTB" runat="server" CssClass="cajaTexto  form-control"></asp:TextBox>
                      
                    </div>
                    <div class="form-group">
                        Primer apellido <br>
                        <asp:TextBox ID="ape1TB" runat="server" CssClass="cajaTexto form-control"></asp:TextBox>
                       
                    </div>
                    <div class="form-group">
                         Segundo apellido<br>
                        <asp:TextBox ID="ape2TB" runat="server" CssClass="cajaTexto form-control"></asp:TextBox>                       
                    </div>   

                </div>

                <%-- SECCION 2 --%>
                <div class="col-sm-4" id="ubicationForm" ">

                    <h4>Ubicacion</h4>
                     Provincia<br>
                     <asp:TextBox ID="provinciaTB" runat="server" CssClass="cajaTexto form-control"></asp:TextBox>

                     Canton<br>
                     <asp:TextBox ID="cantonTB" runat="server" CssClass="cajaTexto form-control"></asp:TextBox>

                     Distrito<br>
                     <asp:TextBox ID="distritoTB" runat="server" CssClass="cajaTexto form-control"></asp:TextBox>
                     Otras señas<br>
                     <asp:TextBox ID="sennas" runat="server" CssClass="cajaTexto form-control"></asp:TextBox>
                   
                    <%-- SUBMMIT BUTTON --%>
                   
                </div>

                <%-- SECCION 3 (MAPA) --%>
                <div class="col-sm-4">
                     <h4>Contactos</h4>
                    <div class="form-group">
                        Teléfono<br>
                        <asp:TextBox ID="telTB" runat="server" MaxLength="8" ViewStateMode="Enabled" CssClass="cajaTexto form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        Teléfono2<br>
                        <asp:TextBox ID="tel2TB" runat="server" MaxLength="8" ViewStateMode="Enabled" CssClass="cajaTexto form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        Correo<br>
                        <asp:TextBox ID="correoTB" runat="server" MaxLength="8" ViewStateMode="Enabled" CssClass="cajaTexto form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
             <asp:Button ID="btnRegistrar" type="submmit" runat="server" Text="Registrar" class="btn btn-outline-primary btn-lg " OnClick="btnRegistrar_Click" />
                    <asp:Label ID="info" runat="server"></asp:Label>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
