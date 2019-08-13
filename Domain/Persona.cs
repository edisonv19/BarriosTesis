using Domain.Interfaces;

namespace Domain
{
    public class Persona: IDataEncuesta
    {
        public int? IdPersona { get; set; }
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public int? IdLugar { get; set; }
        public int? IdSocioEconomico { get; set; }
        public int? IdSexo { get; set; }
        public int? IdNivelEducativo { get; set; }
        public int? IdOcupacion { get; set; }
        public int? IdEstacion { get; set; }
        public string Identificacion { get; set; }
    }
}
