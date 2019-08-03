namespace Domain
{
    public class Lugar
    {
        public int? IdLugar { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int IdRadioCensal { get; set; }
        public int IdZona { get; set; }
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public int Radio { get; set; }
    }
}
