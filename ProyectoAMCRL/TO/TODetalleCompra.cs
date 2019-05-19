using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TODetalleCompra
    {
        public int claveLinea { get; set; }
        public int codCompra { get; set; }
        public int codMaterial { get; set; }
        public double montoLinea { get; set; }
        public double cantidadLinea { get; set; }


        public TODetalleCompra(){ }

        public TODetalleCompra(int claveLinea,int codCompra, int codMaterial, double montoLinea, double cantidadLinea) {
            this.claveLinea = claveLinea;
            this.codCompra = codCompra;
            this.codMaterial = codMaterial;
            this.montoLinea = montoLinea;
            this.cantidadLinea = cantidadLinea;
        }

        public TODetalleCompra(int codCompra, int codMaterial, double montoLinea, double cantidadLinea) {
            this.codCompra = codCompra;
            this.codMaterial = codMaterial;
            this.montoLinea = montoLinea;
            this.cantidadLinea = cantidadLinea;
        }

    }
}
