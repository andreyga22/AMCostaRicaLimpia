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


            try {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("andreyga22@gmail.com");
                mail.To.Add("freddy.gonzalez@ucrso.info");
                mail.Subject = "Prueba de correo";
                mail.Body = "Prueba de body";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("andreyga22@gmail.com", "p1Su#UQHwL8jXDKJ");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("mail Send");
            } catch(Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}