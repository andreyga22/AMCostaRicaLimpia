using BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
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
                if (!IsPostBack)
                {
                    try
                    {
                        if (Convert.ToString(Session["tipoFactura"]).Equals("compra")) {
                            cargarBodegasCompra();
                        } else {
                            cargarBodegasVenta();
                        }
                        cargarMonedas();
                        cargarCajero();
                        cargarNumFactura();
                        cargarFecha();
                        cargarMaterialesDrop();
                    }
                    catch (Exception exx)
                    {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se pudo cargar los datos de bodega. Revise su conexión a internet.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
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
                bodegasDd.Items.Add(new ListItem(bodega.ToString()));
            }
            Session["idBodegaCompra"] = bodegasDd.Items[0].Value;
        }
        private void cargarBodegasCompra() {
            BLManejadorInventario manejador = new BLManejadorInventario();
            List<String> lista = manejador.buscarBodegasCompra();
            //bodDD.Items.Insert(0, new ListItem("--SELECCIONE UNA BODEGA--"));
            foreach (Object bodega in lista) {
                bodegasDd.Items.Add(new ListItem(bodega.ToString()));
            }
            Session["idBodegaCompra"] = bodegasDd.Items[0].Value;
        }

        private void cargarMonedas() {
            BLManejadorMoneda manejador = new BLManejadorMoneda();
            List<String> lista = manejador.listaMonedas();
            foreach (Object moneda in lista) {
                monedaDd.Items.Add(new ListItem(moneda.ToString()));
            }
            Session["idMonedaCompra"] = monedaDd.Items[0].Value;
        }

        private void cargarMaterialesDrop() {
            BLManejadorMateriales manejador = new BLManejadorMateriales();
            List<String> lista = manejador.buscarMat();
            //bodDD.Items.Insert(0, new ListItem("--SELECCIONE UNA BODEGA--"));
            foreach (Object material in lista) {
                materialesDd.Items.Add(new ListItem(material.ToString()));
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


        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void bodegasDd_SelectedIndexChanged(object sender, EventArgs e) {
            Session["idBodegaCompra"] = bodegasDd.SelectedValue;
            actualizarCantidad();
        }

        protected void monedaDd_SelectedIndexChanged(object sender, EventArgs e) {
            Session["idMonedaCompra"] = monedaDd.SelectedValue;
            BLManejadorMoneda mon = new BLManejadorMoneda();
            BLMoneda mone = mon.consultarAdmin(monedaDd.SelectedValue);
            //if (!mone.idMoneda.Equals("CRC")) {
                //convertirMonedas(mone);
            //}
        }

        //private void convertirMonedas(BLMoneda mone) {
        //    if (mone.idMoneda.Equals("CRC")) {
        //        precioCV.Text = Convert.ToString(mone.idMoneda );
        //    }



        //    if (!string.IsNullOrEmpty(precioCV.Text.Trim())) { 
        //        double precioCVt = Convert.ToDouble(precioCV.Text.Trim());
        //        precioCV.Text = Convert.ToString(precioCVt * conversion);
        //    }
        //    if (!string.IsNullOrEmpty(precioTotal.Text.Trim())) {
        //        double preciototalt = Convert.ToDouble(precioTotal.Text.Trim());
        //        precioTotal.Text = Convert.ToString(preciototalt * conversion);
        //    }
        //    if (!string.IsNullOrEmpty(subtotalTb.Text.Trim())) {
        //        double subtotalt = Convert.ToDouble(subtotalTb.Text.Trim());
        //        subtotalTb.Text = Convert.ToString(subtotalt * conversion);
        //    }
        //    if (!string.IsNullOrEmpty(totalTb.Text.Trim())) {
        //        double totalt = Convert.ToDouble(totalTb.Text.Trim());
        //        totalTb.Text = Convert.ToString(totalt * conversion);
        //    }
        //}

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
        }

        protected void agregarBtn_Click(object sender, EventArgs e) {
            double cantidadCVt = 0;
            double preciolinea = 0;
            double impuesto = 0;
            double descuento = 0;
            if (!consultarSiExisteEnTabla(Convert.ToString(Session["materialSeleccionado"]))) {
                if (!String.IsNullOrEmpty(cantidadVC.Text.Trim()) && double.TryParse(cantidadVC.Text.Trim(), out cantidadCVt)) {
                    if (!(cantidadCVt <= 0)) {
                        if (Convert.ToString(Session["tipoFactura"]).Equals("venta")) {
                            if (!(cantidadCVt > consultarStock())) {
                                if (!String.IsNullOrEmpty(precioCV.Text.Trim())) {
                                    if (double.TryParse(precioCV.Text.Trim(), out preciolinea)) {
                                        if (!(preciolinea <= 0)) {
                                            //impuesto + descuento
                                            if (!String.IsNullOrEmpty(impuestoTb.Text.Trim())) {
                                                if (double.TryParse(impuestoTb.Text.Trim(), out impuesto)) {
                                                    if (!(impuesto < 0) && !(impuesto > 100)) {
                                                        if (!String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                            if (double.TryParse(descuentoTb.Text.Trim(), out descuento)) {
                                                                if (!(descuento < 0) && !(descuento > 100)) {

                                                                } else {
                                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                    lblError.Visible = true;
                                                                }
                                                            } else {
                                                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                lblError.Visible = true;
                                                            }
                                                        }
                                                    } else {
                                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                        lblError.Visible = true;
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                }
                                            }
                                            //solo descuento
                                            if (!String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                if (double.TryParse(descuentoTb.Text.Trim(), out descuento)) {
                                                    if (!(descuento < 0) && !(descuento > 100)) {
                                                        if (String.IsNullOrEmpty(impuestoTb.Text.Trim())) {
                                                            impuesto = 0;

                                                        }
                                                    } else {
                                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                        lblError.Visible = true;
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                }
                                            }
                                            //solo impuesto
                                            if (!String.IsNullOrEmpty(impuestoTb.Text.Trim())) {
                                                if (double.TryParse(impuestoTb.Text.Trim(), out impuesto)) {
                                                    if (!(impuesto < 0) && !(impuesto > 100)) {
                                                        if (String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                            descuento = 0;

                                                        }
                                                    } else {
                                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                        lblError.Visible = true;
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                }
                                            }
                                        } else {
                                            lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio es un número menor o igual a 0. Utilice solo números positivos.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                            lblError.Visible = true;
                                        }
                                    } else {
                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio contienen caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                        lblError.Visible = true;
                                    }
                                } else {
                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio no puede ir en blanco. El precio minimo es 1.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                    lblError.Visible = true;
                                }
                            } else {
                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se puede vender una cantidad mayor al stock de bodega de este material.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                lblError.Visible = true;
                            }
                        } else {
                            if (!String.IsNullOrEmpty(precioCV.Text.Trim())) {
                                if (double.TryParse(precioCV.Text.Trim(), out preciolinea)) {
                                    if (!(preciolinea < 0)) {
                                        if (!String.IsNullOrEmpty(impuestoTb.Text.Trim())) {
                                            if (double.TryParse(impuestoTb.Text.Trim(), out impuesto)) {
                                                if (!(impuesto < 0) && !(impuesto > 100)) {
                                                    if (!String.IsNullOrEmpty(descuentoTb.Text.Trim())) {
                                                        if (double.TryParse(descuentoTb.Text.Trim(), out descuento)) {
                                                            if (!(descuento < 0) && !(descuento > 100)) {

                                                            } else {
                                                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                                lblError.Visible = true;
                                                            }
                                                        } else {
                                                            lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El descuento contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                            lblError.Visible = true;
                                                        }
                                                    }
                                                } else {
                                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto debe ser un porcentaje entre 0 y 100.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                    lblError.Visible = true;
                                                }
                                            } else {
                                                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El impuesto contiene caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                                lblError.Visible = true;
                                            }
                                        }
                                    } else {
                                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio es un número menor a 0. Utilice solo números positivos.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                        lblError.Visible = true;
                                    }
                                } else {
                                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El precio contienen caracteres que no son números.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                                    lblError.Visible = true;
                                }
                            }
                        }
                    } else {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> La cantidad de compra/venta es menor a 0. Utilice solo números positivos<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                } else {
                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> El control cantidad esta vacío o tiene caracteres que no son número.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
            } else {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> Ya se encuentro el material seleccionado en la lista de compra/venta.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }

        }

        private Boolean consultarSiExisteEnTabla(String mate) {
            //codigo, cantidad, precio, impueto, descuento, precio linea
            List<List<String>> lista = (List<List<String>>)(Session["listaCompra"]);
            if (lista != null) {
                foreach (List<String> item in lista) {
                    if (item.ElementAt(0).Equals(mate)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}