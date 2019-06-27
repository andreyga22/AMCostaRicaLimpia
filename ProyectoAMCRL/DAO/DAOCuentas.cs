using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TO;
namespace DAO {
    public class DAOCuentas {


        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);


        public void guardarCuenta(TOCuenta cuenta) {


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
                "insert into credenciales(id_usuario, clave, rol, estado, nombre_usuario) values(@id_usuario, @clave, @rol, @estado, @nombre_usuario);";
                    sentencia.Parameters.AddWithValue("@id_usuario", cuenta.id_usuario);
                    sentencia.Parameters.AddWithValue("@clave", cuenta.clave);
                    sentencia.Parameters.AddWithValue("@rol", cuenta.rol);
                    sentencia.Parameters.AddWithValue("@estado", cuenta.estado);
                    sentencia.Parameters.AddWithValue("@nombre_usuario", cuenta.nombre_usuario);
                    sentencia.ExecuteNonQuery();

                    // Commit the transaction.
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
                }
                throw;
                }
            }
        }

        public void modificarCuenta(TOCuenta cuenta) {


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
                "update credenciales set rol= @rol, estado= @estado, nombre_usuario= @nombre_usuario where id_usuario = @id_usuario;";
                    sentencia.Parameters.AddWithValue("@id_usuario", cuenta.id_usuario);
                    string rol = "";
                    if(cuenta.rol.Equals("a")) {
                        rol = "a";
                    } else {
                        rol = "r";
                    }
                    sentencia.Parameters.AddWithValue("@rol", rol);
                    sentencia.Parameters.AddWithValue("@estado", cuenta.estado);
                    sentencia.Parameters.AddWithValue("@nombre_usuario", cuenta.nombre_usuario);
                    sentencia.ExecuteNonQuery();

                    // Commit the transaction.
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
                    }
                    throw;
                }
            }
        }

        public void modificarContrasena(string id, string vieja, string nueva) {


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
                "update credenciales set clave= @nueva where (id_usuario = @id_usuario) and (clave = @clave);";
                    sentencia.Parameters.AddWithValue("@id_usuario", id);
                    sentencia.Parameters.AddWithValue("@clave", vieja);
                    sentencia.Parameters.AddWithValue("@nueva", nueva);
                    sentencia.ExecuteNonQuery();

                    // Commit the transaction.
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
                    }
                    throw;
                }
            }
        }


        public void restaurarContra(string id, string nueva) {


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
                "update credenciales set clave= @nueva where id_usuario = @id_usuario;";
                    sentencia.Parameters.AddWithValue("@id_usuario", id);
                    sentencia.Parameters.AddWithValue("@nueva", nueva);
                    sentencia.ExecuteNonQuery();

                    // Commit the transaction.
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
                    }
                    throw;
                }
            }
        }


        public DataTable buscar(string busqueda) {
            try {
                using(conexion) {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "select id_usuario as Identificador, nombre_usuario as 'Nombre Usuario', rol as Rol, estado as Estado from credenciales";
                    if(!string.IsNullOrEmpty(busqueda)) {
                        sql += " WHERE (id_usuario LIKE '%' + @pal + '%')  or (rol LIKE '%' + @pal + '%') or (estado LIKE '%' + @pal + '%') or (nombre_usuario LIKE '%' + @pal + '%');";
                        cmd.Parameters.AddWithValue("@pal", busqueda);
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = conexion;
                    using(SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        foreach(DataRow dr in dt.Rows) // search whole table
                        {
                            if(dr["rol"].Equals("a")) // if id==2
                            {
                                dr["rol"] = "Administrador"; //change the name
                                                             //break; break or not depending on you
                            } else {
                                if(dr["rol"].Equals("r")) {
                                    dr["rol"] = "Regular";
                                }
                            }
                        }

                        return dt;
                    }
                }
            } catch(Exception) {
                throw;
            }
        }

        public TOCuenta consultarCuenta(String idCuenta) {
            try {
                TOCuenta cuenta = new TOCuenta();
                SqlCommand buscar = new SqlCommand("SELECT id_usuario, nombre_usuario, rol, estado FROM credenciales WHERE id_usuario = @id;", conexion);
                buscar.Parameters.AddWithValue("@id", idCuenta);

                if(conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                SqlDataReader reader = buscar.ExecuteReader();
                if(reader.HasRows) {
                    while(reader.Read()) {
                        cuenta.id_usuario = reader.GetString(0);
                        cuenta.nombre_usuario = reader.GetString(1);
                        cuenta.rol = reader.GetString(2);
                        cuenta.estado = reader.GetBoolean(3);
                    }
                }

                if(conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return cuenta;
            } catch(Exception) {
                throw;
            }
        }

        public TOCuenta login(String idCuenta, string contra) {
            try {
                TOCuenta cuenta = null;
                SqlCommand buscar = new SqlCommand("SELECT id_usuario, nombre_usuario, rol, estado FROM credenciales WHERE (id_usuario = @id) and (clave = @contra);", conexion);
                buscar.Parameters.AddWithValue("@id", idCuenta);
                buscar.Parameters.AddWithValue("@contra", contra);

                if(conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                SqlDataReader reader = buscar.ExecuteReader();
                if(reader.HasRows) {
                    cuenta = new TOCuenta();
                    while(reader.Read()) {
                        cuenta.id_usuario = reader.GetString(0);
                        cuenta.nombre_usuario = reader.GetString(1);
                        cuenta.rol = reader.GetString(2);
                        cuenta.estado = reader.GetBoolean(3);
                    }
                }

                if(conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return cuenta;
            } catch(Exception) {
                throw;
            }
        }

        public Boolean consultarContra(String idCuenta, string contra) {
            try {
                String id = "";
                Boolean exists = false;
                SqlCommand buscar = new SqlCommand("SELECT id_usuario FROM credenciales WHERE (id_usuario = @id) and (clave = @clave);", conexion);
                buscar.Parameters.AddWithValue("@id", idCuenta);
                buscar.Parameters.AddWithValue("@clave", contra);

                if(conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                SqlDataReader reader = buscar.ExecuteReader();
                if(reader.HasRows) {
                    while(reader.Read()) {
                        id = reader.GetString(0);
                    }
                }

                if(!string.IsNullOrEmpty(id) && (!id.Equals(""))) {
                    exists = true;
                }

                if(conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return exists;
            } catch(Exception) {
                throw;
            }
        }

    }
}
