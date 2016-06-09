using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatheAsp.Models
{
    public  class Regisseur
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Woonplaats { get; set; }
        public string FotoUrl { get; set; }

        public Regisseur(int id, string naam, DateTime geboorteDatum, string woonplaats, string fotoUrl)
        {
            this.Id = id;
            this.Naam = naam;
            this.GeboorteDatum = geboorteDatum;
            this.Woonplaats = woonplaats;
            this.FotoUrl = fotoUrl;
        }
    }
}
