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

        public DataTable buscarFiltrado(string busqueda)
        {
            return new DAOManejadorSocios().buscarSociosFiltro(busqueda);
        }

        public BLSocioNegocio buscarSocio(String id, String tipoSocio) {
            DAOManejadorSocios manejadorDAO = new DAOManejadorSocios();
            TOSocioNegocio socioTO = manejadorDAO.buscarSocio(id, tipoSocio);
            BLSocioNegocio socioBL = null;

            if (socioTO != null)
                socioBL = parsearSocio(socioTO);
            else
                return null;

            return socioBL;
        }

        private BLSocioNegocio parsearSocio(TOSocioNegocio socioTO) {

                BLSocioNegocio socioBL = new BLSocioNegocio();
                socioBL.cedula = socioTO.cedula;
                socioBL.nombre = socioTO.nombre;
                socioBL.apellido1 = socioTO.apellido1;
                socioBL.apellido2 = socioTO.apellido2;
                socioBL.rol = socioTO.rol;
                socioBL.estado_socio = socioTO.estado_socio;
                BLContactos contactos = null;
                if (socioTO.contactos != null)
                    contactos = new BLContactos(socioTO.contactos.telefono_hab, socioTO.contactos.telefono_pers, socioTO.contactos.email);
                socioBL.contactos = contactos;
                TODireccion tODireccion = socioTO.direccion;
                if(tODireccion != null)
                socioBL.direccion = new BLDireccion(tODireccion.provincia, tODireccion.canton, tODireccion.distrito, tODireccion.otras_sennas, tODireccion.cod_direccion);
  
            return socioBL;
        }


    }
}
