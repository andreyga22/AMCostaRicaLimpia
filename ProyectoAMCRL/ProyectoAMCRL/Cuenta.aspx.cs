using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;


namespace ProyectoAMCRL {
    public partial class Cuenta : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnGuardar_Click(object sender, EventArgs e) {
            //string id = (String)Session["idBodega"];
            //if(!string.IsNullOrEmpty(id)) {

            //BLManejadorCuentas miCuen = consultarBodega(id);
            string accionCuenta = (String)Session["accionCuenta"];
            if(accionCuenta.Equals("0")) {

            } else {
                if(accionCuenta.Equals("1")) {

                }
            }


            string securepass = FormsAuthentication.HashPasswordForStoringInConfigFile(contraTb.Text.Trim(), "MD5");

            String estado = estadoRb.SelectedValue;
            Boolean estadoB = true;
            if(estado.Equals("Activado")) {
                estadoB = true;
            } else {
                estadoB = false;
            }

            BLCuenta cuenta = new BLCuenta(idTB.Text.Trim(), securepass, nombreTB.Text.Trim(), estadoB, estadoRb.SelectedItem.Text.Trim());
                BLManejadorCuentas man = new BLManejadorCuentas();
                man.guardarModificarCuenta(cuenta);
                //"<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>Se guardó la bodega con éxito.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";

                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó la cuenta correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            //} else {
            //    BLBodega bodega = new BLBodega(codigoTb.Text.Trim(), nombreTB.Text.Trim(), activaCb.Checked, new BLDireccion(provinciaTb.Text.Trim(), cantonTb.Text.Trim(), distritoTb.Text.Trim(), otrasTb.Text.Trim(), 0));
            //    BLManejadorBodega man = new BLManejadorBodega();
            //    man.guardarModificarBodega(bodega);
            //    lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>Se guardó la bodega con éxito.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
            //    lblError.Visible = true;
            //}
        }
    }
}