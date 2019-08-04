using Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using Utils.Helpers;

namespace DataAccessLayer
{
    public class LugarDataAccess : DataAccess
    {
        public LugarDataAccess() : base() { }

        public Lugar Insert(Lugar lugar)
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

                    oComm.Parameters.Add(new SqlParameter("@Idlugar", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, null, DataRowVersion.Original, lugar.IdLugar));
                    oComm.Parameters.Add(new SqlParameter("@Calle", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, lugar.Calle));
                    oComm.Parameters.Add(new SqlParameter("@Numero", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, lugar.Numero));
                    oComm.Parameters.Add(new SqlParameter("@Latitud", SqlDbType.Decimal, 100, ParameterDirection.Input, false, 9, 7, null, DataRowVersion.Original, lugar.Latitud));
                    oComm.Parameters.Add(new SqlParameter("@Longitud", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 9, 7, null, DataRowVersion.Original, lugar.Longitud));
                    oComm.Parameters.Add(new SqlParameter("@IdRadioCensal", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, lugar.IdRadioCensal));
                    oComm.Parameters.Add(new SqlParameter("@IdZona", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, lugar.IdZona));
                    oComm.Parameters.Add(new SqlParameter("@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, lugar.IdCategoria));
                    oComm.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, lugar.Descripcion));
                    oComm.Parameters.Add(new SqlParameter("@Radio", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, lugar.Radio));

                    int rowsAffected = oComm.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se insertó ningún registro. Por favor, reintente la operación.");
                    }

                    lugar.IdLugar = (int)oComm.Parameters["@Idlugar"].Value;

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

            return lugar;
        }
    }
}
