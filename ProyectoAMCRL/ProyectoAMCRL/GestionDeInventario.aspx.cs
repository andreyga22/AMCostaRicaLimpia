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

namespace ProyectoAMCRL {
    public partial class GestionDeInventario : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(Session["cuentaLogin"] != null) {
                if(!this.IsPostBack) {
                    if(ViewState["sorting"] == null) {
                        this.cargarBodegas();
                    }
                }
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        protected void gridInventario_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gridInventario.PageIndex = e.NewPageIndex;
            this.buscar();
            if(Session["SortedView"] != null) {
                gridInventario.DataSource = Session["SortedView"];
                gridInventario.DataBind();
            }
        }

        protected void gridInventario_SelectedIndexChanged(object sender, EventArgs e) {
            foreach(GridViewRow row in gridInventario.Rows) {
                if(row.RowIndex == gridInventario.SelectedIndex) {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                } else {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Clic para abrir.";
                }
            }

            string id = gridInventario.SelectedRow.Cells[0].Text;
            Session["idInventario"] = id;
            Response.Redirect("Monedas.aspx");
        }

        protected void cargarBodegas()
        {
            BLManejadorInventario manejador = new BLManejadorInventario();
            List<String> lista = manejador.buscarBodegas();
            bodDD.Items.Insert(0, new ListItem("--SELECCIONE UNA BODEGA--"));
            foreach (Object bodega in lista){
                bodDD.Items.Add(new ListItem(bodega.ToString()));
            }
        }

        protected void gridInventario_Sorting(object sender, GridViewSortEventArgs e) {
            DataTable datat = this.buscar();
            DataView dv = new DataView(datat);
            if(ViewState["sorting"] == null || ViewState["sorting"].ToString() == "DESC") {
                dv.Sort = e.SortExpression + " ASC";
                ViewState["sorting"] = "ASC";

            } else {
                if(ViewState["sorting"].ToString() == "ASC") {
                    dv.Sort = e.SortExpression + " DESC";
                    ViewState["sorting"] = "DESC";
                }
            }
            Session["sortedView"] = dv;
            gridInventario.DataSource = dv;
            gridInventario.DataBind();


            //if(ViewState["sorting"].ToString() == "ASC") {
            //    int index = GetColumnIndex(datat, e.SortExpression);
            //    gridInventario.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
            //} else {
            //    int index = GetColumnIndex(datat, e.SortExpression);
            //    gridInventario.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
            //}
        }

        protected void gridInventario_RowDataBound(object sender, GridViewRowEventArgs e) {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridInventario, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }


        /// <summary>
        /// Método que permite la función de busqueda para la tabla. Consulta la base de datos en caso de 
        ///que hayan coincidencias con la palabra buscada.
        ///Se cargan dos tipos diferentes de datos en la tabla dependiendo del rol del usuario en sesión.
        /// </summary>
        /// <returns>Datatable con elr esultado de la busqueda</returns>
        private DataTable buscar() {

                BLManejadorInventario man = new BLManejadorInventario();
                DataTable datat = man.buscarStock(bodDD.Text.Trim());
                gridInventario.DataSource = datat;
                gridInventario.DataBind();
                return datat;
        }

        /// <summary>
        /// Permite la busqueda en la tabla al presionar enter en el campo de texto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                this.buscar();
            }
        }

        protected void bodDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}