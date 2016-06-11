﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatheAsp.Models;
using Pathe_ASP2015.Models;

namespace Pathe_ASP2015.Controllers
{
    public class VoorstellingenController : Controller
    {
        // GET: Voorstellingen
        public ActionResult Index()
        {
            return View();
        }

        [Route("Voorstellingen/VoorstellingInfo/{id}")]
        public ActionResult VoorstellingInfo(int id)
        {
            Voorstelling temp = null;
            foreach (Bioscoop b in Handler.Bioscopen)
            {
                foreach (Zaal z in b.Zalen)
                {
                    foreach (Voorstelling v in z.Voorstellingen)
                    {
                        if (v.Id == id)
                        {
                            temp = v;
                        }
                    }
                }
            }
            if (temp != null)
            {
                return View(temp);
            }
            return HttpNotFound();
        }
    }
}