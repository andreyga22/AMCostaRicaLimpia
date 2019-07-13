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
        /// <summary>
        /// Carga todos los componentes de la pantalla. 
        ///Reisa si hay un usuario en sesión para permitir o negar la carga
        ///de la página.En caso de negarlo vuelve al login.
        ///Carga la tabla con los datos de las bodegas.
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
        /// Evento que permite el cambio de páginas de la tabla de bodegas. Se actualiza la tabla después del cambio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridBodegas_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gridBodegas.PageIndex = e.NewPageIndex;
                this.buscar();
                if(Session["SortedView"] != null) {
                    gridBodegas.DataSource = Session["SortedView"];
                    gridBodegas.DataBind();
                }
            } catch(Exception) {

            }
        }

        /// <summary>
        /// Método que se encarga de ordenar el gridview de bodegas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridBodegas_Sorting(object sender, GridViewSortEventArgs e) {
            try {
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
                gridBodegas.DataSource = dv;
                gridBodegas.DataBind();


                if(ViewState["sorting"].ToString() == "ASC") {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridBodegas.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
                } else {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridBodegas.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
                }
            } catch(Exception) {

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
        /// <returns></returns>
        private DataTable buscar() {
            try {
                BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
                if(usuarioLogin.rol.Equals("r")) {
                    BLManejadorBodega man = new BLManejadorBodega();
                    DataTable datat = man.buscar(palabraTb.Text.Trim());
                    gridBodegas.DataSource = datat;
                    gridBodegas.DataBind();
                  
                    return datat;
                } else {
                    BLManejadorBodega man = new BLManejadorBodega();
                    DataTable datat = man.buscarAdmin(palabraTb.Text.Trim());
                    gridBodegas.DataSource = datat;
                    gridBodegas.DataBind();
                  
                    return datat;
                }
            } catch(Exception) {

            }
            return null;
        }

        /// <summary>
        /// Permite la busqueda en el campo de busqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void palabraTb_TextChanged(object sender, EventArgs e) {
            try {
                this.buscar();
            } catch(Exception) {

            }
        }

        /// <summary>
        /// Método que permite al usuario dar clic en cualquier lugar de las filas de la tabla bodegas para
        ///visualizar su contenido completo.
        ///Redirecciona a la pagina Bodega.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridBodegas_SelectedIndexChanged(object sender, EventArgs e) {
            try {
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
            } catch(Exception) {

            }
        }

        /// <summary>
        /// Método que maneja el evento de dar click en el boton "nuevo" de la página.
        ///Este pone la variable sesión idBodega en null y redirige a la página Bodega.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregar_Click(object sender, EventArgs e) {
            try {
                Session["idBodega"] = null;
                Response.Redirect("Bodega.aspx");
            } catch(Exception) {

            }
        }

        /// <summary>
        /// Permite la busqueda en la tabla al presionar enter en el campo de texto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            try {
                if(e.KeyCode == Keys.Enter) {
                    this.buscar();
                }
            } catch(Exception) {

            }
        }

        /// <summary>
        /// Método que enlaza el clic en la fila de la tabla cuentas con el evento de selectedindexchanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridBodegas_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if(e.Row.RowType == DataControlRowType.DataRow) {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridBodegas, "Select$" + e.Row.RowIndex);
                    e.Row.ToolTip = "Clic para abrir.";
                }
            } catch(Exception) {

            }
        }

    }
}