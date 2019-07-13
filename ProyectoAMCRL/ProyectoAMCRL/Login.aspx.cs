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
using System.Net;

namespace ProyectoAMCRL {
    public partial class Login : System.Web.UI.Page {
        /// <summary>
        /// Carga el contenido de la pagina de Login y se asegura de que todas las variables de sesion esten en null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            Session["accionCuenta"] = null;
            Session["idBodega"] = null;
            Session["idCuenta"] = null;
            Session["cuentaLogin"] = null;
            Session["SortedView"] = null;
            Session["idUnidad"] = null;
            Session["idSocio"] = null;
        }

        /// <summary>
        /// Método del evento entrar, que permite a una cuenta acceder al sistema por medio de las credenciales 
        /// pre establecidas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntrar_Click(object sender, EventArgs e) {
            BLManejadorCuentas man = new BLManejadorCuentas();
            string securepass = FormsAuthentication.HashPasswordForStoringInConfigFile(contraTb.Text.Trim(), "MD5");
            BLCuenta cuenta = man.login(usuarioTb.Text.Trim(), securepass);
            if(cuenta != null) {
                if(cuenta.estado) {
                    Session["cuentaLogin"] = cuenta;
                    Response.Redirect("Principal.aspx");
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

        /// <summary>
        /// Permite a un usuario reestablecer su contraseña de sistema en caso de que la olvide
        /// Envia un correo electronico al usuario con una nueva contraseña autogenerada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnviar_Click(object sender, EventArgs e) {
            BLCuenta cuenta = new BLManejadorCuentas().consultarCuenta(recuperarUsuarioTb.Text.Trim());
            if(cuenta != null) {
                try {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient("mail.amcostaricaverde.site",25);

                    mail.From = new MailAddress("amcrlcuentas@amcostaricaverde.site");
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

                    //SmtpServer.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("amcrlcuentas@amcostaricaverde.site", "aMCRL.03");
                    //smtpClient.EnableSsl = true;

                    smtpClient.Send(mail);


                    //MailMessage msg = new MailMessage();
                    //msg.From = new MailAddress("amcrlcuentas@amcostaricaverde.site");
                    //msg.To.Add(new MailAddress(cuenta.id_usuario));
                    //msg.Subject = "Meeting";
                    //msg.Body = "Body message";

                    //SmtpClient smtp = new SmtpClient("mail.amcostaricaverde.site", 25);
                    //smtp.Credentials = new NetworkCredential("amcrlcuentas@amcostaricaverde.site", "aMCRL.03");
                    //smtp.Send(msg);



                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se ha restaurado la contraseña. Revise su correo electronico<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                } catch(Exception ex) {
                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Error al enviar el correo electrónico. Contacte con su administrador.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
            }

        }

        //private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
        //    if(e.KeyCode == Keys.Enter) {
                
        //    }
        //}

        private void txt_Item_Number_KeyDown2(object sender, KeyEventArgs e) {
            try {
                if(e.KeyCode == Keys.Enter) {
                    BLManejadorCuentas man = new BLManejadorCuentas();
                    string securepass = FormsAuthentication.HashPasswordForStoringInConfigFile(contraTb.Text.Trim(), "MD5");
                    BLCuenta cuenta = man.login(usuarioTb.Text.Trim(), securepass);
                    if(cuenta != null) {
                        if(cuenta.estado) {
                            Session["cuentaLogin"] = cuenta;
                            Response.Redirect("Principal.aspx");
                        } else {
                            lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Su cuenta se encuentra deshabilitada. Contacte con un administrador del sistema.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        }

                    } else {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Credenciales incorrectos.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                }
            } catch(Exception) {

            }
        }
    }
}