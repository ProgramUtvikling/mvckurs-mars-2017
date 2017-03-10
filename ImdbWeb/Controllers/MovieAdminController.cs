using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using ImdbWeb.Helpers;
using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Security.Application;
using ImdbWeb.ViewModels.MovieAdminModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "MyAuthMiddleware")]
    public class MovieAdminController : ImdbControllerBase
    {
        public MovieAdminController(ImdbContext db) : base(db)
        {
        }

        public async Task<ViewResult> Index()
        {
            ViewData.Model = await Db.Movies
                .Select(m => new MovieAdminIndexModel {Id = m.MovieId, Title = m.Title })
                .ToListAsync();
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Genres = new SelectList(await Db.Genres.ToListAsync(), "GenreId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieAdminCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    MovieId = model.MovieId,
                    Title = model.Title,
                    OriginalTitle = model.OriginalTitle,
                    Description = model.Description,
                    ProductionYear = model.ProductionYear,
                    RunningLength = model.RunningLengthHours * 60 + model.RunningLengthMinutes,
                    GenreId = model.GenreId
                };
                Db.Movies.Add(movie);
                await Db.SaveChangesAsync();

                await Db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return await Create();
        }

        public static ValidationResult CheckId(string id, ValidationContext ctx)
        {
            var db = (ImdbContext)ctx.GetService(typeof(ImdbContext));
            if (db.Movies.Any(m => m.MovieId == id))
            {
                return new ValidationResult("Filmen er allerede registrert");
            }
            return ValidationResult.Success;
        }
    }
}
