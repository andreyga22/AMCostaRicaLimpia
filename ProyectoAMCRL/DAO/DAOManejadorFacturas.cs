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
    public class DAOManejadorFacturas
    {

        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        private List<TODetalleFactura> listaDetalles = new List<TODetalleFactura>();

        private DataTable tabla = new DataTable();

        //public String registrarDetalles()
        //{
        //    String m = "";
        //    int codVenta = 2;
        //    listaDetalles.Add(new TODetalleCompra(codVenta, 5, 30000, 15));
        //    listaDetalles.Add(new TODetalleCompra(codVenta, 4, 27750, 18.5));
        //    listaDetalles.Add(new TODetalleCompra(codVenta, 3, 38400, 24));

        //    tabla.Columns.Add(new DataColumn("COD_VENTA", typeof(int)));
        //    tabla.Columns.Add(new DataColumn("COD_MATERIAL", typeof(int)));
        //    tabla.Columns.Add(new DataColumn("MONTO_LINEA_V", typeof(double)));
        //    tabla.Columns.Add(new DataColumn("KILOS_VENTA", typeof(double)));

        //    //poblar el DataTable
        //    foreach (var detalle in listaDetalles)
        //        tabla.Rows.Add(detalle.codCompra, detalle.codMaterial, detalle.montoLinea, detalle.cantidadLinea);

        //    //consulta
        //    try
        //    {
        //        if (conexion.State == ConnectionState.Closed)
        //            conexion.Open();

        //        SqlCommand cmd = new SqlCommand("dbo.INSERTAR_DETALLES_VENTA", conexion);
        //        SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@Lista", tabla);
        //        SqlParameter sqlParameterCodVenta = cmd.Parameters.AddWithValue("@cod_venta", codVenta);

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        sqlParameter.SqlDbType = SqlDbType.Structured;
        //        sqlParameter.TypeName = "dbo.DETALLES_VENTA";

        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //    return m;
        //}


        //Retorna todas Facturas de Ventas

            //Retorna lista completa
        public List<TOFactura> lista_Facturas()
        {
            {
                try
                {
                    SqlCommand cmdVenta = new SqlCommand("Select v.COD_FACTURA, v.CEDULA, v.ID_BODEGA, v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA;", conexion);

                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmdVenta;
                    adapter.Fill(table);
                    List<TOFactura> lista = new List<TOFactura>();


                    for (int x = 0; x < table.Rows.Count; x++)
                    {
                        TOFactura venta = new TOFactura();
                        venta.cod_Factura = Convert.ToInt16(table.Rows[x]["COD_FACTURA"]);
                        venta.cedula = Convert.ToString(table.Rows[x]["CEDULA"]);
                        venta.id_Bodega = Convert.ToString(table.Rows[x]["ID_BODEGA"]);
                        venta.id_Moneda = Convert.ToString(table.Rows[x]["ID_MONEDA"]);
                        venta.monto_Total = Convert.ToDouble(table.Rows[x]["MONTO_TOTAL"]);
                        venta.fecha = Convert.ToDateTime(table.Rows[x]["FECHA_FACTURA"]);
                        venta.tipo = Convert.ToString(table.Rows[x]["TIPO"]);
                        venta.nombreCompleto = Convert.ToString(table.Rows[x]["NOMBRE"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO1"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO2"]);

                        lista.Add(venta);
                    }
                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }
                    return lista;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        //Retorna factura por identificador
        public TOFactura factPorId(int id)
        {
            try
            {
                TOFactura to = new TOFactura();
                String qry = "select v.COD_FACTURA, v.CEDULA, v.ID_BODEGA, v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA and v.COD_FACTURA = @num;";
                SqlCommand comm = new SqlCommand(qry, conexion);
                comm.Parameters.AddWithValue("@num", id);
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        to.cod_Factura = (Int16)reader.GetDecimal(0);
                        to.cedula = reader.GetString(1);
                        to.id_Bodega = (reader.GetString(2));
                        to.id_Moneda = reader.GetString(3);
                        to.monto_Total = (Double)reader.GetDecimal(4);
                        to.fecha = reader.GetDateTime(5);
                        to.tipo = reader.GetString(6);

                        to.nombreCompleto = reader.GetString(7) + " " + reader.GetString(8) + " " + reader.GetString(9);
                    }
                }
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
                return to;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        //lista de detalles de una factura
        public List<TODetalleFactura> listaDetalle(String idFactura)
        {
            try
            {
                SqlCommand cmdDet = new SqlCommand("Select d.COD_LINEA, d.COD_FACTURA, d.COD_MATERIAL, m.NOMBRE_MATERIAL, d.MONTO_LINEA, d.KILOS from DETALLE_FACTURA d, FACTURA v, material m where d.COD_FACTURA = @codFact and d.COD_FACTURA = v.COD_FACTURA and d.COD_MATERIAL = m.COD_MATERIAL;", conexion);
                cmdDet.Parameters.AddWithValue("@codFact", idFactura);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdDet;
                adapter.Fill(table);
                List<TODetalleFactura> lista = new List<TODetalleFactura>();


                for (int x = 0; x < table.Rows.Count; x++)
                {
                    TODetalleFactura detVenta = new TODetalleFactura();
                    detVenta.cod_Linea = Convert.ToInt16(table.Rows[x]["COD_LINEA"]);
                    detVenta.cod_Factura = Convert.ToInt16(table.Rows[x]["COD_FACTURA"]);
                    detVenta.cod_Material = Convert.ToInt16(table.Rows[x]["COD_MATERIAL"]);
                    detVenta.nombreMaterial = Convert.ToString(table.Rows[x]["NOMBRE_MATERIAL"]);
                    detVenta.monto_Linea = Convert.ToDouble(table.Rows[x]["MONTO_LINEA"]);
                    detVenta.kilos_Linea = Convert.ToDouble(table.Rows[x]["KILOS"]);

                    lista.Add(detVenta);
                }
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
