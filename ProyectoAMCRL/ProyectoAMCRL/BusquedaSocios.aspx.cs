using BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace ProyectoAMCRL
{
    public partial class BusquedaSocios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void gridSocios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

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

        private void buscar()
        {
            BLManejadorSocios manejador = new BLManejadorSocios();
            String filtro = "";


            
            gridSocios.DataSource = manejador.buscarFiltrado(filtro);
            gridSocios.DataBind();
            gridSocios.HeaderRow.Cells[0].Text = "Cedula";
            gridSocios.HeaderRow.Cells[1].Text = "Nombre";
            gridSocios.HeaderRow.Cells[2].Text = "Primer apellido";
            gridSocios.HeaderRow.Cells[3].Text = "Segundo Apellido";
            gridSocios.HeaderRow.Cells[4].Text = "Rol";

        }
    }
}