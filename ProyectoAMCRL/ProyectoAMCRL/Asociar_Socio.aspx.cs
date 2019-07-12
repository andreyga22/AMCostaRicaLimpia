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

namespace ProyectoAMCRL
{
    public partial class Asociar_Socio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cuentaLogin"] != null)
            {
                if (!this.IsPostBack)
                {
                    if (ViewState["sorting"] == null)
                    {
                        this.buscarIzquierda();
                        this.cargarLabels();
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        private void cargarLabels() {
            string idSocio = Convert.ToString(Session["idSocio"]);
            BLManejadorSocios manejador = new BLManejadorSocios();
            BLSocioNegocio socio = manejador.buscarCedula(idSocio);
            idLbl.Text = socio.cedula;
            nombreLbl.Text = socio.nombre + " " + socio.apellido1 + " " + socio.apellido2;
            rolLbl.Text = socio.rol;
            this.buscarDerecha(idSocio);
        }

        protected void txtPalabra_TextChanged(object sender, EventArgs e)
        {
            this.buscarIzquierda();
        }

        private DataTable buscarIzquierda()
        {
            BLManejadorSocios manejador = new BLManejadorSocios();
            DataTable tabla = manejador.buscarIzquierdaSocios(txtPalabra.Text.Trim(), Convert.ToString(Session["idSocio"]));
            gridSocios.DataSource = tabla;
            gridSocios.DataBind();
            foreach (GridViewRow row in gridSocios.Rows)
            {
                LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
                lb.ForeColor = System.Drawing.Color.Blue;
                lb.Text = "Asociar";
            }
            return tabla;
        }

        private DataTable buscarDerecha(string cedula)
        {
            BLManejadorSocios manejador = new BLManejadorSocios();
            DataTable tabla = manejador.buscarDerechaSocios(cedula);
            gridAsociados.DataSource = tabla;
            gridAsociados.DataBind();
            foreach (GridViewRow row in gridAsociados.Rows)
            {
                LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
                lb.ForeColor = System.Drawing.Color.Red;
                lb.Text = "Desasociar";
            }
            return tabla;
        }

        protected void gridSocios_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable datat = this.buscarIzquierda();
                DataView dv = new DataView(datat);
                if (ViewState["sorting"] == null || ViewState["sorting"].ToString() == "DESC")
                {
                    dv.Sort = e.SortExpression + " ASC";
                    ViewState["sorting"] = "ASC";
                    //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortasc";

                }
                else
                {
                    if (ViewState["sorting"].ToString() == "ASC")
                    {
                        dv.Sort = e.SortExpression + " DESC";
                        ViewState["sorting"] = "DESC";
                        //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortdesc";
                    }
                }
                Session["sortedView"] = dv;
                gridSocios.DataSource = dv;
                gridSocios.DataBind();
                foreach (GridViewRow row in gridSocios.Rows)
                {
                    LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
                    lb.ForeColor = System.Drawing.Color.Blue;
                    lb.Text = "Asociar";
                }

                if (ViewState["sorting"].ToString() == "ASC")
                {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridSocios.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
                }
                else
                {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridSocios.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
                }
            }
            catch (Exception)
            {

            }
        }

        protected void gridAsociados_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable datat = this.buscarDerecha(Convert.ToString(Session["idSocio"]));
                DataView dv = new DataView(datat);
                if (ViewState["sorting"] == null || ViewState["sorting"].ToString() == "DESC")
                {
                    dv.Sort = e.SortExpression + " ASC";
                    ViewState["sorting"] = "ASC";
                    //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortasc";

                }
                else
                {
                    if (ViewState["sorting"].ToString() == "ASC")
                    {
                        dv.Sort = e.SortExpression + " DESC";
                        ViewState["sorting"] = "DESC";
                        //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortdesc";
                    }
                }
                Session["sortedView"] = dv;
                gridAsociados.DataSource = dv;
                gridAsociados.DataBind();
                foreach (GridViewRow row in gridAsociados.Rows)
                {
                    LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
                    lb.ForeColor = System.Drawing.Color.Red;
                    lb.Text = "Desasociar";
                }

                if (ViewState["sorting"].ToString() == "ASC")
                {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridAsociados.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
                }
                else
                {
                    int index = GetColumnIndex(datat, e.SortExpression);
                    gridAsociados.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
                }
            }
            catch (Exception)
            {

            }
        }

        private int GetColumnIndex(DataTable dt, string name)
        {
            return dt.Columns.IndexOf(name);
        }


        protected void gridSocios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.ToolTip = "Clic para agregar.";
            }
        }

        protected void gridAsociados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.ToolTip = "Clic para eliminar.";
            }
        }

        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buscarIzquierda();
            }
        }

        protected void gridSocios_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridSocios.Rows)
            {
                if (row.RowIndex == gridSocios.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Clic para asociar.";
                }

            }
            string id = gridSocios.SelectedRow.Cells[1].Text;
            BLManejadorSocios manejador = new BLManejadorSocios();
            manejador.asociarSocio(id, Convert.ToString(Session["idSocio"]));
            this.buscarDerecha(Convert.ToString(Session["idSocio"]));
        }

        protected void gridAsociados_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridAsociados.Rows)
            {
                if (row.RowIndex == gridAsociados.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Clic para desasociar.";
                }

            }
            string id = gridAsociados.SelectedRow.Cells[1].Text;
            BLManejadorSocios manejador = new BLManejadorSocios();
            manejador.desasociarSocio(id, Convert.ToString(Session["idSocio"]));
            this.buscarIzquierda();
            this.buscarDerecha(Convert.ToString(Session["idSocio"]));
        }
    }
}