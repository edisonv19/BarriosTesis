using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface IPersonaRepository
    {
        int Insert(Persona persona);
        Persona GetByCodigo(Persona persona);
    }
}
