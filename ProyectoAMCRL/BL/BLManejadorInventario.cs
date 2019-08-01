using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BL {
    public class BLManejadorInventario {

        DAOInventario manejador = new DAOInventario();

        public DataTable buscarStock(string bodega)
        {
            return null;
        }

        public List<String> buscarBodegas()
        {
            return manejador.buscarBodegas();
        }
    }
}
