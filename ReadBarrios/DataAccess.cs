﻿using ReadBarrios.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ReadBarrios
{
    public class DataAccess
    {
        private static string conexion = "Data Source=DESKTOP-KJ1KTE2\\SQLEXPRESS; Initial Catalog=Encuestas; Persist Security Info=False; User ID=user; Password=user";

        // ESPACIO
        public static Espacio InsertEspacio(Espacio espacio)
        {
            SqlConnection oConn = new SqlConnection(conexion);
            oConn.Open();
            SqlTransaction oTran = oConn.BeginTransaction();

            try
            {
                using (SqlCommand oComm = new SqlCommand())
                {
                    oComm.Connection = oConn;
                    oComm.Transaction = oTran;

                    oComm.CommandType = CommandType.StoredProcedure;
                    oComm.CommandText = "Espacio_Insert";

                    oComm.Parameters.Add(new SqlParameter("@IdEspacio", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, null, DataRowVersion.Original, espacio.IdEspacio));
                    oComm.Parameters.Add(new SqlParameter("@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, espacio.IdCategoria));
                    oComm.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, espacio.Descripcion));
                    oComm.Parameters.Add(new SqlParameter("@Coordenadas", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, espacio.Coordenadas));
                    oComm.Parameters.Add(new SqlParameter("@IdPadre", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, espacio.IdPadre));
                    oComm.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, espacio.Codigo));

                    int rowsAffected = oComm.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se insertó ningún registro. Por favor, reintente la operación.");
                    }

                    espacio.IdEspacio = (int)oComm.Parameters["@IdEspacio"].Value;

                    oTran.Commit();
                }
            }
            catch (Exception e)
            {
                oTran.Rollback();
                throw new Exception("Hubo un error al insertar a una compañia en la base de datos.");
            }
            finally
            {
                oConn.Close();
                oTran.Dispose();
            }

            return espacio;
        }

        public static DataSet GetCodigoByClave(Codigo codigo)
        {
            // Creo la conexión y la transacción
            SqlConnection oConn = new SqlConnection(conexion);
            oConn.Open();

            DataSet ds = new DataSet();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand oComm = new SqlCommand())
                    {
                        oComm.Connection = oConn;

                        oComm.CommandType = CommandType.StoredProcedure;
                        oComm.CommandText = "Codigo_GetByClave";

                        oComm.Parameters.Add(new SqlParameter("@Clave", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, codigo.Clave));
                        oComm.Parameters.Add(new SqlParameter("@Grupo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, codigo.Grupo));

                        adapter.SelectCommand = oComm;
                        adapter.Fill(ds);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oConn.Close();
            }

            return ds;
        }

        public static DataSet GetEspacioByCodigo(Espacio espacio)
        {
            // Creo la conexión y la transacción
            SqlConnection oConn = new SqlConnection(conexion);
            oConn.Open();

            DataSet ds = new DataSet();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand oComm = new SqlCommand())
                    {
                        oComm.Connection = oConn;

                        oComm.CommandType = CommandType.StoredProcedure;
                        oComm.CommandText = "Espacio_GetByCodigo";

                        oComm.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, espacio.Codigo));

                        adapter.SelectCommand = oComm;
                        adapter.Fill(ds);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oConn.Close();
            }

            return ds;
        }

        public static DataSet GetEspacioByFilter(Espacio espacio)
        {
            // Creo la conexión y la transacción
            SqlConnection oConn = new SqlConnection(conexion);
            oConn.Open();

            DataSet ds = new DataSet();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand oComm = new SqlCommand())
                    {
                        oComm.Connection = oConn;

                        oComm.CommandType = CommandType.StoredProcedure;
                        oComm.CommandText = "Espacio_GetByFilter";

                        oComm.Parameters.Add(new SqlParameter("@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, espacio.IdCategoria));
                        oComm.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, espacio.Descripcion));
                        oComm.Parameters.Add(new SqlParameter("@IdPadre", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, espacio.IdPadre));
                        oComm.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, espacio.Codigo));

                        adapter.SelectCommand = oComm;
                        adapter.Fill(ds);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oConn.Close();
            }

            return ds;
        }
    }
}
