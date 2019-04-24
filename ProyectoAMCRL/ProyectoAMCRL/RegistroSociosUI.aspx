<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroSociosUI.aspx.cs" Inherits="ProyectoAMCRL.RegistroSociosUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">



        <form id="form1" runat="server">
            <div class="info_div" runat="server">
                <asp:Label runat="server" ID="alertar"></asp:Label>
                <asp:Button runat="server" Text="ok" ID="ok_btn" />
            </div>
            <br />
            <br />
            <div class="row">
                <h4>Registro cliente/proveedor</h4>
            </div>
            <br />
            <br />
            <br />


            <%-- SECCION 1 --%>
            <div class="row">
                <div class="col-sm-4">
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
                </div>

                <%-- SECCION 2 --%>
                <div class="col-sm-4">
                    <div class="row">
                        <h4>Ubicacion</h4>
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
                <div class="col-sm-4">
                    <div class="row">
                        <h4>Contactos</h4>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <label for="telTB">Teléfono</label>
                            <asp:TextBox type="text" ID="telTB" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label for="tel2TB">Teléfono 2</label>
                            <asp:TextBox type="text" ID="tel2TB" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label for="correoTB">Correo electrónico</label>
                            <asp:TextBox type="email" ID="correoTB" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <%-- ROL --%>
                    <div class="row">
                        <div class="form-group">
                            <asp:RadioButtonList ID="rolRadios" runat="server" RepeatDirection="Horizontal" CellPadding="5" CssClass="d-inline">
                                <asp:ListItem>Cliente</asp:ListItem>
                                <asp:ListItem>Proveedor</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>

            </div>
            <br />
                <br />
            <br />
            <%-- SUBMMIT BUTTON --%>
            <div class="row justify-content-center">
                <div class="form-group">
                    <asp:Button ID="btnRegistrar" type="submmit" runat="server" Text="Registrar" class="btn btn-outline-primary btn-lg " OnClick="btnRegistrar_Click" />
                </div>
            </div>
            <asp:Label ID="info" runat="server"></asp:Label>
            <br />
            <br />
            <br />













        </form>

    </div>
</asp:Content>




















<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
