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

        public void guardarModificarBodega(BLBodega bod) {
            try {
                new DAOBodegas().guardarModificarBodega(convert(bod));
            } catch(Exception) {
                throw;
            }
        }

        public List<BLBodegaTabla> listaBodegas() {
            try {
                List<TOBodegaTabla> to = new DAOBodegas().listaBodega();
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
                return new DAOBodegas().buscar(busqueda);
            } catch(Exception) {
                throw;
            }
        }

        public BLBodega consultarBodega(String id) {
            try {
                return convert(new DAOBodegas().consultarBodega(id));
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
