<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroSociosUI.aspx.cs" Inherits="ProyectoAMCRL.RegistroSociosUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h3>Agregar nuevo cliente o proveedor</h3>
    <div class="row">
        <div class="col-sm-4"></div>
        <div  class="col-sm-4 sfg" >
            <div class="form-group">
                <label for="usr">Identificacion:</label>
                <input type="text" class="form-control" id="usr">
            </div>
            <div class="form-group">
                <label for="pwd">Nombre:</label>
                <input type="text" class="form-control" id="pwd">
            </div>
            <div class="form-group">
                <label for="ape1">Primer apellido:</label>
                <input type="text" class="form-control" id="ape1">
            </div>
            <div class="form-group">
                <label for="ape2">Segundo apellido:</label>
                <input type="text" class="form-control" id="ape2">
            </div>
            <div class="form-group">
                <label for="tel">Telefono:</label>
                <input type="number" class="form-control" id="tel">
            </div>
             <div class="form-group">
                <label for="dir">Direccion:</label>
                <input type="text" class="form-control" id="dir">
            </div>

            <asp:RadioButton runat="server" Text="Cliente" CssClass="radioL"/>
            <asp:RadioButton runat="server" Text="Proveedor"/>

            <div class="row">
                <asp:Button type="button" ID="Button1" runat="server" Text="Button" class="alert-success" />
            </div>   
        </div>
        <div class="col-sm-4"></div>
    </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
