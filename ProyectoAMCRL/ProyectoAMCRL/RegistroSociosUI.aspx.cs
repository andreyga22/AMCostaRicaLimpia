using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class RegistroSociosUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e) {
            //BLManejadorSocios manejador = new BLManejadorSocios();
            //BLSocioNegocio socio = new BLSocioNegocio();
            //try {

            //    socio.cedula = idTB.Text.ToString();
            //    socio.nombre = nombreTB.Text.ToString();
            //    socio.apellido1 = ape1TB.Text.ToString();
            //    socio.apellido2 = ape2TB.Text.ToString();
            //    socio.rol = rolRadios.SelectedValue.ToString();

            //    BLDireccion direccionN = new BLDireccion(provinciaTB.Text.ToString(), cantonTB.Text.ToString(),
            //        distritoTB.Text.ToString(), sennas.Text.ToString(), 0);

            //    socio.direccion = direccionN;
            //    socio.telHab = int.Parse(telTB.Text.ToString());
            //    socio.telPers = int.Parse(tel2TB.Text.ToString());
            //    socio.correo = correoTB.Text.ToString();



            //} catch (Exception ex) {
            //    lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + ex.ToString() + "</strong> Por favor intentelo de nuevo.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
            //}

            //string message = manejador.agregarSocioBL(socio);

            //if (message.Equals("listo")) {
            //    lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong> Guardado con éxito.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
            //} else {
            //    lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + message + "</strong> Por favor intentelo de nuevo.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
            //}
            //lblError.Visible = true;
            //}
        }
    }
}