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

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            BLManejadorSocios manejador = new BLManejadorSocios();
            BLSocioNegocio socio = new BLSocioNegocio();

                socio.cedula = idTB.Text.ToString();
                socio.nombre = nombreTB.Text.ToString();
                socio.apellido1 = ape1TB.Text.ToString();
                socio.apellido2 = ape2TB.Text.ToString();
                socio.rol = rolRadios.SelectedValue.ToString();

                socio.direccion = new BLDireccion(provinciaTB.Text.ToString(), cantonTB.Text.ToString(),
                distritoTB.Text.ToString(), sennas.Text.ToString(), 0);
                socio.contactos = new BLContactos(int.Parse(telTB.Text.ToString()),
                int.Parse(tel2TB.Text.ToString()), correoTB.Text.ToString());
            if (activaCb.Checked)
            {
                socio.estado_socio = true;
            }
            else {
                socio.estado_socio = false;
            }
                Boolean insertado = manejador.agregarSocioBL(socio);
  

        }
    }
}
