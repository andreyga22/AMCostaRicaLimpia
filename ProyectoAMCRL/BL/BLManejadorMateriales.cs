using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;
using TO;

namespace BL
{
    public class BLManejadorMateriales
    {

        DAOManejadorMateriales manejador = new DAOManejadorMateriales();
        public DataSet listarMaterialesBL() {
            return manejador.obtenerMateriales();
        }
        

       public DataSet listarMaterialesEnBodegaBL(String idBodega){
            DataSet materiales = manejador.obtenerMaterialesEnBodegaActual(idBodega);
            return materiales;
        }

        public double traerCantidadVendidaBL(String idM) {
            double resultado = 0;
            resultado = manejador.traerCantidadVendidaDAO(Int32.Parse(idM));
            return resultado;
        }

        public string registrarActualizarMaterialBL(string code, string nom, string precioT)
        {
            if (nom.Equals("") || precioT.Equals(""))
                return "Datos incompletos. Por favor, verifique e intente de nuevo";

            int codigo;
            try {
               codigo = Int32.Parse(code);
            }
            catch (Exception ex) {
                codigo = -1;
            }
           

            double precio = Double.Parse(precioT);
            TOMaterial m = new TOMaterial(codigo, nom, precio);
            
            return manejador.registrarActualizarMaterialDAO(m);
        }
    }
}
