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
    public class DAOManejadorCompras
    {

        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        private List<TODetalleCompra> listaDetalles = new List<TODetalleCompra>();

       
        public String registrarCompra()//RECIBE LOS PARAMETROS, SE ASUME QUE LAS CANTIDADES VIENEN EN KILOS,
        {
            String m = "";
            

            //DATOS DE PRUEBA QUEMADOS
            //{
            //encabezado compra
            int codCompra = 2;
            int idBodega = 1;
            String idProveedor = "001";
            String idMoneda = "COL";
            double montoCompra = 0;
            DateTime fecha = DateTime.Today;

            //detalles
            listaDetalles.Add(new TODetalleCompra(codCompra, 1, 78000, 52));
            listaDetalles.Add(new TODetalleCompra(codCompra, 4, 109950, 73.3));
            listaDetalles.Add(new TODetalleCompra(codCompra, 5, 116000, 58));
            //}

            /*
             * BORRAR
             * 
            DataTable tabla = new DataTable();
            tabla.Columns.Add(new DataColumn("COD_COMPRA", typeof(int)));
            tabla.Columns.Add(new DataColumn("COD_MATERIAL", typeof(int)));
            tabla.Columns.Add(new DataColumn("MONTO_LINEA_C", typeof(double)));
            tabla.Columns.Add(new DataColumn("KILOS_COMPRA", typeof(double)));
            */


            //poblar el DataTable
            foreach (var detalle in listaDetalles)
                montoCompra += detalle.montoLinea; //pasar a BL
                                                   //tabla.Rows.Add(detalle.codCompra, detalle.codMaterial, detalle.montoLinea, detalle.cantidadLinea);

            //TRANSACCION
            using (conexion)
            {
                conexion.Open();

                //Se inicializa la transaccion local
                SqlTransaction transaction = conexion.BeginTransaction();

                //Se asigna un comando a la transaccion
                SqlCommand command = conexion.CreateCommand();
                command.Transaction = transaction;

                //TEXTOS CONSULTAS 
                String sqlEncabezado = "INSERT INTO COMPRA (ID_BODEGA, CEDULA, ID_MONEDA, MONTO_TOTAL_C, FECHA_COMPRA)" +
                    "VALUES (@BODEGA,@CED,@MONEDA,@TOTAL,@FECHA)";
                String swqlCodCompra = "select IDENT_CURRENT('COMPRA')";
                String sqlDetalles = "INSERT INTO DETALLE_COMPRA (COD_COMPRA, COD_MATERIAL, KILOS_COMPRA, MONTO_LINEA_C)" +
                    "VALUES";
                String sqlSumarStock = "";


                try
                {
                 //REGISTRAR ENCABEZADO
                    command.CommandText = sqlEncabezado;
                    command.Parameters.AddWithValue("@BODEGA", idBodega); //validar formato en parametros(?)
                    command.Parameters.AddWithValue("@CED", idProveedor);
                    command.Parameters.AddWithValue("@MONEDA", idMoneda);
                    command.Parameters.AddWithValue("@TOTAL", montoCompra);//el monto debe venir calculado desde BL
                    command.Parameters.AddWithValue("@FECHA", fecha);
                    command.ExecuteNonQuery();

                 //EXTRAER CODIGO(IDENTITY) DE COMPRA_INGRESADA
                    command.CommandText = swqlCodCompra;
                    codCompra = Convert.ToInt32(command.ExecuteScalar());

                 //REGISTRAR DETALLES   (?)bloquear materiales(?)
                    foreach (var detalle in listaDetalles)
                       sqlDetalles += "(" + codCompra + "," + detalle.codMaterial + "," + detalle.cantidadLinea + "," + detalle.montoLinea + "),";
                    
                    sqlDetalles = sqlDetalles.Remove(sqlDetalles.Length - 1);
                    sqlDetalles += ";";
                    command.CommandText = sqlDetalles;
                    command.ExecuteNonQuery();

                 //SUMAR LOS MATERIALES COMPRADOS A STOCK DE LA BODEGA RESPECTIVA, 
                 //SE CONSTRUYE DOS PARTES DE CONSULTA DE UN "SWITCH" PARA ACTUALIZAR EL STOCK.
                    String sqlUpdateParte1 = "UPDATE STOCK SET KILOS_STOCK = CASE ";
                    String sqlUpdateParte2 = "WHERE (ID_BODEGA = @ID_BOD AND COD_MATERIAL IN (";

                    foreach (var detalle in listaDetalles) {
                        sqlUpdateParte1 += "WHEN COD_MATERIAL = " + detalle.codMaterial +
                        " THEN (KILOS_STOCK + " + detalle.cantidadLinea + ") ";

                        sqlUpdateParte2 += detalle.codMaterial+",";
                    }

                    sqlUpdateParte2 = sqlUpdateParte2.Remove(sqlUpdateParte2.Length - 1);
                    sqlUpdateParte2 += "));";
                    sqlSumarStock = sqlUpdateParte1 + " END "+sqlUpdateParte2;
                    command.Parameters.AddWithValue("@ID_BOD", idBodega);
                    command.CommandText = sqlSumarStock;
                    command.ExecuteNonQuery();

                 //COMMIT A LA TRANSACCION
                    transaction.Commit();
                    return "Compra registrada correctamente";
                }
                catch (Exception ex)
                {

                    m = "Ocurrió un error en la operación, contacte al administrador. Error: " + ex.Source;

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
                return m;
            }//CONEXION

        }//METODO REGISTRAR COMPRA



    }
}
