using System;
using System.Collections.Generic;

namespace PatheAsp.Models
{
    public class Zaal
    {
        public int Id { get; set; }
        public int Nummer { get; set; }
        public List<Stoel> Stoelen { get; set; }
        public List<Voorstelling> Voorstellingen { get; set; }

        public Zaal(int id, int nummer, List<Stoel> stoelen, List<Voorstelling> voorstellingen)
        {
            this.Id = id;
            this.Nummer = nummer;
            this.Stoelen = stoelen;
            this.Voorstellingen = voorstellingen;
        }

        public Zaal(int id, int nummer, List<Stoel> stoelen)
        {
            this.Id = id;
            this.Nummer = nummer;
            this.Stoelen = stoelen;
            this.Voorstellingen = new List<Voorstelling>();
            
        }

        public Zaal(int id, int nummer)
        {
            this.Id = id;
            this.Nummer = nummer;
            this.Stoelen = new List<Stoel>();
            this.Voorstellingen = new List<Voorstelling>();

        }

        public void VoegStoelenToe(List<Stoel> stoelen)
        {
            this.Stoelen = stoelen;
        }
    }
}