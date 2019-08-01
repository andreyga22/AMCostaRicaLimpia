﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BL {
    public class BLManejadorInventario {

        DAOInventario manejador = new DAOInventario();

        public DataTable buscarStock(String bodega)
        {
            return manejador.buscarStock(bodega);
        }

        public List<String> buscarBodegas()
        {
            return manejador.buscarBodegas();
        }
    }
}