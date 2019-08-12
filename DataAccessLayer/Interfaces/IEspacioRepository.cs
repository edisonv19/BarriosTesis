using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface IEspacioRepository
    {
        int Insert(Espacio espacio);
        Espacio GetByCodigo(Espacio espacio);
    }
}
