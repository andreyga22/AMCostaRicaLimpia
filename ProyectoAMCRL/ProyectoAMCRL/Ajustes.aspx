<%@ Page Title="AMCRL" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Ajustes.aspx.cs" Inherits="ProyectoAMCRL.Ajustes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-3.4.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <link href="ownStyles.css" rel="stylesheet" />



    <%-- TABLA JQUERY --%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css">
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>

    <script>

        //$(document).ready(function () {
        //    $("#addSpan").click(function () {
        //        $("#nuevoAjusteDiv").show();
        //        $("#cancelSpan").show();
        //        $("#addSpan").hide();
        //    });
        //});

        //$(document).ready(function () {
        //    $("#cancelSpan").click(function () {
        //        $("#nuevoAjusteDiv").hide();
        //        $("#addSpan").show();
        //        $("#cancelSpan").hide();
        //    });
        //});


        function abrirDetalleClick(infoAjuste) {
            window.location.replace("DetalleAjuste.aspx?awf=" + infoAjuste);
        }

        //CONSULTA JQUERY
        $(document).ready(function () {
            $('#tablaAjustes').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });
        });

        function cerrarError() {
            $("#errorDiv").hide();
        }


        //function ocultarDivNuevoAjuste() {
        //    $("#nuevoAjusteDiv").hide();
        //    $("#cancelSpan").hide();
        //}

        //window.onload = ocultarDivNuevoAjuste;

    </script>

    <script>

        $(document).ready(function () {
            $('[data-toggle="popover"]').popover();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active" style="color: dodgerblue">Ajustes de inventario</li>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="row justify-content-center" id="errorDiv">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>

    <div class="row justify-content-end" style="width: 100%;">
        <div style="float:right; ">
            <label class="h6">Bodega:</label>
            <asp:Label runat="server" CssClass="h6">B001</asp:Label>
        </div>
    </div>
    <br>


    <div class="container">
        <%-- <asp:HiddenField runat="server" ID="stock_id_escondido" />--%>
        <%--  <input class="btn btn-link" type="button" id="nuevoAjusteBTN2" value="Nuevo ajuste" style="float: right">--%>
        <%--  <div>
            <span id="addSpan" class="btn btn-light font-weight-bolder" style="width: 125px"><i class="fa fa-plus" style='color: forestgreen; margin-right: 5px'></i>Añadir</span>
            <span id="cancelSpan" class="btn btn-light font-weight-bolder" onclick="cancelSpanPresionado()" style="width: 125px"><i class="fas fa-times" style='color: red; margin-right: 5px'></i>Cancelar</span>
        </div>--%>

        <a href="DetalleAjuste.aspx" class="btn btn-info btn-sm" style="float: right">
            <span class="fa fa-plus"></span>
        </a>
        <table class="table table-bordered table-sm" id="tablaAjustes">
            <thead class="tabla_encabezado">
                <tr>
                    <%--fecha, peso, movimiento, stock--%>
                    <th scope="col" style="width:100px;">Consecutivo</th>
                    <th scope="col">Fecha</th>
                    <th scope="col">Peso</th>
                    <th scope="col">Bodega</th>
                    <th scope="col">Tipo</th>
                </tr>
            </thead>
            <tbody id="cuerpoTabla">
                <asp:PlaceHolder runat="server" ID="tablaPlaceHolder"></asp:PlaceHolder>
            </tbody>
        </table>
    </div>

</asp:Content>
