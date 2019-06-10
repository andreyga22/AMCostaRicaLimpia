using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO {
    public class TOCuenta {
        public string id_usuario;
        public string clave;
        public string nombre_usuario;
        public string rol;
        public Boolean estado;

        public TOCuenta() { }

        public TOCuenta(string id_usuario, string clave, string nombre_usuario, string rol, Boolean estado) {
            this.id_usuario = id_usuario;
            this.clave = clave;
            this.nombre_usuario = nombre_usuario;
            this.rol = rol;
            this.estado = estado;
        }
    }
}
