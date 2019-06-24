using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TODetalleFactura
    {
        public int cod_Linea { get; set; }
        public int cod_Factura { get; set; }
        public String cod_Material { get; set; }
        public string nombreMaterial { get; set; }
        public double monto_Linea { get; set; }
        public double kilos_Linea { get; set; }


        public TODetalleFactura()
        {

        }

        public TODetalleFactura(int cod_Linea, int cod_Factura, String cod_Material, double monto_Linea, double kilos_Linea)
        {
            this.cod_Linea = cod_Linea;
            this.cod_Factura = cod_Factura;
            this.cod_Material = cod_Material;
            this.monto_Linea = monto_Linea;
            this.kilos_Linea = kilos_Linea;
        }

        public TODetalleFactura(int cod_Linea, int cod_Factura, String cod_Material, string nombreMaterial, double monto_Linea, double kilos_Linea)
        {
            this.cod_Linea = cod_Linea;
            this.cod_Factura = cod_Factura;
            this.cod_Material = cod_Material;
            this.nombreMaterial = nombreMaterial;
            this.monto_Linea = monto_Linea;
            this.kilos_Linea = kilos_Linea;
        }
    }
}
