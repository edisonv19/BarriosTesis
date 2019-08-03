using System;

namespace Domain
{
    public class Viaje
    {
        public int? IdViaje { get; set; }
        public int? IdPersona { get; set; }

        public DateTime? Fecha { get; set; }
        public int IdOrigen { get; set; }
        public int IdTipoLugarOrigen { get; set; }
        public int IdDestino { get; set; }
        public int IdTipoLugarDestino { get; set; }
        public int IdMotivo { get; set; }

        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int IdTransporte { get; set; }
        public string Observaciones { get; set; }
    }
}
