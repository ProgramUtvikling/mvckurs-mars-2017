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
    public class PersonController : ImdbControllerBase
    {
        public PersonController(ImdbContext db) : base(db)
        {
        }

        public ViewResult Actors()
        {
            var persons = from person in Db.Persons
                          where person.ActedMovies.Any()
                          select person;
            ViewData.Model = persons.WithTitle("Skuespillere");
            return View("Index");
        }

        public ViewResult Producers()
        {
            var persons = from person in Db.Persons
                          where person.ProducedMovies.Any()
                          select person;
            ViewData.Model = persons.WithTitle("Produsenter");
            return View("Index");
        }

        public ViewResult Directors()
        {
            var persons = from person in Db.Persons
                          where person.DirectedMovies.Any()
                          select person;
            ViewData.Model = persons.WithTitle("Regisører");
            return View("Index");
        }

        [Route("Person/{id:int}")]
        public ViewResult Details(int id)
        {
            var person = Db.Persons.Find(id);
            ViewData.Model = person;
            return View();
        }
    }
}
