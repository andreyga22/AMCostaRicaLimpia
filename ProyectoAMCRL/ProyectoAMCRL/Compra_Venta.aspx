<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Compra_Venta.aspx.cs" Inherits="ProyectoAMCRL.Compra_Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
    <script src="jquery-3.4.0.min.js"></script>
    <script>
        var nomIdeSocioCambiado = false;
    </script>
    <script type="text/javascript">
        function lineaNueva() {
            alert("asdas");
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
            <li class="breadcrumb-item active">Compras</li>
            <li class="breadcrumb-item active">Registrar nueva compra</li>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="sideNavBody" runat="server">
   <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
<div class="row justify-content-center" style="background-color:red">
       <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
</div>
    <div class="container">
            <div class="row">
                <div id="divFuncionPagina" class="cell col-9" style="">
                    <h4 id="funcionPaginaLabel" class="font-weight-bold">Compra</h4>
                </div>
                <div id="fecha" class="row col-3">
                    <h5 style="margin-right: 10%;">Fecha: </h5>
                    <h5 id="fechaLabel">4/5/19</h5>
                </div>
            </div>
            <br>
             <br>
            <label class="font-weight-bold">AM Costa Rica Limpia</label>
            <br>
            <div class="row">
                <div class="col-lg-6">
                    <label class="font-weight-bold campoIzq" for="telLabel">Número telefónico: </label>
                    <asp:Label class="campo" runat="server" ID="telLabel">87885522</asp:Label>
                    <br>
                    <label class="font-weight-bold campoIzq" for="telLabel">Bodega: </label>
                    <asp:Label class="campo" runat="server" ID="Label1">Bodega San Ramon</asp:Label>
                    <br>
                    <label class="font-weight-bold campoIzq" for="telLabel">Dirección: </label>
                    <asp:Label class="campo" runat="server" ID="Label4">San Ramón, Alajuela</asp:Label>
                </div>
                <div class="col-lg-6">
                    <label class="font-weight-boldv campoIzq" for="telLabel">Factura #: </label>
                    <asp:Label class="campo" runat="server" ID="Label2">0111</asp:Label>
                    <br>
                    <label class="font-weight-bold campoIzq" for="telLabel">Moneda: </label>
                    <asp:DropDownList ID="monedas" runat="server" CssClass="btn dropdown btn-light"></asp:DropDownList>
                    <%-- <asp:Label class="campo" runat="server" ID="Label3">COL</asp:Label>--%>
                </div>
            </div>
             <br>
             <br>
            <h5 class="font-weight-bold">Datos del socio</h5>
            <%-- SECCION 2 --%>
            <div class="container">
                <div class="row">
                    <div class="col-lg-3">
                        <asp:TextBox type="text"  class="form-control"  ID="nomIdeSocio" runat="server" placeholder="Nombre o identificación"
                           ></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:Button CssClass="btn btn-success" runat="server" Text="Buscar" />

                    </div>
                    <div class="col-lg-3">
                        <asp:LinkButton ID="LinkButton1" runat="server">Proveedor nuevo? Agregar</asp:LinkButton>
                    </div>
                </div>
                <div class="overflow-auto" style="height: 150px; width: 100%; "> 
                    
                    <table id="clientesTable" class="table-sm table-bordered table-hover" style="width: 100%">
                        <thead>
                            <tr class="tabla_encabezado"> 
                                <th scope="col">#</th>
                                <th scope="col">Identificacion</th>
                                <th scope="col">Nombre</th>
                                <th scope="col">Telefono</th>
                                <th scope="col">Ver</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">1</th>
                                <td>54687</td>
                                <td>Jorge González</td>
                                <td>88775566</td>
                                <td>
                                    <asp:Button Height="100%" CssClass="btn btn-link" runat="server" Text="Abrir detalle" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">2</th>
                                <td>54688</td>
                                <td>Julio Jaramillo</td>
                                <td>88775566</td>
                                <td>
                                    <asp:Button Height="100%" CssClass="btn btn-link" runat="server" Text="Abrir detalle" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">3</th>
                                <td>45688</td>
                                <td>Selena Gomez</td>
                                <td>88775566</td>
                                <td>
                                    <asp:Button Height="100%" CssClass="btn btn-link" runat="server" Text="Abrir detalle" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <br>
            <h5 class="font-weight-bold">Detalles de compra</h5>
            
            <%-- SECCION 3 --%>
            <%-- fila para agregar lineas --%>
             <div class="row table-sm" id="divFilaDetalles" ">
                    <div class="d-sm-table-cell" style="margin-left:2%">
                        <asp:DropDownList ID="productosTB" AutoPostBack="false" runat="server" CssClass="btn dropup btn-light"></asp:DropDownList>
                    </div>
                    <div class="d-sm-table-cell" style="margin-left:2%">
                        <asp:TextBox ID="cantidadTB" runat="server" type="number" CssClass="btn btn-light" />
                    </div>
                    <div class="d-sm-table-cell" style="margin-left:2%">
                        <asp:DropDownList ID="unidadTB" runat="server" CssClass="btn dropup btn-light"></asp:DropDownList>
                    </div>
                    <div class="d-sm-table-cell" style="margin-left:2%">
                        <asp:TextBox ID="precioUnidadTB" runat="server" type="number" CssClass="btn btn-light" />
                    </div> 
                    <div class="d-sm-table-cell" style="margin-left:2%">
                       <asp:Button ID="agregarDetalleBTN" runat="server" Text="Agregar linea"  CssClass="btn btn-primary" OnClick="agregarLineaClick"/>
                    </div>
                   <%--  <asp:Button Height="100%" CssClass="btn btn-link" ForeColor="red" runat="server" Text="Quitar línea" />--%>
                </div>
            <%-- fila label agregados --%>
             <div class="row" style="text-align:right; " >
                    <div class="col-10" style="margin-right:4%"></div>
                     <div class="col-1 text-left" style="margin-right:1%;">
                      <h6 class="card-title font-weight-bold">Agregados:</h6>
                    </div>
                      <asp:Label runat="server" id="labelC" CssClass="font-weight-bolder">0</asp:Label>
                </div>

            <%-- seccion de detalles --%>
            <div class="overflow-auto" style="height: 180px; width: 100%; border:1px solid rgba(208, 205, 205, 0.64)" >
                    <asp:Table id="tablaDetalles" runat="server" class="table-sm table-bordered table-hover" style="width: 100%">
                        <asp:TableHeaderRow CssClass="btn-light font-weight-bolder">
                            <asp:TableCell>Producto</asp:TableCell>
                            <asp:TableCell>Cantidad</asp:TableCell>
                            <asp:TableCell>Unidad</asp:TableCell>
                            <asp:TableCell>Precio unidad</asp:TableCell>
                            <asp:TableCell>Precio total</asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableHeaderRow>
                    </asp:Table> 
            </div>
            <br>
            <div class="align-content-center" style="text-align:center">
                <asp:Button runat="server" Text="Registrar compra" usesubmitbehavior="true" CssClass="btn btn-info" />
            </div>
            <br>
            <br>
    </div>
</asp:Content>
