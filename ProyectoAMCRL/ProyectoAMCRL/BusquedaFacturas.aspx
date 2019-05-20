<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BusquedaFacturas.aspx.cs" Inherits="ProyectoAMCRL.BusquedaFacturas" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Principal.aspx">Principal</a></li>
                <li class="breadcrumb-item active" aria-current="page">Busqueda Facturas</li>
            </ol>
        </nav>

        <form id="form1" runat="server">
            <br />
            <div class="row justify-content-center">
                <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
            </div>
            <br />
            <div class="row">
                <h4>Busqueda de facturas</h4>
            </div>
            <br />
            <br />
            <br />



            <div class="col">

                <div class="row">
                    <h5>Filtros</h5>
                </div>

                <div class="row">
                    <div class="form-group">
                        <label for="palabraTb">Palabra Clave</label>
                        <asp:TextBox type="text" ID="palabraTb" class="form-control" runat="server" TextMode="SingleLine" placeholder="Código o socio"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group">
                        <label for="fechasCb">Fecha</label>
                        <asp:CheckBox ID="fechasCb" type="checkbox" runat="server" />
                    </div>
                </div>


                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <div class="row">
                <div class="col-6">
                    Fecha inicio:<br />
                    <div style="text-align: center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Calendar ID="CalendFechaIni" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" SelectedDate="11/25/2018 12:21:25">
                                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#8DAA9D" ForeColor="White" />
                                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#8DAA9D" />
                                    <TodayDayStyle BackColor="#CCCCCC" />
                                </asp:Calendar>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <br />
                    </div>
                </div>
                <div class=" col-6">
                    Fecha Fin:<div style="text-align: center">
                        <div style="text-align: center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="calendFechaFin" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" SelectedDate="11/25/2018 12:21:35">
                                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="#16ACB8" ForeColor="White" />
                                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#8DAA9D" />
                                        <TodayDayStyle BackColor="#8DAA9D" />
                                    </asp:Calendar>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                        </div>
                        <br />
                    </div>
                </div>
                </div>


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
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group">
                        <label for="montoCb">Monto</label>
                        <asp:CheckBox ID="montoCb" type="checkbox" runat="server" />
                    </div>
                </div>

                <div class="row">
                    <div class="form-group">
                        <label for="montoInicialTb">Esciba el monto minimo</label>
                        <asp:TextBox type="text" ID="montoInicialTb" class="form-control" runat="server" TextMode="SingleLine" ></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group">
                        <label for="montoMaximoTb">Esciba el monto maximo</label>
                        <asp:TextBox type="text" ID="montoMaximoTb" class="form-control" runat="server" TextMode="SingleLine" ></asp:TextBox>
                    </div>
                </div>

            </div>

            <br />
            <br />
            <br />
            <br />
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Código Factura</th>
                        <th scope="col">Fecha</th>
                        <th scope="col">Monto</th>
                        <th scope="col">Socio</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>54687</td>
                        <td>22/09/2015</td>
                        <td>25000</td>
                        <td>Jorge González</td>
                    </tr>
                    <tr>
                        <th scope="row">2</th>
                        <td>54688</td>
                        <td>23/09/2015</td>
                        <td>30000</td>
                        <td>María Gómez</td>
                    </tr>
                    <tr>
                        <th scope="row">3</th>
                        <td>54689</td>
                        <td>23/09/2015</td>
                        <td>45000</td>
                        <td>Selena Gómez</td>
                    </tr>
                </tbody>
            </table>

            <br />
            <br />

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















