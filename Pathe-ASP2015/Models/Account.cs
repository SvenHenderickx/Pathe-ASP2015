using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PatheAsp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public EnumGeslacht Geslacht { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Emailadres { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Wachtwoord { get; set; }
        public bool KrijgtNieuwsBrief { get; set; }

        public Account(int id, string voornaam, string tussenvoegsel, string achternaam, DateTime geboorteDatum,
            int geslacht, string emailadres, string wachtwoord, bool nieuwsbrief)
        {
            this.Id = id;
            this.Voornaam = voornaam;
            this.Tussenvoegsel = tussenvoegsel;
            this.Achternaam = achternaam;
            this.GeboorteDatum = geboorteDatum;
            this.Geslacht = (EnumGeslacht) geslacht;
            this.Emailadres = emailadres;
            this.Wachtwoord = wachtwoord;
            this.KrijgtNieuwsBrief = nieuwsbrief;
        }

    }
}
