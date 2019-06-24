using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLMaterial
    {
        public String codigoM { get; set; }
        public String nombreMaterial { get; set; }
        public double precioKilo { get; set; }
        public BLUnidad unidadBase { get; set; }

        public BLMaterial(String codigoM, String nombreMaterial, double precioKilo, BLUnidad unidadBase)
        {
            this.codigoM = codigoM;
            this.nombreMaterial = nombreMaterial;
            this.precioKilo = precioKilo;
            this.unidadBase = unidadBase;
        }

        public BLMaterial()
        {
        }
        public BLMaterial(string codigo, string nom, double precio)
        {
            this.codigoM = codigo;
            this.nombreMaterial = nom;
            this.precioKilo = precio;
        }
    }
}
