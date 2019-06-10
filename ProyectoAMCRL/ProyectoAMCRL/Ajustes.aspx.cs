﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace ProyectoAMCRL {
    public partial class Ajustes : System.Web.UI.Page {

        BLManejadorAjustes manejador = new BLManejadorAjustes();
        BLManejadorMateriales manejadorM = new BLManejadorMateriales();

        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {

                unidadTB.Items.Add("KG");
                unidadTB.Items.Add("TONELADA");
                unidadTB.Items.Add("TARIMA");

                bodegasDrop.Items.Add("B001");
                cargarMateriales();


            }
            cargarTabla();

        }

        private void cargarMateriales()
        {                           // se debe tomar el valor el sesion de la bodega
            DataSet materialesDS = manejadorM.listarMaterialesEnBodegaBL("B01"); // <--------
            stock_id_escondido.Value = Convert.ToString(materialesDS.Tables[0].Rows[0]["ID_STOCK"]);

            foreach (DataRow dr in materialesDS.Tables[0].Rows)
            {
                ListItem item = new ListItem();
                
                String codigo = Convert.ToString(dr["COD_MATERIAL"]);
                String nombre = Convert.ToString(dr["NOMBRE_MATERIAL"]);
                String id_stock = Convert.ToString(dr["ID_STOCK"]);
                item.Text = nombre;
                item.Value = codigo+"-"+ id_stock;
                materialDD.Items.Add(item);
            }
        }

        private void cargarTabla() {

            DataSet dataSet = manejador.listarAjustesBL();

            String cuerpoTablaHTML = "";

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                //fecha, peso, movimiento, stock   12/20/2019 12:00:00 AM
                String fechaInfo =  Convert.ToString(dr["Fecha_Ajuste"]);
   
                String peso = Convert.ToString(dr["PESO_AJUSTE"]);
                String movimientoNumber = Convert.ToString(dr["MOVIMIENTO_A"]);
                String movimiento = "";
                switch (movimientoNumber) {
                    case "1":
                        movimiento = "ENTRADA";
                        break;

                    case "0":
                        movimiento = "SALIDA";
                        break;
                       
                }

                String stock = Convert.ToString(dr["ID_STOCK"]);
                String idAjuste = Convert.ToString(dr["ID_AJUSTE"]);

                String btnHTML = "<input id='" + idAjuste + "' type='button' class='btn btn-sm btn-link' value='Ver' onclick='abrirDetalleClick(this.id)'>";
                String filaHTML = "<tr>" +
                "<td>" + fechaInfo + "</td>" +
                "<td>" + peso + "</td>" +
                "<td>" + movimiento + "</td >" +
                "<td>" + btnHTML + "</td >" +
                "</tr> ";
                cuerpoTablaHTML += filaHTML;
            }
            tablaPlaceHolder.Controls.Add(new Literal { Text = cuerpoTablaHTML.ToString() });


        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(pesoTB.Text) || radioAccion.SelectedItem != null)
            {
                String m = "";
                //id_bod, peso, material, unidad, accion, razon
                String[] materialInfo = materialDD.SelectedItem.Value.Split('-');
                String idMaterial = materialInfo[0];
                String idStockMaterial = materialInfo[1];

                m = manejador.registrarAjusteBL("B01", idMaterial, idStockMaterial, pesoTB.Text, unidadTB.SelectedItem.Text, radioAccion.SelectedItem.Value, razonTb.Text);

                if (m.Equals("Operación efectuada correctamente"))
                {
                    lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
                else
                {
                    lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>" + m + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }
            }
            else {
                lblError.Text = "<br /><br /><div class=\"alert alert-danger alert - dismissible fade show\" role=\"alert\"> <strong>Datos incompletos, intente de nuevo</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                lblError.Visible = true;
            }



                            
        }
    }
}