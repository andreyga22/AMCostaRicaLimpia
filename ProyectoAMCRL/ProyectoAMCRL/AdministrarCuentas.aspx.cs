using System;
using System.Collections.Generic;
using System.Data;
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

        /*Ordenar: el if del viewstate es necesario*/

        /// <summary>
        /// Carga todos los componentes de la pantalla. 
        ///Reisa si hay un usuario en sesión para permitir o negar la carga
        ///de la página.En caso de negarlo vuelve al login.
        ///Carga la tabla con los datos de las cuentas.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            try { 
            if(Session["cuentaLogin"] != null) {
                if(!this.IsPostBack) {
                    if(ViewState["sorting"] == null) {
                        this.buscar();
                    }
                }
            } else {
                Response.Redirect("Login.aspx");
            }
            } catch(Exception) {

            }
        }

        /// <summary>
        /// Permite la busqueda en el campo de busqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void palabraTb_TextChanged(object sender, EventArgs e) {
            this.buscar();
        }

        /// <summary>
        /// Método que maneja el evento de dar click en el boton "nuevo" de la página.
        ///Este pone la variable sesión idCuenta en null y redirige a la página Cuenta.aspx
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        protected void btnAgregar_Click(object sender, EventArgs e) {
            Session["idCuenta"] = null;
            Session["accionCuenta"] = 0;
            Response.Redirect("Cuenta.aspx");
        }

        /*
         Ordenar: es necesario el if de session
         */

        /// <summary>
        /// Evento que permite el cambio de páginas de la tabla de cuentas. Se actualiza la tabla después del cambio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridCuentas_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gridCuentas.PageIndex = e.NewPageIndex;
            this.buscar();
            if(Session["SortedView"] != null) {
                gridCuentas.DataSource = Session["SortedView"];
                gridCuentas.DataBind();
            }
        }

        /*
         ordenar: se ocupa todo
             */

        /// <summary>
        /// Método que se encarga de ordenar el gridview de Cuentas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridCuentas_Sorting(object sender, GridViewSortEventArgs e) {
            DataTable datat = this.buscar();
            DataView dv = new DataView(datat);
            if(ViewState["sorting"] == null || ViewState["sorting"].ToString() == "DESC") {
                dv.Sort = e.SortExpression + " ASC";
                ViewState["sorting"] = "ASC";
                //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortasc";
                
            } else {
                if(ViewState["sorting"].ToString() == "ASC") {
                    dv.Sort = e.SortExpression + " DESC";
                    ViewState["sorting"] = "DESC";
                    //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortdesc";
                }
            }
            Session["sortedView"] = dv;
            gridCuentas.DataSource = dv;
            gridCuentas.DataBind();

           
            if(ViewState["sorting"].ToString() == "ASC") {
                int index = GetColumnIndex(datat, e.SortExpression);
                gridCuentas.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
            } else {
                int index = GetColumnIndex(datat, e.SortExpression);
                gridCuentas.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
            }

        }

        /*
         Ordenar: se ocupa todo
             */
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
             /// <returns></returns>
        private DataTable buscar() {
            BLManejadorCuentas man = new BLManejadorCuentas();
            DataTable datat = man.buscar(palabraTb.Text.Trim());
            gridCuentas.DataSource = datat;
            gridCuentas.DataBind();
            //gridCuentas.HeaderRow.Cells[0].Text = "Identificador";
            //gridCuentas.HeaderRow.Cells[1].Text = "Nombre Usuario";
            //gridCuentas.HeaderRow.Cells[2].Text = "Rol";
            //gridCuentas.HeaderRow.Cells[3].Text = "Estado";
            return datat;
        }

        /// <summary>
        ///  Método que permite al usuario dar clic en cualquier lugar de las filas de la tabla cuentas para
        ///visualizar su contenido completo.
        ///Redirecciona a la pagina Cuenta.aspx
             /// </summary>
             /// <param name="sender"></param>
             /// <param name="e"></param>
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
        /// Método que enlaza el clic en la fila de la tabla bodegas con el evento de selectedindexchanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridCuentas_RowDataBound(object sender, GridViewRowEventArgs e) {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridCuentas, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

    }
}