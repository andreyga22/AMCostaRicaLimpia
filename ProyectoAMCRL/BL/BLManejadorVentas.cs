using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BL
{
    public class BLManejadorVentas
    {

        public String guadarDetallesVentaBL()
        {
            DAOManejadorVentas manejador = new DAOManejadorVentas();
            return manejador.registrarDetalles();
        }

    }
}
