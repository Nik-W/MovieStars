using MovieStars.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MovieStars.Controllers
{
    public class QueryController : Controller
    {
        MovieStarsContext db = new MovieStarsContext();

        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            ViewBag.Films = new SelectList(db.Films, "Id", "Title");
            ViewBag.MovieStars = new SelectList(db.MovieStars, "Id", "FullName");
            return View();
        }


        [HttpPost]
        public ActionResult AvgRatingFilms()
        {
            ViewBag.Films = new SelectList(db.Films, "Id", "Title");
            ViewBag.MovieStars = new SelectList(db.MovieStars, "Id", "FullName");

            if (Request.Form["Films"] != "")
            {
                var selectedFilm = db.Films.Find(Convert.ToInt32(Request.Form["Films"]));
                var avgRatingFilm = selectedFilm.Ratings.Any()
                    ? selectedFilm.Ratings.Select(x => x.Rating).Average() : 0;
                ViewBag.AvgRatingFilm = selectedFilm.Title + " - " + avgRatingFilm;
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult AvgHonorariums()
        {
            ViewBag.Films = new SelectList(db.Films, "Id", "Title");
            ViewBag.MovieStars = new SelectList(db.MovieStars, "Id", "FullName");

            if (Request.Form["MovieStars"] != "")
            {
                var selectedMS = db.MovieStars.Find(Convert.ToInt32(Request.Form["MovieStars"]));
                var avgHonorarium = selectedMS.ListOfFilmed.Any()
                    ? selectedMS.ListOfFilmed.Select(x => x.Honorarium).Average() : 0;
                ViewBag.AvgHonorarium = selectedMS.FullName + " - " + avgHonorarium + "$";
            }
            return View("Index");
        }
    }
}