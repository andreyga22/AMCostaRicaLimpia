﻿using BL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ProyectoAMCRL
{
    public partial class CompraVenta2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cuentaLogin"] != null)
            {
                //hidden.Text = Convert.ToString(Session["cantidadVentana"]);
                Session["idBodegaCompra"] = bodegasDd.SelectedValue;
                Session["idMonedaCompra"] = monedaDd.SelectedValue;
                if (!IsPostBack)
                {
                    try
                    {
                        if (Convert.ToString(Session["tipoFactura"]).Equals("compra")) {
                            tipoLb.Text = "Factura de Compra";
                            cargarBodegasCompra();
                        } else {
                            tipoLb.Text = "Factura de Venta";
                            cargarBodegasVenta();
                        }

                        cargarMonedas();
                        cargarCajero();
                        cargarNumFactura();
                        cargarFecha();
                        cargarMaterialesDrop();
                        cargarNombre(materialesDd.SelectedValue);
                        refrescarGrid();
                    }
                    catch (Exception exx)
                    {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se pudo cargar los datos de bodega. Revise su conexión a internet.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                        lblError2.Text = lblError.Text;
                        lblError2.Visible = true;
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void cargarFecha() {
            fechaLb.Text = Convert.ToString(DateTime.Now);
        }

        private void cargarCajero() {
            BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
            cajeroLb.Text = usuarioLogin.nombre_usuario;
        }

        private void cargarNumFactura() {
            BLManejadorFacturas man = new BLManejadorFacturas();
            int num = man.consultarSigFactura();
            numFacturaLb.Text = Convert.ToString(num+1);
        }

        private void cargarBodegasVenta() {
            BLManejadorInventario manejador = new BLManejadorInventario();
            List<String> lista = manejador.buscarBodegasVenta();
            //bodDD.Items.Insert(0, new ListItem("--SELECCIONE UNA BODEGA--"));
            foreach (Object bodega in lista) {
                bodegasDd.Items.Add(new System.Web.UI.WebControls.ListItem(bodega.ToString()));
            }
            Session["idBodegaCompra"] = bodegasDd.Items[0].Value;
        }
        private void cargarBodegasCompra() {
            BLManejadorInventario manejador = new BLManejadorInventario();
            List<String> lista = manejador.buscarBodegasCompra();
            //bodDD.Items.Insert(0, new ListItem("--SELECCIONE UNA BODEGA--"));
            foreach (Object bodega in lista) {
                bodegasDd.Items.Add(new System.Web.UI.WebControls.ListItem(bodega.ToString()));
            }
            Session["idBodegaCompra"] = bodegasDd.Items[0].Value;
        }

        private void cargarMonedas() {
            BLManejadorMoneda manejador = new BLManejadorMoneda();
            List<String> lista = manejador.listaMonedas();
            foreach (Object moneda in lista) {
                monedaDd.Items.Add(new System.Web.UI.WebControls.ListItem(moneda.ToString()));
            }
            Session["idMonedaCompra"] = monedaDd.Items[0].Value;
        }

        private void cargarMaterialesDrop() {
            BLManejadorMateriales manejador = new BLManejadorMateriales();
            List<String> lista = manejador.buscarMat();
            //bodDD.Items.Insert(0, new ListItem("--SELECCIONE UNA BODEGA--"));
            foreach (Object material in lista) {
                materialesDd.Items.Add(new System.Web.UI.WebControls.ListItem(material.ToString()));
            }
            Session["materialSeleccionado"] = materialesDd.Items[0].Value;
            actualizarCantidad();
        }

        private double consultarStock() {
            String bode = Convert.ToString(Session["idBodegaCompra"]);
            String mate = Convert.ToString(Session["materialSeleccionado"]);
            BLManejadorMateriales man = new BLManejadorMateriales();
            return man.consultarStock(bode, mate);
        }

        private void actualizarCantidad() {
            String bode = Convert.ToString( Session["idBodegaCompra"]);
            String mate = Convert.ToString(Session["materialSeleccionado"]);
            BLManejadorMateriales man = new BLManejadorMateriales();
            double cant = man.consultarStock(bode, mate);
            cantDisponibleTb.Text = Convert.ToString(cant);

            BLMaterial blmat = man.consultarMaterialAdmin(mate);
            String cod_uni = blmat.cod_Unidad;
            BLManejadorUnidad manuni = new BLManejadorUnidad();
            BLUnidad uni = manuni.consultar(cod_uni);
            unidadTb.Text = uni.codigo;

            String tipoF = Convert.ToString(Session["tipoFactura"]);
            if (tipoF.Equals("compra")) {
                precioCV.Text = Convert.ToString(blmat.precioCompraK);
            } else {
                precioCV.Text = Convert.ToString(blmat.precioVentaK);
            }
         }

        protected void bodegasDd_SelectedIndexChanged(object sender, EventArgs e) {
            Session["idBodegaCompra"] = bodegasDd.SelectedValue;
            actualizarCantidad();
        }

        protected void monedaDd_SelectedIndexChanged(object sender, EventArgs e) {
            Session["idMonedaCompra"] = monedaDd.SelectedValue;
            calcularTotales();
        }

        private double convertirMonedas(BLMoneda mone, double conversion) {
            return conversion / mone.equivalencia_Colon;
        }

        protected void txtPalabra_TextChanged(object sender, EventArgs e) {
            this.buscar();
        }

        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                this.buscar();
            }
        }

        protected void gridSocios_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gridSocios.PageIndex = e.NewPageIndex;
            this.buscar();
            if (Session["SortedView"] != null) {
                gridSocios.DataSource = Session["SortedView"];
                gridSocios.DataBind();
            }
        }

        protected void gridSocios_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (GridViewRow row in gridSocios.Rows) {
                if (row.RowIndex == gridSocios.SelectedIndex) {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                } else {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Clic para abrir.";
                }

            }
            string id = gridSocios.SelectedRow.Cells[0].Text;
            BLManejadorSocios man = new BLManejadorSocios();
            Session["idSocioCompra"] = man.buscarCedula(id);
            BLSocioNegocio soc = (BLSocioNegocio)Session["idSocioCompra"];
            cedulaCP.Text = soc.cedula;
            NombreCP.Text = soc.nombre;
            telCP.Text = Convert.ToString( soc.contactos.telefono_pers);
            correoCP.Text = soc.contactos.email;
            direccionCP.Text= soc.direccion.distrito + ", " + soc.direccion.canton + ", " + soc.direccion.provincia;
            //cedulaCP.Visible = true;
            //NombreCP.Visible = true;
            //telCP.Visible = true;
            //correoCP.Visible = true;
            //direccionCP.Visible = true;
            agregarCP.Text = "Editar";
        }

        protected void gridSocios_Sorting(object sender, GridViewSortEventArgs e) {
            try {
                DataTable datat = this.buscar();
                DataView dv = new DataView(datat);
                if (ViewState["sorting"] == null || ViewState["sorting"].ToString() == "DESC") {
                    dv.Sort = e.SortExpression + " ASC";
                    ViewState["sorting"] = "ASC";
                    //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortasc";

                } else {
                    if (ViewState["sorting"].ToString() == "ASC") {
                        dv.Sort = e.SortExpression + " DESC";
                        ViewState["sorting"] = "DESC";
                        //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortdesc";
                    }
                }
                Session["sortedView"] = dv;
                gridSocios.DataSource = dv;
                gridSocios.DataBind();


                if (ViewState["sorting"].ToString() == "ASC") {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridSocios.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
                } else {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridSocios.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
                }
            } catch (Exception) {

            }
        }

        protected void gridSocios_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridSocios, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

        private DataTable buscar() {
            BLManejadorSocios manejador = new BLManejadorSocios();
            DataTable tabla = new DataTable();
            BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
            if (usuarioLogin.rol.Equals("r")) {
                tabla = manejador.buscarDatosRegular(txtPalabra.Text.Trim());
            } else {
                tabla = manejador.buscarDatosAdmin(txtPalabra.Text.Trim());
            }
            gridSocios.DataSource = tabla;
            gridSocios.DataBind();
            return tabla;
        }

        private int GetColumnIndex(DataTable dt, string name) {
            return dt.Columns.IndexOf(name);
        }

        protected void agregarCP_Click(object sender, EventArgs e) {
            this.buscar();
        }

        protected void refrescarbtn_Click(object sender, EventArgs e) {
            this.buscar();
        }

        protected void materialesDd_SelectedIndexChanged(object sender, EventArgs e) {
            Session["materialSeleccionado"] = materialesDd.SelectedValue;
            actualizarCantidad();
            cargarNombre(materialesDd.SelectedValue);
        }

        private void cargarNombre(String codMat) {
            BLManejadorMateriales man = new BLManejadorMateriales();
            String nombre = man.buscarNombre(codMat);
            nombreMat.Text = nombre;
        }

        protected void agregarBtn_Click(object sender, EventArgs e) {
            double cantidadCVt = 0;
            double preciolinea = 0;
            double impuesto = 0;
            double descuento = 0;
            NumberFormatInfo format = new NumberFormatInfo();
            // Set the 'splitter' for thousands
            //format.NumberGroupSeparator = ".";
            // Set the decimal seperator
            format.NumberDecimalSeparator = ",";
            if (!consultarSiExisteEnTabla(Convert.ToString(Session["materialSeleccionado"]))) {
                if (!String.IsNullOrEmpty(cantidadVC.Text.Trim()) && double.TryParse(cantidadVC.Text.Trim(), NumberStyles.Any, format, out cantidadCVt)) {
                    if (!(cantidadCVt <= 0)) {
                        if (Convert.ToString(Session["tipoFactura"]).Equals("venta")) {
                            if (!(cantidadCVt > consultarStock())) {
                                if (!String.IsNullOrEmpty(precioCV.Text.Trim())) {
                                    if (double.TryParse(precioCV.Text.Trim(), NumberStyles.Any, format, out preciolinea)) {
                                        if (!(preciolinea <= 0)) {
                                            //sin descuento ni impuesto
                                            if (String.IsNullOrEmpty(impuestoTb.Text.Trim()) && String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                List<String> lista = new List<string>();
                                                lista.Add(Convert.ToString(Session["materialSeleccionado"]));
                                                lista.Add(Convert.ToString(cantidadCVt));
                                                lista.Add(Convert.ToString(preciolinea));
                                                lista.Add(Convert.ToString(0));
                                                lista.Add(Convert.ToString(0));
                                                lista.Add(Convert.ToString(calcularPrecioLinea()));
                                                List<List<String>> lis = (List<List<String>>)Session["listaCompra"];
                                                if (lis != null) {
                                                    lis.Add(lista);
                                                    Session["listaCompra"] = lis;
                                                    refrescarGrid();
                                                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                    lblError2.Text = lblError.Text;
                                                    lblError2.Visible = true;
                                                } else {
                                                    lis = new List<List<string>>();
                                                    lis.Add(lista);
                                                    Session["listaCompra"] = lis;
                                                    bodegasDd.Enabled = false;
                                                    refrescarGrid();
                                                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                    lblError2.Text = lblError.Text;
                                                    lblError2.Visible = true;
                                                }
                                                calcularTotales();
                                            }
                                            //impuesto + descuento
                                            if (!String.IsNullOrEmpty(impuestoTb.Text.Trim()) && !String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                if (double.TryParse(impuestoTb.Text.Trim(), NumberStyles.Any, format, out impuesto)) {
                                                    if (!(impuesto < 0) && !(impuesto > 100)) {
                                                        if (!String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                            if (double.TryParse(descuentoTb.Text.Trim(), NumberStyles.Any, format, out descuento)) {
                                                                if (!(descuento < 0) && !(descuento > 100)) {
                                                                    List<String> lista = new List<string>();
                                                                    lista.Add(Convert.ToString(Session["materialSeleccionado"]));
                                                                    lista.Add(Convert.ToString(cantidadCVt));
                                                                    lista.Add(Convert.ToString(preciolinea));
                                                                    lista.Add(Convert.ToString(impuesto));
                                                                    lista.Add(Convert.ToString(descuento));
                                                                    lista.Add(Convert.ToString(calcularPrecioLinea()));
                                                                    List<List<String>> lis = (List<List<String>>)Session["listaCompra"];
                                                                    if (lis != null) {
                                                                        lis.Add(lista);
                                                                        Session["listaCompra"] = lis;
                                                                        refrescarGrid();
                                                                        lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                        lblError.Visible = true;
                                                                        lblError2.Text = lblError.Text;
                                                                        lblError2.Visible = true;
                                                                    } else {
                                                                        lis = new List<List<string>>();
                                                                        lis.Add(lista);
                                                                        Session["listaCompra"] = lis;
                                                                        refrescarGrid();
                                                                        lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                        lblError.Visible = true;
                                                                        lblError2.Text = lblError.Text;
                                                                        lblError2.Visible = true;
                                                                    }
                                                                    calcularTotales();
                                                                } else {
                                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                    lblError.Visible = true;
                                                                    lblError2.Text = lblError.Text;
                                                                    lblError2.Visible = true;
                                                                }
                                                            } else {
                                                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                lblError.Visible = true;
                                                                lblError2.Text = lblError.Text;
                                                                lblError2.Visible = true;
                                                            }
                                                        }
                                                    } else {
                                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                        lblError.Visible = true;
                                                        lblError2.Text = lblError.Text;
                                                        lblError2.Visible = true;
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                    lblError2.Text = lblError.Text;
                                                    lblError2.Visible = true;
                                                }
                                            }
                                            //solo descuento
                                            if (!String.IsNullOrEmpty(descuentoTb.Text.Trim()) && String.IsNullOrEmpty(impuestoTb.Text.Trim())) {
                                                if (double.TryParse(descuentoTb.Text.Trim(), NumberStyles.Any, format, out descuento)) {
                                                    if (!(descuento < 0) && !(descuento > 100)) {
                                                        if (String.IsNullOrEmpty(impuestoTb.Text.Trim())) {
                                                            List<String> lista = new List<string>();
                                                            lista.Add(Convert.ToString(Session["materialSeleccionado"]));
                                                            lista.Add(Convert.ToString(cantidadCVt));
                                                            lista.Add(Convert.ToString(preciolinea));
                                                            lista.Add(Convert.ToString(0));
                                                            lista.Add(Convert.ToString(descuento));
                                                            lista.Add(Convert.ToString(calcularPrecioLinea()));
                                                            List<List<String>> lis = (List<List<String>>)Session["listaCompra"];
                                                            if (lis != null) {
                                                                lis.Add(lista);
                                                                Session["listaCompra"] = lis;
                                                                refrescarGrid();
                                                                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                lblError.Visible = true;
                                                                lblError2.Text = lblError.Text;
                                                                lblError2.Visible = true;
                                                            } else {
                                                                lis = new List<List<string>>();
                                                                lis.Add(lista);
                                                                Session["listaCompra"] = lis;
                                                                refrescarGrid();
                                                                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                lblError.Visible = true;
                                                                lblError2.Text = lblError.Text;
                                                                lblError2.Visible = true;
                                                            }
                                                            calcularTotales();
                                                        }
                                                    } else {
                                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                        lblError.Visible = true;
                                                        lblError2.Text = lblError.Text;
                                                        lblError2.Visible = true;
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                    lblError2.Text = lblError.Text;
                                                    lblError2.Visible = true;
                                                }
                                            }
                                            //solo impuesto
                                            if (!String.IsNullOrEmpty(impuestoTb.Text.Trim()) && String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                if (double.TryParse(impuestoTb.Text.Trim(), NumberStyles.Any, format, out impuesto)) {
                                                    if (!(impuesto < 0) && !(impuesto > 100)) {
                                                        if (String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                            List<String> lista = new List<string>();
                                                            lista.Add(Convert.ToString(Session["materialSeleccionado"]));
                                                            lista.Add(Convert.ToString(cantidadCVt));
                                                            lista.Add(Convert.ToString(preciolinea));
                                                            lista.Add(Convert.ToString(impuesto));
                                                            lista.Add(Convert.ToString(0));
                                                            lista.Add(Convert.ToString(calcularPrecioLinea()));
                                                            List<List<String>> lis = (List<List<String>>)Session["listaCompra"];
                                                            if (lis != null) {
                                                                lis.Add(lista);
                                                                Session["listaCompra"] = lis;
                                                                refrescarGrid();
                                                                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                lblError.Visible = true;
                                                                lblError2.Text = lblError.Text;
                                                                lblError2.Visible = true;
                                                            } else {
                                                                lis = new List<List<string>>();
                                                                lis.Add(lista);
                                                                Session["listaCompra"] = lis;
                                                                refrescarGrid();
                                                                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                lblError.Visible = true;
                                                                lblError2.Text = lblError.Text;
                                                                lblError2.Visible = true;
                                                            }
                                                            calcularTotales();
                                                        }
                                                    } else {
                                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                        lblError.Visible = true;
                                                        lblError2.Text = lblError.Text;
                                                        lblError2.Visible = true;
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                    lblError2.Text = lblError.Text;
                                                    lblError2.Visible = true;
                                                }
                                            }
                                        } else {
                                            lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio es un número menor o igual a 0. Utilice solo números positivos.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                            lblError.Visible = true;
                                            lblError2.Text = lblError.Text;
                                            lblError2.Visible = true;
                                        }
                                    } else {
                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio contienen caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                        lblError.Visible = true;
                                        lblError2.Text = lblError.Text;
                                        lblError2.Visible = true;
                                    }
                                } else {
                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio no puede ir en blanco. El precio minimo es 1.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                    lblError.Visible = true;
                                    lblError2.Text = lblError.Text;
                                    lblError2.Visible = true;
                                }
                            } else {
                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se puede vender una cantidad mayor al stock de bodega de este material.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                lblError.Visible = true;
                                lblError2.Text = lblError.Text;
                                lblError2.Visible = true;
                            }
                        } else {
                            if (!String.IsNullOrEmpty(precioCV.Text.Trim())) {
                                if (double.TryParse(precioCV.Text.Trim(), NumberStyles.Any, format, out preciolinea)) {
                                    if (!(preciolinea <= 0)) {
                                        //sin descuento ni impuesto
                                        if (String.IsNullOrEmpty(impuestoTb.Text.Trim()) && String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                            List<String> lista = new List<string>();
                                            lista.Add(Convert.ToString(Session["materialSeleccionado"]));
                                            lista.Add(Convert.ToString(cantidadCVt));
                                            lista.Add(Convert.ToString(preciolinea));
                                            lista.Add(Convert.ToString(0));
                                            lista.Add(Convert.ToString(0));
                                            lista.Add(Convert.ToString(calcularPrecioLinea()));
                                            List<List<String>> lis = (List<List<String>>)Session["listaCompra"];
                                            if (lis != null) {
                                                lis.Add(lista);
                                                Session["listaCompra"] = lis;
                                                refrescarGrid();
                                                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                lblError.Visible = true;
                                                lblError2.Text = lblError.Text;
                                                lblError2.Visible = true;
                                            } else {
                                                lis = new List<List<string>>();
                                                lis.Add(lista);
                                                Session["listaCompra"] = lis;
                                                refrescarGrid();
                                                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                lblError.Visible = true;
                                                lblError2.Text = lblError.Text;
                                                lblError2.Visible = true;
                                            }
                                            calcularTotales();
                                        }
                                        //impuesto + descuento
                                        if (!String.IsNullOrEmpty(impuestoTb.Text.Trim()) && !String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                            if (double.TryParse(impuestoTb.Text.Trim(), NumberStyles.Any, format, out impuesto)) {
                                                if (!(impuesto < 0) && !(impuesto > 100)) {
                                                    if (!String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                        if (double.TryParse(descuentoTb.Text.Trim(), NumberStyles.Any, format, out descuento)) {
                                                            if (!(descuento < 0) && !(descuento > 100)) {
                                                                List<String> lista = new List<string>();
                                                                lista.Add(Convert.ToString(Session["materialSeleccionado"]));
                                                                lista.Add(Convert.ToString(cantidadCVt));
                                                                lista.Add(Convert.ToString(preciolinea));
                                                                lista.Add(Convert.ToString(impuesto));
                                                                lista.Add(Convert.ToString(descuento));
                                                                lista.Add(Convert.ToString(calcularPrecioLinea()));
                                                                List<List<String>> lis = (List<List<String>>)Session["listaCompra"];
                                                                if (lis != null) {
                                                                    lis.Add(lista);
                                                                    Session["listaCompra"] = lis;
                                                                    refrescarGrid();
                                                                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                    lblError.Visible = true;
                                                                    lblError2.Text = lblError.Text;
                                                                    lblError2.Visible = true;
                                                                } else {
                                                                    lis = new List<List<string>>();
                                                                    lis.Add(lista);
                                                                    Session["listaCompra"] = lis;
                                                                    refrescarGrid();
                                                                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                    lblError.Visible = true;
                                                                    lblError2.Text = lblError.Text;
                                                                    lblError2.Visible = true;
                                                                }
                                                                calcularTotales();
                                                            } else {
                                                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                lblError.Visible = true;
                                                                lblError2.Text = lblError.Text;
                                                                lblError2.Visible = true;
                                                            }
                                                        } else {
                                                            lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                            lblError.Visible = true;
                                                            lblError2.Text = lblError.Text;
                                                            lblError2.Visible = true;
                                                        }
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                    lblError2.Text = lblError.Text;
                                                    lblError2.Visible = true;
                                                }
                                            } else {
                                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                //lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se pudo cargar los datos de bodega. Revise su conexión a internet.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                lblError.Visible = true;
                                                lblError2.Text = lblError.Text;
                                                lblError2.Visible = true;
                                            }
                                        }
                                        //solo descuento
                                        if (!String.IsNullOrEmpty(descuentoTb.Text.Trim()) && String.IsNullOrEmpty(impuestoTb.Text.Trim())) {
                                            if (double.TryParse(descuentoTb.Text.Trim(), NumberStyles.Any, format, out descuento)) {
                                                if (!(descuento < 0) && !(descuento > 100)) {
                                                    if (String.IsNullOrEmpty(impuestoTb.Text.Trim())) {
                                                        List<String> lista = new List<string>();
                                                        lista.Add(Convert.ToString(Session["materialSeleccionado"]));
                                                        lista.Add(Convert.ToString(cantidadCVt));
                                                        lista.Add(Convert.ToString(preciolinea));
                                                        lista.Add(Convert.ToString(0));
                                                        lista.Add(Convert.ToString(descuento));
                                                        lista.Add(Convert.ToString(calcularPrecioLinea()));
                                                        List<List<String>> lis = (List<List<String>>)Session["listaCompra"];
                                                        if (lis != null) {
                                                            lis.Add(lista);
                                                            Session["listaCompra"] = lis;
                                                            refrescarGrid();
                                                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                            lblError.Visible = true;
                                                            lblError2.Text = lblError.Text;
                                                            lblError2.Visible = true;
                                                        } else {
                                                            lis = new List<List<string>>();
                                                            lis.Add(lista);
                                                            Session["listaCompra"] = lis;
                                                            refrescarGrid();
                                                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                            lblError.Visible = true;
                                                            lblError2.Text = lblError.Text;
                                                            lblError2.Visible = true;
                                                        }
                                                        calcularTotales();
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                    lblError2.Text = lblError.Text;
                                                    lblError2.Visible = true;
                                                }
                                            } else {
                                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                lblError.Visible = true;
                                                lblError2.Text = lblError.Text;
                                                lblError2.Visible = true;
                                            }
                                        }
                                        //solo impuesto
                                        if (!String.IsNullOrEmpty(impuestoTb.Text.Trim()) && String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                            if (double.TryParse(impuestoTb.Text.Trim(), NumberStyles.Any, format, out impuesto)) {
                                                if (!(impuesto < 0) && !(impuesto > 100)) {
                                                    if (String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                        List<String> lista = new List<string>();
                                                        lista.Add(Convert.ToString(Session["materialSeleccionado"]));
                                                        lista.Add(Convert.ToString(cantidadCVt));
                                                        lista.Add(Convert.ToString(preciolinea));
                                                        lista.Add(Convert.ToString(impuesto));
                                                        lista.Add(Convert.ToString(0));
                                                        lista.Add(Convert.ToString(calcularPrecioLinea()));
                                                        List<List<String>> lis = (List<List<String>>)Session["listaCompra"];
                                                        if (lis != null) {
                                                            lis.Add(lista);
                                                            Session["listaCompra"] = lis;
                                                            refrescarGrid();
                                                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                            lblError.Visible = true;
                                                            lblError2.Text = lblError.Text;
                                                            lblError2.Visible = true;
                                                        } else {
                                                            lis = new List<List<string>>();
                                                            lis.Add(lista);
                                                            Session["listaCompra"] = lis;
                                                            refrescarGrid();
                                                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> Linea agregada.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                            lblError.Visible = true;
                                                        }
                                                        calcularTotales();
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                    lblError2.Text = lblError.Text;
                                                    lblError2.Visible = true;
                                                }
                                            } else {
                                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                lblError.Visible = true;
                                                lblError2.Text = lblError.Text;
                                                lblError2.Visible = true;
                                            }
                                        }
                                    } else {
                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio es un número menor o igual a 0. Utilice solo números positivos.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                        lblError.Visible = true;
                                        lblError2.Text = lblError.Text;
                                        lblError2.Visible = true;
                                    }
                                } else {
                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio contienen caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                    lblError.Visible = true;
                                    lblError2.Text = lblError.Text;
                                    lblError2.Visible = true;
                                }
                            } else {
                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio no puede ir en blanco. El precio minimo es 1.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                lblError.Visible = true;
                                lblError2.Text = lblError.Text;
                                lblError2.Visible = true;
                            }
                        }
                    } else {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> La cantidad de compra/venta es menor a 0. Utilice solo números positivos<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                        lblError2.Text = lblError.Text;
                        lblError2.Visible = true;
                    }
                } else {
                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El control cantidad esta vacío o tiene caracteres que no son número.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                    lblError2.Text = lblError.Text;
                    lblError2.Visible = true;
                }
            } else {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Ya se encuentro el material seleccionado en la lista de compra/venta.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
                lblError2.Text = lblError.Text;
                lblError2.Visible = true;
            }
            List<List<String>> lil = (List<List<String>>)Session["listaCompra"];
            if (lil != null) {
                if (lil.Count > 0) {
                    bodegasDd.Enabled = false;
                } else {
                    bodegasDd.Enabled = false;
                }
            }
        }

        private Boolean consultarSiExisteEnTabla(String mate) {
            //codigo, cantidad, precio, impueto, descuento, precio linea
            List<List<String>> lil = (List<List<String>>)Session["listaCompra"];
            if (lil != null) {
                foreach (List<String> item in lil) {
                    if (item[0].Equals(mate)) {
                        return true;
                    }
                }
            }
            return false;
        }

        private void refrescarGrid() {

            DataTable datat = new DataTable();
            
            datat.Columns.Add("Código", typeof(String));
            datat.Columns.Add("Cantidad", typeof(String));
            datat.Columns.Add("Precio Unidad", typeof(String));
            datat.Columns.Add("Impuesto", typeof(String));
            datat.Columns.Add("Descuento", typeof(String));
            datat.Columns.Add("Precio Linea", typeof(String));

            List<List<String>> lil = (List<List<String>>)Session["listaCompra"];
            if (lil != null) {
                
                foreach (List<String> item in lil) {
                    DataRow row = datat.NewRow();
                    for (int i = 0; i < item.Count; i++) {
                        row[i] = item[i];
                    }
                    datat.Rows.Add(row);
                }
                gridFactura.DataSource = datat;
                gridFactura.DataBind();
            }
            foreach (GridViewRow row in gridFactura.Rows) {
                LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
                lb.Text = "Eliminar";
            }
        }

        protected void gridFactura_SelectedIndexChanged(object sender, EventArgs e) {
            
                string id = gridFactura.SelectedRow.Cells[1].Text;
                borrarDelista(id);
            
        }

        private void borrarDelista(String id) {
            List<List<String>> list = (List<List<String>>)Session["listaCompra"];
            if (list != null) {
                for (int i = 0; i < list.Count; i++) {
                    if (list[i][0].Equals(id)) {
                        list.RemoveAt(i);
                    }
                }
                Session["listaCompra"] = list;
                if (list.Count == 0) {
                    bodegasDd.Enabled = true;
                }
                refrescarGrid();
            }
            calcularTotales();
        }

        protected void cantidadVC_TextChanged(object sender, EventArgs e) {
            precioTotal.Text = Convert.ToString(calcularPrecioLinea());
        }

        private double calcularPrecioLinea() {
            double total = 0;
            double totalConImp = 0;
            double totalConDesc = 0;
            double cant = 0;
            NumberFormatInfo format = new NumberFormatInfo();
            // Set the 'splitter' for thousands
            //format.NumberGroupSeparator = ".";
            // Set the decimal seperator
            format.NumberDecimalSeparator = ",";
            if (!String.IsNullOrEmpty(cantidadVC.Text.Trim()) && double.TryParse(cantidadVC.Text.Trim(), NumberStyles.Any, format, out cant)) {
            }
            double precio = 0;
            if (!String.IsNullOrEmpty(precioCV.Text.Trim()) && double.TryParse(precioCV.Text.Trim(), NumberStyles.Any, format, out precio)) {
            }

            double imp = 0;
            if (!String.IsNullOrEmpty(impuestoTb.Text.Trim()) && double.TryParse(impuestoTb.Text.Trim(), NumberStyles.Any, format, out imp)) {
            }
            double desc = 0;
            if (!String.IsNullOrEmpty(descuentoTb.Text.Trim()) && double.TryParse(descuentoTb.Text.Trim(), NumberStyles.Any, format, out desc)) {
            }

            total = (cant * precio);
            totalConImp = total * (imp / 100);
            totalConDesc = total * (desc / 100);
            total += totalConImp;
            total -= totalConDesc;
            return total;
        }

        protected void precioCV_TextChanged(object sender, EventArgs e) {
            precioTotal.Text = Convert.ToString(calcularPrecioLinea());
        }

        protected void impuestoTb_TextChanged(object sender, EventArgs e) {
            precioTotal.Text = Convert.ToString(calcularPrecioLinea());
        }

        protected void descuentoTb_TextChanged(object sender, EventArgs e) {
            precioTotal.Text = Convert.ToString(calcularPrecioLinea());
        }

        private void calcularTotales() {
            BLManejadorMoneda mon = new BLManejadorMoneda();
            BLMoneda mone = mon.consultarAdmin(monedaDd.SelectedValue);
            double totalLibre = 0;
            double impuesto = 0;
            double descuento = 0;
            double total = 0;
            double conversion = 0;
            List<List<String>> lil = (List<List<String>>)Session["listaCompra"];
            if (lil != null) {
                foreach (List<String> item in lil) {
                    totalLibre += Convert.ToDouble(item[1]) * Convert.ToDouble(item[2]);
                    if (Convert.ToDouble(item[3]) > 0) {
                        impuesto += Convert.ToDouble(Convert.ToDouble(item[1]) * Convert.ToDouble(item[2]) * (Convert.ToDouble(item[3]) / 100));
                    }
                    if (Convert.ToDouble(item[4]) > 0) {
                        descuento += Convert.ToDouble(Convert.ToDouble(item[1]) * Convert.ToDouble(item[2]) * (Convert.ToDouble(item[4]) / 100));
                    }
                    total = totalLibre + impuesto;
                    total = total- descuento;
                    if (!monedaDd.SelectedValue.Equals("CRC")) {
                        conversion = convertirMonedas(mone, total);
                    } else {
                        conversion = total;
                    }
                }
                subtotalTb.Text = Convert.ToString(totalLibre);
                impuestoTot.Text = Convert.ToString(impuesto);
                descuentoTot.Text = Convert.ToString(descuento);
                totalTb.Text = Convert.ToString(total);
                totalConvert.Text = Convert.ToString(conversion);
            }
        }

        protected void generarBtn_Click(object sender, EventArgs e) {
            //Session["tipoFactura"] = null;
            //Session["idBodegaCompra"] = null;
            //Session["idMonedaCompra"] = null;
            //Session["idSocioCompra"] = null;
            //Session["materialSeleccionado"] = null;
            //codigo, cantidad, precio, impueto, descuento, precio linea
            

            List<BLDetalleFactura> det = new List<BLDetalleFactura>();
            List<List<String>> lil = (List < List < String >> )Session["listaCompra"];
            if (lil != null) {
                foreach (List<String> item in lil) {
                    BLDetalleFactura deta = new BLDetalleFactura();
                    deta.codigoMaterial = item[0];
                    deta.cantidad = Convert.ToDouble(item[1]);
                    deta.precio = Convert.ToDouble(item[2]);
                    deta.impuesto = Convert.ToDouble(item[3]);
                    deta.descuento = Convert.ToDouble(item[4]);
                    deta.monto_Linea = Convert.ToDouble(item[5]);
                    det.Add(deta);
                }
                if ((BLSocioNegocio)Session["idSocioCompra"] != null) {
                    String idbod = Convert.ToString(Session["idBodegaCompra"]);
                    String idmon = Convert.ToString(Session["idMonedaCompra"]);
                    String idcajero = ((BLCuenta)Session["cuentaLogin"]).id_usuario;
                    String idsocio = ((BLSocioNegocio)Session["idSocioCompra"]).cedula;
                    String tipofac = Convert.ToString(Session["tipoFactura"]);
                    BLFactura fac = new BLFactura(0, idbod, idmon, idcajero, idsocio, Convert.ToDouble(subtotalTb.Text.Trim()), Convert.ToDouble(impuestoTot.Text.Trim()), Convert.ToDouble(descuentoTot.Text.Trim()), Convert.ToDouble(totalTb.Text.Trim()), Convert.ToDouble(totalConvert.Text.Trim()), DateTime.Now, tipofac);
                    try {
                        BLManejadorFacturas man = new BLManejadorFacturas();
                        man.guardarFactura(det, fac);
                        lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong> La factura se creó correctamente<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                        lblError2.Text = lblError.Text;
                        lblError2.Visible = true;
                        //generarPDF("15", DateTime.Now);
                    } catch (Exception exx) {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se puede guardar la factura en el sistema. Revise su conexión a internet e intentelo nuevamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                        lblError2.Text = lblError.Text;
                        lblError2.Visible = true;
                    }
                } else {
                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Debe seleccionar un socio para la compra o venta.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                    lblError2.Text = lblError.Text;
                    lblError2.Visible = true;
                }
                
            } else {

            }

            

            
        }

        private void generarPDF(String numeroFactura, DateTime fecha) {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document documento = new Document(PageSize.A4, 10, 10, 10, 10);
                String path = Server.MapPath("~/Facturas/");
                PdfWriter.GetInstance(documento, new FileStream(path + "/" + numeroFactura  + ".pdf", FileMode.Create));
                documento.Open();

                Chunk chunk = new Chunk("Prueba Chunk ");
                documento.Add(chunk);

                Phrase phrase = new Phrase("Prueba frase.");
                documento.Add(phrase);

                Paragraph para = new Paragraph("Prueba Parrafo.");
                documento.Add(para);

                string text = "PDF creado con exito!!";
                Paragraph paragraph = new Paragraph();
                paragraph.SpacingBefore = 10;
                paragraph.SpacingAfter = 10;
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f, BaseColor.GREEN);
                paragraph.Add(text);
                documento.Add(paragraph);

                documento.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";

                string nombrepdf = "PruebaProyecto";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + nombrepdf + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }

        protected void nuevoMat_Click(object sender, EventArgs e) {
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('RegistroMateriales.aspx');", true);
            //Response.Write("<script>window.open ('RegistroMateriales.aspx','_blank');</script>");
        }

        protected void nuevoSoc_Click(object sender, EventArgs e) {
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('RegistroSociosUI.aspx');", true);
            //Response.Write("<script>window.open ('RegistroSociosUI.aspx','_blank');</script>");
        }

        protected void refrescarMate_Click(object sender, EventArgs e) {
            materialesDd.Items.Clear();
            cargarMaterialesDrop();
        }

    }

}