using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;

namespace BusinessLayer
{
    public class LugarBusiness : ILugarBusiness
    {
        private readonly ILugarRepository _lugarRepository;
        public LugarBusiness(ILugarRepository lugarRepository)
        {
            _lugarRepository = lugarRepository;
        }
        public Lugar Insert(Lugar lugar)
        {
            lugar.IdLugar = _lugarRepository.Insert(lugar);
            return lugar;
        }

        public Lugar GetByLatLng(Lugar lugar)
        {
            return _lugarRepository.GetByLatLng(lugar);
        }
    }
}
