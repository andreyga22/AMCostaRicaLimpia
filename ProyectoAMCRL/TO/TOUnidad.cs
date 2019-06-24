﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOUnidad
    {

        public String codigo { get; set; }
        public String nombre { get; set; }
        public double equivalencia { get; set; }

        public TOUnidad(string codigo, string nombre, double equivalencia)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.equivalencia = equivalencia;
        }

        public TOUnidad()
        {
        }
    }
}
