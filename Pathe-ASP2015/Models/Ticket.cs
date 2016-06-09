namespace PatheAsp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Stoel Stoel { get; set; }
        public Prijs Prijs { get; set; }
    }
}