using Domain;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IEspacioRepository
    {
        int Insert(Espacio espacio);
        Espacio GetByCodigo(Espacio espacio);
        IEnumerable<Espacio> GetByFilter(Espacio espacio);
    }
}
