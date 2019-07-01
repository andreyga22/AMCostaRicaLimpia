using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;
using System.Data;

namespace BL {
    public class BLManejadorBodega {
        /// <summary>
        /// Se encarga de enviar el objeto bodega recivido a la capa DAO para su almacenamiento. 
        /// Solo funciona para un suario administrador
        /// </summary>
        /// <param name="bod">Objeto bodega que se desea guardar o actualizar en BD</param>
        public void guardarModificarBodegaAdmin(BLBodega bod) {
            try {
                new DAOBodegas().guardarModificarBodegaAdmin(convert(bod));
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Se encarga de enviar el objeto bodega recivido a la capa DAO para su almacenamiento. 
        /// Solo funciona para un usuario regular
        /// </summary>
        /// <param name="bod">Objeto bodega que se desea guardar o actualizar en BD</param>
        public void guardarModificarBodegaRegular(BLBodega bod) {
            try {
                new DAOBodegas().guardarModificarBodegaRegular(convert(bod));
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Devuelve una lista de todas las bodegas en bases de datos
        /// </summary>
        /// <returns>Una lista de BLBodegaTabla</returns>
        public List<BLBodegaTabla> listaBodegas() {
            try {
                List<TOBodegaTabla> to = new DAOBodegas().listaBodegaUsuarioRegular();
                List<BLBodegaTabla> listaBL = new List<BLBodegaTabla>();
                foreach(TOBodegaTabla bodega in to) {
                    listaBL.Add(convert(bodega));
                }
                return listaBL;
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite la funcionalidad de busqueda en la pagina de bodegas
        /// (Solo funciona para el usuario regular)
        /// </summary>
        /// <param name="busqueda">Parametro de busqueda</param>
        /// <returns>Datatable con el restultado de la busqueda</returns>
        public DataTable buscar(string busqueda) {
            try {
                return new DAOBodegas().buscarUsuarioRegular(busqueda);
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite la funcionalidad de busqueda en la pagina de bodegas
        /// Solo funciona para el usuario administrador
        /// </summary>
        /// <param name="busqueda">Parametro de busqueda</param>
        /// <returns>Datatable con el restultado de la busqueda</returns>
        public DataTable buscarAdmin(string busqueda) {
            try {
                return new DAOBodegas().buscarUsuarioAdmin(busqueda);
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite la consulta de los datos de una bodega mediante el identificador de bodega.
        /// Trae todos los datos relevantes para un administrador.
        /// </summary>
        /// <param name="id">Identificador de la bodega que se desea buscar</param>
        /// <returns>BLBodega con el resultado de la busqueda</returns>
        public BLBodega consultarBodegaAdmin(String id) {
            try {
                return convert(new DAOBodegas().consultarBodegaAdmin(id));
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite la consulta de los datos de una bodega mediante el identificador de bodega.
        /// Trae todos los datos relevantes para un usuario regular.
        /// </summary>
        /// <param name="id">Identificador de la bodega que se desea buscar</param>
        /// <returns>BLBodega con el resultado de la busqueda</returns>
        public BLBodega consultarBodegaRegular(String id) {
            try {
                return convert(new DAOBodegas().consultarBodegaRegular(id));
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite la conversion de un objeto BLBodegaTabla a TOBodegaTabala
        /// </summary>
        /// <param name="bod">Objeto que se desea convertir</param>
        /// <returns>Objeto convertido</returns>
        private BLBodegaTabla convert(TOBodegaTabla bod) {
            return new BLBodegaTabla(bod.codigo, bod.nombre, bod.estado, bod.distrito);
        }

        /// <summary>
        /// Permite la conversion de un objeto TOBodegaTabla a BLBodegaTabala
        /// </summary>
        /// <param name="bod">Objeto que se desea convertir</param>
        /// <returns>Objeto convertido</returns>
        private TOBodegaTabla convert(BLBodegaTabla bod) {
            return new TOBodegaTabla(bod.codigo, bod.nombre, bod.estado, bod.distrito);
        }

        /// <summary>
        /// Permite la conversion de un objeto BLBodega  a TOBodega
        /// </summary>
        /// <param name="bod">Objeto que se desea convertir</param>
        /// <returns>Objeto convertido</returns>
        private BLBodega convert(TOBodega bod) {
            return new BLBodega(bod.codigo, bod.nombre, bod.estado, convert(bod.direccion));
        }

        /// <summary>
        /// Permite la conversion de un objeto TOBodega a BLBodega
        /// </summary>
        /// <param name="bod">Objeto que se desea convertir</param>
        /// <returns>Objeto convertido</returns>
        private TOBodega convert(BLBodega bod) {
            return new TOBodega(bod.codigo, bod.nombre, bod.estado, convert(bod.direccion));
        }

        /// <summary>
        /// Permite la conversion de un objeto BLDireccion a TODireccion
        /// </summary>
        /// <param name="dir">Objeto que se desea convertir</param>
        /// <returns>Objeto convertido</returns>
        private BLDireccion convert(TODireccion dir) {
            return new BLDireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
        }

        /// <summary>
        /// Permite la conversion de un objeto TODireccion a BLDireccion
        /// </summary>
        /// <param name="dir">objeto que se desea convertir</param>
        /// <returns>Objeto convertido</returns>
        private TODireccion convert(BLDireccion dir) {
            return new TODireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
        }

    }
}
