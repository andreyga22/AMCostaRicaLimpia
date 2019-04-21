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

        //Properties.Settings.Default.conexion
        private SqlConnection conexion = new SqlConnection("");


        public String agregarSocio(TOSocioNegocio socio)
        {
            String message = "";
            SqlCommand cmd = new SqlCommand("INSERT  INTO SOCIO_NEGOCIO VALUES" +
                "@CED, @CED_AS,@APE1,@APE2,@ROL", conexion);

            try
            {
                cmd.Parameters.AddWithValue("@CED", socio.cedula);
                cmd.Parameters.AddWithValue("@CED_AS", socio.cedula_asociado);
                cmd.Parameters.AddWithValue("@APE1", socio.apellido1);
                cmd.Parameters.AddWithValue("@APE2", socio.apellido2);
                cmd.Parameters.AddWithValue("@ROL", socio.rol);

                message = "" + cmd.ExecuteNonQuery();

                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                conexion.Close();
            }
            catch (SqlException ex)
            { //podria ser mas especifico
                return ex.Message;
            }
            return message;
        }

    }
}
