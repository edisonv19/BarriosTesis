using BusinessLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReadBarrios.Generators;
using System;

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
            //ViajeBusiness viajeBusiness = new ViajeBusiness();
            //viajeBusiness.Insert(new Domain.Viaje());

            // ---------------------------------------------------------------------------------------------

            // Setup our DI
            // Create service collection and configure our services
            //var serviceProvider = ConfigureServices();    // Generate a provider

            //// Kick off our actual code
            //serviceProvider.GetService<ConsoleApplication>().Run();
        }

        //private static IServiceProvider ConfigureServices()
        //{
        //    IServiceCollection services = new ServiceCollection();

        //    // Services
        //    services.AddScoped<ITestService, TestService>();
        //    services.AddTransient<ConsoleApplication>();

        //    return services.BuildServiceProvider();
        //}
    }
}
