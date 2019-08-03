using System;
using System.Collections.Generic;
using System.Data;

namespace ReadBarrios.Models
{
    public class Codigo
    {
        public int? IdCodigo { get; set; }
        public string Valor { get; set; }
        public string Clave { get; set; }
        public string Grupo { get; set; }
        public string Descripcion { get; set; }

        public static Codigo GetFromDataRow(DataRow row)
        {
            return new Codigo()
            {
                IdCodigo = Convert.ToInt32(row["IdCodigo"]),
                Valor = Convert.ToString(row["Valor"]),
                Clave = Convert.ToString(row["Clave"]),
                Grupo = Convert.ToString(row["Grupo"]),
                Descripcion = Convert.ToString(row["Descripcion"])
            };
        }

        public static List<Codigo> GetFromDS(DataSet ds)
        {
            if (ds.Tables.Count == 0) return null;

            List<Codigo> retList = new List<Codigo>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                retList.Add(GetFromDataRow(row));
            }

            return retList;
        }
    }
}
