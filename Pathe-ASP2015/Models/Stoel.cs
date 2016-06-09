namespace PatheAsp.Models
{
    public class Stoel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Rij { get; set; }
        public int Nummer { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string Status { get; set; }

        public Stoel(int id, string type, int rij, int nummer, int xPos, int yPos, string status)
        {
            this.Id = id;
            this.Type = type;
            this.Rij = rij;
            this.Nummer = nummer;
            this.XPos = xPos;
            this.YPos = yPos;
            this.Status = status;
        }
    }
}