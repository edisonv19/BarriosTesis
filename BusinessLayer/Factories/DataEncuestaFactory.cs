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

        public DataEncuestaFactory()
        {
            _lugarRepository = new LugarDataAccess();
            _codigoRepository = new CodigoDataAccess();
            _personaRepository = new PersonaDataAccess();
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
                    //return _personaRepository.GetInt(new Persona() { Grupo = "Sexo", Clave = data.Split(";")[0] });
                    return null;
                default:
                    return null;
            }
        }
    }
}
