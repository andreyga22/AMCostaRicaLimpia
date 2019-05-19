using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BL
{
    public class BLManejadorCompras
    {

        public String guadarDetallesCompraBL() {
            DAOManejadorCompras manejador = new DAOManejadorCompras();
            return manejador.registrarCompra();
        }

    }
}
