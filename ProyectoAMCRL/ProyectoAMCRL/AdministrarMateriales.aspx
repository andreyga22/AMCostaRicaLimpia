<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="AdministrarMateriales.aspx.cs" Inherits="ProyectoAMCRL.Materiales" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active"><a href="AdministrarMateriales.aspx">Materiales</a></li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>

    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        <div class="row">
            <h4>Administrar Materiales</h4>
        </div>
        <br />
        <div class="row justify-content-center">
            <div class="col-2">
                <asp:Button ID="btnAgregar" runat="server" Text="Nuevo" class="btn btn-info" OnClick="btnAgregar_Click" />
            </div>
            <div class="offset-7 col-3">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Buscar" OnTextChanged="palabraTb_TextChanged" AutoPostBack="true" OnKeyDown="txt_Item_Number_KeyDown"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>
        <br />
        <div class="row justify-content-center">
            <div class="col-12 table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridMateriales" runat="server" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gridMateriales_PageIndexChanging" OnSorting="gridMateriales_Sorting" PageSize="5" OnSelectedIndexChanged="gridMateriales_SelectedIndexChanged" OnKeyDown="" OnRowDataBound="gridMateriales_RowDataBound">
                            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NextPreviousFirstLast" PageButtonCount="4" />
                            <PagerStyle HorizontalAlign="Right" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</asp:Content>
