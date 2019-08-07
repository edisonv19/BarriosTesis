using BusinessLayer;
using ReadBarrios.Generators;

namespace ReadBarrios
{
    public class Program
    {
        static void Main(string[] args)
        {
            //SpaceGenerator spaceGenerator = new SpaceGenerator("E:\\Tesis2\\ReadBarrios\\ReadBarrios\\Radios censales.kml");
            //spaceGenerator.Generate();

            //ZonaGenerator zonaGenerator = new ZonaGenerator("E:\\Tesis2\\Definitive\\Prod\\personas.xlsx", "E:\\Tesis2\\Definitive\\Prod\\personas_with_zonas.xlsx");
            //zonaGenerator.ReloadZonesPerson();
            ViajeBusiness viajeBusiness = new ViajeBusiness();

            viajeBusiness.Insert(new Domain.Viaje());
        }
    }
}
