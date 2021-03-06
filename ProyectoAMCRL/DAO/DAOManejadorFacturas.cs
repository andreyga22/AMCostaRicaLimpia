﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;

namespace DAO {
    public class DAOManejadorFacturas {

        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        private List<TODetalleFactura> listaDetalles = new List<TODetalleFactura>();

        private DataTable tabla = new DataTable();




        public void guardarFactura(List<TODetalleFactura> det, TOFactura fac) {

            using (conexion) {
                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                SqlTransaction sqlTran = conexion.BeginTransaction();

                SqlCommand sentencia = conexion.CreateCommand();
                sentencia.Transaction = sqlTran;

                try {
                    sentencia.CommandText =
            "insert into Factura(cedula, id_bodega, id_moneda, monto_total, fecha_factura, tipo, id_usuario, subtotal, impuesto, descuento, montoInternacional) values(@cedula, @id_bodega, @id_moneda, @monto_total, @fecha_factura, @tipo, @id_usuario, @subtotal, @impuesto, @descuento, @montoInternacional);" +
        " SELECT SCOPE_IDENTITY();";
                    sentencia.Parameters.AddWithValue("@cedula", fac.cedula);
                    sentencia.Parameters.AddWithValue("@id_bodega", fac.id_Bodega);
                    sentencia.Parameters.AddWithValue("@id_moneda", fac.id_Moneda);
                    sentencia.Parameters.AddWithValue("@monto_total", fac.total);
                    sentencia.Parameters.AddWithValue("@fecha_factura", fac.fecha);
                    sentencia.Parameters.AddWithValue("@tipo", fac.tipo);
                    sentencia.Parameters.AddWithValue("@id_usuario", fac.cajero);
                    sentencia.Parameters.AddWithValue("@subtotal", fac.subtotal);
                    sentencia.Parameters.AddWithValue("@impuesto", fac.impuesto);
                    sentencia.Parameters.AddWithValue("@descuento", fac.descuento);
                    sentencia.Parameters.AddWithValue("@montoInternacional", fac.montoInternacional);
                    int codFac = 0;
                    codFac = Convert.ToInt32(sentencia.ExecuteScalar());

                    sentencia.Parameters.Clear();

                    sentencia.CommandText =
                        "insert into detalle_factura(cod_factura, monto_linea, kilos, cod_material, precio, impuesto, descuento) values(@cod_factura, @monto_linea, @kilos, @cod_material, @precio, @impuesto, @descuento);";
                    sentencia.Parameters.AddWithValue("@cod_factura", codFac);
                    sentencia.Parameters.AddWithValue("@monto_linea", "");
                    sentencia.Parameters.AddWithValue("@kilos", "");
                    sentencia.Parameters.AddWithValue("@cod_material", "");
                    sentencia.Parameters.AddWithValue("@precio", "");
                    sentencia.Parameters.AddWithValue("@impuesto", "");
                    sentencia.Parameters.AddWithValue("@descuento", "");

                    foreach (TODetalleFactura item in det) {

                        sentencia.Parameters["@monto_linea"].Value = item.monto_Linea;
                        sentencia.Parameters["@kilos"].Value = item.kilos_Linea;
                        sentencia.Parameters["@cod_material"].Value = item.nombreMaterial;
                        sentencia.Parameters["@precio"].Value = item.precio;
                        sentencia.Parameters["@impuesto"].Value = item.impuesto;
                        sentencia.Parameters["@descuento"].Value = item.descuento;
                        sentencia.ExecuteNonQuery();

                    }

                    sentencia.Parameters.Clear();
                    if (fac.tipo.Equals("venta")) {
                        sentencia.CommandText =
                            "UPDATE stock SET kilos_stock = (kilos_stock - @cantidad) WHERE (cod_material = @cod_material) and (id_bodega = @id_bodega);";
                        sentencia.Parameters.AddWithValue("@cantidad", "");
                        sentencia.Parameters.AddWithValue("@cod_material", "");
                        sentencia.Parameters.AddWithValue("@id_bodega", fac.id_Bodega);
                        foreach (TODetalleFactura item in det) {
                            sentencia.Parameters["@cantidad"].Value = item.kilos_Linea;
                            sentencia.Parameters["@cod_material"].Value = item.nombreMaterial;
                            sentencia.ExecuteNonQuery();
                        }
                    } else {
                        sentencia.CommandText =
                            "UPDATE stock SET kilos_stock = (kilos_stock + @cantidad) WHERE (cod_material = @cod_material) and (id_bodega = @id_bodega);";
                        sentencia.Parameters.AddWithValue("@cantidad", "");
                        sentencia.Parameters.AddWithValue("@cod_material", "");
                        sentencia.Parameters.AddWithValue("@id_bodega", fac.id_Bodega);
                        foreach (TODetalleFactura item in det) {
                            sentencia.Parameters["@cantidad"].Value = item.kilos_Linea;
                            sentencia.Parameters["@cod_material"].Value = item.nombreMaterial;
                            sentencia.ExecuteNonQuery();
                        }
                    }
                    sqlTran.Commit();
                    if (conexion.State != ConnectionState.Closed) {
                        conexion.Close();
                    }


                } catch (Exception exx) {
                    try {
                        sqlTran.Rollback();
                        throw;
                    } catch (Exception) {

                        throw;
                    }
                } finally {
                    conexion.Close();
                }
            }

        }






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
            using (conexion) {
                conexion.Open();

                //Se inicializa la transaccion local
                SqlTransaction transaction = conexion.BeginTransaction();

                //Se asigna un comando a la transaccion
                SqlCommand command = conexion.CreateCommand();
                command.Transaction = transaction;

                //TEXTOS CONSULTAS 
                //FACTURA
                // COD_FACTURA, CEDULA, ID_BODEGA, ID_MONEDA, MONTO_TOTAL, FECHA_FACTURA, TIPO
                String sqlEncabezado = "INSERT INTO FACTURA (CEDULA, ID_BODEGA, ID_MONEDA, MONTO_TOTAL, FECHA_FACTURA, TIPO)" +
                    "VALUES (@CED,@BODEGA, @MONEDA,@TOTAL,@FECHA,@TIPO)";
                String swqlCodCompra = "select IDENT_CURRENT('FACTURA')";
                //DETALLE
                //COD_LINEA, COD_FACTURA, COD_MATERIAL, MONTO_LINEA, KILOS
                String sqlDetalles = "INSERT INTO DETALLE_FACTURA (COD_FACTURA, COD_MATERIAL, KILOS, MONTO_LINEA)" +
                    "VALUES";
                String sqlSumarStock = "";


                try {
                    //REGISTRAR ENCABEZADO
                    command.CommandText = sqlEncabezado;
                    command.Parameters.AddWithValue("@CED", cedula);
                    command.Parameters.AddWithValue("@BODEGA", idBodega);
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
                        sqlDetalles += "(" + codCompra + ", '" + detalle.nombreMaterial + "'," + detalle.kilos_Linea + "," + detalle.monto_Linea + "),";

                    sqlDetalles = sqlDetalles.Remove(sqlDetalles.Length - 1);
                    sqlDetalles += ";";
                    command.CommandText = sqlDetalles;
                    command.ExecuteNonQuery();

                    //SUMAR LOS MATERIALES COMPRADOS A STOCK DE LA BODEGA RESPECTIVA, 
                    //SE CONSTRUYE DOS PARTES DE CONSULTA DE UN "SWITCH" PARA ACTUALIZAR EL STOCK.
                    String sqlUpdateParte1 = "UPDATE STOCK SET KILOS_STOCK = CASE ";
                    String sqlUpdateParte2 = "WHERE (ID_BODEGA = @ID_BOD AND COD_MATERIAL IN (";

                    foreach (var detalle in detalles) {
                        sqlUpdateParte1 += "WHEN COD_MATERIAL = '" + detalle.nombreMaterial +
                        "' THEN (KILOS_STOCK " + operacion + " " + detalle.kilos_Linea + ") ";

                        sqlUpdateParte2 += "'" + detalle.nombreMaterial + "',";
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
                } catch (Exception ex) {
                    String mensaje = ex.Message;
                    String[] mensajeArray = mensaje.Split('*');
                    mensaje = mensajeArray[0].Remove(mensajeArray[0].Length - 2);
                    m = mensaje;

                    try {
                        // Se intenta hacer rollback de la transaccion
                        transaction.Rollback();
                    } catch (Exception exRollback) {
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
        public List<TOFactura> lista_Facturas_Top3() {

            try {
                List<TOFactura> lista = new List<TOFactura>();
                String sql = "Select f.COD_FACTURA, f.CEDULA, f.ID_MONEDA, f.MONTO_TOTAL, f.FECHA_FACTURA, f.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA f, SOCIO_NEGOCIO s where f.CEDULA = s.CEDULA order by f.FECHA_FACTURA desc;";
                SqlCommand cmdVenta = new SqlCommand(sql, conexion);

                //if (string.IsNullOrEmpty(busqueda) == false)
                //{
                //    sql += " and ((v.COD_FACTURA LIKE '%' + @pal + '%')  or (V.CEDULA LIKE '%' + @pal + '%') or (v.MONTO_TOTAL LIKE '%' + @pal + '%') or (v.FECHA_FACTURA LIKE '%' + @pal + '%') or (s.NOMBRE LIKE '%' + @pal + '%') or (s.APELLIDO1 LIKE '%' + @pal + '%') or (s.APELLIDO2 LIKE '%' + @pal + '%'));";
                //    cmdVenta.Parameters.AddWithValue("@pal", "'may'");
                //}
                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                SqlDataReader reader = cmdVenta.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        TOFactura to = new TOFactura();
                        to.cod_Factura = (Int16)reader.GetDecimal(0);
                        to.cedula = reader.GetString(1);
                        to.id_Moneda = reader.GetString(2);
                        to.total = (Double)reader.GetDecimal(3);
                        to.fecha = reader.GetDateTime(4);
                        to.tipo = reader.GetString(5);

                        to.nombreCompleto = reader.GetString(6) + " " + reader.GetString(7) + " " + reader.GetString(8);
                        lista.Add(to);
                    }
                }
                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return lista;
            } catch (Exception) {
                throw;
            } finally {
                conexion.Close();
            }

        }

        /// <summary>
        /// Buscar una factura por el número de factura
        /// </summary>
        /// <param name="id">Número de factura de la factura que se busca</param>
        /// <returns>Retorna la factura que tiene el número de factura</returns>
        public TOFactura factPorId(int id) {
            try {
                TOFactura to = new TOFactura();
                String qry = "select v.COD_FACTURA, v.CEDULA,  v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2, v.ID_BODEGA from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA and v.COD_FACTURA = @num;";
                SqlCommand comm = new SqlCommand(qry, conexion);
                comm.Parameters.AddWithValue("@num", id);
                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        to.cod_Factura = (Int16)reader.GetDecimal(0);
                        to.cedula = reader.GetString(1);
                        to.id_Moneda = reader.GetString(2);
                        to.total = (Double)reader.GetDecimal(3);
                        to.fecha = reader.GetDateTime(4);
                        to.tipo = reader.GetString(5);

                        to.nombreCompleto = reader.GetString(6) + " " + reader.GetString(7) + " " + reader.GetString(8);
                        to.id_Bodega = reader.GetString(9);
                    }
                }
                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return to;
            } catch (Exception) {
                throw;
            } finally {
                conexion.Close();
            }
        }

