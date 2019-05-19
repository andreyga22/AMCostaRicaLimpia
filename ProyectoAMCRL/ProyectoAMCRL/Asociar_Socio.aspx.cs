using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoAMCRL
{
    public partial class Asociar_Socio : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("#", typeof(int)),
                new DataColumn("Cedula", typeof(string)),
                    new DataColumn("Nombre", typeof(string)),
                    new DataColumn("Telefono",typeof(string)), new DataColumn("Asociar",typeof(string)) });
            dt.Rows.Add(1, "2850349506", "Alberto Alvarez Penado", "82837465","");
            dt.Rows.Add(2, "2345689545", "Alvin Esquivel Lopez", "830293849", "");
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

    }
}