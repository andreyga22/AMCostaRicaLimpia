﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TO;

namespace DAO {
    public class DAOManejadorSocios
    {


        private SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionHost);

        AMCRLEntities context = new AMCRLEntities();


        public Boolean agregarModificarSocio(TOSocioNegocio socio)
        {
            
            int direccion = agregarDireccionSocio(socio.direccion);
            

            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from SOCIO_NEGOCIO with (updlock, serializable)" +
                            "where CEDULA = @CEDULA) begin update SOCIO_NEGOCIO set COD_DIRECCION = @COD_DIRECCION, "
                            + "NOMBRE = @NOMBRE, ROL_SOCIO = @ROL_SOCIO, APELLIDO1 = @APELLIDO1, " +
                            "APELLIDO2 = @APELLIDO2, ESTADO_SOCIO = @ESTADO_SOCIO where CEDULA = @CEDULA;" +
                            " end else begin insert into SOCIO_NEGOCIO(CEDULA, COD_DIRECCION, NOMBRE, ROL_SOCIO, " +
                            "APELLIDO1, APELLIDO2, ESTADO_SOCIO) values(@CEDULA, @COD_DIRECCION, @NOMBRE," +
                            " @ROL_SOCIO, @APELLIDO1, @APELLIDO2, @ESTADO_SOCIO); END commit tran;", conexion);

                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();
                insertar.Parameters.AddWithValue("@COD_DIRECCION", direccion);
                insertar.Parameters.AddWithValue("@CEDULA", socio.cedula);
                insertar.Parameters.AddWithValue("@NOMBRE", socio.nombre);
                insertar.Parameters.AddWithValue("@ROL_SOCIO", socio.rol);
                insertar.Parameters.AddWithValue("@APELLIDO1", socio.apellido1);
                insertar.Parameters.AddWithValue("@APELLIDO2", socio.apellido2);
                
            if (socio.estado_socio == true) {
                    insertar.Parameters.AddWithValue("@ESTADO_SOCIO", 1);
                }
                else
                {
                    insertar.Parameters.AddWithValue("@ESTADO_SOCIO", 0);
                }
                
                insertar.ExecuteNonQuery();

                conexion.Close();

            Boolean contacto = agregarContactosSocio(socio.cedula, socio.contactos);

            return true;
        }

        private int agregarDireccionSocio(TODireccion direccion)
        {

            int codigo = 0;
            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from DIRECCION with (updlock, serializable)" +
                            "where COD_DIRECCION = @COD_DIRECCION) begin update DIRECCION set PROVINCIA = @PROVINCIA, "
                            + "CANTON = @CANTON, DISTRITO = @DISTRITO, OTRAS_SENNAS = @SENNAS;" +
                            " end else begin insert into DIRECCION(PROVINCIA, CANTON, DISTRITO, OTRAS_SENNAS) values" +
                            "(@PROVINCIA, @CANTON, @DISTRITO, @SENNAS); END commit tran;", conexion);

            insertar.Parameters.AddWithValue("@PROVINCIA", direccion.provincia);
            insertar.Parameters.AddWithValue("@CANTON", direccion.canton);
            insertar.Parameters.AddWithValue("@DISTRITO", direccion.distrito);
            insertar.Parameters.AddWithValue("@SENNAS", direccion.otras_sennas);

            SqlCommand buscarCodigo = new SqlCommand("Select COD_DIRECCION from DIRECCION where OTRAS_SENNAS = @SENNAS;", conexion);
            buscarCodigo.Parameters.AddWithValue("@SENNAS", direccion.otras_sennas);

            if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

            SqlDataReader reader = buscarCodigo.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    codigo = reader.GetInt32(0);
                }
            }
            reader.Close();


            if (direccion.cod_direccion != 0) {
                insertar.Parameters.AddWithValue("@COD_DIRECCION", direccion.cod_direccion);
            }
            else
            {
                insertar.Parameters.AddWithValue("@COD_DIRECCION", codigo);
            }

            insertar.ExecuteNonQuery();
            conexion.Close();

            

            return codigo;
        }


        private Boolean agregarContactosSocio(String cedula, TOContactos contactos)
        {
            SqlCommand insertar = new SqlCommand("BEGIN Tran if exists(select * from CONTACTOS with (updlock, serializable)" +
                            "where CEDULA = @CEDULA) begin update CONTACTOS set CEDULA = @CEDULA, "
                            + "TELEFONO_HAB = @TEL_HAB, TELEFONO_PERS = @TEL_PERS, EMAIL = @EMAIL;" +
                            " end else begin insert into CONTACTOS(CEDULA, TELEFONO_HAB, TELEFONO_PERS, EMAIL) values" +
                            "(@CEDULA, @TEL_HAB, @TEL_PERS, @EMAIL); END commit tran;", conexion);

                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                insertar.Parameters.AddWithValue("@CEDULA", cedula);
                insertar.Parameters.AddWithValue("@TEL_HAB", contactos.telefono_hab);
                insertar.Parameters.AddWithValue("@TEL_PERS", contactos.telefono_pers);
                insertar.Parameters.AddWithValue("@EMAIL", contactos.email);

                insertar.ExecuteNonQuery();

                conexion.Close();

            
            return true;
        }

        //ajustar al filtro, revisar el null.
        public DataTable buscarSociosFiltro(String busqueda)
        {
            try {
                using (conexion)
                {
                    SqlCommand buscar = conexion.CreateCommand();
                    String sql = "Select cedula, nombre, apellido1, apellido2, rol from SOCIO_NEGOCIO";
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        sql += busqueda;
                        buscar.CommandText = sql;
                        buscar.Connection = conexion;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(buscar))
                        {
                            DataTable tabla = new DataTable();
                            adapter.Fill(tabla);
                            return tabla;
                        }


                    }
                }
                
            }catch(Exception ex){
                throw;
            }
            return null;
        }

        public TODireccion buscarDireccion(String cedula)
        {
            SqlCommand buscar = new SqlCommand("Select PROVINCIA, CANTON, DISTRITO, OTRAS_SENNAS from DIRECCION where CEDULA = @CEDULA", conexion);
            TODireccion direccion = new TODireccion();
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            SqlDataReader reader = buscar.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    direccion.provincia = reader.GetString(0);
                    direccion.canton = reader.GetString(1);
                    direccion.distrito = reader.GetString(2);
                    direccion.otras_sennas = reader.GetString(3);
                }
            }
            else
            {
                return direccion;
            }
            return direccion;
        }

        public TOContactos buscarContacto(String cedula)
        {
            SqlCommand buscarContacto = new SqlCommand("Select TELEFONO_HAB, TELEFONO_PERS, EMAIL from CONTACTOS where CEDULA = @CEDULA", conexion);
            TOContactos contacto = new TOContactos();
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            SqlDataReader reader = buscarContacto.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    contacto.telefono_hab = Int32.Parse(reader.GetString(0));
                    contacto.telefono_pers = Int32.Parse(reader.GetString(1));
                    contacto.email = reader.GetString(2);
                }
            }
            else
            {
                return contacto;
            }
            return contacto;
        }

        public Boolean asociarSocio(String cedulaAsociado, String cedulaSocio) {
            SqlCommand asociar = new SqlCommand("Insert into Asociaciones (SOCIO, ASOCIADO) values (@SOCIO, @ASOCIADO)", conexion);
            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();

                asociar.Parameters.AddWithValue("@SOCIO", cedulaSocio);
                asociar.Parameters.AddWithValue("@ASOCIADO", cedulaAsociado);

                asociar.ExecuteNonQuery();
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
                return true;
            }
            return false;  
        }
    }
}
