using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoAMCRL
{
    public partial class Reporte_General : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> Esta funcionalidad aún no se encuentra completa para esta versión, lamentamos el inconveniente. <button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
            lblError.Visible = true;
        }
    }
}