using BusinessLayer.Interfaces;

namespace ReadBarrios.Generators
{
    public class ViajeGenerator
    {
        private readonly IViajeBusiness _viajeBusiness;

        public ViajeGenerator(IViajeBusiness viajeBusiness)
        {
            _viajeBusiness = viajeBusiness;
        }

        public int GenereteViaje(string filePath)
        {
            return _viajeBusiness.InsertByExcel(filePath);
        }
    }
}
