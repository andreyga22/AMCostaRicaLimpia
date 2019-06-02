<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaSocios.aspx.cs" Inherits="ProyectoAMCRL.BusquedaSocios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-3.4.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <link href="ownStyles.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $("#montosCb").click(function () {
                if (document.getElementById("montosCb").checked == true) {
                    $("#divMontos").show();
                } else {
                    $("#divMontos").hide();
                }
            });
        });

        $(document).ready(function () {
            $("#materialesCb").click(function () {
                if (document.getElementById("materialesCb").checked == true) {
                    $("#divMateriales").show();
                } else {
                    $("#divMateriales").hide();
                }
            });
        });

        $(document).ready(function () {
            $("#ubicacionCb").click(function () {
                if (document.getElementById("ubicacionCb").checked == true) {
                    $("#divUbicaciones").show();
                } else {
                    $("#divUbicaciones").hide();
                }
            });
        });

        $(document).ready(function () {
            $("#rolCb").click(function () {
                if (document.getElementById("rolCb").checked == true) {
                    $(".rolDiv").show();
                } else {
                    $(".rolDiv").hide();
                }
            });
        });

        $(document).keydown(function (keyPressed) {
            if (keyPressed.keyCode == 13) {
                alert("ENTER PRESIONADO");
            }
        });

        function ocultarFiltros() {
            $("#divMontos").hide();
            $("#divMateriales").hide();
            $("#divUbicaciones").hide();
            $(".rolDiv").hide();

        }

        window.onload = ocultarFiltros;

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Busqueda Socios</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="sideNavBody" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <h4 class="font-weight-bold">Búsqueda de socios</h4>
        </div>
        <br>
        <%-- SECCION 1 FILTROS--%>
        <div class="row">
            <h5>Seleccione uno o más filtros:</h5>
        </div>
        <div class="row" id="barraFiltros">
            <%--  <div class="col-lg-2" style="background-color:lightgrey">
                    <label for="palabraTb">Palabra Clave</label>
                    <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código o nombre">
                    </asp:TextBox>
                </div>--%>
            <%-- FILTRO MONTOS --%>
            <div class="filtroCell col-lg-3">
                <label>
                    <input class="form-check-input" type="checkbox" id="montosCb" value="">Monto en facturas</label>
                <div class="row" id="divMontos">
                    <asp:TextBox ID="montoMinimo" Height="30px" runat="server" type="number" CssClass="btn btn-light" Width="90%" placeholder="Máximo" />
                    <asp:TextBox ID="montoMax" Height="30px" runat="server" type="number" CssClass="btn btn-light" Width="90%" placeholder="Mínimo" />
                </div>
            </div>
            <%-- FILTRO MATERIALES --%>
            <div class="col-lg-3 filtroCell">
                <div class="form-group">
                    <label>
                        <input class="form-check-input" type="checkbox" id="materialesCb" value="">Material</label>
                    <div class="row" id="divMateriales">
                        <asp:DropDownList class="btn btn-light" Height="40px" ID="materialDd" runat="server" Width="90%" AutoPostBack="false">
                            <asp:ListItem>Aluminio</asp:ListItem>
                            <asp:ListItem>Cobre</asp:ListItem>
                            <asp:ListItem>Hierro</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <%-- FILTRO UBICACION --%>
            <div class="col-lg-3 filtroCell">
                <label>
                    <input class="form-check-input" type="checkbox" id="ubicacionCb" value="">Ubicacion</label>
                <div class="row" id="divUbicaciones">
                    <asp:TextBox class="form-control" ID="TextBox1" runat="server" Width="90%" Height="30px" type="text" CssClass="btn btn-light" placeholder="Provincia" />
                    <asp:TextBox class="form-control" ID="TextBox2" runat="server" Width="90%" type="text" Height="30px" CssClass="btn btn-light" placeholder="Cantón" />
                    <asp:TextBox class="form-control" ID="TextBox3" runat="server" Width="90%" type="text" Height="30px" CssClass="btn btn-light" placeholder="Distrito" />
                </div>
            </div>
            <%-- FILTRO ROL --%>
            <div class="col-lg-3 filtroCell">
                <label>
                    <input class="form-check-input" type="checkbox" id="rolCb" value="">Rol</label>
                <div style="width: 100%" class="rolDiv">
                    <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="PROVEEDOR" />
                </div>
                <div style="width: 100%" class="rolDiv">
                    <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="CLIENTE" />
                </div>
            </div>
        </div>
        <div class="row justify-content-center">
            <asp:Button ID="btnActualizar" type="submit" runat="server" Text="Actualizar búsqueda" class="btn btn-outline-secondary" />
        </div>
        <br />
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
