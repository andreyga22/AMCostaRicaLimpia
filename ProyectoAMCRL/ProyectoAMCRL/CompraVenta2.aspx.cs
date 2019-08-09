using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoAMCRL
{
    public partial class CompraVenta2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cuentaLogin"] != null)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        
                    }
                    catch (Exception)
                    {
                        lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se pudo cargar los datos de bodega. Revise su conexión a internet.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}