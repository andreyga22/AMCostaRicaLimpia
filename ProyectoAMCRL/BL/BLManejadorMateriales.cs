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
        public DataSet listarMaterialesBL()
        {
            return manejador.obtenerMateriales();
        }

        public DataTable buscar(string busqueda)
        {
            try
            {
                return new DAOManejadorMateriales().buscarUsuarioRegular(busqueda);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable buscarAdmin(string busqueda)
        {
            //try
            //{
            return new DAOManejadorMateriales().buscarUsuarioAdmin(busqueda);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public DataSet listarMaterialesEnBodegaBL(String idBodega)
        {
            DataSet materiales = manejador.obtenerMaterialesEnBodegaActual(idBodega);
            return materiales;
        }

        public double traerCantidadVendidaBL(String idM)
        {
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
            try
            {
                precioBase = Double.Parse(precioT);
            }
            catch (Exception e)
            {
                return "El formato de precio solo admite números, por favor intente de nuevo.";
            }
            if (precioBase < 0)
                return "El precio debe ser un valor positivo, por favor intente de nuevo.";

            double precio = Double.Parse(precioT);

            TOUnidad unidad = new TOUnidad();
            unidad.codigo = unidadBaseCodigo;
            //TOMaterial m = new TOMaterial(codigo, nom, precio, unidad);

            //return manejador.registrarActualizarMaterialDAO(m, tipo);
            return null;
        }

        /// <summary>
        /// Método para conocer los 3 materiales con mayor cantidad en stock
        /// </summary>
        /// <returns></returns>
        public List<BLMaterial> top3_Materiales()
        {
            try
            {
                DAOManejadorMateriales dao = new DAOManejadorMateriales();
                List<BLMaterial> listaBl = new List<BLMaterial>();
                List<TOMaterial> listaTo = dao.top3_Materiales();
                foreach (TOMaterial to in listaTo)
                {
                    listaBl.Add(new BLMaterial(to.codigoM, to.nombreMaterial, to.precioVentaK, to.codigoM, to.precioCompraK, to.estado_Material));
                }
                return listaBl;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para consultar un material
        /// </summary>
        /// <param name="id">Identificador con el que se va a buscar el material</param>
        /// <returns>Retorna el BLMaterial que se buscaba</returns>
        public BLMaterial consultarMaterialAdmin(String id)
        {
            //try
            //{
                TOMaterial to = new DAOManejadorMateriales().buscarMaterialAdmin(id);
                return new BLMaterial(to.codigoM, to.nombreMaterial, to.precioVentaK, to.cod_Unidad, to.precioCompraK, to.estado_Material);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// Método para guardar o modificar un material
        /// </summary>
        /// <param name="mat">Objeto de material que se va a guardar o modificar</param>
        public void guardarModificarBodegaAdmin(BLMaterial mat)
        {
            try
            {
                new DAOManejadorMateriales().guardarModificarBodegaAdmin(convert(mat));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para convertir un objeto BLMaterial en un TOMaterial
        /// </summary>
        /// <param name="bl">Objeto BLMaterial que se va a convertir</param>
        /// <returns>Retorna el nuevo objeto TOMaterial</returns>
        public TOMaterial convert(BLMaterial bl)
        {
            return new TOMaterial(bl.codigoM, bl.nombreMaterial, bl.precioVentaK, bl.cod_Unidad, bl.precioCompraK, bl.estado_Material);
        }

        /// <summary>
        /// Método para convertir un objeto TOMaterial en un BLMaterial
        /// </summary>
        /// <param name="bl">Objeto TOMaterial que se va a convertir</param>
        /// <returns>Retorna el nuevo objeto BLMaterial</returns>
        public BLMaterial convert(TOMaterial to)
        {
            return new BLMaterial(to.codigoM, to.nombreMaterial, to.precioVentaK, to.cod_Unidad, to.precioCompraK, to.estado_Material);
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
