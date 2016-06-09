using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatheAsp.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public Genre(int id, string naam)
        {
            this.Id = id;
            this.Naam = naam;
        }
    }
}
