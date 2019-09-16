using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class LugarBusiness : ILugarBusiness
    {
        private readonly ILugarRepository _lugarRepository;
        private readonly IEspacioRepository _espacioRepository;
        private readonly ICodigoRepository _codigoRepository;
        public LugarBusiness(ILugarRepository lugarRepository, IEspacioRepository espacioRepository, ICodigoRepository codigoRepository)
        {
            _lugarRepository = lugarRepository;
            _espacioRepository = espacioRepository;
            _codigoRepository = codigoRepository;
        }
        public Lugar Insert(Lugar lugar)
        {
            lugar.IdLugar = _lugarRepository.Insert(lugar);
            return lugar;
        }

        public Lugar GetByLatLng(Lugar lugar)
        {
            return _lugarRepository.GetByLatLng(lugar);
        }

        public IEnumerable<Lugar> GetByFilter(Lugar lugar)
        {
            return _lugarRepository.GetByFilter(lugar);
        }

        public int ReloadRadiosCensales()
        {
            int count = 0;
            var lugares = GetByFilter(new Lugar());
            var polygons = GetAllPolygons().ToList();

            foreach (var lugar in lugares)
            {
                var radioCensal = polygons.Find(polygon => (polygon.contains(new Coordenada((double)lugar.Latitud, (double)lugar.Longitud))));

                if (radioCensal != null)
                {
                    lugar.IdRadioCensal = radioCensal.IdEspacio;

                    // update RadioCensal
                    var rowAffected = _lugarRepository.Update(lugar);

                    count = rowAffected > 0 ? ++count : count;
                }
            }

            return count;
        }

        private IEnumerable<Espacio> GetAllPolygons()
        {
            // Busco los RRCC
            var codigo = _codigoRepository.GetByClave(new Codigo() { Grupo = "CategoriaEspacio", Clave = "RadioCensal" });

            var espacios = _espacioRepository.GetByFilter(new Espacio() { IdCategoria = codigo.IdCodigo });

            // Cargo las coordenadas en List<Coordenada>
            return espacios.Select(x => { x.Coordenadas = JsonConvert.DeserializeObject<List<Coordenada>>(x.CoordenadasStr); return x; }).ToList();
        }
    }
}
