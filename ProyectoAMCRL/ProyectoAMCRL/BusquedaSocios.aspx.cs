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
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace ProyectoAMCRL
{
    public partial class BusquedaSocios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["cuentaLogin"] != null)
            {
                if (!this.IsPostBack)
                {
                    this.buscar();
                    Session["idSocio"] = "";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void buscar(List<BLSocioNegocio> listSocios)
        {
            if (listSocios.Count != 0)
            {
                gridSocios.DataSource = listSocios;
            }
            else
            {
                BLManejadorSocios manejador = new BLManejadorSocios();
                List<BLSocioNegocio> list = manejador.listaSoc(txtPalabra.Text.Trim()); 

                gridSocios.DataSource = list;
            }
            gridSocios.DataBind();
            cargarEncabezados();
        }

        private void buscar()
        {
            BLManejadorSocios manejador = new BLManejadorSocios();
            DataTable tabla = manejador.buscarDatos(txtPalabra.Text.Trim());
            gridSocios.DataSource = tabla;
            gridSocios.DataBind();
            cargarEncabezados();
        }

        private void cargarEncabezados()
        {
            gridSocios.HeaderRow.Cells[0].Text = "Cedula";
            gridSocios.HeaderRow.Cells[1].Text = "Nombre";
            gridSocios.HeaderRow.Cells[2].Text = "Primer apellido";
            gridSocios.HeaderRow.Cells[3].Text = "Segundo Apellido";
            gridSocios.HeaderRow.Cells[4].Text = "Rol";

        }

        protected void gridSocios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridSocios.PageIndex = e.NewPageIndex;
            this.buscar();
        }

        protected void gridSocios_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gridSocios_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridSocios.Rows)
            {
                if (row.RowIndex == gridSocios.SelectedIndex)
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
            string id = gridSocios.SelectedRow.Cells[0].Text;
            Session["idSocio"] = id;
            Response.Redirect("RegistroSociosUI.aspx");
        }

        protected void gridSocios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridSocios, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

        }


        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

            
        }

        protected void txtPalabra_TextChanged(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                this.buscar();
            }
        }


    }
}