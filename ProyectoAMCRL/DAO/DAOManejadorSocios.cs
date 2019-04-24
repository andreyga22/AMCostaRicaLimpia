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

        
        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);


        public String agregarSocio(TOSocioNegocio socio)
        {
            String message = "Se ha registrado un nuevo " + socio.rol + ".";
            SqlCommand cmd = new SqlCommand("INSERT  INTO SOCIO_NEGOCIO " +
                "(CEDULA, NOMBRE, ROL_SOCIO, APELLIDO1, APELLIDO2,ESTADO_SOCIO)" +
                "VALUES" +
                "(@CED, @NOM, @ROL, @APE1, @APE2, @ESTADO)", conexion);

            try
            {
                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                cmd.Parameters.AddWithValue("@CED", socio.cedula);
                //cmd.Parameters.AddWithValue("@CED_AS", null);
                cmd.Parameters.AddWithValue("@NOM", socio.nombre);
                cmd.Parameters.AddWithValue("@ROL", socio.rol);
                cmd.Parameters.AddWithValue("@APE1", socio.apellido1);
                cmd.Parameters.AddWithValue("@APE2", socio.apellido2);
                cmd.Parameters.AddWithValue("@ESTADO", socio.estado_socio);

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
            catch (SqlException ex)
            { //podria ser mas especifico
                return "ERROR. El socio con identificacion "+ socio.cedula + " ya se encuentra registrado";
            }
            return message;
        }

    }
}
