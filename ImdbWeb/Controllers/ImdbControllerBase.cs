using ImdbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Controllers
{
    public abstract class ImdbControllerBase : Controller
    {
        protected readonly ImdbContext Db;

        public ImdbControllerBase(ImdbContext db)
        {
            Db = db;
        }

    }
}
