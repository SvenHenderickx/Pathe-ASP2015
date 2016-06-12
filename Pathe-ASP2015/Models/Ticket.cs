namespace PatheAsp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Stoel Stoel { get; set; }
        public Prijs Prijs { get; set; }

        public Ticket(int id)
        {
            Id = id;
        }

        public void VoegPrijsToe(Prijs prijs)
        {
            this.Prijs = prijs;
        }

        public void VoegStoelToe(Stoel stoel)
        {
            this.Stoel = stoel;
        }
    }
}