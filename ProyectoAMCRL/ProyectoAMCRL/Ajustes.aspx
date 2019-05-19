<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Ajustes.aspx.cs" Inherits="ProyectoAMCRL.Ajustes" %>

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
            <div class="row">
                <h4>Ajustes de Inventario</h4>
            </div>
            <br />
            <br />
            <br />


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
                <div class="form-group">
                    <asp:Button ID="btnGuardar" type="submmit" runat="server" Text="Guardar" class="btn btn-outline-primary btn-lg" />
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />


            <asp:GridView ID="ajustesGv" runat="server">
                <Columns>
                    <asp:BoundField DataField="Aluminio" HeaderText="Material" />
                    <asp:BoundField HeaderText="Peso" />
                    <asp:BoundField HeaderText="Unidad Medida" />
                    <asp:BoundField HeaderText="Bodega" />
                    <asp:BoundField HeaderText="Acción" />
                </Columns>
            </asp:GridView>



            <br />
            <br />
            <br />
        </form>

    </div>
</asp:Content>
