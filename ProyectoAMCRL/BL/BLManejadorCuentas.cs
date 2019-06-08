﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;
using System.Data;


namespace BL {
    public class BLManejadorCuentas {
        public void guardarModificarCuenta(BLCuenta cuenta) {
            //try {
            new DAOCuentas().guardarCuenta(convert(cuenta));
            //} catch(Exception) {
            //    throw;
            //}
        }

        //public List<BLBodegaTabla> listaCuentas() {
        //    List<TOBodegaTabla> to = new DAOBodegas().listaBodega();
        //    List<BLBodegaTabla> listaBL = new List<BLBodegaTabla>();
        //    foreach(TOBodegaTabla bodega in to) {
        //        listaBL.Add(convert(bodega));
        //    }
        //    return listaBL;
        //}

        //public DataTable buscar(string busqueda) {
        //    return new DAOBodegas().buscar(busqueda);
        //}

        //public BLBodega consultarBodega(String id) {
        //    return convert(new DAOBodegas().consultarBodega(id));
        //}

        //private BLBodegaTabla convert(TOBodegaTabla bod) {
        //    return new BLBodegaTabla(bod.codigo, bod.nombre, bod.estado, bod.distrito);
        //}

        //private TOBodegaTabla convert(BLBodegaTabla bod) {
        //    return new TOBodegaTabla(bod.codigo, bod.nombre, bod.estado, bod.distrito);
        //}

        private BLCuenta convert(TOCuenta cuenta) {
            return new BLCuenta(cuenta.id_usuario, cuenta.clave, cuenta.nombre_usuario, cuenta.estado, cuenta.rol);
        }

        private TOCuenta convert(BLCuenta cuenta) {
            return new TOCuenta(cuenta.id_usuario, cuenta.clave, cuenta.nombre_usuario, cuenta.estado, cuenta.rol);
        }

        //private BLDireccion convert(TODireccion dir) {
        //    return new BLDireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
        //}

        //private TODireccion convert(BLDireccion dir) {
        //    return new TODireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
        //}
    }
}
