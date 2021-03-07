using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utils.Helpers;

namespace DataAccessLayer
{
    public class ViajeDataAccess : DataAccess, IViajeRepository
    {
        public ViajeDataAccess() : base(){ }

        public int Insert(Viaje viaje)
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

                    oComm.Parameters.Add(new SqlParameter("@IdPersona", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.IdPersona));
                    oComm.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.Fecha));
                    oComm.Parameters.Add(new SqlParameter("@IdOrigen", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.IdOrigen));
                    oComm.Parameters.Add(new SqlParameter("@IdTipoLugarOrigen", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.IdTipoLugarOrigen));
                    oComm.Parameters.Add(new SqlParameter("@IdDestino", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.IdDestino));
                    oComm.Parameters.Add(new SqlParameter("@IdTipoLugarDestino", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.IdTipoLugarDestino));
                    oComm.Parameters.Add(new SqlParameter("@IdMotivo", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.IdMotivo));
                    oComm.Parameters.Add(new SqlParameter("@HoraInicio", SqlDbType.Time, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.HoraInicio));
                    oComm.Parameters.Add(new SqlParameter("@HoraFin", SqlDbType.Time, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.HoraFin));
                    oComm.Parameters.Add(new SqlParameter("@IdTransporte", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.IdTransporte));
                    oComm.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.NVarChar, 200, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.Observaciones));
                    oComm.Parameters.Add(new SqlParameter("@FechaStr", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, viaje.FechaStr));

                    id = (int)oComm.ExecuteScalar();

                    oTran.Commit();
                }
            }
            catch (Exception ex)
            {
                oTran.Rollback();
                throw new Exception("Error to insert a Person");
            }
            finally
            {
                oConn.Close();
                oTran.Dispose();
            }

            return id;
        }

        public IEnumerable<ViajeJourney> GetByPersona(Persona persona)
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

                        oComm.Parameters.Add(new SqlParameter("@IdPersona", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, persona.IdPersona));

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
                return CreateListFromTable<ViajeJourney>(ds.Tables[0]);
            }
            return null;
        }
    }
}
