using BusinessLayer.Caches;
using BusinessLayer.Factories;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain;
using Domain.Interfaces;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using Utils.Helpers;

namespace BusinessLayer
{
    public class ViajeBusiness
    {
        // Repositories
        private IViajeRepository _viajeRepository;

        // Cache
        private ICache<IDataEncuesta> _cache;

        public ViajeBusiness()
        {
            _viajeRepository = new ViajeDataAccess();

            _cache = new DataCache<IDataEncuesta>(new DataEncuestaFactory());
        }

        public Viaje Insert(Viaje viaje)
        {
            viaje.IdViaje = _viajeRepository.Insert(viaje);

            return viaje;
        }

        public int InsertByExcel(FileInfo file)
        {
            int count = 0;
            // Obtengo la hoja de excel
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[1];

                // Get column Index
                int lat_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Latitud")).Start.Column;
                int lgn_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Longitud")).Start.Column;
                int ingreso_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Ingreso")).Start.Column;
                int sexo_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Sexo")).Start.Column;
                int estudios_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Estudios")).Start.Column;
                int ocupacion_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Ocupación")).Start.Column;
                int zonaResid_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Tipo de zona de residencia")).Start.Column;
                int estacion_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Estación")).Start.Column;

                int nombre_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Nombre")).Start.Column;
                int edad_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Edad")).Start.Column;

                // Recorro las filas del excel
                for (int i = 2; i <= excelWorksheet.Dimension.Rows; i++)
                {
                    // Get Lugar
                    Lugar lugar = new Lugar()
                    {
                        Latitud = excelWorksheet.Cells[i, lat_j].Value.GetDouble(),
                        Longitud = excelWorksheet.Cells[i, lgn_j].Value.GetDouble()
                    };

                    lugar = (Lugar)_cache.GetObject($"latlng_{lugar.Latitud};{lugar.Longitud}");

                    // Id SocioEconómico
                    Codigo nivelSocioEcon = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, ingreso_j].Value.GetString()
                    };

                    nivelSocioEcon = (Codigo)_cache.GetObject($"ingreso_{nivelSocioEcon.Clave}");

                    // Id Sexo
                    Codigo sexo = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, sexo_j].Value.GetString()
                    };

                    sexo = (Codigo)_cache.GetObject($"sexo_{nivelSocioEcon.Clave}");

                    // Id NivelEducativo
                    Codigo nivelEducativo = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, estudios_j].Value.GetString()
                    };

                    nivelEducativo = (Codigo)_cache.GetObject($"estudio_{nivelEducativo.Clave}");

                    // Id Ocupacion
                    Codigo ocupacion = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, ocupacion_j].Value.GetString()
                    };

                    ocupacion = (Codigo)_cache.GetObject($"ocupacion_{ocupacion.Clave}");

                    // Id Zona Residencial
                    Codigo zonaResidencial = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, zonaResid_j].Value.GetString()
                    };

                    // Id Estadion
                    Codigo estacion = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, estacion_j].Value.GetString()
                    };

                    estacion = (Codigo)_cache.GetObject($"estacion_{estacion.Clave}");

                    // Persona

                    Viaje viaje = new Viaje()
                    {
                        
                    };

                    viaje = Insert(viaje);

                    count = viaje.IdViaje == null ? count : count++;
                }
            }

            return count;
        }
    }
}
