using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain
{
    public class Espacio: IDataEncuesta
    {
        public int? IdEspacio { get; set; }
        public string name { get; set; }
        public string CoordenadasStr { get; set; }
        public int? IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public int? IdPadre { get; set; }
        public string Codigo { get; set; }

        public List<Coordenada> Coordenadas { get; set; }

        public bool contains(Coordenada location)
        {
            bool contains = false;
            for (int i = 0, j = this.Coordenadas.Count - 1; i < this.Coordenadas.Count; j = i++)
            {
                if (((this.Coordenadas[i].latitude > location.latitude) != (this.Coordenadas[j].latitude > location.latitude)) && (location.longitude < (this.Coordenadas[j].longitude - this.Coordenadas[i].longitude) * (location.latitude - this.Coordenadas[i].latitude) / (this.Coordenadas[j].latitude - this.Coordenadas[i].latitude) + this.Coordenadas[i].longitude))
                    contains = !contains;
            }
            return contains;
        }
    }
}
