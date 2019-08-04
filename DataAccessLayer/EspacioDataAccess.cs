using Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using Utils.Helpers;

namespace DataAccessLayer
{
    public class EspacioDataAccess : DataAccess
    {
        public EspacioDataAccess() : base() { }

        public Espacio Insert(Espacio espacio)
        {
            SqlConnection oConn = new SqlConnection(connectionString);
            oConn.Open();
            SqlTransaction oTran = oConn.BeginTransaction();

            try
            {
                using (SqlCommand oComm = new SqlCommand())
                {
                    oComm.Connection = oConn;
                    oComm.Transaction = oTran;

                    oComm.CommandType = CommandType.StoredProcedure;
                    oComm.CommandText = $"{tableName}_{this.GetMethodName()}";

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
            catch (Exception)
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

        public DataSet GetByCodigo(Espacio espacio)
        {
            // Creo la conexión y la transacción
            SqlConnection oConn = new SqlConnection(connectionString);
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

        public DataSet GetByFilter(Espacio espacio)
        {
            // Creo la conexión y la transacción
            SqlConnection oConn = new SqlConnection(connectionString);
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
