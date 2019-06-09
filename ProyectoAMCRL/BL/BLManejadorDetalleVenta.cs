using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;

namespace BL
{
    public class BLManejadorDetalleVenta
    {

        public List<BLDetalleFactura> listaDetalleVenta(string idFactura)
        {
            try
            {
                DAOManejadorFacturas dao = new DAOManejadorFacturas();
                List<TODetalleFactura> lista = dao.listaDetalle(idFactura);
                List<BLDetalleFactura> listaBL = new List<BLDetalleFactura>();
                foreach (TODetalleFactura venta in lista)
                {
                    listaBL.Add(convert(venta));
                }
                return listaBL;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BLDetalleFactura convert(TODetalleFactura to)
        {
            return new BLDetalleFactura(to.cod_Linea, to.cod_Factura, to.cod_Material, to.nombreMaterial, to.monto_Linea, to.kilos_Linea);
        }

        public TODetalleFactura convert(BLDetalleFactura bl)
        {
            return new TODetalleFactura(bl.cod_Linea, bl.cod_Factura, bl.cod_Material, bl.nombreMaterial, bl.monto_Linea, bl.kilos_Linea);
        }
    }
}
