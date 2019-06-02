﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Materiales.aspx.cs" Inherits="ProyectoAMCRL.Materiales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Administrar Materiales</li>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="sideNavBody" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <br />
        <div class="row">
            <h4>Administrar Materiales</h4>
        </div>
        <button type="button" class=" btn btn-link" id="LinkButton1" runat="server">Añadir nuevo</button>
        <div class="row">
            <div class="col-lg-3">
                <asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Nombre">
                </asp:TextBox>
            </div>
            <div class="col-lg-3">
                <asp:TextBox type="number" ID="precioKgTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Precio(Kg)">
                </asp:TextBox>
            </div>
            <asp:Button runat="server" CssClass="btn btn-info" ID="nuevoBTN" Text="Agregar" />
        </div>

        <asp:GridView ID="GridView1" CssClass="table table-hover table-sm" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="COD_MATERIAL" DataSourceID="SqlDataSource1" OnRowCreated="GridView1_RowCreated">

            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="COD_MATERIAL" HeaderText="CODIGO" InsertVisible="FALSE" ReadOnly="TRUE" SortExpression="COD_MATERIAL" />
                <asp:BoundField DataField="NOMBRE_MATERIAL" HeaderText="NOMBRE" SortExpression="NOMBRE_MATERIAL" />
                <asp:BoundField DataField="PRECIO_KILO" HeaderText="PRECIO KILO" SortExpression="PRECIO_KILO" />
            </Columns>

        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:AMCRLConnectionString %>"
            DeleteCommand="DELETE FROM [MATERIAL] WHERE [COD_MATERIAL] = @original_COD_MATERIAL"
            InsertCommand="INSERT INTO [MATERIAL] ([NOMBRE_MATERIAL], [PRECIO_KILO]) VALUES (@NOMBRE_MATERIAL, @PRECIO_KILO)" OldValuesParameterFormatString="original_{0}"
            SelectCommand="SELECT * FROM [MATERIAL]" UpdateCommand="UPDATE [MATERIAL] SET [NOMBRE_MATERIAL] = @NOMBRE_MATERIAL, [PRECIO_KILO] = @PRECIO_KILO WHERE [COD_MATERIAL] = @original_COD_MATERIAL">
            <DeleteParameters>
                <asp:Parameter Name="original_COD_MATERIAL" Type="Decimal" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="NOMBRE_MATERIAL" Type="String" />
                <asp:Parameter Name="PRECIO_KILO" Type="Decimal" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="NOMBRE_MATERIAL" Type="String" />
                <asp:Parameter Name="PRECIO_KILO" Type="Decimal" />
                <asp:Parameter Name="original_COD_MATERIAL" Type="Decimal" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <br />
        <div class="row justify-content-center">
        </div>

        <br />
        <br />


        <br />
        <br />
        <div />
    </div>
</asp:Content>
