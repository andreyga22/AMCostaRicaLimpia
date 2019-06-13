<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroSociosUI.aspx.cs" Inherits="ProyectoAMCRL.RegistroSociosUI" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Registrar y Modificar Socio</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">

        <div class="row">
            <h4>Registro cliente/proveedor</h4>
        </div>
        <br />
        <asp:Button ID="btnFactur" type="submmit" runat="server" Text="Facturas" class="btn btn-outline-secondary" Width="15%" Visible="false" />
        <asp:Button ID="btnAsociar" type="submmit" runat="server" Text="Asociar" class="btn btn-outline-secondary" Width="15%" Visible="false" />
        <div class="row">
            <div class="form-group">
                <label for="rolRadios">Rol</label>
                <br />
                <asp:RadioButtonList ID="rolRadios" runat="server" RepeatDirection="Horizontal" CellPadding="5" CssClass="d-inline">
                    <asp:ListItem>Proveedor</asp:ListItem>
                    <asp:ListItem>Cliente</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <br />
        <br />

        <%-- SECCION 1 --%>

        <div class="row">
            <div class="col-lg-4">
                <div class="row">

                    <h4>Datos personales</h4>
                </div>
                <br />
                <br />


                <div class="row">
                    <div class="form-group">
                        <label for="idTB">Identificación</label>
                        <asp:TextBox type="text" ID="idTB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="nombreTB">Nombre</label>
                        <asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="ape1TB">Primer apellido</label>
                        <asp:TextBox type="text" ID="ape1TB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="ape2TB">Primer apellido</label>
                        <asp:TextBox type="text" ID="ape2TB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <label for="activaCb">Activado</label>
                    <asp:CheckBox ID="activaCb" type="checkbox" runat="server" />
                </div>
            </div>

            <%-- SECCION 2 --%>
            <div class="col-lg-4">
                <div class="row">
                    <h4>Ubicación</h4>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="provinciaTB">Provincia</label>
                        <asp:TextBox type="text" ID="provinciaTB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="cantonTB">Cantón</label>
                        <asp:TextBox type="text" ID="cantonTB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="distritoTB">Distrito</label>
                        <asp:TextBox type="text" ID="distritoTB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="sennas">Otras Señas</label>
                        <asp:TextBox type="text" ID="sennas" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>

            <%-- SECCION 3 (MAPA) --%>
            <div class="col-lg-4">
                <div class="row">
                    <h4>Contactos</h4>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="telTB">Teléfono Habitación</label>
                        <asp:TextBox type="text" ID="telTB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="tel2TB">Teléfono Personal</label>
                        <asp:TextBox type="text" ID="tel2TB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="correoTB">Correo electrónico</label>
                        <asp:TextBox type="email" ID="correoTB" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <%-- SUBMMIT BUTTON --%>
        <div class="row justify-content-center">
            <asp:Button ID="btnRegistrar" type="submit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" OnClick="btnRegistrar_Click" />
        </div>
        <asp:Label ID="info" runat="server"></asp:Label>
        <br />
    </div>
</asp:Content>
