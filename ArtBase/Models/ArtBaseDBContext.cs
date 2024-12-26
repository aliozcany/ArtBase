using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ArtBase.Models
{
    public class ArtBaseDBContext : DbContext
    {
        public ArtBaseDBContext(DbContextOptions<ArtBaseDBContext> options)
            : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<FavoriteMovieModel> FavoriteMovies { get; set; }
        public DbSet<FavoriteSeriesModel> FavoriteSeries { get; set; }
        public DbSet<FilmWatchlistModel> FilmWatchlist { get; set; }
        public DbSet<WatchedSeriesModel> SeriesWatchlist { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
    }
}
