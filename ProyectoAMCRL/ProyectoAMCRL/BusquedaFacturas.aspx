<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaFacturas.aspx.cs" Inherits="ProyectoAMCRL.BusquedaFacturas" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-3.4.0.min.js" type='text/javascript'></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <link href="ownStyles.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <!-- Script -->
    <script src='jquery-3.2.1.min.js' type='text/javascript'></script>

    <!-- jQuery UI -->
    <link href='jquery-ui.min.css' rel='stylesheet' type='text/css' />
    <script src='jquery-ui.min.js' type='text/javascript'></script>

    <!-- Language script -->
    <script src='datepicker-es.js' type='text/javascript'></script>
    <script>
        $(function () {
            $("#datepicker").datepicker($.datepicker.regional["es"]);
        });
    </script>
    <script>
        $(function () {
            $("#datepicker2").datepicker($.datepicker.regional["es"]);
        });
    </script>

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
            $("#divUbicaciones").hide();
            $(".rolDiv").hide();
            $("#divFecha").hide();
        }

        window.onload = ocultarFiltros;

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Búsqueda Facturas</li>
</asp:Content>

<%-- SE AGREGAN MAS LINEAS PARA QUE CALCE EL TAMAÑO --%>
<asp:Content ID="Content4" ContentPlaceHolderID="sideNavBody" runat="server">
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <h4>Búsqueda de facturas</h4>
        </div>
        <br />
        <h5>Filtros</h5>

        <div class="row" style="margin-left: 10px">
            <div class="form-group">

                <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código o socio"></asp:TextBox>
            </div>
            <%--      <div class="form-group">
            <label for="fechasCb">Fecha</label>
            <asp:CheckBox ID="fechasCb" type="checkbox" runat="server" />
        </div>--%>
        </div>

        <div class="row" id="barraFiltros">
            <%--   <div class="row" style="margin-left: 10px">--%>
            <div class="col-lg-3 filtroCell">

                <label>
                    <input class="form-check-input" type="checkbox" id="fechasCb" value="">Fecha</label>
                <%--</div>--%>
                <%--</div>--%>
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <%-- Height="190px" Width="350px" Font-Size="9pt"--%>
                <div class="row" id="divFecha">
                    <p>
                        Fecha Inicio:
        <input type="text" id="datepicker" runat="server" clientidmode="Static" />
                    </p>
                    <%-- <div class="col-5">
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
                <br />
                <br />
            </div>
        </div>--%>
                    <%--        <div class=" col-5">--%>

                    <p>
                        Fecha Fin:
        <input type="text" id="datepicker2" runat="server" clientidmode="Static"/>
                    </p>

                    <%--     Fecha Fin:<br />
            <div style="text-align: center">
                <div style="text-align: center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Calendar ID="calendFechaFin" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana"  ForeColor="Black"  NextPrevFormat="FullMonth" Font-Size="XX-Small"  SelectedDate="11/25/2018 12:21:35">
                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#16ACB8" ForeColor="White" />
                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#8DAA9D" />
                                <TodayDayStyle BackColor="#8DAA9D" />
                            </asp:Calendar>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </div>
                <br />
            </div>--%>
                    <%--</div>--%>
                </div>
            </div>
            <%--</div>--%>
            <%-- SECCION 1 FILTROS--%>
            <%--<h5>Seleccione uno o más filtros:</h5>--%>

            <%--  <div class="col-lg-2" style="background-color:lightgrey">
                    <label for="palabraTb">Palabra Clave</label>
                    <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código o nombre">
                    </asp:TextBox>
                </div>--%>
            <%-- FILTRO MONTOS --%>
            <div class="filtroCell col-lg-3">
                <label>
                    <input class="form-check-input" type="checkbox" id="montosCb" value="">Monto en facturas</label>
                <%-- <label for="montosCb">Rol</label>
             <asp:CheckBox ID="montosCb" type="checkbox" runat="server" />--%>
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
            <%--        <div class="col-lg-3 filtroCell">
            <label>
                <input class="form-check-input" type="checkbox" id="ubicacionCb" value="">Ubicación</label>
            <div class="row" id="divUbicaciones">
                <asp:TextBox class="form-control" ID="TextBox1" runat="server" Width="90%" Height="30px" type="text" CssClass="btn btn-light" placeholder="Provincia" />
                <asp:TextBox class="form-control" ID="TextBox2" runat="server" Width="90%" type="text" Height="30px" CssClass="btn btn-light" placeholder="Cantón" />
                <asp:TextBox class="form-control" ID="TextBox3" runat="server" Width="90%" type="text" Height="30px" CssClass="btn btn-light" placeholder="Distrito" />
            </div>
        </div>--%>
            <%-- FILTRO ROL --%>
            <div class="col-lg-3 filtroCell">
                <label>
                    <input class="form-check-input" type="checkbox" id="rolCb" value="">Tipo</label>
                <%-- <label for="rolCb">Rol</label>
             <asp:CheckBox ID="rolCb" type="checkbox" runat="server" />--%>
                <div style="width: 100%" class="rolDiv">
                    <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="Venta" />
                </div>
                <div style="width: 100%" class="rolDiv">
                    <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="Compra" />
                </div>
            </div>
        </div>
        <br />
        <div class="row justify-content-center">
            <asp:Button ID="btnActualizar" type="submit" runat="server" Text="Actualizar búsqueda" class="btn btn-outline-secondary" OnClick="btnActualizar_Click" />
        </div>

        <br />
        <br />
        <%--  <table class="table table-bordered">
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
    </table>--%>
        <asp:GridView ID="gridFacturas" class="table table-hover table-bordered table-striped" AutoGenerateSelectButton="True" runat="server" OnSelectedIndexChanged="gridFact_SelectedIndexChanged" AllowPaging="True" AllowSorting="True" PageSize="5">
            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NumericFirstLast" PageButtonCount="2" />
         <HeaderStyle BackColor="#94BD8B" />
            </asp:GridView>
        <br />
        <%-- <div class="row justify-content-center">
        <asp:Button ID="btnGuardar" type="submmit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" />
    </div>--%>
    </div>
</asp:Content>















