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
    public partial class RegistroMateriales : System.Web.UI.Page
    {

        BLManejadorMateriales manejador = new BLManejadorMateriales();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (Request.QueryString.Get("idM") != null) {
                    labelAccion.Text = "Actualización material";
                    escondidillo.Value= Request.QueryString.Get("idM");
                    nombreTB.Text = Request.QueryString.Get("nom");
                    precioKgTB.Text = Request.QueryString.Get("prec");

                    PropertyInfo isreadonly =
                    typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
                    "IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    // make collection editable
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    // remove
                    this.Request.QueryString.Remove("idM");
                    this.Request.QueryString.Remove("nom");
                    this.Request.QueryString.Remove("prec");
                    this.DataBind();
                    isreadonly.SetValue(this.Request.QueryString, true, null);
                    materialLabel.Text = nombreTB.Text;
                    fijarCantidad(escondidillo.Value);
                    btnGuardarActualizar.Text = "Actualizar";
                    
                }
        }

        private void fijarCantidad(String id)
        {
            accionMaterialLabel.Text = "Cantidad vendida";
            cantidadLabel.Text = ""+manejador.traerCantidadVendidaBL(id);
        }

        protected void btnGuardarActualizar_Click(object sender, EventArgs e)
        {
            String nom = nombreTB.Text;
            String precio = precioKgTB.Text;
            String m = "";

            if (btnGuardarActualizar.Text.Equals("Guardar"))
            {
                m = manejador.registrarMaterialBL(nom, precio);

            }
            else {
                int cod = Int32.Parse(escondidillo.Value);
                m = manejador.actualizarMaterialBL( cod, nom, precio);
            }
        }
    }
}