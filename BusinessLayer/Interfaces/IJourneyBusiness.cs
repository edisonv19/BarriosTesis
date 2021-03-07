using Domain;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IJourneyBusiness
    {
        IEnumerable<IEnumerable<ViajeJourney>> GetJourneys(Persona persona);
    }
}
