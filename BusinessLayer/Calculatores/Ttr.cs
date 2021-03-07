using Domain;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Calculatores
{
    public static class Ttr
    {
        public static double GetActivityTotalTime(IEnumerable<ViajeJourney> journey)
        {
            return journey.Sum(x => (x.HoraFin - x.HoraInicio).TotalHours);
        }

        public static double GetTravelTotalTime(IEnumerable<ViajeJourney> journey)
        {
            var totalTime = 0.0d;
            for (int i = 0; i < journey.Count() - 1; i++)
            {
                totalTime += (journey.ElementAt(i).HoraFin - journey.ElementAt(i + 1).HoraInicio).TotalHours;
            }

            return totalTime;
        }

        public static double GetPrimeTime(IEnumerable<ViajeJourney> journey, double activityTime, double travelTime)
        {
            return journey.Sum(x => (travelTime * (x.HoraFin - x.HoraInicio).TotalHours) / activityTime);
        }

        public static double GetTtrJourney1(IList<ViajeJourney> journey)
        {
            var tt = GetTravelTotalTime(journey);
            var ta = (journey[0].HoraFin - journey[0].HoraFin).TotalHours;

            return tt / (tt + ta);
        }

        public static double GetTtrJourney2(IList<ViajeJourney> journey)
        {
            var ta = GetActivityTotalTime(journey);
            var t = GetTravelTotalTime(journey);
            var tt = GetPrimeTime(journey, ta, t);

            return tt / (tt + ta);
        }

        public static double GetTtrJourney3(IList<ViajeJourney> journey)
        {
            return GetTtrJourney1(journey);
        }

        public static double GetTtrJourney4(IList<ViajeJourney> journey)
        {
            var ta = (journey[0].HoraFin - journey[0].HoraFin).TotalHours;
            var tt = GetTravelTotalTime(journey);

            var spentTimeBases = GetSpentTimeBases(journey[0], journey[1]);

            tt -= spentTimeBases;
            return tt / (tt + ta);
        }

        public static double GetTtrJourney5(IList<ViajeJourney> journey)
        {
            var ta = GetActivityTotalTime(journey);
            var t = GetTravelTotalTime(journey);

            var spentTimeBases = GetSpentTimeBases(journey[0], journey[1]);

            t -= spentTimeBases;

            var tt = GetPrimeTime(journey, ta, t);

            return tt / (tt + ta);
        }

        private static double GetSpentTimeBases(ViajeJourney base1, ViajeJourney base2)
        {
            return 0;
        }
    }
}
