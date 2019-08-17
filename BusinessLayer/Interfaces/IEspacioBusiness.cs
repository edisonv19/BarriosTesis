using Domain;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IEspacioBusiness
    {
        Espacio GetByCodigo(Espacio espacio);
        IEnumerable<Espacio> GetByFilter(Espacio espacio);
        Espacio Insert(Espacio espacio);
        IEnumerable<Espacio> InsertList(List<Espacio> espacios);
    }
}
