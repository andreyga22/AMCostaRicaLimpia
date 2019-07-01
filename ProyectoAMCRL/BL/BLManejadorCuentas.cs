using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;
using System.Data;


namespace BL {
    public class BLManejadorCuentas {
        /// <summary>
        /// Permite almacenar una cuenta por primera vez
        /// </summary>
        /// <param name="cuenta">Cuenta que se desea guardar en base de datos</param>
        public void guardarCuenta(BLCuenta cuenta) {
            try {
                new DAOCuentas().guardarCuenta(convert(cuenta));
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite actualizar los datos de una cuenta.
        /// </summary>
        /// <param name="cuenta">Cuenta que se desea ser modificada</param>
        public void modificarCuenta(BLCuenta cuenta) {
            try {
                new DAOCuentas().modificarCuenta(convert(cuenta));
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite el cambio de la contraseña de una cuenta.
        /// </summary>
        /// <param name="id">Identificador de usuario</param>
        /// <param name="vieja">Contraseña anterior</param>
        /// <param name="nueva">Nueva contraseña</param>
        public void modificarContrasena(string id, string vieja, String nueva) {
            try {
                new DAOCuentas().modificarContrasena(id, vieja, nueva);
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite la restauración de una contraseña en caso de olvido.
        /// </summary>
        /// <param name="id">Identificador de usuario</param>
        /// <param name="nueva">Nueva contraseña</param>
        public void restaurarContra(string id, String nueva) {
            try {
                new DAOCuentas().restaurarContra(id, nueva);
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Consulta si la cuenta existe en el sistema
        /// </summary>
        /// <param name="id">Identificador de usuario</param>
        /// <param name="vieja">Contraseña de usuario</param>
        /// <returns>Objeto con el resultado de la busqueda</returns>
        public Boolean consultarContra(string id, string vieja) {
            try {
               return  new DAOCuentas().consultarContra(id, vieja);
            } catch(Exception) {
                throw;
            }
        }

        //public List<BLBodegaTabla> listaCuentas() {
        //    List<TOBodegaTabla> to = new DAOBodegas().listaBodega();
        //    List<BLBodegaTabla> listaBL = new List<BLBodegaTabla>();
        //    foreach(TOBodegaTabla bodega in to) {
        //        listaBL.Add(convert(bodega));
        //    }
        //    return listaBL;
        //}


        /// <summary>
        /// Permite la busqueda de cuentas por medio de una palabra clave.
        /// </summary>
        /// <param name="busqueda">Parametro de busqueda</param>
        /// <returns>Datatable con el resultado de la busqueda</returns>
        public DataTable buscar(string busqueda) {
            try {
                return new DAOCuentas().buscar(busqueda);
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Consulta una cuenta utilizando el identificador de la cuenta como parametro de busqueda
        /// </summary>
        /// <param name="id">Identificador de usuario</param>
        /// <returns>Objeto BLCuenta con el resultado de la busqueda</returns>
        public BLCuenta consultarCuenta(String id) {
            try {
                return convert(new DAOCuentas().consultarCuenta(id));
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Permite el login en el sistema por medio del identificador y la contraseña de una cuenta
        /// </summary>
        /// <param name="id">Identificador de usuario</param>
        /// <param name="contra">Contraseña de usuario</param>
        /// <returns>BLCuenta con los datos del usuario de login</returns>
        public BLCuenta login(String id, string contra) {
            try {
                DAOCuentas dao = new DAOCuentas();
                TOCuenta cuenta = dao.login(id, contra);
                if(cuenta != null) {
                    return convert(cuenta);
                } else {
                    return null;
                }
            } catch(Exception) {
                throw;
            }
        }

        //private BLBodegaTabla convert(TOBodegaTabla bod) {
        //    return new BLBodegaTabla(bod.codigo, bod.nombre, bod.estado, bod.distrito);
        //}

        //private TOBodegaTabla convert(BLBodegaTabla bod) {
        //    return new TOBodegaTabla(bod.codigo, bod.nombre, bod.estado, bod.distrito);
        //}

        /// <summary>
        /// Permite la conversion de un objeto BLCuenta a TOCuenta
        /// </summary>
        /// <param name="cuenta">Objeto a convertir</param>
        /// <returns>Objeto Convertido</returns>
        private BLCuenta convert(TOCuenta cuenta) {
            return new BLCuenta(cuenta.id_usuario, cuenta.clave, cuenta.nombre_usuario, cuenta.rol, cuenta.estado);
        }

        /// <summary>
        /// Permite la conversion de un objeto TOcuenta a BLCuenta
        /// </summary>
        /// <param name="cuenta">Ojeto a convertir</param>
        /// <returns>Objeto convertido</returns>
        private TOCuenta convert(BLCuenta cuenta) {
            return new TOCuenta(cuenta.id_usuario, cuenta.clave, cuenta.nombre_usuario, cuenta.rol, cuenta.estado);
        }

        //private BLDireccion convert(TODireccion dir) {
        //    return new BLDireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
        //}

        //private TODireccion convert(BLDireccion dir) {
        //    return new TODireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
        //}
    }
}
