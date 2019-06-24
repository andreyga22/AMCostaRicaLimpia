using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLDetalleAjuste
    {

        public String id_Material { get; set; }
        public int id_Stock { get; set; }
        public double kilos_Linea { get; set; }
        public String unidadMedida { get; set; }

        public BLDetalleAjuste(string id_Material, int id_Stock, double kilos_Linea)
        {
            this.id_Material = id_Material;
            this.id_Stock = id_Stock;
            this.kilos_Linea = kilos_Linea;
        }

        public BLDetalleAjuste()
        {
        }

        public BLDetalleAjuste(string id_Material, int id_Stock, double kilos_Linea, string unidadMedida) : this(id_Material, id_Stock, kilos_Linea)
        {
            this.unidadMedida = unidadMedida;
        }
    }
}
