<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaSocios.aspx.cs" Inherits="ProyectoAMCRL.BusquedaSocios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-3.4.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#fechasCb").click(function () {
                $("#divMontos").hide();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="Principal.aspx">Principal</a></li>
            <li class="breadcrumb-item active" aria-current="page">Busqueda Socios</li>
        </ol>
    </nav>

    <div class="container">

        <form id="form1" runat="server">
            <%-- ERROR LITERAL --%>
            <div class="row justify-content-center">
                <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
            </div>
            <br />
            <div class="row">
                <h4 class="font-weight-bold">Búsqueda de socios</h4>
            </div>
            <br>
             <%-- SECCION 1 --%>
            <div class="row">
                <h5>Seleccione uno o más filtros:</h5>
            </div>
            <br>
            <div class="row">
                <div class="col-lg-2">
                    <label for="palabraTb">Palabra Clave</label>
                    <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código o nombre">
                    </asp:TextBox>
                </div>
                <div class="col-lg-2">
                    <label for="fechasCb">Monto en facturas</label>
                    <asp:CheckBox ID="fechasCb" type="checkbox" runat="server" />
                    <br>
                    <div class="row" id="divMontos">
                        <asp:TextBox class="form-control" ID="montoMinimo" runat="server" type="number" CssClass="btn btn-light" placeholder="Monto mínimo" />
                        <asp:TextBox class="form-control" ID="montoMax" runat="server" type="number" CssClass="btn btn-light" placeholder="Monto máximo" />
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="form-group">
                        <label for="materialCb">Material</label>
                        <asp:CheckBox ID="materialCb" type="checkbox" runat="server" />
                        <br>
                        <div class="row">
                            <asp:DropDownList class="btn btn-light dropup" ID="materialDd" runat="server" AutoPostBack="True">
                                <asp:ListItem>Aluminio</asp:ListItem>
                                <asp:ListItem>Cobre</asp:ListItem>
                                <asp:ListItem>Hierro</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <label for="ubicacionCb">Ubicación</label>
                    <asp:CheckBox ID="ubicacionCb" type="checkbox" runat="server" />
                    <br>
                    <div class="row">
                        <asp:TextBox class="form-control" ID="TextBox1" runat="server" type="text" CssClass="btn btn-light" placeholder="Provincia" />
                        <asp:TextBox class="form-control" ID="TextBox2" runat="server" type="text" CssClass="btn btn-light" placeholder="Cantón" />
                        <asp:TextBox class="form-control" ID="TextBox3" runat="server" type="text" CssClass="btn btn-light" placeholder="Distrito" />
                    </div>
                </div>
                <div class="col-lg-2">
                    <label for="rolCb">Rol</label>
                    <asp:CheckBox ID="rolCb" type="checkbox" runat="server" />
                    <div style="width: 100%">
                        <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="PROVEEDOR" />
                    </div>
                    <div style="width: 100%">
                        <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="CLIENTE" />
                    </div>
                </div>
            </div>
             <br />
            <div class="row justify-content-center">
                <div class="form-group">
                    <asp:Button ID="btnGuardar" type="submmit" runat="server" Text="Actualizar búsqueda" class="btn btn-primary" />
                </div>
            </div>
             <%-- SECCION 2 --%>
            <div class="overflow-auto" style="height: 180px; width: 100%;">
                <table class="table-sm table-bordered table-hover" style="width: 100%">
                    <thead>
                        <tr class="tabla_encabezado">
                            <th scope="col">#</th>
                            <th scope="col">Identificacion</th>
                            <th scope="col">Nombre</th>
                            <th scope="col">Telefono</th>
                            <th scope="col">Ver</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row">1</th>
                            <td>54687</td>
                            <td>Jorge González</td>
                            <td>88775566</td>
                            <td>
                                <asp:Button Height="100%" CssClass="btn btn-link" runat="server" Text="Abrir detalle" />
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">2</th>
                            <td>54688</td>
                            <td>Julio Jaramillo</td>
                            <td>88775566</td>
                            <td>
                                <asp:Button Height="100%" CssClass="btn btn-link" runat="server" Text="Abrir detalle" />
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">3</th>
                            <td>45688</td>
                            <td>Selena Gomez</td>
                            <td>88775566</td>
                            <td>
                                <asp:Button Height="100%" CssClass="btn btn-link" runat="server" Text="Abrir detalle" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <br />
            <br />
        </form>

    </div>
</asp:Content>
