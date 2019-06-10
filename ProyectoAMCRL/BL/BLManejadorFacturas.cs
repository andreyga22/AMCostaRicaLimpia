using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;

namespace BL
{
    public class BLManejadorFacturas
    {

        //public String guadarDetallesVentaBL()
        //{
        //    DAOManejadorFacturas manejador = new DAOManejadorFacturas();
        //    return manejador.registrarDetalles();
        //}

        public List<BLFactura> facturasVentas()
        {
            try
            {
                DAOManejadorFacturas dao = new DAOManejadorFacturas();
                List<TOFactura> lista = dao.lista_Facturas();
                List<BLFactura> listaBL = new List<BLFactura>();
                foreach (TOFactura venta in lista)
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

        public BLFactura buscarVentaID(int id)
        {
            DAOManejadorFacturas dao = new DAOManejadorFacturas();
            TOFactura to = dao.factPorId(id);
            return convert(to);
        }

        public List<BLDetalleFactura> listaDetalle(String id)
        {
            try
            {
                DAOManejadorFacturas dao = new DAOManejadorFacturas();
                List<TODetalleFactura> lista = dao.listaDetalle(id);
                List<BLDetalleFactura> listaBL = new List<BLDetalleFactura>();
                foreach (TODetalleFactura detalleFactura in lista)
                {
                    listaBL.Add(convertDetalle(detalleFactura));
                }
                return listaBL;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BLFactura convert(TOFactura to)
        {
            return new BLFactura(to.cod_Factura, to.cedula, to.id_Bodega, to.id_Moneda, to.monto_Total, to.tipo, to.fecha, to.nombreCompleto);
        }

        public TOFactura convert(BLFactura bl)
        {
            return new TOFactura(bl.cod_Factura, bl.cedula, bl.id_Bodega, bl.id_Moneda, bl.monto_Total, bl.tipo, bl.fecha, bl.nombreCompleto);
        }


        public BLDetalleFactura convertDetalle(TODetalleFactura to)
        {
            return new BLDetalleFactura(to.cod_Linea, to.cod_Factura, to.cod_Material, to.nombreMaterial, to.monto_Linea, to.kilos_Linea);
        }

        public TODetalleFactura convertDetalle(BLDetalleFactura bl)
        {
            return new TODetalleFactura(bl.cod_Linea, bl.cod_Factura, bl.cod_Material, bl.nombreMaterial, bl.monto_Linea, bl.kilos_Linea);
        }
    }
}
