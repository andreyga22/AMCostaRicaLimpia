using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;

namespace BL
{
    public class BLCompra
    {
        public int cod_Compra { get; set; }
        public int id_Bodega { get; set; }
        public String id_Moneda { get; set; }
        public String cedula { get; set; }
        public double monto_Total { get; set; }
        public DateTime fecha {get;set;}

        public BLCompra() { }

        public BLCompra(int cod_Compra, int id_Bodega, String id_Moneda, String cedula, double monto_Total, DateTime fecha)
        {
            this.cod_Compra = cod_Compra;
            this.id_Bodega = id_Bodega;
            this.id_Moneda = id_Moneda;
            this.cedula = cedula;
            this.monto_Total = monto_Total;
            this.fecha = fecha;
        }
    }
}
