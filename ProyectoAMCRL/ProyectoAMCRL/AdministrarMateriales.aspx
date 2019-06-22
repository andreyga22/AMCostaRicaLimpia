<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="AdministrarMateriales.aspx.cs" Inherits="ProyectoAMCRL.Materiales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <link rel='stylesheet' href='https://use.fontawesome.com/releases/v5.7.0/css/all.css' />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>


    <%-- TABLA JQUERY --%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css">
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>

    <script>

        //CONSULTA JQUERY
        $(document).ready(function () {
            $('#tablaMateriales2').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });
        });

        function abrirDetalleClick(id) {
            window.location.replace("RegistroMateriales.aspx?idM=" + id);
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="AdministrarMateriales.aspx" style="color: dodgerblue">Materiales</a></li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>


    <h4>Materiales en sistema</h4>
    <br />
    <!-- Modal -->
    <div class="container">

        <!-- Trigger the modal with a button -->
        <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#myModal">Filtrar</button>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color: rgba(230, 230, 230, 0.48)">
                        <h6 class="modal-title font-weight-bolder" style="float: left">Selección de filtros</h6>
                        <button type="button" class="close" data-dismiss="modal" style="float: right">&times;</button>
                    </div>
                    <div class="modal-body">
                        <%-- BODEGA --%>
                        <div class="col-lg-3">
                            <label class="h6 dato">Bodegas:</label>
                            <asp:DropDownList OnSelectedIndexChanged="bodegasDrop_SelectedIndexChanged" class="btn btn-light dropdown-toggle" type="dropdown" ata-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ID="bodegasDrop" runat="server" AutoPostBack="true" Width="150px">
                                <asp:ListItem Value="">Ninguna</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <br>
    <a href="RegistroMateriales.aspx" class="btn btn-info btn-sm" style="float: right">
        <span class="fa fa-plus"></span>
    </a>

    <table class="table table-bordered table-sm" id="tablaMateriales2">
        <thead>
            <tr>
                <th scope="col">Codigo</th>
                <th scope="col">Nombre</th>
                <th scope="col">Precio kilo</th>
            </tr>
        </thead>
        <tbody id="cuerpoTabla" class="table-sm">
            <asp:PlaceHolder runat="server" ID="tablaPlaceHolder"></asp:PlaceHolder>
        </tbody>
    </table>
    <div />
    <br>
    <br>
</asp:Content>
