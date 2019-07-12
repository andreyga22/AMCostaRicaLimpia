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
using System.Windows.Forms;

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
                String id = Convert.ToString(Session["idFactura"]);
                if (!String.IsNullOrEmpty(id) || (!String.IsNullOrWhiteSpace(id)))
                {
                    detalles = new List<string>();
                    desactivarCampos();
                    cargarFactura(id);
                    Session.Remove("idFactura");
                }
                else
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
                    buscarSocioBTN.Attributes.Add("onclick", "document.body.style.cursor = 'wait';");
                    materialDD.Attributes.Add("onclick", "document.body.style.cursor = 'wait';");

                    // Se cargan las unidades, bodegas, monedas y materiales existentes 
                    // (La seleccion de bodega debe especificar los materiales disponibles)
                    cargarUnidadesBodegasMonedas();
                    cargarMateriales(bodegasDrop.Items[0].Value);
                    datepickerT.Text = DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;
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
        Desactiva y coloca no visibles algunos espacios de la pantalla
        para el modo de ver como factura ya realizada
        */
        private void desactivarCampos()
        {
            identificacionTB.Enabled = false;
            nombreLabel.Enabled = false;
            labelDireccion.Enabled = false;
            monedasDD.Enabled = false;
            bodegasDrop.Enabled = false;
            datepickerT.Enabled = false;
            btnGuardar.Visible = false;
        }

        /*
         Se setea el texto de los labels de la pantalla 
         segun la operacion que se va a realizar (compra/venta)
        */
        private void cargarPantalla(String modo)
        {

            String textoBreadCrum1 = "Compra";
            String textoBreadCrum2 = "Registrar Compra";
            String textoDatoSocio = "Datos del proveedor";
            String textoDatoConsecutivo = "Compra #";

            if (modo.Equals("venta"))
            {
                textoBreadCrum1 = "Venta";
                textoBreadCrum2 = "Registrar Venta";
                textoDatoSocio = "Datos del cliente";
                textoDatoConsecutivo = "Venta #";
            }
            labelBreadCrum1.Text = textoBreadCrum1;
            labelBreadCrum2.Text = textoBreadCrum2;
            labelDatosSocio.Text = textoDatoSocio;
            labelDatoConsecutivo.Text = textoDatoConsecutivo;
        }


        /// <summary>
        /// Carga la pantalla, como modo de vista únicamente, con una factura ya realizada
        /// </summary>
        /// <param name="id">Identificador de la factura que se va a mostrar en la pantalla</param>
        private void cargarFactura(string id)
        {
            try
            {
                BLManejadorFacturas manejFact = new BLManejadorFacturas();
                BLManejadorMoneda manejMond = new BLManejadorMoneda();
                BLManejadorSocios manejSocios = new BLManejadorSocios();
                BLManejadorBodega manejBod = new BLManejadorBodega();

                String tipoFact = "";
                String tipoSocio = "";
                BLFactura blFactura = manejFact.buscarVentaID(Convert.ToInt32(id));
                String texto = "Vista de venta";
                if (blFactura.tipo.Equals("v"))
                {
                    tipoFact = "venta";
                    tipoSocio = "Cliente";
                }
                else
                {
                    texto = "Vista de compra";
                    tipoFact = "compra";
                    tipoSocio = "Proveedor";
                }
                cargarPantalla(tipoFact);
                labelBreadCrum2.Text = texto;

                BLSocioNegocio socio = manejSocios.buscarSocio(blFactura.cedula, tipoSocio);
                List<BLDetalleFactura> detallesFactura = manejFact.listaDetalle(blFactura.cod_Factura);
                BLContactos contac = manejSocios.buscarContactos(blFactura.cedula);
                if (contac.telefono_pers != 0)
                {
                    labelTel.Text = contac.telefono_pers + "";
                }
                else
                {
                    if (contac.telefono_hab != 0)
                    {
                        labelTel.Text = contac.telefono_hab + "";
                    }
                    else
                    {
                        labelTel.Text = "No posee teléfono";
                    }
                }

                foreach (BLDetalleFactura bl in detallesFactura)
                {
                    String lineaDetalle = "";
                    lineaDetalle = bl.nombreMaterial + "&" +
                    bl.monto_Linea + "&" + bl.kilos_Linea + "&" + " KILOS";

                    detalles.Add(lineaDetalle);
                    pegarLineasTablaFacturaCargada();
                    labelAgregados.Text = detalles.Count().ToString();
                }

                identificacionTB.Text = blFactura.cedula;
                nombreLabel.Text = blFactura.nombreCompleto;
                labelDireccion.Text = socio.direccion.distrito + ", " + socio.direccion.canton;
                monedasDD.Items.Add(manejMond.buscarMonedaId(blFactura.id_Moneda).detalleMoneda);
                monedasDD.CssClass = "btn btn-light dropdown-toggle";

                bodegasDrop.Items.Add(manejBod.consultarBodegaAdmin(blFactura.id_Bodega).nombre);
                bodegasDrop.CssClass = "btn btn-light dropdown-toggle";

                datepickerT.Text = blFactura.fecha.Day + "/" + blFactura.fecha.Month + "/" + blFactura.fecha.Year;
                datepickerT.CssClass = "form-control font-weight-bolder";
                labelValorDatoConsecutivo.Text = Convert.ToString(blFactura.cod_Factura);
                totalLabel.Text = Convert.ToString(blFactura.monto_Total);

                filaAgregarDetalles.Visible = false;
                buscarSocioBTN.Visible = false;
                materialDD.Visible = false;
                precioKg2TB.Visible = false;
                cantidad2TB.Visible = false;
                unidadDD.Visible = false;
                agregarLineaBTN.Visible = false;
            }
            catch (Exception)
            {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>No se ha podido cargar la información de la factura.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if (detalles.Count == 0)//no se han agregado detalles
            {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>No se han especificado detalles para la transacción, por favor intente de nuevo.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            else if (String.IsNullOrEmpty(nombreLabel.Text))
            {
                String tipoSocio = labelBreadCrum1.Text.Equals("Compra") ? "Proveedor" : "Cliente";
                pegarLineasTabla();
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong> " + tipoSocio + " no especificado, por favor intente de nuevo.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
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
                    totalLabel.Text = "";
                    nombreLabel.Text = "";
                    labelDireccion.Text = "";
                    labelTel.Text = "";

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
                cantidad2TB.Style.Add("border-color", "transparent");
                String lineaAjusteInfo = "";
                Double precioEspecificado = String.IsNullOrEmpty(precioKg2TB.Value) ? 0 : Double.Parse(precioKg2TB.Value);
                lineaAjusteInfo = materialDD.SelectedItem.Value + '#' + materialDD.SelectedItem.Text + "&" +
                precioEspecificado + "&" + cantidad2TB.Value + "&" + unidadDD.SelectedItem.Value + '#' + unidadDD.SelectedItem.Text;
                detalles.Add(lineaAjusteInfo);
                Session.Add("listaDetallesC", detalles);
                pegarLineasTabla();
                labelAgregados.Text = detalles.Count().ToString();
                refrescarDatos();
            }
            else
            {
                cantidad2TB.Style.Add("border-color", "red");
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
            tablaDetalles.Rows.Clear();
            for (int i = 0; i < detalles.Count; i++)
            {
                String linea = detalles[i];

                String[] lineaInfo = linea.Split('&');
                TableCell productoCell = new TableCell();
                productoCell.Style.Add("width", "30%");
                TableCell precioKg = new TableCell();
                precioKg.Style.Add("width", "20%");
                TableCell cantidadCell = new TableCell();
                cantidadCell.Style.Add("width", "20%");

                TableCell unidadCell = new TableCell();
                unidadCell.Style.Add("width", "20%");

                TableCell quitarFilaCampo = new TableCell();
                quitarFilaCampo.Style.Add("width", "10%");
                TableRow filaNueva = new TableRow();

                String[] materialInfo = lineaInfo[0].Split('#');
                String[] idANDstock = materialInfo[0].Split('-');
                productoCell.Text = idANDstock[0]+"-"+materialInfo[1];
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
      Se agregan las líneas a la pantalla según los detalles en la lista, 
      en el modo de ver factura realizada con anterioridad
      */
        private void pegarLineasTablaFacturaCargada()
        {
            for (int i = 0; i < detalles.Count; i++)
            {
                String linea = detalles[i];

                String[] lineaInfo = linea.Split('&');
                TableCell productoCell = new TableCell();
                TableCell precioKg = new TableCell();
                TableCell cantidadCell = new TableCell();

                TableCell unidadCell = new TableCell();
                TableRow filaNueva = new TableRow();


                productoCell.Text = lineaInfo[0];
                precioKg.Text = lineaInfo[1];
                cantidadCell.Text = lineaInfo[2];
                unidadCell.Text = lineaInfo[3];

                filaNueva.Cells.Add(productoCell);
                filaNueva.Cells.Add(precioKg);
                filaNueva.Cells.Add(cantidadCell);
                filaNueva.Cells.Add(unidadCell);
                tablaDetalles.Rows.Add(filaNueva);
            }
            labelAgregados.Text = detalles.Count.ToString();
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
                    item.Value = codigo + "-" + id_stock;
                    materialDD.Items.Add(item);
                }
            }
            materialDD.DataBind();
        }

        private void cargarUnidadesBodegasMonedas()
        {
            cargarUnidades();

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
                moneda.Value = (Convert.ToString(dr["ID_MONEDA"])) + "#" + (Convert.ToString(dr["EQUIVALENCIA_COLON"]));
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

        private void Textbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                labelDatosSocio.BackColor = System.Drawing.Color.Red;
                //aqui codigo
            }

        }

        protected void buscarSocioBTN_Click(object sender, EventArgs e)
        {
            String id = identificacionTB.Text;
            nombreLabel.Text = "";
            labelDireccion.Text = "";
            labelTel.Text = "";
            String tipoSocio = labelBreadCrum1.Text.Equals("Compra") ? "Proveedor" : "Cliente";

            if (!String.IsNullOrEmpty(id))
            {
                BLManejadorSocios manejadorS = new BLManejadorSocios();
                BLSocioNegocio socio = manejadorS.buscarSocio(id, tipoSocio);
                if (socio != null)
                {


                    nombreLabel.Text = socio.nombre + " " + socio.apellido1 + " " + socio.apellido2;

                    if (socio.direccion != null)
                        labelDireccion.Text = socio.direccion.provincia + ", " + socio.direccion.canton
                        + ", " + socio.direccion.distrito;

                    if (socio.contactos != null)
                        labelTel.Text = socio.contactos.telefono_pers.ToString();

                    pegarLineasTabla();
                }
                else
                {
                    pegarLineasTabla();
                    lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + tipoSocio + " no encontrado, intente de nuevo. " + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }


            }
            else
            {
                pegarLineasTabla();
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + "Ingrese una identificación válida e intente de nuevo. " + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        protected void materialDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] materialInfo = materialDD.SelectedItem.Value.Split('-');
            String codigo = materialInfo[0];

            DataSet materialInfoSet = new DataSet();

            if (manejadorM == null)
                manejadorM = new BLManejadorMateriales();

            if (labelBreadCrum1.Text.Equals("Compra"))
                materialInfoSet = manejadorM.traerUnidadYprecioBase(codigo, 'c');
            else
                materialInfoSet = manejadorM.traerUnidadYprecioBase(codigo, 'v');

            if (materialInfoSet != null) {
                String unidadBase = Convert.ToString(materialInfoSet.Tables[0].Rows[0]["COD_UNIDAD"]);
                String precioString = Convert.ToString(materialInfoSet.Tables[0].Rows[0]["PRECIO_BASE"]);
                double precioBase = Double.Parse(precioString);

                precioKg2TB.Value = precioString;
                int index = 0;

                unidadDD.Items.Clear();
                cargarUnidades();

                for (int ind = 0; ind < unidadDD.Items.Count; ind++) {
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


        private void cargarUnidades() {

            if (manejadorU == null)
                manejadorU = new BLManejadorUnidades();

            DataSet unidades = manejadorU.listarUnidades();

            foreach (DataRow dr in unidades.Tables[0].Rows)
            {
                // COD_UNIDAD, NOMBRE_UNIDAD 

                String codigo = Convert.ToString(dr["COD_UNIDAD"]);
                String nombre = Convert.ToString(dr["NOMBRE_UNIDAD"]);
                String equiv = Convert.ToString(dr["EQUIVALENCIA_KG"]);
                String infoUnidad = codigo + "*" + equiv;

                ListItem item = new ListItem(nombre, infoUnidad);
                unidadDD.Items.Add(item);
            }

        }

    }
}