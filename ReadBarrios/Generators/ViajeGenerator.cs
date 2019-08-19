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

        public void GenereteViaje(string filePath)
        {
            _viajeBusiness.InsertByExcel(filePath);
        }
    }
}
