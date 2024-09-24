namespace GameZone.Models
{
    public class UpcomingGame
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Platform { get; set; }
    }

}
