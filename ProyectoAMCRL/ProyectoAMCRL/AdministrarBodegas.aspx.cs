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
        /*
            Carga todos los componentes de la pantalla. 
            Reisa si hay un usuario en sesión para permitir o negar la carga 
            de la página. En caso de negarlo vuelve al login.
            Carga la tabla con los datos de las bodegas.
             */
        protected void Page_Load(object sender, EventArgs e) {
            if(Session["cuentaLogin"] != null) {
                if(!this.IsPostBack) {
                    this.buscar();
                }
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        /*
         Evento que permite el cambio de páginas de la tabla de bodegas. Se actualiza la tabla después del cambio.
             */
        protected void gridBodegas_PageIndexChanging(object sender, GridViewPageEventArgs e) {

            gridBodegas.PageIndex = e.NewPageIndex;
            this.buscar();
        }

        protected void gridBodegas_Sorting(object sender, GridViewSortEventArgs e) {

        }

        /*
         Método que permite la función de busqueda para la tabla. Consulta la base de datos en caso de 
         que hayan coincidencias con la palabra buscada.
         Se cargan dos tipos diferentes de datos en la tabla dependiendo del rol del usuario en sesión.

         Variables:
         man = instancia del objeto BLManejadorBodega, el cúal contiene toda la funcionalidad de las bodegas.
             */
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

        /*
         Permite la busqueda en el campo de busqueda
             */
        protected void palabraTb_TextChanged(object sender, EventArgs e) {
            this.buscar();
        }

        /*
         Método que permite al usuario dar clic en cualquier lugar de las filas de la tabla bodegas para
         visualizar su contenido completo.
         Redirecciona a la pagina Bodega.aspx

         Variables:
         string id = almacena el contenido del id de la fila seleccionada para luego ser almacenda en una 
         variable de sesión para luego ser utilizada en la siguiente página.
             */
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

        /*
         Método que maneja el evento de dar click en el boton "nuevo" de la página.
         Este pone la variable sesión idBodega en null y redirige a la página Bodega.aspx
             */
        protected void btnAgregar_Click(object sender, EventArgs e) {
            Session["idBodega"] = null;
            Response.Redirect("Bodega.aspx");
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
         Método que enlaza el clic en la fila de la tabla cuentas con el evento de selectedindexchanging
             */
        protected void gridBodegas_RowDataBound(object sender, GridViewRowEventArgs e) {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridBodegas, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

    }
}