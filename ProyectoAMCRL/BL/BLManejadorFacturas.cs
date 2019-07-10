using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;
using System.Data;

namespace BL
{
    public class BLManejadorFacturas
    {

        /// <summary>
        /// Método para buscar las facturas que se relacionen con una palabra o el tipo de factura
        /// </summary>
        /// <param name="busqueda">Palabra que se utiliza para buscar en el método</param>
        /// <param name="tipo">Tipo de factura que se está revisando</param>
        /// <returns></returns>
        public DataTable buscar(string busqueda, string tipo)
        {
            try
            {
                return new DAOManejadorFacturas().buscar(busqueda, tipo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BLFactura> listaFact_Top3()
        {
            try
            {
                DAOManejadorFacturas dao = new DAOManejadorFacturas();
                List<TOFactura> lista = dao.lista_Facturas_Top3();
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

        /// <summary>
        /// Busca una factura por el identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna la factura con el identificador que se busca</returns>
        public BLFactura buscarVentaID(int id)
        {
            DAOManejadorFacturas dao = new DAOManejadorFacturas();
            TOFactura to = dao.factPorId(id);
            return convert(to);
        }

        /// <summary>
        /// Método para conocer la lista de detalle de una factura
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna la lista de detalle de una factura</returns>
        public List<BLDetalleFactura> listaDetalle(int id)
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

        public int numeroRangoFecha(string tipo)
        {
            DAOManejadorFacturas dao = new DAOManejadorFacturas();
            return dao.numeroRangoFecha(tipo);
        }

        /// <summary>
        /// Método para filtrar las facturas según un rango de fechas y montos
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio para el rango de filtro por fechas</param>
        /// <param name="fechaFin">Fecha de fin para el rango de filtro por fechas</param>
        /// <param name="montoMaximo">Monto máximo para el rango de filtro por monto</param>
        /// <param name="montoMinimo">Monto mínimo para el rango de filtro por monto</param>
        /// <param name="materiales">Lista de los materiales que se seleccionaron para filtrar</param>
        /// <param name="tipo">Tipo de factura que se realiza</param>
        /// <returns>Retorna una tabla con las facturas que cumplen con los filtros</returns>
        public DataTable filtrarFacturas(string fechaInicio, string fechaFin, string montoMaximo, string montoMinimo, List<string> materiales, string tipo)
        {
            try
            {
                DAOManejadorFacturas manej = new DAOManejadorFacturas();
                DateTime fechaInicioNuevo = new DateTime();
                DateTime fechaFinNuevo = new DateTime();
                string dia = "";
                string mes = "";
                string anno = "";
                int montoMinimoNuevo = 0;
                int montoMaximoNuevo = 0;

                if (string.IsNullOrEmpty(fechaInicio) == false && string.IsNullOrEmpty(fechaFin) == false)
                {
                    dia = fechaInicio[0].ToString() + fechaInicio[1].ToString();
                    mes = fechaInicio[3].ToString() + fechaInicio[4].ToString();
                    anno = fechaInicio[6].ToString() + fechaInicio[7].ToString() + fechaInicio[8].ToString() + fechaInicio[9].ToString();
                    fechaInicioNuevo = new DateTime(int.Parse(anno), int.Parse(mes), int.Parse(dia));

                    dia = fechaFin[0].ToString() + fechaFin[1].ToString();
                    mes = fechaFin[3].ToString() + fechaFin[4].ToString();
                    anno = fechaFin[6].ToString() + fechaFin[7].ToString() + fechaFin[8].ToString() + fechaFin[9].ToString();
                    fechaFinNuevo = new DateTime(int.Parse(anno), int.Parse(mes), int.Parse(dia));
                }
                else
                {
                    if ((string.IsNullOrEmpty(fechaInicio) == true && string.IsNullOrEmpty(fechaFin) == false) || (string.IsNullOrEmpty(fechaInicio) == false && string.IsNullOrEmpty(fechaFin) == true))
                    {
                        if (string.IsNullOrEmpty(fechaInicio) == true)
                        {
                            fechaInicioNuevo = new DateTime(1800, 12, 25);
                            dia = fechaFin[0].ToString() + fechaFin[1].ToString();
                            mes = fechaFin[3].ToString() + fechaFin[4].ToString();
                            anno = fechaFin[6].ToString() + fechaFin[7].ToString() + fechaFin[8].ToString() + fechaFin[9].ToString();
                            fechaFinNuevo = new DateTime(int.Parse(anno), int.Parse(mes), int.Parse(dia));
                        }
                        else
                        {
                            dia = fechaInicio[0].ToString() + fechaInicio[1].ToString();
                            mes = fechaInicio[3].ToString() + fechaInicio[4].ToString();
                            anno = fechaInicio[6].ToString() + fechaInicio[7].ToString() + fechaInicio[8].ToString() + fechaInicio[9].ToString();
                            fechaInicioNuevo = new DateTime(int.Parse(anno), int.Parse(mes), int.Parse(dia));
                            fechaFinNuevo = new DateTime(5000, 12, 25);
                        }
                    }
                    else
                    {
                        fechaInicioNuevo = new DateTime(1800, 12, 25);
                        fechaFinNuevo = new DateTime(5000, 12, 25);
                    }
                }

                if ((string.IsNullOrEmpty(montoMinimo) == true && string.IsNullOrEmpty(montoMaximo) == false) || (string.IsNullOrEmpty(montoMinimo) == false && string.IsNullOrEmpty(montoMaximo) == true))
                {
                    if (string.IsNullOrEmpty(montoMinimo) == true)
                    {
                        montoMinimoNuevo = 0;
                        montoMaximoNuevo = Int32.Parse(montoMaximo);
                    }
                    else
                    {
                        montoMinimoNuevo = Int32.Parse(montoMinimo);
                        montoMaximoNuevo = 999999999;
                    }
                }

                return manej.filtrarFacturas(fechaInicioNuevo, fechaFinNuevo, montoMaximoNuevo, montoMinimoNuevo, materiales, tipo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //return manej.filtrarFacturasDAO(fechaInicio, fechaFin, tipo, montoMaximo, montoMinimo, materiales);
        //List<BLFactura> listaBL = new List<BLFactura>();
        //List<TOFactura> listaTO = manej.filtrarFacturasDAO(fechaInicio, fechaFin, tipo, montoMaximo, montoMinimo, materiales);
        //foreach (TOFactura factura in listaTO)
        //{
        //    listaBL.Add(convert(factura));
        //}
        //return listaBL;



        //public List<BLFactura> listaRangoFecha(DateTime fecha1, DateTime fecha2)
        //{
        //    DAOManejadorFacturas facturas = new DAOManejadorFacturas();
        //    List<BLFactura> listaFiltradaFactTO = new List<BLFactura>();
        //    List<TOFactura> listFacturas= facturas.lista_Facturas("");
        //    if (listFacturas.Count > 0)
        //    {
        //        foreach (TOFactura factTO in listFacturas)
        //        {
        //            factTO.fecha = new DateTime(factTO.fecha.Year, factTO.fecha.Month, factTO.fecha.Day);
        //            int resultado1 = DateTime.Compare(fecha1, factTO.fecha);
        //            int resultado2 = DateTime.Compare(fecha2, factTO.fecha);
        //            if (resultado1 <= 0 && resultado2 >= 0)
        //            {
        //                listaFiltradaFactTO.Add(convert(factTO));
        //            }
        //        }
        //    }
        //    return listaFiltradaFactTO;
        //}

        //public String guadarDetallesVentaBL()
        //{
        //    DAOManejadorFacturas manejador = new DAOManejadorFacturas();
        //    return manejador.registrarDetalles();
        //}



        public List<BLFactura> facturasTipo(string tipo)
        {
            try
            {
                DAOManejadorFacturas dao = new DAOManejadorFacturas();
                List<TOFactura> lista = dao.listaPorTipo(tipo);
                List<BLFactura> listaBL = new List<BLFactura>();
                foreach (TOFactura factura in lista)
                {
                    listaBL.Add(convert(factura));
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
            return new BLDetalleFactura(to.cod_Linea, to.cod_Factura, to.nombreMaterial, to.monto_Linea, to.kilos_Linea, to.cod_Stock);
        }

        public TODetalleFactura convertDetalle(BLDetalleFactura bl)
        {
            return new TODetalleFactura(bl.cod_Linea, bl.cod_Factura, bl.nombreMaterial, bl.monto_Linea, bl.kilos_Linea, bl.cod_Stock);
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

        public double calcularTotalActual(List<String> detalles)
        {
            return calcularSumaDetalles(parsearDetalles(detalles));
        }

        private double calcularSumaDetalles(List<TODetalleFactura> detallesTO)
        {
            double suma = 0;

            foreach (TODetalleFactura detalle in detallesTO)
            {
                suma += detalle.monto_Linea;

            }

            return suma;
        }


        private List<TODetalleFactura> parsearDetalles(List<String> listaInfo)
        {
            List<TODetalleFactura> detallesTO = new List<TODetalleFactura>();
            TODetalleFactura detalleTO = null;

            foreach (String detalle in listaInfo)
            {
                detalleTO = new TODetalleFactura();
                String[] infoLinea = detalle.Split('&');
                //[0]material; [2]precioKilo; [3]cantidad; [4]unidad; 
                String[] materialInfo = infoLinea[0].Split('#');
                String[] idANDstock = materialInfo[0].Split('-');
                detalleTO.cod_Stock = Convert.ToInt16(idANDstock[0]);

                String[] infoUnidad = infoLinea[3].Split('#');
                String[] codUnidadInfo = infoUnidad[0].Split('*');
                String codUnidad = codUnidadInfo[0];
                double equivalencia = Double.Parse(codUnidadInfo[1]);


                //  cantidad                *   equivalencia en kilos
                double kilosLinea = (Int32.Parse(infoLinea[2]) * equivalencia);
                detalleTO.kilos_Linea = kilosLinea;
                double precioKg = Double.Parse(infoLinea[1]);
                detalleTO.monto_Linea = precioKg * kilosLinea;

                detallesTO.Add(detalleTO);
            }
            return detallesTO;
        }

    }




    //public List<BLFactura> listaMontos(double monto1, double monto2)
    //{
    //    try
    //    {
    //        DAOManejadorFacturas dao = new DAOManejadorFacturas();
    //        List<TOFactura> lista = dao.listaPorMonto(monto1, monto2);
    //        List<BLFactura> listaBL = new List<BLFactura>();
    //        foreach (TOFactura factura in lista)
    //        {
    //            listaBL.Add(convert(factura));
    //        }
    //        return listaBL;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
}
