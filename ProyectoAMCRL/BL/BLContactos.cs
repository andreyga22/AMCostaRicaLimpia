using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLContactos
    {
        public int telefono_hab;
        public int telefono_pers;
        public string email;

        public BLContactos(int telefono_hab, int telefono_pers, string email) {
            this.telefono_hab = telefono_hab;
            this.telefono_pers = telefono_pers;
            this.email = email;
        }
    }
}
