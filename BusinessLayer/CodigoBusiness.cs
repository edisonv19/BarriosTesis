using DataAccessLayer;
using Domain;
using System.Data;

namespace BusinessLayer
{
    public class CodigoBusiness
    {
        public Codigo GetCodigoByClave(Codigo codigo)
        {
            var codigoDA = new CodigoDataAccess();
            DataSet ds = codigoDA.GetByClave(codigo);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return Codigo.GetFromDataRow(ds.Tables[0].Rows[0]);
            }
            return null;
        }
    }
}
