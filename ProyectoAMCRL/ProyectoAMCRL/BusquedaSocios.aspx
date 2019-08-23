<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaSocios.aspx.cs" Inherits="ProyectoAMCRL.BusquedaSocios" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-3.4.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Busqueda Socios</li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <h4 class="font-weight-bold">Búsqueda de socios</h4>
        </div>
        <br />

        <br>


       <div class="row justify-content-center">
           <div class="col-2">
                <asp:Button ID="NuevoBtn" type="submit" runat="server" class="btn btn-info" Text="Nuevo" OnClick="NuevoBtn_Click" />
            </div>
            <div class="col-3 offset-7">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox type="text" ID="txtPalabra" class="form-control" runat="server" TextMode="SingleLine" placeholder="Buscar" AutoPostBack="true" OnKeyDown="txt_Item_Number_KeyDown" OnTextChanged="txtPalabra_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <%-- SECCION 2 --%>
        <div class="row justify-content-center">
            <div class="col-12 table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridSocios" runat="server" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gridSocios_PageIndexChanging" OnSorting="gridSocios_Sorting" OnSelectedIndexChanged="gridSocios_SelectedIndexChanged" OnKeyDown="" OnRowDataBound="gridSocios_RowDataBound" PageSize="5">
                            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NextPreviousFirstLast" PageButtonCount="4" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle"/>                             
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                            <PagerStyle HorizontalAlign="Right" />
                        </asp:GridView>
                        <asp:Button ID="printbtn" runat="server" OnClick="printbtn_Click" Text="Generar PDF" />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        </div>
</asp:Content>
