﻿using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using Utils.Helpers;

namespace DataAccessLayer
{
    public class PersonaDataAccess : DataAccess, IPersonaRepository
    {
        public PersonaDataAccess() : base() { }

        public int Insert(Persona persona)
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

                    oComm.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, persona.Nombre));
                    oComm.Parameters.Add(new SqlParameter("@Edad", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, persona.Edad));
                    oComm.Parameters.Add(new SqlParameter("@IdLugar", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, persona.IdLugar));
                    oComm.Parameters.Add(new SqlParameter("@IdSocioEconomico", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, persona.IdSocioEconomico));
                    oComm.Parameters.Add(new SqlParameter("@IdSexo", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, persona.IdSexo));
                    oComm.Parameters.Add(new SqlParameter("@IdNivelEducativo", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, persona.IdNivelEducativo));
                    oComm.Parameters.Add(new SqlParameter("@IdOcupacion", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, persona.IdOcupacion));
                    oComm.Parameters.Add(new SqlParameter("@IdEstacion", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Original, persona.IdEstacion));
                    oComm.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, persona.Identificacion));

                    id = (int)oComm.ExecuteScalar();

                    oTran.Commit();
                }
            }
            catch (Exception)
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

        public Persona GetByIdentificacion(Persona persona)
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

                        oComm.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Original, persona.Identificacion));

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
                return CreateItemFromRow<Persona>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
    }
}
