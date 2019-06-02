<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Materiales.aspx.cs" Inherits="ProyectoAMCRL.Materiales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="Principal.aspx">Principal</a></li>
            <li class="breadcrumb-item active" aria-current="page">Administrar Materiales</li>
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
                <h4>Administrar Materiales</h4>
            </div>
            <br />
            <br />
            <br />
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Código Material</th>
                        <th scope="col">Nombre</th>
                        <th scope="col">Precio Kilo</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>Cod 01</td>
                        <td>Aluminio</td>
                        <td>1000</td>
                    </tr>
                    <tr>
                        <th scope="row">2</th>
                        <td>Cod 02</td>
                        <td>Cobre</td>
                        <td>1500</td>

                    </tr>
                    <tr>
                        <th scope="row">3</th>
                        <td>Cod 03</td>
                        <td>Vibranium</td>
                        <td>10000</td>
                    </tr>
                </tbody>


            </table>
            <div class="row justify-content-center">
                <asp:Button ID="btnAgregar" type="submmit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" />
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
