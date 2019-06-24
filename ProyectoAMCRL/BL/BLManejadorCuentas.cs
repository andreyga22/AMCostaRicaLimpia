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
        public void guardarCuenta(BLCuenta cuenta) {
            try {
                new DAOCuentas().guardarCuenta(convert(cuenta));
            } catch(Exception) {
                throw;
            }
        }

        public void modificarCuenta(BLCuenta cuenta) {
            try {
                new DAOCuentas().modificarCuenta(convert(cuenta));
            } catch(Exception) {
                throw;
            }
        }

        public void modificarContrasena(string id, string vieja, String nueva) {
            try {
                new DAOCuentas().modificarContrasena(id, vieja, nueva);
            } catch(Exception) {
                throw;
            }
        }

        public void restaurarContra(string id, String nueva) {
            try {
                new DAOCuentas().restaurarContra(id, nueva);
            } catch(Exception) {
                throw;
            }
        }

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

        public DataTable buscar(string busqueda) {
            try {
                return new DAOCuentas().buscar(busqueda);
            } catch(Exception) {
                throw;
            }
        }

        public BLCuenta consultarCuenta(String id) {
            try {
                return convert(new DAOCuentas().consultarCuenta(id));
            } catch(Exception) {
                throw;
            }
        }

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

        private BLCuenta convert(TOCuenta cuenta) {
            return new BLCuenta(cuenta.id_usuario, cuenta.clave, cuenta.nombre_usuario, cuenta.rol, cuenta.estado);
        }

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
