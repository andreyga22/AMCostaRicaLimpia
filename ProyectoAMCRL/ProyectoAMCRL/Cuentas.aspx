<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Cuentas.aspx.cs" Inherits="ProyectoAMCRL.Cuentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <nav id="migajasNav" aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="PaginaPrincipal.aspx">Inicio</a></li>
            <li class="breadcrumb-item"><a href="#">Mantenimiento</a></li>
            <li class="breadcrumb-item active" aria-current="page">Cuentas</li>
        </ol>
    </nav>
    <div class="container">
        <form id="form1" runat="server">
             <div class="row justify-content-center">
                <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
            </div>

            <div class="row">
                <div id="divFuncionPagina" class="cell col-9" style="">
                    <h4 id="funcionPaginaLabel" class="font-weight-bold">Mantenimiento de cuentas</h4>
                </div>
                <div id="fecha" class="row col-3">
                    <h5 style="margin-right: 10%;">Fecha: </h5>
                    <h5 id="fechaLabel">4/5/19</h5>
                </div>
            </div>
            <br>
            <br>

            <%-- SECCION 1 --%>

            <h4>Cuentas existentes:</h4>

            <div class="overflow-auto" style="height: 250px; width: 100%; ">
                 <table class="table-sm table-bordered table-hover" style="width: 100%">
                        <thead>
                            <tr class="tabla_encabezado">
                                <th scope="col">Nickname usuario</th>
                                <th scope="col">Nombre</th>
                                <th scope="col">Rol</th>
                                <th scope="col">Estado</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>JG</td>
                                <td>Jorge González</td>
                                <td>EMPLEADO</td>
                                <td>
                                    <asp:Button ForeColor="blue" Height="100%" CssClass="btn btn-link" runat="server" Text="Habilitar" />
                                    /<asp:Button ForeColor="red" Height="100%" CssClass="btn btn-link" runat="server" Text="Deshabilitar" />
                                </td>
                            </tr>
                            <tr>
                                <td>JJ</td>
                                <td>Julio Jaramillo</td>
                                <td>ADMIN</td>
                                <td>
                                   <asp:Button ForeColor="blue" Height="100%" CssClass="btn btn-link" runat="server" Text="Habilitar" />
                                    /<asp:Button ForeColor="red" Height="100%" CssClass="btn btn-link" runat="server" Text="Deshabilitar" />
                                </td>
                            </tr>
                             <tr>
                                <td>CA</td>
                                <td>Carlos Angulo</td>
                                <td>EMPLEADO</td>
                                <td>
                                   <asp:Button ForeColor="blue" Height="100%" CssClass="btn btn-link" runat="server" Text="Habilitar" />
                                    /<asp:Button ForeColor="red" Height="100%" CssClass="btn btn-link" runat="server" Text="Deshabilitar" />
                                </td>
                            </tr>
                            <tr>
                                <td>SG</td>
                                <td>Selena Gomez</td>
                                <td>EMPLEADO</td>
                                <td>
                                  <asp:Button ForeColor="blue" Height="100%" CssClass="btn btn-link" runat="server" Text="Habilitar" />
                                    /<asp:Button ForeColor="red" Height="100%" CssClass="btn btn-link" runat="server" Text="Deshabilitar" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
            </div>
            <div class="row float-right">
                <asp:Button Height="100%" CssClass="btn btn-link" runat="server" Text="+ Añadir cuenta" />
            </div>
            <br>
            <br>
            <div class="align-content-center" style="text-align:center">
                <asp:Button runat="server" Text="Guardar cambios" CssClass="btn btn-info" />
            </div>
        </form>
    </div>
</asp:Content>
