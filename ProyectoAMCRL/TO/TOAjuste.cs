using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOAjuste
    {
        public int idAjuste { get; set; }
        public DateTime fecha { get; set; }
        public String razon { get; set; }
        public double peso { get; set; }
        public int accion { get; set; }
        public int id_stock { get; set; }

        public TOAjuste(DateTime fecha, int idAjuste, string razon, double peso, int accion, int id_stock)
        {
            this.fecha = fecha;
            this.idAjuste = idAjuste;
            this.razon = razon;
            this.peso = peso;
            this.accion = accion;
            this.id_stock = id_stock;
        }

        public TOAjuste()
        {
        }
    }
}
