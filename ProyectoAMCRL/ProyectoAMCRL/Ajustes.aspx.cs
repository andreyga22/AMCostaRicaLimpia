using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoAMCRL {
    public partial class Ajustes : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {

                if (Request.QueryString["val1"] != null)
                TextBox1.Text = Request.QueryString["val1"];

                unidadTB.Items.Add("KG");
                unidadTB.Items.Add("TONELADA");
                productosTB.Items.Add("COBRE");
                productosTB.Items.Add("LATA");
                productosTB.Items.Add("HIERRO");
                productosTB.Items.Add("ALUMINIO");
                bodegasDrop.Items.Add("B001");

                cargarTabla();
            }
           
        }

        private void cargarTabla() {
            String tablaHTML = "<tr>" +
                "<td>Aluminio</td>" +
                "<td> 20 </td>" +
                "<td> KG </td >"+
                "<td> Bodega001 </td>" +
                "<td> Aum </td>" +
                "<td> 15/07/2015 </td>" +
                "</tr> ";
            tablaPlaceHolder.Controls.Add(new Literal { Text = tablaHTML.ToString() });
        }




    }
}