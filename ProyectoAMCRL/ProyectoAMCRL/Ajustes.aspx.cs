using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL {
    public partial class Ajustes : System.Web.UI.Page {

        BLManejadorAjustes manejadorA = new BLManejadorAjustes();


        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {
                cargarMaterialesANDBodegas();

                DataSet ajustes = (DataSet)Session["ajustes"];
                if (ajustes != null)
                {
                    
                    cargarTabla(ajustes);
                }
                else {
                    cargarTabla(null);
                }
                
            }
           

        }

        private void cargarTabla(DataSet ajustes) {

            DataSet dataSet;
            if (ajustes == null)
                dataSet = manejadorA.listarAjustesBL();
            else
                dataSet = ajustes;

            String cuerpoTablaHTML = "";

                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    //fecha, peso, movimiento, stock   12/20/2019 12:00:00 AM
                    String fechaInfo = Convert.ToString(dr["Fecha_Ajuste"]);


                //String materiales = Convert.ToString(dr["MATERIALES"]);
                String movimientoNumber = Convert.ToString(dr["MOVIMIENTO_A"]);
                    String movimiento = "";
                    switch (movimientoNumber)
                    {
                        case "1":
                            movimiento = "ENTRADA";
                            break;

                        case "0":
                            movimiento = "SALIDA";
                            break;

                    }


                    String stock = Convert.ToString(dr["ID_STOCK"]);
                    String idAjuste = Convert.ToString(dr["ID_AJUSTE"]);
                    String idBodega = Convert.ToString(dr["ID_BODEGA"]);

                    String btnHTML2 = "<a href='#' data-toggle='popover' data-placement='left' title='Detalle ajuste' data-html='true' data-content='Some content " + idAjuste + " popover'>Ver</a>";
                    String idEncriptado = BLManejadorEncripcion.Encrypt(idAjuste);
                    String btnHTML = "<input id='" + idAjuste + "' type='button' class='btn btn-sm btn-link' value='" + idAjuste + "' >";
                    String filaHTML = "<tr onclick='abrirDetalleClick(" + idAjuste + ")'>" +
                    "<td>" + idAjuste + "</td>" +
                    "<td>" + fechaInfo + "</td>" +
                    //"<td>" + materiales + "</td>" +
                    "<td>" + idBodega + "</td>" +
                    "<td>" + movimiento + "</td >" +
                    "</tr> ";
                    cuerpoTablaHTML += filaHTML;
                }

            tablaPlaceHolder.Controls.Add(new Literal { Text = cuerpoTablaHTML.ToString() });
            tablaPlaceHolder.DataBind();

        }

        private void cargarMaterialesANDBodegas() {
            BLManejadorMateriales manejadorM = new BLManejadorMateriales();
            DataSet listaM = manejadorM.listarMaterialesBL();

            foreach (DataRow dr in listaM.Tables[0].Rows) {
                String codigo = Convert.ToString(dr["COD_MATERIAL"]);
                String nombre = Convert.ToString(dr["NOMBRE_MATERIAL"]);
                String precio = Convert.ToString(dr["PRECIO_KILO"]);

                ListItem item = new ListItem(nombre, codigo);
                materialesCB.Items.Add(item);

            }
            materialesCB.DataBind();

            BLManejadorBodega manejadorB = new BLManejadorBodega();
            List<BLBodegaTabla> bodegas = manejadorB.listaBodegas();
            foreach (BLBodegaTabla b in bodegas)
            {

                ListItem item = new ListItem(b.nombre, b.codigo);
                bodegasDrop.Items.Add(item);
            }
            bodegasDrop.DataBind();

        }

        protected void btnFiltros_Click(object sender, EventArgs e)
        {
            String fechaInicio = "";
            String fechaFin = "";
            String tipo = "";
            String pesoMaximo = "";
            String pesoMinimo = "";
            String bodega = "";
            List<String> materiales = new List<string>();

            if (!String.IsNullOrEmpty(fechaInicioTB.Text))
                fechaInicio = fechaInicioTB.Text;
            if (!String.IsNullOrEmpty(fechaFinTB.Text))
                fechaFin = fechaFinTB.Text;
            if (tipoRadioL.SelectedItem != null && tipoRadioL.SelectedItem != tipoRadioL.Items[0])
                tipo = tipoRadioL.SelectedItem.Text;
            if (!String.IsNullOrEmpty(pesoMax.Text))
                pesoMaximo = pesoMax.Text;
            if (!String.IsNullOrEmpty(pesoMin.Text))
                pesoMinimo = pesoMin.Text;
            if (bodegasDrop.SelectedItem != bodegasDrop.Items[0])
                bodega = bodegasDrop.SelectedItem.Value;

            foreach (ListItem material in materialesCB.Items) {

                if (material.Selected)
                    materiales.Add(material.Value);

            }
            DataSet ajustesFiltrados = manejadorA.filtrarAjustes(fechaInicio, fechaFin, tipo, pesoMaximo, pesoMinimo, bodega, materiales);
            cargarTabla(ajustesFiltrados);
        }
    }
}