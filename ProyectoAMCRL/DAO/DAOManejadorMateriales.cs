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
        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        public DataSet obtenerMateriales(){

                SqlCommand cmd = new SqlCommand("select * from MATERIAL order by NOMBRE_MATERIAL", conexion);
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
            cmd.Parameters.AddWithValue("@ID_BOD", id_bodega);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("materialesEnBodega");
            sda.Fill(ds);

            return ds;
        }

        public string registrarActualizarMaterialDAO(TOMaterial material, char tipo){

            String msg = "";
            using (conexion)
            {
                conexion.Open();

                //Se inicializa la transaccion local
                SqlTransaction transaction = conexion.BeginTransaction();
                String respuesta = "";

                //Se asigna un comando a la transaccion
                SqlCommand command = conexion.CreateCommand();
                command.Transaction = transaction;

                String sqlInsertar = "insert into MATERIAL(COD_MATERIAL, NOMBRE_MATERIAL, PRECIO_KILO, COD_UNIDAD) values (@COD, @NOMBRE, @PRECIO, @UNIDAD_BASE);";
                String sqlActualizar = "update MATERIAL set NOMBRE_MATERIAL = @NOMBRE, PRECIO_KILO = @PRECIO, COD_UNIDAD = @UNIDAD_BASE where COD_MATERIAL = @COD;";

                //TEXTO SQL 
                //String sqlActualizarRegistrar =
                // "begin tran " +
                // "if exists(select * from MATERIAL with (updlock, serializable) where COD_MATERIAL = @COD) " +
                // "begin update MATERIAL set NOMBRE_MATERIAL = @NOMBRE, PRECIO_KILO = @PRECIO, COD_UNIDAD = @UNIDAD_BASE where COD_MATERIAL = @COD; end " +
                // "else " +
                // "begin insert into MATERIAL(COD_MATERIAL, NOMBRE_MATERIAL, PRECIO_KILO, COD_UNIDAD) values (@COD, @NOMBRE, @PRECIO, @UNIDAD_BASE); " +
                // "end commit tran";

                try{
                    //REGISTRO MATERIAL
                    if (tipo.Equals('r'))
                    {
                        command.CommandText = sqlInsertar;
                        respuesta = "registrado";
                    }
                    else
                    {
                        command.CommandText = sqlActualizar;
                        respuesta = "actualizado";
                    }

                    command.Parameters.AddWithValue("@COD", material.codigoM); 
                    command.Parameters.AddWithValue("@NOMBRE", material.nombreMaterial);
                    command.Parameters.AddWithValue("@PRECIO", material.precioKilo);
                    command.Parameters.AddWithValue("@UNIDAD_BASE", material.unidadBase.codigo);

                    command.ExecuteNonQuery();

                    if (tipo.Equals('r')) {
                        String sqlRegistrarStock = "insert into stock (COD_MATERIAL, ID_BODEGA, KILOS_STOCK) " +
                            "values (@COD, 'B01', 0); ";
                        command.CommandText = sqlRegistrarStock;
                        command.ExecuteNonQuery();


                    }


                    //COMMIT A LA TRANSACCION
                    transaction.Commit();
                    return "Material "+ respuesta+" correctamente";

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

        //public TOMaterial buscarMaterialDAO(string clave)
        //{
        //    TOMaterial MTO = new TOMaterial();
        //    String sql = "select m.COD_MATERIAL, m.NOMBRE_MATERIAL, m.PRECIO_KILO, um.COD_UNIDAD, um.NOMBRE_UNIDAD, um.EQUIVALENCIA_KG from MATERIAL m "+
        //    " inner join UNIDAD_MEDIDA um on(m.COD_UNIDAD = um.COD_UNIDAD and m.COD_MATERIAL = @COD); ";
        //    String codigo = "";
        //    String nombre = "";
        //    Double precioKilo = 0;

        //    String codUnidad = "";
        //    String nomUnidad = "";
        //    Double equivalenciaUnidad = 0;


        //    using (conexion) {
        //        SqlCommand cmd = new SqlCommand(sql,conexion);
        //        cmd.Parameters.AddWithValue("@COD", clave);

        //        try
        //        {

        //            conexion.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.HasRows) {

        //                while (reader.Read())
        //                {
        //                    codigo = (String)reader.GetString(0);
        //                    nombre = (String)reader.GetString(1);
        //                    precioKilo = reader.GetSqlDecimal(2).ToDouble();

        //                    codUnidad = (String)reader.GetString(3);
        //                    nomUnidad = (String)reader.GetString(4);
        //                    equivalenciaUnidad = reader.GetSqlDecimal(5).ToDouble();


        //                }

        //                MTO.codigoM = clave;
        //                MTO.nombreMaterial = nombre;
        //                MTO.precioKilo = precioKilo;
        //                //UNIDAD
        //                //TOUnidad unidad = new TOUnidad(codUnidad, nomUnidad, equivalenciaUnidad);
        //                MTO.unidadBase = unidad;

        //                conexion.Close();
        //                return MTO;

        //            }
        //            else {
        //                return null;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            return null;
        //        }
        //    }
        //}

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


        public List<TOMaterial> top3_Materiales()
        {
            try
            {
                List<TOMaterial> lista = new List<TOMaterial>();
                String sql = "SELECT TOP 3 s.COD_MATERIAL, m.NOMBRE_MATERIAL FROM STOCK s, MATERIAL m WHERE s.COD_MATERIAL = m.COD_MATERIAL ORDER BY s.KILOS_STOCK DESC;";
                SqlCommand cmd_Materiales = new SqlCommand(sql, conexion);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader reader = cmd_Materiales.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TOMaterial to = new TOMaterial();
                        to.codigoM = reader.GetString(0);
                        to.nombreMaterial = reader.GetString(1);
  
                        lista.Add(to);
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
