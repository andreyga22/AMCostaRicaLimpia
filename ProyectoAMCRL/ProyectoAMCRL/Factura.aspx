<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="ProyectoAMCRL.Factura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="sideNavBody" runat="server">
</asp:Content>--%>
<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color:red">
       <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
        </div>
    <div class="container">
            <div class="row">
                <div id="divFuncionPagina" class="cell col-9" style="">
                     <asp:Label  runat="server" ID="lblTitulo" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                </div>
                <div id="fecha" class="row col-3">
                    <h5 style="margin-right: 10%;">Fecha: </h5>
                    <h5> <asp:Label  runat="server" ID="lblFecha"></asp:Label></h5>
                </div>
            </div>
            <br>
             <br>
            <label class="font-weight-bold">AM Costa Rica Limpia</label>
            <br>
            <div class="row">
                <div class="col-lg-6">
                    <label class="font-weight-bold campoIzq" for="telLabel">Teléfono: </label>
                    <asp:Label  runat="server" ID="lblTel"></asp:Label>
                    <br>
                    <label class="font-weight-bold campoIzq" for="telLabel">Bodega: </label>
                    <asp:Label  runat="server" ID="lblBodega"></asp:Label>
                    <br>
                    <label class="font-weight-bold campoIzq" for="telLabel">Dirección: </label>
                      <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                </div>
                <div class="col-lg-6">
                    <label class="font-weight-bold campoIzq" for="telLabel">Factura #: </label>
                   <asp:Label ID="lblFactura" runat="server"></asp:Label>
                    <br>
                    <label class="font-weight-bold campoIzq" for="telLabel">Moneda: </label>
                    <asp:Label ID="lblMoneda" runat="server"></asp:Label>
                    <%-- <asp:Label class="campo" runat="server" ID="Label3">COL</asp:Label>--%>
                </div>
            </div>
             <br>
            <h5 class="font-weight-bold">Datos del socio</h5>
            <%-- SECCION 2 --%>
            <div class="container">
                <div class="row">
                    <div class="col-lg-6">
                        <asp:Label ID="lblNomb" runat="server" Text="Nombre: " Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblNombre" runat="server"></asp:Label>
                    </div>
                   <div class="col-lg-6">
                        <asp:Label ID="Direc" runat="server" Text="Dirección: " Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblDireccio" runat="server"></asp:Label>
                    </div>
             
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <asp:Label ID="Rol" runat="server" Text="Rol: " Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblRol" runat="server"></asp:Label>
                    </div>
                   <div class="col-lg-6">
                        <asp:Label ID="est" runat="server" Text="Estado: " Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblEstado" runat="server"></asp:Label>
                    </div>
             
                </div>
             
                </div>
            <br>
            <h5 class="font-weight-bold">Detalles de compra</h5>
        <br>
     <asp:GridView ID="gridDetalle" class="table table-hover table-bordered table-striped" runat="server" AllowPaging="True" AllowSorting="True" PageSize="5">
            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NumericFirstLast" PageButtonCount="2" />
         <HeaderStyle BackColor="#94BD8B" />
            </asp:GridView>
          <asp:Label ID="lbl" runat="server" Text="Monto Total: " Font-Bold="True"></asp:Label>
         <asp:Label ID="lblMontoTotal" runat="server"></asp:Label>
            <%-- SECCION 3 --%>
            <%-- fila para agregar lineas --%>
             <%--<div class="row table-sm" id="divFilaDetalles" ">
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
                    </div>--%>
                   <%--  <asp:Button Height="100%" CssClass="btn btn-link" ForeColor="red" runat="server" Text="Quitar línea" />--%>
                <%--</div>--%>
            <%-- fila label agregados --%>
            <%-- <div class="row" style="text-align:right; " >
                    <div class="col-10" style="margin-right:4%"></div>
                     <div class="col-1 text-left" style="margin-right:1%;">
                      <h6 class="card-title font-weight-bold">Agregados:</h6>
                    </div>
                      <asp:Label runat="server" id="labelC" CssClass="font-weight-bolder">0</asp:Label>
                </div>--%>

            <%-- seccion de detalles --%>
        <%--    <div class="overflow-auto" style="height: 180px; width: 100%; border:1px solid rgba(208, 205, 205, 0.64)" >
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
            </div>--%>
            <%--<br>--%>
            <%--<div class="align-content-center" style="text-align:center">
                <asp:Button runat="server" Text="Registrar compra" usesubmitbehavior="true" CssClass="btn btn-info" />
            </div>--%>
    </div>
</asp:Content>
