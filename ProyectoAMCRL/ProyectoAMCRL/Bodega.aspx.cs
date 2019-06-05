using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL {
    public partial class Bodega : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {


            string id = (String)Session["idBodega"];
            if(!string.IsNullOrEmpty(id)) {
                BLBodega miBod = consultarBodega(id);
                codigoTb.Text = miBod.codigo;
                codigoTb.Enabled = false;
                nombreTB.Text = miBod.nombre;
                activaCb.Enabled = miBod.estado;
                provinciaTb.Text = miBod.direccion.provincia;
                cantonTb.Text = miBod.direccion.canton;
                distritoTb.Text = miBod.direccion.distrito;
                otrasTb.Text = miBod.direccion.otras_sennas;
                activaCb.Enabled = miBod.estado;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e) {
            //BLBodega bod = (BLBodega)(Session["miBod"]);
            //BLDireccion dir = (BLDireccion)(Session["miDi"]);
            //try {
            string id = (String)Session["idBodega"];
            if(!string.IsNullOrEmpty(id)) {

                BLBodega miBod = consultarBodega(id);


                BLBodega bodega = new BLBodega(codigoTb.Text.Trim(), nombreTB.Text.Trim(), activaCb.Enabled, new BLDireccion(provinciaTb.Text.Trim(), cantonTb.Text.Trim(), distritoTb.Text.Trim(), otrasTb.Text.Trim(), miBod.direccion.cod_direccion));
                BLManejadorBodega man = new BLManejadorBodega();
                man.guardarModificarBodega(bodega);
                lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>Se guardó la bodega con éxito.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            } else {
                BLBodega bodega = new BLBodega(codigoTb.Text.Trim(), nombreTB.Text.Trim(), activaCb.Enabled, new BLDireccion(provinciaTb.Text.Trim(), cantonTb.Text.Trim(), distritoTb.Text.Trim(), otrasTb.Text.Trim(), 0));
                BLManejadorBodega man = new BLManejadorBodega();
                man.guardarModificarBodega(bodega);
                lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>Se guardó la bodega con éxito.</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }

            //} catch(Exception) {
            //    lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>No se pudo guardar los datos</strong> Por favor intentelo de nuevo.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
            //}
        }

        private BLBodega consultarBodega(String id) {
            BLManejadorBodega man = new BLManejadorBodega();
            return man.consultarBodega(id);
        }
    }
}