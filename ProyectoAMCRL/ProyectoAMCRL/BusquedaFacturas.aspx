<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaFacturas.aspx.cs" Inherits="ProyectoAMCRL.BusquedaFacturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Principal.aspx">Principal</a></li>
                <li class="breadcrumb-item active" aria-current="page">Busqueda Facturas</li>
            </ol>
        </nav>

        <form id="form1" runat="server">
            <br />
            <div class="row justify-content-center">
                <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
            </div>
            <br />
            <div class="row">
                <h4>Busqueda de facturas</h4>
            </div>
            <br />
            <br />
            <br />



            <div class="row">

                <div class="col-lg">
                    <div class="form-group">
                        <label for="palabraTb">Palabra Clave</label>
                        <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" ToolTip="Código o socio"></asp:TextBox>
                    </div>
                </div>

                <div class="col-lg">
                    <div class="form-group">
                        <label for="fechasCb">Fecha</label>
                        <asp:CheckBox ID="fechasCb" type="checkbox" runat="server" />
                    </div>
                </div>

                <div class="col-lg">
                    <div class="form-group">
                        <label for="materialCb">Material</label>
                        <asp:CheckBox ID="materialCb" type="checkbox" runat="server" />
                    </div>
                </div>

                <div class="col-lg">
                    <div class="form-group">
                        <label for="montoCb">Monto</label>
                        <asp:CheckBox ID="montoCb" type="checkbox" runat="server" />
                    </div>
                </div>


            </div>


            <asp:GridView ID="facturasGrid" runat="server" OnLoad="facturasGrid_Load"></asp:GridView>





            <div class="row justify-content-center">
                <div class="form-group">
                    <asp:Button ID="btnGuardar" type="submmit" runat="server" Text="Guardar" class="btn btn-outline-primary btn-lg" />
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />


            <asp:GridView ID="ajustesGv" runat="server">
                <Columns>
                    <asp:BoundField DataField="Aluminio" HeaderText="Material" />
                    <asp:BoundField HeaderText="Peso" />
                    <asp:BoundField HeaderText="Unidad Medida" />
                    <asp:BoundField HeaderText="Bodega" />
                    <asp:BoundField HeaderText="Acción" />
                </Columns>
            </asp:GridView>



            <br />
            <br />
            <br />
        </form>

    </div>
</asp:Content>















