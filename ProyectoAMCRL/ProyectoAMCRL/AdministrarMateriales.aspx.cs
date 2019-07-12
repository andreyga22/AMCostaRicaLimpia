using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BL;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoAMCRL
{
    public partial class Materiales : System.Web.UI.Page
    {

        /// <summary>
        /// Carga todos los componentes de la pantalla. Revisa si hay un usuario en sesión para 
        /// permitir o negar la carga de la página, en caso de negarlo vuelve al login. Carga
        /// los la tabla con los datos de los materiales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["cuentaLogin"] != null)
                {
                    if (!this.IsPostBack)
                    {
                        if (ViewState["sorting"] == null)
                        {
                            this.buscar();
                        }
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo cargar la página.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        /// <summary>
        /// Evento que permite el cambio de páginas de la tabla de materiales. Se actualiza la tabla después del cambio.       
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridMateriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gridMateriales.PageIndex = e.NewPageIndex;
                this.buscar();
                if (Session["SortedView"] != null)
                {
                    gridMateriales.DataSource = Session["SortedView"];
                    gridMateriales.DataBind();
                }
            }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo realizar la paginación de tabla.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        /// <summary>
        ///  Método que se encarga de ordenar el gridview de materiales.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridMateriales_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable datat = this.buscar();
                DataView dv = new DataView(datat);
                if (ViewState["sorting"] == null || ViewState["sorting"].ToString() == "DESC")
                {
                    dv.Sort = e.SortExpression + " ASC";
                    ViewState["sorting"] = "ASC";
                }
                else
                {
                    if (ViewState["sorting"].ToString() == "ASC")
                    {
                        dv.Sort = e.SortExpression + " DESC";
                        ViewState["sorting"] = "DESC";
                    }
                }
                Session["sortedView"] = dv;
                gridMateriales.DataSource = dv;
                gridMateriales.DataBind();


                if (ViewState["sorting"].ToString() == "ASC")
                {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridMateriales.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
                }
                else
                {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridMateriales.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
                }
            }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo ordenar la tabla.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        /// <summary>
        /// Devuelve el índice de una columna en un datatable enviado, utilizando el nombre como parámetro.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="name"></param>
        /// <returns>Retorna el índice de la columna</returns>
        private int GetColumnIndex(DataTable dt, string name)
        {
            return dt.Columns.IndexOf(name);
        }

        /// <summary>
        /// Permite la búsqueda en el campo de búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void palabraTb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.buscar();
            }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo realizar la búsqueda de la palabra.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        /// <summary>
        ///  Método que permite al usuario dar clic en cualquier lugar de las filas de la tabla materiales para
        ///  visualizar su contenido completo.
        ///  Redirecciona a la pagina RegistroMateriales.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridMateriales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gridMateriales.Rows)
                {
                    if (row.RowIndex == gridMateriales.SelectedIndex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                        row.ToolTip = string.Empty;
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                        row.ToolTip = "Clic para abrir.";
                    }
                }
                string id = gridMateriales.SelectedRow.Cells[0].Text;
                Session["idMaterial"] = id;
                Response.Redirect("RegistroMateriales.aspx");
        }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo seleccionar el material.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
}

        /// <summary>
        /// Método que maneja el evento de dar click en el botón "nuevo" de la página.
        /// Este pone la variable sesión idBodega en null y redirige a la página RegistroMateriales.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            Session["idMaterial"] = null;
            Response.Redirect("RegistroMateriales.aspx");

        }

        /// <summary>
        /// Permite la búsqueda en la tabla al presionar enter en el campo de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.buscar();
                }
            }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo realizar la búsqueda.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        /// <summary>
        /// Método que enlaza el clic en la fila de la tabla cuentas con el evento de selectedindexchanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridMateriales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridMateriales, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

        /// <summary>
        /// Método que permite la función de busqueda para la tabla. Consulta la base de datos en caso de 
        /// que hayan coincidencias con la palabra buscada.
        /// Se cargan dos tipos diferentes de datos en la tabla dependiendo del rol del usuario en sesión.
        /// </summary>
        /// <returns>Retorna el datatable con los datos buscados</returns>
        private DataTable buscar()
        {
            try
            {
                BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
                if (usuarioLogin.rol.Equals("r"))
                {
                    BLManejadorMateriales man = new BLManejadorMateriales();
                    DataTable datat = man.buscar(palabraTb.Text.Trim());
                    gridMateriales.DataSource = datat;
                    gridMateriales.DataBind();
                    return datat;
                }
                else
                {
                    BLManejadorMateriales man = new BLManejadorMateriales();
                    DataTable datat = man.buscarAdmin(palabraTb.Text.Trim());
                    gridMateriales.DataSource = datat;
                    gridMateriales.DataBind();
                    return datat;
                }
            }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo cargar la tabla de materiales.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            return null;
        }
    }
}