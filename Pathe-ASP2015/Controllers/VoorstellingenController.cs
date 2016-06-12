using System;
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
            Handler.GetPrijzen();
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
                Session["Voorstelling"] = temp;
                return View(temp);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult SelectTicket(FormCollection formCollection)
        {
            string ticketNaam = formCollection["ticket"];
            Prijs prijs = null;
            foreach (Prijs p in Handler.GetPrijzen())
            {
                if (p.Naam == ticketNaam)
                {
                    prijs = p;
                }
            }
            Session["ticketPrijs"] = prijs;
            Voorstelling voorstelling = Session["Voorstelling"] as Voorstelling;
            return RedirectToAction("StoelKiezen");
        }

        public ActionResult StoelKiezen()
        {
            Voorstelling voorstelling = Session["Voorstelling"] as Voorstelling;
            return View(voorstelling);
        }

        [HttpPost]
        public ActionResult SelectStoel(FormCollection formCollection)
        {
            int id = Convert.ToInt32(formCollection["stoel"]);
            Ticket ticket = null;
            foreach (Stoel s in Handler.GetAllStoelen())
            {
                if (id == s.Id)
                {
                    ticket = new Ticket(Ticket.Highestid, s, Session["ticketPrijs"] as Prijs);
                }
            }
            Session["Ticket"] = ticket;
            Voorstelling voorstelling = Session["Voorstelling"] as Voorstelling;
            foreach (Voorstelling v in Handler.GetAllVoorstellingen())
            {
                if (v.Id == voorstelling.Id)
                {
                    voorstelling.AddTicket(ticket);
                }
            }

            return RedirectToAction("TicketInfo");
        }

        public ActionResult TicketInfo()
        {
            return View(Session["Ticket"] as Ticket);
        }
    }
}