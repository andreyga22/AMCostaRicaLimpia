using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLMoneda
    {
        public String idMoneda { get; set; }
        public String detalleMoneda { get; set; }
        public double equivalencia_Colon { get; set; }
        public Boolean estado { get; set; }

        public BLMoneda()
        {

        }

        public BLMoneda(String idMoneda, String detalleMoneda, double equivalencia_Colon)
        {
            this.idMoneda = idMoneda;
            this.detalleMoneda = detalleMoneda;
            this.equivalencia_Colon = equivalencia_Colon;
        }

        public BLMoneda(String idMoneda, String detalleMoneda, double equivalencia_Colon, Boolean estado) {
            this.idMoneda = idMoneda;
            this.detalleMoneda = detalleMoneda;
            this.equivalencia_Colon = equivalencia_Colon;
            this.estado = estado;
        }
    }
}
