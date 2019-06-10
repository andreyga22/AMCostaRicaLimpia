<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaFacturas.aspx.cs" Inherits="ProyectoAMCRL.BusquedaFacturas" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

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
            //<a href="BusquedaFacturas.aspx">BusquedaFacturas.aspx</a>
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

        $(document).ready(function () {
            $("#fechasCb").click(function () {
                if (document.getElementById("fechasCb").checked == true) {
                    $("#divFecha").show();
                } else {
                    $("#divFecha").hide();
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
            $("#divFecha").hide();

        }

        window.onload = ocultarFiltros;

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Busqueda Facturas</li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <%-- SECCION 1 FILTROS--%>
        <h5>Seleccione uno o más filtros:</h5>
        <div class="row" id="barraFiltros">

            <%-- FILTRO MONTOS --%>
            <div class="col-lg-2" style="padding-right:2%; margin-left:1%">
                <label>
                    <input class="form-check-input" type="checkbox" id="montosCb" value="">Monto en facturas</label>
                <div class="row" id="divMontos">
                    <asp:TextBox ID="montoMinimo" Height="30px" runat="server" type="number" CssClass="btn btn-light" Width="100%" placeholder="Máximo" />
                    <asp:TextBox ID="montoMax" Height="30px" runat="server" type="number" CssClass="btn btn-light" Width="100%" placeholder="Mínimo" />
                </div>
            </div>
            <%-- FILTRO MATERIALES --%>
            <div class="col-lg-2" style="padding-left:2%; margin-left:3%">
                <div class="form-group">
                    <label>
                        <input class="form-check-input" type="checkbox" id="materialesCb" value="">Material</label>
                    <div class="row" id="divMateriales">
                        <asp:DropDownList class="btn btn-light" Height="40px" ID="materialDd" runat="server" Width="100%" AutoPostBack="false">
                            <asp:ListItem>Aluminio</asp:ListItem>
                            <asp:ListItem>Cobre</asp:ListItem>
                            <asp:ListItem>Hierro</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <%-- FILTRO FECHA --%>
            <div class="col-lg-7 row" style="margin-left: 3%";>
                <div class=" row" style="width:100%" >
                    <label><input class="form-check-input" type="checkbox" id="fechasCb" value="">Rango de fechas</label>
                </div>
                <br>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <div class="row" id="divFecha">

                    <%-- FECHA INICIO --%>
                    <div class="">
                        Fecha inicio:<br />
                        <div style="text-align: center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="CalendFechaIni" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" ForeColor="Black" NextPrevFormat="FullMonth" Font-Size="XX-Small">
                                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="#8DAA9D" ForeColor="White" />
                                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#8DAA9D" />
                                        <TodayDayStyle BackColor="#CCCCCC" />
                                    </asp:Calendar>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                      <%-- FECHA FIN --%>
                    <div class="" style="margin-left: 50px">
                        Fecha Fin:<br />
                        <div style="text-align: center">
                            <div style="text-align: center">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Calendar ID="calendFechaFin" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" ForeColor="Black" NextPrevFormat="FullMonth" Font-Size="XX-Small" SelectedDate="11/25/2018 12:21:35">
                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <SelectedDayStyle BackColor="#16ACB8" ForeColor="White" />
                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#8DAA9D" />
                                            <TodayDayStyle BackColor="#8DAA9D" />
                                        </asp:Calendar>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>     
            <%-- fin filtro fecha --%>

        </div>
        <div class="row justify-content-start">
                 <asp:Button ID="btnActualizar" type="submit" runat="server" Text="Buscar" class="btn btn-primary" OnClick="btnActualizar_Click" />
        </div>
        <br />
        <table class="table table-bordered">
            <thead>
                <tr class="tabla_encabezado" style="background-color: #94BD8B">
                    <th scope="col">#</th>
                    <th scope="col">Código Factura</th>
                    <th scope="col">Fecha</th>
                    <th scope="col">Monto</th>
                    <th scope="col">Socio</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">1</th>
                    <td>54687</td>
                    <td>22/09/2015</td>
                    <td>25000</td>
                    <td>Jorge González</td>
                </tr>
                <tr>
                    <th scope="row">2</th>
                    <td>54688</td>
                    <td>23/09/2015</td>
                    <td>30000</td>
                    <td>María Gómez</td>
                </tr>
                <tr>
                    <th scope="row">3</th>
                    <td>54689</td>
                    <td>23/09/2015</td>
                    <td>45000</td>
                    <td>Selena Gómez</td>
                </tr>
            </tbody>
        </table>
        <asp:Button ID="btnGuardar" type="submmit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" />
    </div>

</asp:Content>















