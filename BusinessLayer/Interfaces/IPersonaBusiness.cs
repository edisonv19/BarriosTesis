using Domain;

namespace BusinessLayer.Interfaces
{
    public interface IPersonaBusiness
    {
        Persona Insert(Persona persona);
        int InsertByExcel(string pathFile);
        Persona GetByIdentificacion(Persona persona);
    }
}
