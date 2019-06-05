<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="~/AdministrarBodegas.aspx.cs" Inherits="ProyectoAMCRL.BusquedaBodegas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Administrar Bodegas</li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        <div class="row">
            <div class="form-group">
                <h4>Administrar Bodegas</h4>
            </div>
        </div>
        <div class="row">
            <div class="col-4">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código o socio" OnTextChanged="palabraTb_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-5">
                <asp:Button ID="btnAgregar" runat="server" Text="Nuevo" class="btn btn-info" />
            </div>
        </div>
        <br />

        <div class="row justify-content-center">
            <div class="col-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridBodegas" runat="server" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gridBodegas_PageIndexChanging" OnSorting="gridBodegas_Sorting" PageSize="5" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gridBodegas_SelectedIndexChanged">
                            <HeaderStyle BackColor="#94BD8B" />
                            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NumericFirstLast" PageButtonCount="2" />
                            <PagerStyle HorizontalAlign="Right" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</asp:Content>

