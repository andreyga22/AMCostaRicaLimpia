<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="~/AdministrarBodegas.aspx.cs" Inherits="ProyectoAMCRL.BusquedaBodegas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Administrar Bodegas</li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <br />
        <div class="row">
            <h4>Administrar Bodegas</h4>
        </div>
        <br />
        <br />
        <br />
        <div class="row">
            <h5>Filtros</h5>
        </div>

        <div class="row">
            <div class="col-4">
                <div class="form-group">
                    <label for="palabraTb">Criterio</label>
                    <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código o socio"></asp:TextBox>
                </div>
            </div>

            <div class="col-3 offset-1">
                <div class="row">
                    <div class="form-group">
                        <label for="materialCb">Material</label>
                        <asp:CheckBox ID="materialCb" type="checkbox" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <div>
                            <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="materialDd" runat="server" AutoPostBack="True">
                                <asp:ListItem>Aluminio</asp:ListItem>
                                <asp:ListItem>Cobre</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-4">
                <div class="form-group">
                    <label for="txtUb">Ubicación</label>
                    <asp:TextBox type="text" ID="txtUb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Palmares"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />


        <br />
        <br />
        <table class="table table-bordered">
            <thead>
                <tr class="tabla_encabezado">
                    <th scope="col">#</th>
                    <th scope="col">Código Bodega</th>
                    <th scope="col">Nombre</th>
                    <th scope="col">Ubicación</th>
                    <th scope="col">Estado</th>
                    <th scope="col">Selección</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">1</th>
                    <td>54687</td>
                    <td>Bodega 01</td>
                    <td>Palmares</td>
                    <td>
                        <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="DropDownList2" runat="server" AutoPostBack="True">
                            <asp:ListItem>Activo</asp:ListItem>
                            <asp:ListItem>Inactivo</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <div class="form-group">
                            <asp:Button ID="btnVer1" type="submmit" runat="server" Text="Ver" class="btn btn-info" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <th scope="row">2</th>
                    <td>54688</td>
                    <td>Bodega 02</td>
                    <td>San Ramón</td>
                    <td>
                        <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="DropDownList3" runat="server" AutoPostBack="True">
                            <asp:ListItem>Activo</asp:ListItem>
                            <asp:ListItem>Inactivo</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <div class="form-group">
                            <asp:Button ID="btnVer2" type="submmit" runat="server" Text="Ver" class="btn btn-info" />
                        </div>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <th scope="row">3</th>
                    <td>54689</td>
                    <td>Bodega 03</td>
                    <td>Naranjo</td>
                    <td>
                        <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="DropDownList4" runat="server" AutoPostBack="True">
                            <asp:ListItem>Activo</asp:ListItem>
                            <asp:ListItem>Inactivo</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <div class="form-group">
                            <asp:Button ID="btnVer3" type="submmit" runat="server" Text="Ver" class="btn btn-info" />
                        </div>
                    </td>
                </tr>
            </tbody>


        </table>
        <div class="row justify-content-center">
            <asp:Button ID="btnAgregar" type="submmit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" />
        </div>

        <br />
        <br />

        <%--        </form>--%>
    </div>


</asp:Content>

