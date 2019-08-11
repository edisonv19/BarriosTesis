using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain;
using Domain.Interfaces;
using Utils.Helpers;

namespace BusinessLayer.Factories
{
    public class DataPersonFactory : IAbstractFactory<IDataPerson>
    {
        private ILugarRepository _lugarRepository;
        private ICodigoRepository _codigoRepository;

        public DataPersonFactory()
        {
            _lugarRepository = new LugarDataAccess();
            _codigoRepository = new CodigoDataAccess();
        }

        public IDataPerson GetData(string key)
        {
            string group = key.Split("_")[0];
            string data = key.Split("_")[1];

            switch (group)
            {
                case "latlng":
                    return _lugarRepository.GetByLatLng(new Lugar() { Latitud = data.Split(";")[0].GetDouble(), Longitud = data.Split(";")[1].GetDouble() });
                case "ingresos":
                    return _codigoRepository.GetByClave(new Codigo() { Grupo = "NivelSocioEconomico", Clave = data.Split(";")[0] });
                case "sexo":
                    return _codigoRepository.GetByClave(new Codigo() { Grupo = "Sexo", Clave = data.Split(";")[0] });
                case "estudio":
                    return _codigoRepository.GetByClave(new Codigo() { Grupo = "NivelEducativo", Clave = data.Split(";")[0] });
                case "ocupacion":
                    return _codigoRepository.GetByClave(new Codigo() { Grupo = "Ocupacion", Clave = data.Split(";")[0] });
                case "zonaResidencial":
                    return _codigoRepository.GetByClave(new Codigo() { Grupo = "TipoZonaResidencia", Clave = data.Split(";")[0] });
                case "estacion":
                    return _codigoRepository.GetByClave(new Codigo() { Grupo = "Estacion", Clave = data.Split(";")[0] });
                default:
                    return null;
            }
        }
    }
}
