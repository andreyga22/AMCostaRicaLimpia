<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaFacturas.aspx.cs" Inherits="ProyectoAMCRL.BusquedaFacturas" %>

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
            $("#fechaInicio").datepicker($.datepicker.regional["es"]);
        });
    </script>
    <script>
        $(function () {
            $("#fechaFin").datepicker($.datepicker.regional["es"]);
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

        //$(document).keydown(function (keyPressed) {
        //    if (keyPressed.keyCode == 13) {
        //        alert("ENTER PRESIONADO");
        //    }
        //});

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

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <h4>Búsqueda de facturas</h4>
        </div>
        <br />

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>




        <!-- Modal -->
        <%--      <div class="modal fade" id="filtros" role="dialog">
            <div class="modal-dialog modal-lg">--%>


        <!-- Modal content-->
        <%--       <div class="modal-content " style="height: 500px;">

                    <div class="modal-header" style="background-color: rgba(230, 230, 230, 0.48)">
                        <h6 class="modal-title font-weight-bolder" style="float: left">Selección de filtros</h6>
                        <button type="button" class="close" data-dismiss="modal" style="float: right">&times;</button>
                    </div>--%>

        <%--                    <div class="modal-body">
                        <div class="row" id="barraFiltros">
                            <div class="col-lg-4">--%>
        <%-- <label>Fecha inicio:</label>
                                <asp:TextBox CssClass="form-control" type="text" ID="fechaInicio" runat="server" ClientIDMode="Static" />
                                <label>Fecha fin:</label>
                                <asp:TextBox CssClass="form-control" type="text" ID="fechaFin" runat="server" ClientIDMode="Static" />--%>
        <%--<br>--%>
        <%-- FILTRO TIPO --%>
        <%--         <label style="margin-left: 10px">Tipo</label>
                                <asp:RadioButtonList runat="server" ID="tipoRadioL">
                                    <asp:ListItem Text="No especificar" />
                                    <asp:ListItem Text="Venta" />
                                    <asp:ListItem Text="Compra" />
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-lg-3">--%>
        <%-- <label>Montos</label>
                                <asp:TextBox ID="montoMinimo" runat="server" type="number" CssClass="btn btn-light" Width="100%" placeholder="Monto mínima" />
                                <br />
                                <br />
                                <asp:TextBox ID="montoMaximo" runat="server" type="number" CssClass="btn btn-light" Width="100%" placeholder="Monto máxima" />--%>
        <%--        <br>
                                <br>
                            </div>

                            <div class="col-lg-5">
                                <label>Materiales</label>--%>
        <%--    <div class="overflow-auto" style="height: 300px; width: 100%; border: 1px solid rgba(221, 221, 221, 0.42)" aria-labelledby="dropdownMenuButton" aria-multiselectable="true">
                                    <asp:CheckBoxList runat="server" ID="materialesCB">
                                    </asp:CheckBoxList>
                                </div>--%>
        <%--  </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnFiltrarModal" type="submit" runat="server" Text="Filtrar" class="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>--%>



        <div class="row justify-content-center">
            <div class="col-2">
                <button class="btn btn-outline-secondary" type="button" data-toggle="collapse" data-target=".multi-collapse" aria-expanded="false"
                    aria-controls="filtroFechas filtroMontos filtroMateriales">
                    Búsqueda avanzada</button>
            </div>
            <div class="offset-7 col-3">
                <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <asp:TextBox type="text" ID="txtPalabra" class="form-control" runat="server" TextMode="SingleLine" placeholder="Búsqueda" AutoPostBack="true" OnKeyDown="txt_Item_Number_KeyDown" OnTextChanged="txtPalabra_TextChanged"></asp:TextBox>
                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <div class="collapse multi-collapse" id="filtroFechas">
                    <div class="card card-body">
                        <label>Fecha inicio:</label>
                        <asp:TextBox CssClass="form-control" type="text" ID="fechaInicio" runat="server" ClientIDMode="Static" />
                        <label>Fecha fin:</label>
                        <asp:TextBox CssClass="form-control" type="text" ID="fechaFin" runat="server" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="collapse multi-collapse" id="filtroMontos">
                    <div class="card card-body">
                        <label>Montos</label>
                        <asp:TextBox ID="montoMinimo" runat="server" type="number" CssClass="btn btn-light" Width="100%" placeholder="Monto mínima" />
                        <br />
                        <br />
                        <asp:TextBox ID="montoMaximo" runat="server" type="number" CssClass="btn btn-light" Width="100%" placeholder="Monto máxima" />
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="collapse multi-collapse" id="filtroMateriales">
                    <div class="card card-body">
                        <label>Materiales</label>
                        <div class="overflow-auto" style="height: 150px; width: 100%; border: 1px solid rgba(221, 221, 221, 0.42)" aria-labelledby="dropdownMenuButton" aria-multiselectable="true">
                            <asp:CheckBoxList runat="server" ID="materialesCB">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col"></div>
            <div class="col"></div>
            <div class="col">
                <div class="collapse multi-collapse" id="botonBusquedaAvanzada">
                    <asp:Button ID="btnBusquedaAvanzada" type="submit" runat="server" Text="Buscar" class="btn btn-primary" OnClick="btnFiltrar_Click" />
                </div>
            </div>
            <div class="col"></div>
            <div class="col"></div>
        </div>
        <br />

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView class="table table-hover table-bordered table-striped" ID="gridFacturas" runat="server" AllowSorting="True" PageSize="5" AllowPaging="True" OnPageIndexChanging="gridFact_PageIndexChanging" OnSorting="gridFact_Sorting" OnSelectedIndexChanged="gridFact_SelectedIndexChanged" OnKeyDown="" OnRowDataBound="gridFact_RowDataBound">
                    <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NumericFirstLast" PageButtonCount="2" />
                    <PagerStyle HorizontalAlign="Right" />
                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />

    </div>
</asp:Content>















