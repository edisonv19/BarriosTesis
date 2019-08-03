namespace Domain
{
    public class Coordenada
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Coordenada(double lat, double lon)
        {
            this.latitude = lat;
            this.longitude = lon;
        }
    }
}
