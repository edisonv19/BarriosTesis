using Domain.Interfaces;

namespace Domain
{
    public class Codigo : IDataEncuesta
    {
        public int? IdCodigo { get; set; }
        public string Valor { get; set; }
        public string Clave { get; set; }
        public string Grupo { get; set; }
        public string Descripcion { get; set; }
    }
}
