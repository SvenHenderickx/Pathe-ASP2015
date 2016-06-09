using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PatheAsp.Models;

namespace Pathe_ASP2015.Models
{
    public static class Handler
    {
        public static List<Bioscoop> Bioscopen = new List<Bioscoop>();

        public static void GetBioscopen()
        {
            Bioscopen.Add(new Bioscoop(1, "Eindhoven", "Eindhoven", "Dorpstraat", "6096AR", true, true, true, "Dolby", "Ja", GetZalen(1)));
            Bioscopen.Add(new Bioscoop(2, "Amersfoort", "Amersfoort", "Lindestraat", "6096AR", true, true, true, "Dolby", "Ja", GetZalen(2)));
        }

        public static List<Zaal> GetZalen(int biosId)
        {
            List<Zaal> tempList = new List<Zaal>();
            tempList.Add(new Zaal(1, 1, GetStoelen(1)));
            tempList.Add(new Zaal(2, 2, GetStoelen(1)));
            tempList.Add(new Zaal(3, 3, GetStoelen(1)));
            return tempList;
        }

        public static List<Stoel> GetStoelen(int zaalId)
        {
            List<Stoel> tempList = new List<Stoel>();
            tempList.Add(new Stoel(1, "Normaal", 1, 1, 1, 1, "vrij"));
            tempList.Add(new Stoel(2, "Normaal", 1, 2, 1, 2, "vrij"));
            tempList.Add(new Stoel(3, "Normaal", 2, 1, 2, 1, "vrij"));
            tempList.Add(new Stoel(4, "Normaal", 2, 2, 2, 2, "vrij"));
            return tempList;
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
    }
}