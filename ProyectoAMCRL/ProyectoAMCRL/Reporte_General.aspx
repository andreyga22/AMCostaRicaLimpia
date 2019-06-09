<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Reporte_General.aspx.cs" Inherits="ProyectoAMCRL.Reporte_General" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item active">Reportes</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <div class="row">
            <h4 class="font-weight-bold">Reporte</h4>
        </div>
        <%-- FILTROS --%>
        <br>
        <div class="row float-right">
            <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" AutoPostBack="True" ID="DropDownList1" runat="server">
                <asp:ListItem>Reporte ventas y compras</asp:ListItem>
                <asp:ListItem>Reporte ventas</asp:ListItem>
                <asp:ListItem>Reporte compras</asp:ListItem>
            </asp:DropDownList>
        </div>
        <br>
        <h5>Seleccione las secciones que desea agregar al reporte:</h5>
        <br />
        <br />
        <div class="row">
            <div class="col-4">
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" id="bodegasCB" value="bodegas">
                    <label class="form-check-label" for="inlineCheckbox1">Incluir bodegas</label>
                </div>
            </div>
            <div class="col-4">
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" id="materialesCB" value="materiales">
                    <label class="form-check-label" for="inlineCheckbox2">Incluir materiales</label>
                </div>
            </div>
            <div class="col-4">
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" id="sociosCB" value="socios">
                    <label class="form-check-label" for="inlineCheckbox3">Incluir socios</label>
                </div>
            </div>
        </div>
        <br>
        <%-- FILTRO FECHAS --%>
        <div class="row" style="margin-left: 0.2%">
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="rangoFech" value="fechas">
                <label class="form-check-label font-weight-bold" for="inlineCheckbox4">Rango de Fechas</label>
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
        <br />
        <div class="row float-right" style="border: 1px solid #ccc4c4; margin-right: 0.1%">
            <asp:Label runat="server" ID="fechaActDatLabel">Fecha actual: 19/5/2019</asp:Label>
        </div>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th scope="col">Especificacion</th>
                    <th scope="col">Entradas</th>
                    <th scope="col">Salidas</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">General</th>
                    <td>
                        <p><strong>Entradas totales:</strong> 500000₡</p>
                        <p><strong>Ventas totales:</strong> 500000₡</p>
                        <p><strong>Entradas por ajustes:</strong> 00₡</p>
                    </td>
                    <td>
                        <p><strong>Salidas totales:</strong> 500000₡</p>
                        <p><strong>Compras totales:</strong> 500000₡</p>
                        <p><strong>Salidas por ajustes:</strong> 00₡</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">Bodegas</th>
                    <td>
                        <p><strong>Bodegas con mayor ventas:</strong></p>
                        <ul>
                            <li>
                                <p><strong>BODEGA-1: San Ramon:</strong> 200000₡</p>
                            </li>
                            <li>
                                <p><strong>BODEGA-2: Naranjo:</strong> 300000₡</p>
                            </li>
                        </ul>
                    </td>
                    <td>
                        <p><strong>Bodegas con más compras:</strong></p>
                        <ul>
                            <li>
                                <p><strong>BODEGA-1: San Ramon:</strong> 400000₡</p>
                            </li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <th scope="row">Materiales</th>
                    <td>
                        <p><strong>Materiales más vendidos:</strong></p>
                        <ul>
                            <li>Total vendido Aluminio: 50000₡</li>
                            <li>Total vendido Cobre: 70000₡</li>
                        </ul>
                    </td>
                    <td>
                        <p><strong>Materiales más comprados:</strong></p>
                        <ul>
                            <li>Total comprado Aluminio: 57000₡</li>
                            <li>Total comprado Hierro: 85000₡</li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <th scope="row">Socios</th>
                    <td>Socios:
                            Cliente con mayor frecuencia en ventas 
                            12355 Pedro Gomez pedro@gmail.com
                    </td>
                    <td>Socios:
                            Proveedor con mas compras registradas
                            12344 Juan Perez juan@gmail.com
                            
                    </td>
                </tr>
            </tbody>
        </table>
        <%-- SUBMMIT BUTTON --%>
        <div class="row justify-content-center">
            <asp:Button ID="btnDescargar" type="submit" runat="server" Text="Descargar" class="btn btn-info" Width="15%" />
        </div>
    </div>
</asp:Content>
