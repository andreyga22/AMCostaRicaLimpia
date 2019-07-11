<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Asociar_Socio.aspx.cs" Inherits="ProyectoAMCRL.Asociar_Socio" EnableEventValidation="false" %>



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

        <div class="col-2">
            <h6 class="font-weight-bold">Cédula:</h6>
            <br />
            <h6 class="font-weight-bold">Nombre:</h6>
            <br />
            <h6 class="font-weight-bold">Rol:</h6>
            <br />
        </div>

        <select class="mdb-select md-form" multiple searchable="Search here..">
  <option value="" disabled selected>Choose your country</option>
  <option value="1">USA</option>
  <option value="2">Germany</option>
  <option value="3">France</option>
  <option value="3">Poland</option>
  <option value="3">Japan</option>
</select>
<button class="btn-save btn btn-primary btn-sm">Save</button>

        </div>
</asp:Content>


