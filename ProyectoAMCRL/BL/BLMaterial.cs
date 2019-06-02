using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLMaterial
    {
        public int codigoM { get; set; }
        public int nombreMaterial { get; set; }
        public int precioKilo { get; set; }

        public BLMaterial(int codigoM, int nombreMaterial, int precioKilo)
        {
            this.codigoM = codigoM;
            this.nombreMaterial = nombreMaterial;
            this.precioKilo = precioKilo;
        }

        public BLMaterial()
        {
        }
    }
}
