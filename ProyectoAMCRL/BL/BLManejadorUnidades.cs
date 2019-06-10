using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLManejadorUnidades
    {

        public List<BLUnidad> unidades { get; set; }


        public BLManejadorUnidades() {
            unidades = new List<BLUnidad>();
            unidades.Add(new BLUnidad("UND", "UNIDAD", 0));
            unidades.Add(new BLUnidad("KG", "KILO", 1));
            unidades.Add(new BLUnidad("TON", "TONELADA", 1000));
            unidades.Add(new BLUnidad("CON", "CONTENEDOR", 2000));
        }

    }
}
