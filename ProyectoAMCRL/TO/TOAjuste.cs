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
        public String accion { get; set; }
        public List<TODetalleAjuste> detalles { get; set; }

        public TOAjuste(DateTime fecha, int idAjuste, string razon, String accion, List<TODetalleAjuste> detalles)
        {
            this.fecha = fecha;
            this.idAjuste = idAjuste;
            this.razon = razon;
            this.accion = accion;
            this.detalles = detalles;
        }

        public TOAjuste()
        {
        }

        public String traerFecha() {
            return fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
        }
    }
}
