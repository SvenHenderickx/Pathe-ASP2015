﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatheAsp.Models
{
    public class Bioscoop
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Plaats { get; set; }
        public string Adres { get; set; }
        public string PostCode { get; set; }
        public bool Lift { get; set; }
        public bool RolstoelMogelijkheid { get; set; }
        public bool Ringleiding { get; set; }
        public string Geluidssysteem { get; set; }
        public string Openingstijden { get; set; }
        public List<Zaal> Zalen { get; set; }

        public Bioscoop(int id, string naam, string plaats, string adres, string postCode, bool lift,
            bool rolstoelMogelijkheid, bool ringleiding, string geluidssysteem, string openingstijden, List<Zaal> zalen)
        {
            this.Id = id;
            this.Naam = naam;
            this.Plaats = plaats;
            this.Adres = adres;
            this.PostCode = postCode;
            this.Lift = lift;
            this.RolstoelMogelijkheid = rolstoelMogelijkheid;
            this.Ringleiding = ringleiding;
            this.Geluidssysteem = geluidssysteem;
            this.Openingstijden = openingstijden;
            this.Zalen = zalen;
        }

        public Bioscoop(int id, string naam, string plaats, string adres, string postCode, bool lift, bool rolstoelMogelijkheid, bool ringleiding, string geluidssysteem)
        {
            Id = id;
            Naam = naam;
            Plaats = plaats;
            Adres = adres;
            PostCode = postCode;
            Lift = lift;
            RolstoelMogelijkheid = rolstoelMogelijkheid;
            Ringleiding = ringleiding;
            Geluidssysteem = geluidssysteem;
            Zalen = new List<Zaal>();
        }

        public void VoegZalenToe(List<Zaal> zalen)
        {
            this.Zalen = zalen;
        }

        public List<Stoel> GetAllStoelen()
        {
            List<Stoel> tempList = new List<Stoel>();
            foreach (Zaal z in Zalen)
            {
                foreach (Stoel s in z.Stoelen)
                {
                    tempList.Add(s);
                }
            }
            return tempList;
        }

        public List<Voorstelling> GetAlleVoorstellingen()
        {
            List<Voorstelling> voorstellingen = new List<Voorstelling>();
            foreach (Zaal z in Zalen)
            {
                foreach (Voorstelling v in z.Voorstellingen)
                {
                    voorstellingen.Add(v);
                }
            }
            return voorstellingen;
        }
    }
}
