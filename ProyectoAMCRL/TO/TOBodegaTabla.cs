using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO {
    public class TOBodegaTabla {
        public String codigo { get; set; }
        public String nombre { get; set; }
        public String distrito { get; set; }
        public Boolean estado { get; set; }

        public TOBodegaTabla() {
        }

        public TOBodegaTabla(String codigo, String nombre, Boolean estado, String distrito) {
            this.codigo = codigo;
            this.nombre = nombre;
            this.distrito = distrito;
            this.estado = estado;
        }
    }
}
