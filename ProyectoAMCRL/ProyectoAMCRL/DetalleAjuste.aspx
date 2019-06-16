<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="DetalleAjuste.aspx.cs" Inherits="ProyectoAMCRL.DetalleAjuste" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        #razonDiv {
            margin-left: 0%;
            width: 100%;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item" style="color: dodgerblue"><a href="Ajustes.aspx">Ajustes de inventario</a></li>
    <li class="breadcrumb-item active">
        <asp:Label ID="labelBreadCrumb" Text="Nuevo ajuste" runat="server" />
    </li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>

    <div class="row" style="padding-right: 3.5%;">

        <div class="col">
            <asp:Label runat="server" CssClass="h5 font-weight-bolder">Detalle de ajuste</asp:Label>
        </div>
        <%--<div style="float: right; height: 30px;" class="rounded btn-dark" onclick="volverAajustes()">
            <span>
                <i class="fa fa-fast-backward" style="margin-left: 5px; margin-top: 5px;"></i>
            </span>
            <span style="margin-right: 5px">
                <label>Regresar</label>
            </span>
        </div>--%>
    </div>

    <div style="padding: 5px; width: 100%">

        <%-- ENCABEZADO --%>
        <div class="row rounded encabezado" style="width: 100%; background-color: rgba(226, 230, 227, 0.76)">
            <div class="col">
                <%-- COSECUTIVO --%>
                <div style="margin-top: 5px; float: left">
                    <asp:Label ID="labelDatoConsecutivo" runat="server" class="h6 dato">Ajuste número: </asp:Label>
                    <asp:Label CssClass="h6" Text="5" runat="server" ID="labelNumero" />
                </div>

                <%-- FECHA --%>
                <div style="float: right">
                    <input class="form-control font-weight-bolder" type="text" id="datepicker" runat="server" clientidmode="Static" style="width: 120px" />
                    <%-- <asp:Label CssClass="h5" Text="12/10/2019" runat="server" ID="labelFecha" />
                    <asp:Label CssClass="h5" Text="SALIDA" runat="server" ID="labelTipo" />--%>
                </div>
            </div>
        </div>

        <div class="row" style="width: 100%; margin-left: 0%;">
            <%-- BODEGA --%>
            <div>
                <label class="h6 dato">Bodega:</label><br>
                <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="bodegasDrop" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </div>

            <%-- TIPO --%>
            <label class="h6 dato" style="margin-left: 10%">Tipo:</label>
            <asp:RadioButtonList ID="radioAccion" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                <asp:ListItem Value="1">Entrada</asp:ListItem>
                <asp:ListItem Value="0">Salida</asp:ListItem>
            </asp:RadioButtonList>

        </div>
        <%-- DETALLES --%>
        <div class="row justify-content-end" style="width: 100%; margin-left: 0%; padding-right: 1%">
            <label class="font-weight-bolder" style="margin-right: 1%">Agregados</label>
            <asp:Label Text="0" runat="server" ID="labelAgregados" />
        </div>

        <div id="divDetallesEncabezado">
            <asp:Table ID="Table1" runat="server" class="table-sm " Style="width: 100%">
                <asp:TableHeaderRow CssClass="btn-light font-weight-bolder position-relative">
                    <asp:TableCell Width="30%">Material</asp:TableCell>
                    <asp:TableCell Width="30%">Cantidad</asp:TableCell>
                    <asp:TableCell Width="30%">Unidad</asp:TableCell>
                    <asp:TableCell Width="10%">Acción</asp:TableCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <%-- Producto --%>
                    <asp:TableCell>
                        <asp:DropDownList Width="100%" ID="materialDD" AutoPostBack="false" runat="server" CssClass="btn btn-light btn-sm dropdown-toggle dropup"></asp:DropDownList>
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
                        <asp:LinkButton Width="100%" ID="agregarLineaBTN" runat="server" CssClass="btn btn-info btn-sm" OnClick="agregarLineaClick">
                           <i class="fa fa-plus"></i> Agregar</asp:LinkButton>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <%-- CUERPO DE TABLA --%>
        <div class="overflow-auto" style="height: 250px;" id="divDetalles">
            <asp:Table ID="tablaDetalles" runat="server" class="table-sm  table-hover" Style="width: 100%">
                <asp:TableRow Height="0%">
                    <asp:TableCell Width="31%"></asp:TableCell>
                    <asp:TableCell Width="30%"></asp:TableCell>
                    <asp:TableCell Width="30%"></asp:TableCell>
                    <asp:TableCell Width="9%"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <%-- RAZON --%>
        <div class="row" style="width: 100%; margin-left: 0%;">
            <label class="h6 dato">Razón de ajuste:</label>
            <div id="razonDiv">
                <asp:TextBox type="text" ID="razonTb" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <br />
        <asp:Button ID="btnGuardar" type="button" runat="server" Text="Guardar" class="btn btn-info" Width="15%" OnClick="btnGuardar_Click" />
    </div>

</asp:Content>
