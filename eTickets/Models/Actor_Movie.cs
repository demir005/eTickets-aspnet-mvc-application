namespace eTickets.Models
{
    public class Actor_Movie
    {
        public int MovieId { get; set; }
        public Movies Movies { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
