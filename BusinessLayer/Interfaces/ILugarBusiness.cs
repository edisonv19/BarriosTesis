using Domain;

namespace BusinessLayer.Interfaces
{
    public interface ILugarBusiness
    {
        Lugar Insert(Lugar lugar);
        Lugar GetByLatLng(Lugar lugar);
    }
}
