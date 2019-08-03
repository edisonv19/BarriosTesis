using System;
using System.Collections.Generic;
using System.Data;

namespace ReadBarrios.Models
{
    public class Espacio
    {
        public int? IdEspacio { get; set; }
        public string name { get; set; }
        public List<Coordenada> coordinates { get; set; }
        public string Coordenadas { get; set; }
        public int? IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public int? IdPadre { get; set; }
        public string Codigo { get; set; }

        public List<Coordinate> coordinates2 { get; set; }

        public bool contains(Coordinate location)
        {
            bool contains = false;
            for (int i = 0, j = this.coordinates2.Count - 1; i < this.coordinates2.Count; j = i++)
            {
                if (((this.coordinates2[i].latitude > location.latitude) != (this.coordinates2[j].latitude > location.latitude)) && (location.longitude < (this.coordinates2[j].longitude - this.coordinates2[i].longitude) * (location.latitude - this.coordinates2[i].latitude) / (this.coordinates2[j].latitude - this.coordinates2[i].latitude) + this.coordinates2[i].longitude))
                    contains = !contains;
            }
            return contains;
        }

        public static Espacio GetFromDataRow(DataRow row)
        {
            return new Espacio()
            {
                IdEspacio = Convert.ToInt32(row["IdEspacio"]),
                IdCategoria = Convert.ToInt32(row["IdCategoria"]),
                Codigo = Convert.ToString(row["Codigo"]),
                Descripcion = Convert.ToString(row["Descripcion"]),
                Coordenadas = Convert.ToString(row["Coordenadas"]),
                IdPadre = DBNull.Value.Equals(row["IdPadre"]) ? null : (int?)Convert.ToInt32(row["IdPadre"])
            };
        }

        public static List<Espacio> GetFromDS(DataSet ds)
        {
            if (ds.Tables.Count == 0) return null;

            List<Espacio> retList = new List<Espacio>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                retList.Add(GetFromDataRow(row));
            }

            return retList;
        }
    }
}
