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
            $("#datepickerTB").datepicker($.datepicker.regional["es"]);
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
    </div>

    <div style="padding: 5px; width: 100%">

        <%-- ENCABEZADO --%>
        <div class="row rounded encabezado" style="width: 100%; background-color: rgba(226, 230, 227, 0.76); margin-left: 0%">

            <%-- COSECUTIVO --%>
            <div class="col-lg-2" style="margin-top: 5px;">
                <asp:Label ID="labelDatoConsecutivo" runat="server" class="h6 dato">Ajuste # </asp:Label>
                <asp:Label CssClass="h6" Text="5" runat="server" ID="labelDatoConsecutivoValor" />
            </div>
            <%-- BODEGA --%>
            <div class="col-lg-3">
                <label class="h6 dato">Bodega:</label>
                <asp:DropDownList OnSelectedIndexChanged="bodegasDrop_SelectedIndexChanged" class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="bodegasDrop" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </div>

            <%-- TIPO --%>
            <label class="font-weight-bolder" style="margin-top: 5px;">Tipo:</label>
            <div class="col-lg-5" style="margin-top: 5px; margin-right: 0%">
                <asp:RadioButtonList ID="radioAccion" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">Entrada</asp:ListItem>
                    <asp:ListItem Value="0">Salida</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <%-- FECHA --%>
            <div class="col-lg" style="margin-left: 0%">
                <asp:TextBox class="form-control font-weight-bolder" type="text" ID="datepickerTB" runat="server" ClientIDMode="Static" />
            </div>
        </div>
        <br />
        <%-- FILA PARA AGREGAR --%>
        <div class="row" style="margin-left: 0%; padding-left: 0%;">
            <asp:Label runat="server" id="labelInfoLinea" class="font-weight-bolder" style="margin-top: 4px; margin-right: 10px">Datos línea: </asp:Label>

            <%-- Producto --%>
            <asp:DropDownList ID="materialDD" OnSelectedIndexChanged="materialDD_SelectedIndexChanged1" AutoPostBack="true" runat="server" CssClass=" col-2 form-control form-control-sm dropdown-toggle"></asp:DropDownList>
            <div class="col-2" style="margin-left: 5px">
                <%-- Cantidad --%>
                <asp:TextBox Width="100%" min="0" placeholder="Cantidad" ID="cantidadTB" runat="server" type="text" class="form-control form-control-sm"/>
            </div>
            <%-- Unidad --%>
            <div class="col-3">
                <asp:DropDownList Width="100%" ID="unidadDD" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
            </div>
            <%-- Acción --%>
            <div class="col-1">
                <asp:LinkButton Width="100%" ID="agregarLineaBTN" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="agregarLineaClick" >
                  <i class="fa fa-plus"></i></asp:LinkButton>
            </div>
        </div>
         <div class="row">
             <div class="col-3" style="margin-right: 0%"></div>
             <div class="col-2">
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" display="Dynamic" runat="server" ErrorMessage="Cantidad inválida." ControlToValidate="cantidadTB" ForeColor="Red" ValidationExpression="^[0-9]+(\.([0-9]{1,8})?)?$" ValidationGroup="ajusteG"></asp:RegularExpressionValidator>
             </div>                                                                                                                                                                                                         
             <div class="col-2" style="margin-left:0%;">
             </div>
             <div class="col-3"></div>

        </div>
        

        <%-- DETALLES --%>
        <div class="row justify-content-end" style="width: 100%; margin-left: 0%; padding-right: 1%">
            <asp:Label ID="agregadosTextLabel" runat="server" class="" Style="margin-right: 1%" Text="Agregados:"></asp:Label>
            <asp:Label Text="0" runat="server" ID="labelAgregados" />
        </div>

        <div id="divDetallesEncabezado">
            <asp:Table ID="Table1" runat="server" class="table-sm " Style="width: 100%">
                <asp:TableHeaderRow ID="fila0Encabezado1" CssClass="btn-light font-weight-bolder position-relative">
                    <asp:TableCell Width="30%">Material</asp:TableCell>
                    <asp:TableCell Width="30%">Cantidad</asp:TableCell>
                    <asp:TableCell Width="30%">Unidad</asp:TableCell>
                    <asp:TableCell Width="10%">Acción</asp:TableCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <%-- CUERPO DE TABLA --%>
        <div class="overflow-auto" style="height: 250px;" id="divDetalles">
            <asp:Table ID="tablaDetalles" runat="server" class="table-sm  table-hover" Style="width: 100%">
                <asp:TableRow ID="fila0EncabezadoT2" Height="0%">
                    <asp:TableCell Width="31%"></asp:TableCell>
                    <asp:TableCell Width="30%"></asp:TableCell>
                    <asp:TableCell Width="30%"></asp:TableCell>
                    <asp:TableCell Width="9%" ID="columnaAccionCuerpo"></asp:TableCell>
                </asp:TableRow>
                <asp:TableHeaderRow ID="fila0Encabezado2" Visible="false" CssClass="btn-light font-weight-bolder position-relative">
                    <asp:TableCell>Material</asp:TableCell>
                    <asp:TableCell>Cantidad</asp:TableCell>
                    <asp:TableCell>Unidad</asp:TableCell>
                </asp:TableHeaderRow>
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
