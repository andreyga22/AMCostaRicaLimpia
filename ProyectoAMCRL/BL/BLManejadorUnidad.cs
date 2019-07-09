using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;

namespace BL {
    public class BLManejadorUnidad {
        public DataTable buscar(String palabra) {
            try {
                return new DAOUnidadMedida().buscar(palabra);
            } catch(Exception) {
                throw;
            }
        }

        public void guardarActualizarRegular(BLUnidad unidad) {
            new DAOUnidadMedida().guardarActualizarRegular(convert(unidad));
        }

        public void guardarActualizarAdmin(BLUnidad unidad) {
            new DAOUnidadMedida().guardarActualizarAdmin(convert(unidad));
        }

        public BLUnidad consultar(String id) {
            return convert(new DAOUnidadMedida().consultar(id));
        }


        private TOUnidad convert(BLUnidad um) {
            return new TOUnidad(um.codigo, um.nombre, um.equivalencia, um.estado);
        }

        private BLUnidad convert(TOUnidad um) {
            return new BLUnidad(um.codigo, um.nombre, um.equivalencia, um.estado);
        }





    }
}
