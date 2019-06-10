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

        $(document).ready(function () {
            $("#addSpan").click(function () {
                $("#nuevoAjusteDiv").show();
                $("#cancelSpan").show();
                $("#addSpan").hide();
            });
        });

        $(document).ready(function () {
            $("#cancelSpan").click(function () {
                $("#nuevoAjusteDiv").hide();
                $("#addSpan").show();
                $("#cancelSpan").hide();
            });
        });


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
            

        function ocultarDivNuevoAjuste() {
            $("#nuevoAjusteDiv").hide();
            $("#cancelSpan").hide();
        }

        window.onload = ocultarDivNuevoAjuste;

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Ajustes de inventario</li>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
   
    <div class="row justify-content-center" id="errorDiv">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div style="float: right;">
        <label class="h6">Bodega:</label>
        <asp:Label runat="server" CssClass="h6">B001</asp:Label>
    </div>

    <div class="container">
        <asp:HiddenField runat="server" ID="stock_id_escondido"/>
        <%--  <input class="btn btn-link" type="button" id="nuevoAjusteBTN2" value="Nuevo ajuste" style="float: right">--%>
        <div>
            <span id="addSpan" class="btn btn-light font-weight-bolder" style="width: 125px"><i class="fa fa-plus" style='color: forestgreen; margin-right: 5px'></i>Añadir</span>
            <span id="cancelSpan" class="btn btn-light font-weight-bolder" onclick="cancelSpanPresionado()" style="width: 125px"><i class="fas fa-times" style='color: red; margin-right: 5px'></i>Cancelar</span>
        </div>
        <div id="nuevoAjusteDiv">
            <%-- SECCION 1 NUEVO AJUSTE--%>
            <div class="row" style="margin-left: 1%">
                <h5>Detalles de ajuste</h5>
            </div>
            <div class="row" id="barraFiltros">
                <%-- PESO --%>
                <div class="filtroCell col-lg-2">
                    <label class="font-weight-bolder">Peso</label>
                    <div class="row" id="divPeso">
                        <asp:TextBox ID="pesoTB" runat="server" type="number" CssClass="btn btn-light" Width="90%" placeholder="Kg" />
                    </div>
                </div>
                <%-- MATERIALES --%>
                <div class="col-lg-2 filtroCell">
                    <div class="form-group">
                        <label class="font-weight-bolder">Material</label>
                        <div class="row" id="divMateriales">
                            <asp:DropDownList ID="materialDD" AutoPostBack="false" runat="server" CssClass="btn dropup btn-light">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <%-- UNIDAD--%>
                <div class="col-lg-2 filtroCell">
                    <label class="font-weight-bolder">Unidad</label>
                    <div class="row" id="divUbicaciones">
                        <div class="d-sm-table-cell" style="margin-left: 2%">
                            <asp:DropDownList ID="unidadTB" runat="server" CssClass="btn dropup btn-light"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <%-- ACCION --%>
                <div class="col-lg-2 filtroCell">
                    <label class="font-weight-bolder">Tipo</label>
                    <br>
                    <asp:RadioButtonList ID="radioAccion" runat="server" RepeatLayout="Flow">
                        <asp:ListItem Value="1">ENTRADA</asp:ListItem>
                        <asp:ListItem Value="0">SALIDA</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <%-- BODEGA --%>
                <div class="col-lg-2 filtroCell">
                    <label class="font-weight-bolder">Bodega</label>
                    <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="bodegasDrop" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <%-- RAZON --%>
            <div class="col-lg">
                <div class="form-group">
                    <label for="razonTb" class="font-weight-bolder">Razón de ajuste</label>
                    <asp:TextBox type="text" ID="razonTb" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <asp:Button ID="btnGuardar" type="button" runat="server" Text="Guardar" class="btn btn-info" Width="15%" OnClick="btnGuardar_Click" />
            </div>
            <%-- BOTON GUARDAR --%>
            <div class="row justify-content-start">
            </div>
        </div>
        <br>
        <table class="table table-bordered table-sm" id="tablaAjustes">
            <thead class="tabla_encabezado">
                <tr>
                    <%--fecha, peso, movimiento, stock--%>
                    <th scope="col">Fecha</th>
                    <th scope="col">Peso</th>
                    <th scope="col">Tipo</th>
                    <th scope="col">Acción</th>
                </tr>
            </thead>
            <tbody id="cuerpoTabla">
                <asp:PlaceHolder runat="server" ID="tablaPlaceHolder"></asp:PlaceHolder>
            </tbody>
        </table>
    </div>
</asp:Content>
