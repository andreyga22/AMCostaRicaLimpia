using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TO
{
    public class TOSocioNegocio
    {
        public String cedula { get; set; }
        public String cedula_asociado { get; set; }
        public String nombre { get; set; }
        public String rol { get; set; }
        public String apellido1 { get; set; }
        public String apellido2 { get; set; }
        public TODireccion direccion { get; set; }
        public TOContactos contactos { get; set; }
        public String estado_socio { get; set; }

        public TOSocioNegocio() { }
        /*para el caso de que se agregue y se asocie inmediatamente*/
        public TOSocioNegocio(String cedula, String cedula_asociado, String nombre, String rol, String apellido1, String apellido2,
            int tel1, int tel2, String correo, TODireccion direccion, TOContactos contactos, String estado){ 

            this.cedula = cedula;
            this.cedula_asociado = cedula_asociado;
            this.nombre = nombre;
            this.rol = rol;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.direccion = direccion;
            this.estado_socio = estado;
            this.contactos = contactos;
        }

        /*Para agregar un socio*/
        public TOSocioNegocio(String cedula, String nombre, String rol, String apellido1, String apellido2, TODireccion direccion,
            TOContactos contactos, String estado)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.rol = rol;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.direccion = direccion;
        }

    }
}
