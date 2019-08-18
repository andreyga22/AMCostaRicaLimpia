using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL
{
    public partial class RegistroSociosUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cuentaLogin"] != null)
            {
                if (!this.IsPostBack)
                {
                    BLCuenta usuarioLogin = (BLCuenta)Session["cuentaLogin"];
                    String cedula = (String)Session["idSocio"];
                    if (usuarioLogin.rol.Equals("r"))
                    {
                        if (!String.IsNullOrEmpty(cedula))
                        {
                            LinkAsoc.Visible = true;
                            BLManejadorSocios manejador = new BLManejadorSocios();
                            BLSocioNegocio socio = manejador.buscarCedula(cedula);
                            idTB.Enabled = false;
                            idTB.Text = socio.cedula;
                            nombreTB.Text = socio.nombre;
                            ape1TB.Text = socio.apellido1;
                            ape2TB.Text = socio.apellido2;

                            Boolean ess = socio.estado_socio;
                            int est = 0;
                            if (ess)
                            {
                                est = 0;
                            }
                            else
                            {
                                est = 1;
                            }
                            estadoRb.SelectedIndex = est;
                            
                            if (socio.rol.Equals("Proveedor"))
                            {
                                rolRadios.SelectedIndex = 0;
                            }
                            else
                            {
                                rolRadios.SelectedIndex = 1;
                            }

                            provinciaTB.Text = socio.direccion.provincia;
                            cantonTB.Text = socio.direccion.canton;
                            distritoTB.Text = socio.direccion.distrito;
                            sennas.Text = socio.direccion.otras_sennas;

                            telTB.Text = socio.contactos.telefono_hab;
                            tel2TB.Text = socio.contactos.telefono_pers;
                            correoTB.Text = socio.contactos.email;
                            estadolb.Visible = false;
                            estadoRb.Visible = false;
                            LinkAsoc.Visible = true;
                        }
                        else {
                            estadolb.Visible = false;
                            estadoRb.Visible = false;
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(cedula))
                        {
                            LinkAsoc.Visible = true;
                            BLManejadorSocios manejador = new BLManejadorSocios();
                            BLSocioNegocio socio = manejador.buscarCedula(cedula);
                            idTB.Enabled = false;
                            idTB.Text = socio.cedula;
                            nombreTB.Text = socio.nombre;
                            ape1TB.Text = socio.apellido1;
                            ape2TB.Text = socio.apellido2;

                            Boolean ess = socio.estado_socio;
                            int est = 0;
                            if (ess)
                            {
                                est = 0;
                            }
                            else
                            {
                                est = 1;
                            }
                            estadoRb.SelectedIndex = est;

                            //activaCb.Checked = true;
                            if (socio.rol.Equals("Proveedor"))
                            {
                                rolRadios.SelectedIndex = 0;
                            }
                            else
                            {
                                rolRadios.SelectedIndex = 1;
                            }

                            provinciaTB.Text = socio.direccion.provincia;
                            cantonTB.Text = socio.direccion.canton;
                            distritoTB.Text = socio.direccion.distrito;
                            sennas.Text = socio.direccion.otras_sennas;

                            telTB.Text = socio.contactos.telefono_hab;
                            tel2TB.Text = socio.contactos.telefono_pers;
                            correoTB.Text = socio.contactos.email;

                            LinkAsoc.Visible = true;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            BLManejadorSocios manejador = new BLManejadorSocios();

            String cedula = (String)Session["idSocio"];
            if (!String.IsNullOrEmpty(cedula))
            {
                try
                {
                    BLSocioNegocio socio = new BLSocioNegocio();
                    socio.cedula = idTB.Text.ToString();
                    socio.nombre = nombreTB.Text.ToString();
                    socio.apellido1 = ape1TB.Text.ToString();
                    socio.apellido2 = ape2TB.Text.ToString();
                    socio.rol = rolRadios.SelectedValue.ToString();
                    BLSocioNegocio socioTemp = manejador.buscarCedula(cedula);
                    socio.direccion = new BLDireccion(provinciaTB.Text.ToString(), cantonTB.Text.ToString(),
                    distritoTB.Text.ToString(), sennas.Text.ToString(), socioTemp.direccion.cod_direccion);
                    socio.contactos = new BLContactos(telTB.Text.Trim(),
                    tel2TB.Text.Trim(), correoTB.Text.ToString());
                    socio.direccion.cod_direccion = manejador.buscarDir(idTB.Text.ToString());
                    String estado = estadoRb.SelectedValue;
                    Boolean estadoB = true;
                    if(estado.Equals("Activado")) {
                        estadoB = true;
                    } else {
                        estadoB = false;
                    }
                    socio.estado_socio = estadoB;
                    Boolean insertado = manejador.agregarSocioBL(socio);
                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se modificó el socio correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
                catch (Exception ex)
                {
                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + ex.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
            }
            else
            {
                try
                {
                    BLSocioNegocio socio = new BLSocioNegocio();
                    socio.cedula = idTB.Text.ToString();
                    socio.nombre = nombreTB.Text.ToString();
                    socio.apellido1 = ape1TB.Text.ToString();
                    socio.apellido2 = ape2TB.Text.ToString();
                    socio.rol = rolRadios.SelectedValue.ToString();
                    socio.direccion = new BLDireccion(provinciaTB.Text.ToString(), cantonTB.Text.ToString(),
                    distritoTB.Text.ToString(), sennas.Text.ToString(), 0);
                    socio.contactos = new BLContactos(telTB.Text.Trim(),
                    tel2TB.Text.Trim(), correoTB.Text.ToString());
                    //if (activaCb.Checked)
                    //{
                    //    socio.estado_socio = true;
                    //}
                    //else
                    //{
                    //    socio.estado_socio = false;
                    //}
                    String estado = estadoRb.SelectedValue;
                    Boolean estadoB = true;
                    if(estado.Equals("Activado")) {
                        estadoB = true;
                    } else {
                        estadoB = false;
                    }
                    socio.estado_socio = estadoB;
                    Boolean insertado = manejador.agregarSocioBL(socio);
                    lblError.Text = "<div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>¡Éxito! </strong>Se modificó el socio correctamente.<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;

                }
                catch (Exception ex)
                {
                    lblError.Text = "<div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>¡Error! </strong> " + ex.Message + "<button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }

            }
        }

        protected void LinkAsoc_Click(object sender, EventArgs e)
        {
            Response.Redirect("Asociar_Socio.aspx");
        }
    }
}
