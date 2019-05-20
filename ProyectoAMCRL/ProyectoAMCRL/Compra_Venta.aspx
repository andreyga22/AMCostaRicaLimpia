<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Compra_Venta.aspx.cs" Inherits="ProyectoAMCRL.Compra_Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
    <script src="jquery-3.4.0.min.js"></script>
    <script>
        var nomIdeSocioCambiado = false;
    </script>
    <script>
        function quitarTexto() {

            if (!nomIdeSocioCambiado) {
                $('#nomIdeSocio').val("");
                nomIdeSocioCambiado = true;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <nav id="migajasNav" aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="PaginaPrincipal.aspx">Inicio</a></li>
            <li class="breadcrumb-item"><a href="#">Compras</a></li>
            <li class="breadcrumb-item active" aria-current="page">Registrar nueva compra</li>
        </ol>
    </nav>

    <div class="container">
        <form runat="server">
            <div class="row justify-content-center">
                <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
            </div>

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
            <br>
            <%-- SECCION 2 --%>
            <div class="container">
                <div class="row">
                    <div class="col-3">
                        <asp:TextBox type="text"  class="form-control" Height="90%" ID="nomIdeSocio" runat="server" placeholder="Nombre o identificación"
                           ></asp:TextBox>
                    </div>
                    <div class="col-6">
                        <asp:Button Height="90%" CssClass="btn btn-success" runat="server" Text="Buscar" />

                    </div>
                    <div class="col-3">
                        <asp:LinkButton ID="LinkButton1" runat="server">Proveedor nuevo? Agregar</asp:LinkButton>
                    </div>
                </div>
                <div class="overflow-auto" style="height: 180px; width: 100%; ">
                    <table class="table-sm table-bordered table-hover" style="width: 100%">
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
             <br>
            <h5 class="font-weight-bold">Detalles de compra</h5>
            <br>
            <%-- SECCION 3 --%>
            <div class="overflow-auto" style="height: 180px; width: 100%;">
                <table class="table-sm" style="width: 100%">
                    <thead>
                        <tr>
                            <th scope="col">Producto</th>
                            <th scope="col">Cantidad</th>
                            <th scope="col">Unidad</th>
                            <th scope="col">Precio unidad</th>
                            <th scope="col">Precio total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <%-- PRODUCTOS --%>
                            <td>
                                <asp:DropDownList ID="productos" runat="server" CssClass="btn dropup btn-light"></asp:DropDownList>
                            </td>
                            <%-- CANTIDAD --%>
                            <td>
                                <asp:TextBox ID="cantidadM" runat="server" type="number" CssClass="btn btn-light"/>

                            </td>
                            <%-- UNIDAD --%>
                            <td>
                                <asp:DropDownList ID="unidades" runat="server" CssClass="btn dropup btn-light"></asp:DropDownList>
                            </td>
                            <%-- PRECIO UNIDAD --%>
                            <td>
                                <asp:TextBox ID="precioUnidades" runat="server" type="number"  CssClass="btn btn-light"/>
                            </td>
                            <%-- TOTAL  --%>
                            <td>
                                <asp:TextBox ID="total" runat="server" type="number" CssClass="btn btn-light"/>
                            </td>
                            <td>
                                <asp:Button Height="100%" CssClass="btn btn-link" ForeColor="red" runat="server" Text="Quitar línea" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="align-content-center" style="text-align:center">
                <asp:Button runat="server" Text="Agregar linea" CssClass="btn btn-info" />
            </div>
            <br>
            <br>


            <%--    <asp:Button runat="server" Text="Guardar" OnClick="Guardar_click"/>
            <asp:Label runat="server" ID="la"></asp:Label>--%>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
