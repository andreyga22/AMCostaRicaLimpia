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
            if(!IsPostBack) {
                string accionCuenta = Convert.ToString((Int32)Session["accionCuenta"]);
                if(accionCuenta.Equals("0")) { //guardar por primera vez
                    identi.Visible = true;
                    contra.Visible = true;
                    nombre.Visible = true;
                    rol.Visible = true;
                    estado.Visible = true;
                } else {
                    if(accionCuenta.Equals("1")) { //modificar cuenta
                        identi.Visible = true;
                        idTB.Enabled = false;
                        nombre.Visible = true;
                        rol.Visible = true;
                        estado.Visible = true;
                        BLManejadorCuentas man = new BLManejadorCuentas();
                        string id = (String)Session["idCuenta"];
                        BLCuenta cuenta = man.consultarCuenta(id);
                        idTB.Text = cuenta.id_usuario;
                        nombreTB.Text = cuenta.nombre_usuario;
                        string roli = cuenta.rol;
                        int r = 0;
                        if(roli.Equals("a")) {
                            r = 1;
                        } else {
                            r = 0;
                        }
                        rolDd.SelectedIndex = r;
                        Boolean ess = cuenta.estado;
                        int est = 0;
                        if(ess) {
                            est = 0;
                        } else {
                            est = 1;
                        }
                        estadoRb.SelectedIndex = est;
                    } else { //cambiar contrasena
                        contra.Visible = true;
                        nueva.Visible = true;
                        repetir.Visible = true;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e) {

            string accionCuenta = Convert.ToString((Int32)Session["accionCuenta"]);
            if(accionCuenta.Equals("0")) { //guardar por primera vez

                string securepass = FormsAuthentication.HashPasswordForStoringInConfigFile(contraTb.Text.Trim(), "MD5");
                String estado = estadoRb.SelectedValue;
                Boolean estadoB = true;
                if(estado.Equals("Activado")) {
                    estadoB = true;
                } else {
                    estadoB = false;
                }
                BLCuenta cuenta = new BLCuenta(idTB.Text.Trim(), securepass, nombreTB.Text.Trim(), estadoRb.SelectedItem.Text.Trim(), estadoB);
                BLManejadorCuentas man = new BLManejadorCuentas();
                man.guardarCuenta(cuenta);
                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó la cuenta correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;

            } else {
                if(accionCuenta.Equals("1")) { //modificar cuenta

                    int estado = estadoRb.SelectedIndex;
                    Boolean estadoB = true;
                    if(estado == 0) {
                        estadoB = true;
                    } else {
                        estadoB = false;
                    }
                    int rr = rolDd.SelectedIndex;
                    String rola = "";
                    if(rr == 0) {
                        rola = "r";
                    } else {
                        rola = "a";
                    }
                    BLCuenta cuenta = new BLCuenta(idTB.Text.Trim(), "", nombreTB.Text.Trim(), rola, estadoB);
                    BLManejadorCuentas man = new BLManejadorCuentas();
                    man.modificarCuenta(cuenta);
                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó la cuenta correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;

                } else { //cambiar contrasena

                    string nuevaC = FormsAuthentication.HashPasswordForStoringInConfigFile(nuevaTb.Text.Trim(), "MD5");
                    string viejaC = FormsAuthentication.HashPasswordForStoringInConfigFile(contraTb.Text.Trim(), "MD5");
                    string repetir = FormsAuthentication.HashPasswordForStoringInConfigFile(repetirTb.Text.Trim(), "MD5");
                    
                    //BLCuenta cuenta = new BLCuenta(idTB.Text.Trim(), viejaC, nombreTB.Text.Trim(), estadoRb.SelectedItem.Text.Trim(), estadoB );
                    BLManejadorCuentas man = new BLManejadorCuentas();
                    man.modificarContrasena("andreyga", viejaC, nuevaC);
                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó la cuenta correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
            }



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