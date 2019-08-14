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

        public Double consultarStock(String bode, String mate) {
            try {


                SqlCommand buscar = new SqlCommand("SELECT kilos_stock from stock WHERE (cod_material = @cod_material) and (id_bodega = @id_bodega);", conexion);
                buscar.Parameters.AddWithValue("@id_bodega", bode);
                buscar.Parameters.AddWithValue("@cod_material", mate);

                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }
                double canti = 0;
                SqlDataReader reader = buscar.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        canti = Convert.ToDouble( reader.GetDecimal(0));
                    }
                }

                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return canti;
            } catch (Exception exx) {
                throw;
            } finally {
                conexion.Close();
            }
        }

        public List<String> buscarMat() {
            try {
                List<String> lista = new List<String>();

                SqlCommand cmd = conexion.CreateCommand();
                string sql = "Select cod_material from material where estado_material = 1 order by cod_material asc;";
                cmd.CommandText = sql;
                cmd.Connection = conexion;
                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        lista.Add(reader.GetString(0));
                    }
                }
                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
                return lista;
            } catch (Exception) {
                throw;
            } finally {
                conexion.Close();
            }

        }

        public DataSet obtenerMateriales()
        {

            SqlCommand cmd = new SqlCommand("select * from MATERIAL order by NOMBRE_MATERIAL", conexion);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("materiales");
            sda.Fill(ds);

            return ds;
        }

        

        public DataSet obtenerMaterialesEnBodegaActual(String id_bodega)
        {

            String sql = "select s.ID_STOCK, m.COD_MATERIAL, m.NOMBRE_MATERIAL, m.PRECIO_VENTA_KILO, m.PRECIO_COMPRA_KILO from MATERIAL m, STOCK s " +
                "where(m.COD_MATERIAL = s.COD_MATERIAL and s.ID_BODEGA = @ID_BOD);";

            SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@ID_BOD", id_bodega);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("materialesEnBodega");
            sda.Fill(ds);

            return ds;
        }

        public string registrarActualizarMaterialDAO(TOMaterial material, char tipo)
        {

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
                //precio_venta_kilo precio_compra_kilo
                String sqlInsertar = "insert into MATERIAL(COD_MATERIAL, NOMBRE_MATERIAL, precio_venta_kilo, precio_compra_kilo, COD_UNIDAD, ESTADO_MATERIAL) values (@COD, @NOMBRE, @PRECIO_V, @PRECIO_C, @UNIDAD_BASE,  @ESTADO);";
                String sqlActualizar = "update MATERIAL set NOMBRE_MATERIAL = @NOMBRE, precio_venta_kilo = @PRECIO_V, precio_compra_kilo = @PRECIO_C, COD_UNIDAD = @UNIDAD_BASE, ESTADO_MATERIAL = @ESTADO where COD_MATERIAL = @COD;";

                String sqlCodigosBodegas = "select ID_BODEGA from bodega;"; 

                int estado = (material.estado_Material == true) ? 1 : 0;


                try
                {
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
                    command.Parameters.AddWithValue("@PRECIO_V", material.precioVentaK);
                    command.Parameters.AddWithValue("@PRECIO_C", material.precioCompraK);
                    command.Parameters.AddWithValue("@UNIDAD_BASE", material.cod_Unidad);
                    command.Parameters.AddWithValue("@ESTADO", estado);


                    //command.Parameters.AddWithValue("@PRECIO", material.precioKilo);
                    //command.Parameters.AddWithValue("@UNIDAD_BASE", material.unidadBase.codigo);

                    command.ExecuteNonQuery();

                    if (tipo.Equals('r'))
                    {
                        command.CommandText = sqlCodigosBodegas;
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        sda.SelectCommand = command;

                        DataSet ds = new DataSet("bodegas");
                        sda.Fill(ds);

                        String sqlRegistrarStock = "insert into stock (COD_MATERIAL, ID_BODEGA, KILOS_STOCK) values ";
                        String sqlParte2 = "";
                        foreach (DataRow dr in ds.Tables[0].Rows){
                            String codBod = Convert.ToString(dr["ID_BODEGA"]);
                            sqlParte2 += "('" + material.codigoM.Trim() +"' , '"+  codBod.Trim() + "', 0 ) ,";
                        }
                        sqlParte2 = sqlParte2.Remove(sqlParte2.Length - 1);
                        sqlParte2 += ";";
                        String sql = sqlRegistrarStock + sqlParte2;
                        command.CommandText = sql;
                        command.ExecuteNonQuery();


                    }


                    //COMMIT A LA TRANSACCION
                    transaction.Commit();
                    return "Material " + respuesta + " correctamente";

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
            }//CONEXION

        }

        public DataSet traerUnidadYprecioBaseDAO(string codigo, char tipoPrecio)
        {
            DataSet ds = new DataSet("datos");

            String sqlPrecio = tipoPrecio.Equals('v') ? "m.PRECIO_VENTA_KILO" : "m.PRECIO_COMPRA_KILO";

            String sql = "SELECT m.COD_UNIDAD, " + sqlPrecio + " as PRECIO_BASE from MATERIAL m where (m.COD_MATERIAL = @COD)";

                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@COD", codigo);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                }
                catch (Exception e)
                {
                    return null;
                }
            return ds;
        }

     
        public double traerCantidadVendidaDAO(int v)
        {
            double res = 0;

            String sql = "SELECT SUM(KILOS_COMPRA) AS TOTAL FROM DETALLE_COMPRA WHERE COD_MATERIAL = " + v;
            using (conexion)
            {
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

        /// <summary>
        /// Método para retornar los 3 materiales con mayor cantidad en stock
        /// </summary>
        /// <returns>Retorna la lista de material</returns>
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

        /// <summary>
        /// Buscar información del material para la tabla, con rol regular
        /// </summary>
        /// <param name="busqueda">Palabra para buscar en la base de datos</param>
        /// <returns>Retorna el datatable con los datos del material</returns>
        public DataTable buscarUsuarioAdmin(string busqueda)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "SELECT COD_MATERIAL AS 'Código Material', NOMBRE_MATERIAL as 'Nombre', PRECIO_COMPRA_KILO as 'Precio Compra', PRECIO_VENTA_KILO as 'Precio Venta', ESTADO_MATERIAL as 'Estado' FROM MATERIAL";
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        sql += " WHERE ((COD_MATERIAL LIKE '%' + @pal + '%')  or (NOMBRE_MATERIAL LIKE '%' + @pal + '%') or (PRECIO_COMPRA_KILO LIKE '%' + @pal + '%') or (PRECIO_VENTA_KILO LIKE '%' + @pal + '%'));";
                        cmd.Parameters.AddWithValue("@pal", busqueda);
                    }
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
        }

        /// <summary>
        /// Buscar información del material para la tabla, con rol de Administrador
        /// </summary>
        /// <param name="busqueda">Palabra para buscar en la base de datos</param>
        /// <returns>Retorna el datatable con los datos del material</returns>
        public DataTable buscarUsuarioRegular(string busqueda)
        {
            try
            {
                using (conexion)
                {
                    SqlCommand cmd = conexion.CreateCommand();
                    string sql = "SELECT COD_MATERIAL AS 'Código Material', NOMBRE_MATERIAL as 'Nombre', PRECIO_COMPRA_KILO as 'Precio Compra', PRECIO_VENTA_KILO as 'Precio Venta' FROM MATERIAL WHERE ESTADO_MATERIAL = 1";
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        sql += " and (COD_MATERIAL LIKE '%' + @pal + '%')  or (NOMBRE_MATERIAL LIKE '%' + @pal + '%') or (PRECIO_COMPRA_KILO LIKE '%' + @pal + '%') or (PRECIO_VENTA_KILO LIKE '%' + @pal + '%');";
                        cmd.Parameters.AddWithValue("@pal", busqueda);
                    }
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMaterial"></param>
        /// <returns></returns>
        public TOMaterial buscarMaterialAdmin(string idMaterial)
        {
            TOMaterial material = new TOMaterial();
            SqlCommand buscar = new SqlCommand("SELECT * FROM MATERIAL WHERE COD_MATERIAL = @cod;", conexion);
            buscar.Parameters.AddWithValue("@cod", idMaterial);

            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            SqlDataReader reader = buscar.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    material.codigoM = reader.GetString(0);
                    material.nombreMaterial = reader.GetString(1);
                    material.precioVentaK = Convert.ToDouble(reader.GetDecimal(2));
                    material.cod_Unidad = reader.GetString(3);
                    material.precioCompraK = Convert.ToDouble(reader.GetDecimal(4));
                    material.estado_Material = reader.GetBoolean(5);
                }
            }

            if (conexion.State != ConnectionState.Closed)
            {
                conexion.Close();
            }
            return material;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMaterial"></param>
        /// <returns></returns>
        public TOMaterial buscarMaterialRegular(string idMaterial)
        {
            TOMaterial material = new TOMaterial();
            SqlCommand buscar = new SqlCommand("SELECT * FROM MATERIAL WHERE ESTADO_MATERIAL = 1 AND COD_MATERIAL = @cod;", conexion);
            buscar.Parameters.AddWithValue("@cod", idMaterial);

            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            SqlDataReader reader = buscar.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    material.codigoM = reader.GetString(0);
                    material.nombreMaterial = reader.GetString(1);
                    material.precioVentaK = Convert.ToDouble(reader.GetDecimal(2));
                    material.cod_Unidad = reader.GetString(3);
                    material.precioCompraK = Convert.ToDouble(reader.GetDecimal(4));
                }
            }

            if (conexion.State != ConnectionState.Closed)
            {
                conexion.Close();
            }
            return material;
        }



        public void guardarModificarBodegaAdmin(TOMaterial mat)
        {


            using (conexion)
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                // Start a local transaction.
                SqlTransaction sqlTran = conexion.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand sentencia = conexion.CreateCommand();
                sentencia.Transaction = sqlTran;

                try
                {
                    if (String.IsNullOrEmpty(mat.codigoM))
                    {
                       
                        sentencia.CommandText ="insert into material(cod_material, nombre_material, precio_venta_kilo, cod_unidad, precio_compra_kilo, estado_material) " +
                            "values (@cod, @nombre, @precV, @codUnid, @precC, @estadoMat);";
                        sentencia.Parameters.AddWithValue("@cod", mat.codigoM);
                        sentencia.Parameters.AddWithValue("@nombre", mat.nombreMaterial);
                        sentencia.Parameters.AddWithValue("@precV", mat.precioVentaK);
                        sentencia.Parameters.AddWithValue("@codUnid", mat.cod_Unidad);
                        sentencia.Parameters.AddWithValue("@precC", mat.precioCompraK);
                        sentencia.Parameters.AddWithValue("@estadoMat", mat.estado_Material);

                        sentencia.ExecuteNonQuery();

                        sqlTran.Commit();
                        if (conexion.State != ConnectionState.Closed)
                        {
                            conexion.Close();
                        }

                    }
                    else
                    {
                        sentencia.CommandText =
                         "update material set nombre_material = @nombre, precio_venta_kilo = @precV, cod_unidad = @codUnid, precio_compra_kilo = @precC, estado_material = @estadoMat where cod_material = @cod;";
                        sentencia.Parameters.AddWithValue("@cod", mat.codigoM);
                        sentencia.Parameters.AddWithValue("@nombre", mat.nombreMaterial);
                        sentencia.Parameters.AddWithValue("@precV", mat.precioVentaK);
                        sentencia.Parameters.AddWithValue("@codUnid", mat.cod_Unidad);
                        sentencia.Parameters.AddWithValue("@precC", mat.precioCompraK);
                        sentencia.Parameters.AddWithValue("@estadoMat", mat.estado_Material);


                        sentencia.ExecuteNonQuery();
                        sqlTran.Commit();
                        if (conexion.State != ConnectionState.Closed)
                        {
                            conexion.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        sqlTran.Rollback();
                        throw;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

    }

}
