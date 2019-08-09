using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using Utils.Helpers;

namespace DataAccessLayer
{
    public class LugarDataAccess : DataAccess, ILugarRepository
    {
        public LugarDataAccess() : base() { }

        public int Insert(Lugar lugar)
        {
            SqlConnection oConn = new SqlConnection(connectionString);
            oConn.Open();
            SqlTransaction oTran = oConn.BeginTransaction();
            int id;
            try
            {
                using (SqlCommand oComm = new SqlCommand())
                {
                    oComm.Connection = oConn;
                    oComm.Transaction = oTran;

                    oComm.CommandType = CommandType.StoredProcedure;
                    oComm.CommandText = $"{tableName}_{this.GetMethodName()}";

                    oComm.Parameters.Add(new SqlParameter("@Calle", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, lugar.Calle));
                    oComm.Parameters.Add(new SqlParameter("@Numero", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, lugar.Numero));
                    oComm.Parameters.Add(new SqlParameter("@Latitud", SqlDbType.Decimal, 100, ParameterDirection.Input, false, 9, 7, null, DataRowVersion.Original, lugar.Latitud));
                    oComm.Parameters.Add(new SqlParameter("@Longitud", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 9, 7, null, DataRowVersion.Original, lugar.Longitud));
                    oComm.Parameters.Add(new SqlParameter("@IdRadioCensal", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, lugar.IdRadioCensal));
                    oComm.Parameters.Add(new SqlParameter("@IdZona", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, lugar.IdZona));
                    oComm.Parameters.Add(new SqlParameter("@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, lugar.IdCategoria));
                    oComm.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, lugar.Descripcion));
                    oComm.Parameters.Add(new SqlParameter("@Radio", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, lugar.Radio));

                    id = (int)oComm.ExecuteScalar();

                    oTran.Commit();
                }
            }
            catch (Exception)
            {
                oTran.Rollback();
                throw new Exception($"Hubo un error al insertar a un {tableName} en la base de datos.");
            }
            finally
            {
                oConn.Close();
                oTran.Dispose();
            }

            return id;
        }

        public Lugar GetByLatLng(Lugar lugar)
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

                        oComm.Parameters.Add(new SqlParameter("@Latitud", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 9, 7, null, DataRowVersion.Original, lugar.Latitud));
                        oComm.Parameters.Add(new SqlParameter("@Longitud", SqlDbType.Decimal, 100, ParameterDirection.Input, false, 9, 7, null, DataRowVersion.Original, lugar.Longitud));
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
                return CreateItemFromRow<Lugar>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
    }
}
