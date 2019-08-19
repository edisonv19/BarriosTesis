using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;

namespace BusinessLayer
{
    public class CodigoBusiness : ICodigoBusiness
    {
        private readonly ICodigoRepository _codigoRepository;

        public CodigoBusiness(ICodigoRepository codigoRepository)
        {
            _codigoRepository = codigoRepository;
        }

        public Codigo GetCodigoByClave(Codigo codigo)
        {
            return _codigoRepository.GetByClave(codigo);
        }
    }
}
