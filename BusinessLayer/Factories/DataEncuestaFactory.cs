using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain;
using Domain.Interfaces;
using Utils.Helpers;

namespace BusinessLayer.Factories
{
    public class DataEncuestaFactory : IAbstractFactory<IDataEncuesta>
    {
        private ILugarRepository _lugarRepository;
        private ICodigoRepository _codigoRepository;
        private IPersonaRepository _personaRepository;
        private IEspacioRepository _espacioRepository;

        public DataEncuestaFactory()
        {
            _lugarRepository = new LugarDataAccess();
            _codigoRepository = new CodigoDataAccess();
            _personaRepository = new PersonaDataAccess();
            _espacioRepository = new EspacioDataAccess();
        }

        public IDataEncuesta GetData(string key)
        {
            string group = key.Split("_")[0];
            string data = key.Split("_")[1];

            switch (group)
            {
                case "lugar":
                    return _lugarRepository.GetByLatLng(new Lugar() { Latitud = data.Split(";")[0].GetDouble(), Longitud = data.Split(";")[1].GetDouble() });
                case "codigo":
                    return _codigoRepository.GetByClave(new Codigo() { Grupo = data.Split(";")[0], Clave = data.Split(";")[1] });
                case "persona":
                    return _personaRepository.GetByCodigo(new Persona() { Nombre = data.Split(";")[0], IdLugar = data.Split(";")[1].GetInt() });
                    return null;
                case "espacio":
                    return _espacioRepository.GetByCodigo(new Espacio() { Codigo = data });
                default:
                    return null;
            }
        }
    }
}
