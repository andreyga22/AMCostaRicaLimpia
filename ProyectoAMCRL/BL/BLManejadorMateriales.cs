using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;

namespace BL
{
    public class BLManejadorMateriales
    {

        DAOManejadorMateriales manejador = new DAOManejadorMateriales();
        public DataSet listarMaterialesBL() {
            return manejador.obtenerMateriales();
        }

        public double traerCantidadVendidaBL(String idM) {
            double resultado = 0;
            resultado = manejador.traerCantidadVendidaDAO(Int32.Parse(idM));
            return resultado;
        }

        public string registrarMaterialBL(string nom, string precio)
        {
            return manejador.registrarMaterialDAO(nom, precio);
        }

        public string actualizarMaterialBL(int cod, string nom, string precio)
        {
            return manejador.actualizarMaterialDAO( cod,  nom,  precio);
        }
    }
}
