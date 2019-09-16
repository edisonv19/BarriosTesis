using BusinessLayer;
using BusinessLayer.Caches;
using BusinessLayer.Factories;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain.Interfaces;
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
            //var personaService = serviceProvider.GetService<PersonaGenerator>();
            //var personas = personaService.GeneretePersons("E:/Tesis2/Excels/Prod/personas_with_zonas.xlsx");

            //var viajeService = serviceProvider.GetService<ViajeGenerator>();
            //var viajes = viajeService.GenereteViaje("E:/Tesis2/Excels/Prod/viajes_lat_lng.xlsx");

            var viajeService = serviceProvider.GetService<LugarGenerator>();
            var viajes = viajeService.UpdateRadioCensal();

            Console.WriteLine(viajes);
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // Services
            services.AddScoped(typeof(IAbstractFactory<IDataEncuesta>), typeof(DataEncuestaFactory));
            services.AddScoped(typeof(ICache<>), typeof(DataCache<>));
            services.AddScoped<ICodigoBusiness, CodigoBusiness>();
            services.AddScoped<ILugarBusiness, LugarBusiness>();
            services.AddScoped<IEspacioBusiness, EspacioBusiness>();
            services.AddScoped<IPersonaBusiness, PersonaBusiness>();
            services.AddScoped<IViajeBusiness, ViajeBusiness>();

            // Repositories
            services.AddScoped<ICodigoRepository, CodigoDataAccess>();
            services.AddScoped<ILugarRepository, LugarDataAccess>();
            services.AddScoped<IEspacioRepository, EspacioDataAccess>();
            services.AddScoped<IPersonaRepository, PersonaDataAccess>();
            services.AddScoped<IViajeRepository, ViajeDataAccess>();

            services.AddTransient(typeof(CodigoGenerator));
            services.AddTransient(typeof(PersonaGenerator));
            services.AddTransient(typeof(ViajeGenerator));
            services.AddTransient(typeof(LugarGenerator));

            return services.BuildServiceProvider();
        }
    }
}
