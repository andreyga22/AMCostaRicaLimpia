<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProyectoAMCRL.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="login.css" />
    <link rel="stylesheet" type="text/css" href="ownStyles.css" />
    <title>AM Costa Rica Limpia | Login</title>


    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <link href="ownStyles.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" />
    <link href="sideNavStyle.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" integrity="sha384-UHRtZLI+pbxtHCWp1t77Bi1L4ZtiqrqD80Kn4Z8NTSRyMA2Fd33n5dQ8lWUE00s/" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.0/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/js/bootstrap.min.js"></script>

</head>
<body id="miBack">

    <br />







    <%--<div style="width: 60%; float: right; height: 300px">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/fondo.jpg" />
        </div>--%>

    <%--<div style="width: 50%; text-align: left; min-height: 100%; bottom: 0; background-color:white" >--%>
    <%--<div class="col-5" style="background-color:white; min-height: 100%; bottom: 0;">--%>
    <form id="form1" runat="server">
        <div class="modal" id="exampleModalCenter" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Recuperar contraseña</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="row justify-content-center">
                            <asp:Literal ID="lblError2" runat="server" Visible="false"></asp:Literal>
                        </div>

                        <div class="form-group">
                            <label for="recuperarUsuarioTb">Ingrese el correo electronico al que le desea restaurar la contraseña</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="recuperarUsuarioTb" ForeColor="Red" ValidationGroup="modal"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="recuperarUsuarioTb" type="email" runat="server" class="form-control" OnKeyDown="" ValidationGroup="modal"></asp:TextBox>

                            <small id="emailHelp" class="form-text text-danger">Este cambio no se puede deshacer.</small>
                        </div>


                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnEnviar" type="button" class="btn btn-primary" runat="server" Text="Enviar" ValidationGroup="modal" OnClick="btnEnviar_Click" />

                    </div>
                </div>
            </div>
        </div>
        <div class="mx-auto mt-auto fixed-top" style="background-color: #E6E6E6">
            <div class="row">
                <div class="col-lg-3">
                    <br />
                    <div class="container">

                        <br />
                        <br />
                        <br />
                        <div class="row justify-content-center">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logomel2_1.png" Width="35%" Height="35%" />
                        </div>
                        <br />
                        <br />
                        <div class="row justify-content-center">
                            <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
                        </div>
                        <br />
                        <div class="form-group">
                            <label for="usuarioTb" style="font-size: larger">Nombre de usuario</label>
                            <asp:TextBox ID="usuarioTb" type="email" runat="server" class="form-control form-control-lg"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="contraTb" style="font-size: larger">Contraseña</label>
                            <asp:TextBox ID="contraTb" type="password" class="form-control form-control-lg" runat="server"></asp:TextBox>
                        </div>
                        <br />
                        <div class="row justify-content-center">
                            <asp:Button ID="btnEntrar" runat="server" Text="Entrar" class="btn-lg btn-info" type="button" OnClick="btnEntrar_Click" />
                        </div>
                        <br />
                        <div class="offset-6">
                            <asp:LinkButton ID="olvidoLb" runat="server" data-toggle="modal" Style="color: dodgerblue" data-target="#exampleModalCenter" OnClick="olvidoLb_Click">¿Olvidó su contraseña?</asp:LinkButton>
                        </div>











                    </div>
                </div>
                <div class="col-lg-9 d-none d-lg-block d-sm-none">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/fondo.jpg" Width="106%" Height="106%" />
                </div>
            </div>
        </div>
    </form>

    <%--</div>--%>






















    <%--
        <div class="jumbotron jumbotron-fluid col-lg-4">
            <div class="container">


                <form id="form1" runat="server">
                    <div class="row justify-content-center">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logomel2_1.png" Width="30%" Height="30%" />
                    </div>
                    <br />
                    <div class="row justify-content-center">
                        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
                    </div>
                    <div class="form-group">
                        <label for="usuarioTb">Nombre de usuario</label>
                        <asp:TextBox ID="usuarioTb" type="email" runat="server" class="form-control"></asp:TextBox>
                     </div>
                    <div class="form-group">
                        <label for="contraTb">Contraseña</label>
                        <asp:TextBox ID="contraTb" type="password" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnEntrar" runat="server" Text="Entrar" class="btn btn-info" type="button" OnClick="btnEntrar_Click" />

                    <br />
                    <br />
                    <div class="offset-6">
                        <asp:LinkButton ID="olvidoLb" runat="server" data-toggle="modal" data-target="#exampleModalCenter" OnClick="olvidoLb_Click">¿Olvidó su contraseña?</asp:LinkButton>
                    </div>




                    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalCenterTitle">Recuperar contraseña</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">

                                    <div class="row justify-content-center">
                                        <asp:Literal ID="lblError2" runat="server" Visible="false"></asp:Literal>
                                    </div>

                                    <div class="form-group">
                                        <label for="recuperarUsuarioTb">Ingrese el correo electronico al que le desea restaurar la contraseña</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="recuperarUsuarioTb" ForeColor="Red" ValidationGroup="modal"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="recuperarUsuarioTb" type="email" runat="server" class="form-control" OnKeyDown="" ValidationGroup="modal"></asp:TextBox>
                 
                                        <small id="emailHelp" class="form-text text-danger">Este cambio no se puede deshacer.</small>
                                    </div>


                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                                    <asp:Button ID="btnEnviar" type="button" class="btn btn-primary" runat="server" Text="Enviar" ValidationGroup="modal" OnClick="btnEnviar_Click" />

                                </div>
                            </div>
                        </div>
                    </div>




                    <div id="themodal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                                    <h4 class="modal-title" id="myModalLabel">Modal title</h4>
                                </div>
                                <div class="modal-body">
                                    <p>
                                    The most important modal ever created
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>


                </form>
            </div>
        </div>
    --%>




    <%--</div>--%>






    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
</body>
</html>
