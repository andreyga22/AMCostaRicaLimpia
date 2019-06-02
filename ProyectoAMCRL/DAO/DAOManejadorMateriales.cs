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
            



    }
}