        public int consultarSigFactura() {
            try {
                int num = 0;
                String qry = "SELECT IDENT_CURRENT ('Factura') AS ID;";
                SqlCommand comm = new SqlCommand(qry, conexion);
                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                num = Convert.ToInt32(comm.ExecuteScalar());

                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return num;
            } catch (Exception exx) {
                throw;
            } finally {
                conexion.Close();
            }
        }

        //lista filtrada por rango de fechas
        public List<TOFactura> listaPorMonto(double monto1, double monto2) {
            {
                try {
                    SqlCommand cmdFacturas = new SqlCommand("Select v.COD_FACTURA, v.CEDULA, v.ID_BODEGA, v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA  and MONTO_TOTAL >= @monto1 and MONTO_TOTAL <= @monto2;", conexion);
                    cmdFacturas.Parameters.AddWithValue("@monto1", monto1);
                    cmdFacturas.Parameters.AddWithValue("@monto2", monto2);

                    if (conexion.State != ConnectionState.Open) {
                        conexion.Open();
                    }

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmdFacturas;
                    adapter.Fill(table);
                    List<TOFactura> lista = new List<TOFactura>();


                    for (int x = 0; x < table.Rows.Count; x++) {
                        TOFactura venta = new TOFactura();
                        venta.cod_Factura = Convert.ToInt16(table.Rows[x]["COD_FACTURA"]);
                        venta.cedula = Convert.ToString(table.Rows[x]["CEDULA"]);
                        venta.id_Bodega = Convert.ToString(table.Rows[x]["ID_BODEGA"]);
                        venta.id_Moneda = Convert.ToString(table.Rows[x]["ID_MONEDA"]);
                        venta.total = Convert.ToDouble(table.Rows[x]["MONTO_TOTAL"]);
                        venta.fecha = Convert.ToDateTime(table.Rows[x]["FECHA_FACTURA"]);
                        venta.tipo = Convert.ToString(table.Rows[x]["TIPO"]);
                        venta.nombreCompleto = Convert.ToString(table.Rows[x]["NOMBRE"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO1"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO2"]);

                        lista.Add(venta);
                    }
                    if (conexion.State != ConnectionState.Closed) {
                        conexion.Close();
                    }
                    return lista;
                } catch (Exception) {
                    throw;
                } finally {
                    conexion.Close();
                }
            }
        }


