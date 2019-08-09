using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain;

namespace BusinessLayer
{
    public class ViajeBusiness
    {
        private IViajeRepository _viajeRepository;

        public ViajeBusiness()
        {
            _viajeRepository = new ViajeDataAccess();
        }

        public Viaje Insert(Viaje viaje)
        {
            viaje.IdViaje = _viajeRepository.Insert(viaje);

            return viaje;
        }
    }
}
