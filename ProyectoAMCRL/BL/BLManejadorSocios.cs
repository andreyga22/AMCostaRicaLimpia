using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;
using System.Data;

namespace BL
{
    public class BLManejadorSocios
    {
        private DAOManejadorSocios manejadorDAO = new DAOManejadorSocios();

        //Sin asociacion
        public Boolean agregarSocioBL(BLSocioNegocio socioBL)
        {
            TOSocioNegocio socioTO = new TOSocioNegocio();
            socioTO.cedula = socioBL.cedula;
            socioTO.nombre = socioBL.nombre;
            socioTO.rol = socioBL.rol;
            socioTO.apellido1 = socioBL.apellido1;
            socioTO.apellido2 = socioBL.apellido2;
            socioTO.contactos = new TOContactos(socioBL.contactos.telefono_hab, socioBL.contactos.telefono_pers, socioBL.contactos.email);
            socioTO.direccion = new TODireccion(socioBL.direccion.provincia, socioBL.direccion.canton, socioBL.direccion.distrito, socioBL.direccion.otras_sennas);
            socioTO.estado_socio = socioBL.estado_socio;
            manejadorDAO.agregarModificarSocio(socioTO);
            return true;
        }

        public DataTable buscarSocio(String busqueda)
        {
            return manejadorDAO.buscarSocio(busqueda);

        }

    }
}
