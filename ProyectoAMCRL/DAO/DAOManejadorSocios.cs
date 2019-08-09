using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TO;

namespace DAO
{
    public class DAOManejadorSocios
    {


        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        //AMCRLEntities context = new AMCRLEntities();


        /// <summary>
        /// Método que guarda en base de datos o modifica los detalles de un socio. 
        /// </summary>
        /// <param name="soc">Socio a guardar o modificar</param>
        public void guardarModificarSocio(TOSocioNegocio soc)
        {
            SqlConnection conexion2 = new SqlConnection(Properties.Settings.Default.conexionHost);
            using (conexion2)
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion2.Open();
                }

                // Start a local transaction.
                SqlTransaction sqlTran = conexion2.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand sentencia = conexion2.CreateCommand();
                sentencia.Transaction = sqlTran;

                try
                {
                    if (soc.direccion.cod_direccion == 0)
                    {
                        sentencia.CommandText =
                "insert into direccion(provincia, canton, distrito, otras_sennas) values(@prov, @cant, @dist, @otras);" +
            " SELECT SCOPE_IDENTITY();";
                        //sentencia.Parameters.AddWithValue("@cod", bod.direccion.cod_direccion);
                        sentencia.Parameters.AddWithValue("@prov", soc.direccion.provincia);
                        sentencia.Parameters.AddWithValue("@cant", soc.direccion.canton);
                        sentencia.Parameters.AddWithValue("@dist", soc.direccion.distrito);
                        sentencia.Parameters.AddWithValue("@otras", soc.direccion.otras_sennas);
                        int resul = 0;
                        resul = Convert.ToInt32(sentencia.ExecuteScalar());


                        sentencia.CommandText =
                     "insert into Socio_Negocio (cedula, nombre, rol_socio, apellido1, apellido2, estado_socio, cod_direccion, fecha_ingreso) values (@cedula, @nombre, @rol, @apellido1, @apellido2, @estado, @cod_dir, @fecha);";
                        sentencia.Parameters.AddWithValue("@cedula", soc.cedula);
                        sentencia.Parameters.AddWithValue("@nombre", soc.nombre);
                        sentencia.Parameters.AddWithValue("@rol", soc.rol);
                        sentencia.Parameters.AddWithValue("@apellido1", soc.apellido1);
                        sentencia.Parameters.AddWithValue("@apellido2", soc.apellido2);
                        sentencia.Parameters.AddWithValue("@estado", soc.estado_socio);
                        sentencia.Parameters.AddWithValue("@cod_dir", resul);
                        sentencia.Parameters.AddWithValue("@fecha", DateTime.Now);


                        sentencia.ExecuteNonQuery();

                        sentencia.CommandText =
                     "insert into Contactos (cedula, telefono_hab, telefono_pers, email) values (@cedula, @telefono_hab, @telefono_pers, @email);";
                        sentencia.Parameters.AddWithValue("@telefono_hab", soc.contactos.telefono_hab);
                        sentencia.Parameters.AddWithValue("@telefono_pers", soc.contactos.telefono_pers);
                        sentencia.Parameters.AddWithValue("@email", soc.contactos.email);


                        sentencia.ExecuteNonQuery();

                        sqlTran.Commit();
                        if (conexion2.State != ConnectionState.Closed)
                        {
                            conexion2.Close();
                        }
                    }
                    else
                    {
                        sentencia.CommandText =
               "update direccion set provincia = @prov, canton= @cant, distrito= @dist, otras_sennas= @otras where cod_Direccion = @cod;";
                        sentencia.Parameters.AddWithValue("@cod", soc.direccion.cod_direccion);
                        sentencia.Parameters.AddWithValue("@prov", soc.direccion.provincia);
                        sentencia.Parameters.AddWithValue("@cant", soc.direccion.canton);
                        sentencia.Parameters.AddWithValue("@dist", soc.direccion.distrito);
                        sentencia.Parameters.AddWithValue("@otras", soc.direccion.otras_sennas);
                        sentencia.ExecuteNonQuery();



                        sentencia.CommandText =
                         "update socio_negocio set nombre = @nombre, rol_socio = @rol, apellido1 = @apellido1, apellido2 = @apellido2, " +
                         "estado_socio = @estado where cedula = @cedula;";
                        sentencia.Parameters.AddWithValue("@cedula", soc.cedula);
                        sentencia.Parameters.AddWithValue("@nombre", soc.nombre);
                        sentencia.Parameters.AddWithValue("@rol", soc.rol);
                        sentencia.Parameters.AddWithValue("@apellido1", soc.apellido1);
                        sentencia.Parameters.AddWithValue("@apellido2", soc.apellido2);
                        sentencia.Parameters.AddWithValue("@estado", soc.estado_socio);
                        sentencia.ExecuteNonQuery();


                        sentencia.CommandText =
                         "update contactos set  telefono_hab = @telefono_hab, telefono_pers = @telefono_pers, email = @email where cedula = @cedula;";
                        sentencia.Parameters.AddWithValue("@telefono_hab", soc.contactos.telefono_hab);
                        sentencia.Parameters.AddWithValue("@telefono_pers", soc.contactos.telefono_pers);
                        sentencia.Parameters.AddWithValue("@email", soc.contactos.email);


                        sentencia.ExecuteNonQuery();
                        sqlTran.Commit();
                        //if (conexion.State != ConnectionState.Closed)
                        //{
                        conexion2.Close();
                        //}
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        // Attempt to roll back the transaction.
                        sqlTran.Rollback();
                        throw;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        conexion2.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Método que crea una tabla con los socios que cumplan con un criterio especificado si estan activos y sin su estado.
        /// </summary>
        /// <param name="busqueda">Criterio de filtrado</param>
        /// <returns>Tabla con socios</returns>
        public DataTable buscarTablaRegular(string busqueda)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "Select CEDULA as Cédula, NOMBRE as Nombre, APELLIDO1 as 'Primer apellido'," +
                    " APELLIDO2 as 'Segundo apellido', ROL_SOCIO as Rol from SOCIO_NEGOCIO where ESTADO_SOCIO = 1";

                    if (string.IsNullOrEmpty(busqueda) == false)
                    {
                        sql += " AND ((CEDULA LIKE '%' + @pal + '%') or (NOMBRE LIKE '%' + @pal + '%')  or (APELLIDO1 LIKE '%' + @pal + '%') or (APELLIDO2 LIKE '%' + @pal + '%'));";

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
            finally
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Método que crea una tabla con los socios que cumplan con un criterio especificado con su estado.
        /// </summary>
        /// <param name="busqueda">Criterio de filtrado</param>
        /// <returns>Tabla con socios</returns>
        public DataTable buscarTablaAdmin(string busqueda)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "Select CEDULA as Cédula, NOMBRE as Nombre, APELLIDO1 as 'Primer apellido'," +
                    " APELLIDO2 as 'Segundo apellido', ROL_SOCIO as Rol, ESTADO_SOCIO as Estado from SOCIO_NEGOCIO";

                    if (string.IsNullOrEmpty(busqueda) == false)
                    {
                        sql += " where ((CEDULA LIKE '%' + @pal + '%') or (NOMBRE LIKE '%' + @pal + '%')  or (APELLIDO1 LIKE '%' + @pal + '%') or (APELLIDO2 LIKE '%' + @pal + '%'));";

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
            finally
            {
                conexion.Close();
            }
        }

        public DataTable buscarTablaIzquierda(string busqueda, string id)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "Select CEDULA as Cédula, NOMBRE + ' ' +  APELLIDO1 + ' ' + APELLIDO2 as Nombre from SOCIO_NEGOCIO where ESTADO_SOCIO = 1";

                    if (string.IsNullOrEmpty(busqueda) == false)
                    {
                        sql += " AND ((CEDULA LIKE '%' + @pal + '%') or (NOMBRE LIKE '%' + @pal + '%')  or (APELLIDO1 LIKE '%' + @pal + '%') or (APELLIDO2 LIKE '%' + @pal + '%'));";

                        cmd.Parameters.AddWithValue("@pal", busqueda);
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = conexion;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        int filas = dt.Rows.Count;
                        for (int i = 0; i < filas; i++)
                        {
                            string s = Convert.ToString(dt.Rows[i]["Cédula"]);
                            if ((Convert.ToString(dt.Rows[i]["Cédula"])).Equals(id))
                            {
                                dt.Rows.Remove(dt.Rows[i]);
                                break;
                            }
                        }
                        return dt;
                    }
                }
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

        public DataTable buscarTablaDerecha(string socio)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "select Cedula as Cédula, (nombre + ' ' + apellido1 + ' ' + apellido2 + ' ') as Nombre from SOCIO_NEGOCIO where cedula in (select asociado from Asociaciones where socio = @pal);";


                    cmd.Parameters.AddWithValue("@pal", socio);

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
            finally
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Método para buscar el código de dirección de un socio en la base de datos.
        /// </summary>
        /// <param name="cedula">cédula del socio</param>
        /// <returns>código de dirección asociado</returns>
        public int buscarCodDireccion(string cedula)
        {
            try
            {
                int direccion = 0;
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "Select cod_direccion from SOCIO_NEGOCIO where cedula = @cedula ";
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    cmd.CommandText = sql;
                    cmd.Connection = conexion;
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            direccion = reader.GetInt32(0);
                        }
                        reader.Close();
                    }
                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }
                    return direccion;
                }
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
        /// Método que busca a un socio en específico por medio de su número de cédula.
        /// </summary>
        /// <param name="cedula">Cédula de identidad del socio</param>
        /// <returns>Socio de Negocio</returns>

