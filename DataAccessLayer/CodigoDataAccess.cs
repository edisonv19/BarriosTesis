using System;
using System.Data;
using System.Data.SqlClient;
using Domain;

namespace DataAccessLayer
{
    public class CodigoDataAccess
    {
        private const string conexion = "Data Source=DESKTOP-KJ1KTE2\\SQLEXPRESS; Initial Catalog=Encuestas; Persist Security Info=False; User ID=user; Password=user";
        /// <summary>
        /// Obtiene un código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public DataSet GetCodigoByClave(Codigo codigo)
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
    }
}