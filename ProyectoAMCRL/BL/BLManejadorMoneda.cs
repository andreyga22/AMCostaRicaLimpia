using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;
using System.Data;

namespace BL
{
    public class BLManejadorMoneda
    {
        public BLMoneda buscarMonedaId(string id_Moneda)
        {
            DAOManejadorMoneda dao = new DAOManejadorMoneda();
            return convert(dao.buscarMonedaId(id_Moneda));
        }

        public BLMoneda consultar(string id) {
            return convertt(new DAOManejadorMoneda().consultar(id));
        }

        public DataTable buscar(String pal) {
            return new DAOManejadorMoneda().buscar(pal);
        }

        public void guardarActualizarRegular(BLMoneda mon) {
            new DAOManejadorMoneda().guardarActualizarRegular(convertt(mon));
        }

        public void guardarActualizarAdmin(BLMoneda mon) {
            new DAOManejadorMoneda().guardarActualizarAdmin(convertt(mon));
        }

        public BLMoneda convert(TOMoneda to)
        {
            return new BLMoneda(to.idMoneda, to.detalleMoneda, to.equivalencia_Colon);
        }

        public TOMoneda convert(BLMoneda bl)
        {
            return new TOMoneda(bl.idMoneda, bl.detalleMoneda, bl.equivalencia_Colon);
        }

        public BLMoneda convertt(TOMoneda to) {
            return new BLMoneda(to.idMoneda, to.detalleMoneda, to.equivalencia_Colon, to.estado);
        }

        public TOMoneda convertt(BLMoneda bl) {
            return new TOMoneda(bl.idMoneda, bl.detalleMoneda, bl.equivalencia_Colon, bl.estado);
        }

        public DataSet listarMonedas()
        {
            DAOManejadorMoneda dao = new DAOManejadorMoneda();
            return dao.listarMonedasDAO();
        }
    }
}
