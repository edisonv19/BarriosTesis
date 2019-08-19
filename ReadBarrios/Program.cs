using BusinessLayer;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
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
            var serviceProvider = ConfigureServices();    // Generate a provider

            // Kick off our actual code
            var service = serviceProvider.GetService<CodigoGenerator>();
            var codigo = service.GetCode("3", "NivelEducativo");

            Console.WriteLine(codigo.Valor);
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // Services
            services.AddScoped<ICodigoBusiness, CodigoBusiness>();
            services.AddScoped<ILugarBusiness, LugarBusiness>();
            services.AddScoped<IEspacioBusiness, EspacioBusiness>();
            services.AddScoped<IPersonaBusiness, PersonaBusiness>();
            services.AddScoped<IViajeBusiness, ViajeBusiness>();

            // Repositories
            services.AddScoped<ICodigoRepository, CodigoDataAccess>();
            services.AddScoped<ILugarRepository, LugarDataAccess>();
            services.AddScoped<IEspacioRepository, EspacioDataAccess>();
            services.AddScoped<IPersonaBusiness, PersonaBusiness>();
            services.AddScoped<IViajeRepository, ViajeDataAccess>();

            services.AddTransient(typeof(CodigoGenerator));

            return services.BuildServiceProvider();
        }
    }
}
