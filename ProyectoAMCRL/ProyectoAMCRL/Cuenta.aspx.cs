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
        /// <summary>
        /// Carga todos los componentes de la pantalla. 
        ///Reisa si hay un usuario en sesión para permitir o negar la carga
        ///de la página.En caso de negarlo vuelve al login.
        ///En caso de que se abra esta pantalla desde al seleccionar una opcion de la tabla
        ///cuentas de la página AdministrarCuentas.aspx, los campos se cargarán con los datos de la cuenta
        ///seleccionada.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            if(Session["cuentaLogin"] != null) {
                if(!IsPostBack) {
                    try {
                        string accionCuenta = Convert.ToString((Int32)Session["accionCuenta"]);
                        if(accionCuenta.Equals("0")) { //guardar por primera vez
                            identi.Visible = true;
                            contra.Visible = true;
                            nombre.Visible = true;
                            rol.Visible = true;
                            estado.Visible = true;
                            breadObj.InnerText = "Creación de cuenta";
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
                                breadObj.InnerText = "Modificación de cuenta";
                                tituloCuenta.InnerText = "Modificación de cuenta";
                            } else { //cambiar contrasena
                                contra.Visible = true;
                                nueva.Visible = true;
                                repetir.Visible = true;
                                contra.Attributes["class"] = "form-group offset-1 col-md-10";
                                contraTb.Text = "Contraseña actual";
                                breadObj.InnerText = "Cambio de contraseña";
                                tituloCuenta.InnerText = "Cambio de contraseña";
                            }
                        }
                    } catch(Exception exx) {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                }
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        /// <summary>
        /// Guarda o actualiza el contenido de la pagina Cuenta con 3 diferentes opciones:
        /// 1. Se guarda la cuenta por primera vez (Realizado por admin)
        /// 2. Se actualiza los datos de la cuenta (Realizado por admin)
        /// 3. Se cambia la contraseña de la cuenta (Realizado por el dueño de la cuenta)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e) {
            try {
                string accionCuenta = Convert.ToString((Int32)Session["accionCuenta"]);
                if(accionCuenta.Equals("0")) { //guardar por primera vez
                    try {
                        string securepass = FormsAuthentication.HashPasswordForStoringInConfigFile(contraTb.Text.Trim(), "MD5");
                        String estado = estadoRb.SelectedValue;
                        Boolean estadoB = true;
                        if(estado.Equals("Activado")) {
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
                        BLCuenta cuenta = new BLCuenta(idTB.Text.Trim(), securepass, nombreTB.Text.Trim(), rola, estadoB);
                        BLManejadorCuentas man = new BLManejadorCuentas();
                        man.guardarCuenta(cuenta);
                        lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó la cuenta correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    } catch(Exception exx) {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                } else {
                    if(accionCuenta.Equals("1")) { //modificar cuenta
                        try { 
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
                        lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se modificó la cuenta correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                        } catch(Exception exx) {
                            lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        }
                    } else { //cambiar contrasena
                        try {
                            string nuevaC = FormsAuthentication.HashPasswordForStoringInConfigFile(nuevaTb.Text.Trim(), "MD5");
                            string viejaC = FormsAuthentication.HashPasswordForStoringInConfigFile(contraTb.Text.Trim(), "MD5");
                            string repetir = FormsAuthentication.HashPasswordForStoringInConfigFile(repetirTb.Text.Trim(), "MD5");
                            BLCuenta cuenta = (BLCuenta)(Session["cuentaLogin"]);

                            //BLCuenta cuenta = new BLCuenta(idTB.Text.Trim(), viejaC, nombreTB.Text.Trim(), estadoRb.SelectedItem.Text.Trim(), estadoB );
                            BLManejadorCuentas man = new BLManejadorCuentas();
                            Boolean exists = man.consultarContra(cuenta.id_usuario, viejaC);
                            if(exists) {
                                man.modificarContrasena(cuenta.id_usuario, viejaC, nuevaC);
                                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se cambió la contraseña correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                lblError.Visible = true;
                            } else {
                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> La contraseña no coincide con su usuario. <button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                lblError.Visible = true;
                            }
                           
                        } catch(Exception exx) {
                            lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        }
                    }

                }
            } catch(Exception exx) {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }
    }
}