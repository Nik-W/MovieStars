using System;
using MovieStars.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nito.AsyncEx;

namespace MovieStars.Controllers
{
    public class HomeController : Controller
    {
        MovieStarsContext db = new MovieStarsContext();
        private readonly object locker1 = new object();
        private readonly object locker2 = new object();

        public async Task<ActionResult> Index()
        {
            var movieStars = await db.MovieStars.ToListAsync();
            ViewBag.MovieStars = movieStars;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Rewards(int? id)
        {
            var rewards = await db.Rewards.Where(x => x.MovieStar.Id == id).ToListAsync();
            ViewBag.Rewards = rewards;
            ViewBag.MovieStar = db.MovieStars.Find(id);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Roles(int? id)
        {
            var listOfFilmed = await db.ListOfFilmed.Where(x => x.MovieStar.Id == id).ToListAsync();
            ViewBag.Roles = listOfFilmed;
            ViewBag.MovieStar = db.MovieStars.Find(id);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Films(int? id)
        {
            var listOfFilmed = await db.ListOfFilmed.Where(x => x.MovieStar.Id == id).Include(x => x.Film.Director).ToListAsync();
            var films = new List<Film>();
            listOfFilmed.ForEach(x => films.Add(x.Film));
            ViewBag.Films = films;
            ViewBag.MovieStar = db.MovieStars.Find(id);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AllFilms()
        {
            var films = await db.Films.Include(x => x.Director).ToListAsync();
            ViewBag.Films = films;
            return View();  //возвращаем представление
        }

        [HttpGet]
        public async Task<ActionResult> Rating(int? id)
        {
            var ratingList = await db.FilmRatings.Where(x => x.Film.Id == id).Include(x => x.Critic).ToListAsync();
            ViewBag.Rating = ratingList;
            ViewBag.Film = db.Films.Find(id);
            return View();
        }

        [HttpGet]
        public ActionResult AddAsync()
        {
            var ind = 0;
            var maxCountThread = 5;
            var count = 100;

            var listMovieStar = GetListMovieStars(count);
            var listReward = GetListReward(count);
            var tasks = new List<Task>();
            var token = new CancellationTokenSource().Token;

            while (true)
            {
                var boundaryIndex = listMovieStar.Count - listMovieStar.Count / maxCountThread;
                if (ind < boundaryIndex)
                {
                    if (tasks.Count >= maxCountThread)
                    {
                        Task.Delay(TimeSpan.FromMilliseconds(100), token).Wait(token);
                        tasks.RemoveAll(t => t.IsCompleted || t.IsFaulted || t.IsCanceled);
                    }
                    else
                    {
                        var task = Task.Factory.StartNew(() => AddNewRow(listMovieStar, listReward, ref ind, maxCountThread), token);
                        tasks.Add(task);
                    }
                }
                else
                {
                    break;
                }
            }

            var movieStars = db.MovieStars;
            ViewBag.MovieStars = movieStars;
            return View("Index");
        }

        private void AddNewRow(List<MovieStar> listMovieStars, List<Reward> listRewards, ref int ind, int maxCountThread)
        {
            var dbAsync = new MovieStarsContext();
            var movieStars = listMovieStars.GetRange(ind, listMovieStars.Count / maxCountThread);
            var rewards = listRewards.GetRange(ind, listRewards.Count / maxCountThread);
            ind += listMovieStars.Count / maxCountThread;

            dbAsync.MovieStars.AddRange(movieStars);
            lock (locker1)
            {
                dbAsync.SaveChanges();
            }
            dbAsync.Rewards.AddRange(rewards);
            lock (locker2)
            {
                dbAsync.SaveChanges();
            }
        }

        private List<MovieStar> GetListMovieStars(int count)
        {
            var list = new List<MovieStar>();
            for (var i = 0; i < count; i++)
            {
                var star = new MovieStar { FullName = "Test" + i, Age = i * 10, FilmsCount = i * 5 };
                list.Add(star);
            }
            return list;
        }

        private List<Reward> GetListReward(int count)
        {
            var list = new List<Reward>();
            for (var i = 0; i < count; i++)
            {
                var star = new Reward { Title = "Reward" + i, DateOfPresentation = new DateTime(2000 + i, 01, 01) };
                list.Add(star);
            }
            return list;
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