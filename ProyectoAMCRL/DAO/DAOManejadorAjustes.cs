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
    public class DAOManejadorAjustes
    {
       
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        public DataSet listarAjustesDAO()
        {
            
            String sql = "SELECT [ID_AJUSTE],(convert(varchar, [Fecha_Ajuste], 103)) as Fecha_Ajuste, [RAZON], [PESO_AJUSTE], [MOVIMIENTO_A], a.[ID_STOCK], s.[ID_BODEGA]  FROM AJUSTE a " +
            "inner join STOCK s on (a.ID_STOCK = s.ID_STOCK and s.ID_BODEGA = @id_bodega) ORDER BY Fecha_Ajuste DESC; "; 
            SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@id_bodega", "B01");//cambiar el id de la bodega a parametro
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("ajustes");
            sda.Fill(ds);

            return ds;


        }

        public string registrarAjusteDAO(String id_bod, string id_material, int id_stock, double peso, string accion, string razon)
        {
            String msg = "";
            String sqlAjuste = "INSERT INTO AJUSTE ([RAZON], [PESO_AJUSTE], [MOVIMIENTO_A], [ID_STOCK], [Fecha_Ajuste]) " +
                "VALUES (@RAZON, @PESO_AJUSTE, @MOVIMIENTO, @ID_STOCK, @Fecha)";

            char operacion = '-';
            if(accion.Equals("1"))
                operacion = '+';

            String sqlStock = "UPDATE STOCK SET KILOS_STOCK = (KILOS_STOCK "+operacion+ " @PESO_AJUSTE) " +
                "WHERE (ID_STOCK = @ID_STOCK AND COD_MATERIAL = @COD_MATERIAL AND ID_BODEGA = @ID_BODEGA);";

            String fecha = System.DateTime.Today.ToShortDateString();

            using (conexion)
            {

                conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

                conexion.Open();
                //Se asigna un comando a la transaccion
                SqlCommand command = conexion.CreateCommand();
                

                    //Se inicializa la transaccion local
                    SqlTransaction transaction = conexion.BeginTransaction();

                    command.Connection = conexion;
                    command.Transaction = transaction;

                try
                {

                    //REGISTRO AJUSTE
                    command.CommandText = sqlAjuste;

                    command.Parameters.AddWithValue("@RAZON", razon);
                    command.Parameters.AddWithValue("@COD_MATERIAL", id_material);
                    command.Parameters.AddWithValue("@MOVIMIENTO", accion);
                    command.Parameters.AddWithValue("@ID_STOCK", id_stock);
                    command.Parameters.AddWithValue("@PESO_AJUSTE", peso);
                    command.Parameters.AddWithValue("@Fecha", fecha);

                    command.ExecuteNonQuery();

                    //RESTAR O SUMAR A STOCK 
                    command.CommandText = sqlStock;
                    command.Parameters.AddWithValue("@ID_BODEGA", id_bod);

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

        public String buscarAjusteDAO(string idAjuste)
        {
            //conexion = new SqlConnection(Properties.Settings.Default.conexionHost);
            String ajusteInfo = "No encontrado";

            String sql = "select AJUSTE.Fecha_Ajuste, AJUSTE.MOVIMIENTO_A, AJUSTE.PESO_AJUSTE, MATERIAL.NOMBRE_MATERIAL, BODEGA.NOMBRE_BOD, AJUSTE.RAZON" +
                " from AJUSTE , MATERIAL , BODEGA , STOCK " +
                "where(AJUSTE.ID_STOCK = STOCK.ID_STOCK and MATERIAL.COD_MATERIAL = STOCK.COD_MATERIAL and STOCK.ID_BODEGA = BODEGA.ID_BODEGA and AJUSTE.ID_AJUSTE = @ID_AJUSTE);";

            SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@ID_AJUSTE", idAjuste);

                conexion.Open();

                try
                {

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                        //fecha, movimiento, peso, nombreMaterial, nombreBodega, razon
                        DateTime fecha = (DateTime)reader.GetDateTime(0);
                        String movN = (String)reader.GetSqlString(1).ToString();
                        String movimiento = movN.Equals("1") ? "ENTRADA" : "SALIDA";
                        Double peso = Double.Parse(reader.GetDecimal(2).ToString());
                        String nomMaterial = reader.GetSqlString(3).ToString();
                        String nombreBodega = reader.GetSqlString(4).ToString();
                        String razon = reader.GetSqlString(5).ToString();

                        ajusteInfo = fecha.ToString() + "_" + movimiento + "_" +
                            peso + "_" + nomMaterial + "_" + nombreBodega + "_" + razon;
                    }
                    conexion.Close();
                }


            catch (Exception e) {
                conexion.Close();
                return "Error " + e.ToString() ;
                }

            return ajusteInfo;
        }

        public double consultarCantidadStockDAO(int id_stock)
        {
            String sql = "SELECT KILOS_STOCK FROM STOCK WHERE (ID_STOCK = @ID_STOCK);";
            SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@ID_STOCK", id_stock);
            Double kilosStock = 0;
            using (conexion) {
                conexion.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        kilosStock = reader.GetSqlDecimal(0).ToDouble();
                    }
                    conexion.Close();

                }
                catch (Exception e)
                {
                    return kilosStock;
                }
        }
            return kilosStock;

        }
    }
}
