using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BL;

namespace ProyectoAMCRL
{
    public partial class Materiales : System.Web.UI.Page
    {
        BLManejadorMateriales manejador = new BLManejadorMateriales();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            
            
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
    }
}