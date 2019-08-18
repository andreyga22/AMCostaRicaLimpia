using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOFactura {
        public int cod_Factura { get; set; }
        public String id_Bodega { get; set; }
        public String id_Moneda { get; set; }
        public String cajero { get; set; }
        public String cedula { get; set; }
        public double subtotal { get; set; }
        public double impuesto { get; set; }
        public double descuento { get; set; }
        public double total { get; set; }
        public DateTime fecha { get; set; }
        public String tipo { get; set; }
        public String nombreCompleto { get; set; }

        public TOFactura() { }

        public TOFactura(int cod_Factura, String cedula, String id_Bodega, String id_Moneda, double monto_Total, DateTime fecha, String tipo) {
            this.cod_Factura = cod_Factura;
            this.id_Bodega = id_Bodega;
            this.id_Moneda = id_Moneda;
            this.cedula = cedula;
            this.total = monto_Total;
            this.fecha = fecha;
            this.tipo = tipo;
        }

        public TOFactura(int cod_Factura, String cedula, String id_Bodega, String id_Moneda, double monto_Total, String tipo,
            DateTime fecha, String nombreCompleto) {
            this.cod_Factura = cod_Factura;
            this.id_Bodega = id_Bodega;
            this.id_Moneda = id_Moneda;
            this.cedula = cedula;
            this.total = monto_Total;
            this.fecha = fecha;
            this.nombreCompleto = nombreCompleto;
            this.tipo = tipo;
        }


        public TOFactura(int cod_factura, String id_bodega, String id_moneda, String cajero, String cedula, double subtotal, double impuesto, double descuento, double total, DateTime fecha, String tipo) {
            this.cod_Factura = cod_factura;
            this.id_Bodega = id_bodega;
            this.id_Moneda = id_moneda;
            this.cajero = cajero;
            this.cedula = cedula;
            this.subtotal = subtotal;
            this.impuesto = impuesto;
            this.descuento = descuento;
            this.total = total;
            this.fecha = fecha;
            this.tipo = tipo;
        }
    }
}
