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

        /// <summary>
        /// Método para agregar los datos asociados a un cliente o proveedor
        /// del negocio.
        /// </summary>
        /// <param name="socioBL">Socio del negocio</param>
        /// <returns></returns>
        public Boolean agregarSocioBL(BLSocioNegocio socioBL)
        {
            try
            {
                TOSocioNegocio socioTO = new TOSocioNegocio();
                socioTO.cedula = socioBL.cedula;
                socioTO.nombre = socioBL.nombre;
                socioTO.rol = socioBL.rol;
                socioTO.apellido1 = socioBL.apellido1;
                socioTO.apellido2 = socioBL.apellido2;
                socioTO.contactos = new TOContactos(socioBL.contactos.telefono_hab, socioBL.contactos.telefono_pers, socioBL.contactos.email);
                socioTO.direccion = new TODireccion(socioBL.direccion.provincia, socioBL.direccion.canton, socioBL.direccion.distrito, socioBL.direccion.otras_sennas, socioBL.direccion.cod_direccion);
                socioTO.estado_socio = socioBL.estado_socio;
                manejadorDAO.guardarModificarSocio(socioTO);
                return true;
            }catch (Exception)
            {
                throw;
            }
        }

        
        public List<BLSocioNegocio> listaSoc(String busqueda)
        {
            try
            {
                DAOManejadorSocios dao = new DAOManejadorSocios();
                List<TOSocioNegocio> lista = dao.lista_Socios(busqueda);
                List<BLSocioNegocio> listaBL = new List<BLSocioNegocio>();
                foreach (TOSocioNegocio socio in lista)
                {
                    listaBL.Add(convert(socio));
                }
                return listaBL;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para buscar el código de la dirección asociada a un socio en caso de que sea necesario
        /// actualizar la información del mismo
        /// </summary>
        /// <param name="cedula">Cédula del socio del cual se desea conocer el código de dirección</param>
        /// <returns>código de dirección asociado</returns>
        public int buscarDir(String cedula) {
            try
            {
                return manejadorDAO.buscarCodDireccion(cedula);
            }catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para cargar los socios del negocio, según criterios que el usuario haya especificado,
        /// en una tabla, a fin de que este pueda ver los detalles de los socios que cumplen con dichos criterios.
        /// </summary>
        /// <param name="busqueda">criterio bajo el cual se filtran los socios</param>
        /// <returns>Una tabla con socios</returns>
        public DataTable buscarDatos(string busqueda)
        {
            try
            {
                return new DAOManejadorSocios().buscarTabla(busqueda);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable buscarIzquierdaSocios(string busqueda, string id)
        {
            try
            {
                return new DAOManejadorSocios().buscarTablaIzquierda(busqueda, id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable buscarDerechaSocios(string busqueda)
        {
            try
            {
                return new DAOManejadorSocios().buscarTablaDerecha(busqueda);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para crear un nuevo objeto BLSocio a partir de un objeto de transferencia de socio.
        /// </summary>
        /// <param name="socio">Objeto de transferencia de socio</param>
        /// <returns>Objeto tipo BLSocio</returns>
        public BLSocioNegocio convert(TOSocioNegocio socio)
        {
            try
            {
                return new BLSocioNegocio(socio.cedula, socio.nombre, socio.rol, socio.apellido1, socio.apellido2,
                convert(socio.direccion), convert(socio.contactos), socio.estado_socio);
        }
            catch (Exception ex){
                throw;
            }
}

        /// <summary>
        /// Método para crear un nuevo objeto BLDireccion a partir de un objeto de transferencia de dirección.
        /// </summary>
        /// <param name="dir">Objeto de trtansferencia de dirección</param>
        /// <returns>Objeto tipo BLDireccion</returns>
        private BLDireccion convert(TODireccion dir)
        {
            try
            {
                return new BLDireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
            }catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para crear un nuevo objeto BLContacto a partir de un objeto de transferencia de contacto.
        /// </summary>
        /// <param name="cont">Objeto de trtansferencia de contacto</param>
        /// <returns>Objeto tipo BLContacto</returns>
        private BLContactos convert(TOContactos cont)
        {
            try
            {
                return new BLContactos(cont.telefono_hab, cont.telefono_pers, cont.email);
            }catch (Exception ex)
            {
                throw;
            }
        }


        //public DataTable buscarFiltrado(string busqueda)
        //{
        //    try
        //    {
        //        return new DAOManejadorSocios().buscarSociosFiltro(busqueda);
        //    }catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

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

        public void asociarSocio(string asociado, string socio) {
            manejadorDAO.asociarSocio(asociado, socio);
        }

        public void desasociarSocio(string asociado, string socio)
        {
            manejadorDAO.desasociarSocio(asociado, socio);
        }


        /// <summary>
        /// Método que busca a un socio en específico según su cédula.
        /// </summary>
        /// <param name="cedula">cédula del socio</param>
        /// <returns>Un socio en específico</returns>
        public BLSocioNegocio buscarCedula(string cedula)
        {
            try
            {
                return convert((new DAOManejadorSocios().buscarSocioCedula(cedula)));
        }
            catch (Exception ex){
                throw;
            }
}

        /// <summary>
        /// Método para retornar los últimos socios de negocio, proveedores o clientes, agregados a la base de datos
        /// </summary>
        /// <param name="rolSocio">Tipo de rol del socio: Proveedor o Cliente</param>
        /// <returns>Retorna la lista con el top 3 de los últimos socios de negocio agregados</returns>
        public List<BLSocioNegocio> top3_UltimosSocios(String rolSocio)
        {
            try
            {
                DAOManejadorSocios dao = new DAOManejadorSocios();
                List<BLSocioNegocio> listaBl = new List<BLSocioNegocio>();
                List<TOSocioNegocio> listaTo = dao.top3_UltimosSocios(rolSocio);
                foreach(TOSocioNegocio to in listaTo) 
                {
                    listaBl.Add(new BLSocioNegocio(to.cedula, to.nombre, to.rol, to.apellido1, to.apellido2, to.estado_socio));
                }
                return listaBl;
            } catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Buscar los contactos de un socio
        /// </summary>
        /// <param name="cedula">Cédula del socio</param>
        /// <returns>Retorna los contactos del socio</returns>
        public BLContactos buscarContactos(String cedula)
        {
            try
            {
                DAOManejadorSocios dao = new DAOManejadorSocios();
                TOContactos contac = dao.buscarContacto(cedula);
                return new BLContactos(contac.telefono_hab, contac.telefono_pers, contac.email);
            } catch(Exception)
            {
                throw;
            }
        }

    }
}
