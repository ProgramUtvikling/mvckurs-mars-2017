﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using System.Xml.Linq;
using System.Data.Entity;
using System.Net;
using ImdbWeb.Models.WebAPI;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    public class ImdbApiController : ImdbControllerBase
    {
        public ImdbApiController(ImdbContext db)
            : base(db)
        {
        }

        public async Task<IActionResult> Movies(string fmt = "xml")
        {
            switch (fmt.ToLower())
            {
                case "xml": return await MoviesAsXml();
                case "json": return await MoviesAsJson();
                default:
                    return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        private async Task<IActionResult> MoviesAsXml()
        {
            var doc = new XElement("movies", from movie in await Db.Movies.ToListAsync()
                                             select new XElement("movie",
                                                 new XAttribute("id", movie.MovieId),
                                                 new XAttribute("title", movie.Title)
                                             )
                                  );

            return Content(doc.ToString(), "application/xml");
        }

        private async Task<IActionResult> MoviesAsJson()
        {
            var movies = from movie in await Db.Movies.ToListAsync()
                         select new { id = movie.MovieId, title = movie.Title };

            return Json(movies);
        }


        [Route("/Movie/Details/{id}.xml")]
        public async Task<IActionResult> MovieDetails(string id)
        {

            var movie = await Db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var doc = new XElement("movie",
                          new XAttribute("id", movie.MovieId),
                          new XAttribute("title", movie.Title),
                          new XAttribute("origTitle", movie.OriginalTitle),
                          new XAttribute("prodYear", movie.ProductionYear),
                          new XAttribute("runLen", movie.RunningLength),
                          from p in movie.Directors select new XElement("director", p.Name),
                          from p in movie.Producers select new XElement("producer", p.Name),
                          from p in movie.Actors select new XElement("actor", p.Name),
                          new XCData(movie.Description)
                      );

            return Content(doc.ToString(), "application/xml");

        }

        // The webAPI approach:
        [HttpGet]
        [Route("api/movies")]
        public IEnumerable<MovieIndexModel> Get()
        {
            return Db.Movies.Select(m => new MovieIndexModel { Id = m.MovieId, Title = m.Title, ProdYear = m.ProductionYear });
        }
    }
}
