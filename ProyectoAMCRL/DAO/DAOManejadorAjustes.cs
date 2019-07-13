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
            //id_bod, peso, material, unidad, accion, razon
            //---------------------------CORREGIR FECHAS------------------------------
            String sql = "SELECT pk.Fecha_Ajuste, aj.ID_AJUSTE, COUNT(ID_AJUSTE_STOCK) AS MATERIALES, pk.MOVIMIENTO_A, s.ID_BODEGA FROM AJUSTE_STOCK aj inner join " +
            " (select a.ID_AJUSTE, (convert(varchar, a.[Fecha_Ajuste], 103)) as Fecha_Ajuste, a.MOVIMIENTO_A from AJUSTE a) pk " +
            " on(pk.ID_AJUSTE = aj.ID_AJUSTE) inner join(select* from STOCK) s ON(aj.ID_STOCK = s.ID_STOCK) "+
            " GROUP BY aj.ID_AJUSTE , pk.MOVIMIENTO_A, s.ID_BODEGA, Fecha_Ajuste"; 
            SqlCommand cmd = new SqlCommand(sql, conexion);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet("ajustes");
            sda.Fill(ds);

            return ds;


        }

        public string registrarAjusteDAO(String id_bod, string accion, string razon, List<TODetalleAjuste> lineas, String fechaS)
        {
            String msg = "";
            String[] fechaInfo = fechaS.Split('/');
            int anio = Int32.Parse(fechaInfo[2]);
            int mes = Int32.Parse(fechaInfo[1]);
            int dia = Int32.Parse(fechaInfo[0]);

            DateTime fecha = new DateTime(anio, mes, dia);

            //PARTE 1: INSERTAR ENCABEZADO
            String sqlAjuste = "INSERT INTO AJUSTE ([RAZON], [MOVIMIENTO_A], [FECHA_AJUSTE]) " +
                "VALUES (@RAZON, @MOVIMIENTO, @Fecha)";
           

            //PARTE 2: INSERTAR A CROSSTABLE
            String swqlCodAjuste= "select IDENT_CURRENT('AJUSTE')";

            String sqlAjusteStock = "INSERT INTO AJUSTE_STOCK ([PESO_AJUSTE], [ID_AJUSTE], [ID_STOCK] ) " +
                "VALUES ";

            //PARTE 3: ACTUALIZAR STOCK (SUMAR O RESTAR A INVENTARIO)

            //PARTE 3: ACTUALIZAR STOCK (SUMAR O RESTAR A INVENTARIO)
            char operacion = accion.Equals("1") ? '+' : '-';
            
            String sqlStock = "";

           
            //INICIO
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

                    //----------------------------------PARTE 1: INSERTAR ENCABEZADO
                    //[RAZON], [MOVIMIENTO_A], [FECHA_AJUSTE]
                    command.CommandText = sqlAjuste;

                    command.Parameters.AddWithValue("@RAZON", razon);
                    command.Parameters.AddWithValue("@MOVIMIENTO", accion);
                    command.Parameters.AddWithValue("@Fecha", fecha);

                    command.ExecuteNonQuery();

                    //----------------------------------PARTE 2: INSERTAR A CROSSTABLE
                    //[PESO_AJUSTE], [ID_AJUSTE], [ID_STOCK]

                    //EXTRAER CODIGO(IDENTITY) DE COMPRA_INGRESADA
                    command.CommandText = swqlCodAjuste;
                    int codAj= Convert.ToInt32(command.ExecuteScalar());


                    foreach (TODetalleAjuste detalle in lineas)
                        sqlAjusteStock += "(" + detalle.kilos_Linea + ", " + codAj + "," + detalle.id_Stock + "),";

                    sqlAjusteStock = sqlAjusteStock.Remove(sqlAjusteStock.Length - 1);
                    sqlAjusteStock += ";";
                    command.CommandText = sqlAjusteStock;
                    command.ExecuteNonQuery();

                    //----------------------------------PARTE 3: ACTUALIZAR STOCK (SUMAR O RESTAR A INVENTARIO)

                    //SUMAR LOS MATERIALES DEL AJUSTE A STOCK DE LA BODEGA RESPECTIVA, 
                    //SE CONSTRUYE DOS PARTES DE CONSULTA DE UN "SWITCH" PARA ACTUALIZAR EL STOCK.
                    String sqlUpdateParte1 = "UPDATE STOCK SET KILOS_STOCK = CASE ";
                    String sqlUpdateParte2 = "WHERE (ID_BODEGA = @ID_BOD AND COD_MATERIAL IN (";

                    foreach (var detalle in lineas)
                    {
                        sqlUpdateParte1 += "WHEN COD_MATERIAL = '" + detalle.id_Material +
                        "' THEN (KILOS_STOCK " + operacion + " " + detalle.kilos_Linea + ") ";

                        sqlUpdateParte2 += "'"+detalle.id_Material + "',";
                    }

                    sqlUpdateParte2 = sqlUpdateParte2.Remove(sqlUpdateParte2.Length - 1);
                    sqlUpdateParte2 += "));";
                    sqlStock = sqlUpdateParte1 + " END " + sqlUpdateParte2;
                    command.Parameters.AddWithValue("@ID_BOD", id_bod);
                    command.CommandText = sqlStock;

                    //puede tirar excepcion del trigger
                    command.ExecuteNonQuery();

                    //COMMIT A LA TRANSACCION
                    transaction.Commit();

                    return "Ajuste realizado correctamente";
                }
                catch (Exception ex)
                {

                    String mensaje = ex.Message;
                    String[] mensajeArray = mensaje.Split('*');
                    mensaje = mensajeArray[0].Remove(mensajeArray[0].Length - 2);
                    msg = mensaje;

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

        public DataSet filtrarAjustesDAO(string fechaInicio, string fechaFin, string tipo, string pesoMaximo, string pesoMinimo, string bodega, List<string> materiales)
        {
            DataSet dataSet = new DataSet("ajustes");
            String parteFechas = "";
            String parteTipo = "";
            String tipoBD = "";
            switch (tipo) {
                case "Entrada":
                    tipoBD = "1";
                    break;

                case "Salida":
                    tipoBD = "0";
                    break;

                default:
                    tipoBD = "";
                    break;
            }
            String partePeso= "";
            String parteBodega = "";
            String parteMateriales = "";
            

            

            try
            {
            if (!String.IsNullOrEmpty(fechaInicio)) {
             String[] fecha = fechaInicio.Split('/');
             fechaInicio = fecha[2] + "-" + fecha[1] + "-" + fecha[0];
             parteFechas = " and pk.Fecha_Ajuste >= '" + fechaInicio+"' ";
             }

            if (!String.IsNullOrEmpty(fechaFin)) {
             String[] fecha = fechaFin.Split('/');
             fechaFin = fecha[2] + "-" + fecha[1] + "-" + fecha[0];

               if (!String.IsNullOrEmpty(fechaInicio))
                    parteFechas = " and pk.Fecha_Ajuste >= '" + fechaInicio+ "' and pk.Fecha_Ajuste <= '" + fechaFin+"' ";
               else
                    parteFechas = " and pk.Fecha_Ajuste <= '" + fechaFin+"' ";
            }

            if (!String.IsNullOrEmpty(tipo)) 
                parteTipo = "and pk.MOVIMIENTO_A = " + tipoBD +" ";


            if (!String.IsNullOrEmpty(pesoMaximo))
                    partePeso= "HAVING COUNT(ID_AJUSTE_STOCK) <= " +pesoMaximo+" ";

                if (!String.IsNullOrEmpty(pesoMinimo))
                    if (!String.IsNullOrEmpty(pesoMaximo))
                    {
                        partePeso = "HAVING COUNT(ID_AJUSTE_STOCK) >= " + pesoMinimo + " and " +
                            "HAVING COUNT(ID_AJUSTE_STOCK) <= " + pesoMaximo + " ";
                    }
                    else
                        partePeso = "HAVING COUNT(ID_AJUSTE_STOCK) >= " + pesoMinimo + " ";
               

            if (!String.IsNullOrEmpty(bodega))
                parteBodega = "AND s.ID_BODEGA = '"+bodega.Trim(' ')+"' ";

            if (materiales.Count > 0) {
                    parteMateriales =  " AND (s.COD_MATERIAL in (";

                foreach (String id in materiales) 
                    parteMateriales += " "+id+" ,";

                parteMateriales = parteMateriales.Remove(parteMateriales.Length-1);
                parteMateriales += " ))";

            }

                //---------------------------CORREGIR FECHAS------------------------------
                String sqlCuerpo = "SELECT pk.Fecha_Ajuste, aj.ID_AJUSTE, COUNT(ID_AJUSTE_STOCK) AS MATERIALES, pk.MOVIMIENTO_A, s.ID_BODEGA FROM AJUSTE_STOCK aj inner join " +
            " (select a.ID_AJUSTE, (convert(varchar, [Fecha_Ajuste], 103)) as Fecha_Ajuste, a.MOVIMIENTO_A from AJUSTE a) pk " +
            " on(pk.ID_AJUSTE = aj.ID_AJUSTE) inner join(select* from STOCK) s ON(aj.ID_STOCK = s.ID_STOCK) WHERE ('S'='S' " ;

                String sqlFinal = ") GROUP BY aj.ID_AJUSTE , pk.MOVIMIENTO_A, s.ID_BODEGA, Fecha_Ajuste ";
                   

            String[] sqlArray = {parteBodega, parteFechas, parteTipo, parteMateriales};
              
                for (int i = 0;i < sqlArray.Length; i++ ) {
                    String parte = sqlArray[i];

                    if (!parte.Equals("")) {
                        sqlCuerpo += parte;
                    }
                }
                String sql = sqlCuerpo +sqlFinal+ partePeso;

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conexion);
                adapter.Fill(dataSet);
            }
            catch (Exception e) {
                return null;
            }


           return dataSet;
        }

        public TOAjuste buscarAjusteDAO(string idAjuste)
        {
            TOAjuste ajuste = new TOAjuste();
            int ajusteN = Int32.Parse(idAjuste);
            //PRIMERA PARTE: TRAER ENCABEZADO
            String sqlEncabezado = "SELECT ID_AJUSTE, (convert(varchar, [Fecha_Ajuste], 103)) as Fecha_Ajuste, MOVIMIENTO_A, RAZON  FROM AJUSTE WHERE ID_AJUSTE = @ID_AJUSTE";

            //SEGUNDA PARTE: TRAER DETALLES
            //--nombre_material, cantidad unidad(Kg)
            String sqlDetalles = "SELECT m.NOMBRE_MATERIAL, aj.PESO_AJUSTE FROM AJUSTE_STOCK aj, MATERIAL m, STOCK s " +
                "where(aj.ID_STOCK = s.ID_STOCK and m.COD_MATERIAL = s.COD_MATERIAL and aj.ID_AJUSTE = @ID_AJUSTE)";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
          
                try
                {

                    //PRIMERA PARTE: TRAER ENCABEZADO
                    cmd.CommandText = sqlEncabezado;
                    cmd.Parameters.AddWithValue("@ID_AJUSTE", ajusteN);

                    conexion.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //fecha, movimiento, peso, nombreMaterial, nombreBodega, razon
                        String fecha = (String)reader.GetValue(1).ToString();
                        String movN = (String)reader.GetSqlString(2).ToString();
                        String movimiento = movN.Equals("1") ? "ENTRADA" : "SALIDA";
                        String razon = reader.GetSqlString(3).ToString();
                        ajuste.idAjuste = Int32.Parse(idAjuste);

                        String[] fechaInfo = fecha.Split('/');
                        

                    int dia = Int32.Parse(fechaInfo[0]);
                    int mes = Int32.Parse(fechaInfo[1]);
                    int anio = Int32.Parse(fechaInfo[2]);
                    DateTime fechaAjuste = new DateTime(anio, mes, dia);

                    ajuste.fecha = fechaAjuste;
                        ajuste.accion = movimiento;
                        ajuste.razon = razon;             
                    }
                    reader.Close();

                    //SEGUNDA PARTE: TRAER DETALLES
                    cmd.CommandText = sqlDetalles;
                    SqlDataReader reader2 = cmd.ExecuteReader();
                    List<TODetalleAjuste> detalles = new List<TODetalleAjuste>();

                    while (reader2.Read())
                    {
                        TODetalleAjuste nuevo = new TODetalleAjuste();
                        String nomMaterial = reader2.GetSqlString(0).ToString();
                        Double peso = Double.Parse(reader2.GetDecimal(1).ToString());
                        nuevo.id_Material = nomMaterial;
                        nuevo.kilos_Linea = peso;
                        nuevo.unidadMedida = "KILOS";
                        detalles.Add(nuevo);
                    }
                    reader.Close();
                    ajuste.detalles = detalles;

                    conexion.Close();
                }
                catch (Exception e) {
                conexion.Close();
                return null;
                }

            return ajuste;
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
