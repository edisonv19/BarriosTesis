using Domain;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Calculatores
{
    public static class JourneyType
    {
        // Base1 -> place -> Base1
        public static bool IsJourney1(IEnumerable<ViajeJourney> journeys)
        {
            return journeys.Count() == 2
                && journeys.ElementAt(0).EsOrigenBase
                && !journeys.ElementAt(0).EsDestinoBase
                && !journeys.ElementAt(0).EsOrigenBase
                && journeys.ElementAt(1).EsDestinoBase
                && journeys.ElementAt(0).IdOrigen.Value == journeys.ElementAt(1).IdDestino.Value;
        }

        // Base1 -> place1 -> ... -> placeN -> Base1
        public static bool IsJourney2(IEnumerable<ViajeJourney> journeys)
        {
            return journeys.Count() > 2
                && journeys.ElementAt(0).EsOrigenBase
                && !journeys.ElementAt(0).EsDestinoBase
                && !journeys.ElementAt(journeys.Count() - 1).EsOrigenBase
                && journeys.ElementAt(journeys.Count() - 1).EsDestinoBase
                && journeys.ElementAt(0).IdOrigen.Value == journeys.ElementAt(journeys.Count() - 1).IdDestino.Value;
        }

        // Base1 -> Base2 -> Base1
        public static bool IsJourney3(IEnumerable<ViajeJourney> journeys)
        {
            return journeys.Count() == 2
                && journeys.ElementAt(0).EsOrigenBase
                && journeys.ElementAt(0).EsDestinoBase
                && journeys.ElementAt(1).EsOrigenBase
                && journeys.ElementAt(1).EsDestinoBase
                && journeys.ElementAt(0).IdDestino.Value == journeys.ElementAt(1).IdOrigen.Value
                && journeys.ElementAt(1).IdDestino.Value == journeys.ElementAt(0).IdOrigen.Value;
        }

        // Base1 -> place1 -> Base2
        public static bool IsJourney4(IEnumerable<ViajeJourney> journeys)
        {
            return journeys.Count() == 2
                && journeys.ElementAt(0).EsOrigenBase
                && !journeys.ElementAt(0).EsDestinoBase
                && !journeys.ElementAt(1).EsOrigenBase
                && journeys.ElementAt(1).EsDestinoBase
                && journeys.ElementAt(0).IdOrigen.Value != journeys.ElementAt(1).IdDestino.Value;
        }

        // Base1 -> place1 -> ... -> placeN -> Base2
        public static bool IsJourney5(IEnumerable<ViajeJourney> journeys)
        {
            return journeys.Count() > 2
                && journeys.ElementAt(0).EsOrigenBase
                && !journeys.ElementAt(0).EsDestinoBase
                && !journeys.ElementAt(journeys.Count() - 1).EsOrigenBase
                && journeys.ElementAt(journeys.Count() - 1).EsDestinoBase
                && journeys.ElementAt(0).IdOrigen.Value != journeys.ElementAt(journeys.Count() - 1).IdDestino.Value;
        }
    }
}
