using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

        private static List<BLDetalleAjuste> detalles;

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
                detalles = new List<BLDetalleAjuste>();

                cargarUnidadesBodegas();
                cargarMateriales(bodegasDrop.Items[0].Value);
                datepickerTB.Text = DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;
                labelDatoConsecutivo.Visible = true;
                labelDatoConsecutivoValor.Visible = false;


                if (Request.QueryString.Get("view") != null)
                {
                    labelDatoConsecutivo.Visible = true;
                    labelDatoConsecutivoValor.Visible = true;
                    labelBreadCrumb.Text = "Vista de ajuste";
                    //id, bod : '-'
                    String[] info = Request.QueryString.Get("view").Split('-');
                    String idAjuste = info[0];
                    String bodegaId = info[1];
                    //REMOVER EL PARAMETRO DEL URL
                    borrarParametroURL("view");
                    BLManejadorAjustes manejadorA = new BLManejadorAjustes();
                    BLAjuste ajuste = manejadorA.buscarAjusteBL(idAjuste);
                    if ( ajuste != null)
                    {
                        cargarAjuste(ajuste, bodegaId);
                    }
                    else
                    {
                        lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "Error, por favor contácte con el administrador." + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
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
                    detalles = (List<BLDetalleAjuste>)Session["listaDetalles"];

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

        private void cargarAjuste( BLAjuste ajuste, String idBodega)
        {

            cargarUnidadesBodegas();
            labelInfoLinea.Visible = false;
            materialDD.Visible = false;
            cantidadTB.Visible = false;
            unidadDD.Visible = false;
            agregarLineaBTN.Visible = false;
            btnGuardar.Visible = false;
            labelAgregados.Visible = false;
            agregadosTextLabel.Visible = false;
            Color color = Color.LightYellow;

            detalles = ajuste.detalles;

            datepickerTB.Text = ajuste.traerFecha();
            datepickerTB.Enabled = false;
            datepickerTB.BackColor = color;
            datepickerTB.CssClass = "form-control font-weight-bolder";

            if (ajuste.accion.Equals("ENTRADA"))
                radioAccion.Items[0].Selected = true;
            else
                radioAccion.Items[1].Selected = true;

            radioAccion.Enabled = false;

            labelDatoConsecutivoValor.Text = ajuste.idAjuste.ToString();

            for (int i = 0; i < bodegasDrop.Items.Count; i++) {
                if (bodegasDrop.Items[i].Value.Contains(idBodega))
                {
                    bodegasDrop.Items[i].Selected = true;
                    break;
                }
            }

            bodegasDrop.Enabled = false;
            bodegasDrop.BackColor = color;
            bodegasDrop.CssClass = "btn btn-light btn-sm dropdown-toggle dropup";

            razonTb.Text = ajuste.razon;
            razonTb.Enabled = false;
            razonTb.BackColor = color;
            razonTb.CssClass = "form-control";

            fila0Encabezado1.Visible = false;
            fila0EncabezadoT2.Visible = false;
            fila0Encabezado2.Visible = true;


            foreach (BLDetalleAjuste a in ajuste.detalles) {

                TableCell productoCell = new TableCell();
                TableCell cantidadCell = new TableCell();
                TableCell unidadCell = new TableCell();
                productoCell.Text = a.id_Material;
                cantidadCell.Text = a.kilos_Linea.ToString();
                unidadCell.Text =a.unidadMedida;
                TableRow filaNueva = new TableRow();
                filaNueva.Cells.Add(productoCell);
                filaNueva.Cells.Add(cantidadCell);
                filaNueva.Cells.Add(unidadCell);

                tablaDetalles.Rows.Add(filaNueva);
            }
            tablaDetalles.DataBind();

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

                    BLManejadorAjustes manejadorA = new BLManejadorAjustes();
                    m = manejadorA.registrarAjusteBL(bodegasDrop.SelectedItem.Value, radioAccion.SelectedItem.Value, razonTb.Text, detalles, datepickerTB.Text);

                    if (m.Equals("Ajuste realizado correctamente"))
                    {
                        Session.Remove("listaDetalles");
                        detalles = new List<BLDetalleAjuste>();
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
            if (!cantidadTB.Text.Contains("-") && !(String.IsNullOrEmpty(cantidadTB.Text)) && (cantidadTB.Text.Length <= 10))
            {
                cantidadTB.BorderColor = System.Drawing.Color.Transparent;

                String[] materialInfo = materialDD.SelectedItem.Value.Split('*');
                String idMaterial = materialInfo[0];
                int idStockMaterial = Int32.Parse(materialInfo[2]);
                //String claveNombreM = idMaterial + "&" + materialDD.SelectedItem.Text;
                Double cantidad = Double.Parse(cantidadTB.Text);
                String unidadPesoClave = unidadDD.SelectedItem.Value;

                BLDetalleAjuste lineaA = new BLDetalleAjuste(materialDD.SelectedItem.Value, idStockMaterial, cantidad, unidadPesoClave);
                detalles.Add(lineaA);
                Session.Add("listaDetalles", detalles);
                pegarLineasTabla();
                labelAgregados.Text = detalles.Count().ToString();
                refrescarDatos();
            }
            else {
                pegarLineasTabla();
                cantidadTB.BorderColor = System.Drawing.Color.Red;
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
                BLDetalleAjuste linea = detalles[i];

                TableCell productoCell = new TableCell();
                TableCell cantidadCell = new TableCell();
                TableCell unidadCell = new TableCell();

                TableCell quitarFilaCampo = new TableCell();
                TableRow filaNueva = new TableRow();

                String[] materialInfo = linea.id_Material.Split('*');
                String nombreMaterial = materialInfo[1];

                String[] unidadInfo = linea.unidadMedida.Split('*');
                String nombreUnidad = unidadInfo[2];

                productoCell.Text = nombreMaterial;
                cantidadCell.Text = linea.kilos_Linea.ToString();
                unidadCell.Text = nombreUnidad;

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

        private void cargarMateriales(String idBodega)
        {
            String nombreBodegaSeleccionada = bodegasDrop.SelectedItem.Text;
            manejadorM = new BLManejadorMateriales();
            materialDD.Items.Clear();
            detalles = new List<BLDetalleAjuste>();
            Session.Add("listaDetallesC", detalles);
            pegarLineasTabla();

            DataSet materialesDS = manejadorM.listarMaterialesEnBodegaBL(idBodega);

            if (materialesDS == null || materialesDS.Tables[0].Rows.Count == 0)
            {
                bodegasDrop.BorderColor = System.Drawing.Color.Red;
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "No existen materiales registrados para la bodega " + nombreBodegaSeleccionada + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            else
            {
                bodegasDrop.BorderColor = System.Drawing.Color.Transparent;
                foreach (DataRow dr in materialesDS.Tables[0].Rows)
                {
                    ListItem item = new ListItem();

                    String codigo = Convert.ToString(dr["COD_MATERIAL"]);
                    String nombre = Convert.ToString(dr["NOMBRE_MATERIAL"]);
                    String id_stock = Convert.ToString(dr["ID_STOCK"]);
                    item.Text = nombre;
                    item.Value = codigo + "*" + nombre + "*" + id_stock;
                    materialDD.Items.Add(item);
                }
            }
            materialDD.DataBind();
        }

        private void cargarUnidadesBodegas()
        {
            cargarUnidades();

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

        protected void bodegasDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            String idBodega = bodegasDrop.SelectedItem.Value;
            cargarMateriales(idBodega);
        }

        private void cargarUnidades()
        {

            if (manejadorU == null)
                manejadorU = new BLManejadorUnidades();

            DataSet unidades = manejadorU.listarUnidades();

            foreach (DataRow dr in unidades.Tables[0].Rows)
            {
                // COD_UNIDAD, NOMBRE_UNIDAD 

                String codigo = Convert.ToString(dr["COD_UNIDAD"]);
                String nombre = Convert.ToString(dr["NOMBRE_UNIDAD"]);
                String equiv = Convert.ToString(dr["EQUIVALENCIA_KG"]);
                String infoUnidad = codigo + "*" + equiv + '*' + nombre;

                ListItem item = new ListItem(nombre, infoUnidad);
                unidadDD.Items.Add(item);
            }

        }

        protected void materialDD_SelectedIndexChanged1(object sender, EventArgs e)
        {
            String[] materialInfo = materialDD.SelectedItem.Value.Split('*');
            String codigo = materialInfo[0];

            DataSet materialInfoSet = new DataSet();

            if (manejadorM == null)
                manejadorM = new BLManejadorMateriales();

            materialInfoSet = manejadorM.traerUnidadYprecioBase(codigo, 'c');

            if (materialInfoSet != null)
            {
                String unidadBase = Convert.ToString(materialInfoSet.Tables[0].Rows[0]["COD_UNIDAD"]);

                int index = 0;

                unidadDD.Items.Clear();
                cargarUnidades();

                for (int ind = 0; ind < unidadDD.Items.Count; ind++)
                {
                    ListItem item = unidadDD.Items[ind];
                    unidadDD.Items[ind].Selected = false;
                    String[] infoCod = item.Value.Split('*');
                    String cod = infoCod[0];

                    if (cod.Equals(unidadBase))
                    {

                        index = ind;
                        break;
                    }
                }

                unidadDD.Items[index].Selected = true;
                unidadDD.DataBind();

            }
            pegarLineasTabla();
            String nombre = materialInfo[1];
        }
    }
}