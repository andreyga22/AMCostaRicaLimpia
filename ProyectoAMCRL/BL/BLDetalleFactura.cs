using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;


namespace BL
{
    public class BLDetalleFactura
    {
        public int cod_Linea { get; set; }
        public int cod_Factura { get; set; }
        public string nombreMaterial { get; set; }
        public double monto_Linea { get; set; }
        public double kilos_Linea { get; set; }
        public String cod_Stock { get; set; }

        public BLDetalleFactura()
        {

        }

        public BLDetalleFactura(int cod_Linea, int cod_Factura, double monto_Linea, double kilos_Linea, String cod_Stock)
        {
            this.cod_Linea = cod_Linea;
            this.cod_Factura = cod_Factura;
            this.monto_Linea = monto_Linea;
            this.kilos_Linea = kilos_Linea;
            this.cod_Stock = cod_Stock;
        }

        public BLDetalleFactura(int cod_Linea, int cod_Factura, string nombreMaterial, double monto_Linea, double kilos_Linea, String cod_Stock)
        {
            this.cod_Linea = cod_Linea;
            this.cod_Factura = cod_Factura;
            this.nombreMaterial = nombreMaterial;
            this.monto_Linea = monto_Linea;
            this.kilos_Linea = kilos_Linea;
            this.cod_Stock = cod_Stock;
        }
    }
}
