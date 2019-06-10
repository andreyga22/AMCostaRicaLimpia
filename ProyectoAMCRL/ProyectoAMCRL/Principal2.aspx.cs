using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class Principal2 : System.Web.UI.Page
    {
        BLManejadorSocios manejadorSocios = new BLManejadorSocios();
        List<BLSocioNegocio> sociosD = new List<BLSocioNegocio>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack) {
            //    sociosD = manejadorSocios.cargarLista();
            //    cargarTabla();
            //}


        }

        //private void cargarTabla() {
        //    foreach (BLSocioNegocio socio in sociosD) {
              
        //        TableCell idCell = new TableCell();
        //        TableCell nombreCell = new TableCell();
        //        TableCell emailCell = new TableCell();
        //        TableCell telCell = new TableCell();
        //        TableRow filaNueva = new TableRow();

        //        idCell.Text = socio.cedula;
        //        nombreCell.Text = socio.nombre;
        //        emailCell.Text = socio.correo;
        //        telCell.Text = socio.telPers+"";
        //        nombreCell.ForeColor = System.Drawing.Color.Blue;

        //        filaNueva.Cells.Add(idCell);
        //        filaNueva.Cells.Add(nombreCell);
        //        filaNueva.Cells.Add(emailCell);
        //        filaNueva.Cells.Add(telCell);

        //        tablaSocios.Rows.Add(filaNueva);
        //    }
        //    tablaSocios.DataBind();
        //}


    }
}