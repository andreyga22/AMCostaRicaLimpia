<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="Reporte_General.aspx.cs" Inherits="ProyectoAMCRL.Reporte_General" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="PaginaPrincipal.aspx">Principal</a></li>
                <li class="breadcrumb-item active" aria-current="page">Reportes</li>
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
                <h4>Reporte</h4>
            </div>
            <br />
            <div class="row">
                <asp:DropDownList class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" AutoPostBack="True" ID="DropDownList1" runat="server">
                    <asp:ListItem>Entradas</asp:ListItem>
                    <asp:ListItem>Salidas</asp:ListItem>
                    <asp:ListItem>Entradas y Salidas</asp:ListItem>
                </asp:DropDownList>

                <div class="row offset-8">
                    <asp:Button ID="Actualizarbtn" runat="server" Text="Actualizar" />
                    <div />
                    <br />
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-3">
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="checkbox" id="bodegasCB" value="bodegas">
                        <label class="form-check-label" for="inlineCheckbox1">Bodegas</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="checkbox" id="materialesCB" value="materiales">
                        <label class="form-check-label" for="inlineCheckbox2">Materiales</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="checkbox" id="sociosCB" value="socios">
                        <label class="form-check-label" for="inlineCheckbox3">Socios</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="checkbox" id="rangoFech" value="fechas">
                        <label class="form-check-label" for="inlineCheckbox4">Rango de Fechas</label>
                    </div>
                </div>
                <br />
                <br />
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
                <br />
                <div class="col-3">
                    <label class="font-weight-bold campo_izquierda" for="fechaActLabel">Fecha Actual: </label>
                    <asp:Label class="datosLbl" runat="server" ID="fechaActDatLabel">19/5/2019</asp:Label>
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
                            Entradas totales: 500000
                            Compras totales: 500000
                            Entradas por ajustes: 000
                        </td>
                        <td>
                            Entradas totales: 500000
                            Compras totales: 500000
                            Entradas por ajustes: 000

                        </td>
                    </tr>
                    <tr>
                        <th scope="row">Bodegas</th>
                        <td>
                            Bodegas:
                            BODEGAS-1: San Ramon
                            -Entradas totales: 5000
                        </td>
                        <td>
                           Bodegas:
                           BODEGAS-1: San Ramon
                           -Salidas totales: 5000 
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">Materiales</th>
                        <td>
                            Materiales:
                            Aluminio:
                            Total comprado: 5000
                            Total comprado: 7000
                        </td>
                        <td>
                            Materiales:
                            Aluminio:
                            Total comprado: 5000
                            Total comprado: 7000
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">Socios</th>
                        <td>
                            Socios:
                            Proveedor con mas compras registradas
                            12344 Juan Perez juan@gmail.com
                        </td>
                        <td>
                            Socios:
                            Cliente con mayor frecuencia en ventas 
                            12355 Pedro Gomez pedro@gmail.com
                        </td>
                    </tr>
                </tbody>
            </table>
            </div>

        </form>
    </div>
</asp:Content>
