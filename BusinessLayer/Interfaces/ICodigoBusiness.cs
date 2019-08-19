using Domain;

namespace BusinessLayer.Interfaces
{
    public interface ICodigoBusiness
    {
        Codigo GetCodigoByClave(Codigo codigo);
    }
}
