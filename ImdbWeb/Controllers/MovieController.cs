using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    public class MovieController : Controller
    {
        public string Index()
        {
            return "MovieController.Index()";
        }

        public string Details(string id)
        {
            return $"MovieController.Details({id})";
        }

        public string Genres()
        {
            return "MovieController.Genres()";
        }

        [Route("Movie/Genre/{genrename}")]
        public string MoviesByGenre(string genrename)
        {
            return $"MovieController.MoviesByGenre({genrename})";
        }
    }
}
