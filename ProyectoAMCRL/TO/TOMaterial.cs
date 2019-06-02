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
        public int nombreMaterial { get; set; }
        public int precioKilo { get; set; }

        public TOMaterial(int codigoM, int nombreMaterial, int precioKilo)
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
