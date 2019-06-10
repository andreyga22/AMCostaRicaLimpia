using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;

namespace BL
{
    public class BLManejadorMoneda
    {
        public BLMoneda buscarMonedaId(string id_Moneda)
        {
            DAOManejadorMoneda dao = new DAOManejadorMoneda();
            return convert(dao.buscarMonedaId(id_Moneda));
        }

        public BLMoneda convert(TOMoneda to)
        {
            return new BLMoneda(to.idMoneda, to.detalleMoneda, to.equivalencia_Colon);
        }

        public TOMoneda convert(BLMoneda bl)
        {
            return new TOMoneda(bl.idMoneda, bl.detalleMoneda, bl.equivalencia_Colon);
        }
    }
}
