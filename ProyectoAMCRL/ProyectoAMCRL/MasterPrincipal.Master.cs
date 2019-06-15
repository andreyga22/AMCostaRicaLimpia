using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoAMCRL {
    public partial class MasterPrincipal : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void contrasenaLb_Click(object sender, EventArgs e) {
            Session["accionCuenta"] = 2;
            Response.Redirect("Cuenta.aspx");
        }

        protected void cuentasLb_Click(object sender, EventArgs e) {
            Session["accionCuenta"] = null;
            Response.Redirect("AdministrarCuentas.aspx");
        }

        protected void cerrarSesion(object sender, EventArgs e) {
            Response.Redirect("Login.aspx");
        }
    }
}