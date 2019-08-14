using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface IPersonaRepository
    {
        int Insert(Persona persona);
        Persona GetByIdentificacion(Persona persona);
    }
}