        //lista filtrada por rango tipo de factura
        public List<TOFactura> listaPorTipo(string tipo) {
            try {
                SqlCommand cmdFacturas = new SqlCommand("Select v.COD_FACTURA, v.CEDULA, v.ID_BODEGA, v.ID_MONEDA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE, s.APELLIDO1, s.APELLIDO2 from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA  and v.TIPO = @tipo", conexion);
                cmdFacturas.Parameters.AddWithValue("@tipo", tipo);

                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdFacturas;
                adapter.Fill(table);
                List<TOFactura> lista = new List<TOFactura>();


                for (int x = 0; x < table.Rows.Count; x++) {
                    TOFactura venta = new TOFactura();
                    venta.cod_Factura = Convert.ToInt16(table.Rows[x]["COD_FACTURA"]);
                    venta.cedula = Convert.ToString(table.Rows[x]["CEDULA"]);
                    venta.id_Bodega = Convert.ToString(table.Rows[x]["ID_BODEGA"]);
                    venta.id_Moneda = Convert.ToString(table.Rows[x]["ID_MONEDA"]);
                    venta.total = Convert.ToDouble(table.Rows[x]["MONTO_TOTAL"]);
                    venta.fecha = Convert.ToDateTime(table.Rows[x]["FECHA_FACTURA"]);
                    venta.tipo = Convert.ToString(table.Rows[x]["TIPO"]);
                    venta.nombreCompleto = Convert.ToString(table.Rows[x]["NOMBRE"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO1"]) + " " + Convert.ToString(table.Rows[x]["APELLIDO2"]);

                    lista.Add(venta);
                }
                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return lista;
            } catch (Exception) {
                throw;
            } finally {
                conexion.Close();
            }
        }


        /// <summary>
        /// Lista los detalles de una factura
        /// </summary>
        /// <param name="idFactura">Número de factura de la factura que se desea el </param>
        /// <returns></returns>
        public List<TODetalleFactura> listaDetalle(int idFactura) {
            //try
            //{
            SqlCommand cmdDet = new SqlCommand("Select d.COD_LINEA, d.COD_FACTURA, m.NOMBRE_MATERIAL, d.MONTO_LINEA, d.KILOS, d.COD_MATERIAL from DETALLE_FACTURA d, FACTURA v, MATERIAL m where d.COD_FACTURA = @cod and d.COD_FACTURA = v.COD_FACTURA and d.COD_MATERIAL = m.COD_MATERIAL;", conexion);
            cmdDet.Parameters.AddWithValue("@cod", idFactura);

            if (conexion.State != ConnectionState.Open) {
                conexion.Open();
            }

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmdDet;
            adapter.Fill(table);
            List<TODetalleFactura> lista = new List<TODetalleFactura>();


            for (int x = 0; x < table.Rows.Count; x++) {
                TODetalleFactura detVenta = new TODetalleFactura();
                detVenta.cod_Linea = Convert.ToInt16(table.Rows[x]["COD_LINEA"]);
                detVenta.cod_Factura = Convert.ToInt16(table.Rows[x]["COD_FACTURA"]);
                detVenta.nombreMaterial = Convert.ToString(table.Rows[x]["NOMBRE_MATERIAL"]);
                detVenta.monto_Linea = Convert.ToDouble(table.Rows[x]["MONTO_LINEA"]);
                detVenta.kilos_Linea = Convert.ToDouble(table.Rows[x]["KILOS"]);
                detVenta.cod_Stock = Convert.ToString(table.Rows[x]["COD_MATERIAL"]);

                lista.Add(detVenta);
            }
            if (conexion.State != ConnectionState.Closed) {
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

        /// <summary>
        /// Método para buscar las facturas que se relacionen con una palabra o el tipo de factura
        /// </summary>
        /// <param name="busqueda">Palabra que se utiliza para buscar en el método</param>
        /// <param name="tipo">Tipo de factura que se está buscando</param>
        /// <returns></returns>
        public DataTable buscar(string busqueda, string tipo) {
            try {
                using (conexion) {
                    SqlCommand cmd = conexion.CreateCommand();
                    //cod, bod, moneda, cedula, monto, fecha, tipo, socio
                    string sql = "Select s.NOMBRE + ' ' + s.APELLIDO1 + ' ' +  s.APELLIDO2 as Socio,  v.CEDULA as 'Cédula', v.COD_FACTURA as 'Número Factura', (convert(varchar, v.FECHA_FACTURA, 103)) as 'Fecha Factura', v.MONTO_TOTAL as 'Monto Total' from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA and v.TIPO = @tipo ";

                    cmd.Parameters.AddWithValue("@tipo", tipo);

                    if (string.IsNullOrEmpty(busqueda) == false) {
                        sql += " and ((v.COD_FACTURA LIKE '%' + @pal + '%')  or (V.CEDULA LIKE '%' + @pal + '%') or (v.MONTO_TOTAL LIKE '%' + @pal + '%') or (v.FECHA_FACTURA LIKE '%' + @pal + '%') or (s.NOMBRE LIKE '%' + @pal + '%') or (s.APELLIDO1 LIKE '%' + @pal + '%') or (s.APELLIDO2 LIKE '%' + @pal + '%')) ORDER BY COD_FACTURA DESC;";

                        cmd.Parameters.AddWithValue("@pal", busqueda);
                    } else {
                        sql += " ORDER BY COD_FACTURA DESC;";
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = conexion;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            } catch (Exception) {
                throw;
            }
        }

        public int numeroRangoFecha(string tipo) {
            try {
                TOFactura to = new TOFactura();
                String qry = "SELECT COUNT(COD_FACTURA) as 'Total' from FACTURA WHERE tipo=@tipo and YEAR(FECHA_FACTURA) = YEAR(GETDATE()) and MONTH(FECHA_FACTURA) = MONTH(GETDATE());";
                SqlCommand comm = new SqlCommand(qry, conexion);
                comm.Parameters.AddWithValue("@tipo", tipo);
                Int32 numero = 0;
                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        numero = reader.GetInt32(0);
                    }
                }
                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return numero;
            } catch (Exception) {
                throw;
            } finally {
                conexion.Close();
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
        public DataTable filtrarFacturas(DateTime fechaInicio, DateTime fechaFin, int montoMaximo, int montoMinimo, List<string> materiales, string tipo) {
            //try
            //{
            using (conexion) {
                SqlCommand cmd = conexion.CreateCommand();
                string sql = "Select s.NOMBRE + ' ' + s.APELLIDO1 + ' ' +  s.APELLIDO2 as SOCIO,  v.CEDULA as 'CÉDULA', v.COD_FACTURA as 'NÚMERO FACTURA', v.FECHA_FACTURA as 'FECHA FACTURA', v.MONTO_TOTAL as 'MONTO TOTAL' from FACTURA v, SOCIO_NEGOCIO s, DETALLE_FACTURA d where v.CEDULA = s.CEDULA and v.COD_FACTURA = d.COD_FACTURA and v.TIPO = @tipo ";
                cmd.Parameters.AddWithValue("@tipo", tipo);

                if (fechaInicio != null && fechaFin != null) {
                    sql += " and FECHA_FACTURA between @fechaInicio and @fechaFin";

                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                }
                //if (string.IsNullOrEmpty(montoMinimo) == false && string.IsNullOrEmpty(montoMaximo) == false)
                if (montoMinimo != 0 && montoMaximo != 0) {
                    sql += " and v.MONTO_TOTAL between @montoMinimo and @montoMaximo";

                    cmd.Parameters.AddWithValue("@montoMinimo", montoMinimo);
                    cmd.Parameters.AddWithValue("@montoMaximo", montoMaximo);
                }
                if (materiales.Count > 0) {
                    sql += " and d.COD_MATERIAL in (";
                    foreach (String id in materiales) {
                        sql += " @id,";
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                    sql = sql.Remove(sql.Length - 1);
                    sql += " )";
                }
                cmd.CommandText = sql;
                cmd.Connection = conexion;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public DataTable buscarTodo(string busqueda, string tipo) {
            //try
            //{
            using (conexion) {
                SqlCommand cmd = conexion.CreateCommand();
                //cod, bod, moneda, cedula, monto, fecha, tipo, socio
                string sql = "Select v.COD_FACTURA, v.ID_BODEGA, v.ID_MONEDA, v.CEDULA, v.MONTO_TOTAL, v.FECHA_FACTURA, v.TIPO, s.NOMBRE + ' ' + s.APELLIDO1 + ' ' +  s.APELLIDO2 as SOCIO from FACTURA v, SOCIO_NEGOCIO s where v.CEDULA = s.CEDULA and v.TIPO = @tipo ";

                cmd.Parameters.AddWithValue("@tipo", tipo);

                if (string.IsNullOrEmpty(busqueda) == false) {
                    sql += " and ((v.COD_FACTURA LIKE '%' + @pal + '%')  or (V.CEDULA LIKE '%' + @pal + '%') or (v.MONTO_TOTAL LIKE '%' + @pal + '%') or (v.FECHA_FACTURA LIKE '%' + @pal + '%') or (s.NOMBRE LIKE '%' + @pal + '%') or (s.APELLIDO1 LIKE '%' + @pal + '%') or (s.APELLIDO2 LIKE '%' + @pal + '%'));";

                    cmd.Parameters.AddWithValue("@pal", busqueda);
                }
                cmd.CommandText = sql;
                cmd.Connection = conexion;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }


    }
}
