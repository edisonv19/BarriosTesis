using BusinessLayer.Calculatores;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class JourneyBusiness : IJourneyBusiness
    {
        private readonly IViajeRepository _viajeRepository;
        public JourneyBusiness(IViajeRepository viajeRepository)
        {
            _viajeRepository = viajeRepository;
        }

        public IEnumerable<IEnumerable<ViajeJourney>> GetJourneys(Persona persona)
        {
            var viajes = _viajeRepository.GetByPersona(persona);
            var journeys = new List<IEnumerable<ViajeJourney>>();
            var journey = new List<ViajeJourney>();

            // get juorneys
            foreach (var viaje in viajes)
            {
                journey.Add(viaje);

                if (viaje.EsDestinoBase)
                {
                    journeys.Add(journey);
                    journey.Clear();
                }
            }

            return journeys;
        }

        private void x(IEnumerable<IEnumerable<ViajeJourney>> journeys)
        {
            foreach (var journey in journeys)
            {
                if (JourneyType.IsJourney1(journey))
                {
                    // Calcular 1
                    break;
                }
                if (JourneyType.IsJourney2(journey))
                {
                    // Calcular 1
                    break;
                }
                if (JourneyType.IsJourney3(journey))
                {
                    // Calcular 1
                    break;
                }
                if (JourneyType.IsJourney4(journey))
                {
                    // Calcular 1
                    break;
                }
                if (JourneyType.IsJourney5(journey))
                {
                    // Calcular 1
                    break;
                }
            }
        }
    }
}
