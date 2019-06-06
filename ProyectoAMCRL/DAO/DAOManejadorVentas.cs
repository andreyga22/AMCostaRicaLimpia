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


        //Retorna todas Facturas de Ventas
        public List<TOVenta> facturas_Ventas()
        {
            //using (conexion)
            //{
                //try
                //{
                    //SqlCommand cmdCompra = new SqlCommand("select c.COD_COMPRA, c.CEDULA, c.MONTO_TOTAL_C, c.Fecha, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from COMPRA c, SOCIO_NEGOCIO s where c.CEDULA = s.CEDULA;", conexion);
                    SqlCommand cmdVenta = new SqlCommand("Select v.COD_VENTA, v.CEDULA, v.MONTO_TOTAL_V, " +
                        "v.Fecha_Venta, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from VENTA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA;", conexion);

                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    //SqlDataReader reader = cmdCompra.ExecuteReader();
                    //SqlDataReader reader2 = cmdVenta.ExecuteReader();

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmdVenta;
                    adapter.Fill(table);
                    List<TOVenta> lista = new List<TOVenta>();


                    for (int x = 0; x < table.Rows.Count; x++)
                    {
                        TOVenta venta = new TOVenta();
                        venta.cod_Venta = Convert.ToInt16(table.Rows[x]["COD_VENTA"]);
                        venta.cedula = Convert.ToString(table.Rows[x]["CEDULA"]);
                        venta.monto_Total = Convert.ToDouble(table.Rows[x]["MONTO_TOTAL_V"]);
                        venta.fecha = Convert.ToDateTime(table.Rows[x]["FECHA_VENTA"]);
                        venta.nombreCompleto = Convert.ToString(table.Rows[x]["NOMBRE"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO1"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO2"]);

                        lista.Add(venta);
                    }
                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }
                    return lista;
                //}
                //catch (Exception)
                //{
                //    throw;
                //}
                //finally
                //{
                //    conexion.Close();
                //}
            }
        }
    //}
}
