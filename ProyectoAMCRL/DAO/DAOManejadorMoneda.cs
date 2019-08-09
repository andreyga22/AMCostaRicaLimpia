using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAOManejadorMoneda
    {

        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        public TOMoneda buscarMonedaId(string idMoneda)
        {
            try
            {
                TOMoneda to = new TOMoneda();
                String qry = "select * from moneda where id_Moneda = @id";
                SqlCommand comm = new SqlCommand(qry, conexion);
                comm.Parameters.AddWithValue("@id", idMoneda);
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        to.idMoneda = reader.GetString(0);
                        to.detalleMoneda = reader.GetString(1);
                        to.equivalencia_Colon = (Double)reader.GetDecimal(2);
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

        public DataSet listarMonedasDAO()
        {
            List<TOMoneda> monedas = new List<TOMoneda>();

            String sql = "SELECT * FROM MONEDA";
            SqlCommand cmd = new SqlCommand(sql, conexion);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("ajustes");
            sda.Fill(ds);

            return ds;
        }

        public TOMoneda consultarAdmin(String id) {
            try {
                TOMoneda mon = new TOMoneda();
                SqlCommand buscar = new SqlCommand("SELECT id_moneda, detalle_moneda, equivalencia_colon, estado FROM Moneda WHERE id_moneda = @id;", conexion);
                buscar.Parameters.AddWithValue("@id", id);

                if(conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = buscar;
                adapter.Fill(table);
                TOMoneda und = new TOMoneda();

                for(int x = 0; x < table.Rows.Count; x++) {

                    und.idMoneda = Convert.ToString(table.Rows[x]["ID_MONEDA"]);
                    und.detalleMoneda = Convert.ToString(table.Rows[x]["DETALLE_MONEDA"]);
                    und.equivalencia_Colon = Convert.ToDouble(table.Rows[x]["EQUIVALENCIA_COLON"]);
                    und.estado = Convert.ToBoolean(table.Rows[x]["Estado"]);

                }

                if(conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }

                return und;

            } catch(Exception) {
                throw;
            } finally {
                conexion.Close();
            }

        }


        public TOMoneda consultarRegular(String id) {
            try {
                TOMoneda mon = new TOMoneda();
                SqlCommand buscar = new SqlCommand("SELECT id_moneda, detalle_moneda, equivalencia_colon FROM Moneda WHERE id_moneda = @id;", conexion);
                buscar.Parameters.AddWithValue("@id", id);

                if(conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = buscar;
                adapter.Fill(table);
                TOMoneda und = new TOMoneda();

                for(int x = 0; x < table.Rows.Count; x++) {

                    und.idMoneda = Convert.ToString(table.Rows[x]["ID_MONEDA"]);
                    und.detalleMoneda = Convert.ToString(table.Rows[x]["DETALLE_MONEDA"]);
                    und.equivalencia_Colon = Convert.ToDouble(table.Rows[x]["EQUIVALENCIA_COLON"]);

                }

                if(conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }

                return und;

            } catch(Exception) {
                throw;
            } finally {
                conexion.Close();
            }

        }


        public DataTable buscarAdmin(string palabra) {
            try {
                using(conexion) {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "select id_moneda as Código, detalle_moneda as 'Detalle', equivalencia_colon as 'Equivalencia Colón', Estado as 'Estado' from Moneda";
                    if(!string.IsNullOrEmpty(palabra)) {
                        sql += " WHERE (id_moneda LIKE '%' + @pal + '%')  or (detalle_moneda LIKE '%' + @pal + '%') or (equivalencia_colon LIKE '%' + @pal + '%');";
                        cmd.Parameters.AddWithValue("@pal", palabra);
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = conexion;
                    using(SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        return dt;
                    }
                }
            } catch(Exception) {
                throw;
            } finally {
                conexion.Close();
            }
        }

        public DataTable buscarRegular(string palabra) {
            try {
                using(conexion) {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "select id_moneda as Código, detalle_moneda as 'Detalle', equivalencia_colon as 'Equivalencia Colón' from Moneda where estado = 1";
                    if(!string.IsNullOrEmpty(palabra)) {
                        sql += " and ((id_moneda LIKE '%' + @pal + '%')  or (detalle_moneda LIKE '%' + @pal + '%') or (equivalencia_colon LIKE '%' + @pal + '%'));";
                        cmd.Parameters.AddWithValue("@pal", palabra);
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = conexion;
                    using(SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        return dt;
                    }
                }
            } catch(Exception) {
                throw;
            } finally {
                conexion.Close();
            }
        }

        public void guardarActualizarRegular(TOMoneda mon) {

            using(conexion) {
                if(conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                // Start a local transaction.
                SqlTransaction sqlTran = conexion.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand sentencia = conexion.CreateCommand();
                sentencia.Transaction = sqlTran;

                try {
                    sentencia.CommandText =
            "begin tran if exists(select * from moneda with (updlock, serializable) where id_Moneda = @id_moneda) begin update unidad_medida set detalle_moneda = @detalle_moneda, equivalencia_colon = @equivalencia_colon where id_moneda = @id_moneda; end else begin insert into moneda(id_moneda, detalle_moneda, equivalencia_colon, estado) values(@id_moneda, @detalle_moneda, @equivalencia_colon, true); end commit tran";
                    //sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                    sentencia.Parameters.AddWithValue("@id_moneda", mon.idMoneda);
                    sentencia.Parameters.AddWithValue("@detalle_moneda", mon.detalleMoneda);
                    sentencia.Parameters.AddWithValue("@equivalencia_colon", mon.equivalencia_Colon);
                    sentencia.ExecuteNonQuery();

                    sqlTran.Commit();
                    if(conexion.State != ConnectionState.Closed) {
                        conexion.Close();
                    }


                } catch(Exception) {
                    try {
                        // Attempt to roll back the transaction.
                        sqlTran.Rollback();
                        throw;
                    } catch(Exception) {

                        throw;
                    } finally {
                        conexion.Close();
                    }
                }
            }
        }


        public void guardarActualizarAdmin(TOMoneda mon) {

            using(conexion) {
                if(conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                // Start a local transaction.
                SqlTransaction sqlTran = conexion.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand sentencia = conexion.CreateCommand();
                sentencia.Transaction = sqlTran;

                try {
                    sentencia.CommandText =
            "begin tran if exists(select * from moneda with (updlock, serializable) where id_moneda = @id_moneda) begin update moneda set detalle_moneda = @detalle_moneda, equivalencia_colon = @equivalencia_colon, estado = @estado where id_moneda = @id_moneda; end else begin insert into moneda(id_moneda, detalle_moneda, equivalencia_colon, estado) values(@id_moneda, @detalle_moneda, @equivalencia_colon, @estado); end commit tran";
                    //sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                    sentencia.Parameters.AddWithValue("@id_moneda", mon.idMoneda);
                    sentencia.Parameters.AddWithValue("@detalle_moneda", mon.detalleMoneda);
                    sentencia.Parameters.AddWithValue("@equivalencia_colon", mon.equivalencia_Colon);
                    sentencia.Parameters.AddWithValue("@estado", mon.estado);
                    sentencia.ExecuteNonQuery();

                    sqlTran.Commit();
                    if(conexion.State != ConnectionState.Closed) {
                        conexion.Close();
                    }


                } catch(Exception) {
                    try {
                        // Attempt to roll back the transaction.
                        sqlTran.Rollback();
                        throw;
                    } catch(Exception) {

                        throw;
                    } finally {
                        conexion.Close();
                    }
                }
            }
        }


    }
}

