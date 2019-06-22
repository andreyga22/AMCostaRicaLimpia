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
    <div class="container">

        <br />





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
                        <%--<input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email">--%>
                        <%--                    <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>--%>
                    </div>
                    <div class="form-group">
                        <label for="contraTb">Contraseña</label>
                        <asp:TextBox ID="contraTb" type="password" class="form-control" runat="server"></asp:TextBox>
                        <%--<input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password">--%>
                    </div>
                    <%-- <div class="row">--%>

                    <asp:Button ID="btnEntrar" runat="server" Text="Entrar" class="btn btn-info" OnClick="btnEntrar_Click" />

                    <%--</div>--%>
                    <br />
                    <br />
                    <div class="offset-6">
                        <asp:LinkButton ID="olvidoLb" runat="server" data-toggle="modal" data-target="#exampleModalCenter" OnClick="olvidoLb_Click">¿Olvidó su contraseña?</asp:LinkButton>
                    </div>






                    <!-- Modal -->
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
                                        <label for="recuperarUsuarioTb">Ingrese el correo electronico al que le desea restaurar la contraseña</label>
                                        <asp:TextBox ID="recuperarUsuarioTb" type="email" runat="server" class="form-control" OnKeyDown="txt_Item_Number_KeyDown"></asp:TextBox>
                                        <small id="emailHelp" class="form-text text-danger">Este cambio no se puede deshacer.</small>
                                    </div>


                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                                    <asp:Button ID="btnEnviar" type="button" class="btn btn-primary" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />

                                </div>
                            </div>
                        </div>
                    </div>



                </form>
            </div>
        </div>








    </div>


    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
</body>
</html>
