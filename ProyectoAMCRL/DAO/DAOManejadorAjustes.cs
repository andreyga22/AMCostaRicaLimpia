using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOManejadorAjustes
    {

        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);

        public DataSet listarAjustesDAO()
        {
       
            String sql = "SELECT [ID_AJUSTE],(convert(varchar, [Fecha_Ajuste], 23)) as Fecha_Ajuste, [RAZON], [PESO_AJUSTE], [MOVIMIENTO_A], a.[ID_STOCK]  FROM AJUSTE a " +
            "inner join STOCK s on (a.ID_STOCK = s.ID_STOCK and s.ID_BODEGA = @id_bodega) ORDER BY Fecha_Ajuste DESC; "; 
            SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@id_bodega", 1);//cambiar el id de la bodega a parametro
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("ajustes");
            sda.Fill(ds);

            return ds;


        }

        public string registrarAjusteDAO(int id_bod, string peso, string material, string accion, string razon)
        {
            String msg = "";
            String sql = "INSERT INTO AJUSTE ([RAZON], [PESO_AJUSTE], [MOVIMIENTO_A])" +
                "VALUES(@ID_BODEGA, @PESO, @MATERIAL, @ACCION, @RAZON);";



            using (conexion)
            {
                conexion.Open();
                //Se inicializa la transaccion local
                SqlTransaction transaction = conexion.BeginTransaction();

                //Se asigna un comando a la transaccion
                SqlCommand command = conexion.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    //REGISTRO MATERIAL
                    command.CommandText = sql;

                    command.Parameters.AddWithValue("@ID_BODEGA", id_bod);
                    command.Parameters.AddWithValue("@PESO", peso);
                    command.Parameters.AddWithValue("@MATERIAL", material);
                    command.Parameters.AddWithValue("@ACCION", accion);
                    command.Parameters.AddWithValue("@RAZON", razon);
                    command.ExecuteNonQuery();

                    //COMMIT A LA TRANSACCION
                    transaction.Commit();
                    return "Operación efectuada correctamente";

                }
                catch (Exception ex)
                {

                    msg = "Ocurrió un error en la operación, contacte al administrador. Error: " + ex.Source;

                    try
                    {
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
            }
        }




    }
}
