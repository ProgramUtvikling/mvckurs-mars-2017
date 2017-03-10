using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using ImdbWeb.ViewModels;
using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Security.Application;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    //[Authorize(ActiveAuthenticationSchemes = "MyAuthMiddleware")]
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

            foreach (var comment in movie.Comments)
            {
                comment.Body = Sanitizer.GetSafeHtmlFragment(comment.Body);
            }


            ViewData.Model = movie;
            return View("Details");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(string id, string author, string headline, string body)
        {
            if (ModelState.IsValid)
            {
                var movie = await Db.Movies.FindAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }

                var comment = new Comment
                {
                    Author = author,
                    Headline = headline,
                    Body = body
                };

                movie.Comments.Add(comment);
                await Db.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id });
        }
    }
}
