using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOMaterial
    {
        public int codigoM { get; set; }
        public String nombreMaterial { get; set; }
        public double precioKilo { get; set; }

        public TOMaterial(int codigoM, String nombreMaterial, double precioKilo)
        {
            this.codigoM = codigoM;
            this.nombreMaterial = nombreMaterial;
            this.precioKilo = precioKilo;
        }

        public TOMaterial()
        {
        }
    }
}
