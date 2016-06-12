namespace PatheAsp.Models
{
    public class Prijs
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int PrijsInCenten { get; set; }
        public string Informatie { get; set; }

        public Prijs(int id, string naam, int prijsInCenten, string informatie)
        {
            Id = id;
            Naam = naam;
            PrijsInCenten = prijsInCenten;
            Informatie = informatie;
        }
    }
}