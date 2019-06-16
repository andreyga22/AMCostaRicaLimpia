using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            Session["accionCuenta"] = null;
            Session["idBodega"] = null;
            Session["idCuenta"] = null;
            Session["cuentaLogin"] = null;
        }

        protected void btnEntrar_Click(object sender, EventArgs e) {
            BLManejadorCuentas man = new BLManejadorCuentas();
            string securepass = FormsAuthentication.HashPasswordForStoringInConfigFile(contraTb.Text.Trim(), "MD5");
            BLCuenta cuenta = man.login(usuarioTb.Text.Trim(), securepass);
            if(cuenta != null) {
                Session["cuentaLogin"] = cuenta;
                Response.Redirect("AdministrarCuentas.aspx");
            } else {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Credenciales incorrectos.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }
    }
}