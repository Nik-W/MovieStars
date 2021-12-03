using System.Data.Entity;

namespace MovieStars.Models
{
    public class MovieStarsContext : DbContext
    {
        public MovieStarsContext() : base("MovieStarsDataBase")
        { }
        
        public DbSet<MovieStar> MovieStars { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Filmed> ListOfFilmed { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Critic> Critics { get; set; }
        public DbSet<FilmRating> FilmRatings { get; set; }
    }
}