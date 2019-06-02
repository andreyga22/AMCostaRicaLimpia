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

    }
}
