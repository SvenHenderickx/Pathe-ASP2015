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

        public static List<Stoel> GetStoelen(int zaalId)
        {
            return DatabaseManager.GetStoelenFromZaalId(zaalId);
        }

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

        public static List<Acteur> GetActeursFromFilm(int filmId)
        {
            return DatabaseManager.GetActeursFromFilm(filmId);
        }

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

        public static bool GetBoolFromInt(int inv)
        {
            if (inv == 1)
            {
                return true;
            }
            return false;
        }

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

        public static List<Prijs> GetPrijzen()
        {
            return DatabaseManager.GetPrijzen();
        }

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

        public static Prijs GetPrijsFromTicket(int ticketId)
        {
            return DatabaseManager.GetPrijsFromTicket(ticketId);
        }

        public static Stoel GetStoelFromTicket(int ticketId)
        {
            return DatabaseManager.GetStoelFromTicket(ticketId);
        }
    }
}