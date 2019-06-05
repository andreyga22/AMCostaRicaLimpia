using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class BusquedaFacturas : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                cargarGrid();
                //lblError.Visible = false;
            //}
            //catch (Exception)
            //{
            //    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error al cargar la información. </strong>Verifique su conexión a internet.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
            //    lblError.Visible = true;
            //}
        }

        private void cargarGrid()
        {
            try
            {
                List<BLVenta> listFacturas = new BLManejadorVentas().facturasVentas();

                //listaConsultaGV.Columns[1].Visible = false;
                gridFacturas.DataSource = listFacturas;

                gridFacturas.DataBind();
                gridFacturas.HeaderRow.Cells[1].Text = "Código Factura";
                gridFacturas.HeaderRow.Cells[4].Text = "Cédula";
                gridFacturas.HeaderRow.Cells[5].Text = "Monto";
                gridFacturas.HeaderRow.Cells[6].Text = "Fecha";
                gridFacturas.HeaderRow.Cells[7].Text = "Socio";

                foreach (GridViewRow row in gridFacturas.Rows)
                {
                    LinkButton lb = (LinkButton)row.Cells[0].Controls[0];
                    lb.Text = "Seleccionar";
                }

                gridFacturas.HeaderRow.Cells[2].Visible = false;
                for (int i = 0; i < gridFacturas.Rows.Count; i++)
                {
                    gridFacturas.Rows[i].Cells[2].Visible = false;
                }

                gridFacturas.HeaderRow.Cells[3].Visible = false;
                for (int i = 0; i < gridFacturas.Rows.Count; i++)
                {
                    gridFacturas.Rows[i].Cells[3].Visible = false;
                }
            }
            catch (Exception)
            {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error al cargar los datos de la lista. </strong>Por favor recargue la página o vuelva a la página principal.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }

        }


        protected void gridFact_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string id = gridEmpl.SelectedRow.Cells[1].Text;
                //BLManejadorEmpleado manejEmpleado = new BLManejadorEmpleado();
                //BLEmpleado empleado = manejEmpleado.obtenerEmpleado(id);
                //manejEmpleado.crearActualizarEmpleado(new BLEmpleado(empleado.id, empleado.contrasenna, empleado.rol, empleado.nombreEmpleado, !empleado.estado));
                //Response.Redirect("Administrador.aspx");
            }
            catch (Exception)
            {
                //lblMensaje.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Error. </strong>No se pudo seleccionar el empleado.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                //lblMensaje.Visible = true;
            }
        }
    }
}