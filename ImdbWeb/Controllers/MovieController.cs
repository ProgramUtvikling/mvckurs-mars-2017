using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using ImdbWeb.Helpers;
using System.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    public class MovieController : Controller
    {
        private readonly ImdbContext _db;

        public MovieController(ImdbContext db)
        {
            _db = db;
        }

        public async Task<ViewResult> Index()
        {
            ViewData.Model = await _db.Movies.ToListAsync();
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if(movie == null)
            {
                return NotFound();
            }

            ViewData.Model = movie;
            return View();
        }

        public async Task<ViewResult> Genres()
        {
            ViewData.Model = await _db.Genres.ToListAsync();
            return View();
        }

        [Route("Movie/Genre/{genrename}")]
        public async Task<ViewResult> MoviesByGenre(string genrename)
        {
            var movies = await _db.Movies.Where(m => m.Genre.Name == genrename).ToListAsync();
            ViewData.Model = movies.WithTitle(genrename);
            return View("Index");
        }
    }
}
