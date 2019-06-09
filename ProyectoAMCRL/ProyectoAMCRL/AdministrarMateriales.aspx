<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="AdministrarMateriales.aspx.cs" Inherits="ProyectoAMCRL.Materiales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <link rel='stylesheet' href='https://use.fontawesome.com/releases/v5.7.0/css/all.css'>

    <%-- TABLA JQUERY --%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css">
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>



    <script>
        //$(document).ready(function () {
        //    $("#inputBusqueda").on("keyup", function () {
        //        var value = $(this).val().toLowerCase();
        //        $("#cuerpoTabla tr").filter(function () {
        //            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        //        });
        //    });
        //});

        //CONSULTA JQUERY
        $(document).ready(function () {
            $('#tablaMateriales2').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });
        });


        //function addSpanPresionado() {
        //    $("#divCamposNuevoMaterial").show();
        //    $("#addSpan").hide();
        //    $("#cancelSpan").show();
        //}

        //function cancelSpanPresionado() {
        //    $("#divCamposNuevoMaterial").hide();
        //    $("#cancelSpan").hide();
        //    $("#addSpan").show();
        //}

        //function ocultarDivs() {
        //    $("#divCamposNuevoMaterial").hide();
        //    $("#cancelSpan").hide();
        //}

        function abrirDetalleClick(id) {
            var info = id.split(".");
            var idM = info[0];
            var nombre = info[1];
            var precio = info[2];
            window.location.replace("RegistroMateriales.aspx?idM="+idM+"&nom="+nombre+"&prec="+precio);
        }

        window.onload = ocultarDivs;

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
  <li class="breadcrumb-item"><a href="AdministrarMateriales.aspx">Materiales</a></li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <br>
    <a href="RegistroMateriales.aspx" class="btn btn-info btn-sm" style="float:right">
      <span class="fa fa-plus"></span>
    </a>
     <%--<span id="addSpan2" class="btn btn-light font-weight-bolder" onclick="addSpanPresionado()" 
         style="float:right; margin-left:0px">
         <i class="fa fa-plus" style='color: forestgreen;'></i>
         <a href="RegistroMateriales.aspx"></a>
     </span>--%>
    <table class="table table-bordered table-sm" id="tablaMateriales2">
        <thead>
            <tr>
                <th scope="col">Codigo</th>
                <th scope="col">Nombre</th>
                <th scope="col">Precio kilo</th>
                <th scope="col">Acción  
                </th>
            </tr>
        </thead>  
        <tbody id="cuerpoTabla" class="table-sm">
            <asp:PlaceHolder runat="server" ID="tablaPlaceHolder"></asp:PlaceHolder>
        </tbody>
    </table>
    <div />
    <br><br>
</asp:Content>
