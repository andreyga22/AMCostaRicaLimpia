using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLFilaAjuste
    {

        String idMaterial { get; set; }
        String peso { get; set; }
        String idUnidad { get; set; }

        public BLFilaAjuste(string idMaterial, string peso, string idUnidad)
        {
            this.idMaterial = idMaterial;
            this.peso = peso;
            this.idUnidad = idUnidad;
        }

        public BLFilaAjuste()
        {
        }
    }
}
