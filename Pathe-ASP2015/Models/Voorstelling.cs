using System;
using System.Collections.Generic;

namespace PatheAsp.Models
{
    public class Voorstelling
    {
        public int Id { get; set; }
        public DateTime TijdEnDatum { get; set; }
        public string Formaat { get; set; }
        public int FilmId { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Film Film { get; set; }

        public Voorstelling(int id, DateTime tijdEnDatum, string formaat, List<Ticket> tickets, Film film)
        {
            this.Id = id;
            this.TijdEnDatum = tijdEnDatum;
            this.Formaat = formaat;
            this.Tickets = tickets;
            this.Film = film;
        }

        public Voorstelling(int id, DateTime tijdEnDatum, string formaat, Film film)
        {
            this.Id = id;
            this.TijdEnDatum = tijdEnDatum;
            this.Formaat = formaat;
            this.Film = film;
            this.Tickets = new List<Ticket>();
        }

        public Voorstelling(int id, string formaat, int filmId)
        {
            this.Id = id;
            this.TijdEnDatum = DateTime.Now;
            this.Formaat = formaat;
            this.FilmId = filmId;
            this.Tickets = new List<Ticket>();
        }

        public void FilmToevoegen(Film film)
        {
            this.Film = film;
        }
    }
}