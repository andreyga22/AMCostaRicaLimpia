using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL {
    public partial class CuentasUsuario : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!this.IsPostBack) {
                this.buscar();
            }
        }

        protected void palabraTb_TextChanged(object sender, EventArgs e) {
            this.buscar();
        }

        protected void btnAgregar_Click(object sender, EventArgs e) {
            Session["idCuenta"] = null;
            Response.Redirect("Bodega.aspx");
        }

        protected void gridCuentas_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gridCuentas.PageIndex = e.NewPageIndex;
            this.buscar();
        }

        protected void gridCuentas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {
            string id = gridCuentas.SelectedRow.Cells[1].Text;
            Session["idBodega"] = id;
            Response.Redirect("Bodega.aspx");
        }

        protected void gridCuentas_Sorting(object sender, GridViewSortEventArgs e) {

        }

        private void buscar() {
            BLManejadorBodega man = new BLManejadorBodega();
            gridCuentas.DataSource = man.buscar(palabraTb.Text.Trim());
            gridCuentas.DataBind();
            gridCuentas.HeaderRow.Cells[1].Text = "Identificador";
            gridCuentas.HeaderRow.Cells[2].Text = "Nombre Usuario";
            gridCuentas.HeaderRow.Cells[3].Text = "Rol";
            gridCuentas.HeaderRow.Cells[4].Text = "Estado";

            foreach(GridViewRow row in gridCuentas.Rows) {
                LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
                lb.Text = "Abrir";
            }
        }
    }
}