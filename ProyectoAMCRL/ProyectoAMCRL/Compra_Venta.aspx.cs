using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class Compra_Venta : System.Web.UI.Page
    {

        List<BLSocioNegocio> socios = new List<BLSocioNegocio>();
        protected void Page_Load(object sender, EventArgs e)
        {
            productos.Items.Add("COBRE");
            productos.Items.Add("LATA");
            productos.Items.Add("HIERRO");
            productos.Items.Add("ALUMINIO");

            unidades.Items.Add("KG");
            unidades.Items.Add("TONELADA");

            monedas.Items.Add("COL");
            monedas.Items.Add("USD");

        }

        protected void Guardar_click(object sender, EventArgs e)
        {
            BLManejadorCompras manejadorCompras = new BLManejadorCompras();
           
            //la.Text = manejadorCompras.guadarDetallesCompraBL();
        }


    }
}