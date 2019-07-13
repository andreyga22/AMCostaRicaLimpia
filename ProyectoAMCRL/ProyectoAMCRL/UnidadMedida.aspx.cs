using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;


namespace ProyectoAMCRL {
    public partial class UnidadMedida : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(Session["cuentaLogin"] != null) {
                if(!this.IsPostBack) {
                    string id = (String)Session["idUnidad"];
                    if(!string.IsNullOrEmpty(id)) {
                        try {
                            BLManejadorUnidad man = new BLManejadorUnidad();
                            BLUnidad und = man.consultar(id);
                            codigoTb.Text = und.codigo;
                            codigoTb.Enabled = false;
                            nombreTB.Text = und.nombre;
                            equivalenciaTb.Text = Convert.ToString(und.equivalencia);
                            Boolean num = und.estado;
                            if(num) {
                                estadoRb.SelectedIndex = 0;
                            } else {
                                estadoRb.SelectedIndex = 1;
                            }
                            BLCuenta sesi = (BLCuenta)Session["cuentaLogin"];
                            if(sesi.rol.Equals('r')) {
                                estadoRb.Visible = false;
                            }
                        } catch(Exception) {
                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se pudo cargar los datos de la unidad de medida. Revise su conexión a internet.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        }
                    }
                }
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e) {
            try {
                BLCuenta sesi = (BLCuenta)Session["cuentaLogin"];
                if(sesi.rol.Equals('r')) {
                    BLManejadorUnidad man = new BLManejadorUnidad();
                    man.guardarActualizarRegular(new BLUnidad(codigoTb.Text.Trim(), nombreTB.Text.Trim(), Convert.ToDouble(equivalenciaTb.Text.Trim()), false));
                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó correctamente la unidad de medida.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                } else {
                    String estado = estadoRb.SelectedValue;
                    Boolean estadoB = true;
                    if(estado.Equals("Activado")) {
                        estadoB = true;
                    } else {
                        estadoB = false;
                    }
                    BLManejadorUnidad man = new BLManejadorUnidad();
                    man.guardarActualizarAdmin(new BLUnidad(codigoTb.Text.Trim(), nombreTB.Text.Trim(), Convert.ToDouble(equivalenciaTb.Text.Trim()), estadoB));
                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó correctamente la unidad de medida.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
            } catch(Exception exx) {
                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se pudo guardar la unidad de medida. Revise los datos y su conexión a internet.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }
    }
}