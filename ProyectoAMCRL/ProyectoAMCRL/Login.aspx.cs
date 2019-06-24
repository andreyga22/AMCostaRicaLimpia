using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using System.Windows.Forms;
using System.Net.Mail;

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
                if(cuenta.estado) {
                    Session["cuentaLogin"] = cuenta;
                    Response.Redirect("AdministrarCuentas.aspx");
                } else {
                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Su cuenta se encuentra deshabilitada. Contacte con un administrador del sistema.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }

            } else {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Credenciales incorrectos.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        protected void olvidoLb_Click(object sender, EventArgs e) {



        }

        protected void btnEnviar_Click(object sender, EventArgs e) {
            BLCuenta cuenta = new BLManejadorCuentas().consultarCuenta(recuperarUsuarioTb.Text.Trim());
            if(cuenta != null) {
                try {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("amcrlcuentas@gmail.com");
                    mail.To.Add(cuenta.id_usuario);
                    mail.Subject = "Cambio de contraseña";

                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var stringChars = new char[10];
                    var random = new Random();

                    for(int i = 0; i < stringChars.Length; i++) {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    string finalString = new String(stringChars);
                    string securepass = FormsAuthentication.HashPasswordForStoringInConfigFile(finalString, "MD5");

                    new BLManejadorCuentas().restaurarContra(cuenta.id_usuario, securepass);

                    mail.Body = "Se ha registrado una petición de cambio de contraseña a la cuenta " + cuenta.id_usuario + "\n" +
                                    "Su nueva contraseña temporal es: " + finalString + "\n" + 
                                    "\n\nSi usted no solicitó este cambio contacte con su administrador del sistema.";

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("amcrlcuentas@gmail.com", "AMCRL.03");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    lblError2.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se ha restaurado la contraseña. Revise su correo electronico<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError2.Visible = true;
                } catch(Exception ex) {
                    lblError2.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + ex.ToString() +"<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError2.Visible = true;
                }
            }

        }

        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                
            }
        }
    }
}