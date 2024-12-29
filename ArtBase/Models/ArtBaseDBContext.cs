using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArtBase.Models
{
    public class ArtBaseDBContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ArtBaseDBContext(DbContextOptions<ArtBaseDBContext> options)
            : base(options) { }

        public DbSet<UserModel> CustomUsers { get; set; }
        public DbSet<RoleModel> CustomRoles { get; set; }
        public DbSet<FavoriteMovieModel> FavoriteMovies { get; set; }
        public DbSet<FavoriteSeriesModel> FavoriteSeries { get; set; }
        public DbSet<FilmWatchlistModel> FilmWatchlist { get; set; }
        public DbSet<WatchedSeriesModel> SeriesWatchlist { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
    }
}
