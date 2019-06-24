using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL {
    public partial class MasterPrincipal : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
            if (usuarioLogin.rol.Equals("r"))
            {
                cuentasSubmenu2.Visible = false;
            }
            else
            {
                cuentasSubmenu2.Visible = true;
            }
        }

        protected void contrasenaLb_Click(object sender, EventArgs e) {
            Session["accionCuenta"] = 2;
            Response.Redirect("Cuenta.aspx");
        }

        protected void cuentasLb_Click(object sender, EventArgs e) {
            Session["accionCuenta"] = null;
            Response.Redirect("AdministrarCuentas.aspx");
        }

        protected void compraLB_Click(object sender, EventArgs e)
        {
            Session.Add("modo", "compra");
            Response.Redirect("Compra_Venta.aspx");
        }

        protected void ventaLB_Click(object sender, EventArgs e)
        {
            Session.Add("modo", "venta");
            Response.Redirect("Compra_Venta.aspx");
        }

        protected void cerrarSesion(object sender, EventArgs e) {
            Response.Redirect("Login.aspx");
        }

        protected void cambiarContra(object sender, EventArgs e) {
            Session["accionCuenta"] = 2;
            Response.Redirect("Cuenta.aspx");
        }
    }
}