using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BL
{
    public class BLManejadorUnidades
    {
        DAOManejadorUnidades manejadorU = new DAOManejadorUnidades();
        public DataSet listarUnidades() {
            return manejadorU.listarUnidadesDAO();
        }

        internal double consultarEquivalenciaUnidadBL(string codUnidad)
        {
            return manejadorU.consultarEquivalenciaUnidadDAO(codUnidad);
        }
    }
}
