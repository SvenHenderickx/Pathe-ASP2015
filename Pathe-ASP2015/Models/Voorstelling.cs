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

        public Voorstelling(int id, DateTime tijdEnDatum, string formaat, int filmId)
        {
            this.Id = id;
            this.TijdEnDatum = tijdEnDatum;
            this.Formaat = formaat;
            this.FilmId = filmId;
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

        public void TicketsToevoegen(List<Ticket> tickets)
        {
            this.Tickets = tickets;
        }

        public string ToString()
        {
            return Film.Naam + " - " + TijdEnDatum.Day.ToString() + "-" +  TijdEnDatum.Month.ToString() + "-" + TijdEnDatum.Year.ToString() + " " + TijdEnDatum.Hour.ToString() + ":" +
                   TijdEnDatum.Minute.ToString();
        }

        public void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
        }

        public List<Stoel> GetVrijeStoelen(List<Stoel> stoelen)
        {
            List<Stoel> tempList = new List<Stoel>();
            foreach (Ticket t in Tickets)
            {
                foreach (Stoel s in stoelen)
                {
                    if (t.Stoel.Id == s.Id)
                    {

                    }
                    else
                    {
                        bool check = false;
                        foreach (Ticket ts in Tickets)
                        {
                            if (s.Id == ts.Stoel.Id)
                            {
                                check = true;
                            }
                        }
                        
                        foreach (Stoel st in tempList)
                        {
                            if (st.Id == s.Id)
                            {
                                check = true;
                            }
                            
                        }
                        if (!check)
                        {
                            tempList.Add(s);
                        }
                    }
                }
            }
            return tempList;
        }
    }
}