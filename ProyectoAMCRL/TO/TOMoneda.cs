using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOMoneda
    {
        public String idMoneda { get; set; }
        public String detalleMoneda { get; set; }
        public double equivalencia_Colon { get; set; }


        public TOMoneda()
        {

        }

        public TOMoneda(String idMoneda, String detalleMoneda, double equivalencia_Colon)
        {
            this.idMoneda = idMoneda;
            this.detalleMoneda = detalleMoneda;
            this.equivalencia_Colon = equivalencia_Colon;
        }
    }
}
