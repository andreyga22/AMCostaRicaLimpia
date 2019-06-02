using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using System.IO;

namespace ProyectoAMCRL
{
    public partial class Compra_Venta : System.Web.UI.Page
    {
        List<BLSocioNegocio> socios;
        private static List<String> detalles;
        static int numeroDetalles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                numeroDetalles = 0;
                detalles = new List<String>();
                socios = new List<BLSocioNegocio>();
                productosTB.Items.Add("COBRE");
                productosTB.Items.Add("LATA");
                 productosTB.Items.Add("HIERRO");
                 productosTB.Items.Add("ALUMINIO");

                 unidadTB.Items.Add("KG");
                 unidadTB.Items.Add("TONELADA");

                 monedas.Items.Add("COL");
                 monedas.Items.Add("USD");
        }
        }


        private void pegarLineasTabla() {
            
            foreach (String l in detalles) {
                String[] lineaInfo = l.Split(',');
                TableCell productoCell = new TableCell();
                TableCell cantidadCell = new TableCell();
                TableCell unidadCell = new TableCell();
                TableCell precioUnCell = new TableCell();
                TableCell PrecioTotCell = new TableCell();
                TableCell quitarFilaCampo = new TableCell();
                TableRow filaNueva = new TableRow();

                productoCell.Text = lineaInfo[0];
                cantidadCell.Text = lineaInfo[1];
                unidadCell.Text = lineaInfo[2];
                precioUnCell.Text = lineaInfo[3];
                PrecioTotCell.Text = lineaInfo[4];
                quitarFilaCampo.Text = "Quitar linea";
                quitarFilaCampo.ForeColor = System.Drawing.Color.Red;
            
                filaNueva.Cells.Add(productoCell);
                filaNueva.Cells.Add(cantidadCell);
                filaNueva.Cells.Add(unidadCell);
                filaNueva.Cells.Add(precioUnCell);
                filaNueva.Cells.Add(PrecioTotCell);
                filaNueva.Cells.Add(quitarFilaCampo);    
                    
                tablaDetalles.Rows.Add(filaNueva);
            }
            tablaDetalles.DataBind();
        }

        protected void Guardar_click(object sender, EventArgs e)
        {
            //la.Text = manejadorCompras.guadarDetallesCompraBL();
        }

        protected void agregarLineaClick(object sender, EventArgs e)
        {

            String detalleNuevo = "";
           
            double precioTotal = Int64.Parse(cantidadTB.Text) * Int64.Parse(precioUnidadTB.Text);
            numeroDetalles += 1;
            labelC.Text = numeroDetalles.ToString();
            detalleNuevo = productosTB.Text + "," + cantidadTB.Text + "," + unidadTB.Text + "," + precioUnidadTB.Text + "," + precioTotal;
            detalles.Add(detalleNuevo);
            pegarLineasTabla();

            
        }
    }
}