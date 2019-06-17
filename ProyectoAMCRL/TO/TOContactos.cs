using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOContactos
    {
        public int telefono_hab;
        public int telefono_pers;
        public String email;

        public TOContactos() {
        }

        public TOContactos(int telefono_hab, int telefono_pers, String email)
        {
            this.telefono_hab = telefono_hab;
            this.telefono_pers = telefono_pers;
            this.email = email;
        }
    }
}
