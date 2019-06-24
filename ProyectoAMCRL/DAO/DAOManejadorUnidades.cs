using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOManejadorUnidades
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);


        public DataSet listarUnidadesDAO(){
            String sql = "select * from unidad_medida";
            SqlCommand cmd = new SqlCommand(sql, conexion);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("unidades");
            sda.Fill(ds);

            return ds;
        }

        public double consultarEquivalenciaUnidadDAO(string codUnidad)
        {
            String sql = "SELECT EQUIVALENCIA_KG FROM UNIDAD_MEDIDA WHERE COD_UNIDAD = @COD";
            Double equivalencia = 0;
            
            using (conexion) {

                conexion.Open();


                try {
                    SqlCommand cmd = new SqlCommand(sql, conexion);

                } catch (Exception ex) {
                    return -1;
                }

            }
            return equivalencia;


        }
    }
}
