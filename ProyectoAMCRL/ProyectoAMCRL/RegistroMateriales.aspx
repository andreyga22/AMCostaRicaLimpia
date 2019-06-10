<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroMateriales.aspx.cs" Inherits="ProyectoAMCRL.RegistroMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarMateriales.aspx">Materiales</a></li>
    <li class="breadcrumb-item"><asp:Label id="breadLabel" runat="server">Nuevo</asp:Label></li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
     <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <br>
    <div class="row">
        <div class="col-4" style="margin-left: 2%">
            <asp:Label runat="server" ID="labelAccion" CssClass="h5">Nuevo material</asp:Label>
            <br>
            <br>
            <asp:HiddenField runat="server" ID="escondidillo" Value=""/>
            <div class="row">
                <asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Nombre">
                </asp:TextBox>
            </div>
            <br>
            <div class="row">
                <asp:TextBox type="number" ID="precioKgTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Precio(Kg)">
                </asp:TextBox>
            </div>
            <br>
            <div class="row justify-content-end" style="">
                <asp:Button runat="server" CssClass="btn btn-info" ID="btnGuardarActualizar" Text="Guardar" OnClick="btnGuardarActualizar_Click" />
            </div>

        </div>
        <div class="col">
        </div>
        <div class="col-6 card text-white bg-success mb-3">
            <div class="row">
                <div style="width: 50%; height: 100%; text-align: center" class="justify-content-center">
                    <br>
                   <asp:Label ID="accionMaterialLabel" Text="Material más vendido" runat="server" class="h4" />
                    <br>
                    <asp:Label ID="materialLabel" Text="Aluminio" runat="server" class="h1" />
                    <br>
                    <div class="row justify-content-center" style="height:60px; width:100%; margin-left:1.5%;">
                            <asp:Label id="cantidadLabel" Text="24" runat="server" class="h1 align-bottom"/>
                          <h6 style="align-self:flex-end">Kg</h6>
                    </div>
                  
                </div>
                <div class="col-sm-3" style="padding-left: 10%; padding-top: 4%">
                    <i class=' fas fa-award' style='font-size: 150px; color: black'></i>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
