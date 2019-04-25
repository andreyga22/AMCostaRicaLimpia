using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TO;

namespace DAO {
    public class DAOManejadorSocios {


        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);


        public String agregarSocio(TOSocioNegocio socio) {
            String message = "listo";
            SqlCommand cmd = new SqlCommand("INSERT  INTO SOCIO_NEGOCIO " +
                "(CEDULA, NOMBRE, ROL_SOCIO, APELLIDO1, APELLIDO2,ESTADO_SOCIO)" +
                "VALUES" +
                "(@CED, @NOM, @ROL, @APE1, @APE2, @ESTADO)", conexion);

            try {
                if(conexion.State == ConnectionState.Closed)
                    conexion.Open();

                cmd.Parameters.AddWithValue("@CED", socio.cedula);
                cmd.Parameters.AddWithValue("@NOM", socio.nombre);
                cmd.Parameters.AddWithValue("@ROL", socio.rol);
                cmd.Parameters.AddWithValue("@APE1", socio.apellido1);
                cmd.Parameters.AddWithValue("@APE2", socio.apellido2);
                cmd.Parameters.AddWithValue("@ESTADO", socio.estado_socio);

                cmd.ExecuteNonQuery();

                conexion.Close();
                string m1 = "";
                string m2 = "";
                m1 = agregarDireccionSocio(socio.cedula, socio.direccion);
                m2 = agregarContactosSocio(socio.cedula, socio.telHab, socio.telPers, socio.correo);
                if(!m1.Equals("")) {
                    message = m1;
                } else {
                    if(!m2.Equals("")) {
                        message = m2;
                    }
                }

        } catch(SqlException ex) { //podria ser mas especifico
                return "ERROR. El socio con identificacion " + socio.cedula + " ya se encuentra registrado";
            }
            return message;
        }

        public String agregarDireccionSocio(String id, TODireccion direccion) {
            String m = "";

            String query = "insert into DIRECCION (cedula, provincia, canton, distrito, otras_sennas) values (@CED,@PROV,@CAN,@DIST,@SENNAS);";
            SqlCommand cmd = new SqlCommand(query, conexion);

            try {

                if(conexion.State == ConnectionState.Closed)
                    conexion.Open();

                cmd.Parameters.AddWithValue("@CED", id);
                cmd.Parameters.AddWithValue("@PROV", direccion.provincia);
                cmd.Parameters.AddWithValue("@CAN", direccion.canton);
                cmd.Parameters.AddWithValue("@DIST", direccion.distrito);
                cmd.Parameters.AddWithValue("@SENNAS", direccion.otras_sennas);

                cmd.ExecuteNonQuery();

                conexion.Close();

        } catch(SqlException ex) {
                return "Error al insertar la direccion";
            }
            return m;
        }


        public String agregarContactosSocio(String id, Int64 telHab, Int64 telPer, String email) {
            String m = "";

            String query = "insert  into CONTACTOS (cedula, telefono_hab, telefono_pers, email)values(@CED,@TEL_HAB,@TEL_PERS,@EMAIL);";
            SqlCommand cmd = new SqlCommand(query, conexion);

            try {

                if(conexion.State == ConnectionState.Closed)
                    conexion.Open();

                cmd.Parameters.AddWithValue("@CED", id);
                cmd.Parameters.AddWithValue("@TEL_HAB", telHab);
                cmd.Parameters.AddWithValue("@TEL_PERS", telPer);
                cmd.Parameters.AddWithValue("@EMAIL", email);

                cmd.ExecuteNonQuery();

                conexion.Close();

            } catch(SqlException ex) {
                return "Error al insertar los contactos del socio";
            }
            return m;
        }

    }
}
