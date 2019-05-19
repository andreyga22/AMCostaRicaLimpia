using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;

namespace DAO
{
    public class DAOManejadorVentas
    {

        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);

        private List<TODetalleCompra> listaDetalles = new List<TODetalleCompra>();

        private DataTable tabla = new DataTable();

        public String registrarDetalles()
        {
            String m = "";
            int codVenta = 2;
            listaDetalles.Add(new TODetalleCompra(codVenta, 5, 30000, 15));
            listaDetalles.Add(new TODetalleCompra(codVenta, 4, 27750, 18.5));
            listaDetalles.Add(new TODetalleCompra(codVenta, 3, 38400, 24));

            tabla.Columns.Add(new DataColumn("COD_VENTA", typeof(int)));
            tabla.Columns.Add(new DataColumn("COD_MATERIAL", typeof(int)));
            tabla.Columns.Add(new DataColumn("MONTO_LINEA_V", typeof(double)));
            tabla.Columns.Add(new DataColumn("KILOS_VENTA", typeof(double)));

            //poblar el DataTable
            foreach (var detalle in listaDetalles)
                tabla.Rows.Add(detalle.codCompra, detalle.codMaterial, detalle.montoLinea, detalle.cantidadLinea);

            //consulta
            try
            {
                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                SqlCommand cmd = new SqlCommand("dbo.INSERTAR_DETALLES_VENTA", conexion);
                SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@Lista", tabla);
                SqlParameter sqlParameterCodVenta = cmd.Parameters.AddWithValue("@cod_venta", codVenta);

                cmd.CommandType = CommandType.StoredProcedure;
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = "dbo.DETALLES_VENTA";

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return m;
        }



  

    }
}
