using Domain;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface ILugarRepository
    {
        int Insert(Lugar lugar);
        Lugar GetByLatLng(Lugar lugar);
        IEnumerable<Lugar> GetByFilter(Lugar espacio);
        int Update(Lugar lugar);
    }
}
