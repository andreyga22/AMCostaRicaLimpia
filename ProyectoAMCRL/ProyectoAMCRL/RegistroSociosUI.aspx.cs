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
            alertar.Visible = false;
            ok_btn.Visible = false;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            BLSocioNegocio socio = new BLSocioNegocio();
            socio.cedula = idTB.Text.ToString();
            socio.nombre = nombreTB.Text.ToString();
            socio.apellido1 = ape1TB.Text.ToString();
            socio.apellido2 = ape2TB.Text.ToString();
            socio.rol = rolRadios.SelectedValue.ToString();

            BLDireccion direccionN = new BLDireccion(provinciaTB.Text.ToString(), cantonTB.Text.ToString(),
                distritoTB.Text.ToString(), sennas.Text.ToString());

            socio.direccion = direccionN;
            socio.tel1 = telTB.Text.ToString();
            socio.tel2 = tel2TB.Text.ToString();
            socio.correo = correoTB.Text.ToString();

            BLManejadorSocios manejador = new BLManejadorSocios();

            alertar.Text = manejador.agregarSocioBL(socio); ;
            alertar.Visible = true;
            ok_btn.Visible = true;

        }
    }
}