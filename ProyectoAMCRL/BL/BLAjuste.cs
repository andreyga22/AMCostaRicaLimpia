using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLAjuste
    {

        public int idAjuste { get; set; }
        public DateTime fecha { get; set; }
        public String razon { get; set; }
        public String accion { get; set; }
        public List<BLDetalleAjuste> detalles { get; set; }

        public BLAjuste(DateTime fecha, int idAjuste, string razon, String accion, List<BLDetalleAjuste> detalles)
        {
            this.fecha = fecha;
            this.idAjuste = idAjuste;
            this.razon = razon;
            this.accion = accion;
            this.detalles = detalles; 
        }

        public BLAjuste()
        {
        }

        public String traerFecha()
        {
            return fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
        }
    }
}
