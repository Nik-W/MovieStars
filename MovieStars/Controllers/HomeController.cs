using MovieStars.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MovieStars.Controllers
{
    public class HomeController : Controller
    {
        MovieStarsContext db = new MovieStarsContext();
        public ActionResult Index()
        {
            var movieStars = db.MovieStars;
            ViewBag.MovieStars = movieStars;
            return View();
        }

        [HttpGet]
        public ActionResult Rewards(int? id)
        {
            var rewards = db.Rewards.Where(x => x.MovieStar.Id == id);
            ViewBag.Rewards = rewards;
            ViewBag.MovieStar = db.MovieStars.Find(id);
            return View();
        }

        [HttpGet]
        public ActionResult Roles(int? id)
        {
            var listOfFilmed = db.ListOfFilmed.Where(x => x.MovieStar.Id == id);
            ViewBag.Roles = listOfFilmed;
            ViewBag.MovieStar = db.MovieStars.Find(id);
            return View();
        }

        [HttpGet]
        public ActionResult Films(int? id)
        {
            var listOfFilmed = db.ListOfFilmed.Where(x => x.MovieStar.Id == id).Include(x => x.Film.Director).ToList();
            var films = new List<Film>();
            listOfFilmed.ForEach(x => films.Add(x.Film));
            ViewBag.Films = films;
            ViewBag.MovieStar = db.MovieStars.Find(id);
            return View();
        }

        [HttpGet]
        public ActionResult AllFilms()
        {
            var films = db.Films.Include(x => x.Director);
            ViewBag.Films = films;
            return View();  //возвращаем представление
        }

        [HttpGet]
        public ActionResult Rating(int? id)
        {
            var ratingList = db.FilmRatings.Where(x => x.Film.Id == id).Include(x => x.Critic);
            ViewBag.Rating = ratingList;
            ViewBag.Film = db.Films.Find(id);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Кинозвёзды";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Разработчик - Петрухин Н.В.";

            return View();
        }
    }
}