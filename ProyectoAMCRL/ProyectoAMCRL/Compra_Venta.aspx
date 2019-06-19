<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Compra_Venta.aspx.cs" Inherits="ProyectoAMCRL.Compra_Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
    <script src="jquery-3.4.0.min.js"></script>

    <%-- DATE PICKER --%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
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
    <style>
        .dato {
            margin-right: 10px;
            font-weight: bolder;
        }

        .datoHead {
            margin-right: 0px;
            font-weight: bolder;
        }

        .encabezado {
            background-color: rgba(155, 173, 155, 0.43);
            margin-left: 0%;
            width: 100%;
            padding-bottom: 5px;
            padding-top: 5px;
            margin-bottom: 5px;
        }

        #divDetallesEncabezado {
            border-left: 1px solid #e8e8e8;
            border-right: 1px solid #e8e8e8;
            border-top: 1px solid #e8e8e8;
        }


        #divDetalles {
            border-left: 1px solid #e8e8e8;
            border-right: 1px solid #e8e8e8;
            border-bottom: 1px solid #e8e8e8;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Compras</li>
    <li class="breadcrumb-item active">
        <asp:Label ID="labelBreadCrum" Text="" runat="server" />
    </li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <%-- DATOS DEL CLIENTE  CARGADOS--%>
    <div id="datosSocio">
        <asp:Label ID="labelDatosSocio" Text="" runat="server" class="font-weight-bold" />
        <br>
        <div class="row" style="margin-left: 0%">
            <div class="col-lg-4">
                <asp:Label runat="server" CssClass="font-weight-bold campoIzq" Height="30px" for="labelCedula" Text="Identificación:"></asp:Label>
                <asp:Label CssClass="campo" runat="server" ID="labelCedula" Text="73648"></asp:Label>
                <br>
                <label class="font-weight-bold campoIzq" for="labelDireccion">Dirección: </label>
                <asp:Label class="campo" runat="server" ID="labelDireccion">San Ramón, Alajuela</asp:Label>
            </div>
            <div class="col-lg-3">
                <label class="font-weight-bold campoIzq" for="labelNombre">Nombre: </label>
                <asp:Label class="campo" runat="server" ID="labelNombre">Pepe Figueres</asp:Label>
                <br>
                <label class="font-weight-bold campoIzq" for="telLabel">Teléfono: </label>
                <asp:Label class="campo" runat="server" ID="labelTel">88447799</asp:Label>
            </div>
        </div>
    </div>

    <%-- SECCION COMPRA --%>
    <div style="padding: 5px; width: 100%;">

        <%-- ENCABEZADO --%>
        <div class="row rounded encabezado" style="width: 100%; background-color: rgba(226, 230, 227, 0.76)">

            <%-- COSECUTIVO --%>
            <div class="col-lg-2" style="margin-top: 5px;">
                <asp:Label ID="labelDatoConsecutivo" runat="server" class="h6 dato"></asp:Label>
                <asp:Label ID="labelValorDatoConsecutivo" CssClass="h6" Text="0" runat="server" />
            </div>
            <%-- BODEGA --%>
            <div class="col-lg-3">
                <label class="h6 dato">Bodega:</label>
                <asp:DropDownList OnSelectedIndexChanged="bodegasDrop_SelectedIndexChanged" class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="bodegasDrop" runat="server" AutoPostBack="true" Width="150px">
                </asp:DropDownList>
            </div>
            <%-- MONEDA --%>
            <div class="col-lg-3">
                <label class="h6 dato">Moneda:</label>
                <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="monedasDD" runat="server" AutoPostBack="True" Width="150px">
                </asp:DropDownList>
            </div>
            <%-- FECHA --%>
            <div class="col-lg" style="float: right; ">
                <input class="form-control font-weight-bolder" type="text" id="datepicker" runat="server" clientidmode="Static" style="width: 120px" />
            </div>

        </div>
        <%-- DETALLES --%>
        <div class="row justify-content-end" style="width: 100%; margin-left: 0%; padding-right: 1%">
            <label class="font-weight-bolder" style="margin-right: 1%">Agregados</label>
            <asp:Label Text="0" runat="server" ID="labelAgregados" />
        </div>

        <div id="divDetallesEncabezado">
            <asp:Table ID="Table1" runat="server" class="table-sm " Style="width: 100%">
                <asp:TableHeaderRow CssClass="btn-light font-weight-bolder position-relative">
                    <asp:TableCell Width="20%">Material</asp:TableCell>
                    <asp:TableCell Width="20%">Precio kilo/unidad</asp:TableCell>
                    <asp:TableCell Width="20%">Cantidad</asp:TableCell>
                    <asp:TableCell Width="30%">Unidad</asp:TableCell>
                    <asp:TableCell Width="10%">Acción</asp:TableCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <%-- Producto --%>
                    <asp:TableCell>
                        <asp:DropDownList Width="100%" ID="materialDD" AutoPostBack="false" runat="server" CssClass="btn btn-light btn-sm dropdown-toggle dropup"></asp:DropDownList>
                    </asp:TableCell>
                    <%-- Precio kilo --%>
                    <asp:TableCell>
                        <asp:TextBox Width="100%" ID="precioKgTB" runat="server" type="number" CssClass="btn btn-light btn-sm" />
                    </asp:TableCell>
                    <%-- Cantidad --%>
                    <asp:TableCell>
                        <asp:TextBox Width="100%" ID="cantidadTB" runat="server" type="number" CssClass="btn btn-light btn-sm" />
                    </asp:TableCell>
                    <%-- Unidad --%>
                    <asp:TableCell>
                        <asp:DropDownList Width="100%" ID="unidadDD" runat="server" CssClass="btn dropup btn-light btn-sm dropu"></asp:DropDownList>
                    </asp:TableCell>
                    <%-- Acción --%>
                    <asp:TableCell>
                        <asp:LinkButton Width="100%" ID="agregarLineaBTN" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="agregarLineaClick">
                           <i class="fa fa-plus"></i></asp:LinkButton>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <%-- CUERPO DE TABLA --%>
        <div class="overflow-auto" style="height: 250px;" id="divDetalles">
            <asp:Table ID="tablaDetalles" runat="server" class="table-sm  table-hover" Style="width: 100%">
                <asp:TableRow Height="0%">
                    <asp:TableCell Width="20%"></asp:TableCell>
                    <asp:TableCell Width="20%"></asp:TableCell>
                    <asp:TableCell Width="20%"></asp:TableCell>
                    <asp:TableCell Width="30%"></asp:TableCell>
                    <asp:TableCell Width="10%"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div style="float: right">
            <asp:Label Text="Total: " runat="server" />
            <asp:Label ID="totalLabel" Text="0" runat="server" />
        </div>
        <br>
        <asp:Button ID="btnGuardar" type="button" runat="server" Text="Guardar" class="btn btn-info" Width="15%" OnClick="btnGuardar_Click" />
    </div>
</asp:Content>
