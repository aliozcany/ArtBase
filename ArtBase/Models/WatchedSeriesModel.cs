namespace ArtBase.Models
{
    public class WatchedSeriesModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EpisodeID { get; set; }
        public UserModel User { get; set; }
    }

}
