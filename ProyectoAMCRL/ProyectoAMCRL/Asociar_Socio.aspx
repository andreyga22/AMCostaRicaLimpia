﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Asociar_Socio.aspx.cs" Inherits="ProyectoAMCRL.Asociar_Socio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Asociaciones</li>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <h4>Asociar</h4>
        </div>
        <br />
        <br />

        <div class="row">
            <label class="font-weight-bold campo_izquierda" for="idLabel">Identificacion: </label>
            <asp:Label class="datosLbl" runat="server" ID="idDatoLabel">27885522654</asp:Label>
        </div>
        <div class="row">
            <label class="font-weight-bold campo_izquierda" for="NomLabel">Nombre y Apellidos: </label>
            <asp:Label class="datosLbl" runat="server" ID="NomDatLabel">Armando Fernandez Reyes</asp:Label>
        </div>
        <div class="row">
            <label class="font-weight-bold campo_izquierda" for="telLabel">Numero Telefonico: </label>
            <asp:Label class="datosLbl" runat="server" ID="telDatoLabel">845675</asp:Label>
        </div>
        <div class="row">
            <label class="font-weight-bold campo_izquierda" for="DirLabel">Direccion: </label>
            <asp:Label class="datosLbl" runat="server" ID="dirDatoLabel">Naranjo, Alajuela</asp:Label>
        </div>

        <br />
        <br />
        <br />
        <div class="row">
            <label class="font-weight-bold campo_izquierda" for="socIdLabel">Identificacion del asociado: </label>
            <asp:TextBox runat="server" ID="socIdTB" type="text" class="form-control" placeholder="Código o Nombre"></asp:TextBox>
        </div>
        <br />
        <br />
        <div class="row-12">
            <asp:GridView class="table table-bordered" ID="GridView1" runat="server" Height="241px">
            </asp:GridView>
        </div>
        <br />
        <br />
        <br />
    </div>
</asp:Content>

