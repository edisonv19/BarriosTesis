using Domain;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ILugarBusiness
    {
        Lugar Insert(Lugar lugar);
        Lugar GetByLatLng(Lugar lugar);
        IEnumerable<Lugar> GetByFilter(Lugar lugar);
        int ReloadRadiosCensales();
    }
}
