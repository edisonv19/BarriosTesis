using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class ViajeDataAccess
    {
        private static string conexion = "Data Source=DESKTOP-KJ1KTE2\\SQLEXPRESS; Initial Catalog=Encuestas; Persist Security Info=False; User ID=user; Password=user";
        public static Viaje InsertPersona(Viaje viaje)
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
                    oComm.CommandText = "Persona_Insert";

                    oComm.Parameters.Add(new SqlParameter("@IdViaje", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, null, DataRowVersion.Original, viaje.IdViaje));
                    oComm.Parameters.Add(new SqlParameter("@IdPersona", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.IdPersona));
                    oComm.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.Fecha));
                    oComm.Parameters.Add(new SqlParameter("@IdOrigen", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.IdOrigen));
                    oComm.Parameters.Add(new SqlParameter("@IdTipoLugarOrigen", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.IdTipoLugarOrigen));
                    oComm.Parameters.Add(new SqlParameter("@IdDestino", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.IdDestino));
                    oComm.Parameters.Add(new SqlParameter("@IdTipoLugarDestino", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.IdTipoLugarDestino));
                    oComm.Parameters.Add(new SqlParameter("@IdMotivo", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.IdMotivo));
                    oComm.Parameters.Add(new SqlParameter("@HoraInicio", SqlDbType.Time, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.HoraInicio));
                    oComm.Parameters.Add(new SqlParameter("@HoraFin", SqlDbType.Time, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.HoraFin));
                    oComm.Parameters.Add(new SqlParameter("@IdTransporte", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.IdTransporte));
                    oComm.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, viaje.Observaciones));
                    int rowsAffected = oComm.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Couldn't insert any rows");
                    }

                    viaje.IdViaje = (int)oComm.Parameters["@IdViaje"].Value;

                    oTran.Commit();
                }
            }
            catch (Exception e)
            {
                oTran.Rollback();
                throw new Exception("Error to insert a Person");
            }
            finally
            {
                oConn.Close();
                oTran.Dispose();
            }

            return viaje;
        }
    }
}
