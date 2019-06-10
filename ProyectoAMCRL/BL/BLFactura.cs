using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;

namespace BL
{
    public class BLFactura
    {
        public int cod_Factura { get; set; }
        public String id_Bodega { get; set; }
        public String id_Moneda { get; set; }
        public String cedula { get; set; }
        public double monto_Total { get; set; }
        public DateTime fecha { get; set; }
        public String tipo { get; set; }
        public String nombreCompleto { get; set; }

        public BLFactura() { }

        public BLFactura(int cod_Factura, String cedula, String id_Bodega, String id_Moneda,  double monto_Total, DateTime fecha, String tipo)
        {
            this.cod_Factura = cod_Factura;
            this.id_Bodega = id_Bodega;
            this.id_Moneda = id_Moneda;
            this.cedula = cedula;
            this.monto_Total = monto_Total;
            this.fecha = fecha;
            this.tipo = tipo;
        }

        public BLFactura(int cod_Factura, String cedula, String id_Bodega, String id_Moneda,  double monto_Total, String tipo,
            DateTime fecha, String nombreCompleto)
        {
            this.cod_Factura = cod_Factura;
            this.id_Bodega = id_Bodega;
            this.id_Moneda = id_Moneda;
            this.cedula = cedula;
            this.monto_Total = monto_Total;
            this.fecha = fecha;
            this.nombreCompleto = nombreCompleto;
            this.tipo = tipo;
        }
    }
}
