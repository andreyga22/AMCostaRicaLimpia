<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroSociosUI.aspx.cs" Inherits="ProyectoAMCRL.RegistroSociosUI" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ownStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="breadcrumbBodyHolder" runat="server">
    <li class="breadcrumb-item"><a href="BusquedaSocios.aspx" style="color: dodgerblue">Búsqueda Socios</a></li>
    <li class="breadcrumb-item active">Socio</li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <div class="row justify-content-center">
        <asp:Literal ID="lblError" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="container">
        <br />
        <div class="form-row">
            <div class="offset-1">
                <h4>Socio</h4>
            </div>
            <div class="col-3 offset-7">
                <asp:LinkButton ID="LinkAsoc" runat="server" Visible="false" Style="color: dodgerblue" OnClick="LinkAsoc_Click">¿Desea asociarlo a otro socio existente?</asp:LinkButton>
            </div>
        </div>
        <br />

        <%--<asp:Button ID="btnFactur" type="submmit" runat="server" Text="Facturas" class="btn btn-outline-secondary" Width="15%" Visible="false" />
        <asp:Button ID="btnAsociar" type="submmit" runat="server" Text="Asociar" class="btn btn-outline-secondary" Width="15%" Visible="false" />--%>
        <div class="offset-1">
            <h5>Datos Personales</h5>
        </div>
        <br />
        <div class="form-row">
            <div class="form-group offset-1 col-md-5">
                <label for="idTB">Identificación*</label>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Requerido" display="Dynamic" ControlToValidate="idTB" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ErrorMessage="Debe tener entre 8 a 12 caracteres " ForeColor="Red" ValidationGroup="socioG" ValidationExpression="^[a-zA-Z0-9]{8,12}$" ControlToValidate="idTb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="idTB" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-5">
               <label for="nombreTB">Nombre*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" display="Dynamic" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="nombreTB" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (máx. caract: 50)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s\.]{1,50}$" ControlToValidate="nombreTb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="nombreTB" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group offset-1 col-md-5">
                <label for="ape1TB">Primer apellido</label>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (máx. caract: 25)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\.]{1,25}$" ControlToValidate="ape1Tb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="ape1TB" class="form-control" runat="server"></asp:TextBox>
                
            </div>
            <div class="form-group col-md-5">
                <label for="ape2TB">Segundo apellido</label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (máx. caract: 25)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\.]{1,25}$" ControlToValidate="ape2Tb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="ape2TB" class="form-control" runat="server"></asp:TextBox>

            </div>
        </div>

        <div class="form-row">
            <div class="form-group offset-1 col-md-3">
                <label for="telTB">Teléfono Habitación*&nbsp;&nbsp;&nbsp; </label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo Requerido" display="Dynamic" ControlToValidate="telTB" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                &nbsp;
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (Requiere 8 Caracteres)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="^[0-9]{8,8}$" ControlToValidate="telTb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="telTB" class="form-control" runat="server"></asp:TextBox>  
            </div>
            <div class="form-group col-md-3">
                <label for="tel2TB">Teléfono Personal*&nbsp;&nbsp;&nbsp; </label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" display="Dynamic" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="tel2TB" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                &nbsp;
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (Requiere 8 Caracteres)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="^[0-9]{8,8}$" ControlToValidate="tel2Tb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="tel2TB" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-4">
                <label for="correoTB">Correo electrónico*</label><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" display="Dynamic" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="correoTB" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                <asp:TextBox type="email" ID="correoTB" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

        <br />
        <div class="offset-1">
            <h5>Dirección</h5>
        </div>
        <br />

        <div class="form-row">
            <div class="form-group offset-1 col-md-5">
                <label for="provinciaTB">Provincia*</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" display="Dynamic" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="provinciaTB" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (máx. caract: 15)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s\.]{1,15}$" ControlToValidate="provinciaTb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="provinciaTB" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-5">
                <label for="cantonTB">Cantón*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator7" display="Dynamic" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="cantonTB" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (máx. caract: 15)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s\.]{1,15}$" ControlToValidate="cantonTb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="cantonTB" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group offset-1 col-md-5">
                <label for="distritoTB">Distrito*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator8" display="Dynamic" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="distritoTB" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (máx. caract: 15)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s\.]{1,15}$" ControlToValidate="distritoTb"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="distritoTB" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-5">
                <label for="sennas">Otras Señas*</label><asp:RequiredFieldValidator ID="RequiredFieldValidator9" display="Dynamic" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="sennas" ForeColor="Red" ValidationGroup="socioG"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" Display="Dynamic" ErrorMessage="Formato inválido (máx. caract: 150)" ForeColor="Red" ValidationGroup="socioG" ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s\.]{1,150}$" ControlToValidate="sennas"></asp:RegularExpressionValidator>
                <asp:TextBox type="text" ID="sennas" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group offset-1 col-md-5">
                <label for="rolRadios">Rol</label>
                <br />
                <asp:RadioButtonList ID="rolRadios" runat="server" RepeatDirection="Horizontal" CellPadding="5" CssClass="d-inline">
                    <asp:ListItem Selected="True">Proveedor</asp:ListItem>
                    <asp:ListItem>Cliente</asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div class="form-group col-md-5" id="estado" runat="server" visible="true">
                <label for="estadoRb">Estado</label>
                <asp:RadioButtonList ID="estadoRb" runat="server" RepeatDirection="Horizontal" CellPadding="5">
                    <asp:ListItem Selected="True">Activado</asp:ListItem>
                    <asp:ListItem>Desactivado</asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <%--<div class="form-group col-md-6">
                <label for="activaCb">Activado</label>
                <asp:CheckBox ID="activaCb" type="checkbox" runat="server" Visible="false" />
            </div>--%>
        </div>

        <%-- SUBMMIT BUTTON --%>
        <div class="row justify-content-center">
            <asp:Button ID="btnRegistrar" type="submit" runat="server" Text="Guardar" class="btn btn-info" Width="15%" OnClick="btnRegistrar_Click" ValidationGroup="socioG" />
        </div>
        <asp:Label ID="info" runat="server"></asp:Label>
        <br />
    </div>
</asp:Content>
