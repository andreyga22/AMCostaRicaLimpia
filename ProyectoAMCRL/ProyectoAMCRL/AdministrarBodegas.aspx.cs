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
            //Ejemplo de lo que se debe poner en el load de cada pagina
            //*************************************************************
            if(Session["cuentaLogin"] != null) {
                if(!this.IsPostBack) {
                    this.buscar();
                }
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        protected void gridBodegas_PageIndexChanging(object sender, GridViewPageEventArgs e) {

            gridBodegas.PageIndex = e.NewPageIndex;
            this.buscar();
        }


        protected void gridBodegas_Sorting(object sender, GridViewSortEventArgs e) {

        }


        private void buscar() {
            BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
            if(usuarioLogin.rol.Equals("r")) {
                BLManejadorBodega man = new BLManejadorBodega();
                gridBodegas.DataSource = man.buscar(palabraTb.Text.Trim());
                gridBodegas.DataBind();
                gridBodegas.HeaderRow.Cells[0].Text = "Código Bodega";
                gridBodegas.HeaderRow.Cells[1].Text = "Nombre Bodega";
                gridBodegas.HeaderRow.Cells[2].Text = "Ubicación";
            } else {
                BLManejadorBodega man = new BLManejadorBodega();
                gridBodegas.DataSource = man.buscarAdmin(palabraTb.Text.Trim());
                gridBodegas.DataBind();
                gridBodegas.HeaderRow.Cells[0].Text = "Código Bodega";
                gridBodegas.HeaderRow.Cells[1].Text = "Nombre Bodega";
                gridBodegas.HeaderRow.Cells[2].Text = "Ubicación";
                gridBodegas.HeaderRow.Cells[3].Text = "Estado";
            }

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