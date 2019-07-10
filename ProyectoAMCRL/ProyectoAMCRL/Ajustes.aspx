<%@ Page Title="AMCRL" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Ajustes.aspx.cs" Inherits="ProyectoAMCRL.Ajustes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-3.4.0.min.js"></script>
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

    <%-- TABLA JQUERY --%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css">
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>

    <script>
        function abrirDetalleClick(infoAjuste) {

            window.location.replace("DetalleAjuste.aspx?view=" + infoAjuste);
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


        $(function () {
            $("#fechaInicioTB").datepicker($.datepicker.regional["es"]);
        });

        $(function () {
            $("#fechaFinTB").datepicker($.datepicker.regional["es"]);
        });


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
    <h4>Ajustes de inventario</h4>
    <br />
    <!-- Modal -->
    <div class="container">

        <!-- Trigger the modal with a button -->
        <button type="button" class="btn btn-outline-secondary " data-toggle="modal" data-target="#myModal">Filtrar</button>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content " style="height: 500px;">
                    <div class="modal-header" style="background-color:rgba(230, 230, 230, 0.48)">
                        <h6 class="modal-title font-weight-bolder" style="float: left">Selección de filtros</h6>
                        <button type="button" class="close" data-dismiss="modal" style="float: right">&times;</button>
                    </div>
                    <div class="modal-body">

                        <div class="row" id="barraFiltros">

                            <%-- FECHAS --%>

                            <div class="col-lg-4">
                                <label>Fecha inicio:</label>
                                <asp:TextBox CssClass="form-control" type="text" ID="fechaInicioTB" runat="server" ClientIDMode="Static" />
                                <label>Fecha fin:</label>
                                <asp:TextBox CssClass="form-control" type="text" ID="fechaFinTB" runat="server" ClientIDMode="Static" />
                                <br>
                                <%-- FILTRO TIPO --%>
                                <label style="margin-left: 10px">Tipo</label>
                                <asp:RadioButtonList runat="server" ID="tipoRadioL">
                                    <asp:ListItem Text="No especificar" />
                                    <asp:ListItem Text="Entrada" />
                                    <asp:ListItem Text="Salida" />
                                </asp:RadioButtonList>

                            </div>

                            <%-- FILTRO MONTOS --%>
                            <div class="col-lg-3" >
                                <label>Cantidad materiales</label>
                                <asp:TextBox ID="pesoMin" runat="server" type="number" CssClass="btn btn-light" Width="100%" placeholder="Cantidad mínima" />
                                <br />
                                <br />
                                <asp:TextBox ID="pesoMax" runat="server" type="number" CssClass="btn btn-light" Width="100%" placeholder="Cantidad máxima" />
                                <br>
                                <br>
                                <%-- BODEGAS --%>
                                <label>Bodegas</label>
                                <div  >
                                <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="bodegasDrop" runat="server" AutoPostBack="false" Width="150px" >
                                    <asp:ListItem Value="">Ninguna</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </div>


                            <%-- FILTRO MATERIALES --%>
                            <div class="col-lg-5">
                                <label>Materiales</label>
                                <div class="overflow-auto" style="height: 300px; width: 100%; border: 1px solid rgba(221, 221, 221, 0.42)" aria-labelledby="dropdownMenuButton" aria-multiselectable="true">
                                    <asp:CheckBoxList runat="server" ID="materialesCB">
                                    </asp:CheckBoxList>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button OnClick="btnFiltros_Click" CssClass="btn btn-primary" Text="Aplicar filtros" runat="server" id="btnFiltros" AutoPostBack="false"/>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%--<div class="row justify-content-end" style="width: 100%;">
        <div style="float: right;">
            <label class="h6">Bodega:</label>
            <asp:Label runat="server" CssClass="h6">B001</asp:Label>
        </div>
    </div>--%>

    <asp:Label runat="server" ID="labelP"></asp:Label>
    <div class="container">

        <a href="DetalleAjuste.aspx" class="btn btn-info btn-sm" style="float: right">
            <span class="fa fa-plus"></span>
        </a>
        <table class="table table-bordered table-sm" id="tablaAjustes">
            <thead class="tabla_encabezado">
                <tr>
                    <%--fecha, # de Materiales, movimiento, stock--%>
                    <th scope="col" style="width: 100px;">Consecutivo</th>
                    <th scope="col">Fecha</th>
                    <th scope="col">Cantidad de materiales</th>
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
