<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Ajustes.aspx.cs" Inherits="ProyectoAMCRL.Ajustes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Ajustes de inventario</li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center" style="background-color: red">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <%--<br />
            <div class="row">
                <h4>Ajustes de Inventario</h4>
            </div>
            <br />--%>

        <%-- SECCION 1 --%>
        <div class="row">
            <h5>Nuevo ajuste</h5>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-3">
                <div class="form-group">
                    <label for="materialDd">Material</label>
                    <div>
                        <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="materialDd" runat="server" AutoPostBack="True">
                            <asp:ListItem>Aluminio</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-lg-2">
                <div class="form-group">
                    <label for="pesoTb">Peso</label>
                    <asp:TextBox type="text" ID="pesoTb" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-lg-2">
                <div class="form-group">
                    <label for="unidadMDd">Medida</label>
                    <div>
                        <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="unidadMDd" runat="server" AutoPostBack="True">
                            <asp:ListItem>KG</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-lg-3">
                <div class="form-group">
                    <label for="bodegaDd">Bodega</label>
                    <div>
                        <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="bodegaDd" runat="server" AutoPostBack="True">
                            <asp:ListItem>Bodega001</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>


            <div class="col-lg-2">
                <div class="form-group">
                    <label for="aumentaRb">Acción</label>
                    <asp:RadioButtonList type="radio" ID="aumentaRb" runat="server">
                        <asp:ListItem>Aumentar</asp:ListItem>
                        <asp:ListItem>Reducir</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

        </div>

        <div class="col-lg">
            <div class="form-group">
                <label for="razonTb">Razón de ajuste</label>
                <asp:TextBox type="text" ID="razonTb" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

        <div class="row justify-content-center">
            <asp:Button ID="btnGuardar" type="submit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" />
        </div>
        <br />
        <br />
        <br />


        <table class="table table-bordered ">
            <thead class="tabla_encabezado">
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Material</th>
                    <th scope="col">Peso</th>
                    <th scope="col">Unidad de Medida</th>
                    <th scope="col">Bodega</th>
                    <th scope="col">Acción</th>
                    <th scope="col">Fecha</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">1</th>
                    <td>Aluminio</td>
                    <td>20</td>
                    <td>KG</td>
                    <td>Bodega001</td>
                    <td>Aum</td>
                    <td>15/07/2015</td>
                </tr>
                <tr>
                    <th scope="row">2</th>
                    <td>Cobre</td>
                    <td>75</td>
                    <td>KG</td>
                    <td>Bodega001</td>
                    <td>Aum</td>
                    <td>10/07/2015</td>
                </tr>
                <tr>
                    <th scope="row">3</th>
                    <td>Lata</td>
                    <td>10</td>
                    <td>KG</td>
                    <td>Bodega001</td>
                    <td>Dis</td>
                    <td>15/06/2015</td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
