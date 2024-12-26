namespace ArtBase.Models
{
    public class FavoriteMovieModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieID { get; set; }
        public UserModel User { get; set; }
    }

}
