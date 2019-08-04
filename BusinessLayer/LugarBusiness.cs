using DataAccessLayer;
using Domain;

namespace BusinessLayer
{
    public class LugarBusiness
    {
        public Lugar Insert(Lugar lugar)
        {
            var EspacioDA = new LugarDataAccess();

            lugar = EspacioDA.Insert(lugar);

            return lugar;
        }
    }
}
