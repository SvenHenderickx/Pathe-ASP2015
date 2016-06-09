using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatheAsp.Models;
using Pathe_ASP2015.Models;

namespace Pathe_ASP2015.Controllers
{
    public class BioscopenController : Controller
    {
        // GET: Bioscopen
        public ActionResult Index()
        {
            List<Bioscoop> bioscopen = Handler.Bioscopen;
            return View(bioscopen);
        }

        [Route("Bioscopen/BioscoopInfo/{bioscoopNaam}")]
        public ActionResult BioscoopInfo(string id)
        {
            Bioscoop tempBioscoop = null;
            foreach (Bioscoop b in Handler.Bioscopen)
            {
                if (b.Naam == id)
                {
                    tempBioscoop = b;
                }
            }
            if (tempBioscoop != null)
            {
                return View(tempBioscoop);
            }
            return HttpNotFound();
        }
    }
}