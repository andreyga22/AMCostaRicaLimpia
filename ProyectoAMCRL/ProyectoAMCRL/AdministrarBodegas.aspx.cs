using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;

namespace ProyectoAMCRL {
    public partial class BusquedaBodegas : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            //BLManejadorBodega man = new BLManejadorBodega();
            //List<BLBodegaTabla> lista = man.listaBodegas();
            //gridBodegas.DataSource = lista;
            //gridBodegas.DataBind();

            if(!this.IsPostBack) {
                this.buscar();
            }
        }

        protected void gridBodegas_PageIndexChanging(object sender, GridViewPageEventArgs e) {

            gridBodegas.PageIndex = e.NewPageIndex;
            this.buscar();
        }


        protected void gridBodegas_Sorting(object sender, GridViewSortEventArgs e) {

        }


        private void buscar() {
            BLManejadorBodega man = new BLManejadorBodega();
            gridBodegas.DataSource = man.buscar(palabraTb.Text.Trim());
            gridBodegas.DataBind();
            gridBodegas.HeaderRow.Cells[0].Text = "Código Bodega";
            gridBodegas.HeaderRow.Cells[1].Text = "Nombre Bodega";
            gridBodegas.HeaderRow.Cells[2].Text = "Ubicación";
            gridBodegas.HeaderRow.Cells[3].Text = "Estado";

            //foreach(GridViewRow row in gridBodegas.Rows) {
            //    LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
            //    lb.Text = "Abrir";
            //}
        }

        protected void palabraTb_TextChanged(object sender, EventArgs e) {
            this.buscar();
        }

        protected void gridBodegas_SelectedIndexChanged(object sender, EventArgs e) {

            foreach(GridViewRow row in gridBodegas.Rows) {
                if(row.RowIndex == gridBodegas.SelectedIndex) {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                } else {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Clic para abrir.";
                }
            }


            string id = gridBodegas.SelectedRow.Cells[0].Text;
            Session["idBodega"] = id;
            Response.Redirect("Bodega.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e) {
            Session["idBodega"] = null;
            Response.Redirect("Bodega.aspx");
        }

        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                this.buscar();
            }
        }

        protected void gridBodegas_RowDataBound(object sender, GridViewRowEventArgs e) {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridBodegas, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

    }
}