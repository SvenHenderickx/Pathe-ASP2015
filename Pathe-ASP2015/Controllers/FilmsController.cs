using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatheAsp.Models;
using Pathe_ASP2015.Models;

namespace Pathe_ASP2015.Controllers
{
    public class FilmsController : Controller
    {
        // GET: Films/Test
        public ActionResult Test()
        {
            List<Acteur> acteurs = new List<Acteur>();
            acteurs.Add(new Acteur("Jan"));
            acteurs.Add(new Acteur("Marian"));
            var film = new Film(1, "Shrek", 90, "Shrek is love, shrek is life.", "Engels, Nederlands Ondertiteld", new DateTime(2008, 09, 27), new Regisseur(1, "Henk", DateTime.Now, "Grathem", "-" ), acteurs, new List<Genre>(), new List<Review>());
            return View(film);
        }

        public ActionResult Index()
        {
            Handler.GetBioscopen();
            List<Film> films = Handler.GetAllFilms();
            return View();
        }
    }
}