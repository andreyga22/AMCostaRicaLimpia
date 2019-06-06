using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO {
    public class TOBodega {
        public String codigo { get; set; }
        public String nombre { get; set; }
        public Boolean estado { get; set; }
        public TODireccion direccion { get; set; }

        public TOBodega() {
        }

        public TOBodega(String codigo, String nombre, Boolean estado, TODireccion direccion) {
            this.codigo = codigo;
            this.nombre = nombre;
            this.estado = estado;
            this.direccion = direccion;
        }
    }
}
