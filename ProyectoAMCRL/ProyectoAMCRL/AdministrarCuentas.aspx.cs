using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Input;
using BL;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace ProyectoAMCRL {
    public partial class CuentasUsuario : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(Session["cuentaLogin"] != null) {
                if(!this.IsPostBack) {
                    this.buscar();
                }
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        protected void palabraTb_TextChanged(object sender, EventArgs e) {
            this.buscar();
        }

        protected void btnAgregar_Click(object sender, EventArgs e) {
            Session["idCuenta"] = null;
            Session["accionCuenta"] = 0;
            Response.Redirect("Cuenta.aspx");
        }

        protected void gridCuentas_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gridCuentas.PageIndex = e.NewPageIndex;
            this.buscar();
        }


        protected void gridCuentas_Sorting(object sender, GridViewSortEventArgs e) {

        }

        private void buscar() {
            BLManejadorCuentas man = new BLManejadorCuentas();
            gridCuentas.DataSource = man.buscar(palabraTb.Text.Trim());
            gridCuentas.DataBind();
            gridCuentas.HeaderRow.Cells[0].Text = "Identificador";
            gridCuentas.HeaderRow.Cells[1].Text = "Nombre Usuario";
            gridCuentas.HeaderRow.Cells[2].Text = "Rol";
            gridCuentas.HeaderRow.Cells[3].Text = "Estado";

            //foreach(GridViewRow row in gridCuentas.Rows) {
            //    LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
            //    lb.Text = "Abrir";
            //}
        }

        protected void gridCuentas_SelectedIndexChanged(object sender, EventArgs e) {
            foreach(GridViewRow row in gridCuentas.Rows) {
                if(row.RowIndex == gridCuentas.SelectedIndex) {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                } else {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Clic para abrir.";
                }
            }

            string id = gridCuentas.SelectedRow.Cells[0].Text;
            Session["idCuenta"] = id;
            Session["accionCuenta"] = 1;
            Response.Redirect("Cuenta.aspx");
        }

        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                this.buscar();
            }
        }

        protected void gridCuentas_RowDataBound(object sender, GridViewRowEventArgs e) {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridCuentas, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }
    }
}