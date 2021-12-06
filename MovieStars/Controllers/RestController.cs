using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using MovieStars.Models;

namespace MovieStars.Controllers
{
    public class RestController : ApiController
    {
        MovieStarsContext db = new MovieStarsContext();

        // GET: api/Rest
        /// <summary>
        /// Получить все записи таблицы MovieStar.
        /// </summary>
        /// <returns>Все записи таблицы.</returns>
        public IEnumerable<MovieStar> Get()
        {
            return db.MovieStars; 
        }

        // GET: api/Rest/{id}
        /// <summary>
        /// Получить конкретную запись таблицы MovieStar.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Найденная запись.</returns>
        public MovieStar GetMovieStarById(int id)
        {
            return db.MovieStars.Find(id);
        }

        // POST: api/Rest
        /// <summary>
        /// Добавить запись в таблицу MovieStar.
        /// </summary>
        /// <param name="movieStar">Добавляемая запись.</param>
        /// <returns>Результат добавления.</returns>
        public string CreateMovieStars([FromBody] MovieStar movieStar)
        {
            int lastId = db.MovieStars.OrderByDescending(x => x.Id).First().Id;
            movieStar.Id = ++lastId;
            db.MovieStars.Add(movieStar);
            db.SaveChanges();
            return "Запись успешно добавлена!";
        }


        // PUT: api/Rest/{id}
        /// <summary>
        /// Заменить запись в таблице MovieStar.
        /// </summary>
        /// <param name="id">ID заменяемой записи.</param>
        /// <param name="movieStar">Запись для замены.</param>
        /// <returns>Результат замены.</returns>
        [HttpPut]
        public string ReplaceMovieStars(int id, [FromBody] MovieStar movieStar)
        {
            if (id == movieStar.Id)
            {
                db.Entry(movieStar).State = EntityState.Modified;
                db.SaveChanges();
                return "Запись заменена";
            }
            return "Запись не найдена";
        }

        // DELETE: api/Rest/{id}
        /// <summary>
        /// Удалить запись из таблицы MovieStar.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Результат удаления.</returns>
        public string DeleteMovieStarsById(int id)
        {
            var movieStar = db.MovieStars.Find(id);
            if (movieStar == null)
            {
                return "Не найдена удаляемая запись";
            }
            db.MovieStars.Remove(movieStar);
            db.SaveChanges();
            return "Запись успешно удалена";
        }

        // PATCH: api/Rest/{id}
        /// <summary>
        /// Модифицировать запись в таблице MovieStar.
        /// </summary>
        /// <param name="id">ID модицифируемой записи.</param>
        /// <param name="movieStar">Модифицированные данные.</param>
        /// <returns>Результат модификации.</returns>
        [HttpPatch]
        public string ModifyMovieStars(int id, [FromBody] MovieStar movieStar)
        {
            if (id == movieStar.Id)
            {
                db.Entry(movieStar).State = EntityState.Modified;
                db.SaveChanges();
                return "Запись модифицирована";
            }
            return "Запись не найдена";
        }
    }
}
