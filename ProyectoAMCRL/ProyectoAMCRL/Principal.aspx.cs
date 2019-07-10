using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BL;

namespace ProyectoAMCRL
{
    public partial class Principal : System.Web.UI.Page
    {

        /// <summary>
        ///  Revisa si hay un usuario en sesión para permitir o negar la carga 
        ///  de la página.En caso de negarlo vuelve al login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cuentaLogin"] != null)
            {
                if (!this.IsPostBack)
                {
                    Session["idFactura"] = "";
                    Session["idFactura1"] = "";
                    Session["idFactura2"] = "";
                    Session["idFactura3"] = "";

                    infoUltimosCliente();
                    infoUltimosProveedor();
                    infoMasMateriales();
                    infoTotalFact();
                    infoUltimasFact();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        /// <summary>
        /// Método utilizado para mostrar los últimos 3 clientes registrados
        /// </summary>
        private void infoUltimosCliente()
        {
            BLManejadorSocios manejSocios = new BLManejadorSocios();
        }

        /// <summary>
        /// Método utilizado para mostrar los últimos 3 proveedores registrados
        /// </summary>
        private void infoUltimosProveedor()
        {
            BLManejadorSocios manejSocios = new BLManejadorSocios();
        }

        /// <summary>
        /// Método utilizado para mostrar el total de ventas y compras realizadas al mes
        /// </summary>
        private void infoTotalFact()
        {
            BLManejadorFacturas manejFact = new BLManejadorFacturas();
            subCompras.Text = "Total de compras: " + manejFact.numeroRangoFecha("c");
            subVentas.Text = "Total de ventas: " + manejFact.numeroRangoFecha("v");

        }

        /// <summary>
        /// Méetodo de evento que permite ir a la página Búsqueda de Facturas de compras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void click_BusquedaFactC(object sender, EventArgs e)
        {
            Session["modo"] = "compra";
            Response.Redirect("BusquedaFacturas.aspx");
        }

        /// <summary>
        /// Méetodo de evento que permite ir a la página Búsqueda de Facturas de compras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void click_BusquedaFactV(object sender, EventArgs e)
        {
            Session["modo"] = "venta";
            Response.Redirect("BusquedaFacturas.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        private void infoMasMateriales()
        {
            ///cambiar por stock (los más almacenados en bodegas, top 3)

        }

      

        /// <summary>
        /// Método que permite mostrar las últimas 3 facturas que se realizaron
        /// </summary>
        private void infoUltimasFact()
        {
            BLManejadorFacturas manej = new BLManejadorFacturas();
            List<BLFactura> listaFact = manej.listaFact_Top3();
            if (listaFact.Count > 0)
            {
                sub1Fac.Text = listaFact[0].nombreCompleto;
                Session["idFactura1"] = listaFact[0].cod_Factura;
                if (listaFact.Count > 1)
                {
                    sub2Fac.Text = listaFact[1].nombreCompleto;
                    Session["idFactura2"] = listaFact[1].cod_Factura;
                }
                if (listaFact.Count > 2)
                {
                    sub3Fac.Text = listaFact[2].nombreCompleto;
                    Session["idFactura3"] = listaFact[2].cod_Factura;
                }
            }
            else
            {
                factNoHay.Text = "No hay facturas";
            }

        }

        /// <summary>
        /// Método del evento que que muestra la última factura realizada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void click_SeleccFact1(object sender, EventArgs e)
        {
            Session["idFactura"] = Session["idFactura1"];
            Response.Redirect("Compra_Venta.aspx");
        }

        /// <summary>
        /// Método del evento que muestra la penúltima factura realizada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void click_SeleccFact2(object sender, EventArgs e)
        {
            Session["idFactura"] = Session["idFactura2"];
            Response.Redirect("Compra_Venta.aspx");
        }

        /// <summary>
        /// Método del evento que muestra la antepenúltima factura realizada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void click_SeleccFact3(object sender, EventArgs e)
        {
            Session["idFactura"] = Session["idFactura3"];
            Response.Redirect("Compra_Venta.aspx");
        }

        /// <summary>
        /// Método que muestra la ventana para la creación de una nueva factura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void click_NuevaFact(object sender, EventArgs e)
        {
            Session["idFactura"] = "";
            Response.Redirect("Compra_Venta.aspx");
        }
    }
}