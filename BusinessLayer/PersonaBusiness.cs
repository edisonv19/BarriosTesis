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
    public class PersonaBusiness
    {
        // Repositories
        private IPersonaRepository _personaRepository;

        // Cache
        private ICache<IDataEncuesta> _cache;

        public PersonaBusiness()
        {
            _personaRepository = new PersonaDataAccess();
            _cache = new DataCache<IDataEncuesta>(new DataEncuestaFactory());
        }

        public Persona Insert(Persona persona)
        {
            persona.IdPersona = _personaRepository.Insert(persona);

            return persona;
        }

        public int InsertByExcel(FileInfo file)
        {
            int count = 0;

            // Obtengo la hoja de excel
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[1];

                // Get column Index
                int zona_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Zona")).Start.Column;
                int calle_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Calle")).Start.Column;
                int nroPostal_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("NroPostal")).Start.Column;
                int nombre_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Nombre")).Start.Column;
                int ingreso_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Ingreso")).Start.Column;
                int sexo_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Sexo")).Start.Column;
                int edad_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Edad")).Start.Column;
                int estudios_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Estudios")).Start.Column;
                int ocupacion_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Ocupación")).Start.Column;
                int zonaResid_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Tipo de zona de residencia")).Start.Column;
                int estacion_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Estación")).Start.Column;
                int lat_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Latitud")).Start.Column;
                int lgn_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Longitud")).Start.Column;

                // Recorro las filas del excel
                for (int i = 2; i <= excelWorksheet.Dimension.Rows; i++)
                {
                    // Id SocioEconómico
                    Codigo nivelSocioEcon = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, ingreso_j].Value.GetString()
                    };

                    nivelSocioEcon = (Codigo)_cache.GetObject($"codigo_NivelSocioEconomico;{nivelSocioEcon.Clave}");

                    // Id Sexo
                    Codigo sexo = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, sexo_j].Value.GetString()
                    };

                    sexo = (Codigo)_cache.GetObject($"codigo_Sexo;{nivelSocioEcon.Clave}");

                    // Id NivelEducativo
                    Codigo nivelEducativo = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, estudios_j].Value.GetString()
                    };

                    nivelEducativo = (Codigo)_cache.GetObject($"codigo_NivelEducativo;{nivelEducativo.Clave}");

                    // Id Ocupacion
                    Codigo ocupacion = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, ocupacion_j].Value.GetString()
                    };

                    ocupacion = (Codigo)_cache.GetObject($"codigo_Ocupacion;{ocupacion.Clave}");

                    // Id Zona Residencial
                    Codigo zonaResidencial = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, zonaResid_j].Value.GetString()
                    };

                    zonaResidencial = (Codigo)_cache.GetObject($"codigo_TipoZonaResidencia;{zonaResidencial.Clave}");

                    // Id Estadion
                    Codigo estacion = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, estacion_j].Value.GetString()
                    };

                    estacion = (Codigo)_cache.GetObject($"codigo_Estacion;{estacion.Clave}");

                    // Id Estadion
                    Codigo zona = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, zona_j].Value.GetString()
                    };

                    estacion = (Codigo)_cache.GetObject($"espacio_{estacion.Clave}");

                    // Id Lugar
                    Lugar lugar = new Lugar()
                    {
                        Latitud = excelWorksheet.Cells[i, lat_j].Value.GetDouble(),
                        Longitud = excelWorksheet.Cells[i, lgn_j].Value.GetDouble()
                    };

                    var lugarCache = (Lugar)_cache.GetObject($"lugar_{lugar.Latitud};{lugar.Longitud}");

                    // Not exists en db => insert
                    if (lugarCache == null)
                    {
                        lugar.Calle = excelWorksheet.Cells[i, calle_j].Value.GetString();
                        lugar.Numero = excelWorksheet.Cells[i, nroPostal_j].Value.GetString();
                    }

                    // Persona

                    Persona persona = new Persona()
                    {
                        Nombre = excelWorksheet.Cells[i, nombre_j].Value.GetString(),
                        Edad = excelWorksheet.Cells[i, edad_j].Value.GetInt(),
                        IdLugar = lugar.IdLugar,
                        IdSocioEconomico = nivelSocioEcon.IdCodigo,
                        IdSexo = sexo.IdCodigo,
                        IdOcupacion = ocupacion.IdCodigo,
                        IdTipoZonaResidencial = zonaResidencial.IdCodigo,
                        IdEstacion = estacion.IdCodigo
                    };

                    persona = Insert(persona);
                    count = persona.IdPersona == null ? count : count++;
                }
            }

            return count;
        }
    }
}
