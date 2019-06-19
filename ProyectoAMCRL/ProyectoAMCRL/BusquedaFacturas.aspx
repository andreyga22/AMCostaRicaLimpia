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




        <button type="button" class="btn btn-outline-secondary" data-toggle="modal" data-target="#filtros">
            Filtros
        </button>

        <!-- Modal -->
            <div class="container">
        <div class="modal fade" id="filtros" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Filtros</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row" id="barraFiltros">
                            <div class="col-lg-6 filtroCell">

                                <strong>
                                    <input class="form-check-input" type="checkbox" id="fechasCb" value="" font-weight: bold>Fecha</strong>
                                <br />
                                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

                                <div class="row" id="divFecha">
                                    <p>
                                        Fecha Inicio:
        <input type="text" id="datepicker" runat="server" clientidmode="Static" />
                                    </p>

                                    <p>
                                        Fecha Fin:
        <input type="text" id="datepicker2" runat="server" clientidmode="Static" />
                                    </p>
                                </div>
                            </div>
                                    <%-- FILTRO MATERIALES --%>
                            <div class="col-lg-6 filtroCell">
                                <div class="form-group">
                                    <strong>
                                        <input class="form-check-input" type="checkbox" id="materialesCb" font-weight: bold value="">Material</strong>
                                    <div class="row" id="divMateriales">
                                        <%--<asp:DropDownList OnSelectedIndexChanged="materialesDrop_SelectedIndexChanged" class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" Height="40px" ID="materialDd" runat="server" Width="90%" AutoPostBack="True">
                                        </asp:DropDownList>--%>
                                        <asp:ListBox ID="materialDda"  runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="filtroCell col-lg-6">
                                <strong>
                                    <input class="form-check-input" type="checkbox" id="montosCb" value="" font-weight: bold>Monto en facturas</strong>

                                <div class="row" id="divMontos">
                                    <asp:TextBox ID="montoMinimo" Height="30px" runat="server" type="number" CssClass="btn btn-light" Width="90%" placeholder="Máximo" />
                                    <asp:TextBox ID="montoMax" Height="30px" runat="server" type="number" CssClass="btn btn-light" Width="90%" placeholder="Mínimo" />
                                </div>
                            </div>
                            <br />
                    
                            <div class="col-lg-6 filtroCell">
                                <strong>
                                    <input class="form-check-input" type="checkbox" id="rolCb" font-weight: bold value="">Tipo</strong>
                                <div style="width: 100%" class="rolDiv">
                                    <asp:RadioButton ID="radioRol" GroupName="MeasurementSystem" runat="server" Text="Venta" />
                                </div>
                                <div style="width: 100%" class="rolDiv">
                                    <asp:RadioButton ID="radioRol2" GroupName="MeasurementSystem" runat="server" Text="Compra" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        <%--<button type="button" class="btn btn-primary">Filtrar</button>--%>
                         <asp:Button ID="btnFiltrarModal" type="submit" runat="server" Text="Filtrar" class="btn btn-primary" OnClick="btnActualizar_Click" />
                    </div>
                </div>
            </div>
        </div>
                </div>







        <%--        <div class="row" id="barraFiltros">--%>

        <%-- <div class="col-lg-3 filtroCell">

                <label>
                    <input class="form-check-input" type="checkbox" id="fechasCb" value="">Fecha</label>

                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

        <%--  <div class="row" id="divFecha">
                    <p>
                        Fecha Inicio:
        <input type="text" id="datepicker" runat="server" clientidmode="Static" />
                    </p>


                    <p>
                        Fecha Fin:
        <input type="text" id="datepicker2" runat="server" clientidmode="Static" />
                    </p>


                </div>
            </div>--%>

        <%-- FILTRO MONTOS --%>
        <%-- <div class="filtroCell col-lg-3">
                <label>
                    <input class="form-check-input" type="checkbox" id="montosCb" value="">Monto en facturas</label>--%>

        <%--    <div class="row" id="divMontos">
                    <asp:TextBox ID="montoMinimo" Height="30px" runat="server" type="number" CssClass="btn btn-light" Width="90%" placeholder="Máximo" />
                    <asp:TextBox ID="montoMax" Height="30px" runat="server" type="number" CssClass="btn btn-light" Width="90%" placeholder="Mínimo" />
                </div>
            </div>--%>
        <%-- FILTRO MATERIALES --%>
        <%--     <div class="col-lg-3 filtroCell">
                <div class="form-group">
                    <label>
                        <input class="form-check-input" type="checkbox" id="materialesCb" value="">Material</label>
                    <div class="row" id="divMateriales">
                        <asp:DropDownList OnSelectedIndexChanged="materialesDrop_SelectedIndexChanged" class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" Height="40px" ID="materialDd" runat="server" Width="90%" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>--%>

        <%-- FILTRO ROL --%>
        <%--  <div class="col-lg-3 filtroCell">
                <label>
                    <input class="form-check-input" type="checkbox" id="rolCb" value="">Tipo</label>
  
                <div style="width: 100%" class="rolDiv">
                    <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="Venta" />
                </div>
                <div style="width: 100%" class="rolDiv">
                    <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="Compra" />
                </div>
            </div>
        </div>
        <br />--%>
        <%--     <div class="row justify-content-center">
           
        </div>--%>

     <%--   <div class="row">
            <h4>Administrar Facturas</h4>
        </div>--%>
        <div class="row justify-content-center">
            <div class="col-3 offset-7">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox type="text" ID="txtPalabra" class="form-control" runat="server" TextMode="SingleLine" placeholder="Buscar" AutoPostBack="True" OnKeyDown="txt_Item_Number_KeyDown" OnDataBinding="palabraTb_TextChanged" OnTextChanged="palabraTb_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-1">
                <asp:Button ID="btnActualizar" type="submit" runat="server" Text="Actualizar búsqueda" class="btn btn-outline-secondary" OnClick="btnActualizar_Click" />
                <%--<asp:Button ID="btnAgregar" runat="server" Text="Nuevo" class="btn btn-info" Width="180%" OnClick="btnAgregar_Click" />--%>
            </div>
        </div>
        <br />


        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridFacturas" runat="server" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gridFact_PageIndexChanging" OnSorting="gridFact_Sorting" OnSelectedIndexChanged="gridFact_SelectedIndexChanged" OnKeyDown="" OnRowDataBound="gridFact_RowDataBound" PageSize="5">
            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NumericFirstLast" PageButtonCount="4" />
            <PagerStyle HorizontalAlign="Right" />

        </asp:GridView>
        <br />
        <%-- <div class="row justify-content-center">
        <asp:Button ID="btnGuardar" type="submmit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" />
    </div>--%>
    </div>
</asp:Content>















