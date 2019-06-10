using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class Factura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String id = Convert.ToString(Session["idFactura"]);
            if (!String.IsNullOrEmpty(id) || (!String.IsNullOrWhiteSpace(id)))
            {
                BLManejadorFacturas manejFact = new BLManejadorFacturas();
                BLManejadorMoneda manejMond = new BLManejadorMoneda();
                BLFactura blFactura = manejFact.buscarVentaID(Convert.ToInt32(id));
                lblFecha.Text = Convert.ToString(blFactura.fecha);
                lblFactura.Text = Convert.ToString(blFactura.cod_Factura);
               //buscar cliente por cedula
                lblNombre.Text = blFactura.nombreCompleto;
                lblMoneda.Text = manejMond.buscarMonedaId(blFactura.id_Moneda).detalleMoneda;
                lblDireccion.Text = "Naranjo, Alajuela";
                if (blFactura.tipo.Equals("V"))
                {
                    lblTitulo.Text = "Factura de Venta";
                } else
                {
                    lblTitulo.Text = "Factura de Compra";
                }

                List<BLDetalleFactura> listDetalle = new BLManejadorFacturas().listaDetalle(id);

                gridDetalle.DataSource = listDetalle;

                gridDetalle.DataBind();
                gridDetalle.HeaderRow.Cells[0].Text = "Código Línea";
                gridDetalle.HeaderRow.Cells[1].Text = "Código Venta";
                gridDetalle.HeaderRow.Cells[2].Text = "Código Detalle";
                gridDetalle.HeaderRow.Cells[3].Text = "Material";
                gridDetalle.HeaderRow.Cells[4].Text = "Monto";
                gridDetalle.HeaderRow.Cells[5].Text = "Cantidad Kilos";
    

                gridDetalle.HeaderRow.Cells[0].Visible = false;
                for (int i = 0; i < gridDetalle.Rows.Count; i++)
                {
                    gridDetalle.Rows[i].Cells[0].Visible = false;
                }

                gridDetalle.HeaderRow.Cells[1].Visible = false;
                for (int i = 0; i < gridDetalle.Rows.Count; i++)
                {
                    gridDetalle.Rows[i].Cells[1].Visible = false;
                }

                gridDetalle.HeaderRow.Cells[2].Visible = false;
                for (int i = 0; i < gridDetalle.Rows.Count; i++)
                {
                    gridDetalle.Rows[i].Cells[2].Visible = false;
                }

                lblMontoTotal.Text = Convert.ToString(blFactura.monto_Total);
            }
        }
    }
}