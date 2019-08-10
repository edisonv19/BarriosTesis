using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain;

namespace BusinessLayer
{
    public class CodigoBusiness
    {
        private ICodigoRepository _codigoRepository;

        public CodigoBusiness()
        {
            _codigoRepository = new CodigoDataAccess();
        }

        public Codigo GetCodigoByClave(Codigo codigo)
        {
            return _codigoRepository.GetByClave(codigo);
        }
    }
}
