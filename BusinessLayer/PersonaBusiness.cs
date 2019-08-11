using BusinessLayer.Factories;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain;
using Domain.Interfaces;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils.Helpers;

namespace BusinessLayer
{
    public class PersonaBusiness
    {
        private IPersonaRepository _personaRepository;
        private ILugarRepository _lugarRepository;
        private ICodigoRepository _codigoRepository;
        private IAbstractFactory<IDataPerson> _dataPersonFactory;

        // Cache
        private Dictionary<string, IDataPerson> _cache;

        public PersonaBusiness()
        {
            _personaRepository = new PersonaDataAccess();
            _lugarRepository = new LugarDataAccess();
            _codigoRepository = new CodigoDataAccess();
            _dataPersonFactory = new DataPersonFactory();

            _cache = new Dictionary<string, IDataPerson>();
        }

        public Persona Insert(Persona persona)
        {
            persona.IdPersona = _personaRepository.Insert(persona);

            return persona;
        }

        public List<Persona> InsertByExcel(FileInfo file, string pathOut)
        {
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

                    lugar = (Lugar)GetObject($"latlng_{lugar.Latitud};{lugar.Longitud}");

                    // Id SocioEconómico
                    Codigo nivelSocioEcon = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, ingreso_j].Value.GetString()
                    };

                    nivelSocioEcon = (Codigo)GetObject($"ingreso_{nivelSocioEcon.Clave}");

                    // Id Sexo
                    Codigo sexo = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, sexo_j].Value.GetString()
                    };

                    sexo = (Codigo)GetObject($"sexo_{nivelSocioEcon.Clave}");

                    // Id NivelEducativo
                    Codigo nivelEducativo = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, estudios_j].Value.GetString()
                    };

                    nivelEducativo = (Codigo)GetObject($"estudio_{nivelEducativo.Clave}");

                    // Id Ocupacion
                    Codigo ocupacion = new Codigo()
                    {
                        Clave = excelWorksheet.Cells[i, ocupacion_j].Value.GetString()
                    };

                    ocupacion = (Codigo)GetObject($"ocupacion_{ocupacion.Clave}");

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

                    estacion = (Codigo)GetObject($"estacion_{estacion.Clave}");

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

                    Insert(persona);
                }
            }

            return null;
        }

        private IDataPerson GetOfCache(string key)
        {
            return _cache[key];
        }

        private void SetToCache(string key, IDataPerson o)
        {
            _cache.Add(key, o);
        }

        private IDataPerson GetObject(string key)
        {
            if (GetOfCache(key) == null)
            {
                IDataPerson dataPerson = _dataPersonFactory.GetData(key);
                SetToCache(key, dataPerson);

                return dataPerson;
            }
            else
            {
                return GetOfCache(key);
            }
        }
    }
}
