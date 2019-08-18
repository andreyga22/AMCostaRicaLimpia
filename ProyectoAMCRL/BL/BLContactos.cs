using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLContactos
    {
        public String telefono_hab;
        public String telefono_pers;
        public String email;

        public BLContactos(String telefono_hab, String telefono_pers, String email) {
            this.telefono_hab = telefono_hab;
            this.telefono_pers = telefono_pers;
            this.email = email;
        }
    }
}
