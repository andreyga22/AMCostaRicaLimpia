using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL {
    public class BLCuenta {
        public string id_usuario;
        public string clave;
        public string nombre_usuario;
        public Boolean estado;
        public string rol;

        public BLCuenta() { }

        public BLCuenta(string id_usuario, string clave, string nombre_usuario, Boolean estado, string rol) {
            this.id_usuario = id_usuario;
            this.clave = clave;
            this.nombre_usuario = nombre_usuario;
            this.estado = estado;
            this.rol = rol;
        }
    }
}
