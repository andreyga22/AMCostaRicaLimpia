<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="GestionDeInventario.aspx.cs" Inherits="ProyectoAMCRL.GestionDeInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">

    <li class="breadcrumb-item active">Gestion de inventario</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
     <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        <%--<div class="row">--%>
            <%--<div class="form-group">--%>
                <h4>Gestion de Inventario</h4>
            <%--</div>--%>
        <%--</div>--%><br />
        <div class="row justify-content-center">
            <div class="col-2">
                <%--<asp:Button ID="btnAgregar" runat="server" Text="Nuevo" class="btn btn-info" OnKeyDown="" OnClick="btnAgregar_Click" />--%>
                <asp:DropDownList ID="bodDD" runat="server" OnSelectedIndexChanged="bodDD_SelectedIndexChanged" Width="236px" AutoPostBack="True" ViewStateMode="Enabled">
                </asp:DropDownList>
            </div>
            <div class="offset-7 col-3">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox type="text" ID="txtPalabra" class="form-control" runat="server" TextMode="SingleLine" placeholder="Buscar" AutoPostBack="true" OnKeyDown="txt_Item_Number_KeyDown" OnTextChanged="txtPalabra_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                    
                </asp:UpdatePanel>
            </div>
        </div>
        <br />

        <div class="row justify-content-center">
            <div class="col-12 table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridInventario" runat="server" AllowSorting="True" AllowPaging="True"  PageSize="5" OnPageIndexChanging="gridInventario_PageIndexChanging" OnSorting="gridInventario_Sorting" OnSelectedIndexChanged="gridInventario_SelectedIndexChanged" OnRowDataBound="gridInventario_RowDataBound" OnKeyDown="">
                            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NextPreviousFirstLast" PageButtonCount="2" />
                            <PagerStyle HorizontalAlign="Right" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle"/>
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</asp:Content>
