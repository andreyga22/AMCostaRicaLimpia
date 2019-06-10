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
            Boolean contacto = agregarContactosSocio(socio.cedula, socio.contactos);

            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from SOCIO_NEGOCIO with (updlock, serializable)" +
                            "where CEDULA = @CEDULA) begin update SOCIO_NEGOCIO set COD_DIRECCION = @COD_DIRECCION, "
                            + "SOC_CEDULA = @SOC_CEDULA, NOMBRE = @NOMBRE, ROL_SOCIO = @ROL_SOCIO, APELLIDO1 = @APELLIDO1, " +
                            "APELLIDO2 = @APELLIDO2, ESTADO_SOCIO = @ESTADO_SOCIO where CEDULA = @CEDULA;" +
                            " end else begin insert into SOCIO_NEGOCIO(CEDULA, COD_DIRECCION, SOC_CEDULA, NOMBRE, ROL_SOCIO, " +
                            "APELLIDO1, APELLIDO2, ESTADO_SOCIO) values(@CEDULA, @COD_DIRECCION, @SOC_CEDULA, @NOMBRE," +
                            " @ROL_SOCIO, @APELLIDO1, @APELLIDO2, @ESTADO_SOCIO); END commit tran;", conexion);

            try
            {
                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();
                insertar.Parameters.AddWithValue("@COD_DIRECCION", direccion);
                insertar.Parameters.AddWithValue("@CEDULA", socio.cedula);
                insertar.Parameters.AddWithValue("@NOMBRE", socio.nombre);
                insertar.Parameters.AddWithValue("@ROL_SOCIO", socio.rol);
                insertar.Parameters.AddWithValue("@APELLIDO1", socio.apellido1);
                insertar.Parameters.AddWithValue("@APELLIDO2", socio.apellido2);
                insertar.Parameters.AddWithValue("@ESTADO_SOCIO", socio.estado_socio);
                insertar.ExecuteNonQuery();

                conexion.Close();

            }
            catch (SqlException ex)
            {
                return false;
            }
            return true;
        }

        public int agregarDireccionSocio(TODireccion direccion)
        {

            int codigo = 0;
            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from DIRECCION with (updlock, serializable)" +
                            "where CEDULA = @CEDULA) begin update DIRECCION set PROVINCIA = @PROVINCIA, "
                            + "CANTON = @CANTON, DISTRITO = @DISTRITO, SENNAS = @SENNAS;" +
                            " end else begin insert into DIRECCION(PROVINCIA, CANTON, DISTRITO, SENNAS) values" +
                            "(@PROVINCIA, @CANTON, @DISTRITO, @SENNAS); END commit tran;", conexion);

            try
            {

                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                insertar.Parameters.AddWithValue("@PROVINCIA", direccion.provincia);
                insertar.Parameters.AddWithValue("@CANTON", direccion.canton);
                insertar.Parameters.AddWithValue("@DISTRITO", direccion.distrito);
                insertar.Parameters.AddWithValue("@SENNAS", direccion.otras_sennas);

                codigo = (int)insertar.ExecuteScalar();

                conexion.Close();

            }
            catch (SqlException ex)
            {

            }
            return codigo;
        }


        public Boolean agregarContactosSocio(String id, TOContactos contactos)
        {
            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from CONTACTOS with (updlock, serializable)" +
                            "where CEDULA = @CEDULA) begin update CONTACTOS set CEDULA = @CEDULA, "
                            + "TELEFONO_HAB = @TEL_HAB, TELEFONO_PERS = @TEL_PERS, EMAIL = @EMAIL;" +
                            " end else begin insert into CONTACTOS(CEDULA, TEL_HAB, TEL_PERS, EMAIL) values" +
                            "(@CEDULA, @TEL_HAB, @TEL_PERS, @EMAIL); END commit tran;", conexion);

            try
            {

                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                insertar.Parameters.AddWithValue("@CEDULA", id);
                insertar.Parameters.AddWithValue("@TEL_HAB", contactos.telefono_hab);
                insertar.Parameters.AddWithValue("@TEL_PERS", contactos.telefono_pers);
                insertar.Parameters.AddWithValue("@EMAIL", contactos.email);

                insertar.ExecuteNonQuery();

                conexion.Close();

            }
            catch (SqlException ex)
            {
                return false;
            }
            return true;
        }

        public TOSocioNegocio buscarSocio(String query)
        {
            SqlCommand buscar = new SqlCommand(query, conexion);
            TOSocioNegocio socio = new TOSocioNegocio();
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            SqlDataReader reader = buscar.ExecuteReader();
            if (reader.HasRows && reader.GetBoolean(5) != false)
            {
                while (reader.Read())
                {
                    socio.cedula = reader.GetString(0);
                    socio.nombre = reader.GetString(1);
                    socio.rol = reader.GetString(2);
                    socio.apellido1 = reader.GetString(3);
                    socio.apellido2 = reader.GetString(4);
                }
            }
            else
            {
                return socio;
            }
            return socio;

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
                    direccion.provincia= reader.GetString(0);
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
    }
}
