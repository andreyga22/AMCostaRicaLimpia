<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaSocios.aspx.cs" Inherits="ProyectoAMCRL.BusquedaSocios" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-3.4.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <link href="ownStyles.css" rel="stylesheet" />
    <script>

        <%-- $(document).ready(function () {
            $("#nombreCb").click(function () {
                if (document.getElementById("nombreCb").checked == true) {
                    $("#divNombre").show();
                } else {
                    $("#divNombre").hide();
                }
            });
        });--%>

        //$(document).ready(function () {
        //    $("#rolCb").click(function () {
        //        if (document.getElementById("rolCb").checked == true) {
        //            $(".rolDiv").show();
        //        } else {
        //            $(".rolDiv").hide();
        //        }
        //    });
        //});

        //$(document).keydown(function (keyPressed) {
        //    if (keyPressed.keyCode == 13) {
        //        alert("ENTER PRESIONADO");
        //    }
        //});

        //function ocultarFiltros() {
            <%--$("#divNombre").hide();--%>
        //    $(".rolDiv").hide();

        //}

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
        <br />

        <br>
       <%--<%-- <%-- SECCION 1 FILTROS--%>

        <!-- Modal -->
        <div class="container">
           <%-- <div class="modal fade" id="filtros" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLongTitle">Filtros</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="container">
                            <div class="row" id="barraFiltros">--%>


                                <%--FILTRO ROL--%>
                                <%--<div class="col-lg-6 filtroCell">
                                    <strong>
                                        <input class="form-check-input" type="checkbox" id="rolCb" font-weight: bold value="">Tipo</strong>
                                    <div style="width: 100%" class="rolDiv">
                                        <asp:RadioButton ID="radioRol" GroupName="MeasurementSystem" runat="server" Text="Cliente" />
                                    </div>
                                    <div style="width: 100%" class="rolDiv">
                                        <asp:RadioButton ID="radioRol2" GroupName="MeasurementSystem" runat="server" Text="Proveedor" />
                                    </div>
                                </div>
                            </div>
                          </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>--%>
                            <%--<button type="button" class="btn btn-primary">Filtrar</button>--%>
                           <%-- <asp:Button ID="btnFiltrarModal" type="submit" runat="server" Text="Filtrar" class="btn btn-primary" OnClick="btnFiltrar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>


       <div class="row justify-content-center">
           <div class="col-2">
                <asp:Button ID="NuevoBtn" type="submit" runat="server" class="btn btn-info" Text="Nuevo" OnClick="NuevoBtn_Click" />
            </div>
            <div class="col-3 offset-7">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox type="text" ID="txtPalabra" class="form-control" runat="server" TextMode="SingleLine" placeholder="Buscar" AutoPostBack="true" OnKeyDown="txt_Item_Number_KeyDown" OnTextChanged="txtPalabra_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <%-- SECCION 2 --%>
        <div class="row justify-content-center">
            <div class="col-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridSocios" runat="server" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gridSocios_PageIndexChanging" OnSorting="gridSocios_Sorting" OnSelectedIndexChanged="gridSocios_SelectedIndexChanged" OnKeyDown="" OnRowDataBound="gridSocios_RowDataBound" PageSize="5">
                            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NextPreviousFirstLast" PageButtonCount="4" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle"/>                             
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                            <PagerStyle HorizontalAlign="Right" />
                        </asp:GridView>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        </div>
</asp:Content>
