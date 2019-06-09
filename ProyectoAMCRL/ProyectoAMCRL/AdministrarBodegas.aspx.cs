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

namespace ProyectoAMCRL {
    public partial class BusquedaBodegas : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

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
            gridBodegas.HeaderRow.Cells[1].Text = "Código Bodega";
            gridBodegas.HeaderRow.Cells[2].Text = "Nombre Bodega";
            gridBodegas.HeaderRow.Cells[3].Text = "Ubicación";
            gridBodegas.HeaderRow.Cells[4].Text = "Estado";

            foreach(GridViewRow row in gridBodegas.Rows) {
                LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
                lb.Text = "Abrir";
            }
        }

        protected void palabraTb_TextChanged(object sender, EventArgs e) {
            this.buscar();
        }

        protected void gridBodegas_SelectedIndexChanged(object sender, EventArgs e) {
            string id = gridBodegas.SelectedRow.Cells[1].Text;
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
    }
}