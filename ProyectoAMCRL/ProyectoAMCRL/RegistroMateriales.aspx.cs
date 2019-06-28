using System;
using System.Collections.Generic;
using System.Data;
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

        BLManejadorMateriales manejadorM = new BLManejadorMateriales();
        BLManejadorUnidades manejadorU = new BLManejadorUnidades();
        BLManejadorBodega manejadorB = new BLManejadorBodega();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                btnGuardarActualizar.Attributes.Add("onclick", "document.body.style.cursor = 'wait';");
                cargarUnidadesBodegas();
                //cantidadTB.CssClass = "btn form-control";
                if (Request.QueryString.Get("idM") != null)
                {
                    labelAccion.Text = "Actualización de material";
                    String codMaterial = Request.QueryString.Get("idM");
                    BLMaterial material = manejadorM.buscarMaterial(codMaterial);
                    cargarMaterialAPantalla(material);
                    PropertyInfo isreadonly =
                    typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
                    "IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    // make collection editable
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    // remove
                    this.Request.QueryString.Remove("idM");
                    this.DataBind();
                    isreadonly.SetValue(this.Request.QueryString, true, null);
                    btnGuardarActualizar.Text = "Actualizar";
                    breadLabel.Text = "Actualización";
                }
                

            }
        }

        private void cargarMaterialAPantalla(BLMaterial material) {
            if (material != null)
            {
                nombreTB.Text = material.nombreMaterial;
                precioKgTB.Text = material.precioKilo.ToString();
                codigoMTB.Text= material.codigoM.ToString();
                seleccionarUnidadMaterial(material.unidadBase.codigo);
                codigoMTB.Width = nombreTB.Width;
                codigoMTB.CssClass = "form-control";
                codigoMTB.BackColor = System.Drawing.Color.LightYellow;
                codigoMTB.Enabled = false;
            }
            else {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "Material no encontrado" + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
           
        }

        private void seleccionarUnidadMaterial(String codUnidad) {
            foreach (ListItem unidad in unidadDD.Items) {
                if (unidad.Value.Equals(codUnidad))
                    unidad.Selected = true;
            }
        }

        protected void btnGuardarActualizar_Click(object sender, EventArgs e)
        {
            String cod = codigoMTB.Text;
            String nom = nombreTB.Text;
            String precio = precioKgTB.Text;
            String codUnidad = unidadDD.SelectedItem.Value;

            String m = "";
            char tipo = labelAccion.Text.Equals("Actualización de material") ? 'a': 'r';

            m = manejadorM.registrarActualizarMaterialBL(cod, nom, precio, codUnidad, tipo);

            if (m.Contains("correctamente")){

                lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>"+ m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            else {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
                
            }

        }


        private void cargarUnidadesBodegas()
        {
            DataSet unidades = manejadorU.listarUnidades();

            foreach (DataRow dr in unidades.Tables[0].Rows)
            {
                // COD_UNIDAD, NOMBRE_UNIDAD 
                String codigo = Convert.ToString(dr["COD_UNIDAD"]);
                String nombre = Convert.ToString(dr["NOMBRE_UNIDAD"]);
                String equiv = Convert.ToString(dr["EQUIVALENCIA_KG"]);

                ListItem item = new ListItem(nombre, codigo);
                unidadDD.Items.Add(item);
            }
        }
    }
}