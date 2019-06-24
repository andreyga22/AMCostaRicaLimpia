using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ProyectoAMCRL
{
    public partial class BusquedaFacturas : System.Web.UI.Page
    {

        /*
            Revisa si hay un usuario en sesión para permitir o negar la carga 
            de la página. En caso de negarlo vuelve al login.
            Carga los materiales que se van a utilizar en el filtro
            Carga la tabla con las facturas existentes.
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cuentaLogin"] != null)
            {
                if (!this.IsPostBack)
                {
                    this.buscar();
                    cargarMateriales();
                    Session["idFactura"] = "";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        /*
         * Se cargan los materiales utilizados en el filtro
         */
        private void cargarMateriales()
        {
            BLManejadorMateriales manej = new BLManejadorMateriales();
            DataSet listaMateriales = manej.listarMaterialesBL();


            foreach (DataRow dr in listaMateriales.Tables[0].Rows)
            {
                ListItem item = new ListItem();

                String codigo = Convert.ToString(dr["COD_MATERIAL"]);
                String nombre = Convert.ToString(dr["NOMBRE_MATERIAL"]);
                item.Text = nombre;
                item.Value = codigo;
                materialesCB.Items.Add(item);
            }
        }

        /*
         * Carga la tabla de facturas con los filtros realizados
         * Entradas:
             listFacturas: Lista de facturas con el filtro
         */
        private void buscar(List<BLFactura> listFacturas)
        {
            if (listFacturas.Count != 0)
            {
                gridFacturas.DataSource = listFacturas;
            }
            else
            {
                BLManejadorFacturas man = new BLManejadorFacturas();
                List<BLFactura> list = man.listaFact(txtPalabra.Text.Trim());

                gridFacturas.DataSource = list;
            }
            gridFacturas.DataBind();
            cargarEncabezados();
        }

        /*
         * Carga la tabla y la muestra en la pantalla
         */
        private void buscar()
        {
            BLManejadorFacturas man = new BLManejadorFacturas();
            DataTable tabla = man.buscar(txtPalabra.Text.Trim());
            gridFacturas.DataSource = tabla;
            gridFacturas.DataBind();
            cargarEncabezados();
        }

        /*
         * Carga los encabezados de la tabla
         */
        private void cargarEncabezados()
        {
            gridFacturas.HeaderRow.Cells[0].Text = "Código Factura";
            gridFacturas.HeaderRow.Cells[1].Text = "Bodega";
            gridFacturas.HeaderRow.Cells[2].Text = "Moneda";
            gridFacturas.HeaderRow.Cells[3].Text = "Cédula";
            gridFacturas.HeaderRow.Cells[4].Text = "Monto";
            gridFacturas.HeaderRow.Cells[5].Text = "Fecha";
            gridFacturas.HeaderRow.Cells[6].Text = "Tipo Factura";
            gridFacturas.HeaderRow.Cells[7].Text = "Socio";

            gridFacturas.HeaderRow.Cells[1].Visible = false;
            for (int i = 0; i < gridFacturas.Rows.Count; i++)
            {
                gridFacturas.Rows[i].Cells[1].Visible = false;
            }

            gridFacturas.HeaderRow.Cells[2].Visible = false;
            for (int i = 0; i < gridFacturas.Rows.Count; i++)
            {
                gridFacturas.Rows[i].Cells[2].Visible = false;
            }

            gridFacturas.HeaderRow.Cells[6].Visible = false;
            for (int i = 0; i < gridFacturas.Rows.Count; i++)
            {
                gridFacturas.Rows[i].Cells[6].Visible = false;
            }
        }


        /*
         * Evento que permite el cambio de páginas de la tabla de facturas. 
         * Se actualiza la tabla después del cambio.
       */
        protected void gridFact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridFacturas.PageIndex = e.NewPageIndex;
            this.buscar(new List<BLFactura>());
        }

        /*    
         * Método que permite al usuario dar clic en cualquier lugar de las filas de la tabla facturas para
         visualizar su contenido completo.
         Redirecciona a la pagina Compra_Venta.aspx
         */
        protected void gridFact_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gridFacturas.Rows)
                {
                    if (row.RowIndex == gridFacturas.SelectedIndex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                        row.ToolTip = string.Empty;
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                        row.ToolTip = "Clic para abrir.";
                    }
                }
                string id = gridFacturas.SelectedRow.Cells[0].Text;
                Session["idFactura"] = id;
                Response.Redirect("Compra_Venta.aspx");
            }
            catch (Exception)
            {
                //lblMensaje.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo seleccionar el empleado.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                //lblMensaje.Visible = true;
            }
        }


        /*
         * Método que enlaza el clic en la fila de la tabla cuentas con el evento de selectedindexchanging
        */
        protected void gridFact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridFacturas, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

        /*
         * Evento que permite cargar la tabla con las opciones que se eligieron.
         */
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            BLManejadorFacturas manej = new BLManejadorFacturas();

            String fechaIni = "";
            String fechFin = "";
            String tipoF = "";
            String montoMaximoF = "";
            String montoMinimoF = "";
            List<String> materiales = new List<string>();

            if (!String.IsNullOrEmpty(fechaInicio.Text))
                fechaIni = fechaInicio.Text;
            if (!String.IsNullOrEmpty(fechaFin.Text))
                fechFin = fechaFin.Text;
            if (tipoRadioL.SelectedItem != null && tipoRadioL.SelectedItem != tipoRadioL.Items[0])
                tipoF = tipoRadioL.SelectedItem.Text;
            if (!String.IsNullOrEmpty(montoMaximo.Text))
                montoMaximoF = montoMaximo.Text;
            if (!String.IsNullOrEmpty(montoMinimo.Text))
                montoMinimoF = montoMinimo.Text;

            foreach (ListItem material in materialesCB.Items)
            {

                if (material.Selected)
                    materiales.Add(material.Value);

            }
            BLManejadorFacturas blFact = new BLManejadorFacturas();
            //List<BLFactura> listaFiltrada = blFact.filtrarFacturas(fechaIni, fechFin, tipoF, montoMaximoF, montoMinimoF, materiales);
            DataSet datSet = blFact.filtrarFacturas(fechaIni, fechFin, tipoF, montoMaximoF, montoMinimoF, materiales);
            List<BLFactura> listaFiltrada = new List<BLFactura>();

            foreach (DataRow dr in datSet.Tables[0].Rows)
            {


                BLFactura fac = new BLFactura(Convert.ToInt32(dr["COD_FACTURA"]), Convert.ToString(dr["CEDULA"]), Convert.ToString(dr["ID_BODEGA"]),
                     Convert.ToString(dr["ID_MONEDA"]), Convert.ToDouble(dr["MONTO_TOTAL"]), Convert.ToDateTime(dr["FECHA_FACTURA"]), Convert.ToString(dr["SOCIO"]));
                listaFiltrada.Add(fac);

            }


            buscar(listaFiltrada);

            //Filtro montos
            //List<BLFactura> listaFiltrada = new List<BLFactura>();
            //if ((!String.IsNullOrEmpty(montoMinimo.Text) || (!String.IsNullOrWhiteSpace(montoMinimo.Text))) &&
            //    (!String.IsNullOrEmpty(montoMaximo.Text) || (!String.IsNullOrWhiteSpace(montoMaximo.Text))))
            //{
            //    listaFiltrada = filtrarMonto(Convert.ToDouble(montoMinimo.Text), Convert.ToDouble(montoMaximo.Text));
            //    buscar(listaFiltrada);
            //}


            //Filtro Fecha
            //if ((!String.IsNullOrEmpty(fechaInicio.Text.Trim()) || (!String.IsNullOrWhiteSpace(fechaInicio.Text.Trim()))) &&
            //    (!String.IsNullOrEmpty(fechaFin.Text.Trim()) || (!String.IsNullOrWhiteSpace(fechaFin.Text.Trim()))))
            //{

            //    List<BLFactura> rangoFecha = manej.listaRangoFecha(Convert.ToDateTime(fechaInicio.Text.Trim()), Convert.ToDateTime(fechaFin.Text.Trim()));

            //    if (listaFiltrada.Count == 0)
            //    {
            //        foreach (BLFactura blF in rangoFecha)
            //        {
            //            listaFiltrada.Add(blF);
            //        }
            //    }
            //    else
            //    {
            //        for (int j = 0; j < rangoFecha.Count; j++)
            //        {
            //            bool a = true;

            //            for (int i = 0; i < listaFiltrada.Count; i++)
            //            {
            //                if (listaFiltrada[i].cod_Factura.Equals(rangoFecha[j].cod_Factura))
            //                {
            //                    a = false;
            //                }
            //            }
            //            if (a == false)
            //            {
            //                listaFiltrada.Add(rangoFecha[j]);
            //            }
            //        }
            //    }
            //}

            //Filtro tipo
            //if (!tipoRadioL.SelectedValue.ToString().Equals("No especificar"))
            {
                string tipo = "";
                if (tipoRadioL.SelectedValue.ToString().Equals("Venta"))
                {
                    tipo = "v";
                }
                else
                {
                    tipo = "c";
                }

                List<BLFactura> listaTipo = manej.facturasTipo(tipo);
                if (listaFiltrada.Count == 0)
                {
                    foreach (BLFactura blF in listaTipo)
                    {
                        listaFiltrada.Add(blF);
                    }
                }
                else
                {
                    for (int j = 0; j < listaTipo.Count; j++)
                    {
                        bool a = true;

                        for (int i = 0; i < listaFiltrada.Count; i++)
                        {
                            if (listaFiltrada[i].cod_Factura.Equals(listaTipo[j].cod_Factura))
                            {
                                a = false;
                            }
                        }
                        if (a == false)
                        {
                            listaFiltrada.Add(listaTipo[j]);
                        }
                    }
                }
            }


            //Para filtrar por materiales
            //List<BLFactura> listaMat = manej.listaFact("");
            //foreach (BLFactura b in listaMat)
            //{

            //}
        }


        //private List<BLFactura> filtrarMonto(double monto1, double monto2)
        //{
        //    return new BLManejadorFacturas().listaMontos(monto1, monto2);
        //}


        //protected void materialesDrop_SelectedIndexChanged(object sender, EventArgs e)
        // {
        //String idBodega = bodegasDrop.SelectedItem.Value;
        //cargarMateriales(idBodega);
        //}


        /*
         *Permite la busqueda en la tabla al presionar enter en el campo de texto.
        */
        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buscar();
            }
        }

        protected void txtPalabra_TextChanged(object sender, EventArgs e)
        {
            this.buscar();
        }

        protected void gridFact_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

    }
}
