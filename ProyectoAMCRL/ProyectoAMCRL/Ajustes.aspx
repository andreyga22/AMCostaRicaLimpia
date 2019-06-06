<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Ajustes.aspx.cs" Inherits="ProyectoAMCRL.Ajustes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-3.4.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <link href="ownStyles.css" rel="stylesheet" />
    <script>
        var con = 1;
        $(document).ready(function () {
            $("#nuevoAjusteBTN2").click(function () {
                if (document.getElementById("nuevoAjusteBTN2").value == "Nuevo ajuste") {
                    $("#nuevoAjusteBTN2").val("Cancelar");
                    $("#nuevoAjusteDiv").show();
                } else {
                    $("#nuevoAjusteBTN2").val("Nuevo ajuste");
                     $("#nuevoAjusteDiv").hide();
                }
            });
        });

        function ocultarDivNuevoAjuste() {
                $("#nuevoAjusteDiv").hide();
        }

        window.onload = ocultarDivNuevoAjuste;

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Ajustes de inventario</li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
         <input class="btn btn-link" type="button" id="nuevoAjusteBTN2" value="Nuevo ajuste">
        <div id="nuevoAjusteDiv">
            <%-- SECCION 1 NUEVO AJUSTE--%>
            <div class="row">
                <h5>Detalles de ajuste</h5>
            </div>
            <div class="row" id="barraFiltros">
                <%-- PESO --%>
                <div class="filtroCell col-lg-2">
                    <label>Peso</label>
                    <div class="row" id="divMontos">
                        <asp:TextBox ID="montoTB" runat="server" type="number" CssClass="btn btn-light" Width="90%" placeholder="Kg" />
                    </div>
                </div>
                <%-- MATERIALES --%>
                <div class="col-lg-2 filtroCell">
                    <div class="form-group">
                        <label>Material</label>
                        <div class="row" id="divMateriales">
                            <asp:DropDownList ID="productosTB" AutoPostBack="false" runat="server" CssClass="btn dropup btn-light">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <%-- UNIDAD--%>
                <div class="col-lg-2 filtroCell">
                    <label>Unidad de peso</label>
                    <div class="row" id="divUbicaciones">
                        <div class="d-sm-table-cell" style="margin-left: 2%">
                            <asp:DropDownList ID="unidadTB" runat="server" CssClass="btn dropup btn-light"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <%-- ACCION --%>
                <div class="col-lg-2 filtroCell">
                    <label>Acción</label>
                    <div style="width: 100%" class="rolDiv">
                        <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="Aumentar" />
                    </div>
                    <div style="width: 100%" class="rolDiv">
                        <asp:RadioButton GroupName="MeasurementSystem" runat="server" Text="Disminuir" />
                    </div>
                </div>
                <%-- BODEGA --%>
                <div class="col-lg-2 filtroCell">
                    <label>Bodega</label>
                    <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="bodegasDrop" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-lg">
                <div class="form-group">
                    <label for="razonTb">Razón de ajuste</label>
                    <asp:TextBox type="text" ID="razonTb" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>

            <div class="row justify-content-center">
                <asp:Button ID="btnGuardar" type="submit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" />
            </div>
        </div>
         
        <table class="table table-bordered " id="tablaMateriales" >
            <thead class="tabla_encabezado">
                <tr>
                    <th scope="col">Material</th>
                    <th scope="col">Peso</th>
                    <th scope="col">Unidad de Medida</th>
                    <th scope="col">Bodega</th>
                    <th scope="col">Acción</th>
                    <th scope="col">Fecha</th>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder runat="server" ID="tablaPlaceHolder"></asp:PlaceHolder>
            </tbody>
        </table>
    </div>
</asp:Content>
