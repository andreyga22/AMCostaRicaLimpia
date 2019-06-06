using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;


namespace DAO
{
    public class DAOManejadorMateriales
    {
        private List<TOMaterial> materiales = new List<TOMaterial>();
        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);

        public DataSet obtenerMateriales(){

                SqlCommand cmd = new SqlCommand("select * from MATERIAL", conexion);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet("materiales");
                sda.Fill(ds);

            return ds;
        }

        public string registrarMaterialDAO(string nom, string precio)
        {
            throw new NotImplementedException();
        }

        public string actualizarMaterialDAO(int cod, string nom, string precio)
        {
            throw new NotImplementedException();
        }

        public double traerCantidadVendidaDAO(int v)
        {
            double res = 0;

            String sql = "SELECT SUM(KILOS_COMPRA) AS TOTAL FROM DETALLE_COMPRA WHERE COD_MATERIAL = " + v;
            using (conexion) {
                //try {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                        res = (Double)reader.GetDecimal(0);
                    }
                //} catch (Exception e) {
                    
                //    return -1;
                //}  
            }
              
            return res;
        }
    }
}
