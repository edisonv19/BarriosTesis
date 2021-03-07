using Domain;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IViajeRepository
    {
        int Insert(Viaje viaje);
        IEnumerable<ViajeJourney> GetByPersona(Persona persona);
    }
}
