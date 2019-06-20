using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using System.IO;
using System.Data;

namespace ProyectoAMCRL
{
    public partial class Compra_Venta : System.Web.UI.Page
    {

        private static List<String> detalles;

        BLManejadorMateriales manejadorM;
        BLManejadorUnidades manejadorU;
        BLManejadorBodega manejadorB;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) 
            {
                String modo = (String)Session["modo"];
                cargarPantalla(modo);

                //Se remueve la lista de detalles de la sesion en caso de que ya estuviera instanciada.
                Session.Remove("listaDetallesC");
                detalles = new List<string>();
                manejadorU = new BLManejadorUnidades();
                manejadorB = new BLManejadorBodega();
               
                //Estilo de espera para cuando se realiza la compra
                btnGuardar.Attributes.Add("onclick", "document.body.style.cursor = 'wait';");

                // Se cargan las unidades, bodegas, monedas y materiales existentes 
                // (La seleccion de bodega debe especificar los materiales disponibles)
                cargarUnidadesBodegasMonedas();
                cargarMateriales(bodegasDrop.Items[0].Value);
                datepickerT.Text = DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;

                if (Request.QueryString.Get("vd") != null)//vista detalle === vd
                {
                    int idFactura = Int32.Parse(Request.QueryString.Get("vd"));
                    //REMOVER EL PARAMETRO DEL URL
                    borrarParametroURL("vd");
                    //Buscar factura
                    BLManejadorFacturas manejadorF = new BLManejadorFacturas();
                    BLFactura factura = manejadorF.buscarVentaID(idFactura);

                    if (factura != null)
                        cargarFactura(factura);
                    else
                    {
                        lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "Factura no encontrada" + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                }
            }
            else
            {
                if (Session["listaDetallesC"] == null)  //primer postback, (primer material añadido)
                    Session.Add("listaDetallesC", detalles);
                else
                {//ya existe la lista
                    detalles = (List<String>)Session["listaDetallesC"];

                    if (Request.QueryString.Get("del") != null)//Se agrega el parametro [del] cuando se va a eliminar una linea
                    {
                        String valorDesecriptado = BLManejadorEncripcion.Decrypt(Request.QueryString.Get("del"));
                        //se obtiene el indice del parametro en la url
                        int indiceAremover = Int32.Parse(valorDesecriptado);
                        removerDetalle(indiceAremover);

                        //REMOVER EL PARAMETRO DEL URL
                        borrarParametroURL("del");
                    }
                }
                lblError.Visible = false;
            }
        }

        /*
         Se setea el texto de los labels de la pantalla 
         segun la operacion que se va a realizar (compra/venta)
        */
        private void cargarPantalla(String modo) {

            String textoBreadCrum = "Registrar Compra";
            String textoDatoSocio = "Datos del proveedor";
            String textoDatoConsecutivo = "Compra #";

            if (modo.Equals("venta")) {
                textoBreadCrum = "Registrar Venta";
                textoDatoSocio = "Datos del cliente";
                textoDatoConsecutivo = "Venta #";
            }
            labelBreadCrum.Text = textoBreadCrum;
            labelDatosSocio.Text = textoDatoSocio;
            labelDatoConsecutivo.Text = textoDatoConsecutivo;
        }


