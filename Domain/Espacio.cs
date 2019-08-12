using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain
{
    public class Espacio: IDataEncuesta
    {
        public int? IdEspacio { get; set; }
        public string name { get; set; }
        public string Coordenadas { get; set; }
        public int? IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public int? IdPadre { get; set; }
        public string Codigo { get; set; }

        public List<Coordenada> coordinates { get; set; }

        public bool contains(Coordenada location)
        {
            bool contains = false;
            for (int i = 0, j = this.coordinates.Count - 1; i < this.coordinates.Count; j = i++)
            {
                if (((this.coordinates[i].latitude > location.latitude) != (this.coordinates[j].latitude > location.latitude)) && (location.longitude < (this.coordinates[j].longitude - this.coordinates[i].longitude) * (location.latitude - this.coordinates[i].latitude) / (this.coordinates[j].latitude - this.coordinates[i].latitude) + this.coordinates[i].longitude))
                    contains = !contains;
            }
            return contains;
        }
    }
}
