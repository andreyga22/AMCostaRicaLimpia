using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BL;

namespace ProyectoAMCRL {
    public partial class UnidadesMedida : System.Web.UI.Page {
        /// <summary>
        /// Carga todos los componentes de la pantalla. 
        ///Reisa si hay un usuario en sesión para permitir o negar la carga
        ///de la página.En caso de negarlo vuelve al login.
        ///Carga la tabla con los datos de las unidades de medida.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            if(Session["cuentaLogin"] != null) {
                if(!this.IsPostBack) {
                    if(ViewState["sorting"] == null) {
                        this.buscar();
                    }
                }
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        /// <summary>
        /// Evento que permite el cambio de páginas de la tabla de unidades de medida.
        /// Se actualiza la tabla después del cambio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridUnidades_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gridUnidades.PageIndex = e.NewPageIndex;
            this.buscar();
            if(Session["SortedView"] != null) {
                gridUnidades.DataSource = Session["SortedView"];
                gridUnidades.DataBind();
            }
        }

        /// <summary>
        /// Método que permite al usuario dar clic en cualquier lugar de las filas de la tabla unidades para
        ///visualizar su contenido completo.
        ///Redirecciona a la pagina UnidadMedida.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridUnidades_SelectedIndexChanged(object sender, EventArgs e) {
            foreach(GridViewRow row in gridUnidades.Rows) {
                if(row.RowIndex == gridUnidades.SelectedIndex) {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                } else {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Clic para abrir.";
                }
            }

            string id = gridUnidades.SelectedRow.Cells[0].Text;
            Session["idUnidad"] = id;
            Response.Redirect("UnidadMedida.aspx");
        }

        /// <summary>
        /// Método que se encarga de ordenar el gridview de unidades de medida.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridUnidades_Sorting(object sender, GridViewSortEventArgs e) {
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
            gridUnidades.DataSource = dv;
            gridUnidades.DataBind();


            if(ViewState["sorting"].ToString() == "ASC") {
                int index = GetColumnIndex(datat, e.SortExpression);
                gridUnidades.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
            } else {
                int index = GetColumnIndex(datat, e.SortExpression);
                gridUnidades.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
            }
        }

        /// <summary>
        /// Método que enlaza el clic en la fila de la tabla unidades con el evento de selectedindexchanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridUnidades_RowDataBound(object sender, GridViewRowEventArgs e) {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridUnidades, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

        /// <summary>
        /// Devuelve el indice de una columna en un datatable enviado, utilizando el nombre como parametro.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetColumnIndex(DataTable dt, string name) {
            return dt.Columns.IndexOf(name);
        }

        /// <summary>
        /// Método que permite la función de busqueda para la tabla. Consulta la base de datos en caso de 
        ///que hayan coincidencias con la palabra buscada.
        ///Se cargan dos tipos diferentes de datos en la tabla dependiendo del rol del usuario en sesión.
        /// </summary>
        /// <returns>Datatable con elr esultado de la busqueda</returns>
        private DataTable buscar() {
            BLManejadorUnidad man = new BLManejadorUnidad();
            DataTable datat = man.buscar(palabraTb.Text.Trim());
            gridUnidades.DataSource = datat;
            gridUnidades.DataBind();
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

        /// <summary>
        /// Permite la busqueda en el campo de busqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void palabraTb_TextChanged1(object sender, EventArgs e) {
            this.buscar();
        }

        /// <summary>
        /// Método que maneja el evento de dar click en el boton "nuevo" de la página.
        ///Este pone la variable sesión idUnidad en null y redirige a la página UnidadMedida.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregar_Click(object sender, EventArgs e) {
            Session["idUnidad"] = null;
            Response.Redirect("UnidadMedida.aspx");
        }
    }
}