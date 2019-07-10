using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;

namespace DAO {
    public class DAOUnidadMedida {
        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        public DataTable buscar(string palabra) {
            try {
                using(conexion) {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "select cod_unidad as Código, nombre_unidad as 'Nombre Unidad', equivalencia_kg as Equivalencia from Unidad_Medida";
                    if(!string.IsNullOrEmpty(palabra)) {
                        sql += " WHERE (cod_unidad LIKE '%' + @pal + '%')  or (nombre_unidad LIKE '%' + @pal + '%') or (equivalencia_kg LIKE '%' + @pal + '%');";
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

        public void guardarActualizarRegular(TOUnidad unidad) {

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
                "begin tran if exists(select * from Unidad_Medida with (updlock, serializable) where cod_unidad = @cod_unidad) begin update unidad_medida set nombre_unidad = @nombre_unidad, equivalencia_kg = @equivalencia_kg where cod_unidad = @cod_unidad; end else begin insert into unidad_medida(cod_unidad, nombre_unidad, equivalencia_kg) values(@cod_unidad, @nombre_unidad, @equivalencia_kg); end commit tran";
                        //sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                        sentencia.Parameters.AddWithValue("@cod_unidad", unidad.codigo);
                        sentencia.Parameters.AddWithValue("@nombre_unidad", unidad.nombre);
                        sentencia.Parameters.AddWithValue("@equivalencia_kg", unidad.equivalencia);
                        sentencia.ExecuteNonQuery();

                        sqlTran.Commit();
                        if(conexion.State != ConnectionState.Closed) {
                            conexion.Close();
                        }
                    

                } catch(Exception) {
                    try {
                        // Attempt to roll back the transaction.
                        sqlTran.Rollback();
                    } catch(Exception) {

                        throw;
                    } finally {
                        conexion.Close();
                    }
                }
            }
        }


        public void guardarActualizarAdmin(TOUnidad unidad) {

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
            "begin tran if exists(select * from Unidad_Medida with (updlock, serializable) where cod_unidad = @cod_unidad) begin update unidad_medida set nombre_unidad = @nombre_unidad, equivalencia_kg = @equivalencia_kg, estado = @estado where cod_unidad = @cod_unidad; end else begin insert into unidad_medida(cod_unidad, nombre_unidad, equivalencia_kg, estado) values(@cod_unidad, @nombre_unidad, @equivalencia_kg, @estado); end commit tran";
                    //sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                    sentencia.Parameters.AddWithValue("@cod_unidad", unidad.codigo);
                    sentencia.Parameters.AddWithValue("@nombre_unidad", unidad.nombre);
                    sentencia.Parameters.AddWithValue("@equivalencia_kg", unidad.equivalencia);
                    sentencia.Parameters.AddWithValue("@estado", unidad.estado);
                    sentencia.ExecuteNonQuery();

                    sqlTran.Commit();
                    if(conexion.State != ConnectionState.Closed) {
                        conexion.Close();
                    }


                } catch(Exception) {
                    try {
                        // Attempt to roll back the transaction.
                        sqlTran.Rollback();
                    } catch(Exception) {

                        throw;
                    } finally {
                        conexion.Close();
                    }
                }
            }
        }

        public TOUnidad consultar(String id) {
            try {
                TOUnidad unid = new TOUnidad();
                SqlCommand buscar = new SqlCommand("SELECT cod_unidad, nombre_unidad, equivalencia_kg, estado FROM Unidad_Medida WHERE cod_unidad = @id;", conexion);
                buscar.Parameters.AddWithValue("@id", id);

                if(conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                //SqlDataReader reader = buscar.ExecuteReader();
                //if(reader.HasRows) {
                //    while(reader.Read()) {
                //        unid.codigo = reader.GetString(0);
                //        unid.nombre = reader.GetString(1);
                //        unid.equivalencia = reader.GetSqlDecimal(2);
                //        unid.estado = reader.GetBoolean(3);
                //    }
                //}

                
                //return unid;


                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = buscar;
                adapter.Fill(table);
                TOUnidad und = new TOUnidad();

                for(int x = 0; x < table.Rows.Count; x++) {
                    
                    und.codigo = Convert.ToString(table.Rows[x]["COD_UNIDAD"]);
                    und.nombre = Convert.ToString(table.Rows[x]["NOMBRE_UNIDAD"]);
                    und.estado = Convert.ToBoolean(table.Rows[x]["ESTADO"]);
                    und.equivalencia = Convert.ToDouble(table.Rows[x]["EQUIVALENCIA_KG"]);
                    
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



        


}
}
