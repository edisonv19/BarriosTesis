using Domain;

namespace BusinessLayer.Interfaces
{
    public interface IViajeBusiness
    {
        Viaje Insert(Viaje viaje);
        int InsertByExcel(string pathFile);
    }
}
