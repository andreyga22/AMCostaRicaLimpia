<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="DetalleAjuste.aspx.cs" Inherits="ProyectoAMCRL.DetalleAjuste" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dato {
            margin-right: 10px;
            font-weight: bolder;
        }

        .encabezado {
            background-color: rgba(155, 173, 155, 0.43);
            margin-left: 0%;
            width: 100%;
            padding-bottom: 5px;
            padding-top: 5px;
        }

        #razonDiv {
            margin-left: 2%;
            width: 98%;
        }
    </style>
    <script>

        function volverAajustes() {
             window.location.replace("Ajustes.aspx");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
     <li class="breadcrumb-item" style="color: dodgerblue"><a href="Ajustes.aspx">Ajustes de inventario</a></li>
    <li class="breadcrumb-item active" >Vista de detalle</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
     <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>

    <div class="row" style="padding-right:11.5%; ">
        <div class="col">
        <h4>Detalle de ajuste</h4>
        </div>

        <div style="float:right; height:30px;" class="rounded btn-dark" onclick="volverAajustes()">
            <span>
                <i class="fa fa-fast-backward" style="margin-left: 5px; margin-top:5px;"></i>
            </span>
            <span style="margin-right:5px">
                <label>Regresar</label>
            </span>
        </div>
    </div>

    <div style="padding: 5px; border: 2px solid rgba(35, 94, 35, 0.34); width: 90%">
        <div class="row rounded encabezado" style="width: 100%">
            <div class="col">
                <label class="h5 dato">Fecha:</label>
                <asp:Label CssClass="h5" Text="13/15/19" runat="server" ID="labelFecha" />
                <div style="float: right">
                    <label class="h5 dato">Tipo:</label>
                    <asp:Label CssClass="h5" Text="SALIDA" runat="server" ID="labelTipo" />
                </div>
            </div>
        </div>
        <br>
        <br>
        <div class="row" style="background-color: white; width: 100%; margin-left: 0%;">
            <label class="h5 dato">Bodega:</label>
            <asp:Label CssClass="h5" Text="Bodega 1" runat="server" ID="labelBodega" />
        </div>
        <br>
        <div class="row" style="background-color: white; width: 100%; margin-left: 0%;">
            <label class="h5 dato">Material involucrado:</label>
            <asp:Label CssClass="h5" Text="Chayotes" runat="server" ID="labelMaterial" />
        </div>
        <br>
        <br>
        <div class="row" style="background-color: white; width: 100%; margin-left: 0%;">
            <label class="h5 dato">Total de kilos de ajuste:</label>
            <asp:Label CssClass="h5" Text="95" runat="server" ID="labelCantidad" />
        </div>
        <br>
        <br>
        <div class="row" style="background-color: white; width: 100%; margin-left: 0%;">
            <label class="h5 dato">Razón de ajuste:</label>
            <div id="razonDiv">
                <asp:TextBox type="text" ID="razonTb" class="form-control" Font-Size="Large" runat="server" BackColor="Transparent" BorderStyle="None" TextMode="MultiLine" Text="Se extravia el material" ForeColor="Black" Enabled="false" Width="100%"></asp:TextBox>
            </div>
        </div>
    </div>




</asp:Content>
