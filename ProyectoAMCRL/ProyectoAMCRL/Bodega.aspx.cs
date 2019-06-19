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
            if(Session["cuentaLogin"] != null) {
                if(!IsPostBack) {
                    try {
                        string id = (String)Session["idBodega"];
                        if(!string.IsNullOrEmpty(id)) {
                            BLCuenta cuenta = (BLCuenta)Session["cuentaLogin"];
                            if(cuenta.rol.Equals("a")) {

                                BLBodega miBod = consultarBodegaAdmin(id);
                                codigoTb.Text = miBod.codigo;
                                codigoTb.Enabled = false;
                                nombreTB.Text = miBod.nombre;
                                Boolean ess = miBod.estado;
                                int est = 0;
                                if(ess) {
                                    est = 0;
                                } else {
                                    est = 1;
                                }
                                estadoRb.SelectedIndex = est;
                                provinciaTb.Text = miBod.direccion.provincia;
                                cantonTb.Text = miBod.direccion.canton;
                                distritoTb.Text = miBod.direccion.distrito;
                                otrasTb.Text = miBod.direccion.otras_sennas;
                            } else {
                                BLBodega miBod = consultarBodegaAdmin(id);
                                codigoTb.Text = miBod.codigo;
                                codigoTb.Enabled = false;
                                nombreTB.Text = miBod.nombre;
                                estado.Visible = false;
                                provinciaTb.Text = miBod.direccion.provincia;
                                cantonTb.Text = miBod.direccion.canton;
                                distritoTb.Text = miBod.direccion.distrito;
                                otrasTb.Text = miBod.direccion.otras_sennas;
                            }
                        }
                    } catch(Exception exx) {
                        lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                }
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e) {
            try {
                string id = (String)Session["idBodega"];
                if(!string.IsNullOrEmpty(id)) {
                    try {
                        BLCuenta cuenta = (BLCuenta)Session["cuentaLogin"];
                        if(cuenta.rol.Equals("a")) {
                            BLBodega miBod = consultarBodegaAdmin(id);
                            String estado = estadoRb.SelectedValue;
                            Boolean estadoB = true;
                            if(estado.Equals("Activado")) {
                                estadoB = true;
                            } else {
                                estadoB = false;
                            }
                            BLBodega bodega = new BLBodega(codigoTb.Text.Trim(), nombreTB.Text.Trim(), estadoB, new BLDireccion(provinciaTb.Text.Trim(), cantonTb.Text.Trim(), distritoTb.Text.Trim(), otrasTb.Text.Trim(), miBod.direccion.cod_direccion));
                            BLManejadorBodega man = new BLManejadorBodega();
                            man.guardarModificarBodegaAdmin(bodega);

                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se modificó la bodega correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        } else {
                            BLBodega miBod = consultarBodegaRegular(id);
                            
                            BLBodega bodega = new BLBodega();
                            bodega.codigo = codigoTb.Text.Trim();
                            bodega.nombre = nombreTB.Text.Trim();
                            BLDireccion dir = new BLDireccion(provinciaTb.Text.Trim(), cantonTb.Text.Trim(), distritoTb.Text.Trim(), otrasTb.Text.Trim(), miBod.direccion.cod_direccion);
                            bodega.direccion = dir;

                            BLManejadorBodega man = new BLManejadorBodega();
                            man.guardarModificarBodegaRegular(bodega);
                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se modificó la bodega correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        }
                    } catch(Exception exx) {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                } else {
                    try {
                        //String estado = estadoRb.SelectedValue;
                        //Boolean estadoB = true;
                        //if(estado.Equals("Activado")) {
                        //    estadoB = true;
                        //} else {
                        //    estadoB = false;
                        //}
                        //BLBodega bodega = new BLBodega(codigoTb.Text.Trim(), nombreTB.Text.Trim(), estadoB, new BLDireccion(provinciaTb.Text.Trim(), cantonTb.Text.Trim(), distritoTb.Text.Trim(), otrasTb.Text.Trim(), 0));
                        //BLManejadorBodega man = new BLManejadorBodega();
                        //man.guardarModificarBodegaAdmin(bodega);
                        //lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó la bodega correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        //lblError.Visible = true;


                        BLCuenta cuenta = (BLCuenta)Session["cuentaLogin"];
                        if(cuenta.rol.Equals("a")) {
                            String estado = estadoRb.SelectedValue;
                            Boolean estadoB = true;
                            if(estado.Equals("Activado")) {
                                estadoB = true;
                            } else {
                                estadoB = false;
                            }
                            BLBodega bodega = new BLBodega(codigoTb.Text.Trim(), nombreTB.Text.Trim(), estadoB, new BLDireccion(provinciaTb.Text.Trim(), cantonTb.Text.Trim(), distritoTb.Text.Trim(), otrasTb.Text.Trim(), 0));
                            BLManejadorBodega man = new BLManejadorBodega();
                            man.guardarModificarBodegaAdmin(bodega);
                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó la bodega correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        } else {

                            BLBodega bodega = new BLBodega();
                            bodega.codigo = codigoTb.Text.Trim();
                            bodega.nombre = nombreTB.Text.Trim();
                            BLDireccion dir = new BLDireccion(provinciaTb.Text.Trim(), cantonTb.Text.Trim(), distritoTb.Text.Trim(), otrasTb.Text.Trim(), 0);
                            bodega.direccion = dir;
                            BLManejadorBodega man = new BLManejadorBodega();
                            man.guardarModificarBodegaRegular(bodega);
                            lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se guardó la bodega correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                            lblError.Visible = true;
                        }



                    } catch(Exception exx) {
                        lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                        lblError.Visible = true;
                    }
                }

            } catch(Exception exx) {
                lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + exx.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }
        }

        private BLBodega consultarBodegaAdmin(String id) {
            try {
                BLManejadorBodega man = new BLManejadorBodega();
                return man.consultarBodegaAdmin(id);
            } catch(Exception exx) {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se puede consultar la bodega<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
                return null;
            }
        }

        private BLBodega consultarBodegaRegular(String id) {
            try {
                BLManejadorBodega man = new BLManejadorBodega();
                return man.consultarBodegaRegular(id);
            } catch(Exception exx) {
                lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> No se puede consultar la bodega<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
                return null;
            }
        }
    }
}