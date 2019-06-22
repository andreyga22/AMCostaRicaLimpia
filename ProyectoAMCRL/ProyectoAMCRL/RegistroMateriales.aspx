<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroMateriales.aspx.cs" Inherits="ProyectoAMCRL.RegistroMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarMateriales.aspx" style="color: dodgerblue">Materiales</a></li>
    <li class="breadcrumb-item">
        <asp:Label ID="breadLabel" runat="server">Nuevo</asp:Label></li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="row" style="float: right; margin-right: 0.5%; margin-top: 0%">
        <label class="h6">Bodega:</label>
        <asp:Label runat="server" CssClass="h6" Text=" B01"></asp:Label>
    </div>
    <asp:Label runat="server" ID="labelAccion" CssClass="h5">Nuevo material</asp:Label>
    <br>
    <br>
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-5" style="margin-left: 0%;">
            <asp:HiddenField runat="server" ID="escondidillo" Value="" />
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
            <div class="row justify-content-start" style="">
                <asp:Button runat="server" CssClass="btn btn-info" ID="btnGuardarActualizar" Text="Guardar" OnClick="btnGuardarActualizar_Click" />
            </div>
            <br>
            <a href="DetalleAjuste.aspx" class="btn btn-link" style="float: right">Registrar Stock</a>
        </div>

    </div>
    <%--<div class="col">
        </div>
        <div class="col-6 card text-white bg-success mb-3">
            <div class="row">
                <div style="width: 50%; height: 100%; text-align: center" class="justify-content-center">
                    <br>
                    <asp:Label ID="accionMaterialLabel" Text="Material más vendido" runat="server" class="h4" />
                    <br>
                    <asp:Label ID="materialLabel" Text="Aluminio" runat="server" class="h1" />
                    <br>
                    <div class="row justify-content-center" style="height: 60px; width: 100%; margin-left: 1.5%;">
                        <asp:Label ID="cantidadLabel" Text="24" runat="server" class="h1 align-bottom" />
                        <h6 style="align-self: flex-end">Kg</h6>
                    </div>

                </div>
                <div class="col-sm-3" style="padding-left: 10%; padding-top: 4%">
                    <i class=' fas fa-award' style='font-size: 150px; color: black'></i>
                </div>
            </div>
        </div>
    </div>--%>
    <%-- <br>
    <br>--%>

    <%-- SECCION PARA REGISTRAR EN UN STOCK ESPECIFICO --%>
    <%-- <div id="divStockNuevo" class="rounded" style="background-color: lightgray; padding-bottom: 20px; padding-top: 10px">
        <div   class="row" style="width: 100%; margin-left: 2%">
            <asp:Label Text="Actualización de inventarios" runat="server" CssClass="h5 font-weight-bolder" />
        </div>
        <br>

        <div class="row" style="margin-left: 0%">

            <div class="col-lg-3">
                <asp:Label Text="Bodega a actualizar" runat="server" CssClass="font-weight-bolder" />
                <br>
                <asp:DropDownList ID="dropDownBodegas" runat="server" CssClass="btn dropup btn-light" OnSelectedIndexChanged="dropDownBodegas_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="col-lg-3">
                <asp:Label Text="Cantidad a registrar" runat="server" CssClass="font-weight-bolder" />
                <br>
                <asp:TextBox type="number" ID="cantidadTB" class="form-control" runat="server" TextMode="SingleLine" placeholder="Ejm: 100"></asp:TextBox>
            </div>

            <div class="col-lg-2" >
                <asp:Label Text="Unidad de peso" runat="server" CssClass="font-weight-bolder" />
                <br>
                <asp:DropDownList ID="dropDownUnidades" runat="server" CssClass="btn dropup btn-light"></asp:DropDownList>
            </div>
            <div class="col-lg-3" style="height:100%">
                <span style="width:100%">
               <i class="fas fa-dolly" style="font-size: 115px; margin-left:20%"></i>
                </span>
                </div>
         
        </div>

        <div class="row justify-content-start" style="margin-left: 45%">
            <asp:Button ID="btnEnlazarStock" runat="server" Text="Guardar" CssClass="btn btn-info" />
        </div>

    </div>--%>
</asp:Content>
