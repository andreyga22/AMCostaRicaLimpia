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

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
            <asp:Button ID="btnActualizar" type="submit" runat="server" Text="Actualizar búsqueda" class="btn btn-outline-secondary" OnClick="btnActualizar_Click" />
        </div>
        <br />
        <%-- SECCION 2 --%>
        <div class="row justify-content-center">
            <div class="col-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridSocios" runat="server" AllowSorting="True" AllowPaging="True"  PageSize="5" OnPageIndexChanging="gridSocios_PageIndexChanging" OnSorting="gridSocios_Sorting" OnSelectedIndexChanged="gridSocios_SelectedIndexChanged" OnRowDataBound="gridSocios_RowDataBound">
                            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NumericFirstLast" PageButtonCount="2" />
                            <PagerStyle HorizontalAlign="Right" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
