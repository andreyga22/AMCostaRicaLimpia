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

        BLManejadorAjustes manejadorA = new BLManejadorAjustes();


        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {
                if(Request.QueryString.Get("res") == "1"){
                    lblError.Text = "<br /><br /><div class=\"alert alert-success alert - dismissible fade show\" role=\"alert\"> <strong>" + "Ajuste registrado" + "</strong><button type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\" onclick=\"cerrarError()\"> <span aria-hidden=\"true\">&times;</span> </button> </div>";
                    lblError.Visible = true;
                }

                    
            }
            cargarTabla();

        }

        private void cargarTabla() {

            DataSet dataSet = manejadorA.listarAjustesBL();

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
                String idBodega = Convert.ToString(dr["ID_BODEGA"]);

                String btnHTML2 = "<a href='#' data-toggle='popover' data-placement='left' title='Detalle ajuste' data-html='true' data-content='Some content "+ idAjuste+" popover'>Ver</a>";
                String idEncriptado = BLManejadorEncripcion.Encrypt(idAjuste);
                String btnHTML = "<input id='" + idAjuste + "' type='button' class='btn btn-sm btn-link' value='"+idAjuste+"' >";
                String filaHTML = "<tr onclick='abrirDetalleClick(" + idAjuste + ")'>" +
                "<td>" + idAjuste + "</td>" +
                "<td>" + fechaInfo + "</td>" +
                "<td>" + peso + "</td>" +
                "<td>" + idBodega + "</td>" +
                "<td>" + movimiento + "</td >" +
                "</tr> ";
                cuerpoTablaHTML += filaHTML;
            }
            tablaPlaceHolder.Controls.Add(new Literal { Text = cuerpoTablaHTML.ToString() });


        }


    }
}