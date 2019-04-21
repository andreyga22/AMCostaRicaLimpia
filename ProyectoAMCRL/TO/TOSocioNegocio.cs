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
        public String rol { get; set; }
        public String apellido1 { get; set; }
        public String apellido2 { get; set; }

        public TOSocioNegocio() { }
        /*para el caso de que se agregue y se asocie inmediatamente*/
        public TOSocioNegocio(String cedula, String cedula_asociado, String rol, String apellido1, String apellido2)
        {
            this.cedula = cedula;
            this.cedula_asociado = cedula_asociado;
            this.rol = rol;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
        }

        public TOSocioNegocio(String cedula, String rol, String apellido1, String apellido2)
        {
            this.cedula = cedula;
            this.rol = rol;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
        }

    }
}
