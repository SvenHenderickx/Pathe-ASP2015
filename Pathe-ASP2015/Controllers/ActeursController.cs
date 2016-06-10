using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatheAsp.Models;
using Pathe_ASP2015.Models;

namespace Pathe_ASP2015.Controllers
{
    public class ActeursController : Controller
    {
        // GET: Acteurs
        public ActionResult Index()
        {
            return View();
        }

        [Route("Acteurs/ActeurInfo/{acteurNaam}")]
        public ActionResult ActeurInfo(string id)
        {
            Acteur tempActeur = null;
            foreach (Film f in Handler.GetAllFilms())
            {
                foreach (Acteur a in f.Acteurs)
                {
                    if (a.Naam == id)
                    {
                        tempActeur = a;
                    }
                }
            }
            if (tempActeur != null)
            {
                return View(tempActeur);
            }
            return HttpNotFound();
        }
    }
}