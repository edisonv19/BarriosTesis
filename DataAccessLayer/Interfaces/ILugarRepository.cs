using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface ILugarRepository
    {
        int Insert(Lugar lugar);
        Lugar GetByLatLng(Lugar lugar);
    }
}
