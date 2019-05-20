<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Asociar_Socio.aspx.cs" Inherits="ProyectoAMCRL.Asociar_Socio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="PaginaPrincipal.aspx">Principal</a></li>
            <li class="breadcrumb-item"><a href="expediente.aspx">Expediente</a></li>
            <li class="breadcrumb-item"><a href="ListaConsultas.aspx">Lista Consultas</a></li>
            <li class="breadcrumb-item"><a href="Consulta.aspx">Consulta</a></li>
            <li class="breadcrumb-item active" aria-current="page">Ficha Paramédico</li>
        </ol>
    </nav>

        <form id="form1" runat="server">
            <br />
            <div class="row justify-content-center">
            <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
            </div>
            <br />
            <br />
            <div class="row">
                <h4>Asociar</h4>
            </div>
            <br />
            <br />
      
        	<div class="row">
            	<div class="col-lg-6">
                	<label class="font-weight-bold campo_izquierda" for="idLabel">Identificacion: </label>
                	<asp:Label class="datosLbl" runat="server" ID="idDatoLabel">27885522654</asp:Label>
                	<br />
                	<label class="font-weight-bold campo_izquierda" for="NomLabel">Nombre y Apellidos: </label>
                	<asp:Label class="datosLbl" runat="server" ID="NomDatLabel">Armando Fernandez Reyes</asp:Label>
                    <br />
                    <label class="font-weight-bold campo_izquierda" for="telLabel">Numero Telefonico: </label>
                	<asp:Label class="datosLbl" runat="server" ID="telDatoLabel">845675</asp:Label>
                	<br />
                	<label class="font-weight-bold campo_izquierda" for="DirLabel">Direccion: </label>
                	<asp:Label class="datosLbl" runat="server" ID="dirDatoLabel">Naranjo, Alajuela</asp:Label>
                    <br />
                    <br />
                    <br />
                    <label class="font-weight-bold campo_izquierda" for="socIdLabel">Identificacion del asociado: </label>
                    <asp:TextBox class="datosLbl" runat="server" ID="socIdTB"></asp:TextBox>
                    <br />
                    <br />
                    <br>
                    <asp:GridView class="table table-bordered" ID="GridView1" runat="server" Height="241px" >
                    </asp:GridView>
                    <br>
                    
            	</div>
        	</div>

           </form>
    </div>
</asp:Content>

