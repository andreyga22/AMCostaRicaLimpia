using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;

namespace BL
{
    public class BLManejadorAjustes
    {
        DAOManejadorAjustes manejador = new DAOManejadorAjustes();

        public DataSet listarAjustesBL()
        {
            return manejador.listarAjustesDAO();
        }
        //id_bod, peso, material, unidad, accion, razon
        public String registrarAjusteBL(String id_bod, string id_material, string id_stock, string peso, string unidadInfo, string accion, string razon)
        {
            
            int pesoN = Int32.Parse(peso);

            double pesoTotal = calcularTotalSegunUnidad(unidadInfo, pesoN);
            int idStockN = Int32.Parse(id_stock);

            if (accion.Equals("0")) { // caso: se debe restar al stock
                double resultadoRestaStock = stockMinimoSuperadoBL(pesoTotal, idStockN);

            if (resultadoRestaStock < 0) 
                    return "Error: la cantidad en inventario (" + (resultadoRestaStock + pesoTotal) +" kg), " +
                        "es menor a la cantidad del ajuste (" + pesoTotal+" kg).";
            }

            return manejador.registrarAjusteDAO(id_bod, id_material, idStockN, pesoTotal, accion,  razon);

        }

        public String buscarAjusteBL(string idAjuste)
        {
            return manejador.buscarAjusteDAO(idAjuste);
        }

        private double stockMinimoSuperadoBL(double peso, int id_stock)
        {
            double cantidad_stock = manejador.consultarCantidadStockDAO(id_stock);
            return cantidad_stock - peso;

        }

        private double calcularTotalSegunUnidad(string unidad, int pesoN)
        {
            //se deberia validar contra las unidades de peso y equivalencias respectivas guardadas en BD
            String[] codUnidadInfo = unidad.Split('*');
            String codUnidad = codUnidadInfo[0];
            double equivalencia = Double.Parse(codUnidadInfo[1]);
            return equivalencia * pesoN;
        }

        public DataSet filtrarAjustes(string fechaInicio, string fechaFin, string tipo, string pesoMaximo, string pesoMinimo, string bodega, List<string> materiales)
        {
            return manejador.filtrarAjustesDAO( fechaInicio,  fechaFin,  tipo, pesoMaximo, pesoMinimo, bodega,  materiales);
        }
    }
}
