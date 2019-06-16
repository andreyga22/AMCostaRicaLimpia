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

        public string registrarVentaBL(String cedula, String idBodega, String moneda, String fecha, List<String> detalles)
        {
            List<TODetalleFactura> detallesTO = parsearDetalles(detalles);

            //extraer info moneda
            String[] infoMonedaArray = moneda.Split('#');
            String idMoneda = infoMonedaArray[0];
            double equivalenciaColon = Double.Parse(infoMonedaArray[1]);

            //calcular total factura
            double sumaMontosDetalle = calcularSumaDetalles(detallesTO);
            double totalFacturaColones = sumaMontosDetalle * equivalenciaColon;

            DAOManejadorFacturas manejadorDAO = new DAOManejadorFacturas();
            
            return manejadorDAO.registrarFacturaDAO(cedula, idBodega, idMoneda, fecha, 'v', detallesTO, totalFacturaColones);
        }


        public string registrarCompraBL(String cedula, String idBodega, String moneda, String fecha, List<String> detalles)
        {
            List<TODetalleFactura> detallesTO = parsearDetalles(detalles);

            //extraer info moneda
            String[] infoMonedaArray = moneda.Split('#');
            String idMoneda = infoMonedaArray[0];
            double equivalenciaColon = Double.Parse(infoMonedaArray[1]);

            //calcular total factura
            double sumaMontosDetalle = calcularSumaDetalles(detallesTO);
            double totalFacturaColones = sumaMontosDetalle * equivalenciaColon;

            DAOManejadorFacturas manejadorDAO = new DAOManejadorFacturas();
           
            return manejadorDAO.registrarFacturaDAO(cedula, idBodega, idMoneda, fecha, 'c', detallesTO, totalFacturaColones);
        }

        private double calcularSumaDetalles(List<TODetalleFactura> detallesTO) {
            double suma = 0;

            foreach (TODetalleFactura detalle in detallesTO) {
                suma += detalle.monto_Linea;
            }

            return suma;
        }


        private void convertirMonto(String monedaInfo, double equivalencia) {
           
        }


        private List<TODetalleFactura> parsearDetalles(List<String> listaInfo) {
            List<TODetalleFactura> detallesTO = new List<TODetalleFactura>();
            TODetalleFactura detalleTO = null;

            foreach (String detalle in listaInfo) {
                detalleTO = new TODetalleFactura();
                String[] infoLinea = detalle.Split('&');
                //[0]material; [2]precioKilo; [3]cantidad; [4]unidad; 
                String[] materialInfo = infoLinea[0].Split('#');
                String[] idANDstock = materialInfo[0].Split('-'); 
                detalleTO.cod_Material = Int32.Parse(idANDstock[0]);
                String[] infoUnidad = infoLinea[3].Split('#');
                                //  cantidad                *   equivalencia en kilos
                int kilosLinea = (Int32.Parse(infoLinea[2]) * Int32.Parse(infoUnidad[0]));
                detalleTO.kilos_Linea = kilosLinea;
                int precioKg = Int32.Parse(infoLinea[1]);
                detalleTO.monto_Linea = precioKg * kilosLinea;

                detallesTO.Add(detalleTO);
            }
            return detallesTO;
        }

    }
}
