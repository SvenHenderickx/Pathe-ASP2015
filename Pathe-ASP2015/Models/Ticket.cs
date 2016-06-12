namespace PatheAsp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Stoel Stoel { get; set; }
        public Prijs Prijs { get; set; }
        public static int Highestid = 0;

        public Ticket(int id)
        {
            Id = id;
            if (id > Highestid)
            {
                Highestid = id;
            }
        }

        public Ticket(int id, Stoel stoel, Prijs prijs)
        {
            Id = id;
            Stoel = stoel;
            Prijs = prijs;
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