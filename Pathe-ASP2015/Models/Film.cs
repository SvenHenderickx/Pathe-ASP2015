using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatheAsp.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int Duur { get; set; }
        public string Beschrijving { get; set; }
        public string Taalversie { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Regisseur Regisseur { get; set; }
        public List<Acteur> Acteurs { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Review> Reviews { get; set; }

        public Film(int id, string naam, int duur, string beschrijving, string taalversie, DateTime releaseDate,
            Regisseur regisseur, List<Acteur> acteurs, List<Genre> genres, List<Review> reviews)
        {
            this.Id = id;
            this.Naam = naam;
            this.Duur = duur;
            this.Beschrijving = beschrijving;
            this.Taalversie = taalversie;
            this.ReleaseDate = releaseDate;
            this.Regisseur = regisseur;
            this.Acteurs = acteurs;
            this.Genres = genres;
            this.Reviews = reviews;
        }

        public Film(string naam)
        {
            this.Naam = naam;
        }

        // Methodes

        public decimal FilmInUren()
        {
            return Duur / 60;
        }
    }
}
