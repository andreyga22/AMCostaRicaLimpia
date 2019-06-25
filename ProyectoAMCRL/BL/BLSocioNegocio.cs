using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLSocioNegocio
    {
        public String cedula { get; set; }
        public String nombre { get; set; }
        public String rol { get; set; }
        public String apellido1 { get; set; }
        public String apellido2 { get; set; }
        public BLDireccion direccion { get; set; }
        public Boolean estado_socio { get; set; }
        public BLContactos contactos { get; set; }

        public BLSocioNegocio() {
        }

        /*Para agregar un socio*/
        public BLSocioNegocio(String cedula, String nombre, String rol, String apellido1, String apellido2, 
            BLDireccion direccion, BLContactos contactos, Boolean estado)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.rol = rol;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.direccion = direccion;
            this.contactos = contactos;
            this.estado_socio = estado;
        }

        public BLSocioNegocio(String cedula, String nombre, String rol, String apellido1, String apellido2, Boolean estado)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.rol = rol;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.estado_socio = estado;
        }
    }
}

