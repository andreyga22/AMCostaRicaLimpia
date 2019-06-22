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

        public void guardarModificarBodegaAdmin(BLBodega bod) {
            try {
                new DAOBodegas().guardarModificarBodegaAdmin(convert(bod));
            } catch(Exception) {
                throw;
            }
        }

        public void guardarModificarBodegaRegular(BLBodega bod) {
            try {
                new DAOBodegas().guardarModificarBodegaRegular(convert(bod));
            } catch(Exception) {
                throw;
            }
        }

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

        public DataTable buscar(string busqueda) {
            try {
                return new DAOBodegas().buscarUsuarioRegular(busqueda);
            } catch(Exception) {
                throw;
            }
        }

        public DataTable buscarAdmin(string busqueda) {
            try {
                return new DAOBodegas().buscarUsuarioAdmin(busqueda);
            } catch(Exception) {
                throw;
            }
        }

        public BLBodega consultarBodegaAdmin(String id) {
            try {
                return convert(new DAOBodegas().consultarBodegaAdmin(id));
            } catch(Exception) {
                throw;
            }
        }

        public BLBodega consultarBodegaRegular(String id) {
            try {
                return convert(new DAOBodegas().consultarBodegaRegular(id));
            } catch(Exception) {
                throw;
            }
        }

        private BLBodegaTabla convert(TOBodegaTabla bod) {
            return new BLBodegaTabla(bod.codigo, bod.nombre, bod.estado, bod.distrito);
        }

        private TOBodegaTabla convert(BLBodegaTabla bod) {
            return new TOBodegaTabla(bod.codigo, bod.nombre, bod.estado, bod.distrito);
        }

        private BLBodega convert(TOBodega bod) {
            return new BLBodega(bod.codigo, bod.nombre, bod.estado, convert(bod.direccion));
        }

        private TOBodega convert(BLBodega bod) {
            return new TOBodega(bod.codigo, bod.nombre, bod.estado, convert(bod.direccion));
        }

        private BLDireccion convert(TODireccion dir) {
            return new BLDireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
        }

        private TODireccion convert(BLDireccion dir) {
            return new TODireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
        }

    }
}
