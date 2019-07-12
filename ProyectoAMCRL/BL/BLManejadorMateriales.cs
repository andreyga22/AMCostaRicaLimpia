using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;
using TO;

namespace BL
{
    public class BLManejadorMateriales
    {

        DAOManejadorMateriales manejador = new DAOManejadorMateriales();
        public DataSet listarMaterialesBL() {
            return manejador.obtenerMateriales();
        }
        

       public DataSet listarMaterialesEnBodegaBL(String idBodega){
            DataSet materiales = manejador.obtenerMaterialesEnBodegaActual(idBodega);
            return materiales;
        }

        public double traerCantidadVendidaBL(String idM) {
            double resultado = 0;
            resultado = manejador.traerCantidadVendidaDAO(Int32.Parse(idM));
            return resultado;
        }

        public string registrarActualizarMaterialBL(string codigo, string nom, string precioT, string unidadBaseCodigo, char tipo)
        {
            if (String.IsNullOrEmpty(codigo) || String.IsNullOrWhiteSpace(codigo) ||
                String.IsNullOrEmpty(nom) || String.IsNullOrWhiteSpace(nom) ||
                String.IsNullOrEmpty(precioT) || String.IsNullOrWhiteSpace(precioT))
                return "Datos incompletos. Por favor, verifique e intente de nuevo";

            double precioBase = 0;
            try {
            precioBase = Double.Parse(precioT);
            }
            catch (Exception e) {
                return "El formato de precio solo admite números, por favor intente de nuevo.";
            }
            if (precioBase < 0)
                return "El precio debe ser un valor positivo, por favor intente de nuevo.";

            double precio = Double.Parse(precioT);

            TOUnidad unidad = new TOUnidad();
            unidad.codigo = unidadBaseCodigo;
            TOMaterial m = new TOMaterial(codigo, nom, precio, unidad);
            
            return manejador.registrarActualizarMaterialDAO(m, tipo);
        }

        public List<BLMaterial> top3_Materiales()
        {
            try
            {
                DAOManejadorMateriales dao = new DAOManejadorMateriales();
                List<BLMaterial> listaBl = new List<BLMaterial>();
                List<TOMaterial> listaTo = dao.top3_Materiales();
                foreach (TOMaterial to in listaTo)
                {
                    listaBl.Add(new BLMaterial(to.codigoM, to.nombreMaterial, to.precioKilo));
                }
                return listaBl;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet traerUnidadYprecioBase(string codigo, char tipoPrecio)
        {
            return manejador.traerUnidadYprecioBaseDAO(codigo, tipoPrecio);
        }
        //public BLMaterial buscarMaterial(string clave) {

        //    TOMaterial materialTO = manejador.buscarMaterialDAO(clave);
        //    return parsearMaterialTO_BL(materialTO);
        //}

        //private BLMaterial parsearMaterialTO_BL(TOMaterial mto) {

        //    BLMaterial m = null;
        //    if(mto != null)
        //    {
        //        m = new BLMaterial();
        //        m.nombreMaterial = mto.nombreMaterial;
        //        m.codigoM = mto.codigoM;
        //        m.precioKilo = mto.precioKilo;
        //        m.unidadBase = new BLUnidad(mto.unidadBase.codigo, mto.unidadBase.nombre, mto.unidadBase.equivalencia);
        //    }

        //    return m;
        //}

    }
}
