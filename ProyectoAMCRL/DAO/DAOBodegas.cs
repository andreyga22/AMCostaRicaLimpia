using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TO;

namespace DAO {
    public class DAOBodegas {




        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        /// <summary>
        /// Permite el almacenamieto o actulización de los datos de una bodega en base de datos.
        /// (Solo sirve con un usuario administrador)
        /// </summary>
        /// <param name="bod">Bodega que se desea actualizar o guardar</param>
        public void guardarModificarBodegaAdmin(TOBodega bod) {


            using (conexion) {
                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }

                // Start a local transaction.
                SqlTransaction sqlTran = conexion.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand sentencia = conexion.CreateCommand();
                sentencia.Transaction = sqlTran;

                try {
                    if (bod.direccion.cod_direccion == 0) {
                        sentencia.CommandText =
                "insert into direccion(provincia, canton, distrito, otras_sennas) values(@prov, @cant, @dist, @otras);" +
            " SELECT SCOPE_IDENTITY();";
                        //sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                        sentencia.Parameters.AddWithValue("@prov", bod.direccion.provincia);
                        sentencia.Parameters.AddWithValue("@cant", bod.direccion.canton);
                        sentencia.Parameters.AddWithValue("@dist", bod.direccion.distrito);
                        sentencia.Parameters.AddWithValue("@otras", bod.direccion.otras_sennas);
                        int resul = 0;
                        resul = Convert.ToInt32(sentencia.ExecuteScalar());


                        sentencia.CommandText =
                     "insert into bodega(id_bodega, nombre_bod, estado_bodega, cod_direccion) values (@codigo, @nombre, @estado, @cod_dir);";
                        sentencia.Parameters.AddWithValue("@codigo", bod.codigo);
                        sentencia.Parameters.AddWithValue("@nombre", bod.nombre);
                        sentencia.Parameters.AddWithValue("@estado", bod.estado);
                        sentencia.Parameters.AddWithValue("@cod_dir", resul);
                        sentencia.ExecuteNonQuery();

                        //guardar todos los materiales en la nueva bodega.
                        List<String> lista = new List<String>();
                        sentencia.CommandText = "Select cod_material from material;";
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = sentencia;
                        adapter.Fill(table);
                        for (int x = 0; x < table.Rows.Count; x++) {

                            lista.Add(Convert.ToString(table.Rows[x]["COD_MATERIAL"]));
                        }
                        sentencia.CommandText =
                            "insert into stock(cod_material, id_bodega, kilos_stock) values(@cod_material, @id_bodega, 0);";
                        sentencia.Parameters.AddWithValue("@id_bodega", bod.codigo);
                        sentencia.Parameters.AddWithValue("@cod_material", "");

                        foreach (String item in lista) {

                            sentencia.Parameters["@cod_material"].Value = item;
                            sentencia.ExecuteNonQuery();

                        }

                        sqlTran.Commit();
                        if (conexion.State != ConnectionState.Closed) {
                            conexion.Close();
                        }

                    } else {
                        sentencia.CommandText =
               "update direccion set provincia = @prov, canton= @cant, distrito= @dist, otras_sennas= @otras where cod_Direccion = @cod;";
                        sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                        sentencia.Parameters.AddWithValue("@prov", bod.direccion.provincia);
                        sentencia.Parameters.AddWithValue("@cant", bod.direccion.canton);
                        sentencia.Parameters.AddWithValue("@dist", bod.direccion.distrito);
                        sentencia.Parameters.AddWithValue("@otras", bod.direccion.otras_sennas);
                        sentencia.ExecuteNonQuery();



                        sentencia.CommandText =
                         "update bodega set nombre_bod = @nombre, estado_bodega = @estado where id_bodega = @codigo;";
                        sentencia.Parameters.AddWithValue("@codigo", bod.codigo);
                        sentencia.Parameters.AddWithValue("@nombre", bod.nombre);
                        sentencia.Parameters.AddWithValue("@estado", bod.estado);
                        //sentencia.Parameters.AddWithValue("@cod_dir", bod.direccion.cod_direccion);


                        sentencia.ExecuteNonQuery();
                        sqlTran.Commit();
                        if (conexion.State != ConnectionState.Closed) {
                            conexion.Close();
                        }
                    }

                    //if(reader.HasRows) {
                    //    while(reader.Read()) {
                    //        resul = reader.GetInt32(0);
                    //    }
                    //}
                    //reader.Close();

                    // Execute two separate commands.

                    // Commit the transaction.

                } catch (Exception exx) {
                    try {
                        // Attempt to roll back the transaction.
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

        /// <summary>
        /// Permite el almacenamieto o actulización de los datos de una bodega en base de datos.
        /// (Solo sirve con un usuario regular)
        /// </summary>
        /// <param name="bod">Bodega que se desea actualizar o guardar</param>
        public void guardarModificarBodegaRegular(TOBodega bod) {


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
                    if(bod.direccion.cod_direccion == 0) {
                        sentencia.CommandText =
                "insert into direccion(provincia, canton, distrito, otras_sennas) values(@prov, @cant, @dist, @otras);" +
            " SELECT SCOPE_IDENTITY();";
                        //sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                        sentencia.Parameters.AddWithValue("@prov", bod.direccion.provincia);
                        sentencia.Parameters.AddWithValue("@cant", bod.direccion.canton);
                        sentencia.Parameters.AddWithValue("@dist", bod.direccion.distrito);
                        sentencia.Parameters.AddWithValue("@otras", bod.direccion.otras_sennas);
                        int resul = 0;
                        resul = Convert.ToInt32(sentencia.ExecuteScalar());


                        sentencia.CommandText =
                     "insert into bodega(id_bodega, nombre_bod, estado_bodega, cod_direccion) values (@codigo, @nombre, @estado, @cod_dir);";
                        sentencia.Parameters.AddWithValue("@codigo", bod.codigo);
                        sentencia.Parameters.AddWithValue("@nombre", bod.nombre);
                        sentencia.Parameters.AddWithValue("@estado", true);

                        sentencia.Parameters.AddWithValue("@cod_dir", resul);

                        sentencia.ExecuteNonQuery();

                        //guardar todos los materiales en la nueva bodega.
                        List<String> lista = new List<String>();
                        sentencia.CommandText = "Select cod_material from material;";
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = sentencia;
                        adapter.Fill(table);
                        for (int x = 0; x < table.Rows.Count; x++) {

                            lista.Add(Convert.ToString(table.Rows[x]["COD_MATERIAL"]));
                        }
                        sentencia.CommandText =
                            "insert into stock(cod_material, id_bodega, kilos_stock) values(@cod_material, @id_bodega, 0);";
                        sentencia.Parameters.AddWithValue("@id_bodega", bod.codigo);
                        sentencia.Parameters.AddWithValue("@cod_material", "");

                        foreach (String item in lista) {

                            sentencia.Parameters["@cod_material"].Value = item;
                            sentencia.ExecuteNonQuery();

                        }

                        sqlTran.Commit();
                        if (conexion.State != ConnectionState.Closed) {
                            conexion.Close();
                        }

                    } else {
                        sentencia.CommandText =
               "update direccion set provincia = @prov, canton= @cant, distrito= @dist, otras_sennas= @otras where cod_Direccion = @cod;";
                        sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                        sentencia.Parameters.AddWithValue("@prov", bod.direccion.provincia);
                        sentencia.Parameters.AddWithValue("@cant", bod.direccion.canton);
                        sentencia.Parameters.AddWithValue("@dist", bod.direccion.distrito);
                        sentencia.Parameters.AddWithValue("@otras", bod.direccion.otras_sennas);
                        sentencia.ExecuteNonQuery();



                        sentencia.CommandText =
                         "update bodega set nombre_bod = @nombre where id_bodega = @codigo;";
                        sentencia.Parameters.AddWithValue("@codigo", bod.codigo);
                        sentencia.Parameters.AddWithValue("@nombre", bod.nombre);
                        //sentencia.Parameters.AddWithValue("@estado", bod.estado);
                        //sentencia.Parameters.AddWithValue("@cod_dir", bod.direccion.cod_direccion);


                        sentencia.ExecuteNonQuery();
                        sqlTran.Commit();
                        if(conexion.State != ConnectionState.Closed) {
                            conexion.Close();
                        }
                    }

                    //if(reader.HasRows) {
                    //    while(reader.Read()) {
                    //        resul = reader.GetInt32(0);
                    //    }
                    //}
                    //reader.Close();

                    // Execute two separate commands.

                    // Commit the transaction.

                } catch(Exception) {
                    try {
                        // Attempt to roll back the transaction.
                        sqlTran.Rollback();
                        throw;
                    } catch(Exception) {

                        throw;
                    }
                }
            }
        }

        //public int consultarUltimaDireccion(string otras) {

        //    try {
        //        int resul = 0;

        //        string select = "select cod_direccion from direccion where otras_sennas = @otras;";
        //        SqlCommand sentencia = new SqlCommand(select, conexion);
        //        sentencia.Parameters.AddWithValue("@otras", otras);

        //        if(conexion.State != ConnectionState.Open) {
        //            conexion.Open();
        //        }

        //        SqlDataReader reader = sentencia.ExecuteReader();
        //        if(reader.HasRows) {
        //            while(reader.Read()) {
        //                resul = reader.GetInt32(0);
        //            }
        //        }

        //        if(conexion.State != ConnectionState.Closed) {
        //            conexion.Close();
        //        }
        //        return resul;
        //    } catch(SqlException) {
        //        throw;
        //    } catch(Exception) {
        //        throw;
        //    } finally {
        //        conexion.Close();
        //    }
        //}

            /// <summary>
            /// Devuelve una lista con los principales datos de las bodegas.
            /// </summary>
            /// <returns>Lista con los datos de las bodegas</returns>
        public List<TOBodegaTabla> listaBodegaUsuarioRegular() {
            try {
                string select = "select b.ID_BODEGA, b.NOMBRE_BOD, d.DISTRITO, b.ESTADO_BODEGA from bodega b join direccion d on b.COD_DIRECCION = d.COD_DIRECCION;";
                SqlCommand sentencia = new SqlCommand(select, conexion);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sentencia;
                adapter.Fill(table);
                List<TOBodegaTabla> lista = new List<TOBodegaTabla>();

                for(int x = 0; x < table.Rows.Count; x++) {
                    TOBodegaTabla bodega = new TOBodegaTabla();
                    bodega.codigo = Convert.ToString(table.Rows[x]["ID_BODEGA"]);
                    bodega.nombre = Convert.ToString(table.Rows[x]["NOMBRE_BOD"]);
                    bodega.distrito = Convert.ToString(table.Rows[x]["DISTRITO"]);
                    bodega.estado = Convert.ToBoolean(table.Rows[x]["ESTADO_BODEGA"]);
                    lista.Add(bodega);
                }

                return lista;
            } catch(SqlException) {
                throw;
            } catch(Exception) {
                throw;
            }
        }

        //public List<TOBodegaTabla> listaBodegaUsuarioAdmin() {
        //    try {
        //        string select = "select b.ID_BODEGA, b.NOMBRE_BOD, d.DISTRITO, b.ESTADO_BODEGA from bodega b join direccion d on b.COD_DIRECCION = d.COD_DIRECCION;";
        //        SqlCommand sentencia = new SqlCommand(select, conexion);
        //        DataTable table = new DataTable();
        //        SqlDataAdapter adapter = new SqlDataAdapter();
        //        adapter.SelectCommand = sentencia;
        //        adapter.Fill(table);
        //        List<TOBodegaTabla> lista = new List<TOBodegaTabla>();

        //        for(int x = 0; x < table.Rows.Count; x++) {
        //            TOBodegaTabla bodega = new TOBodegaTabla();
        //            bodega.codigo = Convert.ToString(table.Rows[x]["ID_BODEGA"]);
        //            bodega.nombre = Convert.ToString(table.Rows[x]["NOMBRE_BOD"]);
        //            bodega.distrito = Convert.ToString(table.Rows[x]["DISTRITO"]);
        //            bodega.estado = Convert.ToBoolean(table.Rows[x]["ESTADO_BODEGA"]);
        //            lista.Add(bodega);
        //        }

        //        return lista;
        //    } catch(SqlException) {
        //        throw;
        //    } catch(Exception) {
        //        throw;
        //    }
        //}

        /// <summary>
        /// Permite el filtro de los datos por medio de una palabra clave.
        /// (Solo sirve para usuario regular)
        /// </summary>
        /// <param name="busqueda">Palabra clave para la busqueda</param>
        /// <returns>Datatable con el resultado de la busqueda</returns>
        public DataTable buscarUsuarioRegular(string busqueda) {
            try {
                using(conexion) {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "select b.ID_BODEGA as 'Código Bodega', b.NOMBRE_BOD as 'Nombre Bodega', d.DISTRITO as Ubicación from bodega b join direccion d on b.COD_DIRECCION = d.COD_DIRECCION where b.ESTADO_BODEGA = 1";
                    if(!string.IsNullOrEmpty(busqueda)) {
                        sql += " and ((b.ID_BODEGA LIKE '%' + @pal + '%')  or (b.NOMBRE_BOD LIKE '%' + @pal + '%') or (d.DISTRITO LIKE '%' + @pal + '%'));";
                        cmd.Parameters.AddWithValue("@pal", busqueda);
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
            }
        }

        /// <summary>
        /// Permite el filtro de los datos por medio de una palabra clave.
        /// (Solo sirve para usuario administrador)
        /// </summary>
        /// <param name="busqueda">Palabra clave</param>
        /// <returns>Datatable con el resultado de la busqueda</returns>
        public DataTable buscarUsuarioAdmin(string busqueda) {
            try {
                using(conexion) {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "select b.ID_BODEGA as 'Código Bodega', b.NOMBRE_BOD as 'Nombre Bodega', d.DISTRITO as 'Ubicación', b.ESTADO_BODEGa as 'Estado' from bodega b join direccion d on b.COD_DIRECCION = d.COD_DIRECCION";
                    if(!string.IsNullOrEmpty(busqueda)) {
                        sql += " WHERE (b.ID_BODEGA LIKE '%' + @pal + '%')  or (b.NOMBRE_BOD LIKE '%' + @pal + '%') or (d.DISTRITO LIKE '%' + @pal + '%');";
                        cmd.Parameters.AddWithValue("@pal", busqueda);
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
            }
        }

        /// <summary>
        /// Permite la obtención de los datos de una bodega por medio de su identificador.
        /// Solo sirve para usuario administrador
        /// </summary>
        /// <param name="idBodega">Identificador de la bodega</param>
        /// <returns>TOBodega con el resultado de la consulta</returns>
        public TOBodega consultarBodegaAdmin(String idBodega) {

            TOBodega Bodega = new TOBodega();
            TODireccion dir = new TODireccion();
            SqlCommand buscar = new SqlCommand("SELECT b.id_bodega, b.nombre_bod, b.estado_bodega, b.cod_direccion, d.cod_direccion, d.provincia, d.canton, d.distrito, d.otras_sennas FROM Bodega b join direccion d on b.cod_direccion = d.cod_direccion WHERE id_bodega = @id;", conexion);
            buscar.Parameters.AddWithValue("@id", idBodega);

            if(conexion.State != ConnectionState.Open) {
                conexion.Open();
            }

            SqlDataReader reader = buscar.ExecuteReader();
            if(reader.HasRows) {
                while(reader.Read()) {
                    Bodega.codigo = reader.GetString(0);
                    Bodega.nombre = reader.GetString(1);
                    Bodega.estado = reader.GetBoolean(2);
                    dir.cod_direccion = reader.GetInt32(3);
                    dir.cod_direccion = reader.GetInt32(4);
                    dir.provincia = reader.GetString(5);
                    dir.canton = reader.GetString(6);
                    dir.distrito = reader.GetString(7);
                    dir.otras_sennas = reader.GetString(8);
                    Bodega.direccion = dir;
                }
            }

            if(conexion.State != ConnectionState.Closed) {
                conexion.Close();
            }
            return Bodega;
        }

        /// <summary>
        ///  Permite la obtención de los datos de una bodega por medio de su identificador.
        /// Solo sirve para usuario administrador
        /// </summary>
        /// <param name="idBodega">Identificador de la bodega</param>
        /// <returns>TOBodega con el resultado de la consulta</returns>
        public TOBodega consultarBodegaRegular(String idBodega) {

            TOBodega Bodega = new TOBodega();
            TODireccion dir = new TODireccion();
            SqlCommand buscar = new SqlCommand("SELECT b.id_bodega, b.nombre_bod, b.cod_direccion, d.cod_direccion, d.provincia, d.canton, d.distrito, d.otras_sennas FROM Bodega b join direccion d on b.cod_direccion = d.cod_direccion WHERE (id_bodega = @id) and b.estado_bodega = 1;", conexion);
            buscar.Parameters.AddWithValue("@id", idBodega);

            if(conexion.State != ConnectionState.Open) {
                conexion.Open();
            }

            SqlDataReader reader = buscar.ExecuteReader();
            if(reader.HasRows) {
                while(reader.Read()) {
                    Bodega.codigo = reader.GetString(0);
                    Bodega.nombre = reader.GetString(1);
                    dir.cod_direccion = reader.GetInt32(2);
                    dir.cod_direccion = reader.GetInt32(3);
                    dir.provincia = reader.GetString(4);
                    dir.canton = reader.GetString(5);
                    dir.distrito = reader.GetString(6);
                    dir.otras_sennas = reader.GetString(7);
                    Bodega.direccion = dir;
                }
            }

            if(conexion.State != ConnectionState.Closed) {
                conexion.Close();
            }
            return Bodega;
        }
    }
}
