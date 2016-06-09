using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatheAsp.Models
{
    public class Acteur
    {
        public int  Id { get; set; }
        public string Naam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Biografie { get; set; }
        public string FotoUrl { get; set; }

        public Acteur(int id, string naam, DateTime geboorteDatum, string biografie, string fotoUrl)
        {
            this.Id = id;
            this.Naam = naam;
            this.GeboorteDatum = geboorteDatum;
            this.Biografie = biografie;
            this.FotoUrl = fotoUrl;
        }

        public Acteur(string naam)
        {
            this.Naam = naam;
        }
    }
}
