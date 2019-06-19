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
    public partial class DetalleAjuste : System.Web.UI.Page
    {

        private static List<String> detalles;

        BLManejadorMateriales manejadorM;
        BLManejadorUnidades manejadorU;
        BLManejadorBodega manejadorB;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) //no existe la lista en sesion
            {           
                manejadorM = new BLManejadorMateriales();
                manejadorU = new BLManejadorUnidades();
                manejadorB = new BLManejadorBodega();

                //agregarLineaBTN EL ESTILO DE ESPERA DE RELOJ AL CURSOR
                btnGuardar.Attributes.Add("onclick", "document.body.style.cursor = 'wait';");

                Session.Remove("listaDetalles");//debe eliminar la lista de la sesion en caso de que ya estuviera instanciada
                detalles = new List<string>();

                cargarUnidadesBodegas();
                cargarMateriales();
                datepicker.Value = DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;

                if (Request.QueryString.Get("awf") != null)
                {
                    //String accion = Request.QueryString.Get("awf");
                    //if (accion.Equals("new")) {
                    //    Session.Remove("listaDetalles");
                    //    detalles = new List<string>();
                    //    borrarParametroURL("awf");
                    //}
                    //else
                    //{
                        String idAjuste = Request.QueryString.Get("vistaA");
                        //REMOVER EL PARAMETRO DEL URL
                        borrarParametroURL("vistaA");
                        BLManejadorAjustes manejadorA = new BLManejadorAjustes();
                        String ajusteInfo = manejadorA.buscarAjusteBL(idAjuste);
                        if (!ajusteInfo.Equals("No encontrado") && !ajusteInfo.Equals("Error"))
                            cargarAjuste(ajusteInfo);
                        else
                        {
                            lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + ajusteInfo + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        }
                    
                }
            }
            else 
            {
                if (Session["listaDetalles"] == null)  //primer postback, (primer material añadido)
                    Session.Add("listaDetalles", detalles);
                else
                {//ya existe la lista
                    detalles = (List<String>)Session["listaDetalles"];

                    if (Request.QueryString.Get("del") != null)
                    {
                        //SE DEBE REMOVER UN ELEMENTO DE LA LISTA
                        int indiceAremover = Int32.Parse(Request.QueryString.Get("del"));
                        removerDetalle(indiceAremover);

                        //REMOVER EL PARAMETRO DEL URL
                        borrarParametroURL("del");
                    }
                }
                lblError.Visible = false;
            }
        }

        private void cargarAjuste( String info)
        {
            //fecha, movimiento, peso, nombreMaterial, nombreBodega, razon
            String[] infoArray = info.Split('_');
            //labelFecha.Text = infoArray[0];
            //labelTipo.Text = infoArray[1];
            //labelCantidad.Text = infoArray[2];
            //labelMaterial.Text = infoArray[3];
            //labelBodega.Text = infoArray[4];
            razonTb.Text= infoArray[5];
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if (detalles.Count == 0)
            {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Por favor, agregue al menos una línea de ajuste</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            else
            {
                if (radioAccion.SelectedItem != null)
                {
                    String m = "";
                    //id_bod, peso, material, unidad, accion, razon
                    String[] materialInfo = materialDD.SelectedItem.Value.Split('-');
                    String idMaterial = materialInfo[0];
                    String idStockMaterial = materialInfo[1];

                    BLManejadorAjustes manejadorA = new BLManejadorAjustes();
                    m = manejadorA.registrarAjusteBL(bodegasDrop.SelectedItem.Value, idMaterial, idStockMaterial, cantidadTB.Text, unidadDD.SelectedItem.Value, radioAccion.SelectedItem.Value, razonTb.Text);

                    if (m.Equals("Operación efectuada correctamente"))
                    {
                        Session.Remove("listaDetalles");
                        detalles = new List<string>();
                        lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                    else
                    {
                        lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                        pegarLineasTabla();
                    }
                }
                else
                {
                    lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Por favor especifique el tipo de ajuste</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                    pegarLineasTabla();
                }
            }
        }

        protected void agregarLineaClick(object sender, EventArgs e)
        {
            if (!cantidadTB.Text.Contains("-") && !(String.IsNullOrEmpty(cantidadTB.Text)) )
            {
                String lineaAjusteInfo = "";
                lineaAjusteInfo = materialDD.SelectedItem.Text + "&" +
                    cantidadTB.Text + "&" + unidadDD.SelectedItem.Text;
                detalles.Add(lineaAjusteInfo);
                Session.Add("listaDetalles", detalles);
                pegarLineasTabla();
                labelAgregados.Text = detalles.Count().ToString();
            }
            else {
                pegarLineasTabla();
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Cantidad especificada incorrecta, intente de nuevo</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }    
        }

        private void removerDetalle(int index)
        {
            if (detalles.Count > 0)
                detalles.RemoveAt(index);
            Session.Add("listaDetalles", detalles);
            pegarLineasTabla();
            labelAgregados.Text = detalles.Count().ToString();
        }

        private void pegarLineasTabla()
        {

            for (int i = 0; i < detalles.Count; i++) {
                String linea = detalles[i];

                String[] lineaInfo = linea.Split('&');
                TableCell productoCell = new TableCell();
                TableCell cantidadCell = new TableCell();

                TableCell unidadCell = new TableCell();

                TableCell quitarFilaCampo = new TableCell();
                TableRow filaNueva = new TableRow();

                productoCell.Text = lineaInfo[0];
                cantidadCell.Text = lineaInfo[1];
                unidadCell.Text = lineaInfo[2];

                LinkButton btn = new LinkButton();
                btn.PostBackUrl = "DetalleAjuste.aspx?del="+(i);
                
                btn.Text = "Borrar";

                quitarFilaCampo.Controls.Add(btn);
                quitarFilaCampo.ForeColor = System.Drawing.Color.Red;

                filaNueva.Cells.Add(productoCell);
                filaNueva.Cells.Add(cantidadCell);
                filaNueva.Cells.Add(unidadCell);
                filaNueva.Cells.Add(quitarFilaCampo);
                tablaDetalles.Rows.Add(filaNueva);
            }
            tablaDetalles.DataBind();
        }

        private void refrescarDatos()
        {
            materialDD.SelectedIndex = 0;
            cantidadTB.Text = "";
            unidadDD.SelectedIndex = 1;
            razonTb.Text = "";
        }

        private void cargarMateriales()
        {
            // se debe tomar el valor el sesion de la bodega
            DataSet materialesDS = manejadorM.listarMaterialesEnBodegaBL("B01"); // <--------

            if (materialesDS.Tables[0].Rows.Count == 0)
            {

                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "No existen materiales registrados para la bodega actual" + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            else
            {
                //stock_id_escondido.Value = Convert.ToString(materialesDS.Tables[0].Rows[0]["ID_STOCK"]);

                foreach (DataRow dr in materialesDS.Tables[0].Rows)
                {
                    ListItem item = new ListItem();

                    String codigo = Convert.ToString(dr["COD_MATERIAL"]);
                    String nombre = Convert.ToString(dr["NOMBRE_MATERIAL"]);
                    String id_stock = Convert.ToString(dr["ID_STOCK"]);
                    item.Text = nombre;
                    item.Value = codigo + "-" + id_stock;
                    materialDD.Items.Add(item);
                }
            }
        }

        private void cargarUnidadesBodegas()
        {
            List<BLUnidad> unidades = manejadorU.unidades;

            foreach (BLUnidad u in unidades)
            {
                ListItem item = new ListItem(u.nombre, u.equivalencia.ToString());
                unidadDD.Items.Add(item);
            }

            List<BLBodegaTabla> bodegas = manejadorB.listaBodegas();
            foreach (BLBodegaTabla b in bodegas)
            {

                ListItem item = new ListItem(b.nombre, b.codigo);
                bodegasDrop.Items.Add(item);
            }
        }


        //ELIMINA EL ULTIMO PARAMETRO PARA ELIMINAR (del) AGREGADO A LA URL 
        private void borrarParametroURL(String id)
        {
            PropertyInfo isreadonly =
                       typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
                       "IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            // make collection editable
            isreadonly.SetValue(this.Request.QueryString, false, null);
            // remove
            this.Request.QueryString.Remove(id);
            this.DataBind();
            //dejar no editable
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

    }
}