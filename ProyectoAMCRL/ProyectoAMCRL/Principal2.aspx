<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Principal2.aspx.cs" Inherits="ProyectoAMCRL.Principal2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="#">Prueba</a></li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <h4 class="font-weight-bold">Principal 2</h4>

    <%-- SECCION 2 --%>
    <div class="overflow-auto" style="height: 180px; width: 100%; border: 1px solid rgba(208, 205, 205, 0.64)">
        <asp:Table ID="tablaSocios" runat="server" class="table-sm table-bordered table-hover" Style="width: 100%">
            <asp:TableHeaderRow CssClass="btn-light font-weight-bolder">
                <asp:TableCell>ID</asp:TableCell>
                <asp:TableCell>Nombre</asp:TableCell>
                <asp:TableCell>Correo</asp:TableCell>
                <asp:TableCell>Telefono personal</asp:TableCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
    <br />

    <div class="row">
        <div class="col-7">
            <h6 class="font-weight-bolder">Volumen de ventas</h6>
            <asp:Chart ID="Chart1" runat="server" DataSourceID="ComprasDataSource" Height="320px" Width="600px" Palette="EarthTones">
                <Series>
                    <asp:Series Name="Series1" ChartType="Line" XValueMember="Fecha_Compra" YValueMembers="MONTO_TOTAL_C"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
                <BorderSkin BackSecondaryColor="64, 0, 0" BorderColor="DimGray" />
            </asp:Chart>
            <br>
            <asp:SqlDataSource ID="ComprasDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:AMCRLConnectionString %>" SelectCommand="SELECT [Fecha_Compra], [MONTO_TOTAL_C] FROM [COMPRA] ORDER BY [Fecha_Compra] DESC, [MONTO_TOTAL_C] DESC"></asp:SqlDataSource>
            <asp:SqlDataSource ID="ComprasVentasDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:AMCRLConnectionString %>" SelectCommand="SELECT [ID_BODEGA], [Fecha_Venta], [MONTO_TOTAL_V] FROM [VENTA]"></asp:SqlDataSource>
        </div>
        <br />
        <div class="col-5">
            <h6 class="font-weight-bolder">Estado stock</h6>
            <asp:Chart ID="Chart2" runat="server" DataSourceID="SqlDataSource1" Height="320px" Width="325px">
                <Series>
                    <asp:Series ChartType="Pie" Name="Series1" XValueMember="NOMBRE_MATERIAL" YValueMembers="KILOS_STOCK">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AMCRLConnectionString %>" SelectCommand="SELECT s.ID_BODEGA, m.NOMBRE_MATERIAL, s.KILOS_STOCK FROM STOCK s inner join MATERIAL m on (s.COD_MATERIAL = m.COD_MATERIAL);"></asp:SqlDataSource>
            <br />
        </div>
    </div>

</asp:Content>

