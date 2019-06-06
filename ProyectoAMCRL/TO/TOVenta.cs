using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOVenta
    {
        public int cod_Venta { get; set; }
        public int id_Bodega { get; set; }
        public String id_Moneda { get; set; }
        public String cedula { get; set; }
        public double monto_Total { get; set; }
        public DateTime fecha { get; set; }
        public String nombreCompleto { get; set; }

        public TOVenta() { }

        public TOVenta(int cod_Venta, int id_Bodega, String id_Moneda, String cedula, double monto_Total, DateTime fecha)
        {
            this.cod_Venta = cod_Venta;
            this.id_Bodega = id_Bodega;
            this.id_Moneda = id_Moneda;
            this.cedula = cedula;
            this.monto_Total = monto_Total;
            this.fecha = fecha;
        }

        public TOVenta(int cod_Venta, int id_Bodega, String id_Moneda, String cedula, double monto_Total, 
            DateTime fecha, String nombreCompleto)
        {
            this.cod_Venta = cod_Venta;
            this.id_Bodega = id_Bodega;
            this.id_Moneda = id_Moneda;
            this.cedula = cedula;
            this.monto_Total = monto_Total;
            this.fecha = fecha;
            this.nombreCompleto = nombreCompleto;
        }
    }
}
