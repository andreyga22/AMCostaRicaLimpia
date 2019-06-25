using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ProyectoAMCRL
{
    public partial class Principal : System.Web.UI.Page
    {


        /*
            Revisa si hay un usuario en sesión para permitir o negar la carga 
            de la página. En caso de negarlo vuelve al login.
             */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cuentaLogin"] != null)
            {
                if (!this.IsPostBack)
                {
                    Session["idFactura"] = "";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

    }
}