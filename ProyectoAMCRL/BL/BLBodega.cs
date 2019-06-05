using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL {
    public class BLBodega {
        public String codigo { get; set; }
        public String nombre { get; set; }
        public Boolean estado { get; set; }
        public BLDireccion direccion { get; set; }

        public BLBodega() {
        }

        public BLBodega(String codigo, String nombre, Boolean estado, BLDireccion direccion) {
            this.codigo = codigo;
            this.nombre = nombre;
            this.estado = estado;
            this.direccion = direccion;
        }
    }
}
