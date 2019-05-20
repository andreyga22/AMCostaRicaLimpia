<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Monedas.aspx.cs" Inherits="ProyectoAMCRL.Monedas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Principal.aspx">Principal</a></li>
                <li class="breadcrumb-item active" aria-current="page">Administrar Monedas</li>
            </ol>
        </nav>
      <div class="container">
     <form id="form1" runat="server">
            <br />
            <div class="row justify-content-center">
                <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
            </div>
            <br />
            <br />
            <div class="row">
                <h4>Administrar Monedas</h4>
            </div>
            <br />
            <br />
            <br />
          <table class="table table-bordered">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Código Moneda</th>
                        <th scope="col">Detalle</th>
                        <th scope="col">Equivalencia colón</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>USD</td>
                        <td>Dólar</td>
                        <td>600</td>
                    </tr>
                    <tr>
                        <th scope="row">2</th>
                        <td>CRC</td>
                        <td>Colón</td>
                        <td>1</td>
                        
                    </tr>
                    <tr>
                        <th scope="row">3</th>
                        <td>EUR</td>
                        <td>Euro</td>
                        <td>700</td>
                        </tr>
                </tbody>

                   
            </table>
                  <div class="form-group">
                    <asp:Button ID="btnAgregar" type="submmit" runat="server" Text="Agregar" class="btn btn-outline-primary btn-lg" />
                </div>

            <br />
            <br />
         

            <br />
            <br />

  </form>
    <div />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
