using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class MasterPrincipal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
            if(usuarioLogin.rol.Equals("r")) {
                cuentasSub.Visible = false;
            } else {
                cuentasSub.Visible = true;
            }
        }

        protected void contrasenaLb_Click(object sender, EventArgs e)
        {
            Session["accionCuenta"] = 2;
            Response.Redirect("Cuenta.aspx");
        }

        protected void cuentasLb_Click(object sender, EventArgs e)
        {
            Session["accionCuenta"] = null;
            Response.Redirect("AdministrarCuentas.aspx");
        }

        protected void compraLB_Click(object sender, EventArgs e)
        {
            Session.Add("modo", "compra");
            Response.Redirect("CompraVenta2.aspx");
        }

        protected void ventaLB_Click(object sender, EventArgs e)
        {
            Session.Add("modo", "venta");
            Response.Redirect("CompraVenta2.aspx");
        }

        protected void cerrarSesion(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnBusqFactVent_Click(object sender, EventArgs e)
        {
            Session.Add("modo", "venta");
            Response.Redirect("BusquedaFacturas.aspx");
        }

        protected void btnBusqFactComp_Click(object sender, EventArgs e)
        {
            Session.Add("modo", "compra");
            Response.Redirect("BusquedaFacturas.aspx");
        }

        protected void RegistroLB_Click(object sender, EventArgs e)
        {
            Session["idSocio"] = null;
            Response.Redirect("RegistroSociosUI.aspx");
        }

        protected void cambiarContra_Click(object sender, EventArgs e) {
            Session["accionCuenta"] = 2;
            Response.Redirect("Cuenta.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e) {
            Response.Redirect("BusquedaSocios.aspx");
        }

        protected void LinkButton5_Click(object sender, EventArgs e) {
            Response.Redirect("GestionDeInventario.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e) {
            Response.Redirect("Ajustes.aspx");
        }

        protected void LinkButton6_Click(object sender, EventArgs e) {
            Response.Redirect("AdministrarBodegas.aspx");
        }

        protected void LinkButton12_Click(object sender, EventArgs e) {
            Response.Redirect("AdministrarMateriales.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e) {
            Response.Redirect("AdministrarMonedas.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e) {
            Response.Redirect("AdministrarUnidadesMedida.aspx");
        }

        protected void LinkButton13_Click(object sender, EventArgs e) {
            Response.Redirect("AdministrarCuentas.aspx");
        }

        protected void logo_Click(object sender, ImageClickEventArgs e) {
            Response.Redirect("Principal.aspx");
        }

    }
}