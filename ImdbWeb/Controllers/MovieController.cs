using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using ImdbWeb.Helpers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    public class MovieController : Controller
    {
        private readonly ImdbContext Db;

        public MovieController()
        {
            Db = new ImdbContext(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Imdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Db != null)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ViewResult Index()
        {
            ViewData.Model = Db.Movies;
            return View();
        }

        public ViewResult Details(string id)
        {
            var movie = Db.Movies.Find(id);
            ViewData.Model = movie;
            return View();
        }

        public ViewResult Genres()
        {
            ViewData.Model = Db.Genres;
            return View();
        }

        [Route("Movie/Genre/{genrename}")]
        public ViewResult MoviesByGenre(string genrename)
        {
            var movies = Db.Movies.Where(m => m.Genre.Name == genrename);
            ViewData.Model = movies.WithTitle(genrename);
            return View("Index");
        }
    }
}
