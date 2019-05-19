using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TODetalleVenta
    {

        public int claveLinea { get; set; }
        public int codVenta { get; set; }
        public int codMaterial { get; set; }
        public double montoLinea { get; set; }
        public double cantidadLinea { get; set; }


        public TODetalleVenta() { }

        public TODetalleVenta(int claveLinea, int codVenta, int codMaterial, double montoLinea, double cantidadLinea)
        {
            this.claveLinea = claveLinea;
            this.codVenta = codVenta;
            this.codMaterial = codMaterial;
            this.montoLinea = montoLinea;
            this.cantidadLinea = cantidadLinea;
        }

        public TODetalleVenta(int codVenta, int codMaterial, double montoLinea, double cantidadLinea)
        {
            this.codVenta = codVenta;
            this.codMaterial = codMaterial;
            this.montoLinea = montoLinea;
            this.cantidadLinea = cantidadLinea;
        }

    }
}
