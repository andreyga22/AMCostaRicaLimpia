using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TO;

namespace DAO {
    public class DAOManejadorSocios
    {


        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        AMCRLEntities context = new AMCRLEntities();


        public Boolean agregarModificarSocio(TOSocioNegocio socio)
        {
            
            int direccion = agregarDireccionSocio(socio.direccion);
            

            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from SOCIO_NEGOCIO with (updlock, serializable)" +
                            "where CEDULA = @CEDULA) begin update SOCIO_NEGOCIO set COD_DIRECCION = @COD_DIRECCION, "
                            + "NOMBRE = @NOMBRE, ROL_SOCIO = @ROL_SOCIO, APELLIDO1 = @APELLIDO1, " +
                            "APELLIDO2 = @APELLIDO2, ESTADO_SOCIO = @ESTADO_SOCIO where CEDULA = @CEDULA;" +
                            " end else begin insert into SOCIO_NEGOCIO(CEDULA, COD_DIRECCION, NOMBRE, ROL_SOCIO, " +
                            "APELLIDO1, APELLIDO2, ESTADO_SOCIO) values(@CEDULA, @COD_DIRECCION, @NOMBRE," +
                            " @ROL_SOCIO, @APELLIDO1, @APELLIDO2, @ESTADO_SOCIO); END commit tran;", conexion);

                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();
                insertar.Parameters.AddWithValue("@COD_DIRECCION", direccion);
                insertar.Parameters.AddWithValue("@CEDULA", socio.cedula);
                insertar.Parameters.AddWithValue("@NOMBRE", socio.nombre);
                insertar.Parameters.AddWithValue("@ROL_SOCIO", socio.rol);
                insertar.Parameters.AddWithValue("@APELLIDO1", socio.apellido1);
                insertar.Parameters.AddWithValue("@APELLIDO2", socio.apellido2);
                
            if (socio.estado_socio == true) {
                    insertar.Parameters.AddWithValue("@ESTADO_SOCIO", 1);
                }
                else
                {
                    insertar.Parameters.AddWithValue("@ESTADO_SOCIO", 0);
                }
                
                insertar.ExecuteNonQuery();

                conexion.Close();

            Boolean contacto = agregarContactosSocio(socio.cedula, socio.contactos);

            return true;
        }

        private int agregarDireccionSocio(TODireccion direccion)
        {

            int codigo = 0;
            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from DIRECCION with (updlock, serializable)" +
                            "where COD_DIRECCION = @COD_DIRECCION) begin update DIRECCION set PROVINCIA = @PROVINCIA, "
                            + "CANTON = @CANTON, DISTRITO = @DISTRITO, OTRAS_SENNAS = @SENNAS;" +
                            " end else begin insert into DIRECCION(PROVINCIA, CANTON, DISTRITO, OTRAS_SENNAS) values" +
                            "(@PROVINCIA, @CANTON, @DISTRITO, @SENNAS); END commit tran;", conexion);

            insertar.Parameters.AddWithValue("@PROVINCIA", direccion.provincia);
            insertar.Parameters.AddWithValue("@CANTON", direccion.canton);
            insertar.Parameters.AddWithValue("@DISTRITO", direccion.distrito);
            insertar.Parameters.AddWithValue("@SENNAS", direccion.otras_sennas);

            SqlCommand buscarCodigo = new SqlCommand("Select COD_DIRECCION from DIRECCION where OTRAS_SENNAS = @SENNAS;", conexion);
            buscarCodigo.Parameters.AddWithValue("@SENNAS", direccion.otras_sennas);

            if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

