﻿using System;
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

        public String buscarNombre(String codMat) {
            try {
                return manejador.buscarNombre(codMat);
            } catch (Exception ex) {
                throw;
            }
        }

        public Double consultarStock(String bode, String mate) {
            try {
                return manejador.consultarStock(bode, mate);
            } catch (Exception exx) {
                throw;
            }
        }

        public List<String> buscarMat() {
            return manejador.buscarMat();
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
            try
            {
                return new DAOManejadorMateriales().buscarUsuarioAdmin(busqueda);
        }
            catch (Exception)
            {
                throw;
            }
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

        public string registrarActualizarMaterialBL(string codigo, string nom, string precioC, String precioV, string unidadBaseCodigo, char tipo, Boolean estado)
        {
            if (String.IsNullOrEmpty(codigo) || String.IsNullOrWhiteSpace(codigo) ||
                String.IsNullOrEmpty(nom) || String.IsNullOrWhiteSpace(nom) ||
                String.IsNullOrEmpty(precioC) || String.IsNullOrWhiteSpace(precioC) ||
                (String.IsNullOrEmpty(precioV) || String.IsNullOrWhiteSpace(precioV)))
                return "Datos incompletos. Por favor, verifique e intente de nuevo";

            double precioBaseC = 0;
            double precioBaseV = 0;
            try
            {
                precioBaseC = Double.Parse(precioC);
                precioBaseV = Double.Parse(precioV);
            }
            catch (Exception e)
            {
                return "El formato de precio solo admite números, por favor intente de nuevo.";
            }
            if (precioBaseC < 0 || precioBaseV < 0)
                return "El precio debe ser un valor positivo, por favor intente de nuevo.";

            TOUnidad unidad = new TOUnidad();
            unidad.codigo = unidadBaseCodigo;
            TOMaterial m = new TOMaterial();
            m.codigoM = codigo;
            m.nombreMaterial = nom;
            m.estado_Material = estado;
            m.precioCompraK = precioBaseC;
            m.precioVentaK = precioBaseV;
            m.cod_Unidad = unidadBaseCodigo;

            return manejador.registrarActualizarMaterialDAO(m, tipo);

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


        public BLMaterial consultarMaterialAdmin(String id)
        {
            try
            {
                TOMaterial to = new DAOManejadorMateriales().buscarMaterialAdmin(id);
                return new BLMaterial(to.codigoM, to.nombreMaterial, to.precioVentaK, to.cod_Unidad, to.precioCompraK, to.estado_Material);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BLMaterial consultarMaterialRegular(String id)
        {
            try
            {
                TOMaterial to = new DAOManejadorMateriales().buscarMaterialRegular(id);
                return new BLMaterial(to.codigoM, to.nombreMaterial, to.precioVentaK, to.cod_Unidad, to.precioCompraK, to.estado_Material);
            }
            catch (Exception)
            {
                throw;
            }
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