        public TOSocioNegocio buscarSocioCedula(String cedula)
        {
            int codigo = 0;
            try
            {
                using (conexion)
                {
                    SqlCommand buscar = conexion.CreateCommand();
                    String texto = "Select cedula, nombre, apellido1, apellido2, rol_socio, estado_socio, cod_direccion from SOCIO_NEGOCIO where CEDULA = @CEDULA";
                    buscar.CommandText = texto;
                    if (!string.IsNullOrEmpty(cedula))
                    {
                        buscar.Parameters.AddWithValue("@CEDULA", cedula);
                        TOSocioNegocio socio = new TOSocioNegocio();
                        TODireccion dir = new TODireccion();

                        if (conexion.State != ConnectionState.Open)
                        {
                            conexion.Open();
                        }

                        SqlDataReader reader = buscar.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                socio.cedula = reader.GetString(0);
                                socio.nombre = reader.GetString(1);
                                socio.apellido1 = reader.GetString(2);
                                socio.apellido2 = reader.GetString(3);
                                socio.rol = reader.GetString(4);
                                socio.estado_socio = reader.GetBoolean(5);
                                codigo = reader.GetInt32(6);
                            }
                            reader.Close();
                        }
                        socio.direccion = this.buscarDireccion(codigo);
                        socio.contactos = this.buscarContacto(socio.cedula);
                        if (conexion.State != ConnectionState.Closed)
                        {
                            conexion.Close();
                        }
                        return socio;
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return null;
        }

        /// <summary>
        /// Método que busca una dirección asociada a un socio en específico.
        /// </summary>
        /// <param name="codigo">codigo de la dirección</param>
        /// <returns>Dirección de un socio</returns>
        public TODireccion buscarDireccion(int codigo)
        {
            try
            {
                SqlConnection conexion2 = new SqlConnection(Properties.Settings.Default.conexionHost);
                SqlCommand buscar = new SqlCommand("Select PROVINCIA, CANTON, DISTRITO, OTRAS_SENNAS from DIRECCION where COD_DIRECCION = @CODIGO;", conexion2);
                buscar.Parameters.AddWithValue("@CODIGO", codigo);
                TODireccion direccion = new TODireccion();
                if (conexion2.State != ConnectionState.Open)
                {
                    conexion2.Open();
                }

                SqlDataReader reader = buscar.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        direccion.provincia = reader.GetString(0);
                        direccion.canton = reader.GetString(1);
                        direccion.distrito = reader.GetString(2);
                        direccion.otras_sennas = reader.GetString(3);
                    }
                    if (conexion2.State != ConnectionState.Closed)
                    {
                        conexion2.Close();
                    }
                    return direccion;
                }
                if (conexion2.State != ConnectionState.Closed)
                {
                    conexion2.Close();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Método que busca el contacto de un socio en específico.
        /// </summary>
        /// <param name="cedula">Cédula de un socio</param>
        /// <returns>Contactos del socio</returns>
        public TOContactos buscarContacto(String cedula)
        {
            try
            {
                SqlConnection conexion2 = new SqlConnection(Properties.Settings.Default.conexionHost);
                SqlCommand buscarContacto = new SqlCommand("Select TELEFONO_HAB, TELEFONO_PERS, EMAIL from CONTACTOS where CEDULA = @CEDULA", conexion2);
                buscarContacto.Parameters.AddWithValue("@CEDULA", cedula);
                TOContactos contacto = new TOContactos();
                if (conexion2.State != ConnectionState.Open)
                {
                    conexion2.Open();
                }

                SqlDataReader reader = buscarContacto.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        contacto.telefono_hab = Convert.ToInt32(reader.GetDecimal(0));
                        contacto.telefono_pers = Convert.ToInt32(reader.GetDecimal(1));
                        contacto.email = reader.GetString(2);

                    }
                    if (conexion2.State != ConnectionState.Closed)
                    {
                        conexion2.Close();
                    }
                    return contacto;
                }
                if (conexion2.State != ConnectionState.Closed)
                {
                    conexion2.Close();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Método para agregar una asociacion entre dos socios del negocio.
        /// </summary>
        /// <param name="cedulaAsociado">Cédula del socio de negocio que se encuentra asociado</param>
        /// <param name="cedulaSocio">Cédula de quien le es asociado otro socio de negocio</param>
        /// <returns></returns>
        public void asociarSocio(String cedulaAsociado, String cedulaSocio)
        {
            try
            {
                SqlCommand asociar = new SqlCommand("Insert into Asociaciones (SOCIO, ASOCIADO) values (@SOCIO, @ASOCIADO)", conexion);
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                asociar.Parameters.AddWithValue("@SOCIO", cedulaSocio);
                asociar.Parameters.AddWithValue("@ASOCIADO", cedulaAsociado);

                asociar.ExecuteNonQuery();
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void desasociarSocio(String cedulaAsociado, String cedulaSocio)
        {
            try
            {
                SqlCommand asociar = new SqlCommand("Delete from Asociaciones where Socio = @socio AND Asociado = @ASOCIADO", conexion);
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                asociar.Parameters.AddWithValue("@SOCIO", cedulaSocio);
                asociar.Parameters.AddWithValue("@ASOCIADO", cedulaAsociado);

                asociar.ExecuteNonQuery();
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }


        public TOSocioNegocio buscarSocio(string id, String tipoSocio)
        {
            try
            {
                TOSocioNegocio socioTO = new TOSocioNegocio(); ;

                String sql = "SELECT SOCIO_NEGOCIO.CEDULA, SOCIO_NEGOCIO.NOMBRE, SOCIO_NEGOCIO.APELLIDO1, SOCIO_NEGOCIO.APELLIDO2, SOCIO_NEGOCIO.ROL_SOCIO, SOCIO_NEGOCIO.ESTADO_SOCIO," +
                "CONTACTOS.TELEFONO_HAB, CONTACTOS.TELEFONO_PERS, CONTACTOS.EMAIL," +
                "DIRECCION.PROVINCIA, DIRECCION.CANTON, DIRECCION.DISTRITO, DIRECCION.OTRAS_SENNAS, DIRECCION.COD_DIRECCION " +
                "FROM SOCIO_NEGOCIO, CONTACTOS, DIRECCION " +
                "where SOCIO_NEGOCIO.CEDULA = CONTACTOS.CEDULA and SOCIO_NEGOCIO.COD_DIRECCION = DIRECCION.COD_DIRECCION and SOCIO_NEGOCIO.CEDULA = @ID and SOCIO_NEGOCIO.ROL_SOCIO = @ROL";


                using (conexion)
                {

                    SqlCommand cmd = new SqlCommand(sql, conexion);

                    try
                    {

                        conexion.Open();
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@ROL", tipoSocio);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (!reader.HasRows)
                            return null;

                        while (reader.Read())
                        {
                            socioTO.cedula = (String)reader.GetSqlString(0).ToString();
                            socioTO.nombre = (String)reader.GetSqlString(1).ToString();
                            socioTO.apellido1 = (String)reader.GetSqlString(2).ToString();
                            socioTO.apellido2 = (String)reader.GetSqlString(3).ToString();
                            String rol = (String)reader.GetSqlString(4).ToString();

                            socioTO.rol = rol;
                            socioTO.estado_socio = (Boolean)reader.GetBoolean(5);

                            //CONTACTOS
                            Object telHObject = (Object)(reader.GetSqlValue(6));
                            String telHString = telHObject.ToString();
                            int telH = Int32.Parse(telHString);
                            Object telPObject = (Object)(reader.GetSqlValue(7));
                            String telPString = telPObject.ToString();
                            int telP = Int32.Parse(telPString);
                            String correo = (String)reader.GetSqlString(8).ToString();
                            TOContactos contactos = new TOContactos(telH, telP, correo);
                            socioTO.contactos = contactos;

                            //DIRECCION
                            TODireccion tODireccion = new TODireccion();
                            tODireccion.provincia = (String)reader.GetSqlString(9).ToString();
                            tODireccion.canton = (String)reader.GetSqlString(10).ToString();
                            tODireccion.distrito = (String)reader.GetSqlString(11).ToString();
                            tODireccion.otras_sennas = (String)reader.GetSqlString(12).ToString();
                            tODireccion.cod_direccion = (Int32)reader.GetSqlInt32(13);
                            socioTO.direccion = tODireccion;
                        }
                        conexion.Close();
                        return socioTO;

                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine(e.ToString());
                        return null;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public List<TOSocioNegocio> lista_Socios(String busqueda)
        {
            {
                try
                {
                    List<TOSocioNegocio> lista = new List<TOSocioNegocio>();
                    String sql = "Select NOMBRE, APELLIDO1, APELLIDO2, ROL_SOCIO from SOCIO_NEGOCIO";
                    SqlCommand cmdSocio = new SqlCommand(sql, conexion);

                    if (string.IsNullOrEmpty(busqueda) == false)
                    {
                        sql += " where ESTADO_SOCIO = 1 AND ((NOMBRE LIKE '%' + @pal + '%')  or (APELLIDO1 LIKE '%' + @pal + '%') or (APELLIDO2 LIKE '%' + @pal + '%'));";
                        cmdSocio.Parameters.AddWithValue("@pal", busqueda);
                    }
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    SqlDataReader reader = cmdSocio.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TOSocioNegocio to = new TOSocioNegocio();
                            to.cedula = reader.GetString(0);
                            to.nombre = reader.GetString(1);
                            to.rol = reader.GetString(2);
                            to.apellido1 = reader.GetString(3);
                            to.apellido2 = reader.GetString(4);
                            to.estado_socio = true;
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
        /// Método para retornar los últimos socios de negocio, proveedores o clientes, agregados a la base de datos
        /// </summary>
        /// <param name="rolSocio">Tipo de rol del socio: Proveedor o Cliente</param>
        /// <returns>Retorna la lista con el top 3 de los últimos socios de negocio agregados</returns>
        public List<TOSocioNegocio> top3_UltimosSocios(String rolSocio)
        {
            try
            {
                List<TOSocioNegocio> lista = new List<TOSocioNegocio>();
                SqlCommand cmdSocio = new SqlCommand("SELECT TOP 3 CEDULA, NOMBRE + ' ' + APELLIDO1 + ' ' + APELLIDO2 AS 'NOMBRE COMPLETO' FROM SOCIO_NEGOCIO WHERE ROL_SOCIO = @rol ORDER BY FECHA_INGRESO DESC;", conexion);
                cmdSocio.Parameters.AddWithValue("@rol", rolSocio);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader reader = cmdSocio.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TOSocioNegocio to = new TOSocioNegocio();
                        to.cedula = reader.GetString(0);
                        to.nombre = reader.GetString(1);
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

}
