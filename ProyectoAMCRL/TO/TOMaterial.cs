using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOMaterial
    {
        private string codigo;
        private string nom;
        private double precio;

        public String codigoM { get; set; }
        public String nombreMaterial { get; set; }
        public double precioKilo { get; set; }
        public TOUnidad unidadBase { get; set; }

        public TOMaterial(String codigoM, String nombreMaterial, double precioKilo, TOUnidad unidadBase)
        {

            this.codigoM = codigoM;
            this.nombreMaterial = nombreMaterial;
            this.precioKilo = precioKilo;
            this.unidadBase = unidadBase;
        }

        public TOMaterial()
        {
        }

        public TOMaterial(string codigo, string nom, double precio)
        {
            this.codigo = codigo;
            this.nom = nom;
            this.precio = precio;
        }
    }
}
