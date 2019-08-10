using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface ICodigoRepository
    {
        Codigo GetByClave(Codigo codigo);
    }
}
