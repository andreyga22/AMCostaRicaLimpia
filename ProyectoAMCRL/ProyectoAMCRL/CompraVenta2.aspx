<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="CompraVenta2.aspx.cs" Inherits="ProyectoAMCRL.CompraVenta2" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>  
    <script>
        $(document).ready(function () {
            $(window).unload(function () {
                alert("Goodbye!");
            });
        });  
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li runat="server" id="bread" class="breadcrumb-item active">Compra
    </li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server" enablepagemethods="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
                <div class="row justify-content-center">
                    <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
                </div>
                <%--</ContentTemplate>
        </asp:UpdatePanel>--%>

                <p>When you click <a href="http://www.javatpoint.com/">this link</a>, or close the window,  
 unload event will be triggered.</p>  

                <br />
                <%--<asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>--%>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="empresaLb" runat="server" Text="AM Costa Rica Verde" Font-Bold="True"></asp:Label>
                    </div>
                    <div class="col-md-2 offset-sm-8">
                        <asp:Label ID="fechaLb" runat="server" Text="" Font-Bold="True"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <%--<div class="row">--%>

                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label2" runat="server" Text="Factura(No final): #" Font-Bold="True"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="numFacturaLb" runat="server" Text=""></asp:Label>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label9" runat="server" Text="Teléfono:" Font-Bold="True"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="telEmpLb" runat="server" Text="83964649"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label10" runat="server" Text="Tipo:" Font-Bold="True"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="tipoLb" runat="server" Text="Factura de compra"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label1" runat="server" Text="Bodega:" Font-Bold="True"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="bodegasDd" AutoPostBack="True" ViewStateMode="Enabled" OnSelectedIndexChanged="bodegasDd_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>

                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label6" runat="server" Text="Encargado:" Font-Bold="True"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="cajeroLb" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <%--</div>--%>
                    </div>
                    <br />
                    <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>--%>
                    <div class="col-md-6">
                        <div class="row">

                            <div class="col-md-6">
                                <asp:Label ID="Label11" runat="server" Text="Socio:" Font-Bold="True"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="NombreCP" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-6">
                                <asp:Label ID="Label12" runat="server" Text="Identificación:" Font-Bold="True"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="cedulaCP" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label13" runat="server" Text="Contactos:" Font-Bold="True"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="telCP" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="correoCP" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="direccionCP" runat="server" Text=""></asp:Label>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:LinkButton ID="agregarCP" runat="server" data-toggle="modal" data-target="#exampleModal" OnClick="agregarCP_Click">Agregar Cliente/Prov</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- 
            
            !-->
        <hr />


        <!-- Button trigger modal -->
        <%--<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
            Launch demo modal
        </button>--%>

        <!-- Modal -->
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Búsqueda de socio</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">


                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="col-3">
                                        <asp:Button ID="refrescarbtn" type="button" runat="server" Text="Refrescar" class="btn btn-primary" OnClick="refrescarbtn_Click" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-3">
                                <asp:Button ID="nuevoSoc" class="btn btn-light" runat="server" Text="¿Nuevo socio?" OnClick="nuevoSoc_Click" />
                            </div>
                            <div class="col-3 offset-3">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox type="text" ID="txtPalabra" class="form-control" runat="server" TextMode="SingleLine" placeholder="Buscar" AutoPostBack="true" OnKeyDown="txt_Item_Number_KeyDown" OnTextChanged="txtPalabra_TextChanged"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <br />
                        <div class="row justify-content-center">
                            <div class="col-12 table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView class="table table-hover table-bordered table-striped" ID="gridSocios" runat="server" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gridSocios_PageIndexChanging" OnSorting="gridSocios_Sorting" OnSelectedIndexChanged="gridSocios_SelectedIndexChanged" OnKeyDown="" OnRowDataBound="gridSocios_RowDataBound" PageSize="5">
                                            <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NextPreviousFirstLast" PageButtonCount="4" />
                                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                                            <PagerStyle HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                    </div>
                </div>
            </div>
        </div>



        <!-- 
            
            !-->


        <br />
        <br />

        <div class="row mb-1">
            <asp:Button ID="nuevoMat" class="btn btn-light" runat="server" Text="¿No encuentra el material?" OnClick="nuevoMat_Click" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>--%>
                
                <div class="row justify-content-center">
                    <div class="col-md-3">
                        <label for="materialesDd">Material</label>
                        <asp:DropDownList runat="server" ID="materialesDd" AutoPostBack="True" ViewStateMode="Enabled" OnSelectedIndexChanged="materialesDd_SelectedIndexChanged"></asp:DropDownList>
                        <asp:LinkButton runat="server" ID="refrescarMate" Text="<i class='fa fa-refresh' aria-hidden='true'></i>" OnClick="refrescarMate_Click"/>
                    </div>
                    <div class="col-md-3">
                        <label for="nombreMat">Nombre Material</label>
                        <asp:TextBox ID="nombreMat" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label for="cantDisponibleTb">Cantidad Disponible</label>
                        <asp:TextBox ID="cantDisponibleTb" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label for="cantidadVC">Cantidad</label>
                        <asp:TextBox ID="cantidadVC" runat="server" OnTextChanged="cantidadVC_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <texbox></texbox>
                    </div>
                </div>
                <div class="row justify-content-center">

                    <div class="col-md-3">
                        <label for="unidadTb">Unidad Medida</label>
                        <asp:TextBox ID="unidadTb" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label for="precioCV">Precio Unidad</label>
                        <asp:TextBox ID="precioCV" runat="server" OnTextChanged="precioCV_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <%--Style="text-align: right"--%>
                    </div>
                    <div class="col-md-3">
                        <label for="impuestoTb">Impuesto</label>
                        <asp:TextBox ID="impuestoTb" runat="server" OnTextChanged="impuestoTb_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label for="descuentoTb">Descuento</label>
                        <asp:TextBox ID="descuentoTb" runat="server" OnTextChanged="descuentoTb_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <div class="row">


                    <div class="col-md-3">
                        <label for="precioTotal">Total linea</label>
                        <asp:TextBox ID="precioTotal" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="agregarBtn" Class="btn btn-info mt-4" runat="server" Text="Agregar" OnClick="agregarBtn_Click" />
                    </div>
                </div>
                <%--<div class="row justify-content-center">
                    
                </div>--%>

                <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
                <div class="row justify-content-center">
                    <asp:Literal ID="lblError2" runat="server" Visible="false"></asp:Literal>
                </div>
                <br />
                <div class="row">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <div class="col-md-12">
                        <div id="popup" style="max-height: 700px; overflow-y: scroll;">
                            <asp:GridView class="table table-hover table-bordered table-striped" ID="gridFactura" runat="server" OnSelectedIndexChanged="gridFactura_SelectedIndexChanged" AutoGenerateSelectButton="True"></asp:GridView>
                        </div>
                    </div>
                    <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
                </div>

                <br />
                <%--<asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>--%>
                <div class="row justify-content-end">
                    <div class="col-md-2">
                        <asp:Label ID="Label3" runat="server" Text="Subtotal"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="subtotalTb" runat="server" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="row justify-content-end">
                    <div class="col-md-2">
                        <asp:Label ID="Label4" runat="server" Text="Impuesto"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="impuestoTot" runat="server" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="row justify-content-end">
                    <div class="col-md-2">
                        <asp:Label ID="Label5" runat="server" Text="Descuento"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="descuentoTot" runat="server" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="row justify-content-end">
                    <div class="col-md-2">
                        <asp:Label ID="Label7" runat="server" Text="Total"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="totalTb" runat="server" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row justify-content-end">
                    <%--<div class="col-md-1">
                        <asp:Label ID="Label8" runat="server" Text="Total en moneda: "></asp:Label>
                    </div>--%>
                    <div class="col-md-2">
                        <label for="monedaDd">Total en moneda: </label>
                        <asp:DropDownList runat="server" ID="monedaDd" AutoPostBack="True" ViewStateMode="Enabled" OnSelectedIndexChanged="monedaDd_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="totalConvert" runat="server" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <%--</ContentTemplate>
        </asp:UpdatePanel>--%>

                <div class="row justify-content-end">
                    <div class="col-md-2">
                        <asp:Button ID="generarBtn" runat="server" Class="btn btn-info" Text="Agregar" OnClick="generarBtn_Click" />
                    </div>
                </div>
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
