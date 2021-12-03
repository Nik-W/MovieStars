using MovieStars.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MovieStars.Controllers
{
    public class DbEditController : Controller
    {
        MovieStarsContext db = new MovieStarsContext();

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovieStar(MovieStar movieStar)
        {
            int lastId = db.MovieStars.OrderByDescending(x => x.Id).First().Id;
            movieStar.Id = ++lastId;
            db.MovieStars.Add(movieStar);
            db.SaveChanges();
            ViewBag.ResultAddMs = "Запись успешно добавлена";
            return View("Index");
        }

        [HttpPost]
        public ActionResult RemoveMovieStar(int? id)
        {
            var movieStar = db.MovieStars.Find(id);
            if (movieStar == null)
            {
                ViewBag.ResultRemoveMs = "Не найдена удаляемая запись";
                return View("Index");
            }
            db.MovieStars.Remove(movieStar);
            db.SaveChanges();
            ViewBag.ResultRemoveMs = "Запись успешно удалена";
            return View("Index");
        }

        [HttpPost]
        public ActionResult AddFilm(Film film, int? idDirector)
        {
            if (film.ReleaseDate < new DateTime(1900, 01, 01))
            {
                ViewBag.ResultAddF = "Неверная дата.\nВведите дату в формате:\nDD.MM.YYYY";
                return View("Index");
            }
            int lastId = db.Films.OrderByDescending(x => x.Id).First().Id;
            film.Id = ++lastId;
            film.Director = db.Directors.Find(idDirector);
            if (film.Director == null)
            {
                ViewBag.ResultAddF = "ID режиссёра не указан \nили указан неверно";
                return View("Index");
            }
            db.Films.Add(film);
            db.SaveChanges();
            ViewBag.ResultAddF = "Запись успешно добавлена";
            return View("Index");
        }

        [HttpPost]
        public ActionResult RemoveFilm(int? id)
        {
            var film = db.Films.Find(id);
            if (film == null)
            {
                ViewBag.ResultRemoveF = "Не найдена удаляемая запись";
                return View("Index");
            }
            db.Films.Remove(film);
            db.SaveChanges();
            ViewBag.ResultRemoveF = "Запись успешно удалена";
            return View("Index");
        }
    }
}