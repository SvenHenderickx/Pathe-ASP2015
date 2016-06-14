using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Participation_ASP.Models;
using PatheAsp.Models;

namespace Pathe_ASP2015.Models
{
    public static class Handler
    {
        public static List<Bioscoop> Bioscopen = new List<Bioscoop>();

        /// <summary>
        /// Alle biosscopen ophalen
        /// </summary>
        public static void GetBioscopen()
        {
            //Bioscopen.Add(new Bioscoop(1, "Eindhoven", "Eindhoven", "Dorpstraat", "6096AR", true, true, true, "Dolby", "Ja", GetZalen(1)));
            //Bioscopen.Add(new Bioscoop(2, "Amersfoort", "Amersfoort", "Lindestraat", "6096AR", true, true, true, "Dolby", "Ja", GetZalen(2)));
            Bioscopen = DatabaseManager.GetAllBioscopen();
            foreach (Bioscoop b in Bioscopen)
            {
                b.VoegZalenToe(GetZalen(b.Id));
            }
        }

        /// <summary>
        /// Alle zalen van een bioscoop
        /// </summary>
        /// <param name="biosId"></param>
        /// <returns></returns>
        public static List<Zaal> GetZalen(int biosId)
        {
            List<Zaal> tempZalen = DatabaseManager.GetZalenFromBioscoopId(biosId);
            foreach (Zaal z in tempZalen)
            {
                z.VoegStoelenToe(GetStoelen(z.Id));
                z.VoegVoorstellingenToe(GetVoorstellingen(z.Id));
            }
            return tempZalen;
        }

        /// <summary>
        /// Alle stoelen van een zaal
        /// </summary>
        /// <param name="zaalId"></param>
        /// <returns></returns>
        public static List<Stoel> GetStoelen(int zaalId)
        {
            return DatabaseManager.GetStoelenFromZaalId(zaalId);
        }

        /// <summary>
        /// Alle voorstellingen van een zaal
        /// </summary>
        /// <param name="zaalId"></param>
        /// <returns></returns>
        public static List<Voorstelling> GetVoorstellingen(int zaalId)
        {
            List<Voorstelling> voorstellingen = DatabaseManager.GetVoorstellingenFromZaalId(zaalId);
            foreach (Voorstelling v in voorstellingen)
            {
                v.FilmToevoegen(GetFilmFromVoorstelling(v.FilmId));
                v.TicketsToevoegen(GetTicketsFromVoorstelling(v.Id));
            }
            return voorstellingen;
        }

        public static Film GetFilmFromVoorstelling(int filmId)
        {
            Film film = DatabaseManager.GetFilmVanVoorstelling(filmId);
            film.ActeurenToevoegen(GetActeursFromFilm(filmId));
            return film;
        }

        /// <summary>
        /// Alle acteurs voor een film
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        public static List<Acteur> GetActeursFromFilm(int filmId)
        {
            return DatabaseManager.GetActeursFromFilm(filmId);
        }

        /// <summary>
        /// Geeft een string terug voor een bool
        /// </summary>
        /// <param name="inv"></param>
        /// <returns></returns>
        public static string GetStringFromBool(bool inv)
        {
            if (inv == true)
            {
                return "Ja";
            }
            else
            {
                return "Nee";
            }
        }

        /// <summary>
        /// Maakt van een int een bool
        /// </summary>
        /// <param name="inv"></param>
        /// <returns></returns>
        public static bool GetBoolFromInt(int inv)
        {
            if (inv == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Geeft alle films terug
        /// </summary>
        /// <returns></returns>
        public static List<Film> GetAllFilms()
        {
            List<Film> films = new List<Film>();
            foreach (Bioscoop b in Bioscopen)
            {
                foreach (Zaal z in b.Zalen)
                {
                    foreach (Voorstelling v in z.Voorstellingen)
                    {
                        films.Add(v.Film);
                    }
                }
            }
            return films;
        }

        /// <summary>
        /// Een lijst van de mogelijke prijzen
        /// </summary>
        /// <returns></returns>
        public static List<Prijs> GetPrijzen()
        {
            return DatabaseManager.GetPrijzen();
        }

        /// <summary>
        /// Geeft de zaal terug van een voorstelling
        /// </summary>
        /// <param name="voorstelling"></param>
        /// <returns></returns>
        public static Zaal GetZaalFromVoorstelling(Voorstelling voorstelling)
        {
            foreach (Bioscoop b in Bioscopen)
            {
                foreach (Zaal z in b.Zalen)
                {
                    foreach (Voorstelling v in z.Voorstellingen)
                    {
                        if (voorstelling == v)
                        {
                            return z;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Geeft alle tickets terug van de vorostelling
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Ticket> GetTicketsFromVoorstelling(int id)
        {
            List<Ticket> tempList = DatabaseManager.GetTicketsFromVoorstelling(id);
            foreach (Ticket t in tempList)
            {
                t.VoegPrijsToe(GetPrijsFromTicket(t.Id));
                t.VoegStoelToe(GetStoelFromTicket(t.Id));
            }
            return tempList;
        }

        /// <summary>
        /// GEeft de prijs terug van een ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public static Prijs GetPrijsFromTicket(int ticketId)
        {
            return DatabaseManager.GetPrijsFromTicket(ticketId);
        }

        /// <summary>
        /// Geeft de stoel terug die bij een kaartje hoort
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public static Stoel GetStoelFromTicket(int ticketId)
        {
            return DatabaseManager.GetStoelFromTicket(ticketId);
        }
        /// <summary>
        /// GEeft alle stoelen terug
        /// </summary>
        /// <returns></returns>
        public static List<Stoel> GetAllStoelen()
        {
            List<Stoel> temp = new List<Stoel>();
            foreach (Bioscoop b in Bioscopen)
            {
                foreach (Zaal z in b.Zalen)
                {
                    foreach (Stoel s in z.Stoelen)
                    {
                        temp.Add(s);
                    }
                }
            }
            return temp;
        }
        /// <summary>
        /// Geeft alle voorstellingen terug
        /// </summary>
        /// <returns></returns>
        public static List<Voorstelling> GetAllVoorstellingen()
        {
            List<Voorstelling> temp = new List<Voorstelling>();
            foreach (Bioscoop b in Bioscopen)
            {
                foreach (Zaal z in b.Zalen)
                {
                    foreach (Voorstelling v in z.Voorstellingen)
                    {
                        temp.Add(v);
                    }
                }
            }
            return temp;
        }

        public static bool AddTicketToVoorstelling(Ticket ticket, Voorstelling voorstelling)
        {
            return DatabaseManager.AddTicket(ticket, voorstelling);
        }
    }
}