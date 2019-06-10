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

        BLManejadorMateriales manejadorM = new BLManejadorMateriales();
        BLManejadorUnidades manejadorU = new BLManejadorUnidades();
        BLManejadorBodega manejadorB = new BLManejadorBodega();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                btnGuardarActualizar.Attributes.Add("onclick", "document.body.style.cursor = 'wait';");
                cargarUnidadesBodegas();
                cantidadTB.CssClass = "btn form-control";
                if (Request.QueryString.Get("idM") != null)
                {
                    deshabilitarSeccionStock("Esta sección está habilitada solo para registrar materiales");
                    labelAccion.Text = "Actualización material";
                    escondidillo.Value = Request.QueryString.Get("idM");
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
                    breadLabel.Text = "Actualización";

                }
                else {
                    deshabilitarSeccionStock("Por favor complete el registro del material para habilitar este campo.");
                }

            }
        }

        private void fijarCantidad(String id)
        {
            accionMaterialLabel.Text = "Cantidad vendida";
            cantidadLabel.Text = ""+manejadorM.traerCantidadVendidaBL(id);
        }

        protected void btnGuardarActualizar_Click(object sender, EventArgs e)
        {
            String cod = escondidillo.Value;
            String nom = nombreTB.Text;
            String precio = precioKgTB.Text;
            String m = "";

            m = manejadorM.registrarActualizarMaterialBL(cod, nom, precio);

            if (m.Equals("Operación efectuada correctamente")){

                lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>"+ m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
                if (escondidillo.Value == "") //registro de nuevo material
                habilitarSeccionStock();
            }
            else {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
                
            }

        }

        private void habilitarSeccionStock() {
            dropDownBodegas.Enabled = true;
            dropDownBodegas.BackColor = System.Drawing.Color.WhiteSmoke;
            cantidadTB.Enabled = true;
            cantidadTB.BackColor = System.Drawing.Color.WhiteSmoke;
            dropDownUnidades.Enabled = true;
            dropDownUnidades.BackColor = System.Drawing.Color.WhiteSmoke;
            btnEnlazarStock.Enabled = true;
            btnEnlazarStock.Visible = true;
            dropDownBodegas.ToolTip = "Bodega donde va a ubicar el material";
            cantidadTB.ToolTip = "Cantidad a registrar a inventario";
            dropDownUnidades.ToolTip = "Unidad que se registra(Kg, Tonelada, etc)";
        }

        private void deshabilitarSeccionStock(String msg)
        {
            
            dropDownBodegas.Enabled = false;
            dropDownBodegas.BackColor = System.Drawing.Color.Red;
            cantidadTB.Enabled = false;
            cantidadTB.BackColor = System.Drawing.Color.Red;
            dropDownUnidades.Enabled = false;
            dropDownUnidades.BackColor = System.Drawing.Color.Red;
            btnEnlazarStock.Enabled = false;
            btnEnlazarStock.Visible = false;
            dropDownBodegas.ToolTip = msg;
            cantidadTB.ToolTip = msg;
            dropDownUnidades.ToolTip = msg;
        }

        protected void dropDownBodegas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void cargarUnidadesBodegas()
        {
            List<BLUnidad> unidades = manejadorU.unidades;

            foreach (BLUnidad u in unidades)
            {
                ListItem item = new ListItem(u.nombre, u.equivalencia.ToString());
                dropDownUnidades.Items.Add(item);
            }

            List<BLBodegaTabla> bodegas = manejadorB.listaBodegas();
            foreach (BLBodegaTabla b in bodegas)
            {

                ListItem item = new ListItem(b.nombre, b.codigo);
                dropDownBodegas.Items.Add(item);
            }
        }
    }
}