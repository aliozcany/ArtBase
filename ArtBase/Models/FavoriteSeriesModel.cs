namespace ArtBase.Models
{
    public class FavoriteSeriesModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeriesID { get; set; }
        public UserModel User { get; set; }
    }
}
