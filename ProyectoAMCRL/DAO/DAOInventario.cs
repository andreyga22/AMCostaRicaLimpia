using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOInventario
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);


        public DataTable buscarStock(string bodega)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "Select a.COD_MATERIAL as Código, (Select NOMBRE_MATERIAL from MATERIAL c where a.COD_MATERIAL = c.COD_MATERIAL) as Material, a.KILOS_STOCK as Cantidad from STOCK a join BODEGA b on a.ID_BODEGA = (Select ID_BODEGA from Bodega where NOMBRE_BOD = @Bod);";

                    cmd.Parameters.AddWithValue("@Bod", bodega);
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


        public List<String> buscarBodegas()
        {
            try
            {
                List<String> lista = new List<String>();

                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "Select NOMBRE_BOD from Bodega;";
                    cmd.CommandText = sql;
                    cmd.Connection = conexion;
                if(conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(reader.GetString(0));
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
