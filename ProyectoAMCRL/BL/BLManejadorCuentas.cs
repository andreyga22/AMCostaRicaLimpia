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
            //try {
            new DAOCuentas().guardarCuenta(convert(cuenta));
            //} catch(Exception) {
            //    throw;
            //}
        }

        public void modificarCuenta(BLCuenta cuenta ) {
            new DAOCuentas().modificarCuenta(convert(cuenta));
        }

        public void modificarContrasena(string id, string vieja, String nueva) {
            new DAOCuentas().modificarContrasena(id, vieja, nueva);
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
            return new DAOCuentas().buscar(busqueda);
        }

        public BLCuenta consultarCuenta(String id) {
            return convert(new DAOCuentas().consultarCuenta(id));
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
