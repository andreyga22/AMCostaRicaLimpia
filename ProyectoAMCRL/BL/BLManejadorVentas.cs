using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;

namespace BL
{
    public class BLManejadorVentas
    {

        public String guadarDetallesVentaBL()
        {
            DAOManejadorVentas manejador = new DAOManejadorVentas();
            return manejador.registrarDetalles();
        }

        public List<BLVenta> facturasVentas()
        {
            //try
            //{
                DAOManejadorVentas dao = new DAOManejadorVentas();
                List<TOVenta> lista = dao.facturas_Ventas();
                List<BLVenta> listaBL = new List<BLVenta>();
                foreach (TOVenta venta in lista)
                {
                    listaBL.Add(convert(venta));
                }
                return listaBL;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public BLVenta convert(TOVenta to)
        {
            return new BLVenta(to.cod_Venta, to.id_Bodega, to.id_Moneda, to.cedula, to.monto_Total, to.fecha, to.nombreCompleto);
        }

        public TOVenta convert(BLVenta bl)
        {
            return new TOVenta(bl.cod_Venta, bl.id_Bodega, bl.id_Moneda, bl.cedula, bl.monto_Total, bl.fecha, bl.nombreCompleto);
        }
    }
}
