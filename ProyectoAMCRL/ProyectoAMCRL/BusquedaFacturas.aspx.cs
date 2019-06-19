﻿using System;
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


        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            if (Session["cuentaLogin"] != null)
            {
                if (!this.IsPostBack)
                {
                    this.buscar();
                    cargarEncabezados2();
                    cargarMateriales();
                    Session["idFactura"] = "";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        //Se cargan los materiales utilizados en el filtro
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
                materialDda.Items.Add(item);
            }
        }

        //Busca en DAO y retorna un DataTable
        private void buscar()
        {
            BLManejadorFacturas man = new BLManejadorFacturas();
            List<BLFactura> list = man.facturasVentas(txtPalabra.Text.Trim());

            gridFacturas.DataSource = list;

            gridFacturas.DataBind();
            gridFacturas.Visible = true;
        }

        //Carga los encabezados de la tabla separando el nombre y apellidos
        private void cargarEncabezados()
        {
            gridFacturas.HeaderRow.Cells[0].Text = "Código Factura";
            gridFacturas.HeaderRow.Cells[1].Text = "Bodega";
            gridFacturas.HeaderRow.Cells[2].Text = "Moneda";
            gridFacturas.HeaderRow.Cells[3].Text = "Cédula";
            gridFacturas.HeaderRow.Cells[4].Text = "Monto";
            gridFacturas.HeaderRow.Cells[5].Text = "Fecha";
            gridFacturas.HeaderRow.Cells[6].Text = "Tipo Factura";
            gridFacturas.HeaderRow.Cells[7].Text = "Nombre";
            gridFacturas.HeaderRow.Cells[8].Text = "Primer Apellido";
            gridFacturas.HeaderRow.Cells[9].Text = "Segundo Apellido";

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

        //Carga los encabezados de la tabla sin separar el nombre y apellidos
        private void cargarEncabezados2()
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

        protected void gridFact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridFacturas.PageIndex = e.NewPageIndex;
            this.buscar();
        }


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
                Response.Redirect("Factura.aspx");
            }
            catch (Exception)
            {
                //lblMensaje.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo seleccionar el empleado.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                //lblMensaje.Visible = true;
            }
        }

        protected void gridFact_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gridFact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridFacturas, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }


        //boton de filtrar
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            BLManejadorFacturas manej = new BLManejadorFacturas();

            //Filtro montos
            List<BLFactura> listaFiltrada = new List<BLFactura>();
            if ((!String.IsNullOrEmpty(montoMinimo.Text) || (!String.IsNullOrWhiteSpace(montoMinimo.Text))) &&
                (!String.IsNullOrEmpty(montoMax.Text) || (!String.IsNullOrWhiteSpace(montoMax.Text))))
            {
                listaFiltrada = filtrarMonto(Convert.ToDouble(montoMinimo.Text), Convert.ToDouble(montoMax.Text));
                cargarGridFiltrada(listaFiltrada);
                cargarEncabezados2();
            }

            //Filtro palabras
            if ((!String.IsNullOrEmpty(txtPalabra.Text) || (!String.IsNullOrWhiteSpace(txtPalabra.Text))))
            {
                cargarEncabezados2();
                cargarGridFiltrada_Table(manej.buscar(txtPalabra.Text.Trim()));
            }

            //Filtro Fecha
            if ((!String.IsNullOrEmpty(datepicker.Value) || (!String.IsNullOrWhiteSpace(datepicker.Value))) &&
                (!String.IsNullOrEmpty(datepicker2.Value) || (!String.IsNullOrWhiteSpace(datepicker2.Value))))
            {
                List <BLFactura> rangoFecha = manej.listaRangoFecha(Convert.ToDateTime(datepicker.Value), Convert.ToDateTime(datepicker2.Value));

                if (listaFiltrada.Count == 0)
                {
                    foreach (BLFactura blF in rangoFecha)
                    {
                        listaFiltrada.Add(blF);
                    }
                }
                else
                {
                    for (int j = 0; j < rangoFecha.Count; j++)
                    {
                        bool a = true;

                        for (int i = 0; i < listaFiltrada.Count; i++)
                        {
                            if (listaFiltrada[i].cod_Factura.Equals(rangoFecha[j].cod_Factura))
                            {
                                a = false;
                            }
                        }
                        if (a == false)
                        {
                            listaFiltrada.Add(rangoFecha[j]);
                        }
                    }
                }
            }

            //Para filtrar por materiales
            List<BLFactura> listaMat = manej.facturasVentas("");
            foreach(BLFactura b in listaMat)
            {

            }

            //Filtro tipo
            if (radioRol.Checked || radioRol2.Checked)
            {
                string tipo = "";
                if(radioRol.Checked)
                {
                    tipo = "v";
                } else
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


            //Final
            if (listaFiltrada.Count == 0)
            {
                //cambiar por mensaje de error
                //cargarGridFiltrada(listaFiltrada);
            }
            else
            {
                cargarGridFiltrada(listaFiltrada);
            }

            //para el de nombre completo
            cargarEncabezados2();
        }

        private List<BLFactura> filtrarMonto(double monto1, double monto2)
        {
            return new BLManejadorFacturas().listaMontos(monto1, monto2);
        }

        private void cargarGridFiltrada(List<BLFactura> listFacturas)
        {
            gridFacturas.DataSource = listFacturas;
            gridFacturas.DataBind();
            cargarEncabezados2();
            gridFacturas.Visible = true;
        }

        private void cargarGridFiltrada_Table(DataTable list)
        {
            gridFacturas.DataSource = list;
            gridFacturas.DataBind();
            cargarEncabezados2();
        }

        protected void materialesDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            //String idBodega = bodegasDrop.SelectedItem.Value;
            //cargarMateriales(idBodega);
        }

        protected void palabraTb_TextChanged(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buscar();
            }
        }
    }
}