using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class Monedas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["cuentaLogin"] != null) {
                if(!this.IsPostBack) {
                    string id = (String)Session["idMoneda"];
                    if(!string.IsNullOrEmpty(id)) {

                        BLManejadorMoneda man = new BLManejadorMoneda();
                        BLMoneda und = man.consultar(id);
                        codigoTb.Text = und.idMoneda;
                        codigoTb.Enabled = false;
                        detalleTb.Text = und.detalleMoneda;
                        equivalenciaTb.Text = Convert.ToString(und.equivalencia_Colon);
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
                    BLManejadorMoneda man = new BLManejadorMoneda();
                    man.guardarActualizarRegular(new BLMoneda(codigoTb.Text.Trim(), detalleTb.Text.Trim(), Convert.ToDouble(equivalenciaTb.Text.Trim()), false));
                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó correctamente la moneda.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                } else {
                    String estado = estadoRb.SelectedValue;
                    Boolean estadoB = true;
                    if(estado.Equals("Activado")) {
                        estadoB = true;
                    } else {
                        estadoB = false;
                    }
                    BLManejadorMoneda man = new BLManejadorMoneda();
                    man.guardarActualizarAdmin(new BLMoneda(codigoTb.Text.Trim(), detalleTb.Text.Trim(), Convert.ToDouble(equivalenciaTb.Text.Trim()), estadoB));
                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó correctamente la moneda.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
            } catch(Exception exx) {
                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }
    }
}