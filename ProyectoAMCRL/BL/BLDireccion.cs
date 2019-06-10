﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLDireccion
    {
        public int cod_direccion { get; set; }
        public String provincia { get; set; }
        public String canton { get; set; }
        public String distrito { get; set; }
        public String otras_sennas { get; set; }

        public BLDireccion() {
        }

        public BLDireccion(String provincia, String canton, String distrito, String otras_sennas, int cod_direccion) {
            this.provincia = provincia;
            this.canton = canton;
            this.distrito = distrito;
            this.otras_sennas = otras_sennas;
            this.cod_direccion = cod_direccion;
        }
    }
}
