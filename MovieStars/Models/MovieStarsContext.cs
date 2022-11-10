using System.Data.Entity;

namespace MovieStars.Models
{
    public class MovieStarsContext : DbContext
    {
        public MovieStarsContext() : base("Data Source=DESKTOP-U9FED9R;Initial Catalog=MovieStarsDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
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