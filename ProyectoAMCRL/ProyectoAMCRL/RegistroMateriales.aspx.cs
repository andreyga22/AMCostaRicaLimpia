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
            try
            {
                string id = (String)Session["idMaterial"];
                if (!string.IsNullOrEmpty(id))
                {
                    //try
                    //{
                    BLCuenta cuenta = (BLCuenta)Session["cuentaLogin"];
                    BLMaterial miMat = consultarMaterialAdmin(id);
                    String estado = estadoRb.SelectedValue;
                    Boolean estadoB = true;
                    if (estado.Equals("Activado"))
                    {
                        estadoB = true;
                    }
                    else
                    {
                        estadoB = false;
                    }
                    BLMaterial material = new BLMaterial(codigoMTB.Text.Trim(), nombreTB.Text.Trim(), Convert.ToDouble(precioKgV.Text.Trim()), unidadDD.Text.Trim(), Convert.ToDouble(precioKgC.Text.Trim()), estadoB);
                    BLManejadorMateriales man = new BLManejadorMateriales();
                    man.guardarModificarBodegaAdmin(material);

                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se modificó el material correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;

                    //}
                    //catch (Exception exx)
                    //{
                    //    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    //    lblError.Visible = true;
                    //}
                }
                else
                {
                    try
                    {
                        BLCuenta cuenta = (BLCuenta)Session["cuentaLogin"];

                        String estado = estadoRb.SelectedValue;
                        Boolean estadoB = true;
                        if (estado.Equals("Activado"))
                        {
                            estadoB = true;
                        }
                        else
                        {
                            estadoB = false;
                        }
                        BLMaterial material = new BLMaterial(codigoMTB.Text.Trim(), nombreTB.Text.Trim(), Convert.ToDouble(precioKgV.Text.Trim()), unidadDD.Text.Trim(), Convert.ToDouble(precioKgC.Text.Trim()), estadoB);
                        BLManejadorMateriales man = new BLManejadorMateriales();
                        man.guardarModificarBodegaAdmin(material);
                        lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó el material correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                    catch (Exception exx)
                    {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception exx)
            {
                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
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