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
            }
            return tempZalen;
        }

        public static List<Stoel> GetStoelen(int zaalId)
        {
            return DatabaseManager.GetStoelenFromZaalId(zaalId);
        }

        public static List<Voorstelling> GetVoorstellingen(int zaalId)
        {
            
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
    }
}