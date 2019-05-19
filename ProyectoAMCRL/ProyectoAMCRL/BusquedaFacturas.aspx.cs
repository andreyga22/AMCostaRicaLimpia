using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoAMCRL {
    public partial class BusquedaFacturas : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            
        }

        protected void facturasGrid_Load(object sender, EventArgs e) {
            BoundField test = new BoundField();
            test.DataField = "Código";
            test.HeaderText = "Código Factura";
            facturasGrid.Columns.Add(test);
            test.DataField = "Fecha";
            test.HeaderText = "Fecha";
            facturasGrid.Columns.Add(test);
            test.DataField = "Monto";
            test.HeaderText = "Monto";
            facturasGrid.Columns.Add(test);
            test.DataField = "Socio";
            test.HeaderText = "Socio";
            facturasGrid.Columns.Add(test);
            facturasGrid.DataBind();


            //facturasGrid.Rows.Add("54652", "22/09/2012", "50000", "Jorge Gutierres");
        }
    }
}