using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain;

namespace BusinessLayer
{
    public class LugarBusiness
    {
        private ILugarRepository _lugarRepository;
        public LugarBusiness()
        {
            _lugarRepository = new LugarDataAccess();
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
