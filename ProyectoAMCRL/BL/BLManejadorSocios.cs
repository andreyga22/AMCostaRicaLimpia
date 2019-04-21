using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;

namespace BL
{
    public class BLManejadorSocios
    {
        private DAOManejadorSocios manejadorDAO = new DAOManejadorSocios();

        public String agregarSocioBL(TOSocioNegocio socioTO)
        {
            return manejadorDAO.agregarSocio(socioTO);
        }

        private BLSocioNegocio generarClon(TOSocioNegocio socioTO)
        { //prototype?
            return new BLSocioNegocio(socioTO.cedula, socioTO.cedula_asociado, socioTO.apellido1, socioTO.apellido2, socioTO.rol);
        }

    }
}
