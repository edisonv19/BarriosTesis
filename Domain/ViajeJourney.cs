using System;

namespace Domain
{
    public class ViajeJourney
    {
        public int ViajeId { get; set; }
        public bool EsOrigenBase { get; set; }
        public bool EsDestinoBase { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaStr { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdDestino { get; set; }
    }
}
