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
        protected void Page_Load(object sender, EventArgs e)
        {



        }

        protected void Guardar_click(object sender, EventArgs e)
        {
            BLManejadorCompras manejadorCompras = new BLManejadorCompras();
           
            la.Text = manejadorCompras.guadarDetallesCompraBL();
        }


    }
}