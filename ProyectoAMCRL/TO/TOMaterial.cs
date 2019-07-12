using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOMaterial
    {
        public String codigoM { get; set; }
        public String nombreMaterial { get; set; }
        public double precioVentaK { get; set; }
        public String cod_Unidad { get; set; }
        public double precioCompraK { get; set; }
        public Boolean estado_Material { get; set; }

        public TOMaterial(String codigoM, String nombreMaterial, double precioVentaK, String cod_Unidad, double precioCompraK, Boolean estado_Material)
        {
            this.codigoM = codigoM;
            this.nombreMaterial = nombreMaterial;
            this.precioVentaK = precioVentaK;
            this.cod_Unidad = cod_Unidad;
            this.precioCompraK = precioCompraK;
            this.estado_Material = estado_Material;
        }

        public TOMaterial()
        {
        }
    }
}