        private void cargarFactura(BLFactura factura)
        {
            //labelFecha.Text = infoArray[0];
            //labelTipo.Text = infoArray[1];
            //labelCantidad.Text = infoArray[2];
            //labelMaterial.Text = infoArray[3];
            //labelBodega.Text = infoArray[4];

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if (detalles.Count == 0)//no se han agregado detalles
            {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>No se han especificado detalles para la transacción, por favor intente de nuevo.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            else
            {

                String m = "";
                String idBodega = bodegasDrop.SelectedItem.Value;
                String moneda = monedasDD.SelectedItem.Value;


                BLManejadorFacturas manejadorF = new BLManejadorFacturas();//strategy(?)
                String modo = (String)Session["modo"];
                String mensajeRespuesta = "";

                switch (modo)
                {
                    case "compra":
                        mensajeRespuesta = "Compra registrada con éxito";
                        m = manejadorF.registrarCompraBL(identificacionTB.Text, idBodega, moneda, datepickerT.Text, detalles);
                        break;

                    case "venta":
                        mensajeRespuesta = "Venta registrada con éxito";
                        m = manejadorF.registrarVentaBL(identificacionTB.Text, idBodega, moneda, datepickerT.Text, detalles);
                        break;
                }

                if (m.Equals("SUCCCES"))
                {
                    Session.Remove("listaDetallesC");
                    detalles = new List<string>();
                    labelAgregados.Text = "0";
                    lblError.Text = "<br /><br /><div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\"> <strong>" + mensajeRespuesta + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
                else
                {
                    lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                    pegarLineasTabla();
                }
            }
        }

        protected void agregarLineaClick(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(cantidad2TB.Value)))
            {
                String lineaAjusteInfo = "";
                cantidad2TB.Style.Add("background-color", "white"); 
                lineaAjusteInfo = materialDD.SelectedItem.Value+'#'+materialDD.SelectedItem.Text + "&" +
                precioKg2TB.Value + "&" + cantidad2TB.Value  + "&" + unidadDD.SelectedItem.Value + '#' + unidadDD.SelectedItem.Text;
                detalles.Add(lineaAjusteInfo);
                Session.Add("listaDetallesC", detalles);
                pegarLineasTabla();
                labelAgregados.Text = detalles.Count().ToString();
                refrescarDatos();
            }
            else
            {
                
                pegarLineasTabla();
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Cantidad especificada incorrecta, intente de nuevo</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        /*
         Se remueve el detalle de la lista, y se actualiza la lista en la sesion
        */
        private void removerDetalle(int index)
        {
            if (detalles.Count > 0)
                detalles.RemoveAt(index);
            Session.Add("listaDetallesC", detalles);
            pegarLineasTabla();
            //labelAgregados.Text = detalles.Count().ToString();
        }

        /*
         Se agregan las lineas a la pantalla segun los detalles en la lista
             */
        private void pegarLineasTabla()
        {
            for (int i = 0; i < detalles.Count; i++)
            {
                String linea = detalles[i];

                String[] lineaInfo = linea.Split('&');
                TableCell productoCell = new TableCell();
                TableCell precioKg = new TableCell();
                TableCell cantidadCell = new TableCell();

                TableCell unidadCell = new TableCell();

                TableCell quitarFilaCampo = new TableCell();
                TableRow filaNueva = new TableRow();

                String[] materialInfo = lineaInfo[0].Split('#');
                productoCell.Text = materialInfo[1];
                precioKg.Text = lineaInfo[1];
                cantidadCell.Text = lineaInfo[2];
                String[] unidadInfo = lineaInfo[3].Split('#');
                unidadCell.Text = unidadInfo[1];

                LinkButton btn = new LinkButton();
                String valorEncriptado = BLManejadorEncripcion.Encrypt(i.ToString());
                btn.PostBackUrl = "Compra_Venta.aspx?del=" + (valorEncriptado);

                btn.Text = "Borrar";

                quitarFilaCampo.Controls.Add(btn);
                quitarFilaCampo.ForeColor = System.Drawing.Color.Red;

                filaNueva.Cells.Add(productoCell);
                filaNueva.Cells.Add(precioKg);
                filaNueva.Cells.Add(cantidadCell);
                filaNueva.Cells.Add(unidadCell);
                filaNueva.Cells.Add(quitarFilaCampo);
                tablaDetalles.Rows.Add(filaNueva);
            }
            labelAgregados.Text = detalles.Count.ToString();
            BLManejadorFacturas manejadorF = new BLManejadorFacturas();//strategy(?)
            totalLabel.Text = manejadorF.calcularTotalActual(detalles).ToString();
            tablaDetalles.DataBind();
        }

        /*
         Se borra el texto de la seccion para agreagar detalles
         */
        private void refrescarDatos()
        {
            materialDD.SelectedIndex = 0;
            precioKg2TB.Value = "";
            cantidad2TB.Value = "";
            unidadDD.SelectedIndex = 1;
        }

        private void cargarMateriales(String idBodega)
        {
            
            String nombreBodegaSeleccionada = bodegasDrop.SelectedItem.Text;
            manejadorM = new BLManejadorMateriales();
            materialDD.Items.Clear();
            detalles = new List<string>();
            Session.Add("listaDetallesC", detalles);
            pegarLineasTabla();

            DataSet materialesDS = manejadorM.listarMaterialesEnBodegaBL(idBodega);

            if (materialesDS == null ||  materialesDS.Tables[0].Rows.Count == 0)
            {
                bodegasDrop.BorderColor = System.Drawing.Color.Red;
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "No existen materiales registrados para la bodega "+ nombreBodegaSeleccionada + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
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
                    item.Value = codigo + "-" + id_stock;
                    materialDD.Items.Add(item);
                }
            }
            materialDD.DataBind();
        }

        private void cargarUnidadesBodegasMonedas()
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

            BLManejadorMoneda manejadorM = new BLManejadorMoneda();

            DataSet listaMonedas = manejadorM.listarMonedas();
            foreach (DataRow dr in listaMonedas.Tables[0].Rows)
            {
                ListItem moneda = new ListItem();
                moneda.Text = Convert.ToString(dr["DETALLE_MONEDA"]);
                moneda.Value = (Convert.ToString(dr["ID_MONEDA"]))+"#"+(Convert.ToString(dr["EQUIVALENCIA_COLON"]));
                monedasDD.Items.Add(moneda);

            }
        }


        //ELIMINA parametros AGREGADOs A LA URL 
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

        protected void buscarSocioBTN_Click(object sender, EventArgs e)
        {
            String id = identificacionTB.Text;
            if (!String.IsNullOrEmpty(id))
            {
                BLManejadorSocios manejadorS = new BLManejadorSocios();
                BLSocioNegocio socio = manejadorS.buscarSocio(id);
                if (socio != null)
                {
                    nombreLabel.Text = socio.nombre;

                }
                else {
                    lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "Socio no encontrado, intente de nuevo. " + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
                
            }
            else {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "Ingrese una identificación válida e intente de nuevo. "  + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }
    }
}