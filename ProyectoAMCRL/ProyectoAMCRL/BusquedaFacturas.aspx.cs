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

                List<BLVenta> listFacturas = new BLManejadorVentas().facturasVentas();

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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

        }
    }
}