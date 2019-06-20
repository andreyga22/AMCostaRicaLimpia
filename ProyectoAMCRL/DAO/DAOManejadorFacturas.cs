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

        public String registrarFacturaDAO(String cedula, String idBodega, String idMoneda, String fechaS, char tipo, List<TODetalleFactura> detalles, double totalFacturaColones)//RECIBE LOS PARAMETROS, SE ASUME QUE LAS CANTIDADES VIENEN EN KILOS,
        {

            String m = "";
            int codCompra = -1;
            String[] fechaInfo = fechaS.Split('/');
            int anio = Int32.Parse(fechaInfo[2]);
            int mes = Int32.Parse(fechaInfo[1]);
            int dia = Int32.Parse(fechaInfo[0]);

            DateTime fecha = new DateTime(anio, mes, dia);
            Double montoTotal = totalFacturaColones;
            char operacion = (tipo.Equals('c') ? '-' : '+');

            //TRANSACCION
            using (conexion)
            {
                conexion.Open();

                //Se inicializa la transaccion local
                SqlTransaction transaction = conexion.BeginTransaction();

                //Se asigna un comando a la transaccion
                SqlCommand command = conexion.CreateCommand();
                command.Transaction = transaction;

                //TEXTOS CONSULTAS 
                //FACTURA
                // COD_FACTURA, CEDULA, ID_BODEGA, ID_MONEDA, MONTO_TOTAL, FECHA_FACTURA, TIPO
                String sqlEncabezado = "INSERT INTO FACTURA (ID_BODEGA, CEDULA, ID_MONEDA, MONTO_TOTAL, FECHA_FACTURA, TIPO)" +
                    "VALUES (@BODEGA,@CED,@MONEDA,@TOTAL,@FECHA,@TIPO)";
                String swqlCodCompra = "select IDENT_CURRENT('FACTURA')";
                //DETALLE
                //COD_LINEA, COD_FACTURA, COD_MATERIAL, MONTO_LINEA, KILOS
                String sqlDetalles = "INSERT INTO DETALLE_FACTURA (COD_FACTURA, COD_MATERIAL, KILOS, MONTO_LINEA)" +
                    "VALUES";
                String sqlSumarStock = "";


                try
                {
                    //REGISTRAR ENCABEZADO
                    command.CommandText = sqlEncabezado;
                    command.Parameters.AddWithValue("@BODEGA", idBodega); //validar formato en parametros(?)
                    command.Parameters.AddWithValue("@CED", cedula);
                    command.Parameters.AddWithValue("@MONEDA", idMoneda);
                    command.Parameters.AddWithValue("@TOTAL", montoTotal);//el monto debe venir calculado desde BL
                    command.Parameters.AddWithValue("@FECHA", fecha);
                    command.Parameters.AddWithValue("@TIPO", tipo);

                    command.ExecuteNonQuery();

                    //EXTRAER CODIGO(IDENTITY) DE COMPRA_INGRESADA
                    command.CommandText = swqlCodCompra;
                    codCompra = Convert.ToInt32(command.ExecuteScalar());

                    //REGISTRAR DETALLES   (?)bloquear materiales(?)
                    foreach (var detalle in detalles)
                        sqlDetalles += "(" + codCompra + "," + detalle.cod_Material + "," + detalle.kilos_Linea + "," + detalle.monto_Linea + "),";

                    sqlDetalles = sqlDetalles.Remove(sqlDetalles.Length - 1);
                    sqlDetalles += ";";
                    command.CommandText = sqlDetalles;
                    command.ExecuteNonQuery();

                    //SUMAR LOS MATERIALES COMPRADOS A STOCK DE LA BODEGA RESPECTIVA, 
                    //SE CONSTRUYE DOS PARTES DE CONSULTA DE UN "SWITCH" PARA ACTUALIZAR EL STOCK.
                    String sqlUpdateParte1 = "UPDATE STOCK SET KILOS_STOCK = CASE ";
                    String sqlUpdateParte2 = "WHERE (ID_BODEGA = @ID_BOD AND COD_MATERIAL IN (";

                    foreach (var detalle in detalles)
                    {
                        sqlUpdateParte1 += "WHEN COD_MATERIAL = " + detalle.cod_Material +
                        " THEN (KILOS_STOCK " + operacion + " " + detalle.kilos_Linea + ") ";

                        sqlUpdateParte2 += detalle.cod_Material + ",";
                    }

                    sqlUpdateParte2 = sqlUpdateParte2.Remove(sqlUpdateParte2.Length - 1);
                    sqlUpdateParte2 += "));";
                    sqlSumarStock = sqlUpdateParte1 + " END " + sqlUpdateParte2;
                    command.Parameters.AddWithValue("@ID_BOD", idBodega);
                    command.CommandText = sqlSumarStock;
                    command.ExecuteNonQuery();

                    //COMMIT A LA TRANSACCION
                    transaction.Commit();
                    return "SUCCCES";
                }
                catch (Exception ex)
                {

                    m = "Ocurrió un error en la operación, contacte al administrador. Error: " + ex.Source;

                    try
                    {
                        // Se intenta hacer rollback de la transaccion
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection 
                        // is closed or the transaction has already been rolled 
                        // back on the server.
                        return (exRollback.Message);
                    }

                }//EXCEPCION
                return m;
            }//CONEXION

        }//METODO REGISTRAR COMPRA

        //Retorna todas Facturas de Ventas

        //Retorna lista completa
        public List<TOFactura> lista_Facturas(String busqueda)
        {
            {
                try
                {
                    List<TOFactura> lista = new List<TOFactura>();
                    String sql = "Select v.COD_FACTURA, v.CEDULA, v.ID_BODEGA, v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA";
                    SqlCommand cmdVenta = new SqlCommand(sql, conexion);

                    if (string.IsNullOrEmpty(busqueda) == false)
                    {
                        sql += " and ((v.COD_FACTURA LIKE '%' + @pal + '%')  or (V.CEDULA LIKE '%' + @pal + '%') or (v.MONTO_TOTAL LIKE '%' + @pal + '%') or (v.FECHA_FACTURA LIKE '%' + @pal + '%') or (s.NOMBRE LIKE '%' + @pal + '%') or (s.APELLIDO1 LIKE '%' + @pal + '%') or (s.APELLIDO2 LIKE '%' + @pal + '%'));";
                        cmdVenta.Parameters.AddWithValue("@pal", "'may'");
                    }
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    SqlDataReader reader = cmdVenta.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TOFactura to = new TOFactura();
                            to.cod_Factura = (Int16)reader.GetDecimal(0);
                            to.cedula = reader.GetString(1);
                            to.id_Bodega = (reader.GetString(2));
                            to.id_Moneda = reader.GetString(3);
                            to.monto_Total = (Double)reader.GetDecimal(4);
                            to.fecha = reader.GetDateTime(5);
                            to.tipo = reader.GetString(6);

                            to.nombreCompleto = reader.GetString(7) + " " + reader.GetString(8) + " " + reader.GetString(9);
                            lista.Add(to);
                        }
                    }
 


                    //DataTable table = new DataTable();
                    //SqlDataAdapter adapter = new SqlDataAdapter();
                    //adapter.SelectCommand = cmdVenta;
                    //adapter.Fill(table);
                    //List<TOFactura> lista = new List<TOFactura>();

                    //for (int x = 0; x < table.Rows.Count; x++)
                    //{
                    //    TOFactura venta = new TOFactura();
                    //    venta.cod_Factura = Convert.ToInt16(table.Rows[x]["COD_FACTURA"]);
                    //    venta.cedula = Convert.ToString(table.Rows[x]["CEDULA"]);
                    //    venta.id_Bodega = Convert.ToString(table.Rows[x]["ID_BODEGA"]);
                    //    venta.id_Moneda = Convert.ToString(table.Rows[x]["ID_MONEDA"]);
                    //    venta.monto_Total = Convert.ToDouble(table.Rows[x]["MONTO_TOTAL"]);
                    //    venta.fecha = Convert.ToDateTime(table.Rows[x]["FECHA_FACTURA"]);
                    //    venta.tipo = Convert.ToString(table.Rows[x]["TIPO"]);
                    //    venta.nombreCompleto = Convert.ToString(table.Rows[x]["NOMBRE"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO1"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO2"]);

                    //    lista.Add(venta);
                    //}
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

        //lista filtrada por rango de fechas
        public List<TOFactura> listaPorMonto(double monto1, double monto2)
        {
            {
                try
                {
                    SqlCommand cmdFacturas = new SqlCommand("Select v.COD_FACTURA, v.CEDULA, v.ID_BODEGA, v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA  and MONTO_TOTAL >= @monto1 and MONTO_TOTAL <= @monto2;", conexion);
                    cmdFacturas.Parameters.AddWithValue("@monto1", monto1);
                    cmdFacturas.Parameters.AddWithValue("@monto2", monto2);

                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmdFacturas;
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


        //lista filtrada por rango tipo de factura
        public List<TOFactura> listaPorTipo(string tipo)

        {
            try
            {
                SqlCommand cmdFacturas = new SqlCommand("Select v.COD_FACTURA, v.CEDULA, v.ID_BODEGA, v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA  and v.TIPO = @tipo", conexion);
                cmdFacturas.Parameters.AddWithValue("@tipo", tipo);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdFacturas;
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


        public DataTable buscar(string busqueda)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "Select v.COD_FACTURA, v.CEDULA, v.ID_BODEGA, v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE + ' ' + s.APELLIDO1 + ' ' +  s.APELLIDO2 as SOCIO from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA ";

                    if (string.IsNullOrEmpty(busqueda) == false)
                    {
                        sql += " and ((v.COD_FACTURA LIKE '%' + @pal + '%')  or (V.CEDULA LIKE '%' + @pal + '%') or (v.MONTO_TOTAL LIKE '%' + @pal + '%') or (v.FECHA_FACTURA LIKE '%' + @pal + '%') or (s.NOMBRE LIKE '%' + @pal + '%') or (s.APELLIDO1 LIKE '%' + @pal + '%') or (s.APELLIDO2 LIKE '%' + @pal + '%'));";

                        cmd.Parameters.AddWithValue("@pal", busqueda);
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = conexion;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
