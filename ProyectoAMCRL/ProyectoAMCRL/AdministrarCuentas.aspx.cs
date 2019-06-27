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
        /*
           Carga todos los componentes de la pantalla. 
           Reisa si hay un usuario en sesión para permitir o negar la carga 
           de la página. En caso de negarlo vuelve al login.
           Carga la tabla con los datos de las cuentas.
            */

            /*Ordenar: el if del viewstate es necesario*/
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

        /*
        Permite la busqueda en el campo de busqueda
            */
        protected void palabraTb_TextChanged(object sender, EventArgs e) {
            this.buscar();
        }

        /*
        Método que maneja el evento de dar click en el boton "nuevo" de la página.
        Este pone la variable sesión idCuenta en null y redirige a la página Cuenta.aspx
            */
        protected void btnAgregar_Click(object sender, EventArgs e) {
            Session["idCuenta"] = null;
            Session["accionCuenta"] = 0;
            Response.Redirect("Cuenta.aspx");
        }

        /*
         Evento que permite el cambio de páginas de la tabla de cuentas. Se actualiza la tabla después del cambio.
             */

            /*
             Ordenar: es necesario el if de session
             */
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
        private int GetColumnIndex(DataTable dt, string name) {
            return dt.Columns.IndexOf(name);
        }


        /*
         Método que permite la función de busqueda para la tabla. Consulta la base de datos en caso de 
         que hayan coincidencias con la palabra buscada.
         Se cargan dos tipos diferentes de datos en la tabla dependiendo del rol del usuario en sesión.

         Variables:
         man = instancia del objeto BLManejadorCuentas, el cúal contiene toda la funcionalidad de las cuentas.
             */
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

        /*
         Método que permite al usuario dar clic en cualquier lugar de las filas de la tabla cuentas para
         visualizar su contenido completo.
         Redirecciona a la pagina Cuenta.aspx

         Variables:
         string id = almacena el contenido del id de la fila seleccionada para luego ser almacenda en una 
         variable de sesión para luego ser utilizada en la siguiente página.
             */
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

        /*
         Permite la busqueda en la tabla al presionar enter en el campo de texto.
             */
        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                this.buscar();
            }
        }

        /*
         Método que enlaza el clic en la fila de la tabla bodegas con el evento de selectedindexchanging
             */
        protected void gridCuentas_RowDataBound(object sender, GridViewRowEventArgs e) {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridCuentas, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

        
    }
}