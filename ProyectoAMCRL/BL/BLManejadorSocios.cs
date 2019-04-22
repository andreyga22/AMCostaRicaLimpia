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

        public String agregarSocioBL(BLSocioNegocio socioBL)
        {

            return manejadorDAO.agregarSocio(generarClon(socioBL));
        }

        private TOSocioNegocio generarClon(BLSocioNegocio socioBL)
        { //prototype?
            return new TOSocioNegocio(socioBL.cedula, socioBL.cedula_asociado, socioBL.nombre,
                socioBL.rol, socioBL.apellido1, socioBL.apellido2, socioBL.tel1, socioBL.tel2, socioBL.correo, clonarDir(socioBL.direccion));
        }

        private TODireccion clonarDir(BLDireccion dir) {
            return new TODireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas);
        }

    }
}
