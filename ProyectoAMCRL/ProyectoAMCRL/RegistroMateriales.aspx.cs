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
        BLManejadorMateriales manejador = new BLManejadorMateriales();

        /// <summary>
        /// Este método permite al usuario guardar o actualizar el contenido de la página Material en el sistema.
        ///Contiene el evento de clic en el botón guardar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cuentaLogin"] != null)
            {
                if (!IsPostBack)
                {
                    //try
                    //{
                    string id = (String)Session["idMaterial"];
                    if (!string.IsNullOrEmpty(id))
                    {
                        labelAccion.Text = "Actualización de material";
                        BLCuenta cuenta = (BLCuenta)Session["cuentaLogin"];

                        BLMaterial miMat = consultarMaterialAdmin(id);
                        codigoMTB.Text = miMat.codigoM;
                        codigoMTB.Enabled = false;
                        nombreTB.Text = miMat.nombreMaterial;
                        Boolean ess = miMat.estado_Material;
                        int est = 0;
                        if (ess)
                        {
                            est = 0;
                        }
                        else
                        {
                            est = 1;
                        }
                        estadoRb.SelectedIndex = est;
                        precioKgC.Text = miMat.precioCompraK + "";
                        precioKgV.Text = miMat.precioVentaK + "";
                        cargarUnidadesBodegas();
                        unidadDD.SelectedValue = miMat.cod_Unidad;
                    }
                    else
                    {
                        cargarUnidadesBodegas();
                    }
                    //}
                    //catch (Exception exx)
                    //{
                    //    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    //    lblError.Visible = true;
                    //}
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        private BLMaterial consultarMaterialAdmin(String id)
        {
            //try
            //{
            BLManejadorMateriales man = new BLManejadorMateriales();
            return man.consultarMaterialAdmin(id);
            //}
            //catch (Exception)
            //{
            //    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se puede consultar la bodega<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
            //    lblError.Visible = true;
            //    return null;
            //}
        }










        //private void cargarMaterialAPantalla(BLMaterial material)
        //{
        //    if (material != null)
        //    {
        //        nombreTB.Text = material.nombreMaterial;
        //        precioKgTB.Text = material.precioKilo.ToString();
        //        codigoMTB.Text = material.codigoM.ToString();
        //        seleccionarUnidadMaterial(material.unidadBase.codigo);
        //        codigoMTB.Width = nombreTB.Width;
        //        codigoMTB.CssClass = "form-control";
        //        codigoMTB.BackColor = System.Drawing.Color.LightYellow;
        //        codigoMTB.Enabled = false;
        //    }
        //    else
        //    {
        //        lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "Material no encontrado" + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
        //        lblError.Visible = true;
        //    }

        //}

        private void seleccionarUnidadMaterial(String codUnidad)
        {
            foreach (ListItem unidad in unidadDD.Items)
            {
                if (unidad.Value.Equals(codUnidad))
                    unidad.Selected = true;
            }
        }

        protected void btnGuardarActualizar_Click(object sender, EventArgs e)
        {
            String cod = codigoMTB.Text;
            String nom = nombreTB.Text;
            String precioC = precioKgC.Text;
            String precioV = precioKgV.Text;
            String codUnidad = unidadDD.SelectedItem.Value;

            String m = "";
            char tipo = labelAccion.Text.Equals("Actualización de material") ? 'a' : 'r';
            Boolean estado = (estadoRb.Items[0].Selected == true) ? true:false;

            m = manejador.registrarActualizarMaterialBL(cod, nom, precioC, precioV, codUnidad, tipo, estado);

            if (m.Contains("correctamente"))
            {

                lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            else
            {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;

            }
        }


        private void cargarUnidadesBodegas()
        {
            DataSet unidades = new BLManejadorUnidades().listarUnidades();

            foreach (DataRow dr in unidades.Tables[0].Rows)
            {
                // COD_UNIDAD, NOMBRE_UNIDAD 
                String codigo = Convert.ToString(dr["COD_UNIDAD"]);
                String nombre = Convert.ToString(dr["NOMBRE_UNIDAD"]);
                String equiv = Convert.ToString(dr["EQUIVALENCIA_KG"]);
                String est = Convert.ToString(dr["ESTADO"]);

                ListItem item = new ListItem(nombre, codigo);
                unidadDD.Items.Add(item);
            }
        }
    }
}