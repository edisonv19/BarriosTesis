using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using Domain;
using Utils.Helpers;

namespace DataAccessLayer
{
    public class CodigoDataAccess : DataAccess, ICodigoRepository
    {
        public CodigoDataAccess() : base() { }

        public Codigo GetByClave(Codigo codigo)
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
                        oComm.CommandText = $"{tableName}_{this.GetMethodName()}";

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

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return CreateItemFromRow<Codigo>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
    }
}