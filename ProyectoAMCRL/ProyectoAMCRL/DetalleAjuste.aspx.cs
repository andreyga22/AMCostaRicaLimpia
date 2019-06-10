using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class DetalleAjuste : System.Web.UI.Page
    {

        BLManejadorAjustes manejadorA = new BLManejadorAjustes();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString.Get("awf") != null)
                {
                    String idAjuste = Request.QueryString.Get("awf");

                    String ajusteInfo = manejadorA.buscarAjusteBL(idAjuste);
                    if (!ajusteInfo.Equals("No encontrado") && !ajusteInfo.Equals("Error"))
                        llenarPantalla(ajusteInfo);
                    else {
                        lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + ajusteInfo + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }

                    PropertyInfo isreadonly =
                    typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
                    "IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    // make collection editable
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    // remove
                    this.Request.QueryString.Remove("awf");
                    this.DataBind();
                    isreadonly.SetValue(this.Request.QueryString, true, null);
                }
            } 
        }

        private void llenarPantalla( String info)
        {
            //fecha, movimiento, peso, nombreMaterial, nombreBodega, razon
            String[] infoArray = info.Split('_');
            labelFecha.Text = infoArray[0];
            labelTipo.Text = infoArray[1];
            labelCantidad.Text = infoArray[2];
            labelMaterial.Text = infoArray[3];
            labelBodega.Text = infoArray[4];
            razonTb.Text= infoArray[5];

        }
    }
}