            SqlDataReader reader = buscarCodigo.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    codigo = reader.GetInt32(0);
                }
            }
            reader.Close();


            if (direccion.cod_direccion != 0) {
                insertar.Parameters.AddWithValue("@COD_DIRECCION", direccion.cod_direccion);
            }
            else
            {
                insertar.Parameters.AddWithValue("@COD_DIRECCION", codigo);
            }

            insertar.ExecuteNonQuery();
            conexion.Close();

            

            return codigo;
        }


        private Boolean agregarContactosSocio(String cedula, TOContactos contactos)
        {
            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from CONTACTOS with (updlock, serializable)" +
                            "where CEDULA = @CEDULA) begin update CONTACTOS set CEDULA = @CEDULA, "
                            + "TELEFONO_HAB = @TEL_HAB, TELEFONO_PERS = @TEL_PERS, EMAIL = @EMAIL;" +
                            " end else begin insert into CONTACTOS(CEDULA, TELEFONO_HAB, TELEFONO_PERS, EMAIL) values" +
                            "(@CEDULA, @TEL_HAB, @TEL_PERS, @EMAIL); END commit tran;", conexion);

                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                insertar.Parameters.AddWithValue("@CEDULA", cedula);
                insertar.Parameters.AddWithValue("@TEL_HAB", contactos.telefono_hab);
                insertar.Parameters.AddWithValue("@TEL_PERS", contactos.telefono_pers);
                insertar.Parameters.AddWithValue("@EMAIL", contactos.email);

                insertar.ExecuteNonQuery();

                conexion.Close();

            
            return true;
        }

        //ajustar al filtro
        public DataTable buscarSociosFiltro(String busqueda)
        {
            using (conexion)
            {
                SqlCommand buscar = conexion.CreateCommand();
                String sql = "Select cedula, nombre, apellido1, apellido2 from SOCIO_NEGOCIO";
                if (!string.IsNullOrEmpty(busqueda))
                {
                    sql += " WHERE (cedula LIKE '%' + @pal + '%')  or (nombre LIKE '%' + @pal + '%') or (apellido1 LIKE '%' + @pal + '%') or (apellido2 LIKE '%' + @pal + '%');";
                    buscar.Parameters.AddWithValue("@pal", busqueda);
                }
                buscar.CommandText = sql;
                buscar.Connection = conexion;
                using (SqlDataAdapter adapter = new SqlDataAdapter(buscar))
                {
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);

                    foreach (DataRow fila in tabla.Rows) // search whole table
                    {
                        if (fila["rol_socio"].Equals("Proveedor")) // si es proveedor
                        {
                            fila["rol_socio"] = "Proveedor"; //change the name
                                                         //break; break or not depending on you
                        }
                        else
                        {
                            if (fila["rol_socio"].Equals("Cliente"))
                            {
                                fila["rol_socio"] = "Cliente";
                            }
                        }

                    }

                    return tabla;
                }
            }
        }

        public TODireccion buscarDireccion(String cedula)
        {
            SqlCommand buscar = new SqlCommand("Select PROVINCIA, CANTON, DISTRITO, OTRAS_SENNAS from DIRECCION where CEDULA = @CEDULA", conexion);
            TODireccion direccion = new TODireccion();
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
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
            }
            else
            {
                return direccion;
            }
            return direccion;
        }

        public TOContactos buscarContacto(String cedula)
        {
            SqlCommand buscarContacto = new SqlCommand("Select TELEFONO_HAB, TELEFONO_PERS, EMAIL from CONTACTOS where CEDULA = @CEDULA", conexion);
            TOContactos contacto = new TOContactos();
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            SqlDataReader reader = buscarContacto.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    contacto.telefono_hab = Int32.Parse(reader.GetString(0));
                    contacto.telefono_pers = Int32.Parse(reader.GetString(1));
                    contacto.email = reader.GetString(2);
                }
            }
            else
            {
                return contacto;
            }
            return contacto;
        }

        public TOSocioNegocio buscarSocio(string id, String tipoSocio)
        {
            TOSocioNegocio socioTO = new TOSocioNegocio(); ; 

            String sql = "SELECT SOCIO_NEGOCIO.CEDULA, SOCIO_NEGOCIO.NOMBRE, SOCIO_NEGOCIO.APELLIDO1, SOCIO_NEGOCIO.APELLIDO2, SOCIO_NEGOCIO.ROL_SOCIO, SOCIO_NEGOCIO.ESTADO_SOCIO," +
            "CONTACTOS.TELEFONO_HAB, CONTACTOS.TELEFONO_PERS, CONTACTOS.EMAIL," +
            "DIRECCION.PROVINCIA, DIRECCION.CANTON, DIRECCION.DISTRITO, DIRECCION.OTRAS_SENNAS, DIRECCION.COD_DIRECCION " +
            "FROM SOCIO_NEGOCIO, CONTACTOS, DIRECCION " +
            "where SOCIO_NEGOCIO.CEDULA = CONTACTOS.CEDULA and SOCIO_NEGOCIO.COD_DIRECCION = DIRECCION.COD_DIRECCION and SOCIO_NEGOCIO.CEDULA = @ID and SOCIO_NEGOCIO.ROL_SOCIO = @ROL";


            using (conexion) {

                SqlCommand cmd = new SqlCommand(sql, conexion);

                try
                {

                    conexion.Open();
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@ROL", tipoSocio);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return null;

                    while (reader.Read()) {
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
                        TOContactos contactos = new TOContactos(telH,telP,correo);
                        socioTO.contactos = contactos;

                        //DIRECCION
                        TODireccion tODireccion = new TODireccion();
                        tODireccion.provincia= (String)reader.GetSqlString(9).ToString(); 
                        tODireccion.canton= (String)reader.GetSqlString(10).ToString(); 
                        tODireccion.distrito= (String)reader.GetSqlString(11).ToString(); 
                        tODireccion.otras_sennas= (String)reader.GetSqlString(12).ToString();
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



    }
}
