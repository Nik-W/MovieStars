using System;
using System.Collections.Generic;

namespace MovieStars.Models
{
    public class MovieStar
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int FilmsCount { get; set; }
        public virtual List<Filmed> ListOfFilmed { get; set; }
        public virtual List<Reward> Rewards { get; set; }
    }

    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual List<Filmed> ListOfFilmed { get; set; }
        public virtual List<FilmRating> Ratings { get; set; }
        public Director Director { get; set; }
    }

    public class Filmed
    {
        public int Id { get; set; }
        public int Honorarium { get; set; }
        public string Role { get; set; }
        public MovieStar MovieStar { get; set; }
        public Film Film { get; set; }
    }

    public class Reward
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateOfPresentation { get; set; }
        public MovieStar MovieStar { get; set; }
    }

    public class Director
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int FilmsMadeCount { get; set; }
        public virtual List<Film> Films { get; set; }
    }

    public class Critic
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Rating { get; set; }
        public virtual List<FilmRating> FilmRatings { get; set; }
    }

    public class FilmRating
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public Film Film { get; set; }
        public Critic Critic { get; set; }
    }
}