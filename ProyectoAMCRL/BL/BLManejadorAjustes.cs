using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;

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
        public String registrarAjusteBL(String id_bod, string accion, string razon, List<BLDetalleAjuste> lista, String fecha)
        {
            List<BLDetalleAjuste> listaBL = new List<BLDetalleAjuste>();
            foreach (BLDetalleAjuste linea in lista) {
                BLDetalleAjuste nuevo = new BLDetalleAjuste();
               
                Double pesoTotalLinea = 0;
                String[] materialInfo = linea.id_Material.Split('*');
                String codMaterial = materialInfo[0];
                //linea.id_Material = codMaterial;
                nuevo.id_Material = codMaterial;
                nuevo.id_Stock = linea.id_Stock;

              String[] unidadInfo = linea.unidadMedida.Split('*');
                Double equiv = Double.Parse(unidadInfo[1]);
                pesoTotalLinea = linea.kilos_Linea * equiv;
                nuevo.kilos_Linea = pesoTotalLinea;
                nuevo.unidadMedida = unidadInfo[1];
                listaBL.Add(nuevo);

            }

            List<TODetalleAjuste> listaTO = parsearDetalles(listaBL);
            return manejador.registrarAjusteDAO(id_bod, accion,  razon, listaTO, fecha);

        }

        private List<TODetalleAjuste> parsearDetalles(List<BLDetalleAjuste> listaBL) {
            List<TODetalleAjuste> listaTO = new List<TODetalleAjuste>();

            if (listaBL != null) {
                foreach (BLDetalleAjuste dbl in listaBL) {
                    TODetalleAjuste dto = new TODetalleAjuste(dbl.id_Material, dbl.id_Stock, dbl.kilos_Linea);
                    listaTO.Add(dto);
                }
            }

            return listaTO;
        }

        private List<BLDetalleAjuste> parsearDetalles(List<TODetalleAjuste> listaBL)
        {
            List<BLDetalleAjuste> listaTO = new List<BLDetalleAjuste>();

            if (listaBL != null)
            {
                foreach (TODetalleAjuste dto in listaBL)
                {
                    BLDetalleAjuste dbl = new BLDetalleAjuste(dto.id_Material, dto.id_Stock, dto.kilos_Linea);
                    dbl.unidadMedida = dto.unidadMedida;
                    listaTO.Add(dbl);
                }
            }

            return listaTO;
        }

        public BLAjuste buscarAjusteBL(string idAjuste)
        {
            TOAjuste ajusteTO = manejador.buscarAjusteDAO(idAjuste);
            BLAjuste ajusteBL = new BLAjuste();

            ajusteBL.idAjuste = ajusteTO.idAjuste;
            ajusteBL.accion = ajusteTO.accion;
            ajusteBL.fecha = ajusteTO.fecha;
            ajusteBL.razon = ajusteTO.razon;

            List<BLDetalleAjuste> detallesBL = parsearDetalles(ajusteTO.detalles);
            ajusteBL.detalles = detallesBL;

            return ajusteBL;
        }

        private double stockMinimoSuperadoBL(double peso, int id_stock)
        {
            double cantidad_stock = manejador.consultarCantidadStockDAO(id_stock);
            return cantidad_stock - peso;

        }

        private double calcularTotalSegunUnidad(string unidad, int pesoN)
        {
            //se deberia validar contra las unidades de peso y equivalencias respectivas guardadas en BD
            String[] codUnidadInfo = unidad.Split('*');
            String codUnidad = codUnidadInfo[0];
            double equivalencia = Double.Parse(codUnidadInfo[1]);
            return equivalencia * pesoN;
        }

        public DataSet filtrarAjustes(string fechaInicio, string fechaFin, string tipo, string pesoMaximo, string pesoMinimo, string bodega, List<string> materiales)
        {
            return manejador.filtrarAjustesDAO( fechaInicio,  fechaFin,  tipo, pesoMaximo, pesoMinimo, bodega,  materiales);
        }
    }
}
