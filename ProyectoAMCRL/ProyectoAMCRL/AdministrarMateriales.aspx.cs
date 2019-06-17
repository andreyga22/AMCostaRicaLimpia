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

        protected void Page_Load(object sender, EventArgs e)
        { 
            llenarTablaMateriales();

        }

        private void llenarTablaMateriales() {
            DataSet dataSet = manejador.listarMaterialesBL();

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
    }
}