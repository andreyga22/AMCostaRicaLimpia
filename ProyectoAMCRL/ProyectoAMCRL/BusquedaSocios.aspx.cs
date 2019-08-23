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
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;

namespace ProyectoAMCRL
{
    public partial class BusquedaSocios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["cuentaLogin"] != null)
            {
                if (!this.IsPostBack)
                {
                    if (ViewState["sorting"] == null)
                    {
                        Session["idSocio"] = null;
                        this.buscar();
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        private DataTable buscar()
        {
            BLManejadorSocios manejador = new BLManejadorSocios();
            DataTable tabla = new DataTable();
            BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
            if (usuarioLogin.rol.Equals("r"))
            {
              tabla = manejador.buscarDatosRegular(txtPalabra.Text.Trim());
            }
            else
            {
                tabla = manejador.buscarDatosAdmin(txtPalabra.Text.Trim());
            }
            gridSocios.DataSource = tabla;
            gridSocios.DataBind();
            return tabla;
        }

        protected void gridSocios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridSocios.PageIndex = e.NewPageIndex;
            this.buscar();
            if (Session["SortedView"] != null) {
                gridSocios.DataSource = Session["SortedView"];
                gridSocios.DataBind();
            }
        }

        protected void gridSocios_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable datat = this.buscar();
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

        private int GetColumnIndex(DataTable dt, string name) {
            return dt.Columns.IndexOf(name);
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
                    row.ToolTip = "Clic para abrir.";
                }

            }
            string id = gridSocios.SelectedRow.Cells[0].Text;
            Session["idSocio"] = id;
            Response.Redirect("RegistroSociosUI.aspx");
        }

        protected void gridSocios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridSocios, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Clic para abrir.";
            }
        }

        protected void txtPalabra_TextChanged(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void txt_Item_Number_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                this.buscar();
            }
        }

        protected void NuevoBtn_Click(object sender, EventArgs e)
        {
            Session["idSocio"] = null;
            Response.Redirect("RegistroSociosUI.aspx");
        }


        protected void printbtn_Click(object sender, EventArgs e)
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document documento = new Document(PageSize.A4, 10, 10, 10, 10);
                String path = Server.MapPath("~/Facturas/");
                PdfWriter.GetInstance(documento, new FileStream(path + "/PruebaProyecto.pdf", FileMode.Create));
                documento.Open();

                Chunk chunk = new Chunk("Prueba Chunk ");
                documento.Add(chunk);

                Phrase phrase = new Phrase("Prueba frase.");
                documento.Add(phrase);

                Paragraph para = new Paragraph("Prueba Parrafo.");
                documento.Add(para);

                string text = "PDF creado con exito!!";
                Paragraph paragraph = new Paragraph();
                paragraph.SpacingBefore = 10;
                paragraph.SpacingAfter = 10;
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f, BaseColor.GREEN);
                paragraph.Add(text);
                documento.Add(paragraph);

                documento.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";

                string nombrepdf = "PruebaProyecto";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + nombrepdf + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }
    }
}