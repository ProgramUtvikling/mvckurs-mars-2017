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
    public class MovieController : ImdbControllerBase
    {

        public MovieController(ImdbContext db) : base(db)
        {
        }

        public async Task<ViewResult> Index()
        {
            ViewData.Model = await Db.Movies.ToListAsync();
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            var movie = await Db.Movies.FindAsync(id);
            if(movie == null)
            {
                return NotFound();
            }

            ViewData.Model = movie;
            return View();
        }

        public async Task<ViewResult> Genres()
        {
            ViewData.Model = await Db.Genres.ToListAsync();
            return View();
        }

        [Route("Movie/Genre/{genrename}")]
        public async Task<ViewResult> MoviesByGenre(string genrename)
        {
            var movies = await Db.Movies.Where(m => m.Genre.Name == genrename).ToListAsync();
            ViewData.Model = movies.WithTitle(genrename);
            return View("Index");
        }
    }
}
