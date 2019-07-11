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
        //    protected void Page_Load(object sender, EventArgs e)
        //    {
        //        if (Session["cuentaLogin"] != null)
        //        {
        //            if (!this.IsPostBack)
        //            {
        //                if (ViewState["sorting"] == null)
        //                {
        //                    this.buscar();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect("Login.aspx");
        //        }

        //    }

        //protected void txtPalabra_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private DataTable buscar()
        //{
        //    BLManejadorSocios manejador = new BLManejadorSocios();
        //    DataTable tabla = manejador.buscarDatos(txtPalabra.Text.Trim());
        //    gridSocios.DataSource = tabla;
        //    gridSocios.DataBind();
        //    return tabla;
        //}

        //    protected void gridSocios_Sorting(object sender, GridViewSortEventArgs e)
        //    {
        //        try
        //        {
        //            DataTable datat = this.buscar();
        //            DataView dv = new DataView(datat);
        //            if (ViewState["sorting"] == null || ViewState["sorting"].ToString() == "DESC")
        //            {
        //                dv.Sort = e.SortExpression + " ASC";
        //                ViewState["sorting"] = "ASC";
        //                //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortasc";

        //            }
        //            else
        //            {
        //                if (ViewState["sorting"].ToString() == "ASC")
        //                {
        //                    dv.Sort = e.SortExpression + " DESC";
        //                    ViewState["sorting"] = "DESC";
        //                    //gridCuentas.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "sortdesc";
        //                }
        //            }
        //            Session["sortedView"] = dv;
        //            gridSocios.DataSource = dv;
        //            gridSocios.DataBind();


        //            if (ViewState["sorting"].ToString() == "ASC")
        //            {
        //                int index = GetColumnIndex(datat, e.SortExpression);
        //                gridSocios.HeaderRow.Cells[index].CssClass = "SortedAscendingHeaderStyle";
        //            }
        //            else
        //            {
        //                int index = GetColumnIndex(datat, e.SortExpression);
        //                gridSocios.HeaderRow.Cells[index].CssClass = "SortedDescendingHeaderStyle";
        //            }
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }

        //    private int GetColumnIndex(DataTable dt, string name)
        //    {
        //        return dt.Columns.IndexOf(name);
        //    }

        //    protected void gridSocios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //    {
        //        gridSocios.PageIndex = e.NewPageIndex;
        //        this.buscar();
        //        if (Session["SortedView"] != null)
        //        {
        //            gridSocios.DataSource = Session["SortedView"];
        //            gridSocios.DataBind();
        //        }
        //    }

        //    protected void gridSocios_RowDataBound(object sender, GridViewRowEventArgs e)
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            e.Row.ToolTip = "Clic para abrir.";
        //        }
        //    }

        //    private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e)
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            this.buscar();
        //        }
        //    }

        //    protected void gridSocios_SelectedIndexChanged(object sender, EventArgs e)
        //    {
        //        foreach (GridViewRow row in gridSocios.Rows)
        //        {
        //            if (row.RowIndex == gridSocios.SelectedIndex)
        //            {
        //                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
        //                row.ToolTip = string.Empty;
        //            }
        //            else
        //            {
        //                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        //                row.ToolTip = "Clic para abrir.";
        //            }

        //        }
        //        string id = gridSocios.SelectedRow.Cells[0].Text;
        //    }
    }
}