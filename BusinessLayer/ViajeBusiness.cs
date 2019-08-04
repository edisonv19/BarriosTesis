using DataAccessLayer;
using Domain;

namespace BusinessLayer
{
    public class ViajeBusiness
    {
        public Viaje Insert(Viaje viaje)
        {
            var ViajeDA = new ViajeDataAccess();

            viaje = ViajeDA.Insert(viaje);

            return viaje;
        }
    }
}
