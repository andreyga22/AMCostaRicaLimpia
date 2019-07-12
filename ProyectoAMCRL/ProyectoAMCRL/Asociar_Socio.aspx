<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Asociar_Socio.aspx.cs" Inherits="ProyectoAMCRL.Asociar_Socio" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Asociar Socios</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">


    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>

    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <h4 class="font-weight-bold">Asociar Socios</h4>
        </div>
        <br />
        <br>

        <div class="col-10">
            <div class="row">
                <div class="col-2">
                    <h6 class="font-weight-bold">Cédula:</h6>
                </div>
                <div class="col-4">
                    <asp:Label ID="idLbl" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-2">
                    <h6 class="font-weight-bold">Nombre:</h6>
                </div>
                <div class="col-4">
                    <asp:Label ID="nombreLbl" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-2">
                    <h6 class="font-weight-bold">Rol:</h6>
                </div>
                <div class="col-4">
                    <asp:Label ID="rolLbl" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <br />
        </div>
        <br />
        <div class="form-row">
            <div class="col-4">
                <h5 class="font-weight-bold">Lista de Socios</h5>
            </div>
            
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:TextBox type="text" ID="txtPalabra" class="form-control" runat="server" TextMode="SingleLine" placeholder="Buscar" AutoPostBack="true" OnKeyDown="txt_Item_Number_KeyDown" OnTextChanged="txtPalabra_TextChanged"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <div class="col-4">
                <h5 class="font-weight-bold">Asociados</h5>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-5">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridSocios" runat="server" AllowSorting="True" OnSorting="gridSocios_Sorting" OnKeyDown="" OnRowDataBound="gridSocios_RowDataBound" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gridSocios_SelectedIndexChanged">
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="col-5 offset-1">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridAsociados" runat="server" AllowSorting="True" OnSorting="gridAsociados_Sorting" OnKeyDown="" OnRowDataBound="gridAsociados_RowDataBound" OnSelectedIndexChanged="gridAsociados_SelectedIndexChanged" AutoGenerateSelectButton="True">
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />

                        </asp:GridView>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</asp:Content>


