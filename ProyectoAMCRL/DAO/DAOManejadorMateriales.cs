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

        public DataSet obtenerMaterialesEnBodegaActual(String id_bodega)
        {

            String sql = "select s.ID_STOCK, m.COD_MATERIAL, m.NOMBRE_MATERIAL, m.PRECIO_KILO from MATERIAL m, STOCK s " +
                "where(m.COD_MATERIAL = s.COD_MATERIAL and s.ID_BODEGA = @ID_BOD);";

            SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@ID_BOD", "B01");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("materialesEnBodega");
            sda.Fill(ds);

            return ds;
        }

        public string registrarActualizarMaterialDAO(TOMaterial material){

            String msg = "";
            using (conexion)
            {
                conexion.Open();

                //Se inicializa la transaccion local
                SqlTransaction transaction = conexion.BeginTransaction();

                //Se asigna un comando a la transaccion
                SqlCommand command = conexion.CreateCommand();
                command.Transaction = transaction;

                //TEXTO SQL 
                String sqlActualizarRegistrar =
                 "begin tran " +
                 "if exists(select * from MATERIAL with (updlock, serializable) where COD_MATERIAL = @COD) " +
                 "begin update MATERIAL set NOMBRE_MATERIAL = @NOMBRE, PRECIO_KILO = @PRECIO where COD_MATERIAL = @COD; end " +
                 "else " +
                 "begin insert into MATERIAL(NOMBRE_MATERIAL, PRECIO_KILO) values (@NOMBRE, @PRECIO); " +
                 "end commit tran";

                try{
                    //REGISTRO MATERIAL
                    command.CommandText = sqlActualizarRegistrar;

                    command.Parameters.AddWithValue("@COD", material.codigoM); 
                    command.Parameters.AddWithValue("@NOMBRE", material.nombreMaterial);
                    command.Parameters.AddWithValue("@PRECIO", material.precioKilo);
                    command.ExecuteNonQuery();

                    //COMMIT A LA TRANSACCION
                    transaction.Commit();
                    return "Operación efectuada correctamente";

                }
                catch (Exception ex)
                {

                    msg = "Ocurrió un error en la operación, contacte al administrador. Error: " + ex.Source;

                    try{
                        // Se intenta hacer rollback de la transaccion
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection 
                        // is closed or the transaction has already been rolled 
                        // back on the server.
                        return (exRollback.Message);
                    }

                }//EXCEPCION
                return msg;
            }//CONEXION

        }


        public double traerCantidadVendidaDAO(int v)
        {
            double res = 0;

            String sql = "SELECT SUM(KILOS_COMPRA) AS TOTAL FROM DETALLE_COMPRA WHERE COD_MATERIAL = " + v;
            using (conexion) {
                try
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                        res = (Double)reader.GetDecimal(0);
                    }
                }
                catch (Exception e)
                {

                    return 0;
                }
            }
              
            return res;
        }
    }
}
