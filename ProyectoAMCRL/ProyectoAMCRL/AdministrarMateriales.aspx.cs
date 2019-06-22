using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BL;

namespace ProyectoAMCRL
{
    public partial class Materiales : System.Web.UI.Page
    {
        BLManejadorMateriales manejador = new BLManejadorMateriales();
        BLManejadorBodega manejadorB = new BLManejadorBodega();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                cargarBodegas();
                llenarTablaMateriales("");
            }
            

        }

        /*
         PROCESO: 

        ENTRADAS:

        SALIDAS:
        */
        private void llenarTablaMateriales(String idBodega) {

            DataSet dataSet = new DataSet();

            if (idBodega.Equals("")) 
                dataSet = manejador.listarMaterialesBL();
            else
                dataSet = manejador.listarMaterialesEnBodegaBL(idBodega);

            String cuerpoTablaHTML = "";

            foreach (DataRow dr in dataSet.Tables[0].Rows) {

                String codigo = Convert.ToString(dr["COD_MATERIAL"]);
                String nombre = Convert.ToString(dr["NOMBRE_MATERIAL"]);
                String precio = Convert.ToString(dr["PRECIO_KILO"]);


                String infoBTN = codigo + "."+ nombre + '.' + precio;
                String btnHTML = "<input id='"+ infoBTN + "' type='button' class='btn btn-sm btn-link' value='Editar'>";
                String filaHTML = "<tr  onclick='abrirDetalleClick("+ codigo + ")' >" +
                "<td>"+codigo+"</td>" +
                "<td>"+ nombre + "</td>" +
                "<td>"+ precio + "</td >" +
                "</tr> ";
                cuerpoTablaHTML += filaHTML;
            }
            tablaPlaceHolder.Controls.Add(new Literal { Text = cuerpoTablaHTML.ToString() });

        }


        private void cargarBodegas()
        {
            List<BLBodegaTabla> bodegas = manejadorB.listaBodegas();
            foreach (BLBodegaTabla b in bodegas)
            {

                ListItem item = new ListItem(b.nombre, b.codigo);
                bodegasDrop.Items.Add(item);
            }

        }

        protected void bodegasDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            String idBodega = bodegasDrop.SelectedItem.Value;
            llenarTablaMateriales(idBodega);
        }
    }
}