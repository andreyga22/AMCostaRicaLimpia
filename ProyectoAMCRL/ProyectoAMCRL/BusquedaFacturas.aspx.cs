using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace ProyectoAMCRL
{
    public partial class BusquedaFacturas : System.Web.UI.Page
    {

        /// <summary>
        ///  Revisa si hay un usuario en sesión para permitir o negar la carga 
        ///  de la página.En caso de negarlo vuelve al login.
        ///  Carga los materiales que se van a utilizar en el filtro
        ///  Carga la tabla con las facturas existentes.
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
                            this.buscar(null);
                            cargarMateriales();
                            Session["idFactura"] = "";
                        }
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            } catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo cargar la página.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }


        /// <summary>
        /// Permite la búsqueda en el campo de búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPalabra_TextChanged(object sender, EventArgs e)
        {
            this.buscar(null);
        }

        /// <summary>
        /// Se cargan los materiales utilizados en el filtro
        /// </summary>
        private void cargarMateriales()
        {
            try
            {
                BLManejadorMateriales manej = new BLManejadorMateriales();
                DataSet listaMateriales = manej.listarMaterialesBL();


                foreach (DataRow dr in listaMateriales.Tables[0].Rows)
                {
                    ListItem item = new ListItem();

                    String codigo = Convert.ToString(dr["COD_MATERIAL"]);
                    String nombre = Convert.ToString(dr["NOMBRE_MATERIAL"]);
                    item.Text = nombre;
                    item.Value = codigo;
                    materialesCB.Items.Add(item);
                }
            } catch(Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo cargar los materiales.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        /// <summary>
        ///  Carga la tabla y la muestra en la pantalla
        /// </summary>
        private DataTable buscar(DataTable facturas)
        {
            try
            {
                DataTable tabla = new DataTable();
                if (facturas == null)
                {
                    BLManejadorFacturas man = new BLManejadorFacturas();
                    String modo = (String)Session["modo"];
                    String modoCarac = "";
                    if (modo.Equals("compra"))
                    {
                        modoCarac = "c";
                    }
                    else
                    {
                        modoCarac = "v";
                    }
                    tabla = man.buscar(txtPalabra.Text.Trim(), modoCarac);
                }
                else
                {
                    tabla = facturas;
                }
                gridFacturas.DataSource = tabla;
                gridFacturas.DataBind();
                return tabla;
            } catch(Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo cargar la tabla.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
                return null;
            }
        }

        /// <summary>
        /// Evento que permite el cambio de páginas de la tabla de facturas. 
        /// Se actualiza la tabla después del cambio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridFact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridFacturas.PageIndex = e.NewPageIndex;
            this.buscar(null);
            if (Session["SortedView"] != null)
            {
                gridFacturas.DataSource = Session["SortedView"];
                gridFacturas.DataBind();
            }
        }

        /// <summary>
        /// Método que permite al usuario dar clic en cualquier lugar de las filas de la tabla facturas para
        /// visualizar su contenido completo.
        /// Redirecciona a la pagina Compra_Venta.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridFact_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gridFacturas.Rows)
                {
                    if (row.RowIndex == gridFacturas.SelectedIndex)
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
                string id = gridFacturas.SelectedRow.Cells[2].Text;
                Session["idFactura"] = id;
                Response.Redirect("Compra_Venta.aspx");
            }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo seleccionar la factura.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }


        /// <summary>
        /// Método que enlaza el clic en la fila de la tabla cuentas con el evento de selectedindexchanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridFact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridFacturas, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

        /// <summary>
        /// Evento que permite cargar la tabla con las opciones que se eligieron.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                BLManejadorFacturas manej = new BLManejadorFacturas();

                String fechaIni = "";
                String fechFin = "";
                String montoMaximoF = "";
                String montoMinimoF = "";
                List<String> materiales = new List<string>();

                if (!String.IsNullOrEmpty(fechaInicio.Text))
                    fechaIni = fechaInicio.Text;
                if (!String.IsNullOrEmpty(fechaFin.Text))
                    fechFin = fechaFin.Text;
                if (!String.IsNullOrEmpty(montoMaximo.Text))
                    montoMaximoF = montoMaximo.Text;
                if (!String.IsNullOrEmpty(montoMinimo.Text))
                    montoMinimoF = montoMinimo.Text;

                foreach (ListItem material in materialesCB.Items)
                {
                    if (material.Selected)
                        materiales.Add(material.Value);
                }
                String modo = (String)Session["modo"];
                String modoCarac = "";
                if (modo.Equals("compra"))
                {
                    modoCarac = "c";
                }
                else
                {
                    modoCarac = "v";
                }
                DataTable facturasFiltradas = manej.filtrarFacturas(fechaIni, fechFin, montoMaximoF, montoMinimoF, materiales, modoCarac);
                fechaInicio.Text = "";
                fechaFin.Text = "";
                montoMaximo.Text = "";
                montoMinimo.Text = "";
                foreach (ListItem material in materialesCB.Items)
                {
                    material.Selected = false;
                }

                buscar(facturasFiltradas);
            } catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo cargar las facturas.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }


        /// <summary>
        /// Permite la busqueda en la tabla al presionar enter en el campo de texto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buscar(null);
            }
        }

        /// <summary>
        /// Método utilizado para ordenar la tabla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridFact_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable datat = this.buscar(null);
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
                gridFacturas.DataSource = dv;
                gridFacturas.DataBind();


                if (ViewState["sorting"].ToString() == "ASC")
                {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridFacturas.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
                }
                else
                {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridFacturas.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
                }
            } catch(Exception)
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
            try
            {
                return dt.Columns.IndexOf(name);
            } catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo retornar el índice de la columna.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
            return -1;
        }


    }
}
