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
            char operacion = (tipo.Equals('v') ? '-' : '+');

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
                        sqlDetalles += "(" + codCompra + ", '" + detalle.cod_Stock + "'," + detalle.kilos_Linea + "," + detalle.monto_Linea + "),";

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
                        sqlUpdateParte1 += "WHEN COD_MATERIAL = '" + detalle.cod_Stock +
                        "' THEN (KILOS_STOCK " + operacion + " " + detalle.kilos_Linea + ") ";

                        sqlUpdateParte2 += detalle.cod_Stock + ",";
                    }

                    sqlUpdateParte2 = sqlUpdateParte2.Remove(sqlUpdateParte2.Length - 1);
                    sqlUpdateParte2 += "));";
                    sqlSumarStock = sqlUpdateParte1 + " END " + sqlUpdateParte2;
                    command.Parameters.AddWithValue("@ID_BOD", idBodega);
                    command.CommandText = sqlSumarStock;

                    //puede tirar excepcion del trigger
                    command.ExecuteNonQuery();



                    //COMMIT A LA TRANSACCION
                    transaction.Commit();
                    return "SUCCCES";
                }
                catch (Exception ex)
                {
                    String mensaje = ex.Message;
                    String[] mensajeArray = mensaje.Split('*');
                    mensaje = mensajeArray[0].Remove(mensajeArray[0].Length - 2);
                    m = mensaje;

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


        /// <summary>
        /// Retorna la lista completa de facturas, ordenadas por la fecha más reciente
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        public List<TOFactura> lista_Facturas_Top3()
        {
            {
                try
                {
                    List<TOFactura> lista = new List<TOFactura>();
                    String sql = "Select f.COD_FACTURA, f.CEDULA, f.ID_MONEDA, f.MONTO_TOTAL, f.FECHA_FACTURA, f.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA f, SOCIO_NEGOCIO s where f.CEDULA = s.CEDULA order by f.FECHA_FACTURA desc;";
                    SqlCommand cmdVenta = new SqlCommand(sql, conexion);

                    //if (string.IsNullOrEmpty(busqueda) == false)
                    //{
                    //    sql += " and ((v.COD_FACTURA LIKE '%' + @pal + '%')  or (V.CEDULA LIKE '%' + @pal + '%') or (v.MONTO_TOTAL LIKE '%' + @pal + '%') or (v.FECHA_FACTURA LIKE '%' + @pal + '%') or (s.NOMBRE LIKE '%' + @pal + '%') or (s.APELLIDO1 LIKE '%' + @pal + '%') or (s.APELLIDO2 LIKE '%' + @pal + '%'));";
                    //    cmdVenta.Parameters.AddWithValue("@pal", "'may'");
                    //}
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
                            to.id_Moneda = reader.GetString(2);
                            to.monto_Total = (Double)reader.GetDecimal(3);
                            to.fecha = reader.GetDateTime(4);
                            to.tipo = reader.GetString(5);

                            to.nombreCompleto = reader.GetString(6) + " " + reader.GetString(7) + " " + reader.GetString(8);
                            lista.Add(to);
                        }
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

        /// <summary>
        /// Buscar una factura por el número de factura
        /// </summary>
        /// <param name="id">Número de factura de la factura que se busca</param>
        /// <returns>Retorna la factura que tiene el número de factura</returns>
        public TOFactura factPorId(int id)
        {
            try
            {
                TOFactura to = new TOFactura();
                String qry = "select v.COD_FACTURA, v.CEDULA,  v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA and v.COD_FACTURA = @num;";
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
                        to.id_Moneda = reader.GetString(2);
                        to.monto_Total = (Double)reader.GetDecimal(3);
                        to.fecha = reader.GetDateTime(4);
                        to.tipo = reader.GetString(5);

                        to.nombreCompleto = reader.GetString(6) + " " + reader.GetString(7) + " " + reader.GetString(8);
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


        /// <summary>
        /// Lista los detalles de una factura
        /// </summary>
        /// <param name="idFactura">Número de factura de la factura que se desea el </param>
        /// <returns></returns>
        public List<TODetalleFactura> listaDetalle(int idFactura)
        {
            try
            {
                SqlCommand cmdDet = new SqlCommand("  Select d.COD_LINEA, d.COD_FACTURA, m.NOMBRE_MATERIAL, d.MONTO_LINEA, d.KILOS, d.ID_STOCK from DETALLE_FACTURA d, FACTURA v, MATERIAL m, STOCK s where d.COD_FACTURA = @codFact and d.COD_FACTURA = v.COD_FACTURA and s.ID_STOCK = d.ID_STOCK and s.COD_MATERIAL = m.COD_MATERIAL;", conexion);
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
                    detVenta.nombreMaterial = Convert.ToString(table.Rows[x]["NOMBRE_MATERIAL"]);
                    detVenta.monto_Linea = Convert.ToDouble(table.Rows[x]["MONTO_LINEA"]);
                    detVenta.kilos_Linea = Convert.ToDouble(table.Rows[x]["KILOS"]);
                    detVenta.cod_Stock = Convert.ToInt16(table.Rows[x]["ID_STOCK"]);

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

        /// <summary>
        /// Método para buscar las facturas que se relacionen con una palabra o el tipo de factura
        /// </summary>
        /// <param name="busqueda">Palabra que se utiliza para buscar en el método</param>
        /// <param name="tipo">Tipo de factura que se está buscando</param>
        /// <returns></returns>
        public DataTable buscar(string busqueda, string tipo)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    //cod, bod, moneda, cedula, monto, fecha, tipo, socio
                    string sql = "Select s.NOMBRE + ' ' + s.APELLIDO1 + ' ' +  s.APELLIDO2 as SOCIO,  v.CEDULA as 'CÉDULA', v.COD_FACTURA as 'NÚMERO FACTURA', v.FECHA_FACTURA as 'FECHA FACTURA', v.MONTO_TOTAL as 'MONTO TOTAL' from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA and v.TIPO = @tipo ";

                    cmd.Parameters.AddWithValue("@tipo", tipo);

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

        /// <summary>
        /// Método para filtrar las facturas según un rango de fechas y montos
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio para el rango de filtro por fechas</param>
        /// <param name="fechaFin">Fecha de fin para el rango de filtro por fechas</param>
        /// <param name="montoMaximo">Monto máximo para el rango de filtro por monto</param>
        /// <param name="montoMinimo">Monto mínimo para el rango de filtro por monto</param>
        /// <param name="materiales">Lista de los materiales que se seleccionaron para filtrar</param>
        /// <param name="tipo">Tipo de factura que se realiza</param>
        /// <returns>Retorna una tabla con las facturas que cumplen con los filtros</returns>
        public DataTable filtrarFacturas(DateTime fechaInicio, DateTime fechaFin, int montoMaximo, int montoMinimo, List<string> materiales, string tipo)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "Select s.NOMBRE + ' ' + s.APELLIDO1 + ' ' +  s.APELLIDO2 as SOCIO,  v.CEDULA as 'CÉDULA', v.COD_FACTURA as 'NÚMERO FACTURA', v.FECHA_FACTURA as 'FECHA FACTURA', v.MONTO_TOTAL as 'MONTO TOTAL' from FACTURA v, SOCIO_NEGOCIO s, DETALLE_FACTURA d where v.CEDULA = s.CEDULA and v.COD_FACTURA = d.COD_FACTURA and v.TIPO = @tipo ";
                    cmd.Parameters.AddWithValue("@tipo", tipo);

                    if (fechaInicio != null && fechaFin != null)
                    {
                        sql += " and FECHA_FACTURA between @fechaInicio and @fechaFin";

                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    }
                    //if (string.IsNullOrEmpty(montoMinimo) == false && string.IsNullOrEmpty(montoMaximo) == false)
                    if (montoMinimo != 0 && montoMaximo != 0)
                    {
                        sql += " and v.MONTO_TOTAL between @montoMinimo and @montoMaximo";

                        cmd.Parameters.AddWithValue("@montoMinimo", montoMinimo);
                        cmd.Parameters.AddWithValue("@montoMaximo", montoMaximo);
                    }
                    if (materiales.Count > 0)
                    {
                        sql += " and d.COD_MATERIAL in (";
                        foreach (String id in materiales)
                        {
                            sql += " @id,";
                            cmd.Parameters.AddWithValue("@id", id);
                        }
                        sql = sql.Remove(sql.Length - 1);
                        sql += " )";
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

        public DataTable buscarTodo(string busqueda, string tipo)
        {
            //try
            //{
            using (conexion)
            {
                SqlCommand cmd = conexion.CreateCommand();
                //cod, bod, moneda, cedula, monto, fecha, tipo, socio
                string sql = "Select v.COD_FACTURA, v.ID_BODEGA, v.ID_MONEDA, v.CEDULA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE + ' ' + s.APELLIDO1 + ' ' +  s.APELLIDO2 as SOCIO from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA and v.TIPO = @tipo ";

                cmd.Parameters.AddWithValue("@tipo", tipo);

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


    }
}
