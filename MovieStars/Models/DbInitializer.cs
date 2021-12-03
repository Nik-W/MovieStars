using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MovieStars.Models
{
 public class DbInitializer : DropCreateDatabaseIfModelChanges<MovieStarsContext>
    {
        protected override void Seed(MovieStarsContext db)
        {
            //без связей: кинозвезда, режисёр, критик
            MovieStarsInitializer(db.MovieStars);
            DirectorInitializer(db.Directors);
            CriticInitializer(db.Critics);

            //со связями: фильмы, снимается, награды, оценки
            FilmsInitializer(db);
            FilmedInitializer(db);
            RewardInitializer(db);
            RatingInitializer(db);

            base.Seed(db);
        }

        private void MovieStarsInitializer(DbSet<MovieStar> ms)
        {
            ms.Add(new MovieStar { Id = 1, FullName = "Роберт Джон Дауни-младший", Age = 55, FilmsCount = 107 });
            ms.Add(new MovieStar { Id = 2, FullName = "Джеки Чан", Age = 66, FilmsCount = 112 });
            ms.Add(new MovieStar { Id = 3, FullName = "Арнольд Алоис Шварценеггер", Age = 72, FilmsCount = 74 });
            ms.Add(new MovieStar { Id = 4, FullName = "Кристофер Майкл Прэтт", Age = 40, FilmsCount = 45 });
            ms.Add(new MovieStar { Id = 5, FullName = "Скарлетт Йоханссон", Age = 35, FilmsCount = 71 });
        }

        private void DirectorInitializer(DbSet<Director> d)
        {
            d.Add(new Director { Id = 1, FullName = "Джеймс Ганн", Age = 53, FilmsMadeCount = 21 });
            d.Add(new Director { Id = 2, FullName = "Джо Руссо", Age = 48, FilmsMadeCount = 60 });
            d.Add(new Director { Id = 3, FullName = "Тайка Вайтити", Age = 44, FilmsMadeCount = 41 });
        }

        private void CriticInitializer(DbSet<Critic> с)
        {
            с.Add(new Critic { Id = 1, FullName = "Андреев Иван Максимович", Rating = 75});
            с.Add(new Critic { Id = 2, FullName = "Пушков Дмитрий Анатольевич", Rating = 98 });
            с.Add(new Critic { Id = 3, FullName = "Сергеев Максим Иванович", Rating = 87 });
        }

        private void FilmsInitializer(MovieStarsContext db)
        {
            db.Films.Add(new Film
            {
                Id = 1,
                Title = "Стражи галактики",
                Genre = "Фантастика",
                ReleaseDate = new DateTime(2014, 04, 21),
                Director = db.Directors.Find(1)
            });
            db.Films.Add(new Film
            {
                Id = 2,
                Title = "Мстители: Финал",
                Genre = "Фантастика",
                ReleaseDate = new DateTime(2019, 04, 22),
                Director = db.Directors.Find(2)
            });
            db.Films.Add(new Film
            {
                Id = 3,
                Title = "Мстители",
                Genre = "Фантастика",
                ReleaseDate = new DateTime(2012, 05, 3),
                Director = db.Directors.Find(2)
            });
        }

        private void FilmedInitializer(MovieStarsContext db)
        {
            db.ListOfFilmed.Add(new Filmed
            {
                Id = 1,
                Honorarium = 200000,
                Role = "Звёздный лорд",
                Film = db.Films.Find(1),
                MovieStar = db.MovieStars.Find(4)
            });
            db.ListOfFilmed.Add(new Filmed
            {
                Id = 2,
                Honorarium = 250000,
                Role = "Чёрная вдова",
                Film = db.Films.Find(2),
                MovieStar = db.MovieStars.Find(5)
            });
            db.ListOfFilmed.Add(new Filmed
            {
                Id = 3,
                Honorarium = 370000,
                Role = "Железный человек",
                Film = db.Films.Find(2),
                MovieStar = db.MovieStars.Find(1)
            });
            db.ListOfFilmed.Add(new Filmed
            {
                Id = 4,
                Honorarium = 250000,
                Role = "Железный человек",
                Film = db.Films.Find(3),
                MovieStar = db.MovieStars.Find(1)
            });
        }

        private void RewardInitializer(MovieStarsContext db)
        {
            db.Rewards.Add(new Reward
            {
                Id = 1,
                Title = "Премия канала «MTV»",
                DateOfPresentation = new DateTime(2019, 01, 01),
                MovieStar = db.MovieStars.Find(1)
            });
            db.Rewards.Add(new Reward
            {
                Id = 2,
                Title = "Сатурн",
                DateOfPresentation = new DateTime(2019, 01, 01),
                MovieStar = db.MovieStars.Find(1)
            });

        }

        private void RatingInitializer(MovieStarsContext db)
        {
            db.FilmRatings.Add(new FilmRating
            {
                Id = 1,
                Critic = db.Critics.Find(1),
                Film = db.Films.Find(2),
                Rating = 86
            });
            db.FilmRatings.Add(new FilmRating
            {
                Id = 2,
                Critic = db.Critics.Find(2),
                Film = db.Films.Find(2),
                Rating = 83
            });
        }
    }
}