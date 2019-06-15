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
    }
}

