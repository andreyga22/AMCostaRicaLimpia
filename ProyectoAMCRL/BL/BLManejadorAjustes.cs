using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BL
{
    public class BLManejadorAjustes
    {
        DAOManejadorAjustes manejador = new DAOManejadorAjustes();

        public DataSet listarAjustesBL()
        {
            return manejador.listarAjustesDAO();
        }
        //id_bod, peso, material, unidad, accion, razon
        public String registrarAjusteBL(int id_bod, string peso, string material, string unidad, string accion, string razon)
        {
            //FALTA VALIDAR LO DE UNIDAD(HACER LA CONVERSION)

            return manejador.registrarAjusteDAO(id_bod, peso, material, accion,  razon);
        }
    }
}
